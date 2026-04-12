using Microsoft.EntityFrameworkCore;
using SportsLeague.DataAccess.Context;
using SportsLeague.Domain.Entities;
using SportsLeague.Domain.Interfaces.Repositories;

namespace SportsLeague.DataAccess.Repositories
{
    public class SponsorRepository : GenericRepository<Sponsor>, ISponsorRepository
    {
        private readonly LeagueDbContext _context;

        public SponsorRepository(LeagueDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Sponsor> CreateAsync(Sponsor sponsor)
        {
            await _context.Sponsors.AddAsync(sponsor);
            await _context.SaveChangesAsync();
            return sponsor;
        }

        public async Task<IEnumerable<Sponsor>> FindAsync(System.Linq.Expressions.Expression<Func<Sponsor, bool>> predicate)
        {
            return await _context.Sponsors.Where(predicate).ToListAsync();
        }
    }
}
