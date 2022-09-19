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

        /// <summary>
        /// Окно сообщения пользователю об ошибки сохранения
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public static void ShowMessageError(string message)
        {
            MessageBox.Show(message, "Error Save", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        /// <summary>
        /// Окно сообщения пользователю о сохранении
        /// </summary>
        public static bool ShowMessageSave()
        {
            var message = "Вы точно хотите сохранить?";
            var result = MessageBox.Show(message, "Save file",
                     MessageBoxButton.YesNo,
                     MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes);
            {
                return true;
            }           

        }
        /// <summary>
        /// Окно сообщения пользователю о не введенных данных
        /// </summary>
        /// <returns></returns>
        public static void ShowMessageValidation()
        {
            var message = "Не все данные введены";
            MessageBox.Show(message, "Error Save", MessageBoxButton.OK, MessageBoxImage.Warning);
            
            

        }
        /// <summary>
        /// Окно сообщения информирования
        /// </summary>
        /// <param name="message"></param>
        public static void ShowMessageInformation(string message)
        {
            MessageBox.Show(message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }



    }
}
