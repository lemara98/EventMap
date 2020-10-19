using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMap.Classes
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Drzava : INotifyPropertyChanged
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Drzava drzava &&
                   _drzavaText == drzava._drzavaText;
        }

        private string _drzavaText;
        private bool _oznacena;

        public string DrzavaText
        {
            get
            {
                return _drzavaText;
            }
            set
            {
                if (value != _drzavaText)
                {
                    _drzavaText = value;
                    OnPropertyChanged("DrzavaText");
                }
            }
        }

        public bool Oznacena
        {
            get
            {
                return _oznacena;
            }
            set
            {
                if (value != _oznacena)
                {
                    _oznacena = value;
                    OnPropertyChanged("Oznacena");
                    ////////////
                    if (Dogadjaj.gradoviUDrzavi.ContainsKey(_drzavaText))
                        foreach(Grad gr in Dogadjaj.gradoviUDrzavi[_drzavaText])
                        {
                            gr.Oznacen = value;
                        }
                }
            }
        }



        public Drzava(string naziv)
        {
            this._drzavaText = naziv;
            _oznacena = true;
        }
        public Drzava()
        {
            this._drzavaText = "";
            _oznacena = true;
        }

        public Drzava(Drzava d)
        {
            this._drzavaText = d._drzavaText;
            this._oznacena = d._oznacena;
        }
    }
}
