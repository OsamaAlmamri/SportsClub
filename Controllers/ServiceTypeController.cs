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
    public class ServiceTypeController : ControllerBase
    {


        private readonly SportsClubContext context;
        private readonly IRepositoryBase<ServiceType> _repostry;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        public ServiceTypeController(IMapper mapper, IRepositoryBase<ServiceType> repostry, SportsClubContext context, IUriService uriService)
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

            var totalRecords = await context.ServiceTypes.CountAsync();
            var Result = mapper.Map<List<ServiceTypeDto>>(pagedData);
            var pagedReponse = PaginationHelper.CreatePagedReponse<ServiceTypeDto>(Result, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }



        [HttpGet("all")]
        public async Task<IActionResult> GetByParentCode()
        {
            var data = await context.ServiceTypes.ToListAsync();

            var ownerResult = mapper.Map<IEnumerable<ServiceTypeDto>>(data);
            return Ok(ownerResult);
            //   return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var serviceType = await context.ServiceTypes.Where(a => a.Id == id).FirstOrDefaultAsync();
            return Ok(new Response<ServiceType>(serviceType));
        }





        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceType serviceType)
        {
            try
            {
                if (serviceType == null)
                {
                    return BadRequest("serviceType object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
                _repostry.Create(serviceType);
                var re = _repostry.LastInserted();

            
                return Ok(re);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] ServiceType serviceType)
        {
            try
            {


                // return Ok(serviceType);

                var re = _repostry.Find(e => e.Id == id);
                if (serviceType == null)
                {
                    return BadRequest("serviceType object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                serviceType.Id = id;
              
                _repostry.Update(serviceType);
                return Ok(serviceType);

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
                    return BadRequest("serviceType object is null");
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