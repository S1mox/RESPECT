using Newtonsoft.Json;
using RESPECT.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RESPECT.Services
{
    class Notificatins_Service
    {
        const string ServiceURI = "https://respectapi.azurewebsites.net/api/notifications";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Notification>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI);
            return JsonConvert.DeserializeObject<IEnumerable<Notification>>(result);
        }

        public async Task<Notification> GetNotification(int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI + "/" + id.ToString());
            return JsonConvert.DeserializeObject<Notification>(result);
        }

        public async Task<Notification> Add(Notification notification)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(ServiceURI,
                new StringContent(
                    JsonConvert.SerializeObject(notification),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Notification>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Notification> PutNotification(Notification notification)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(ServiceURI + "/" + notification.Id,
                new StringContent(
                    JsonConvert.SerializeObject(notification),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Notification>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Notification> DeleteNotifications(Notification notification)
        {
            HttpClient client = GetClient();

            var response = await client.DeleteAsync(ServiceURI + "/" + notification.Id);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Notification>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
