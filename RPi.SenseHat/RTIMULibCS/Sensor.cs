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
using System.Threading.Tasks;

namespace RichardsTech.Sensors
{
	/// <summary>
	/// A generic sensor.
	/// </summary>
	public abstract class Sensor : IDisposable
	{
		protected Sensor()
		{
		}

		public virtual void Dispose()
		{
		}

		/// <summary>
		/// Has the sensor been initialized?
		/// </summary>
		public bool Initiated
		{ get; private set; }

		/// <summary>
		/// The last sensor readings.
		/// </summary>
		public SensorReadings Readings
		{ get; private set; }

		/// <summary>
		/// Event fired when the readings has changed.
		/// </summary>
		public event EventHandler OnReadingsChanged;

		/// <summary>
		/// Initiates the sensor.
		/// If failing, an exception is thrown.
		/// </summary>
		public async Task InitAsync()
		{
			if (Initiated)
			{
				return;
			}

			bool initiated = await InitDeviceAsync();

			if (initiated)
			{
				AfterInitDevice();
			}

			Initiated = initiated;
		}

		protected abstract Task<bool> InitDeviceAsync();

		protected virtual void AfterInitDevice()
		{
		}

		protected void AssignNewReadings(SensorReadings readings, bool processReadings = true)
		{
			if (processReadings)
			{
				ProcessReadings(ref readings);
			}

			Readings = readings;

			OnReadingsChanged?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void ProcessReadings(ref SensorReadings readings)
		{
		}

		/// <summary>
		/// Tries to update the readings.
		/// Returns true if new readings are available, otherwise false.
		/// An exception is thrown if something goes wrong.
		/// </summary>
		public abstract bool Update();
	}
}