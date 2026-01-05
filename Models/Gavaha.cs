using System.ComponentModel.DataAnnotations;

namespace CBCID_APPLICATION.Models
{
    public class Gavaha
    {
        [Key]
        public int  GavahaId { get; set; }
        public int DCFCID { get; set; }
        public int GAVAHA_KA_PRAKAR_ID { get; set; }
        [MaxLength(300)]
        public string? GavahaName { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? Address { get; set; } = string.Empty;
        [MaxLength(15)]
        public string? Phoneno { get; set; } = string.Empty;
        [MaxLength(20)]
        public string? AdharNo { get; set; } = string.Empty;
        public int? GavaTypeId { get; set; }
        public int? AttendenceStatus { get; set; }
        public DateTime? Entrydate { get; set; }
        public int? Present { get; set; }
        public int? Absent { get; set; }
        public int? Examined { get; set; }
        public DateTime? Examineddate { get; set; }
        public int? NonExaminedReasonId { get; set; }
        [MaxLength(500)]
        public string? OtherRemark { get; set; }
        public DateTime? GhavahiNextdate { get; set; }
        [MaxLength(500)]
        public string Remark { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? createdby { get; set; }
        [MaxLength(300)]
        public DateTime? createdon { get; set; }  //createdate
        [MaxLength(300)]
        public string? updatedby { get; set; }
        [MaxLength(300)]
        public DateTime? updatedon { get; set; }
    }
}
