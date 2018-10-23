using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ArcFace.Core
{
    public class KNotifyPropertyChanged : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var exp = propertyExpression.Body as MemberExpression;
            if (exp == null)
                return;
            var propertyName = exp.Member.Name;
            OnPropertyChanged(propertyName);
        }
    }
}
