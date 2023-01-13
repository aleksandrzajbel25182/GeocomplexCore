using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник санитарное состояние
    /// </summary>
    public partial class GuideSanitarycondition
    {
        public int IdSanitar { get; set; }
        public string NameSanitar { get; set; } = null!;
    }
}
