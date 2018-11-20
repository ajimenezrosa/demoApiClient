using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.Language.Intermediate;

namespace DemoApiClient.Api
{
    public class Api
    {
        private const string apiKey = "PutYourAPIKey";
        private const string secret = "PutYourSecretKey";
        private const string endPoint = "PutYourAPIEndPoint";

        private string GetSignature()
        {
            // Compute the signature to be used in the API call (combined key + secret + timestamp in seconds)
            string signature;

            using (var sha = SHA256.Create())
            {
                var ts = (long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                          .TotalMilliseconds / 1000;

                var computedHash = sha.ComputeHash(Encoding.UTF8.GetBytes(apiKey + secret + ts));

                signature = BitConverter.ToString(computedHash).Replace("-", "");
            }

            return signature;
        }

        public async Task<string> GetApiMethod01()
        {
            string method01;
            const string operation = "Method01";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Signature", GetSignature());
                client.DefaultRequestHeaders.Add("Api-Key", apiKey);
                client.DefaultRequestHeaders.Add("Accept", "application/json");

                method01 = await client.GetStringAsync(endPoint + operation);
            }

            return method01;
        }

        public async Task<string> GetApiMethod02()
        {
            string method02;
            const string operation = "controller/method";
            const string parameters = "?param01=value&param02=value";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Signature", GetSignature());
                client.DefaultRequestHeaders.Add("Api-Key", apiKey);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                
                method02 = await client.GetStringAsync(endPoint + operation + parameters);
            }

            return method02;
        }
    }
}
