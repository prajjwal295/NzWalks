using Microsoft.AspNetCore.Identity;

namespace NzWalks.API.Repositories
{
    public interface ITokenRepositroy
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
