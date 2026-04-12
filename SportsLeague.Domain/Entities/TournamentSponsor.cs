using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Entities
{
    public class TournamentSponsor : AuditBase
    {
        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; } = null!;

        public int SponsorId { get; set; }
        public virtual Sponsor Sponsor { get; set; } = null!;

        public decimal ContractAmount { get; set; }
    }
}
