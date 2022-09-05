using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.Model
{
    /// <summary>
    /// Статический локатор для передачи данных в обновление класса DataContainer
    /// </summary>
    public static class LocatorStatic
    {
        public static DataContainer Data { get; }
            = new DataContainer()
            {
                TextMainHeader = "ГЛАВНОЕ МЕНЮ",
                PageHeader =""
            };
    }
}
