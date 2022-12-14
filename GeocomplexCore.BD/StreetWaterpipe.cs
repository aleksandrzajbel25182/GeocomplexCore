using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Улицы снабжаемые водоводом 
    /// </summary>
    public partial class StreetWaterpipe
    {
        public int StWtrpipeId { get; set; }
        public int WaterpipeWtrId { get; set; }
        public string StWtrpipeName { get; set; } = null!;

        public virtual WaterpipeWtr WaterpipeWtr { get; set; } = null!;
    }
}
