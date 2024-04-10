namespace IdentityAuthService.Model;

public class User
{
    public string UserFirstName { get; set; }
    public string UserLastName { get; set; }
    public string UserEmailAddress { get; set; }
    public string UserPassword { get; set; }
    public string UserPhoneNumber { get; set; }
    public string UserAddress { get; set; }
    public bool IsMigrated { get; set; }
}