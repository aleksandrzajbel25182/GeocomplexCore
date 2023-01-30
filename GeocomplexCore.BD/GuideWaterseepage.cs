using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник Тип водопроявления
    /// </summary>
    public partial class GuideWaterseepage
    {
        public int IdWaterseepage { get; set; }
        public string NameWaterseepage { get; set; } = null!;
    }
}
