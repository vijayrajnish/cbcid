using System.ComponentModel.DataAnnotations;

namespace CBCID_APPLICATION.Models
{
    public class INVEST_DTO
    {
        [Key]
        public int INVESTIGATION_OFFICER_ID { get; set; }

        public int DCFCID { get; set; }

        [MaxLength(200)]
        public string? OFFICER_NAME { get; set; } = string.Empty;

        public DateTime? FROMDATE { get; set; }

        public DateTime? TODATE { get; set; }
        public int? DESIGNATIONID { get; set; }

        public String? DESIGNATION { get; set; }
    }
}
