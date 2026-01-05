using CBCID_APPLICATION.Models;

namespace CBCID_APPLICATION.ICBCID
{
    public interface IChangePassword
    {
        Task<Status> UpdatePasswordAsync(Utility_Users model);
    }
}
