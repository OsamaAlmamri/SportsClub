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

namespace SportsClub.Core.Services
{
    public interface IManageSubscription
    {


        public UserSubscription createTempSubscription([FromBody] UserSubscriptionRequest userSubscriptionRequest, string userId);
        public void deleteTempSubscription(string userID);
        public void saveTempSubscription(long id, UserSubscriptionPaymentGatway userSubscriptionPaymentGatway);


    }
}
