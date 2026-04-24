using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;

namespace HelloService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class HelloController : ControllerBase
    {
        private readonly IMemoryCache _cache;

        private readonly List<Course> courseList = new List<Course>() { 
            new Course(){ Cname="DotnetFS", Fee=15000, Duration=5 },
            new Course(){ Cname="JavaFS", Fee=20000, Duration=5 },
            new Course(){ Cname="DevOps", Fee=10000, Duration=4 },
            new Course(){ Cname="Python", Fee=25000, Duration=6 },
        };
        public HelloController(IMemoryCache cache)
        {
            _cache = cache;
        }

        [HttpGet]
        [Route("CourseList")]
        [EnableRateLimiting("FixedPolicy")]
        public async Task<ActionResult> CoruseList()
        {
            if (_cache.TryGetValue("InMemCache", out List<Course>? courses))
            {
                Console.WriteLine("Im mem cache found.. courses found: " + courses.Count);
                return Ok(courses.ToList());
            }
            else
            {
                var cacheEntryPoints = new MemoryCacheEntryOptions()
                                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(1))
                                    .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                                    .SetPriority(CacheItemPriority.Normal);

                _cache.Set("InMemCache", this.courseList, cacheEntryPoints);

                await Task.Delay(5000);//assume the api is taking 10seconds to prepare output
                return Ok(this.courseList);
            }
            
           
        }

        [HttpGet]
        [Route("SayHello")]        
        public async Task<ActionResult> SayHello()
        {           
            return Ok("Hi All!! This is Hello Service");
        }


    }
    internal class Course
    {
        public string Cname { set; get; }
        public int Fee { set; get; }
        public int Duration { set; get; }
    }
}
