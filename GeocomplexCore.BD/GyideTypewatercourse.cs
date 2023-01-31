using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник Тип водотока
    /// </summary>
    public partial class GyideTypewatercourse
    {
        public GyideTypewatercourse()
        {
            Surfacewaters = new HashSet<Surfacewater>();
        }

        public int IdTypewatercourse { get; set; }
        public string NameTypewatercourse { get; set; } = null!;

        public virtual ICollection<Surfacewater> Surfacewaters { get; set; }
    }
}
