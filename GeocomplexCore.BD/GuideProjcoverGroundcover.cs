using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник проективное покрытие напочвенный покров(трава)
    /// </summary>
    public partial class GuideProjcoverGroundcover
    {
        public GuideProjcoverGroundcover()
        {
            Plants = new HashSet<Plant>();
        }

        public int IdPrjGround { get; set; }
        public string NamePrjGround { get; set; } = null!;

        public virtual ICollection<Plant> Plants { get; set; }
    }
}
