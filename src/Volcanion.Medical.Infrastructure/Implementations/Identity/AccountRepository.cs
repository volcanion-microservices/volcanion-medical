using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Volcanion.Core.Infrastructure.Implementations;
using Volcanion.Medical.Infrastructure.Abstractions.Identity;
using Volcanion.Medical.Models.Context;
using Volcanion.Medical.Models.Entities.Identity;
using Volcanion.Medical.Models.Filters;

namespace Volcanion.Medical.Infrastructure.Implementations.Identity;

/// <inheritdoc/>
internal class AccountRepository : BaseRepository<Account, ApplicationDbContext, AccountFilter>, IAccountRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    /// <param name="httpContextAccessor"></param>
    public AccountRepository(ApplicationDbContext context, ILogger<BaseRepository<Account, ApplicationDbContext, AccountFilter>> logger, IHttpContextAccessor httpContextAccessor) : base(context, logger, httpContextAccessor)
    {
    }

    /// <inheritdoc/>
    public async Task<Account?> GetAccountByEmail(string email)
    {
        var account = await _context.Account.FirstOrDefaultAsync(x => x.Email.Equals(email));
        return account;
    }
}
