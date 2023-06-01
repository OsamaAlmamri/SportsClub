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
    [ApiController, Authorize]
    [Route("[controller]")]
    public class UserSubscriptionsController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SportsClubContext context;
        private readonly IRepositoryBase<UserSubscription> _repostry;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        public UserSubscriptionsController(IMapper mapper, SportsClubContext context, IUriService uriService, UserManager<User> userManager)
        {
            this.context = context;
            this.uriService = uriService;
            this._repostry = new UserSubscriptionRepostry(context);
            this.mapper = mapper;
            this._userManager = userManager;
        }


        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _repostry.GetAllPage(filter, a => a.Id);
            var Result = mapper.Map<List<UserSubscriptionDto>>(pagedData);
            var totalRecords = await context.UserSubscriptions.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserSubscriptionDto>(Result, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }







        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userSubscription = _repostry.Find(a => a.Id == id);
            var ownerResult = mapper.Map<UserSubscriptionDto>(userSubscription);
              

            // Return the user ID.
            return Ok(ownerResult);
          
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserSubscriptionRequest userSubscriptionRequest)
        {
            try
            {
                if (userSubscriptionRequest == null)
                {
                    return BadRequest("userSubscription object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var userSubscription = new UserSubscription()
                {
                    UserId = (HttpContext.User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.PrimarySid))?.Value),
                };

                _repostry.Create(userSubscription);

              var  us=_repostry.LastInserted(a => a.Id);
                double total = 0;

                foreach (UserSubscriptionServicesRequest serviceRequest in userSubscriptionRequest.Services)
                {
                    var service=context.Services.Where(s=>s.Id==serviceRequest.ServiceId).FirstOrDefault();
                    total += service.Price;
                    var userSubscriptionSrtvice = new UserSubscriptionService()
                    {
                        UserSubscriptionId = us.Id,
                        ServiceId = service.Id,
                        UserId = us.UserId,
                        StartAt = serviceRequest.StartAt,
                        EndAt = serviceRequest.StartAt?.AddDays(service.Period)

                };
                    context.UserSubscriptionServices.Add(userSubscriptionSrtvice);
                    context.SaveChanges();


          
                }

                us.TotaAmount = total;
                context.SaveChanges();

                var ownerResult = mapper.Map<UserSubscriptionDto>(us);

                return Ok(ownerResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] UserSubscription userSubscription)
        {
            try
            {


                // return Ok(userSubscription);

                var re = _repostry.Find(e => e.Id == id);
                if (userSubscription == null)
                {
                    return BadRequest("userSubscription object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }


                userSubscription.Id = id;

                _repostry.Update(userSubscription);

                var ownerResult = mapper.Map<UserSubscriptionDto>(userSubscription);
                return Ok(ownerResult);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var re = _repostry.Find(e => e.Id == id);
                if (re == null)
                {
                    return BadRequest("userSubscription object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repostry.Delete(re);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }
    }
}