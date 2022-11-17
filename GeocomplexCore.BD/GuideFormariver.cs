using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Форма речной долины
    /// </summary>
    public partial class GuideFormariver
    {
        public GuideFormariver()
        {
            Watchpoints = new HashSet<Watchpoint>();
        }

        public int IdFormariver { get; set; }
        public string NameFormariver { get; set; } = null!;

        public virtual ICollection<Watchpoint> Watchpoints { get; set; }
    }
}
