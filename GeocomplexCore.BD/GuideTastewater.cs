using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник Вкус воды
    /// </summary>
    public partial class GuideTastewater
    {
        public int IdTestwater { get; set; }
        public string NameTestwater { get; set; } = null!;
    }
}
