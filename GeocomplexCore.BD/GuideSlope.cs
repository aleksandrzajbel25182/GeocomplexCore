using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Крутизна склона
    /// </summary>
    public partial class GuideSlope
    {
        public GuideSlope()
        {
            Watchpoints = new HashSet<Watchpoint>();
        }

        public int IdSlope { get; set; }
        public string NameSlope { get; set; } = null!;

        public virtual ICollection<Watchpoint> Watchpoints { get; set; }
    }
}
