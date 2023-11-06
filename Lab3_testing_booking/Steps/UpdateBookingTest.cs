using Lab3_testing_booking.Models;
using RestSharp;
using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;

namespace Lab3_testing_booking.Steps
{
    [Binding]
    public class UpdateBookingSteps
    {
        private int bookingId;
        private RestClient client = new RestClient("http://restful-booker.herokuapp.com/");
        private string token;
        private IRestResponse response;

        private void GetToken()
        {
            RestRequest request = new RestRequest("auth", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeaders(new Dictionary<string, string>
            {
                { "Accept", "application/json"},
                { "Content-Type", "application/json"}
            });
            request.AddBody(new Dictionary<string, string>
            {
                { "username" , "admin" },
                { "password" , "password123" }
            });
            response = client.Execute(request);
            token = JsonSerializer.Deserialize<TokenModel>(client.Execute(request).Content).token;
            
            RestRequest reqId = new RestRequest("booking", Method.GET);
            IRestResponse resId = client.Execute(reqId);
            bookingId = JsonSerializer.Deserialize<List<BookingIdModel>>(resId.Content)[0].bookingid;
        }

        [When(@"send Update booking request")]
        public void WhenSendUpdateBookingRequest()
        {
            GetToken();
            RestRequest request = new RestRequest($"booking/{bookingId}", Method.PUT);
            request.RequestFormat = DataFormat.Json;
            request.AddHeaders(new Dictionary<string, string>
            {
                { "Accept", "application/json"},
                { "Content-Type", "application/json"},
                { "Cookie", $"token={token}" }
            });
            request.AddJsonBody(new BookingModel()
            {
                firstname = "James",
                lastname = "Brown",
                totalprice = 111,
                depositpaid = true,
                bookingdates = new BookingDates()
                {
                    checkin = DateTime.Parse("2018-01-01"),
                    checkout = DateTime.Parse("2019-01-01"),
                },
                additionalneeds = "Breakfast"
            });
            response = client.Execute<BookingModel>(request);
        }
        
        [Then(@"info is update")]
        public void ThenInfoIsUpdate()
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
