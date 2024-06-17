using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bank_Api.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
