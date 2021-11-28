using API.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /*
        Base controller class
        All controllers must inherit from this
    */
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
    }
}