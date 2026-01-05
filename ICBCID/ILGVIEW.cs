using CBCID_APPLICATION.DomainLayer;
namespace CBCID_APPLICATION.ICBCID
{
    public interface ILGVIEW
    {
        Task<LGVW> GetLgDetails(string uname,string password);
        //Task<LGVW> InsertRegistration(Hospital hospitalReg);
    }
}
