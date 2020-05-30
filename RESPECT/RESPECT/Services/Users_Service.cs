using Newtonsoft.Json;
using RESPECT.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RESPECT.Services
{
    class Users_Service
    {
        const string ServiceURI = "https://respectapi.azurewebsites.net/api/users";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<User>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI);
            return JsonConvert.DeserializeObject<IEnumerable<User>>(result);
        }

        public async Task<User> GetUser(int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI + "/" + id.ToString());
            return JsonConvert.DeserializeObject<User>(result);
        }

        public async Task<User> Add(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(ServiceURI,
                new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<User> PutUser(User user)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(ServiceURI + "/" + user.Id,
                new StringContent(
                    JsonConvert.SerializeObject(user),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<User> DeleteUser(User user)
        {
            HttpClient client = GetClient();

            var response = await client.DeleteAsync(ServiceURI + "/" + user.Id);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<User>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
