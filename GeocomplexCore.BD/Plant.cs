using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Раститетельность
    /// </summary>
    public partial class Plant
    {
        public int PlantId { get; set; }
        public int FWatchpoint { get; set; }
        /// <summary>
        /// Густота леса
        /// </summary>
        public int PlantForestDensity { get; set; }
        /// <summary>
        /// Высота подроста
        /// </summary>
        public int PlantHeightUndergrowth { get; set; }
        /// <summary>
        /// Проективное покрытие подроста
        /// </summary>
        public int? PlantProjcoverUndergrowth { get; set; }
        public int FUsrAdd { get; set; }
        /// <summary>
        /// Дата добавления
        /// </summary>
        public DateOnly? PlantData { get; set; }
        /// <summary>
        /// Древостой
        /// </summary>
        public string? PlantStands { get; set; }
        /// <summary>
        /// Подрост
        /// </summary>
        public string? PlantUndergrowth { get; set; }
        /// <summary>
        /// Кустарник
        /// </summary>
        public string? PlantBush { get; set; }
        /// <summary>
        /// Напочвенный покров (трава)
        /// </summary>
        public string? PlantGroundcover { get; set; }
        /// <summary>
        /// Санитарное состояние
        /// </summary>
        public string? PlantSanitarycondition { get; set; }
        /// <summary>
        /// Антропогенное воздействие
        /// </summary>
        public string? PlantHumanimpact { get; set; }
        /// <summary>
        /// Кустарничек
        /// </summary>
        public string? PlantSmallbush { get; set; }
        /// <summary>
        /// Проективное покрытие напочвенный покров (трава)
        /// </summary>
        public int PlantProjcoverGroundcover { get; set; }
        /// <summary>
        /// Густота кустарников
        /// </summary>
        public int? PlantDensityBush { get; set; }

        public virtual UserDatum FUsrAddNavigation { get; set; } = null!;
        public virtual Watchpoint FWatchpointNavigation { get; set; } = null!;
        public virtual GuideForestDensity PlantForestDensityNavigation { get; set; } = null!;
        public virtual GuideHeightUndergrowth PlantHeightUndergrowthNavigation { get; set; } = null!;
        public virtual GuideProjcoverGroundcover PlantProjcoverGroundcoverNavigation { get; set; } = null!;
        public virtual GuideProjcoverUndergrowth? PlantProjcoverUndergrowthNavigation { get; set; }
    }
}
