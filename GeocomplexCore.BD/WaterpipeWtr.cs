using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Водовод на водозаборе
    /// </summary>
    public partial class WaterpipeWtr
    {
        public WaterpipeWtr()
        {
            StreetWaterpipes = new HashSet<StreetWaterpipe>();
        }

        public int WaterpipeWtrId { get; set; }
        public int WpointId { get; set; }
        public double? WaterpipeWtrDiametrpipe { get; set; }
        public double? WaterpipeWtrMaterialpipe { get; set; }
        public double? WaterpipeWtrLength { get; set; }

        public virtual ICollection<StreetWaterpipe> StreetWaterpipes { get; set; }
    }
}
