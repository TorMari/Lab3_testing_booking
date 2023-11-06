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
    public class CreateBookingSteps
    {
        BookingModel bookingModel = new BookingModel();
        private RestClient client = new RestClient("http://restful-booker.herokuapp.com/");
        private RestRequest request;
        private IRestResponse response;

        [Given(@"Input firstname ""(.*)""")]
        public void GivenInputFirstname(string firstname)
        {
            bookingModel.firstname = firstname;
        }

        [Given(@"input lastname ""(.*)""")]
        public void GivenInputLastname(string lastname)
        {
            bookingModel.lastname = lastname;
        }

        [Given(@"set a total price at ""(.*)""")]
        public void GivenSetATotalPriceAt(int totalprice)
        {
            bookingModel.totalprice = totalprice;
        }

        [Given(@"set depositpaid as ""(.*)""")]
        public void GivenSetDepositpaidAs(bool depositpaid)
        {
            bookingModel.depositpaid = depositpaid;
        }

        [Given(@"select the date of checkin in ""(.*)"" and checkout in ""(.*)""")]
        public void GivenSelectTheDateOfCheckinInAndCheckoutIn(DateTime checkin, DateTime checkout)
        {
            bookingModel.bookingdates = new BookingDates();
            bookingModel.bookingdates.checkin = checkin;
            bookingModel.bookingdates.checkout = checkout;
        }

        [Given(@"set in the additional needs ""(.*)""")]
        public void GivenSetInTheAdditionalNeeds(string needs)
        {
            bookingModel.additionalneeds = needs;
        }

        [When(@"send Create booking request")]
        public void WhenSendCreateBookingRequest()
        {
            request = new RestRequest("booking", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(bookingModel);
            request.AddHeaders(new Dictionary<string, string>
            {
                { "Accept", "application/json"},
                { "Content-Type", "application/json"}
            });
            response = client.Execute<BookingModel>(request);
        }

        [Then(@"response is success")]
        public void ThenResponseIsSuccess()
        {
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
