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
	/// <summary>
	/// This class allow the IMU to be virtually repositioned if it is in a non-standard configuration
	/// Standard configuration is X pointing at north, Y pointing east and Z pointing down
	/// with the IMU horizontal. There are 24 different possible orientations as defined
	/// below. Setting the axis rotation code to non-zero values performs the repositioning.</summary>
	public class AxisRotation
	{
		private static readonly int[,] AxisRotationArray =
		{
			{1, 0, 0, 0, 1, 0, 0, 0, 1},                    // XNORTH_YEAST
			{0, -1, 0, 1, 0, 0, 0, 0, 1},                   // XEAST_YSOUTH
			{-1, 0, 0, 0, -1, 0, 0, 0, 1},                  // XSOUTH_YWEST
			{0, 1, 0, -1, 0, 0, 0, 0, 1},                   // XWEST_YNORTH

			{1, 0, 0, 0, -1, 0, 0, 0, -1},                  // XNORTH_YWEST
			{0, 1, 0, 1, 0, 0, 0, 0, -1},                   // XEAST_YNORTH
			{-1, 0, 0, 0, 1, 0, 0, 0, -1},                  // XSOUTH_YEAST
			{0, -1, 0, -1, 0, 0, 0, 0, -1},                 // XWEST_YSOUTH

			{0, 1, 0, 0, 0, -1, -1, 0, 0},                  // XUP_YNORTH
			{0, 0, 1, 0, 1, 0, -1, 0, 0},                   // XUP_YEAST
			{0, -1, 0, 0, 0, 1, -1, 0, 0},                  // XUP_YSOUTH
			{0, 0, -1, 0, -1, 0, -1, 0, 0},                 // XUP_YWEST

			{0, 1, 0, 0, 0, 1, 1, 0, 0},                    // XDOWN_YNORTH
			{0, 0, -1, 0, 1, 0, 1, 0, 0},                   // XDOWN_YEAST
			{0, -1, 0, 0, 0, -1, 1, 0, 0},                  // XDOWN_YSOUTH
			{0, 0, 1, 0, -1, 0, 1, 0, 0},                   // XDOWN_YWEST

			{1, 0, 0, 0, 0, 1, 0, -1, 0},                   // XNORTH_YUP
			{0, 0, -1, 1, 0, 0, 0, -1, 0},                  // XEAST_YUP
			{-1, 0, 0, 0, 0, -1, 0, -1, 0},                 // XSOUTH_YUP
			{0, 0, 1, -1, 0, 0, 0, -1, 0},                  // XWEST_YUP

			{1, 0, 0, 0, 0, -1, 0, 1, 0},                   // XNORTH_YDOWN
			{0, 0, 1, 1, 0, 0, 0, 1, 0},                    // XEAST_YDOWN
			{-1, 0, 0, 0, 0, 1, 0, 1, 0},                   // XSOUTH_YDOWN
			{0, 0, -1, -1, 0, 0, 0, 1, 0}                   // XWEST_YDOWN
		};

		private AxisDirection _direction;
		private Func<Vector3, double> _xConverter;
		private Func<Vector3, double> _yConverter;
		private Func<Vector3, double> _zConverter;

		public AxisRotation()
		{
			Direction = AxisDirection.Default;
		}

		/// <summary>
		/// The current direction of the axis.
		/// </summary>
		public AxisDirection Direction
		{
			get
			{
				return _direction;
			}
			set
			{
				if ((value >= AxisDirection.XNorthYEast) && (value <= AxisDirection.XWestYDown))
				{
					_direction = value;
				}
				else
				{
					_direction = AxisDirection.Default;
				}

				SetConverterFunctions();
			}
		}

		private void SetConverterFunctions()
		{
			int axisRotationValue = (int)_direction;

			// Get converter function for the X-value
			if (AxisRotationArray[axisRotationValue, 0] != 0)
			{
				_xConverter = v => v.X * AxisRotationArray[axisRotationValue, 0];
			}
			else if (AxisRotationArray[axisRotationValue, 1] != 0)
			{
				_xConverter = v => v.Y * AxisRotationArray[axisRotationValue, 1];
			}
			else if (AxisRotationArray[axisRotationValue, 2] != 0)
			{
				_xConverter = v => v.Z * AxisRotationArray[axisRotationValue, 2];
			}
			else
			{
				_xConverter = v => v.X;
			}

			// Get converter function for the Y-value
			if (AxisRotationArray[axisRotationValue, 3] != 0)
			{
				_yConverter = v => v.X * AxisRotationArray[axisRotationValue, 3];
			}
			else if (AxisRotationArray[axisRotationValue, 4] != 0)
			{
				_yConverter = v => v.Y * AxisRotationArray[axisRotationValue, 4];
			}
			else if (AxisRotationArray[axisRotationValue, 5] != 0)
			{
				_yConverter = v => v.Z * AxisRotationArray[axisRotationValue, 5];
			}
			else
			{
				_yConverter = v => v.Y;
			}

			// Get converter function for the Z-value
			if (AxisRotationArray[axisRotationValue, 6] != 0)
			{
				_zConverter = v => v.X * AxisRotationArray[axisRotationValue, 6];
			}
			else if (AxisRotationArray[axisRotationValue, 7] != 0)
			{
				_zConverter = v => v.Y * AxisRotationArray[axisRotationValue, 7];
			}
			else if (AxisRotationArray[axisRotationValue, 8] != 0)
			{
				_zConverter = v => v.Z * AxisRotationArray[axisRotationValue, 8];
			}
			else
			{
				_zConverter = v => v.Z;
			}
		}

		/// <summary>
		/// Rotates a vector according to the current axis directions.
		/// </summary>
		/// <param name="vector">The vector to rotate.</param>
		public Vector3 Rotate(Vector3 vector)
		{
			Vector3 original = vector;

			vector.X = _xConverter(original);
			vector.Y = _yConverter(original);
			vector.Z = _zConverter(original);

			return vector;
		}
	}
}