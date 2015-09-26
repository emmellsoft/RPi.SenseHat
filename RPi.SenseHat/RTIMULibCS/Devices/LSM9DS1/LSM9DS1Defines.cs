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

namespace RichardsTech.Sensors.Devices.LSM9DS1
{
	public static class LSM9DS1Defines
	{
		public const byte ADDRESS0 = 0x6a;
		public const byte ADDRESS1 = 0x6b;
		public const byte ID = 0x68;

		public const byte MAG_ADDRESS0 = 0x1c;
		public const byte MAG_ADDRESS1 = 0x1d;
		public const byte MAG_ADDRESS2 = 0x1e;
		public const byte MAG_ADDRESS3 = 0x1f;
		public const byte MAG_ID = 0x3d;

		//  LSM9DS1 Register map

		public const byte ACT_THS = 0x04;
		public const byte ACT_DUR = 0x05;
		public const byte INT_GEN_CFG_XL = 0x06;
		public const byte INT_GEN_THS_X_XL = 0x07;
		public const byte INT_GEN_THS_Y_XL = 0x08;
		public const byte INT_GEN_THS_Z_XL = 0x09;
		public const byte INT_GEN_DUR_XL = 0x0A;
		public const byte REFERENCE_G = 0x0B;
		public const byte INT1_CTRL = 0x0C;
		public const byte INT2_CTRL = 0x0D;
		public const byte WHO_AM_I = 0x0F;
		public const byte CTRL1 = 0x10;
		public const byte CTRL2 = 0x11;
		public const byte CTRL3 = 0x12;
		public const byte ORIENT_CFG_G = 0x13;
		public const byte INT_GEN_SRC_G = 0x14;
		public const byte OUT_TEMP_L = 0x15;
		public const byte OUT_TEMP_H = 0x16;
		public const byte STATUS = 0x17;
		public const byte OUT_X_L_G = 0x18;
		public const byte OUT_X_H_G = 0x19;
		public const byte OUT_Y_L_G = 0x1A;
		public const byte OUT_Y_H_G = 0x1B;
		public const byte OUT_Z_L_G = 0x1C;
		public const byte OUT_Z_H_G = 0x1D;
		public const byte CTRL4 = 0x1E;
		public const byte CTRL5 = 0x1F;
		public const byte CTRL6 = 0x20;
		public const byte CTRL7 = 0x21;
		public const byte CTRL8 = 0x22;
		public const byte CTRL9 = 0x23;
		public const byte CTRL10 = 0x24;
		public const byte INT_GEN_SRC_XL = 0x26;
		public const byte STATUS2 = 0x27;
		public const byte OUT_X_L_XL = 0x28;
		public const byte OUT_X_H_XL = 0x29;
		public const byte OUT_Y_L_XL = 0x2A;
		public const byte OUT_Y_H_XL = 0x2B;
		public const byte OUT_Z_L_XL = 0x2C;
		public const byte OUT_Z_H_XL = 0x2D;
		public const byte FIFO_CTRL = 0x2E;
		public const byte FIFO_SRC = 0x2F;
		public const byte INT_GEN_CFG_G = 0x30;
		public const byte INT_GEN_THS_XH_G = 0x31;
		public const byte INT_GEN_THS_XL_G = 0x32;
		public const byte INT_GEN_THS_YH_G = 0x33;
		public const byte INT_GEN_THS_YL_G = 0x34;
		public const byte INT_GEN_THS_ZH_G = 0x35;
		public const byte INT_GEN_THS_ZL_G = 0x36;
		public const byte INT_GEN_DUR_G = 0x37;

		//  Mag Register Map

		public const byte MAG_OFFSET_X_L = 0x05;
		public const byte MAG_OFFSET_X_H = 0x06;
		public const byte MAG_OFFSET_Y_L = 0x07;
		public const byte MAG_OFFSET_Y_H = 0x08;
		public const byte MAG_OFFSET_Z_L = 0x09;
		public const byte MAG_OFFSET_Z_H = 0x0A;
		public const byte MAG_WHO_AM_I = 0x0F;
		public const byte MAG_CTRL1 = 0x20;
		public const byte MAG_CTRL2 = 0x21;
		public const byte MAG_CTRL3 = 0x22;
		public const byte MAG_CTRL4 = 0x23;
		public const byte MAG_CTRL5 = 0x24;
		public const byte MAG_STATUS = 0x27;
		public const byte MAG_OUT_X_L = 0x28;
		public const byte MAG_OUT_X_H = 0x29;
		public const byte MAG_OUT_Y_L = 0x2A;
		public const byte MAG_OUT_Y_H = 0x2B;
		public const byte MAG_OUT_Z_L = 0x2C;
		public const byte MAG_OUT_Z_H = 0x2D;
		public const byte MAG_INT_CFG = 0x30;
		public const byte MAG_INT_SRC = 0x31;
		public const byte MAG_INT_THS_L = 0x32;
		public const byte MAG_INT_THS_H = 0x33;
	}
}