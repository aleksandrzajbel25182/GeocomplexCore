using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Высотность рельефа
    /// </summary>
    public partial class GuideHeightreliefa
    {
        public GuideHeightreliefa()
        {
            Watchpoints = new HashSet<Watchpoint>();
        }

        public int IdHeightreliefa { get; set; }
        public string NameHeightreliefa { get; set; } = null!;

        public virtual ICollection<Watchpoint> Watchpoints { get; set; }
    }
}
