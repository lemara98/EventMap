using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventMap.Classes
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class Grad : INotifyPropertyChanged
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
            return obj is Grad grad &&
                   EqualityComparer<Drzava>.Default.Equals(DrzavaGradova, grad.DrzavaGradova);
        }

        private string _gradText;
        private Drzava _drzavaGradova;
        private bool _oznacen;
        private double _xx;
        private double _yy;

        public double XX
        {
            get
            {
                return _xx;
            }
            set
            {
                if (value != _xx)
                {
                    _xx = value;
                    OnPropertyChanged("XX");
                }
            }
        }

        public double YY
        {
            get
            {
                return _yy;
            }
            set
            {
                if (value != _yy)
                {
                    _yy = value;
                    OnPropertyChanged("YY");
                }
            }
        }

        public string GradText
        {
            get
            {
                return _gradText;
            }
            set
            {
                if (value != _gradText)
                {
                    _gradText = value;
                    OnPropertyChanged("GradText");
                }
            }
        }

        public Drzava DrzavaGradova
        {
            get
            {
                return _drzavaGradova;
            }
            set
            {
                if (value != _drzavaGradova)
                {
                    _drzavaGradova = value;
                    OnPropertyChanged("DrzavaGradova");
                }

            }
        }

        public bool Oznacen
        {
            get
            {
                return _oznacen;
            }
            set
            {
                if (value != _oznacen)
                {
                    _oznacen = value;
                    OnPropertyChanged("Oznacen");
                }
            }
        }

        public Grad()
        {
            this._gradText = "";
            this._drzavaGradova = new Drzava();
            this._oznacen = true;
            this._xx = 0;
            this._yy = 0;
        }

        public Grad(string naziv, string drzava, double xx, double yy)
        {
            this._gradText = naziv;
            this._drzavaGradova = new Drzava(drzava);
            this._oznacen = true;
            this._xx = xx;
            this._yy = yy;
        }

        public Grad(string naziv, Drzava drzava, double xx, double yy)
        {
            this._gradText = naziv;
            this._drzavaGradova = new Drzava(drzava);
            this._oznacen = true;
            this._xx = xx;
            this._yy = yy;
        }

        public Grad(Grad g, double xx, double yy)
        {
            this._gradText = g._gradText;
            this._drzavaGradova = new Drzava(g._drzavaGradova);
            this._oznacen = g._oznacen;
            this._xx = xx;
            this._yy = yy;
        }

    }
}
