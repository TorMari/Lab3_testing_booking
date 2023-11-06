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
    public class DeleteBookingSteps
    {
        private int bookingId;
        private RestClient client = new RestClient("http://restful-booker.herokuapp.com/");
        private string token;
        private IRestResponse<BookingModel> response;

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
            
            token = JsonSerializer.Deserialize<TokenModel>(client.Execute(request).Content).token;

            RestRequest reqId = new RestRequest("booking", Method.GET);
            IRestResponse resId = client.Execute(reqId);
            bookingId = JsonSerializer.Deserialize<List<BookingIdModel>>(resId.Content)[0].bookingid;
        }

        [When(@"send Delete booking request")]
        public void WhenSendDeleteBookingRequest()
        {
            GetToken();
            RestRequest request = new RestRequest($"booking/{bookingId}", Method.DELETE);
            request.AddHeaders(new Dictionary<string, string>
            {
                { "Content-Type", "application/json"},
                { "Cookie", $"token={token}" }
            });
            response = client.Execute<BookingModel>(request);
        }
        
        [Then(@"info is deleted")]
        public void ThenInfoIsDeleted()
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }
    }
}
