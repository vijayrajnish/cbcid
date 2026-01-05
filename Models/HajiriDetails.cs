namespace CBCID_APPLICATION.Models
{
    public class HajiriDetails
    {
        public int HajidetID { get; set; }
        public int? DCFCID { get; set; }
        public int? CaseStatusId { get; set; }
        public int? SunvahiABHIYUKTHKAIVIRUDHMSG_ID { get; set; } = 0;
        public int? SunvahiSahayakAbhiyukt_ID { get; set; } = 0;
        public String? SunvahiCASERMK { get; set; } = string.Empty;
    }
}
