using System.Diagnostics.CodeAnalysis;
using System.Runtime;
using System.Configuration;
using System.ComponentModel.DataAnnotations;


namespace CBCID_APPLICATION.Models
{
    public class DET_CRIME_FEMALE_CHILDREN
    {
        [Key]
        public int DCFCID { get; set; }
        public int? FMTID { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.DateTime> FRMDATE { get; set; }
        public Nullable<System.DateTime> TODATE { get; set; }
        public int? SECTORID { get; set; }
        public int? DISTRICTID { get; set; }
        [MaxLength(50)]
        public string? CBCID_NO { get; set; } = string.Empty;
        [MaxLength(100)]
        public string? APRADHNO { get; set; } = string.Empty;
        [MaxLength(300)]
        public string? ACT { get; set; } = string.Empty;
        public int? THANAID { get; set; }

        [MaxLength(50)]
        public string? TransferFrm { get; set; } = string.Empty;
        [MaxLength(200)]
        public string? BNAME { get; set; } = string.Empty;
        public int COURTID { get; set; }
        [MaxLength(50)]
        public String? CNRNO { get; set; }
        public String? LETTERNO { get; set; }
        public Nullable<System.DateTime> LETTERDATE { get; set; }
        public String? DOCUMENTNO { get; set; }
        public Nullable<System.DateTime> DAKILDATE { get; set; }
        public Nullable<System.DateTime> ARROPVICHARANTHITHI { get; set; }
        [MaxLength(250)]
        public string? PSHAKSHI_NAME { get; set; } = string.Empty;
        [MaxLength(250)]
        public string? RSHAKSHI_NAME { get; set; } = string.Empty;
        [MaxLength(250)]
        public string? ADATHAN_STATUS { get; set; } = string.Empty;
        public DateTime? NEXT_HEARING_DATE { get; set; }
        [MaxLength(300)]
        public string? ABHIYUKT { get; set; } = string.Empty;
        public int? Status { get; set; } = 0;
        public int? YEAR { get; set; }
        public int? MONTH { get; set; }
        [MaxLength(500)]
        public String? REMARK { get; set; }
        public int? APRADHYEAR { get; set; }
        public int? CaseStatusId { get; set; }
        public int? GOVTOFFICER { get; set; } = 0;
        [MaxLength(50)]
        public String? PLNO { get; set; }

        [MaxLength(300)]
        public String? FATHERNAME { get; set; }
        [MaxLength(500)]
        public String? ADDRESS { get; set; }
        [MaxLength(20)]
        public String? MOBILENO { get; set; }

        [MaxLength(500)]
        public String? GOVTOFFICERDETAILS { get; set; }

        [MaxLength(300)]
        public String? PERSON_INVOLVED_REGISTRATION { get; set; }

        [MaxLength(300)]
        public String? GOVTOFFICER_DESIGNATION { get; set; }

        [MaxLength(200)]
        public String? TIMEPERIOD { get; set; }

        [MaxLength(300)]
        public String? CASEDETAILS { get; set; }

        [MaxLength(300)]
        public String? CASEREMARK { get; set; }
        public Nullable<System.DateTime> CREATEDON { get; set; }
        [MaxLength(150)]
        public String? UPDATEDBY { get; set; }
        public DateTime? UPDATEDON { get; set; }
        public String? CREATEDBY { get; set; }
       
        public int PSAVE { get; set; } = 0;
        [MaxLength(150)]
        public string? SIBNO { get; set; } = string.Empty;
        public int? SIBNOYR { get; set; }
        public int? ABHIYUKTHKAIVIRUDHMSG_ID { get; set; } = 0;

        
    }
}
