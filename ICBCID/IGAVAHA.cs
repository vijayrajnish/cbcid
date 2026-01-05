using CBCID_APPLICATION.Models;

namespace CBCID_APPLICATION.ICBCID
{
    public interface IGAVAHA
    {
        Task<IEnumerable<Gavaha>> viewgavahaformat(string DCFCID,string gavahaid);
        Task<Status> AddGavahaAsync(Gavaha model);
        Task<Status> UpdateGavahaAsync(Gavaha model);
        Task<Status> DeleteGavaha(string DCFCID, string gavahaid,string ID); 
    }
}
