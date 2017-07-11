using System.Net.Http;
using System.Threading.Tasks;
using AddressSearchApp.entity;
using Newtonsoft.Json;

namespace AddressSearchApp.api {
    class APIAddress {
        public APIAddress() {
        }

        public static async Task<dynamic> GetDataFromService(string queryString) {
            dynamic data = null;
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);
            if (response == null) {
                return null;
            }
            if ((int)response.StatusCode >= 400) {
                return null;
            }
            string json = response.Content.ReadAsStringAsync().Result;
            data = JsonConvert.DeserializeObject(json);
            return data;
        }

        public static async Task<dynamic> GetAddressSearchResult(string address) {
            string pzNumber = address.Substring(0, 3);
            string taNumber = address.Substring(3);
            string queryString = "http://api.thni.net/jzip/X0401/JSON/" +
                pzNumber + "/" + taNumber + ".js";
            dynamic results = await GetDataFromService(queryString).ConfigureAwait(false);

            if (results == null) {
                return null;
            }
            if (string.IsNullOrEmpty((string)results["state"])) {
                return null;
            }
            Address Ad = new Address();
            Ad.state = (string)results["state"];
            Ad.stateName = (string)results["stateName"];
            Ad.city = (string)results["city"];
            Ad.street = (string)results["street"];
            return Ad;
        }
    }
}
