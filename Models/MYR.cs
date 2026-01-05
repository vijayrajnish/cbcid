using System.ComponentModel.DataAnnotations;

namespace CBCID_APPLICATION.Models
{
    public class MYR
    {
        [Key]
        public int YrId { get; set; }
        public int Year { get; set; } 
    }
}
