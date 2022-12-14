using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник Тип процесс ЭГП
    /// </summary>
    public partial class GuideTypeprocess
    {
        public GuideTypeprocess()
        {
            Egps = new HashSet<Egp>();
        }

        public int IdTypeprocess { get; set; }
        public string? NameTypeprocess { get; set; }
        public int? FGroupprocess { get; set; }

        public virtual ICollection<Egp> Egps { get; set; }
    }
}
