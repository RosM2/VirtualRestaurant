﻿namespace VirtualRestaurant.Domain.Models
{
    public class Reservation
    {
        public DateTime ReservationDate { get; set; }

        public string VisitorEmail { get; set; }

        public int VisitorsCount { get; set; }

        public Table Table { get; set; }
    }
}
