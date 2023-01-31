using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник цвет
    /// </summary>
    public partial class GuideColor
    {
        public GuideColor()
        {
            Grounds = new HashSet<Ground>();
            Surfacewaters = new HashSet<Surfacewater>();
        }

        public int IdColor { get; set; }
        public string NameColor { get; set; } = null!;
        public int? WaterColor { get; set; }
        public int? BreedColor { get; set; }
        public int? PrimaryColor { get; set; }
        public int? SecondaryColor { get; set; }

        public virtual ICollection<Ground> Grounds { get; set; }
        public virtual ICollection<Surfacewater> Surfacewaters { get; set; }
    }
}
