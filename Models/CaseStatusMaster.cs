using System.ComponentModel.DataAnnotations;

namespace CBCID_APPLICATION.Models
{
    public class CaseStatusMaster
    {
        [Key]
        public int CaseStatusId { get; set; }
        [MaxLength(300)]
        public string CStatus { get; set; } = string.Empty;
        public int IsActive { get; set; }
    }
}
