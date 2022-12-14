using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Участки
    /// </summary>
    public partial class District
    {
        public District()
        {
            DistrictPoints = new HashSet<DistrictPoint>();
            Routes = new HashSet<Route>();
        }

        public int IdDistrict { get; set; }
        public int PrgId { get; set; }
        public string? NameDistrict { get; set; }
        public int? IdUser { get; set; }
        public DateOnly? DateAddDistrict { get; set; }

        public virtual UserDatum? IdUserNavigation { get; set; }
        public virtual Project Prg { get; set; } = null!;
        public virtual ICollection<DistrictPoint> DistrictPoints { get; set; }
        public virtual ICollection<Route> Routes { get; set; }
    }
}
