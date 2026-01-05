namespace CBCID_APPLICATION.Models
{
    public class Notification
    {
        //public int? DistrictId { get; set; }
        //public int? SectorId { get; set; }
        //public int? Roleid { get; set; }

        public String DNAME { get; set; } = string.Empty;
        public string NO_OF_ENTRY { get; set; } = string.Empty;
        public string PARTIAL_SAVE { get; set; } = string.Empty;
        public string FINAL_SAVE { get; set; } = string.Empty;
    }
}
