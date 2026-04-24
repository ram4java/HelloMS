using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace HelloCaller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GreetingsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClient;

        private readonly IMemoryCache _cache;

        private readonly List<Product> products = new List<Product>() { 
            new Product{ Pname="Mobile", Price=20000},
            new Product{ Pname="Television", Price=50000},
            new Product{ Pname="Laptop", Price=70000}

        };

        public GreetingsController(IHttpClientFactory httpClientFactory, IMemoryCache cache) { 
            _httpClient = httpClientFactory;
            _cache = cache;
        }


        [HttpGet]
        [Route("GreetMe")]
        public async Task<ActionResult> GreetMe(){
           
            return Ok("Greetings from Microservices App");
        }

    }
    internal class Product { 
        public string Pname { set; get; }
        public int Price { set; get; }
    }
}
