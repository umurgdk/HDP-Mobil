using System;
using Fusillade;
using System.Net.Http;
using Refit;
using ModernHttpClient;

namespace Hdp.CoreRx.Services
{
    public class ApiService : IApiService
    {
        public static string ApiBaseAddress = "http://192.168.0.10:3000";
        public static DeviceType Device = DeviceType.ios;

        public ApiService (string apiBaseAddress = "http://192.168.0.10:3000", DeviceType deviceType = DeviceType.ios)
        {
            Device = deviceType;
            ApiBaseAddress = apiBaseAddress;

            Func<HttpMessageHandler, IHDPApiService> createClient = messageHandler => 
            {
                var client = new HttpClient(messageHandler) {
                    BaseAddress = new Uri(ApiBaseAddress)
                };

                return RestService.For<IHDPApiService>(client);
            };

            _background = new Lazy<IHDPApiService> (() => createClient (new RateLimitedHttpMessageHandler (new NativeMessageHandler (), Priority.Background)));
            _userInitiated = new Lazy<IHDPApiService> (() => createClient (new RateLimitedHttpMessageHandler (new NativeMessageHandler (), Priority.UserInitiated)));
            _speculative = new Lazy<IHDPApiService> (() => createClient (new RateLimitedHttpMessageHandler (new NativeMessageHandler (), Priority.Speculative)));
        }

        private readonly Lazy<IHDPApiService> _background;
        private readonly Lazy<IHDPApiService> _userInitiated;
        private readonly Lazy<IHDPApiService> _speculative;

        #region IApiService implementation
        public IHDPApiService Background {
            get {
                return _background.Value;
            }
        }
        public IHDPApiService UserInitiated {
            get {
                return _userInitiated.Value;
            }
        }
        public IHDPApiService Speculative {
            get {
                return _speculative.Value;
            }
        }
        #endregion
    }
}

