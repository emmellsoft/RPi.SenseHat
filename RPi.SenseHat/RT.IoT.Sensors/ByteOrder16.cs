namespace RichardsTech.Sensors
{
	/// <summary>
	/// The byte order.
	/// </summary>
	public enum ByteOrder
	{
		/// <summary>
		/// Byte order of 0x1234: 0x12, 0x34.
		/// Byte order of 0x123456: 0x12, 0x34, 0x56.
		/// Byte order of 0x12345678: 0x12, 0x34, 0x56, 0x78.
		/// </summary>
		BigEndian,

		/// <summary>
		/// Byte order of 0x1234: 0x34, 0x12.
		/// Byte order of 0x123456: 0x56, 0x34, 0x12.
		/// Byte order of 0x12345678: 0x78, 0x56, 0x34, 0x12.
		/// </summary>
		LittleEndian
	}
}
