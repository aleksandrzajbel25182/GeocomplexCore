using GeocomplexCore.ViewsModel.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.Model
{/// <summary>
/// Класс контейнер для шапки в главной странице
/// </summary>
    public class DataContainer: INotifyPropertyChanged
    {
        /// <summary>
        /// Шапка какая ГЛАВНАЯ страница открыта на данный момент ПОЛЕВОЙ ДНЕВНИК/КАТАЛОГ ....
        /// </summary>
        private string _textmainheader;

        public string TextMainHeader
        {
            get=>_textmainheader;
            set => Set(ref _textmainheader, value);
        }

        /// <summary>
        /// Шапка какая  страница открыта на данный момент проекты/участки....
        /// </summary>
        private string _pageheader;

        public string PageHeader
        {
            get =>_pageheader;
            set => Set(ref _pageheader, value);
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
