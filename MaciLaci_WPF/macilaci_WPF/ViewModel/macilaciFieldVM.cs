using MaciLaci.Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace macilaci_WPF.ViewModel
{
    public class macilaciFieldVM : ViewModelBase
    {
        private int _col = 0;
        private int _row = 0;

        private fType fieldType = fType.GRASS;

        private Brush _bgBrush = new SolidColorBrush(Colors.LawnGreen);

        public int Col
        {
            get { return _col; }
            set
            {
                _col = value;
                OnPropertyChanged();
            }
        }

        public int Row
        {
            get { return _row; }
            set
            {
                _row = value;
                OnPropertyChanged();
            }
        }

        public fType Type
        {
            get { return fieldType; }
            set
            {
                fieldType = value;
                matchType(fieldType);
                OnPropertyChanged();
            }
        }

        public Brush BackgroundBrush
        {
            get { return _bgBrush; }
            set { _bgBrush = value; OnPropertyChanged(); }
        }

        public macilaciFieldVM() { }

        public DelegateCommand? FieldChangeCommand { get; set; }

        private void matchType(fType typeToMatch)
        {
            switch (typeToMatch)
            {
                case fType.OBSTACLE:
                    BackgroundBrush = new SolidColorBrush(Colors.Gray);
                    break;
                case fType.ENEMY:
                    BackgroundBrush = new SolidColorBrush(Colors.Red);
                    break;
                case fType.BASKET:
                    BackgroundBrush = new SolidColorBrush(Colors.Yellow);
                    break;
                case fType.PLAYER:
                    BackgroundBrush = new SolidColorBrush(Colors.LightBlue);
                    break;
                case fType.GRASS:
                    BackgroundBrush = new SolidColorBrush(Colors.LawnGreen);
                    break;
                default:
                    break;
            }
        }
        
    }
}
