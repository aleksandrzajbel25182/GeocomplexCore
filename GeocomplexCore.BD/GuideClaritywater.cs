using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник Прозрачность воды
    /// </summary>
    public partial class GuideClaritywater
    {
        public int IdClaritywater { get; set; }
        public string NameClaritywater { get; set; } = null!;
    }
}
