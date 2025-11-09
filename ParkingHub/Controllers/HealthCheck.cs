using Microsoft.AspNetCore.Mvc;

namespace ParkingHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetHealthCheck()
        {
            return Ok(new
            {
                status = "Ok",
                timestamp = DateTime.Now
            });
        }
    }
}
