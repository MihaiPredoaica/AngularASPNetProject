using AnguilarTutorialAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace AnguilarTutorialAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {
        }
    }
}
