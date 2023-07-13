using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TechBank.Business;
using TechBank.Data;

namespace TechBank.Application.Controllers
{
    public class BaseControllerMVC : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BaseControllerMVC()
        {
        }

        public BaseControllerMVC(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private MasterManager _masterManager = null;
        public MasterManager MasterManager
        {
            get
            {
                if (_masterManager != null)
                    return _masterManager;

                return _masterManager = new MasterManager(_unitOfWork);
            }
        }

        public string RequestEmail(ClaimsPrincipal user)
        {
            ClaimsIdentity claimsIdentity = (ClaimsIdentity)base.User.Identity;
            Claim claim = claimsIdentity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress");
            return claim.Value;
        }
    }

    

}
