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
    public class PaymentGatwaysController : ControllerBase
    {


        private readonly SportsClubContext context;
        private readonly IRepositoryBase<PaymentGatway> _repostry;
        private readonly IMapper mapper;
        private readonly IUriService uriService;
        public PaymentGatwaysController(IMapper mapper, IRepositoryBase<PaymentGatway> repostry, SportsClubContext context, IUriService uriService)
        {
            this.context = context;
            this.uriService = uriService;
            this._repostry = repostry;
            this.mapper = mapper;
        }
     

      
        public async Task<IActionResult> GetAll()
        {
            var data = _repostry.FindAll();

            var ownerResult = mapper.Map<IEnumerable<PaymentGatwayDto>>(data);
            return Ok(ownerResult);
            //   return Ok(data);
        }

       
    }
}