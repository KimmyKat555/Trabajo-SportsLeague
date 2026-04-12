using SportsLeague.Domain.Entities;

namespace SportsLeague.Domain.Interfaces.Repositories
{
    public interface ISponsorRepository : IGenericRepository<Sponsor>
    {
        Task<Sponsor> CreateAsync(Sponsor sponsor);
        Task<IEnumerable<Sponsor>> FindAsync(System.Linq.Expressions.Expression<Func<Sponsor, bool>> predicate);
    }
}
