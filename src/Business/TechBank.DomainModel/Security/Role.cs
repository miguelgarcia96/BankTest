using Microsoft.AspNetCore.Identity;

namespace TechBank.DomainModel
{
    public class Role : IdentityRole<int>
    {

        public Role() : base() { }
        public Role(string roleName) : base(roleName) { }
    }
}
