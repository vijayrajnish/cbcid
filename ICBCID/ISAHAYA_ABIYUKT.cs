using CBCID_APPLICATION.Models;

namespace CBCID_APPLICATION.ICBCID
{
    public interface ISAHAYA_ABIYUKT
    {
        Task<IEnumerable<SAHAYA>> Viewsahayakformat(string DCFCID, string Sahayak_Abhiyukt_ID);
        Task<Status> AddsahayakAsync(SAHAYA model);
        Task<Status> UpdatesahayakAsync(SAHAYA model);

        Task<Status> DeletesahayakAsync(string DCFCID, string Sahayak_Abhiyukt_ID);
    }
}
