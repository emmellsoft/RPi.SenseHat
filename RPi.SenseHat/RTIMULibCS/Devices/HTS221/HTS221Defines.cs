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

namespace RichardsTech.Sensors.Devices.HTS221

{
	public static class HTS221Defines
	{
		//  HTS221 I2C Slave Address

		public const byte ADDRESS = 0x5f;
		public const byte REG_ID = 0x0f;
		public const byte ID = 0xbc;

		//  Register map

		public const byte WHO_AM_I = 0x0f;
		public const byte AV_CONF = 0x10;
		public const byte CTRL1 = 0x20;
		public const byte CTRL2 = 0x21;
		public const byte CTRL3 = 0x22;
		public const byte STATUS = 0x27;
		public const byte HUMIDITY_OUT_L = 0x28;
		public const byte HUMIDITY_OUT_H = 0x29;
		public const byte TEMP_OUT_L = 0x2a;
		public const byte TEMP_OUT_H = 0x2b;
		public const byte H0_H_2 = 0x30;
		public const byte H1_H_2 = 0x31;
		public const byte T0_C_8 = 0x32;
		public const byte T1_C_8 = 0x33;
		public const byte T1_T0 = 0x35;
		public const byte H0_T0_OUT = 0x36;
		public const byte H1_T0_OUT = 0x3a;
		public const byte T0_OUT = 0x3c;
		public const byte T1_OUT = 0x3e;
	}
}