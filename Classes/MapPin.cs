using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EventMap.Classes
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MapPin : RadioButton, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private List<Dogadjaj> _listaDogadjaja;
        private int _brojDogadjaja;

        private double _x;
        private double _y;
        private Grad _grad;
        private Drzava _drzava;



        [JsonProperty]
        public List<Dogadjaj> ListaDogadjaja
        {
            get { return _listaDogadjaja; }
            set
            {
                if (value != _listaDogadjaja)
                {
                    _listaDogadjaja = value;
                    OnPropertyChanged("ListaDogadjaja");
                }
            }
        }

        public List<Dogadjaj> ListaVidljivihDogadjaja
        {
            get
            {
                List<Dogadjaj> l = new List<Dogadjaj>();
                foreach (Dogadjaj d in _listaDogadjaja)
                {
                    if (d.Vidljiv) l.Add(d);
                }
                return l;
            }
        }

        [JsonProperty]
        public int BrojDogadjaja
        {
            get { return _brojDogadjaja; }
            set
            {
                if (value != _brojDogadjaja)
                {
                    _brojDogadjaja = value;                        
                    OnPropertyChanged("BrojDogadjaja");
                }
            }
        }

        [JsonProperty]
        public double X
        {
            get { return _x; }
            set
            {
                if (value != _x)
                {
                    _x = value;
                    OnPropertyChanged("X");
                }
            }
        }

        [JsonProperty]
        public double Y
        {
            get { return _y; }
            set
            {
                if (value != _y)
                {
                    _y = value;
                    OnPropertyChanged("Y");
                }
            }
        }

        [JsonProperty]
        public Grad Grad
        {
            get { return _grad; }
            set
            {
                if (value != _grad)
                {
                    _grad = value;
                    OnPropertyChanged("Grad");
                }
            }
        }

        [JsonProperty]
        public Drzava Drzava
        {
            get { return _drzava; }
            set
            {
                if (value != _drzava)
                {
                    _drzava = value;
                    OnPropertyChanged("Drzava");
                }
            }
        }

        public void dodajDogadjaj(Dogadjaj d)
        {
            foreach (Dogadjaj x in this._listaDogadjaja)
            {
                if (x.isEqual(d))
                {
                    return;
                }
            }
            _listaDogadjaja.Add(d);
            ++_brojDogadjaja;
            OnPropertyChanged("ListaDogadjaja");
            OnPropertyChanged("BrojDogadjaja");

        }

        public void izbrisiDogadjaj(Dogadjaj d)
        {
            foreach (Dogadjaj x in this._listaDogadjaja)
            {
                if (x.isEqual(d))
                {
                    _listaDogadjaja.Remove(d);
                    OnPropertyChanged("ListaDogadjaja");
                    --_brojDogadjaja;
                    break;
                }
            }
        }

        public MapPin()
        {
            //_x = xx;
            //_y = yy;
            _drzava = new Drzava();
            _grad = new Grad();
            _listaDogadjaja = new List<Dogadjaj>();
            //_brojDogadjaja = 0;
            //this.Style = (Style)MainWindow.Resources["PinoviNaMapi"];
        }

        public MapPin(double xx, double yy, string grad, string drzava)
        {
            _x = xx;
            _y = yy;
            _drzava = new Drzava(drzava);
            _grad = new Grad(grad, _drzava, xx, yy);
            _listaDogadjaja = new List<Dogadjaj>();
            _brojDogadjaja = 0;
        }

        public MapPin(double xx, double yy, string grad, string drzava, List<Dogadjaj> lista)
        {
            _x = xx;
            _y = yy;
            _drzava = new Drzava(drzava);
            _grad = new Grad(grad, _drzava, xx, yy);
            _listaDogadjaja = new List<Dogadjaj>(lista);
            _brojDogadjaja = _listaDogadjaja.Count;
        }

        public MapPin(double xx, double yy, string grad, string drzava, Dogadjaj dog)
        {
            _x = xx;
            _y = yy;
            _drzava = new Drzava(drzava);
            _grad = new Grad(grad, _drzava, xx, yy);
            _listaDogadjaja = new List<Dogadjaj>();
            _listaDogadjaja.Add(dog);
            _brojDogadjaja = 1;
        }

        public MapPin(List<Dogadjaj> listaDogadjaja, double x, double y, Grad grad, Drzava drzava)
        {
            _listaDogadjaja = new List<Dogadjaj>();
            foreach (Dogadjaj d in listaDogadjaja)
            {
                Dogadjaj ddd = new Dogadjaj(d);
                _listaDogadjaja.Add(ddd);

            }
            _brojDogadjaja = _listaDogadjaja.Count;
            _x = x;
            _y = y;
            _grad = grad;
            _drzava = drzava;
        }

        public MapPin(MapPin pin)
        {
            _listaDogadjaja = new List<Dogadjaj>();
            foreach (Dogadjaj d in pin._listaDogadjaja)
            {
                Dogadjaj ddd = new Dogadjaj(d);
                _listaDogadjaja.Add(ddd);

            }
            _brojDogadjaja = _listaDogadjaja.Count;
            _x = pin._x;
            _y = pin._y;
            _grad = new Grad(pin._grad, pin._x, pin._y);
            _drzava = new Drzava(pin._drzava);
        }
    }



}
