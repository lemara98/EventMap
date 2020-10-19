using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace EventMap.Classes
{
    public class Etiketa : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        
        private string _info;
        private Color _boja;
        private SolidColorBrush _bojaBrush; 
        private string _jedinstvenaOznakaEtikete;

        public Etiketa()
        {
            _info = "";
            _boja = System.Windows.Media.Colors.Orange;
            _jedinstvenaOznakaEtikete = "";
        }

        public Etiketa(string info, System.Windows.Media.Color boja, string jedinstvenaOznakaEtikete)
        {
            _info = info;
            _boja = boja;
            _jedinstvenaOznakaEtikete = jedinstvenaOznakaEtikete;
        }

        public Etiketa(System.Windows.Media.Color boja, string jedinstvenaOznakaEtikete)
        {
            _info = "Prazna etiketa!";
            _boja = boja;
            _jedinstvenaOznakaEtikete = jedinstvenaOznakaEtikete;
        }

        public Etiketa(string jedinstvenaOznakaEtikete)
        {
            _info = "Još praznija etiketa";
            _boja = System.Windows.Media.Colors.Orange;
            _jedinstvenaOznakaEtikete = jedinstvenaOznakaEtikete;
        }

        public Etiketa(string info, string jedinstvenaOznakaEtikete)
        {
            _info = info;
            _boja = System.Windows.Media.Colors.Orange;
            _jedinstvenaOznakaEtikete = jedinstvenaOznakaEtikete;
        }

        public Etiketa(Etiketa etik)
        {
            _info = etik._info;
            _boja = etik._boja;
            _jedinstvenaOznakaEtikete = etik._jedinstvenaOznakaEtikete;
        }

        public string Info
        {
            get
            {
                return _info;
            }
            set
            {
                if (value != _info)
                {
                    _info = value;
                    OnPropertyChanged("Info");
                }
            }
        }

        public Color Boja
        {
            get
            {
                return _boja;
            }
            set
            {
                if (value != _boja)
                {
                    _boja = value;
                    this.BojaBrush = new SolidColorBrush(_boja);
                    OnPropertyChanged("Boja");
                }
            }
        }

        public SolidColorBrush BojaBrush
        {
            get
            {
                _bojaBrush = new SolidColorBrush(_boja);
                return _bojaBrush;
            }
            set
            {
                if (value != _bojaBrush)
                {
                    _bojaBrush = value;
                    OnPropertyChanged("BojaBrush");
                }
            }
        }

        public string JedinstvenaOznakaEtikete
        {
            get
            {
                return _jedinstvenaOznakaEtikete;
            }
            set
            {
                if (value != _jedinstvenaOznakaEtikete)
                {
                    _jedinstvenaOznakaEtikete = value;
                    OnPropertyChanged("JedinstvenaOznakaEtikete");
                }
            }
        }
    }
}
