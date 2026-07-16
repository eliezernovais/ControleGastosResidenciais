using Microsoft.AspNetCore.Http;
using Backend.DTOs.Total;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalController(ITotalService service) : ControllerBase
    {
    }
}
