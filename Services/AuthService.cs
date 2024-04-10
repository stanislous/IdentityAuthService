using System.Net;
using IdentityAuthService.Model;

namespace IdentityAuthService.Services;

public class AuthService
{
    public async Task<B2CResponseModel> UserLoginValidation(InputClaimsModel inputClaim)
    {
        var user = TestUserData.UserData.GetUser(inputClaim.SignInName, inputClaim.Password);
        var outputClaims = new B2CResponseModel("", HttpStatusCode.OK) { status = 200 };

        if (user != null)
        {
            try
            {
                outputClaims.needToMigrate = "local";
                outputClaims.newPassword = inputClaim.Password;
                outputClaims.email = user.UserEmailAddress;
                outputClaims.displayName = user.UserFirstName + " " + user.UserLastName;
                outputClaims.surName = user.UserFirstName;
                outputClaims.givenName = user.UserFirstName;
                outputClaims.roles = "Admin";
                return outputClaims;
            }
            catch (Exception ex)
            {
                return new B2CResponseModel($"Internal Error - {ex.Message}", HttpStatusCode.BadGateway) { status = 502 };
            }
        }

        if (user is { IsMigrated: true })
        {
            outputClaims.needToMigrate = null;
            outputClaims.roles = "Admin";
            return outputClaims;
        }

        return new B2CResponseModel("Invalid username or password.", HttpStatusCode.Conflict) { status = 409 };
    }

    public async Task<bool> UserSignUpValidation(SignUpInputClaimModel signUpInputClaimModel)
    {
        var user = new User
        {
            UserFirstName = signUpInputClaimModel.FirstName,
            UserLastName = signUpInputClaimModel.LastName,
            UserEmailAddress = signUpInputClaimModel.SignInName,
            UserPhoneNumber = signUpInputClaimModel.PhoneNumber,
            IsMigrated = true
        };

        var userAvailable = TestUserData.UserData.GetUser(user.UserEmailAddress);

        if(userAvailable == null) 
            return TestUserData.UserData.AddNewUser(user);
        
        return true;
    }
}