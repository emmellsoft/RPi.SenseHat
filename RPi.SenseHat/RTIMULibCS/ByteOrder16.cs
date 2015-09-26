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
