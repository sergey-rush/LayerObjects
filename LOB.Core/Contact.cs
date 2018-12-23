using System;

namespace LOB.Core
{
    public class Contact
    {//Id, Name, Phone, Email, StateId, Created
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public ItemState ItemState { get; set; }
        public DateTime Created { get; set; }
    }
}