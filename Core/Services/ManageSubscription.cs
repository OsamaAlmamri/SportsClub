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
    public class ManageSubscription : IManageSubscription
    {
        private readonly SportsClubContext context;
        private readonly IRepositoryBase<UserSubscription> _repostry;
        public ManageSubscription(SportsClubContext context)
        {
            this.context = context;
            this._repostry = new UserSubscriptionRepostry(context);
        }

        public UserSubscription createTempSubscription([FromBody] UserSubscriptionRequest userSubscriptionRequest ,string userId)
        {
            this.deleteTempSubscription(userId);
         
            var userSubscription = new UserSubscription()
            {
                UserId = userId ,
            };

            _repostry.Create(userSubscription);

            var us = _repostry.LastInserted(a => a.Id);
            double total = 0;

            foreach (UserSubscriptionServicesRequest serviceRequest in userSubscriptionRequest.Services)
            {
                var service = context.Services.Where(s => s.Id == serviceRequest.id).FirstOrDefault();
                total += service.Price;
                var userSubscriptionSrtvice = new UserSubscriptionService()
                {
                    UserSubscriptionId = us.Id,
                    ServiceId = service.Id,
                    UserId = us.UserId,
                    StartAt = serviceRequest.startAt,
                    EndAt = serviceRequest.startAt?.AddDays(service.Period)

                };
                context.UserSubscriptionServices.Add(userSubscriptionSrtvice);
                context.SaveChanges();



            }

            us.TotaAmount = total;
            context.SaveChanges();

            return us;
            

        }

        public void deleteTempSubscription(string userID)
        {
            var itemsToRemove = context.UserSubscriptions.Where(item => item.UserId== userID && !item.ISPayed).ToList();

            // Remove the items from the table
            context.UserSubscriptions.RemoveRange(itemsToRemove);

            // Save the changes to the database
            context.SaveChanges();
            return;
        }
        public void saveTempSubscription(long id, UserSubscriptionPaymentGatway userSubscriptionPaymentGatway)
        {
            var userSubscription = context.UserSubscriptions.Where(item => item.Id== id).FirstOrDefault();
            userSubscription.ISPayed = true;
            // Remove the items from the table
            context.SaveChanges();
            context.UserServicePaymentGatways.Add(userSubscriptionPaymentGatway);
            // Save the changes to the database
            context.SaveChanges();
            return;
        }
    }
}
