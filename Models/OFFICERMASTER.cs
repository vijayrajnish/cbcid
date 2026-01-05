using System.ComponentModel.DataAnnotations;

namespace CBCID_APPLICATION.Models
{
    public class OFFICERMASTER
    {
        [Key]
        public int OFFICER_ID { get; set; }

       

        [MaxLength(200)]
        public string? OFF_NAME { get; set; } = string.Empty;

       

        public int? DESIGNATIONID { get; set; }
        
    }
}
