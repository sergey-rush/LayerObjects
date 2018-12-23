using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    public class Customer
    {
        public int Id { get; set; }
        public Guid CustomerUid { get; set; }
        public int UserId { get; set; }
        public int Gender { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Created { get; set; }
    }
}