namespace CBCID_APPLICATION.Models
{
    public class CASESTATUS_CNT_SECTOR_WISE
    {
        public String SEC_ENAME { get; set; } = string.Empty;
        public String CStatus { get; set; } = string.Empty;
        public int SECTORID { get; set; }

        public int CaseStatusId { get; set; }

        public int CNT { get; set; }
    }
}
