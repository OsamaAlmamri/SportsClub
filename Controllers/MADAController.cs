using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace SportsClub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MADAController : ControllerBase
    {
        [HttpGet("create")]
        public IActionResult CreatePayment(PaymentService paymentService)
        {
          var r=  paymentService.RequestPayment();

            return Ok(new { r});
        }

        public class PaymentService
        {
            private readonly HttpClient httpClient;
            private readonly IConfiguration configuration;

            public PaymentService(HttpClient httpClient, IConfiguration configuration)
            {
                this.httpClient = httpClient;
                this.configuration = configuration.GetSection("oppwa"); ;
            }

            public async Task<string> RequestPayment()
            {
                var requestData = new Dictionary<string, string>
        {
            { "entityId", "8a8294174b7ecb28014b9699220015ca" },
            { "amount", "10.00" },
            { "currency", "EUR" },
            { "testMode", "EXTERNAL" },
            { "paymentType", "PA" },
            { "paymentBrand", "MADA" },
            { "card.cvv", "742" },
            { "card.holder", "Jane Jones" },
            { "card.number", "5297411013651575" },
            { "card.expiryYear", "2025" },
            { "card.expiryMonth", "04" }
        };

                var url = "https://eu-test.oppwa.com/v1/payments";
                var content = new FormUrlEncodedContent(requestData);

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", configuration["ApiToken"]);

                var response = await httpClient.PostAsync(url, content);
                var responseString = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(responseString);
                var description = responseData["result"]["description"].ToString();

                return description;
            }
        }


    }
}
