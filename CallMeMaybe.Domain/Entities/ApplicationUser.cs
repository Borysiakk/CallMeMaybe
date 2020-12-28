using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CallMeMaybe.Domain.Entities
{
    public class ApplicationUser :IdentityUser
    {
        [InverseProperty("User")]
        public virtual ICollection<Friend> FriendsOne { get; set; }

        [InverseProperty("UserFriend")]
        public virtual ICollection<Friend> FriendsTwo { get; set; }
        
        public virtual ICollection<Session> Sessions { get; set; } 
    }
}