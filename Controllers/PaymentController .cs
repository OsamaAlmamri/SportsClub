
﻿using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SportsClub.Core.Requests;
using SportsClub.Core.Services;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using SportsClub.Models;

namespace SportsClub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private static readonly string PayPalClientId = "AYzcTmXVDg24ZmqQBnjNvKUvEYroWKwmZWMMTqGbVYwT5id5sefMddtQcJDdsVMvMWsfHBqjjmM-1tRZ";
        private static readonly string PayPalClientSecret = "EBIF-KfhSUQEWX1QZlbfNsN4BTRj4A7jmBGMyHTzAZXA7I5VpmIYmEre2sJtBY_2WRovxvzrzeyOpjO5";
        private readonly IManageSubscription manageSubscription;
        private string userId;
        private readonly SportsClubContext context;
        public PaymentController( SportsClubContext context)
        {
            this.context = context;
            this.manageSubscription = new ManageSubscription(context);
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult CreatePayment([FromBody] UserSubscriptionRequest userSubscriptionRequest)
        {
            this.userId = (HttpContext.User.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.PrimarySid))?.Value);

            
             var   userSubscription  = manageSubscription.createTempSubscription(userSubscriptionRequest, this.userId);
            var apiContext = new APIContext(new OAuthTokenCredential(PayPalClientId, PayPalClientSecret).GetAccessToken());


            var itemList = new ItemList()
            {
                items = new List<Item>()
                {

                    new Item()
                    {
                        name = "Services Subscription",
                        currency = "USD",
                        price = userSubscription.TotaAmount.ToString(),
                        quantity = "1",
                        sku = "SAMPLE_SKU"
                    }
                }
            };


            var payer = new Payer() { payment_method = "paypal" };

            var redirectUrls = new RedirectUrls()
            {
                cancel_url = "https://localhost:44428/api/payment/cancel",
                return_url = "https://localhost:44428/api/payment/execute"
            };

            var amount = new Amount()
            {
                currency = "USD",
                total = userSubscription.TotaAmount.ToString(),
            };

            var transaction = new Transaction()
            {
                description = "Services Subscription",
                invoice_number =userSubscription.Id.ToString(),
                amount = amount,
                item_list = itemList
            };

            var payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                redirect_urls = redirectUrls,
                transactions = new List<Transaction>() { transaction }
            };

            var createdPayment = payment.Create(apiContext);

            var paymentId = createdPayment.id; // Retrieve the payment ID
            var approvalUrl = createdPayment.links.Find(x => x.rel == "approval_url").href; // Retrieve the approval URL

            var paymentData = new { paymentId, approvalUrl };

            return Ok(paymentData);

        }

        [HttpGet("execute")]
        public IActionResult ExecutePayment(string paymentId, string token, string PayerID)
        {
            var apiContext = new APIContext(new OAuthTokenCredential(PayPalClientId, PayPalClientSecret).GetAccessToken());

            var paymentExecution = new PaymentExecution() { payer_id = PayerID };

            var payment = new Payment() { id = paymentId };

            var executedPayment = payment.Execute(apiContext, paymentExecution);

       
            // Process the payment response as needed
            var paymentStatus = executedPayment.state;
            if (paymentStatus== "approved")
            {
                var userSubscriptionId = (long)Convert.ToDouble(executedPayment.transactions[0].invoice_number);
                var userSubscriptionPaymentGatway = new UserSubscriptionPaymentGatway()
                {
                    UserSubscriptionId = userSubscriptionId,
                    PaymentGatwayId = 1,
                    ResponseCode = executedPayment.state,
                    TransactionId = executedPayment.id,
                    Amount = Convert.ToDouble(executedPayment.transactions[0].amount.total),
                    Currency = executedPayment.transactions[0].amount.currency,
                };
                manageSubscription.saveTempSubscription(userSubscriptionId, userSubscriptionPaymentGatway);
            }
            //invoice_number 
        

            // Redirect to a specific URL based on the payment status
            // var redirectUrl = paymentStatus == "approved" ? "/payment" : "/counter";
            return Redirect("/cart/" + paymentStatus);
            return Ok(paymentStatus);
            return Ok(executedPayment);
        }

        [HttpGet("cancel")]
        public IActionResult CancelPayment()
        {
            // Handle payment cancellation
            return Redirect("/cart");
            return Ok("cancel");
        }

        [HttpGet("status/{paymentId}")]
        public IActionResult CheckPaymentStatus(string paymentId)
        {
            // Query the payment status from your data store or PayPal API
            /*
             * 
             * 
 created: The payment is created but not yet approved or executed.
approved: The payment is approved and completed successfully.
failed: The payment failed or encountered an error during processing.
canceled: The payment was canceled by the user or the merchant.
expired: The payment authorization has expired.
pending: The payment is still pending approval or processing.
in_progress: The payment is currently being processed or executed.
partially_paid: Only a partial amount of the payment has been received.
refunded: The payment has been refunded to the buyer.
voided: The payment authorization has been voided.
             */

            var apiContext = new APIContext(new OAuthTokenCredential(PayPalClientId, PayPalClientSecret).GetAccessToken());

            var payment = Payment.Get(apiContext, paymentId);

            var paymentStatus = payment.state;

            var paymentData = new { paymentId, paymentStatus };
            return Ok(paymentData);
        }

        public class PaymentService
        {
            private readonly HttpClient httpClient;
            private readonly IConfiguration configuration;

            public PaymentService(HttpClient httpClient, IConfiguration configuration)
            {
                this.httpClient = httpClient;
                this.configuration = configuration;
            }


        }

    }
}