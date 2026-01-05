namespace CBCID_APPLICATION.Models
{
    public class SectorWiseNotification
    {
        public int DCFCID { get; set; }
        public string FORMAT { get; set; }
        public int SECTORID { get; set; }

        public string DISTRICT { get; set; }
        public string SECTOR { get; set; }
        public string CBCID_NO { get; set; }

        public string APRADHNO { get; set; }
        public string ACT { get; set; }
        public string THANAID { get; set; }

        public string Thana { get; set; }
        
        public Nullable<System.DateTime> NEXTHEARINGDATE { get; set; }
        public string Court { get; set; }
    }
}
