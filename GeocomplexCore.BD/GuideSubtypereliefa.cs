using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Подтип рельефа
    /// </summary>
    public partial class GuideSubtypereliefa
    {
        public GuideSubtypereliefa()
        {
            Watchpoints = new HashSet<Watchpoint>();
        }

        public int IdSubtypereliefa { get; set; }
        public string NameSubtypereliefa { get; set; } = null!;

        public virtual ICollection<Watchpoint> Watchpoints { get; set; }
    }
}
