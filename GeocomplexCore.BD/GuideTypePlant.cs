using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник тип растительности
    /// </summary>
    public partial class GuideTypePlant
    {
        public GuideTypePlant()
        {
            GuidePlants = new HashSet<GuidePlant>();
        }

        public int IdTypePlant { get; set; }
        public string NameTypePlant { get; set; } = null!;

        public virtual ICollection<GuidePlant> GuidePlants { get; set; }
    }
}
