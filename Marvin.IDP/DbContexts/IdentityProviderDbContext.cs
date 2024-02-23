using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Marvin.IDP.DbContexts
{
    public class IdentityProviderDbContext : IdentityDbContext
    {
        public IdentityProviderDbContext(DbContextOptions<IdentityProviderDbContext> options) : base(options)
        {
        }
    }
}