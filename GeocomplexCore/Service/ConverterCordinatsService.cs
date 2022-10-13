using GeocomplexCore.Model.Coordinat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.Service
{
    /// <summary>
    /// 
    /// </summary>
    internal class ConverterCordinatsService
    {
        /// <summary>
        /// Промежуточная переменная для подсчета градусов
        /// </summary>
        private int degrees;
        /// <summary>
        /// Промежуточная переменная для подсчета минут
        /// </summary>
        private int minutes;
        /// <summary>
        /// Промежуточная переменная для подсчета секунд
        /// </summary>
        private double seconds;

        /// <summary>
        /// Список Угловой меры в градусы минуты секунды
        /// </summary>
        public List<AngularMeasure> angulars;


        /// <summary>
        /// Список координит X,Y в десятичной системе 
        /// </summary>
        private List<CoordinatModel> dataCoordinats;

        public ConverterCordinatsService(List<CoordinatModel> dataCoord)
        {
            dataCoordinats = dataCoord;
            angulars = new List<AngularMeasure>();
        }


        /// <summary>
        /// Создание списка с форматированным данными
        /// </summary>
        /// <returns> Возвращает готовый список конвертированный в градусы минуты секунды
        public List<AngularMeasure> ConverterDecimal()
        {
            for (int i = 0; i < dataCoordinats.Count; i++)
            {
                angulars.Add(new AngularMeasure
                {
                    ID = dataCoordinats[i].ID,
                    PointZ = dataCoordinats[i].PointZ,
                    Longitude = FormatingDecimal(dataCoordinats[i].PointX),
                    Width = FormatingDecimal(dataCoordinats[i].PointY)
                });
            }           
            return angulars;
        }
        /// <summary>
        /// Перевод десятичной системы в Градусы/минуты/секунды 
        /// </summary>
        /// <param name="X">Переменная которую нужно конвертировать</param>
        /// <returns></returns>
        private string FormatingDecimal(double? X)
        {
            degrees = (int)X;
            // Получаем минуты
            var _m = (X - degrees) * 60;
            // Получаем целую часть для минут
            minutes = (int)_m;
            seconds = (double)((_m - minutes) * 60);
            seconds = Math.Round(seconds, 2);

            string result = string.Format("{0}° {1}' {2}''", degrees, minutes, seconds);
            return result;
        }
    }

}

