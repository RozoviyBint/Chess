using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ChessEngine
{
    public class BaseNotificationClass : INotifyPropertyChanged//Когда объект класса изменяет значение свойства, то он через событие
                                                               //PropertyChanged извещает систему об изменении свойства.
                                                               //А система обновляет все привязанные объекты.
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
