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
        private double? _pointX;
        /// <summary>
        /// Координат X
        /// </summary>
        public double? PointX
        {
            get { return _pointX; }
            set => Set(ref _pointX, value);
        }
        private double? _pointY;

        /// <summary>
        /// Координат Y
        /// </summary>
        public double? PointY
        {
            get { return _pointY; }
            set => Set(ref _pointY, value);

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
