using System.ComponentModel;
using System.Windows.Media;

namespace OscDemo.Wpf
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _oscText;
        public string OscText
        {
            get
            {
                return _oscText;
            }

            set
            {
                _oscText = value;
                OnPropertyChanged("OscText");
            }
        }

        private Brush _bgColor;
        public Brush BgColor
        {
            get
            {
                return _bgColor;
            }

            set
            {
                _bgColor = value;
                OnPropertyChanged("BgColor");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }

        
       
    }
}
