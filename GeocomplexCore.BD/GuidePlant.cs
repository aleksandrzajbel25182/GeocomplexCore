using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник растительности
    /// </summary>
    public partial class GuidePlant
    {
        public int IdPlant { get; set; }
        public string NamePlant { get; set; } = null!;
        /// <summary>
        /// ID тип растительности
        /// </summary>
        public int FTypePlant { get; set; }

        public virtual GuideTypePlant FTypePlantNavigation { get; set; } = null!;
    }
}
