using CBCID_APPLICATION.DomainLayer;
using CBCID_APPLICATION.Models;

namespace CBCID_APPLICATION.ICBCID
{
    public interface IDET_CRIME_FEMALE_CHILDREN
    {
        Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewformatAdminSrh(string scid,string dsid,string cbcid,string fmttypeid, string CaseStatusId,string Appstatus,string CREATEDBY, string YEAR, string APRADHYEAR);
        Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewformatAdminSrhAdmin(string scid, string dsid, string cbcid, string fmttypeid);


        Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewformat1(string dsid);
        Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewformatsrh(string APDHNO, string cbcid, string fmttypeid,string sid,string Fdate,string Tdate,string CaseStatusId,string DISTRICTID,string dfif, string SIBNO,string strrole,string CRNO);
        Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewformatsrhCIS(string APDHNO, string cbcid, string fmttypeid, string sid);
        Task<IEnumerable<VW_DET_CRIME_FEM_CHILD>> viewAllformatsrh(string APDHNO, string cbcid, string fmttypeid);
        Task<IEnumerable<Fmtvw>> Getformat1ByID(string DCFCID);
        Task<Status> AddFormatAsync(DET_CRIME_FEMALE_CHILDREN model);
        Task<Status> AddFormatAsyncCIS(DET_CRIME_FEMALE_CHILDREN model);
        Task<Status> UpdateFormatAsync(DET_CRIME_FEMALE_CHILDREN model);
        Task<IEnumerable<MDistrict>> GetDistrictLst(string dsid);
        Task<IEnumerable<MSector>> GetSectorLst(string secid);
        Task<IEnumerable<MSector>> GetSectorKhandLst(string secid);
        Task<IEnumerable<MThana>> GetThanaLst(string dsid);
        Task<IEnumerable<Court>> GetCourtLst();
        Task<IEnumerable<MYR>> GetYRLst();
        Task<IEnumerable<FMTMASTER>> GetFMTLst(string fmtid);
        Task<IEnumerable<FMTMASTER>> GetALLFMTLst();
        Task<IEnumerable<MDistrict>> GetDistrictSectorwiseLst(string secid);
        Task<IEnumerable<AdminDashBoardView>> GetFormatLst(string distid,string scid);
        Task<IEnumerable<AdminDashBoardView>> GetFormatLstCIS(string distid, string scid);

        Task<IEnumerable<Gavaha_Cnt>> GetGavahaCnt(string DCFCID);

        Task<IEnumerable<AbhiyukCnt>> GetAbhiyuktCnt(string DCFCID);

        Task<IEnumerable<INVESTIGATION_OFFICER_CNT>> GetINVESTCnt(string DCFCID);

        Task<IEnumerable<CaseStatusMaster>> GetStatusLst();

        Task<IEnumerable<AbhiyukthMsg>> Getabhiyukthmsg();
        

        Task<IEnumerable<MSector>> GetSectorALLLst(string secid);
        Task<IEnumerable<MDistrict>> GetDistrictALLLst(string secid);

        Task<IEnumerable<ADMIN_DASHBOARD_VIEW>> GetApplicationStatus();

        Task<IEnumerable<ApplicationRecordCnt>> GetApplicationRecordCnt();

        Task<IEnumerable<CASESTATUS_CNT_SECTOR_WISE>> GetAPPLICATION_CASE_STATUS();


        Task<IEnumerable<Notification>> GetSectorWiseApplicationStatus(string strsecid);

        Task<IEnumerable<SECTOR_WISE_APPLICATION_RECORD_CNT>> GetSectorApplicationRecordCnt(string strsecid);


    }
}
