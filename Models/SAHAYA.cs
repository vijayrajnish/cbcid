using System.ComponentModel.DataAnnotations;

namespace CBCID_APPLICATION.Models
{
    public class SAHAYA
    {
        [Key]
        public int Sahayak_Abhiyukt_ID { get; set; }
        public int DCFCID { get; set; }
        [MaxLength(300)]
        public string? Name { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? Father { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? Address { get; set; } = string.Empty;
        [MaxLength(20)]
        public string? Phoneno { get; set; } = string.Empty;
        [MaxLength(20)]
        public string? AdharNo { get; set; } = string.Empty;
        [MaxLength(500)]
        public String? Remark { get; set; }
        public int AbhiyuktStatus { get; set; } = 1;
        public string AbhiyukthStatusView { get; set; } = string.Empty;
        public DateTime? Createdon { get; set; }
    }
}
