using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник высоты подроста
    /// </summary>
    public partial class GuideHeightUndergrowth
    {
        public GuideHeightUndergrowth()
        {
            Plants = new HashSet<Plant>();
        }

        public int IdHeight { get; set; }
        public string NameHeight { get; set; } = null!;

        public virtual ICollection<Plant> Plants { get; set; }
    }
}
