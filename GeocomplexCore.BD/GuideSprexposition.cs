using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Экспозиция
    /// </summary>
    public partial class GuideSprexposition
    {
        public GuideSprexposition()
        {
            Watchpoints = new HashSet<Watchpoint>();
        }

        public int IdSprexposition { get; set; }
        public string NameSprexposition { get; set; } = null!;

        public virtual ICollection<Watchpoint> Watchpoints { get; set; }
    }
}
