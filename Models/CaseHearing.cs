using System.ComponentModel.DataAnnotations;

namespace CBCID_APPLICATION.Models
{
    public class CaseHearing
    {
        [Key]
        public int HearingID { get; set; }
        public int? DCFCID { get; set; }
        public  string? CBCID { get; set; }
        public string? APRADHNO { get; set; }
        public string? Bname { get; set; }
        public DateTime? HearingDate { get; set; }
        public DateTime? NextHearingDate { get; set; }
        public int? CaseStatusId { get; set; }
        [MaxLength(500)]
        public string? Remark { get; set; } = string.Empty;
        [MaxLength(250)]
        public string? ADATHAN_STATUS { get; set; } = string.Empty;
        public String? CaseStatus { get; set; }

        
        public String? Prosecutor { get; set; }
        public String? Mobile { get; set; }

        public int? ABHIYUKTHKAIVIRUDHMSG_ID { get; set; } = 0;
        public int? Sahayak_Abhiyukt_ID { get; set; } = 0;

        public String? CASERMK { get; set; } = string.Empty;

        public DateTime? FDATE { get; set; }
    }
}
