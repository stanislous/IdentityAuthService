using IdentityAuthService.Model;

namespace IdentityAuthService.TestUserData;

public static class UserData
{
    static List<User?> users = new()
    {
        new User
        {
            UserFirstName = "John",
            UserLastName = "Doe",
            UserEmailAddress = "john.doe@example.com",
            UserPassword = "1qaz@WSX",
            UserPhoneNumber = "1234567890",
            UserAddress = "123 Main St, Anytown, USA",
            IsMigrated = false
        },
        new User
        {
            UserFirstName = "Jane",
            UserLastName = "Smith",
            UserEmailAddress = "jane.smith@example.com",
            UserPassword = "1qaz@WSX",
            UserPhoneNumber = "9876543210",
            UserAddress = "456 Elm St, Othertown, USA",
            IsMigrated = false
        },
        new User
        {
            UserFirstName = "Alice",
            UserLastName = "Johnson",
            UserEmailAddress = "alice.johnson@example.com",
            UserPassword = "1qaz@WSX",
            UserPhoneNumber = "5556667777",
            UserAddress = "789 Oak St, Anothertown, USA",
            IsMigrated = false
        },
        new User
        {
            UserFirstName = "Bob",
            UserLastName = "Williams",
            UserEmailAddress = "bob.williams@example.com",
            UserPassword = "abcd1234",
            UserPhoneNumber = "4443332222",
            UserAddress = "321 Maple St, Yetanothertown, USA",
            IsMigrated = false
        },
        new User
        {
            UserFirstName = "Emily",
            UserLastName = "Brown",
            UserEmailAddress = "emily.brown@example.com",
            UserPassword = "abcd1234",
            UserPhoneNumber = "7778889999",
            UserAddress = "567 Pine St, Lasttown, USA",
            IsMigrated = false
        }
    };

    public static User? GetUser(string signInName, string password)
    {
        var userAvailable = users.FirstOrDefault(x => x.UserEmailAddress == signInName & x.UserPassword == password);
        if (userAvailable != null)
        {
            userAvailable.IsMigrated = true;
            return userAvailable;
        }

        return null;
    }
    
    public static User? GetUser(string signInName)
    {
        var userAvailable = users.FirstOrDefault(x => x.UserEmailAddress == signInName);
        if (userAvailable != null)
        {
            userAvailable.IsMigrated = true;
            return userAvailable;
        }

        return null;
    }

    public static bool AddNewUser(User user)
    {
        if (users.Exists(x => x.UserEmailAddress == user.UserEmailAddress))
        {
            return true;
        }
        users.Add(user);
        return false;
    }
}
