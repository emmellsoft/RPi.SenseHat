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
	public struct Quaternion
	{
		public double Scalar
		{ get; set; }

		public double X
		{ get; set; }

		public double Y
		{ get; set; }

		public double Z
		{ get; set; }

		public Quaternion(double scalar, double x, double y, double z)
		{
			Scalar = scalar;
			X = x;
			Y = y;
			Z = z;
		}

		public double GetData(int i)
		{
			switch (i)
			{
				case 0:
					return Scalar;
				case 1:
					return X;
				case 2:
					return Y;
				case 3:
					return Z;
				default:
					throw new ArgumentException("Only 0-3 allowed", nameof(i));
			}
		}

		public static Quaternion operator +(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(
				lhs.Scalar + rhs.Scalar,
				lhs.X + rhs.X,
				lhs.Y + rhs.Y,
				lhs.Z + rhs.Z);
		}

		public static Quaternion operator -(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(
				lhs.Scalar - rhs.Scalar,
				lhs.X - rhs.X,
				lhs.Y - rhs.Y,
				lhs.Z - rhs.Z);
		}

		public static Quaternion operator *(Quaternion qa, Quaternion qb)
		{
			return new Quaternion(
				qa.Scalar * qb.Scalar - qa.X * qb.X - qa.Y * qb.Y - qa.Z * qb.Z,
				qa.Scalar * qb.X + qa.X * qb.Scalar + qa.Y * qb.Z - qa.Z * qb.Y,
				qa.Scalar * qb.Y - qa.X * qb.Z + qa.Y * qb.Scalar + qa.Z * qb.X,
				qa.Scalar * qb.Z + qa.X * qb.Y - qa.Y * qb.X + qa.Z * qb.Scalar);
		}

		public static Quaternion operator *(Quaternion lhs, double rhs)
		{
			return new Quaternion(lhs.Scalar * rhs, lhs.X * rhs, lhs.Y * rhs, lhs.Z * rhs);
		}

		public void Normalize()
		{
			double length = Math.Sqrt(Scalar * Scalar + X * X + Y * Y + Z * Z);

			if ((length == 0) || (length == 1))
			{
				return;
			}

			Scalar /= length;
			X /= length;
			Y /= length;
			Z /= length;
		}

		public void ToEuler(out Vector3 vec)
		{
			vec = new Vector3(
				Math.Atan2(2.0 * (Y * Z + Scalar * X), 1 - 2.0 * (X * X + Y * Y)),
				Math.Asin(2.0 * (Scalar * Y - X * Z)),
				Math.Atan2(2.0 * (X * Y + Scalar * Z), 1 - 2.0 * (Y * Y + Z * Z)));
		}

		public void FromEuler(Vector3 vec)
		{
			double cosX2 = Math.Cos(vec.X / 2.0f);
			double sinX2 = Math.Sin(vec.X / 2.0f);
			double cosY2 = Math.Cos(vec.Y / 2.0f);
			double sinY2 = Math.Sin(vec.Y / 2.0f);
			double cosZ2 = Math.Cos(vec.Z / 2.0f);
			double sinZ2 = Math.Sin(vec.Z / 2.0f);

			Scalar = cosX2 * cosY2 * cosZ2 + sinX2 * sinY2 * sinZ2;
			X = sinX2 * cosY2 * cosZ2 - cosX2 * sinY2 * sinZ2;
			Y = cosX2 * sinY2 * cosZ2 + sinX2 * cosY2 * sinZ2;
			Z = cosX2 * cosY2 * sinZ2 - sinX2 * sinY2 * cosZ2;
			Normalize();
		}

		public Quaternion Conjugate()
		{
			return new Quaternion(Scalar, -X, -Y, -Z);
		}

		public void ToAngleVector(out double angle, out Vector3 vec)
		{
			double halfTheta;
			double sinHalfTheta;

			halfTheta = Math.Acos(Scalar);
			sinHalfTheta = Math.Sin(halfTheta);

			vec = sinHalfTheta == 0
				? new Vector3(1.0, 0, 0)
				: new Vector3(X / sinHalfTheta, Y / sinHalfTheta, Z / sinHalfTheta);

			angle = 2.0 * halfTheta;
		}

		public void FromAngleVector(double angle, Vector3 vec)
		{
			double sinHalfTheta = Math.Sin(angle / 2.0);
			Scalar = Math.Cos(angle / 2.0);
			X = vec.X * sinHalfTheta;
			Y = vec.Y * sinHalfTheta;
			Z = vec.Z * sinHalfTheta;
		}

		public override string ToString()
		{
			return $"scalar: {Scalar:F4}, x: {X:F4}, y: {Y:F4}, z: {Z:F4}";
		}
	}
}
