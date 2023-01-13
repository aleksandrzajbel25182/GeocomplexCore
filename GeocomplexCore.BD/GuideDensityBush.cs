using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник густота кустарников
    /// </summary>
    public partial class GuideDensityBush
    {
        public int IdDbush { get; set; }
        public string NameDbush { get; set; } = null!;
    }
}
