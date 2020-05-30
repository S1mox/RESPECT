using Newtonsoft.Json;
using RESPECT.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RESPECT.Services
{
    class Points_Service
    {
        const string ServiceURI = "https://respectapi.azurewebsites.net/api/points";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Points>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI);
            return JsonConvert.DeserializeObject<IEnumerable<Points>>(result);
        }

        public async Task<Points> GetPoints(int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI + "/" + id.ToString());
            return JsonConvert.DeserializeObject<Points>(result);
        }

        public async Task<Points> Add(Points points)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(ServiceURI,
                new StringContent(
                    JsonConvert.SerializeObject(points),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Points>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Points> PutPoints(Points points)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(ServiceURI + "/" + points.Id,
                new StringContent(
                    JsonConvert.SerializeObject(points),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Points>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Points> DeletePoints(Points points)
        {
            HttpClient client = GetClient();

            var response = await client.DeleteAsync(ServiceURI + "/" + points.Id);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Points>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
