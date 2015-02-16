using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestApp
{
	public class ViewModelBase : INotifyPropertyChanged
	{
		protected bool SetProperty<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
		{
			if (Equals(property, value)) return false;

			property = value;
			OnPropertyChanged(propertyName);
			return true;
		}

		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion
	}
}

