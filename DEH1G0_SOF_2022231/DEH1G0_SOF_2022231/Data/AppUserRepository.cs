using DEH1G0_SOF_2022231.Helpers;
using DEH1G0_SOF_2022231.Models;
using Microsoft.EntityFrameworkCore;

namespace DEH1G0_SOF_2022231.Data;

/// <summary>
/// Repository class for AppUser entities
/// </summary>
public class AppUserRepository : GenericRepository<AppUser>, IAppUserRepository
{
    private readonly IPaginatedListBuilder _paginatedListBuilder;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="AppUserRepository"/> class.
    /// </summary>
    /// <param name="context">The <see cref="ApplicationDbContext"/> to use for database operations.</param>
    public AppUserRepository(ApplicationDbContext context, IPaginatedListBuilder builder) : base(context)
    {
        this._paginatedListBuilder = builder;
    }

    /// <summary>
    /// Asynchronously retrieves a collection of <see cref="Torrent"/>s associated with the <see cref="AppUser"/>
    /// with the given ID.
    /// </summary>
    /// <param name="id">The ID of the <see cref="AppUser"/> to retrieve <see cref="Torrent"/>s for.</param>
    /// <returns>A collection of <see cref="Torrent"/>s associated with the <see cref="AppUser"/>.</returns>
    public async Task<ICollection<Torrent>> GetTorrentsByUserId(string id)
    {
        var user = await this.GetByIdAsync(id);
        if (user == null)
        {
            throw new NullReferenceException("User not found.");
        }
        return user.Torrents;
    }
    
    public async Task<PaginatedList<AppUser>> GetPaginatedAppUsersAsync(PageQueryParameters pageQueryParameters)
    {
        var users = this._context.Users.AsNoTracking();
        return await this._paginatedListBuilder.BuildAsync<AppUser>(users, pageQueryParameters);
    }
}


