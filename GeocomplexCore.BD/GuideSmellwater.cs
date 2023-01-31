using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник Запахи воды
    /// </summary>
    public partial class GuideSmellwater
    {
        public GuideSmellwater()
        {
            Surfacewaters = new HashSet<Surfacewater>();
        }

        public int IdSmellwater { get; set; }
        public string NameSmellwater { get; set; } = null!;

        public virtual ICollection<Surfacewater> Surfacewaters { get; set; }
    }
}
