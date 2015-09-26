////////////////////////////////////////////////////////////////////////////
//
//  This file is part of RTIMULibCS
//
//  Copyright (c) 2015, richards-tech, LLC
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy of 
//  this software and associated documentation files (the "Software"), to deal in 
//  the Software without restriction, including without limitation the rights to use, 
//  copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the 
//  Software, and to permit persons to whom the Software is furnished to do so, 
//  subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all 
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A 
//  PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT 
//  HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION 
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE 
//  SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace RichardsTech.Sensors
{
    public class SensorFusionRTQF : SensorFusion
    {
        public SensorFusionRTQF()
        {
        }

        private void Predict()
        {
            if (!EnableGyro || !Gyro.HasValue) {
                return;
            }

            double qs = StateQ.Scalar;
            double qx = StateQ.X;
            double qy = StateQ.Y;
            double qz = StateQ.Z;

            double x2 = Gyro.Value.X / 2.0;
            double y2 = Gyro.Value.Y / 2.0;
            double z2 = Gyro.Value.Z / 2.0;

            // Predict new state

            double timeDeltaSeconds = TimeDelta.TotalSeconds;

            StateQ.Scalar = qs + (-x2 * qx - y2 * qy - z2 * qz) * timeDeltaSeconds;
            StateQ.X = qx + (x2 * qs + z2 * qy - y2 * qz) * timeDeltaSeconds;
            StateQ.Y = qy + (y2 * qs - z2 * qx + x2 * qz) * timeDeltaSeconds;
            StateQ.Z = qz + (z2 * qs + y2 * qx - x2 * qy) * timeDeltaSeconds;

            StateQ.Normalize();
        }

        private void Update()
        {
            if (!EnableMagneticField && !EnableAcceleration) {
                return;
            }

            // calculate rotation delta

            Quaternion rotationDelta = StateQ.Conjugate() * MeasuredQPose;
            rotationDelta.Normalize();

            // take it to the power (0 to 1) to give the desired amount of correction

            double theta = Math.Acos(rotationDelta.Scalar);

            double sinPowerTheta = Math.Sin(theta * SlerpPower);
            double cosPowerTheta = Math.Cos(theta * SlerpPower);

            Vector3 rotationUnitVector = new Vector3(rotationDelta.X, rotationDelta.Y, rotationDelta.Z);
            rotationUnitVector.Normalize();

            Quaternion rotationPower = new Quaternion(cosPowerTheta,
                sinPowerTheta * rotationUnitVector.X,
                sinPowerTheta * rotationUnitVector.Y,
                sinPowerTheta * rotationUnitVector.Z);
            rotationPower.Normalize();

            //  multiple this by predicted value to get result

            StateQ *= rotationPower;
            StateQ.Normalize();
        }

        internal override void ProcessNewImuReadings(ref SensorReadings imuReadings)
        {
            SampleNumber++;

            Gyro = EnableGyro && imuReadings.GyroValid
                ? (Vector3?)imuReadings.Gyro
                : null;

            Acceleration = EnableAcceleration && imuReadings.AccelerationValid
                ? (Vector3?)imuReadings.Acceleration
                : null;

            MagneticField = EnableMagneticField && imuReadings.MagneticFieldValid
                ? (Vector3?)imuReadings.MagneticField
                : null;

            if (FirstTime) {
                LastFusionTime = imuReadings.Timestamp;
                CalculatePose(Acceleration, MagneticField, CompassAdjDeclination);

                //  initialize the poses

                StateQ.FromEuler(MeasuredPose);
                FusionQPose = StateQ;
                FusionPose = MeasuredPose;
                FirstTime = false;
            } else {
                TimeDelta = imuReadings.Timestamp - LastFusionTime;
                if (TimeDelta > TimeSpan.Zero) {
                    CalculatePose(Acceleration, MagneticField, CompassAdjDeclination);
                    Predict();
                    Update();
                    StateQ.ToEuler(out FusionPose);
                    FusionQPose = StateQ;
                }

                LastFusionTime = imuReadings.Timestamp;
            }

            imuReadings.FusionPoseValid = true;
            imuReadings.FusionQPoseValid = true;
            imuReadings.FusionPose = FusionPose;
            imuReadings.FusionQPose = FusionQPose;
        }
    }
}
