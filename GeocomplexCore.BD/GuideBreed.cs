using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Справочник породы
    /// </summary>
    public partial class GuideBreed
    {
        public GuideBreed()
        {
            Grounds = new HashSet<Ground>();
        }

        public int IdBreed { get; set; }
        public int? FTypegroundId { get; set; }
        public string NameBreed { get; set; } = null!;
        /// <summary>
        /// Окончание наименования
        /// </summary>
        public string? NamersBred { get; set; }

        public virtual GuideTypebreed? FTypeground { get; set; }
        public virtual ICollection<Ground> Grounds { get; set; }
    }
}
