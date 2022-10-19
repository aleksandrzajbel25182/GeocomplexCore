using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.Model.Coordinat
{

    /// <summary>
    /// Промежуточный класс хранящий координты X,Y в десятичной системе
    /// </summary>
    internal class CoordinatModel : INotifyPropertyChanged
    {
        private int _id;
        /// <summary>
        /// Координат X
        /// </summary>
        public int ID
        {
            get { return _id; }
            set => Set(ref _id, value);
        }

        private string? _pointX_longitude;
        /// <summary>
        /// Координат X
        /// </summary>
        public string? PointX_Longitude
        {
            get { return _pointX_longitude; }
            set => Set(ref _pointX_longitude, value);
        }
        private string? _pointY_width;

        /// <summary>
        /// Координат Y
        /// </summary>
        public string? PointY_Width
        {
            get { return _pointY_width; }
            set => Set(ref _pointY_width, value);

        }
        private double? _pointZ;
        /// <summary>
        /// Координат Z
        /// </summary>
        public double? PointZ
        {
            get { return _pointZ; }
            set => Set(ref _pointZ, value);

        }



        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string? PropertyName = null)
        {
            // Если значение поля которое хотим обновить уже соответсвует тому значение которое мы передали возвращаем ложь
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        #endregion
    }
}
