using System;

namespace EMS_Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Department { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Birth_Date { get; set; }
        public string  Gender { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Hobbies { get; set; }
        public string Password { get; set; }

    }
}