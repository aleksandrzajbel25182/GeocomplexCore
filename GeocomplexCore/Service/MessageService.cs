using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeocomplexCore.Service
{
    internal class MessageService
    {

        /// <summary>
        /// Сообщение пользователю
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public static void ShowMessage(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);

        }


    }
}
