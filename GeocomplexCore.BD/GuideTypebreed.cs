using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Тип породы
    /// </summary>
    public partial class GuideTypebreed
    {
        public GuideTypebreed()
        {
            GuideBreeds = new HashSet<GuideBreed>();
        }

        public int IdTypebreed { get; set; }
        public string NameTypebreed { get; set; } = null!;

        public virtual ICollection<GuideBreed> GuideBreeds { get; set; }
    }
}
