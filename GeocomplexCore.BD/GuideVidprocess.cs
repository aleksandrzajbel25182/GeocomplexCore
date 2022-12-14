using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник Вид процесса ЭГП
    /// </summary>
    public partial class GuideVidprocess
    {
        public GuideVidprocess()
        {
            Egps = new HashSet<Egp>();
        }

        public int IdTypeprocess { get; set; }
        public string NameTypeprocess { get; set; } = null!;

        public virtual ICollection<Egp> Egps { get; set; }
    }
}
