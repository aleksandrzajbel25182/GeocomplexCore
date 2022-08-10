using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocomplexCore.Model
{   
    /// <summary>
    /// Модель данных меню для hamburgermenu
    /// </summary>
    public class MenuModel
    {

        private string _name;
        private string _imagePath; 

        public string Name { get=>_name;  }

        public string ImagePath { get=> _imagePath; }


        public MenuModel(string name, string path)
        {
            _name = name;
            _imagePath = path;
        }

    }
}
