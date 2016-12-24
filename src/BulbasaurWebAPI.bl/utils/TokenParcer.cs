using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BulbasaurWebAPI.bl.utils
{
    public class TokenParcer 
    {

        /*
         *  USAGE
         *  try
            {
                var ser = new TokenParcerService();
                var t = ser.GetUserIdFromToken(tokenResponse).GetAwaiter().GetResult();
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
         */

        public static int GetUserIdByToken(string Authorization)
        {
            var startIndex = Authorization.IndexOf(' ');
            var token = Authorization.Substring(startIndex, Authorization.Length - startIndex);
            var id = GetUserIdFromToken(token).GetAwaiter().GetResult();
            return id;
        }

        public static async Task<int> GetUserIdFromToken(string tokenResponse)
        {
            var userId = -1;

            // call api
            var client = new HttpClient();
            client.SetBearerToken(tokenResponse);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }

            var content = response.Content.ReadAsStringAsync().Result;

            JArray jsonVal = JArray.Parse(content) as JArray;
            dynamic array = jsonVal;

            foreach (dynamic items in array)
            {
                string type = items.type;
                if (!type.Equals("sub")) continue;
                string subValue = items.value;
                Int32.TryParse(subValue, out userId);
                break;
            }

            return userId;
        }
    }
}
