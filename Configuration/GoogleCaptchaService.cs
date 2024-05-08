using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace GoogleReCaptcha.Configuration
{
    public class GoogleCaptchaService
    {
        private readonly IOptionsMonitor<GoogleCaptchaConfig> _config;
        public GoogleCaptchaService(IOptionsMonitor<GoogleCaptchaConfig> config )
        {
            _config = config;
        }
        public async Task<bool> VerifyToken(string token)
        {
            try
            {
                var url = $"https://www.google.com/recaptcha/api/siteverify";

                var requestData = new Dictionary<string, string>
                {
                    { "secret", _config.CurrentValue.SecretKey },
                    { "response", token }
                };

                using(var client = new HttpClient())
                {
                    var content = new FormUrlEncodedContent(requestData) ;
                    var httpRequest = await client.PostAsync(url, content);
                    if(httpRequest.StatusCode != HttpStatusCode.OK)
                    {
                        return false;
                    }
                    var responseString = await httpRequest.Content.ReadAsStringAsync();
                    var googleResult = JsonConvert.DeserializeObject<GoogleCaptchaResponse>(responseString);
                    return googleResult.success && googleResult.score >= 0.5;
                }

            }
            catch(Exception ex) 
            { 
                Console.WriteLine(ex);
                return false;
            }
            

        }
    }
}
