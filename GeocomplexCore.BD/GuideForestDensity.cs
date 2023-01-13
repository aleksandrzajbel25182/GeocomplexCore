using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник густота леса
    /// </summary>
    public partial class GuideForestDensity
    {
        public GuideForestDensity()
        {
            Plants = new HashSet<Plant>();
        }

        public int IdDforest { get; set; }
        public string NameDforest { get; set; } = null!;

        public virtual ICollection<Plant> Plants { get; set; }
    }
}
