using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник Антропогенное воздействие
    /// </summary>
    public partial class GuideHumanimpact
    {
        public int IdHumanimpact { get; set; }
        public string NameHumanimpact { get; set; } = null!;
    }
}
