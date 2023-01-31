using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Точка наблюдения
    /// </summary>
    public partial class Watchpoint
    {
        public Watchpoint()
        {
            Egps = new HashSet<Egp>();
            Grounds = new HashSet<Ground>();
            Plants = new HashSet<Plant>();
            Surfacewaters = new HashSet<Surfacewater>();
            Techobjects = new HashSet<Techobject>();
            WpointCoordinates = new HashSet<WpointCoordinate>();
        }

        public int WpointId { get; set; }
        public int RouteId { get; set; }
        public string? WpointType { get; set; }
        public string? WpointNumber { get; set; }
        public string? WpointLocation { get; set; }
        public DateOnly? WpointDateAdd { get; set; }
        public string? WpointNote { get; set; }
        public int FUserId { get; set; }
        public int FWpointCoord { get; set; }
        public string? WpointIndLandscape { get; set; }
        public int? IdTypereliefa { get; set; }
        public int? IdFormareliefa { get; set; }
        public int? IdExposition { get; set; }
        public int? IdSlope { get; set; }
        public int? IdFormariver { get; set; }
        public int? IdSubtypereliefa { get; set; }
        public int? IdHeightreliefa { get; set; }

        public virtual UserDatum FUser { get; set; } = null!;
        public virtual GuideSprexposition? IdExpositionNavigation { get; set; }
        public virtual GuideFormareliefa? IdFormareliefaNavigation { get; set; }
        public virtual GuideFormariver? IdFormariverNavigation { get; set; }
        public virtual GuideHeightreliefa? IdHeightreliefaNavigation { get; set; }
        public virtual GuideSlope? IdSlopeNavigation { get; set; }
        public virtual GuideSubtypereliefa? IdSubtypereliefaNavigation { get; set; }
        public virtual GuideTypereliefa? IdTypereliefaNavigation { get; set; }
        public virtual Route Route { get; set; } = null!;
        public virtual ICollection<Egp> Egps { get; set; }
        public virtual ICollection<Ground> Grounds { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<Surfacewater> Surfacewaters { get; set; }
        public virtual ICollection<Techobject> Techobjects { get; set; }
        public virtual ICollection<WpointCoordinate> WpointCoordinates { get; set; }
    }
}
