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

namespace SportsClub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceTimeController : ControllerBase
    {


        private readonly SportsClubContext context;
        private readonly IRepositoryBase<ServiceTime> _repostry;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        public ServiceTimeController(IMapper mapper, IRepositoryBase<ServiceTime> repostry, SportsClubContext context, IUriService uriService)
        {
            this.context = context;
            this.uriService = uriService;
            this._repostry = repostry;
            this.mapper = mapper;
        }
     

        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _repostry.GetAllPage(filter, a => a.Id);

            var totalRecords = await context.ServiceTimes.CountAsync();
            var Result = mapper.Map<List<ServiceTimeDto>>(pagedData);
            var pagedReponse = PaginationHelper.CreatePagedReponse<ServiceTimeDto>(Result, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }



        [HttpGet("all")]
        public async Task<IActionResult> GetByAll()
        {
            var data = _repostry.FindAll();

            var ownerResult = mapper.Map<IEnumerable<ServiceTimeDto>>(data);
            return Ok(ownerResult);
            //   return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceTime =  _repostry.Find(a => a.Id == id); ;
            var ownerResult = mapper.Map<ServiceTimeDto>(serviceTime);
            return Ok(ownerResult);
        }





        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceTime serviceTime)
        {
            try
            {
                if (serviceTime == null)
                {
                    return BadRequest("serviceTime object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repostry.Create(serviceTime);
                var re = _repostry.LastInserted(a=>a.Id);

                var ownerResult = mapper.Map<ServiceTimeDto>(re);
                return Ok(ownerResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ServiceTime serviceTime)
        {
            try
            {


                // return Ok(serviceTime);

                var re = _repostry.Find(e => e.Id == id);
                if (serviceTime == null)
                {
                    return BadRequest("serviceTime object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                serviceTime.Id = id;
              
                _repostry.Update(serviceTime);
              
                var ownerResult = mapper.Map<ServiceTimeDto>(serviceTime);
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
                    return BadRequest("serviceTime object is null");
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