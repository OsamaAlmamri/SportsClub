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
    [Route("[controller]")]
    public class ServiceController : ControllerBase
    {


        private readonly SportsClubContext context;
        private readonly IRepositoryBase<Service> _repostry;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        public ServiceController(IMapper mapper,  SportsClubContext context, IUriService uriService)
        {
            this.context = context;
            this.uriService = uriService;
            this._repostry = new ServiceRepostry(context);
            this.mapper = mapper;
        }
     

        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pagedData = await _repostry.GetAllPage(filter, a => a.Id);
            var Result = mapper.Map<List<ServiceDto>>(pagedData);
            var totalRecords = await context.Services.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<ServiceDto>(Result, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }



      

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var service =  _repostry.Find(a => a.Id == id);
            var ownerResult = mapper.Map<ServiceDto>(service);
            return Ok(ownerResult);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Service service)
        {
            try
            {
                if (service == null)
                {
                    return BadRequest("service object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }
           
                
                _repostry.Create(service);
                var re = _repostry.LastInserted(a => a.Id);
                var ownerResult = mapper.Map<ServiceDto>(re);
                return Ok(ownerResult);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.ToString());
            }
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Service service)
        {
            try
            {


                // return Ok(service);

                var re = _repostry.Find(e => e.Id == id);
                if (service == null)
                {
                    return BadRequest("service object is null");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }


                service.Id = id;
              
                _repostry.Update(service);

                var ownerResult = mapper.Map<ServiceDto>(service);
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
                    return BadRequest("service object is null");
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