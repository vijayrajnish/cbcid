using System.ComponentModel.DataAnnotations;

namespace CBCID_APPLICATION.Models
{
    public class AbhiyukthMsg
    {
        [Key]
        public int ABHIYUKTHKAIVIRUDHMSG_ID { get; set; }
        [MaxLength(300)]
        public string MESSAGE { get; set; } = string.Empty;
       
    }
}
