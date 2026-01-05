using CBCID_APPLICATION.Models;

namespace CBCID_APPLICATION.ICBCID
{
    public interface IInvestigation_officer
    {
        Task<IEnumerable<INVEST_OFFICER>> ViewInvestformat(string DCFCID, string INVESTIGATION_OFFICER_ID);
        Task<Status> AddInvestOfficerAsync(INVEST_OFFICER model);
        Task<Status> UpdateInvestOfficerAsync(INVEST_OFFICER model);
        Task<IEnumerable<INVEST_OFFICER>> Getdet(string DCFCID);

        Task<Status> AddOfficerAsync(OFFICERMASTER model);


    }
}
