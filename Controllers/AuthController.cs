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
using Microsoft.EntityFrameworkCore;

namespace SportsClub.Controllers;



[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly SportsClubContext context;
    private readonly IUserAuthenticationRepository _repostry;
    private readonly IRepositoryBase<User> _userRepostry;
    private readonly IRepositoryBase<UserDetail> _userDetailRepostry;
    private readonly IMapper mapper;
    private readonly IUriService uriService;
    public AuthController(IMapper mapper, IUserAuthenticationRepository repostry, SportsClubContext context, IUriService uriService)
    {
        this.context = context;
        this.uriService = uriService;
        this._repostry = repostry;
        this._userRepostry = new UserRepostry(context);
        this.mapper = mapper;
    }


    [HttpPost]
    //[ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest userRegistration)
    {
        
        var userResult = await _repostry.RegisterUserAsync(userRegistration);



        if (userResult.Succeeded)
        {
            var user = _userRepostry.Find(u => u.Email == userRegistration.Email);
                
             
            var userData = new UserDetail()
            {
                UserId = user.Id,
                FullName = userRegistration.FullName,
                BirthDate = userRegistration.BirthDate,
                Address = userRegistration.Address,
            };
            _userDetailRepostry.Create(userData);
         
         return   StatusCode(201);

        }

        else
            return new BadRequestObjectResult(userResult);
                
               

      
    }

    [HttpPost("login")]
    //[ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
    {
      var result=  await _repostry.ValidateUserAsync(loginRequest);

        if (result)
        {

            var user = _userRepostry.Find(u => u.Email == loginRequest.Email);
            string token = await _repostry.CreateTokenAsync();

            var data = mapper.Map<AuthUserDto>(user);
           data.Token= token;

            return Ok(data);
        }

        else
            return Unauthorized();
            
    }

}
