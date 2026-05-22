using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsLeague.Domain.Entities
{
    public class MatchLineup : AuditBase
    {
        public new int Id { get; set; }
        public int MatchId { get; set; }
        public virtual Match Match { get; set; } = null!;
        public int PlayerId { get; set; }
        public virtual Player Player { get; set; } = null!;
        public bool IsStarter { get; set; }
        public string Position { get; set; } = null!; 
    }
}
