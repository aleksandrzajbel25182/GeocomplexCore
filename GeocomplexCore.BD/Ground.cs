using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Почва и грунт
    /// </summary>
    public partial class Ground
    {
        public int IdGround { get; set; }
        public int? FWpointId { get; set; }
        public double? FromGround { get; set; }
        public double? ToGround { get; set; }
        public int? FColor { get; set; }
        public string? FDopcolor { get; set; }
        public int? FBreedId { get; set; }
        public string? DescriptionGround { get; set; }
        public int? FUserId { get; set; }
        public DateOnly? DataGround { get; set; }

        public virtual GuideBreed? FBreed { get; set; }
        public virtual GuideColor? FColorNavigation { get; set; }
        public virtual UserDatum? FUser { get; set; }
        public virtual Watchpoint? FWpoint { get; set; }
    }
}
