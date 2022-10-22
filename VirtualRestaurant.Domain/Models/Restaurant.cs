﻿namespace VirtualRestaurant.Domain.Models
{
    public class Restaurant
    {
        public string Name { get; set; }

        public Owner Owner { get; set; }

        public int TotalTablesCount { get; set; }

        public int FreeTablesCount { get; set; }
    }
}
