namespace CollegeProject.Responses
{
    public class UserData
    {
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public long RoleId { get; set; }

        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public bool IsAllowed { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsWriteOnly { get; set; }
    }
}
