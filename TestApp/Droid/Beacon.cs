using System;
using TestApp.Interfaces;
using Xamarin.Forms;

namespace TestApp.Droid
{
	public class Beacon : BeaconBase
	{
		public Beacon(Guid proximityId, int major, int minor)
			: base(proximityId, major, minor)
		{
		}

		internal void SetDistance(int txPower, double rssi)
		{
			Distance = CalculateAccuracy(txPower, rssi);
		}

		private static double CalculateAccuracy(int txPower, double rssi) 
		{
			if (rssi == 0) 
			{
				return -1.0; // if we cannot determine accuracy, return -1.
			}

			var ratio_db = txPower - rssi;
			var ratio_linear = Math.Pow(10, ratio_db / 10);

			var r = Math.Sqrt(ratio_linear);
			return r;
		}
	}
}

