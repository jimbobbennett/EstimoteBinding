using System;

using TestApp.Interfaces;
using Xamarin.Forms;
using System.Collections.Generic;

namespace TestApp
{
	public class App : Application
	{
		private List<Guid> _uuids = new List<Guid>
		{
			Guid.Parse("b9407f30-f5f8-466e-aff9-25556b57fe6d")
		};

		public App ()
		{
			MainPage = new BeaconsView();
		}

		protected override void OnStart ()
		{
			DependencyService.Get<IBeaconDiscovery>().StartLookingForBeacons(_uuids);
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

