using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Фото водозабор
    /// </summary>
    public partial class PhotoWaterintake
    {
        public PhotoWaterintake()
        {
            Waterintakes = new HashSet<Waterintake>();
        }

        public int PhotoWaterintakeId { get; set; }
        public string? PhotoWaterintakePath { get; set; }

        public virtual ICollection<Waterintake> Waterintakes { get; set; }
    }
}
