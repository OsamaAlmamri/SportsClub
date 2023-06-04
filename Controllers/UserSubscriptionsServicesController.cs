using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SportsClub.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsClub.Models;
using SportsClub.Models.Repositores;
using SportsClub.Core.Pagination.Wrappers;
using AutoMapper;
using SportsClub.Core.Pagination.Filter;
using SportsClub.Core.Pagination.Services;
using SportsClub.Core.Pagination.Helpers;
using SportsClub.Core.Requests;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace SportsClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserSubscriptionsServicesController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SportsClubContext context;
        private readonly IRepositoryBase<UserSubscriptionService> _repostry;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        public UserSubscriptionsServicesController(IMapper mapper, SportsClubContext context, IUriService uriService, UserManager<User> userManager)
        {
            this.context = context;
            this.uriService = uriService;
            this._repostry = new UserSubscriptionServicesRepostry(context);
            this.mapper = mapper;
            this._userManager = userManager;
        }


        public async Task<IActionResult> GetAll(string id)
        {

            var pagedData =  _repostry.FindByCondition(a => a.UserId == id);
            var Result = mapper.Map<List<UserSubscriptionServiceDto>>(pagedData);
            return Ok(Result);
        }




        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UserSubscriptionService userSubscriptionService)
        {
            try
            {


                // return Ok(serviceType);
               
                var re = _repostry.Find(e => e.Id == id);
                if (re == null)
                {
                    return BadRequest("userSubscriptionService object is null");
                }

               
         
                re.StartAt = userSubscriptionService.StartAt ;
                re.EndAt = userSubscriptionService.StartAt?.AddDays(re.Service.Period) ;

                _repostry.Update(re);

                var ownerResult = mapper.Map<UserSubscriptionServiceDto>(re);
                return Ok(ownerResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }




    }
}