using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// ЭГП
    /// </summary>
    public partial class Egp
    {
        public int EgpId { get; set; }
        public int FWpointId { get; set; }
        /// <summary>
        /// Cкорость развития процесса
        /// </summary>
        public double? EgpSpeed { get; set; }
        /// <summary>
        /// протяженность процесса
        /// </summary>
        public double? EgpLength { get; set; }
        /// <summary>
        /// глубина
        /// </summary>
        public double? EgpDeep { get; set; }
        /// <summary>
        /// ширина
        /// </summary>
        public double? EgpWidth { get; set; }
        /// <summary>
        /// объем
        /// </summary>
        public double? EgpVolume { get; set; }
        /// <summary>
        /// площадь
        /// </summary>
        public double? EgpArea { get; set; }
        public int? FGroupprocess { get; set; }
        public int? FTypeprocess { get; set; }
        public int? FVidprocess { get; set; }
        public int? FEgpelement { get; set; }
        public int? FUserId { get; set; }
        public DateOnly? DataEgp { get; set; }
        public string? EgpDescription { get; set; }

        public virtual GuideEgpelement? FEgpelementNavigation { get; set; }
        public virtual GuideGroupprocce? FGroupprocessNavigation { get; set; }
        public virtual GuideTypeprocess? FTypeprocessNavigation { get; set; }
        public virtual UserDatum? FUser { get; set; }
        public virtual GuideVidprocess? FVidprocessNavigation { get; set; }
        public virtual Watchpoint FWpoint { get; set; } = null!;
    }
}
