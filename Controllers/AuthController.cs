using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SportsClub.DataTransferObjects;
using SportsClub.Models;
using SportsClub.Models.Repositores;
using AutoMapper;
using SportsClub.Core.Requests;
using SportsClub.Core.Pagination.Filter;
using SportsClub.Core.Pagination.Services;
using SportsClub.Core.Pagination.Helpers;

namespace SportsClub.Controllers;



[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly SportsClubContext context;
    private readonly IUserAuthenticationRepository _repostry;
    private readonly IMapper mapper;
    private readonly IUriService uriService;
    public AuthController(IMapper mapper, IUserAuthenticationRepository repostry, SportsClubContext context, IUriService uriService)
    {
        this.context = context;
        this.uriService = uriService;
        this._repostry = repostry;
        this.mapper = mapper;
    }


    [HttpPost]
    //[ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest userRegistration)
    {
        
        var userResult = await _repostry.RegisterUserAsync(userRegistration);
        return !userResult.Succeeded ? new BadRequestObjectResult(userResult) : StatusCode(201);
    }

    [HttpPost("login")]
    //[ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest user)
    {
        return !await _repostry.ValidateUserAsync(user) 
            ? Unauthorized() 
            : Ok(new { Token = await _repostry.CreateTokenAsync() });
    }

}
