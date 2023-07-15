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
        public IActionResult Index(Models.Activity activities)
        {
            decimal access = Convert.ToDecimal(activities.accessibility);
            decimal price = Convert.ToDecimal(activities.price);
            string type = activities.type;
            int participants = activities.participants;


            var client = new HttpClient();

            // Get the URL of the Bored API

            var url = "http://www.boredapi.com/api/activity?";
            url += $"minaccessibility={access}&minprice={price}&type={type}";


            // Make a GET request to the URL
            var response = client.GetAsync(url).Result;

            // Check if the request was successful
            if (response.IsSuccessStatusCode)
            {
                // Get the response body as a string
                var body = response.Content.ReadAsStringAsync().Result;

                var activity1 = JsonConvert.DeserializeObject<Activity>(body);

                // Check if the activity is null or contains empty values
                if (activity1 == null || string.IsNullOrEmpty(activity1.activity))
                {
                    // Handle the case when no activity is found
                    var errorMessage = "No activity found with the specified parameters.";
                    // Pass the error message to the view
                    return View("NotFound");
                }

                // Pass the activity to the view
                return View(activity1);


            }
            else
            {
                // The request failed
                Console.WriteLine("Error getting activity from Bored API");
                return View();
            }
        }
    }
}
