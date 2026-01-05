using CBCID_APPLICATION.Models;

namespace CBCID_APPLICATION.ICBCID
{
    public interface IAddSunvahi
    {
        Task<IEnumerable<CaseHearing>> viewSunvahiformat(string DCFCID, string Hearingid);
        Task<Status> AddSunvahiAsync(CaseHearing model);
        Task<Status> UpdateSunvahiAsync(CaseHearing model);

        Task<ABHIYUKTH_SAVE_STATUS> AddAbhiyukthAsync(CSTATUSDETAILS model);

        Task<IEnumerable<CSTATUSDETAILS>> LstAbhiyukthAsync(string id);

        Task<IEnumerable<CaseStatusMaster>> GetStatusLst();
        Task<IEnumerable<Sunvahidetails>> Getdet(string id);

        Task<IEnumerable<AbhiyukthMsg>> Getabhiyukthmsg();

        Task<IEnumerable<ABHIYUKTHDRP>> GetABHIYUKTHLst(String dcfcid);

        Task<IEnumerable<SunvahiMandatoryDetails>> Getsunvahidet(string id);

    }
}
