using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.Model.Coordinat
{
    /// <summary>
    /// Угловая мера. Градусы,минуты секунды
    /// </summary>
    internal class AngularMeasure
    {

        private int _degrees;
        /// <summary>
        /// Градусы
        /// </summary>
        public int Degrees
        {
            get { return _degrees; }
            set { _degrees = value; }
        }

       
        private int _minutes;
        /// <summary>
        /// Минуты
        /// </summary>
        public int Minutes
        {
            get { return _minutes; }
            set { _minutes = value; }
        }

        private double _seconds;
        /// <summary>
        /// Секунды
        /// </summary>
        public double Seconds
        {
            get { return _seconds; }
            set { _seconds = value; }
        }
    }
}
