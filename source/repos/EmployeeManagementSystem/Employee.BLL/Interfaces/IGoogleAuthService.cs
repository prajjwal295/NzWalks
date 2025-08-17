using Employee.BLL.BOs.Requests;
using Employee.Dal.Entities;
using Google.Apis.Auth;
using GoogleApi.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.Interfaces
{
    public interface IGoogleAuthService
    {
        public Task<GoogleJsonWebSignature.Payload?> GoogleSignIn(GoogleSignInVM model);
    }
}
