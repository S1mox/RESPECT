using Newtonsoft.Json;
using RESPECT.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RESPECT.Services
{
    class Rooms_Service
    {
        const string ServiceURI = "https://respectapi.azurewebsites.net/api/rooms";

        private HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<IEnumerable<Room>> Get()
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI);
            return JsonConvert.DeserializeObject<IEnumerable<Room>>(result);
        }

        public async Task<Room> GetRoom(int id)
        {
            HttpClient client = GetClient();
            string result = await client.GetStringAsync(ServiceURI + "/" + id.ToString());
            return JsonConvert.DeserializeObject<Room>(result);
        }

        public async Task<Room> Add(Room room)
        {
            HttpClient client = GetClient();
            var response = await client.PostAsync(ServiceURI,
                new StringContent(
                    JsonConvert.SerializeObject(room),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Room>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Room> PutRoom(Room room)
        {
            HttpClient client = GetClient();
            var response = await client.PutAsync(ServiceURI + "/" + room.Id,
                new StringContent(
                    JsonConvert.SerializeObject(room),
                    Encoding.UTF8, "application/json"));

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Room>(
                await response.Content.ReadAsStringAsync());
        }

        public async Task<Room> DeleteRoom(Room room)
        {
            HttpClient client = GetClient();

            var response = await client.DeleteAsync(ServiceURI + "/" + room.Id);

            if (response.StatusCode != HttpStatusCode.OK)
                return null;

            return JsonConvert.DeserializeObject<Room>(
               await response.Content.ReadAsStringAsync());
        }
    }
}
