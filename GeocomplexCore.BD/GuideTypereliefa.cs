using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Тип рельефа
    /// </summary>
    public partial class GuideTypereliefa
    {
        public GuideTypereliefa()
        {
            Watchpoints = new HashSet<Watchpoint>();
        }

        public int IdTypereliefa { get; set; }
        public string NameTypereliefa { get; set; } = null!;

        public virtual ICollection<Watchpoint> Watchpoints { get; set; }
    }
}
