using GeocomplexCore.Model.Coordinat;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// Список координит X,Y в десятичной системе 
        /// </summary>
        public ObservableCollection<CoordinatModel> dataCoordinats;

        public ConverterCordinatsService(ObservableCollection<CoordinatModel> dataCoord)
        {
            dataCoordinats = dataCoord;
        }

        public ConverterCordinatsService()
        {
            
        }


        /// <summary>
        /// Создание списка с форматированным данными
        /// </summary>
        /// <returns> Возвращает готовый список конвертированный в градусы минуты секунды
        public ObservableCollection<CoordinatModel> ConverterDecimal()
        {
            for (int i = 0; i < dataCoordinats.Count; i++)
            {
                dataCoordinats[i].PointX_Longitude = FormatingDecimal(Convert.ToDouble(dataCoordinats[i].PointX_Longitude));
                dataCoordinats[i].PointY_Width = FormatingDecimal(Convert.ToDouble(dataCoordinats[i].PointY_Width));
            }           
            return dataCoordinats;
        }
        /// <summary>
        /// Перевод десятичной системы в Градусы/минуты/секунды 
        /// </summary>
        /// <param name="X">Переменная которую нужно конвертировать</param>
        /// <returns></returns>
        public string FormatingDecimal(double? X)
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

