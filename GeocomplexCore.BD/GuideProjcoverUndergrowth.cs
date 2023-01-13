using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник проективное покрытие подроста
    /// </summary>
    public partial class GuideProjcoverUndergrowth
    {
        public GuideProjcoverUndergrowth()
        {
            Plants = new HashSet<Plant>();
        }

        public int IdPrjUnder { get; set; }
        public string NamePrjUnder { get; set; } = null!;

        public virtual ICollection<Plant> Plants { get; set; }
    }
}
