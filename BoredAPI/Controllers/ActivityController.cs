using BoredAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BoredAPI.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Activity activities)
        {
            decimal access = (decimal)activities.accessibility;
            decimal price = (decimal)activities.price;
            string type = activities.type;
            int participants = activities.participants;


            var client = new HttpClient();

            // Get the URL of the Bored API
            var url = "http://www.boredapi.com/api/activity?";


            // Make a GET request to the URL
            var response = client.GetAsync(url).Result;

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Get the response body as a string
                var body = response.Content.ReadAsStringAsync().Result;

                // Deserialize the JSON response into a C# object
                var activity = JsonConvert.DeserializeObject<Activity>(body);

                // Print the activity to the console
                Console.WriteLine(activity.activity);
            }
            else
            {
                // The request failed
                Console.WriteLine("Error getting activity from Bored API");
            }
        }
    }
}
