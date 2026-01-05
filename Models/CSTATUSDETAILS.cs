namespace CBCID_APPLICATION.Models
{
    public class CSTATUSDETAILS
    {
        public int CSDID { get; set; }
        public int? DCFCID { get; set; }
        public int? CASESTATUSID { get; set; }
        public int? ABHIYUKTHKAIVIRUDHMSG_ID { get; set; } = 0;
        public int? Sahayak_Abhiyukt_ID { get; set; } = 0;
        public String? REMARK { get; set; } = string.Empty;
        public Nullable<System.DateTime> AdeshDate { get; set; }
        
    }
}
