namespace CBCID_APPLICATION.Models
{
    public class Sunvahidetails
    {
        public string cbcidno { get; set; } = string.Empty;
        public string Apradhno { get; set; } = string.Empty;
        public string Bname { get; set; } = string.Empty;
        public DateTime? NextHearingDate { get; set; } 
        public DateTime? HearingDate { get; set; }
        public int CaseStatusId { get; set; }
        public string Prosecutor { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public int? DCFCID { get; set; }
        public int? ABHIYUKTHKAIVIRUDHMSG_ID { get; set; } = 0;
        public int? Sahayak_Abhiyukt_ID { get; set; } = 0;

        public String? CASERMK { get; set; } = string.Empty;
        public DateTime? FDATE { get; set; }

    }
}
