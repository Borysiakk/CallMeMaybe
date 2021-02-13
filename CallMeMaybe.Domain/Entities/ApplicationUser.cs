using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CallMeMaybe.Domain.Entities
{
    public class ApplicationUser:IdentityUser
    {
        [InverseProperty("User")]
        public virtual ICollection<Friends> FriendOne { get; set; }
        [InverseProperty("Friend")]
        public virtual ICollection<Friends> FriendTwo { get; set; }
        public virtual ICollection<SessionUser> SessionUsers { get; set; }
    }
}