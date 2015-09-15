namespace RichardsTech.Sensors.Devices.LPS25H
{
	public static class LPS25HDefines
	{
		//  LPS25H I2C Slave Addresses

		public const byte ADDRESS0 = 0x5c;
		public const byte ADDRESS1 = 0x5d;
		public const byte REG_ID = 0x0f;
		public const byte ID = 0xbd;

		//	Register map

		public const byte REF_P_XL = 0x08;
		public const byte REF_P_XH = 0x09;
		public const byte RES_CONF = 0x10;
		public const byte CTRL_REG_1 = 0x20;
		public const byte CTRL_REG_2 = 0x21;
		public const byte CTRL_REG_3 = 0x22;
		public const byte CTRL_REG_4 = 0x23;
		public const byte INT_CFG = 0x24;
		public const byte INT_SOURCE = 0x25;
		public const byte STATUS_REG = 0x27;
		public const byte PRESS_OUT_XL = 0x28;
		public const byte PRESS_OUT_L = 0x29;
		public const byte PRESS_OUT_H = 0x2a;
		public const byte TEMP_OUT_L = 0x2b;
		public const byte TEMP_OUT_H = 0x2c;
		public const byte FIFO_CTRL = 0x2e;
		public const byte FIFO_STATUS = 0x2f;
		public const byte THS_P_L = 0x30;
		public const byte THS_P_H = 0x31;
		public const byte RPDS_L = 0x39;
		public const byte RPDS_H = 0x3a;
	}
}