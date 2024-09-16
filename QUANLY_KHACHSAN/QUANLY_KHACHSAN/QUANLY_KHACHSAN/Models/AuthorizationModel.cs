namespace QUANLY_KHACHSAN.Models
{
    public class AuthorizationModel
    {
        // Models/AuthorizationModel.cs
        public enum UserRole
        {
            Manager,
            Staff
        }

        public class AuthorizationItem
        {
            public UserRole Role { get; set; }
            public List<string> Permissions { get; set; }
        }
    }
}
