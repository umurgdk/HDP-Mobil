using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hdp.Droid.Services
{
    public class ParseService
    {
        public async Task<bool> CreateInstallation (string deviceToken)
        {
            var client = new HttpClient ();

            var json = JsonConvert.SerializeObject (new {
                deviceType = "android",
                pushType = "gcm",
                deviceToken = deviceToken,
                GCMSenderId = "1026338473197",
                channels = new string[] {
                    "newcontent"
                }
            }, Formatting.Indented, new JsonSerializerSettings {
                ContractResolver = new ParseContractResolver()
            });

            var request = new HttpRequestMessage (HttpMethod.Post, "https://api.parse.com/1/installations");
            request.Headers.Add ("X-Parse-Application-Id", "JWf43ZhWQFqlmVWvJ4czR4ZhjCqfafNsxVvIceXi");
            request.Headers.Add ("X-Parse-REST-API-Key", "9dudxBT7Af9frjKMYzUVbqe45pDzspHGqfTjIdMl");

            request.Content = new StringContent (json, System.Text.Encoding.UTF8, "application/json");

            var response = await client.SendAsync (request);

            return response.StatusCode == System.Net.HttpStatusCode.Created;
        }
    }

    public class ParseContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName (string propertyName)
        {
            return propertyName;
        }
    }
}

