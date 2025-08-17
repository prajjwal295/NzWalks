using Employee.BLL.BOs.Requests;
using Employee.BLL.BOs.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.BLL.Interfaces
{
    public interface IAuthService
    {
        Task<SignupResopnseBO> CreateUserAsync(SignupRequestBO request);
        Task<LoginResponseBO> LoginAsync(LoginRequestBO request);
    }
}
