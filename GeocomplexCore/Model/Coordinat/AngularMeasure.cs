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
        private int _id;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }



        private string _longitude;
        /// <summary>
        /// Долгота
        /// </summary>
        public string Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        private string _width;
        /// <summary>
        /// Долгота
        /// </summary>
        public string Width
        {
            get { return _width; }
            set { _width = value; }
        }

        private double? _pointZ;
        /// <summary>
        /// Координат Z
        /// </summary>
        public double? PointZ
        {
            get { return _pointZ; }
            set { _pointZ = value; }

        }
    }
}
