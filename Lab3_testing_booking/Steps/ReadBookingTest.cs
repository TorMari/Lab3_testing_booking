using Lab3_testing_booking.Models;
using RestSharp;
using System;
using System.Net;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Lab3_testing_booking.Steps
{
    [Binding]
    public class ReadBookingTest
    {
        private RestClient client = new RestClient("http://restful-booker.herokuapp.com/");
        private RestRequest request;
        private IRestResponse response;

        [When(@"send Read booking request")]
        public void WhenSendReadBookingRequest()
        {
            request = new RestRequest("booking", Method.GET);
            response = client.Execute<BookingModel>(request);
        }
        
        [Then(@"info is successfully read")]
        public void ThenInfoIsSuccessfullyRead()
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
