using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник группы процессов ЭГП
    /// </summary>
    public partial class GuideGroupprocce
    {
        public GuideGroupprocce()
        {
            Egps = new HashSet<Egp>();
        }

        public int IdGroupprocces { get; set; }
        public string NameGroupprocess { get; set; } = null!;

        public virtual ICollection<Egp> Egps { get; set; }
    }
}
