using System;
using Newtonsoft.Json;

namespace LOB.Core
{
    // Name, Phone, Email, UserUid
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [JsonIgnore]
        public string Pass { get; set; }
        public Guid UserUid { get; set; }
        [JsonIgnore]
        public RoleType Role { get; set; }
        public AccountState AccountState { get; set; }
        [JsonIgnore]
        public UserState UserState { get; set; }
        [JsonIgnore]
        public int FailedCount { get; set; }
        [JsonIgnore]
        public DateTime LastLoginDate { get; set; }
        [JsonIgnore]
        public DateTime Updated { get; set; }
        [JsonIgnore]
        public DateTime Created { get; set; }
        public bool RememberMe { get; set; }
        public bool IsAuthenticated { get; set; }
    }
}
