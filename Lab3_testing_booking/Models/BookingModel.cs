using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3_testing_booking.Models
{
    class BookingModel
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public int totalprice { get; set; }
        public bool depositpaid { get; set; }
        public BookingDates bookingdates { get; set; }
        public string additionalneeds { get; set; }
    }

    public class BookingDates
    {
        public DateTime checkin { get; set; }
        public DateTime checkout { get; set; }
    }


}
