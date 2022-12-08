using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Справочник Вторичный элемент ЭГП
    /// </summary>
    public partial class GuideEgpelement
    {
        public GuideEgpelement()
        {
            Egps = new HashSet<Egp>();
        }

        public int IdEgpelement { get; set; }
        public string NameEgpelement { get; set; } = null!;

        public virtual ICollection<Egp> Egps { get; set; }
    }
}
