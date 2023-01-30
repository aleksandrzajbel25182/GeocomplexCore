using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник Запахи воды
    /// </summary>
    public partial class GuideSmellwater
    {
        public int IdSmellwater { get; set; }
        public string NameSmellwater { get; set; } = null!;
    }
}
