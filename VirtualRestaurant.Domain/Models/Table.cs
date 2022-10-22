﻿namespace VirtualRestaurant.Domain.Models
{
    public class Table
    {
        public Restaurant Restaurant { get; set; }

        public int NumberOfSits { get; set; }

        public string Location { get; set; }

        public bool IsBooked { get; set; }
    }
}