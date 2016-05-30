using System;
using Android.App;
using Android.Widget;
using Android.OS;
using DevProtocol.Xam.ContextDemo.Mobile.Controllers;

namespace DevProtocol.Xam.ContextDemo.Droid
{
	[Activity(Label = "ContextDemo", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		private MainController _controller;
		private TextView _txtResult;


		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);
			_controller = new MainController();
			Button button = FindViewById<Button>(Resource.Id.myButton);
			button.Click += SearchAsync;
			_txtResult = FindViewById<TextView>(Resource.Id.txt_Result);


		}

		private async void SearchAsync(object sender, EventArgs e)
		{
			var endpoint = await _controller.SearchIpAsync(AnErrorOccured);
			if (!string.IsNullOrEmpty(endpoint.ip))
				_txtResult.Text = $"Your IP is: {endpoint.ip}";
		}

		private void AnErrorOccured(string message)
		{
			// Only the original thread that created a view hierarchy can touch its view
			// If you don't use the SynchronizationContext in the controller and forget the RunOnUiThread you get an AndroidRuntimeException 
			//RunOnUiThread(() =>
			//{
			//	_txtResult.Text = message;
			//});


			// Statement below won't cause problems if you've used the SynchronizationContext in the controller
			_txtResult.Text = message;
		}
	}
}


