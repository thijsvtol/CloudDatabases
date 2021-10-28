using System;

namespace Domain
{
    public class House
    {
        public Guid id { get; set; }
        public float Price { get; set; }
        public double Title { get; set; }
        public string ZipCode { get; set; }
        public string Address { get; set; }
        public string ImageURL { get; set; }
    }
}
