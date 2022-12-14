using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Техногенный объект
    /// </summary>
    public partial class Techobject
    {
        public int TechobjId { get; set; }
        public int FWpointId { get; set; }
        public string? TechobjName { get; set; }
        /// <summary>
        /// Лицензия
        /// </summary>
        public string? TechobjLicense { get; set; }
        /// <summary>
        /// Источник техногенного воздействия
        /// </summary>
        public string? TechobjSource { get; set; }
        /// <summary>
        /// Наличие эксплуатационных скважин, родников, колодцев: 0 -нет, 1 - есть
        /// 
        /// </summary>
        public int? TechobjProducthole { get; set; }
        /// <summary>
        /// Наличие наблюдательных скважин: 0 - нет, 1 - есть 
        /// </summary>
        public int? TechobjWatchhole { get; set; }
        /// <summary>
        /// Наличие программы мониторинга качества воды: 0 - нет, 1 - есть
        /// 
        /// </summary>
        public int? TechobjMonitoring { get; set; }
        /// <summary>
        /// Количество эксплуатационных скважин
        /// </summary>
        public int? TechobjAmountProducthole { get; set; }
        /// <summary>
        /// Количество наблюдательных скважин  
        /// </summary>
        public int? TechobjAmountWatchhole { get; set; }
        /// <summary>
        /// Примечание
        /// </summary>
        public string? TechobjDescription { get; set; }
        public int FUserAdd { get; set; }
        public DateOnly TechobjData { get; set; }

        public virtual UserDatum FUserAddNavigation { get; set; } = null!;
        public virtual Watchpoint FWpoint { get; set; } = null!;
    }
}
