using System;
using System.Collections.Generic;

namespace GeocomplexCore.DAL
{
    /// <summary>
    /// Поверхностные воды
    /// </summary>
    public partial class Surfacewater
    {
        public int SwId { get; set; }
        public int? FWpointId { get; set; }
        /// <summary>
        /// Тип водотока
        /// </summary>
        public int? SwTypewatercourseId { get; set; }
        /// <summary>
        /// Ширина русла
        /// </summary>
        public double? SwWidth { get; set; }
        /// <summary>
        /// Скорость течения
        /// </summary>
        public double? SwSpeedwater { get; set; }
        /// <summary>
        /// Тип дна
        /// </summary>
        public int? SwTypebottomId { get; set; }
        /// <summary>
        /// Температура воды
        /// </summary>
        public double? SwWatertemp { get; set; }
        /// <summary>
        /// Температура воздуха
        /// </summary>
        public double? SwAirtemp { get; set; }
        /// <summary>
        /// Цвет
        /// </summary>
        public int? SwColorId { get; set; }
        /// <summary>
        /// Запах
        /// </summary>
        public int? SwOdorwaterId { get; set; }
        public int? UserAddId { get; set; }
        public DateOnly? SwDateAdd { get; set; }
        /// <summary>
        /// Название.Примечание
        /// </summary>
        public string? SwName { get; set; }
        /// <summary>
        /// Дополнительные параметры
        /// </summary>
        public string? SwBloom { get; set; }
        /// <summary>
        /// Прозрачность воды
        /// </summary>
        public int? SwClaritywaterId { get; set; }
        /// <summary>
        /// Дополнительный цвет
        /// </summary>
        public string? SColorSecondary { get; set; }
        /// <summary>
        /// Расход потока м 3
        /// </summary>
        public double? SwWaterFlowRate { get; set; }
        /// <summary>
        /// Вкус воды
        /// </summary>
        public int? SwTastewaterId { get; set; }
    }
}
