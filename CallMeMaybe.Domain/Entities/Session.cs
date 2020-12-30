using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CallMeMaybe.Domain.Entities
{
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        
        public string UserId { get; set; }
        
        public string ConnectionId { get; set; }
        
        public Boolean Status { get; set; }
        
        [ForeignKey("UserId")]
        public virtual ApplicationUser Users { get; set; } 
    }
}