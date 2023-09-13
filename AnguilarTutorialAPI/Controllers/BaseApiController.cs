using AnguilarTutorialAPI.Data;
using AnguilarTutorialAPI.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace AnguilarTutorialAPI.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {
        }
    }
}
