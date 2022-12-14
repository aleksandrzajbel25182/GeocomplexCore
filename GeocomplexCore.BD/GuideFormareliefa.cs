using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Форма рельефа
    /// </summary>
    public partial class GuideFormareliefa
    {
        public GuideFormareliefa()
        {
            Watchpoints = new HashSet<Watchpoint>();
        }

        public int IdFormareliefa { get; set; }
        public string NameFormareliefa { get; set; } = null!;

        public virtual ICollection<Watchpoint> Watchpoints { get; set; }
    }
}
