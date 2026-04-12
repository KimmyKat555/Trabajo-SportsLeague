using System.ComponentModel.DataAnnotations.Schema;
namespace SportsLeague.Domain.Entities;

    public class TournamentSponsor : AuditBase
    {
        public int TournamentId { get; set; }
        public virtual Tournament Tournament { get; set; } = null!;

        public int SponsorId { get; set; }
        public virtual Sponsor Sponsor { get; set; } = null!;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ContractAmount { get; set; }
    }

