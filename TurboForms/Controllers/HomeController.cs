using System.Diagnostics;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using TurboForms.Models;
using TurboForms.ViewModels;
using System.Runtime.Serialization.Json;

namespace TurboForms.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(new CreateOrder());
        }

        [HttpPost]
        public async Task<IActionResult> SubmitFormAsync(CreateOrder Order)
        {
            //send to shipday
            var res = await SendToShipday(Order);

            return View(res);
        }
        
        [HttpPost]
        public IActionResult GetPreview(CreateOrder Order)
        {
            return PartialView("_previewOrder", Order);
        }
        //GetPreview
        private async Task<bool> SendToShipday(CreateOrder Order)
        {
            using var client = new HttpClient();

            // Set the base address
            client.BaseAddress = new Uri("https://api.shipday.com/orders/");

            // Add headers
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", "J9UXhG9FNe.I7hPM9CN6m5mZCfncqLl");

            // Define the request payload
            var payload = new
            {
                orderNumber = Guid.NewGuid().ToString().Substring(0, 5),
                customerName = Order.CustomerName,
                customerAddress = Order.CustomerAdress + " " + Order.CustomerAdressDetails,
                customerPhoneNumber = Order.CustomerPhone,
                restaurantName = Order.Name,
                restaurantAddress = Order.Adress,
                restaurantPhoneNumber = Order.Phone,
                expectedDeliveryDate = Order.PickupDate.ToString("yyyy-MM-dd"),
                expectedPickupTime = Order.PickupDate.ToString("HH:mm:ss"),
                deliveryFee = Order.DeliveryFee,
                totalOrderCost = Order.Total,
                deliveryInstruction = Order.OrderStatus ? "قابلة للكسر" : "غير قابلة للكسر",
                paymentMethod = Order.PaymentMethod,
                orderItem = new[]
                {
                    new { name = "order", quantity = 1, unitPrice = Order.OrderPrice, detail = "" }
                }
            };

            // Serialize the payload to JSON
            var jsonPayload = JsonSerializer.Serialize(payload);

            // Create an HTTP content object
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            // Make the POST request
            HttpResponseMessage response = await client.PostAsync("", content);

            // Read and display the response
            string responseContent = await response.Content.ReadAsStringAsync();
            var ResultData = JsonSerializer.Deserialize<ShipdayResponse>(responseContent);

            return ResultData.success;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
