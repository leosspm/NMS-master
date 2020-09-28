using KERRY.NMS.CORE;
using KERRY.NMS.MODEL.ExternalModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KERRY.NMS.BL.ExternalApi
{
    internal class ExternalApiBase
    {
        private readonly HttpClient httpClient;
        private readonly string groupApi;

        protected ExternalApiBase(HttpClient httpClient, string groupApi)
        {
            this.httpClient = httpClient;
            this.groupApi = groupApi;
        }

        protected async Task<T> PostAsync<T>(string action, Dictionary<string, string> urlParams = null, dynamic dataBody = null)
        {
            string fullUrl = $"{groupApi}/{action}";
            MapParamsUrl(ref fullUrl, urlParams);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
            if (dataBody != null)
            {
                request.Content = new StringContent(dataBody.ToJsonCamel(), Encoding.UTF8, "application/json");
            }
            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                BaseExternalModel<T> baseData = JsonConvert.DeserializeObject<BaseExternalModel<T>>(data);
                return baseData.Data;
            }
            return default(T);
        }

        protected async Task<T> GetAsync<T>(string action, Dictionary<string, string> urlParams = null)
        {
            string fullUrl = $"{groupApi}/{action}";
            MapParamsUrl(ref fullUrl, urlParams);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                BaseExternalModel<T> baseData = JsonConvert.DeserializeObject<BaseExternalModel<T>>(data);
                return baseData.Data;
            }
            return default(T);
        }

        private string MapParamsUrl(ref string fullUrl, Dictionary<string, string> urlParams)
        {
            if (urlParams != null && urlParams.Count > 0)
            {
                fullUrl += "?";
                foreach (var par in urlParams)
                {
                    if (!string.IsNullOrEmpty(par.Key))
                    {
                        fullUrl += $"{par.Key}={par.Value}&";
                    }
                }
                fullUrl = fullUrl.Remove(fullUrl.Length - 1, 1);
            }
            return fullUrl;
        }
    }
}
