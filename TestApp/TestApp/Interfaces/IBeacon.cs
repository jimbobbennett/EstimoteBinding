using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace TestApp.Interfaces
{
	public interface IBeacon : INotifyPropertyChanged
	{
		int Major { get; }
		int Minor { get; }
		Guid ProximityUUID { get; }
		double Distance { get; }
		Color Color { get; }
	}
}

