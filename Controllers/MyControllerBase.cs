using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MyControllerBase(SimpleApiContext context) : ControllerBase
{
    protected readonly SimpleApiContext _context = context;
}