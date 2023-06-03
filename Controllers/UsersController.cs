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

namespace SportsClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {


        private readonly SportsClubContext context;
        private readonly IRepositoryBase<User> _repostry;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        public UsersController(IMapper mapper, SportsClubContext context, IUriService uriService)
        {
            this.context = context;
            this.uriService = uriService;
            this._repostry = new UserRepostry(context);
            this.mapper = mapper;
        }


        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _repostry.GetAllPage(filter, a => a.CreatedAt);
            var Result = mapper.Map<List<UserDto>>(pagedData);
            var totalRecords = await context.Users.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<UserDto>(Result, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }





        [HttpGet("search")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var pagedData = _repostry.FindByCondition(a => a.Email.Contains(searchTerm)  || a.UserName.Contains(searchTerm) || a.UserDetail.FullName.Contains(searchTerm));
            var Result = mapper.Map<List<UserDto>>(pagedData);
            return Ok(Result);

        
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = _repostry.Find(a => a.Id == id);
            var ownerResult = mapper.Map<UserDto>(user);
            return Ok(ownerResult);
        }




    }
}