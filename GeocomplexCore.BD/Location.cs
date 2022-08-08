using System;
using System.Collections.Generic;

namespace GeocomplexCore.BD
{
    /// <summary>
    /// Местоположение
    /// </summary>
    public partial class Location
    {
        public int LocId { get; set; }
        public string? LocDistrict { get; set; }
        public string? LocLocality { get; set; }
        public string? LocRegion { get; set; }
    }
}
