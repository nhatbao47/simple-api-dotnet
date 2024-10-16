using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimpleApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MyControllerBase(SimpleApiContext context, IMapper mapper) : ControllerBase
{
    protected readonly SimpleApiContext _context = context;
    protected readonly IMapper _mapper = mapper;
}