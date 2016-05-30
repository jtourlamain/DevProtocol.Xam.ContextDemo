using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using DevProtocol.Xam.ContextDemo.Mobile.Models;

namespace DevProtocol.Xam.ContextDemo.Mobile.Controllers
{
	public class MainController
	{
		public async Task<Endpoint> SearchIpAsync(Action<string> onError)
		{
			var result = new Endpoint();
			var jsonResponse = string.Empty;
			var context = SynchronizationContext.Current;
			try
			{

				using (var client = new HttpClient())
				{
					var dataResponse = await client.GetAsync("http://ip.jsontest.com/").ConfigureAwait(false);
					throw new Exception("I blew up");
					jsonResponse = await dataResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
				}
				var parsedResponse = await Task.Run(() => JsonConvert.DeserializeObject<Endpoint>(jsonResponse)).ConfigureAwait(false);
				result = parsedResponse;
			}
			catch (Exception ex)
			{
				//onError(ex.Message);
				context.Post(unused => { onError(ex.Message);},null);
			}

			return result;
		}
	}
}

