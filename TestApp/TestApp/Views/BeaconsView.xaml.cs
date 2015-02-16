using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace TestApp
{
	public partial class BeaconsView : ContentPage
	{
		public BeaconsView ()
		{
			InitializeComponent ();

			Padding = new Thickness(0, Device.OnPlatform(40, 20, 20), 0, 0);

			BindingContext = new BeaconsViewModel();
		}
	}
}

