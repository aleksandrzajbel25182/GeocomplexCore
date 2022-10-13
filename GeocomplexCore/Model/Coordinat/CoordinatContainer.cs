﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.Model.Coordinat
{
    /// <summary>
    /// Класс-контейнер в который ложится промежуточная информация для передачи списка в класс для конвертации в градусы минуты секунды
    /// </summary>
    internal class CoordinatContainer: INotifyPropertyChanged
    {
        private List<CoordinatModel> _coordinats = new List<CoordinatModel>();
        /// <summary>
        /// Список координат на основе класса CoordinatModel
        /// </summary>
        public List<CoordinatModel> Coordinats
        {
            get { return _coordinats; }
            set { _coordinats = value; }
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
