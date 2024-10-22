using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.Models
{
    public class Train
    {
        public int TrainId { get; set; }  
        public string TrainingName { get; set; }
        public string TrainingField { get; set; }
        public string TrainingTime { get; set; }
        public string CompanyName { get; set;}
        public byte[]? TrainingImg { get; set; }
        [DataType(DataType.Date)]
        public DateTime? TrainingDate { get; set; }
        public string Email { get; set; } 
        
        [ForeignKey("Users")]
        public string UserId { get; set; }
        public IdentityUser Users { get; set; }  

    }
}
