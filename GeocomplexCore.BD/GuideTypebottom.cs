using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник Тип дна
    /// </summary>
    public partial class GuideTypebottom
    {
        public GuideTypebottom()
        {
            Surfacewaters = new HashSet<Surfacewater>();
        }

        public int IdTypebottom { get; set; }
        public string NameTypebottom { get; set; } = null!;

        public virtual ICollection<Surfacewater> Surfacewaters { get; set; }
    }
}
