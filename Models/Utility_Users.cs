namespace CBCID_APPLICATION.Models
{
    public class Utility_Users
    {
        public int Id { get; set; }
        public  string UserName { get; set; }
        public string EmployeeId { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }
        public DateTime createdOn { get; set; }
        public string updatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
    }
}
