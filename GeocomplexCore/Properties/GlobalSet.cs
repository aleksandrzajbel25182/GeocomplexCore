using GeocomplexCore.ViewsModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.Properties
{
    public static class GlobalSet
    {
        /// <summary>
        /// Переменная для Хранения какой юзер зашел
        /// </summary>
        public static string staticUserID { get; set; }

        /// <summary>
        /// Флаг для понятия какая страница открыта для добавления
        /// </summary>
        public static string FlagStatic { get; set; }

    }
}
