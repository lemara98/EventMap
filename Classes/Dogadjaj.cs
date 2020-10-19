using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace EventMap.Classes
{
    public enum POSECENOST_DOGADJAJA { DO_1000 = 0, IZMEDJU_1000_I_5000, IZMEDJU_5000_I_10000, PREKO_10000 }

    public enum TIP { MUZICKI = 0, FILMSKI, PIVSKI, SLIKARSKI, SAJAM}

    public class Dogadjaj : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }


        public static Dictionary<TIP, System.Drawing.Image> slikeFestivala = new Dictionary<TIP, System.Drawing.Image>()
        {
            { TIP.MUZICKI, System.Drawing.Image.FromFile("C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\music.png") },
            { TIP.FILMSKI, System.Drawing.Image.FromFile("C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\film.png") },
            { TIP.PIVSKI, System.Drawing.Image.FromFile("C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\beer.png") },
            { TIP.SLIKARSKI, System.Drawing.Image.FromFile("C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\art.png") },
            { TIP.SAJAM, System.Drawing.Image.FromFile("C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\fair.png") },
        };

        public static Dictionary<TIP, string> slikeFestivalaPutanje = new Dictionary<TIP, string>()
        {
            { TIP.MUZICKI, "C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\music.png" },
            { TIP.FILMSKI, "C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\film.png" },
            { TIP.PIVSKI, "C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\beer.png" },
            { TIP.SLIKARSKI, "C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\art.png" },
            { TIP.SAJAM, "C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\fair.png" },
        };

        private string _jedCitOzn;
        private string _naziv;
        private string _opis;
        private TIP _tip;
        private POSECENOST_DOGADJAJA _posecenost;
        private string _putanjaIkonice;
        private bool _humKar;
        private uint _troskovi; // U $
        private Drzava _drzava;
        private Grad _grad;
        private List<DateTime> _istorijaDatumaOdrzavanja;
        private DateTime _datumOdrzavanjaZaTekucuGodinu;
        private List<Etiketa> _listaEtiketa;
        private bool _vidljiv;

        private double _xx;
        private double _yy;

        // Pinovati lokaciju na mapi u odnosu na grad koji se nalazi u odredjenoj drzavi 
        public static List<Drzava> sveDrzave;  //= new List<Drzava>();
        public static Dictionary<string, List<Grad>> gradoviUDrzavi; //= new Dictionary<string, List<Grad>>();
        //public static Dictionary<string, LokacijaNaMapi> lokacijeGradovaNaMapi = new Dictionary<string, LokacijaNaMapi>();

        public Dogadjaj()
        {
            this._jedCitOzn = "";
            this._naziv = "";
            this._opis = "";
            this._tip = TIP.MUZICKI;
            this._posecenost = POSECENOST_DOGADJAJA.DO_1000;
            this._putanjaIkonice = "C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\help.png";
            this._humKar = true;
            this._troskovi = 10000;
            this._drzava = new Drzava();
            this._grad = new Grad();

            this._istorijaDatumaOdrzavanja = new List<DateTime>();

            this._datumOdrzavanjaZaTekucuGodinu = new DateTime();
            this._listaEtiketa = new List<Etiketa>();
            //  this._listaEtiketa = new Etiketa(Color.Aqua, this._jedCitOzn + " Etiketa1");

            //if (!this.Drzava.DrzavaText.Equals(""))
            //{
            //    if (!sveDrzave.Contains(this._drzava))
            //    {
            //        sveDrzave.Add(this._drzava);
            //        gradoviUDrzavi[this._drzava.DrzavaText] = new List<Grad>();
            //    }
            //    if (!gradoviUDrzavi[this._drzava.DrzavaText].Contains(this._grad))
            //        gradoviUDrzavi[this._drzava.DrzavaText].Add(this._grad);
            //}

           
            //this._jedCitOzn = "Proba_Ozn";
            //this._naziv = "Proba_Naziv";
            //this._opis = "Proba_Opis";
            //this._tip = TIP.FILMSKI;
            //this._posecenost = POSECENOST_DOGADJAJA.IZMEDJU_1000_I_5000;
            //this._putanjaIkonice = "C:\\Users\\Mile\\source\\repos\\EventMap2\\EventMap\\Resources\\Icons\\help.png";
            //this._humKar = true;
            //this._troskovi = 10000;
            //this._drzava = new Drzava("Srbija");
            //this._grad = new Grad("Novi Sad", this._drzava, 0, 0);

            //this._istorijaDatumaOdrzavanja = new List<DateTime>();
            //this._istorijaDatumaOdrzavanja.Add(new DateTime(2020, 6, 17));
            //this._istorijaDatumaOdrzavanja.Add(new DateTime(2019, 3, 2));
            //this._istorijaDatumaOdrzavanja.Add(new DateTime(2015, 8, 17));

            //this._datumOdrzavanjaZaTekucuGodinu = new DateTime(2025,1,10);
            //this._listaEtiketa = new List<Etiketa>();
            //this._listaEtiketa.Add(new Etiketa(System.Windows.Media.Colors.Red, this._jedCitOzn + " Etiketa1"));
            //this._listaEtiketa.Add(new Etiketa(this._jedCitOzn + " Etiketa2"));
            ////  this._listaEtiketa = new Etiketa(Color.Aqua, this._jedCitOzn + " Etiketa1");

            //if (!sveDrzave.Contains(this._drzava))
            //{
            //    sveDrzave.Add(this._drzava);
            //    gradoviUDrzavi[this._drzava.DrzavaText] = new List<Grad>();
            //}
            //if (!gradoviUDrzavi[this._drzava.DrzavaText].Contains(this._grad))
            //    gradoviUDrzavi[this._drzava.DrzavaText].Add(this._grad);
            this._vidljiv = true;


        }

        public Dogadjaj(string jedCitOzn, 
                        string naziv, 
                        string opis, 
                        TIP tip, 
                        POSECENOST_DOGADJAJA posecenost, 
                        string putanjaIkonice, 
                        bool humKar, 
                        uint troskovi, 
                        string drzava, 
                        string grad, 
                        List<DateTime> istorijaDatumaOdrzavanja, 
                        DateTime datumOdrzavanjaZaTekucuGodinu,
                        List<Etiketa> listaEtiketa,
                        double xx,
                        double yy)
        {
            _jedCitOzn = jedCitOzn;
            _naziv = naziv;
            _opis = opis;
            _tip = tip;
            _posecenost = posecenost;
            _putanjaIkonice = putanjaIkonice;
            _humKar = humKar;
            _troskovi = troskovi;
            _drzava = new Drzava(drzava);
            _grad = new Grad(grad, _drzava, xx, yy);
            _xx = xx;
            _yy = yy;
            _istorijaDatumaOdrzavanja = new List<DateTime>();
            if (istorijaDatumaOdrzavanja != null)
                _istorijaDatumaOdrzavanja.AddRange(istorijaDatumaOdrzavanja);
            _datumOdrzavanjaZaTekucuGodinu = new DateTime(datumOdrzavanjaZaTekucuGodinu.Year, datumOdrzavanjaZaTekucuGodinu.Month, datumOdrzavanjaZaTekucuGodinu.Day);
            _listaEtiketa = new List<Etiketa>(listaEtiketa);
            this._vidljiv = true;
            //if (!sveDrzave.Contains(this._drzava))
            //{
            //    sveDrzave.Add(this._drzava);
            //    gradoviUDrzavi[this._drzava.DrzavaText] = new List<Grad>();
            //}
            //if (!gradoviUDrzavi[this._drzava.DrzavaText].Contains(this._grad))
            //    gradoviUDrzavi[this._drzava.DrzavaText].Add(this._grad);
        }

        public Dogadjaj(Dogadjaj dog)
        {
            _jedCitOzn = dog._jedCitOzn;
            _naziv = dog._naziv;
            _opis = dog._opis;
            _tip = dog._tip;
            _posecenost = dog._posecenost;
            _putanjaIkonice = dog._putanjaIkonice;
            _humKar = dog._humKar;
            _troskovi = dog._troskovi;
            _drzava = new Drzava(dog._drzava);
            _grad = new Grad(dog._grad.GradText, dog._drzava.DrzavaText, _xx, _yy);
            _istorijaDatumaOdrzavanja = new List<DateTime>();
            if (dog._istorijaDatumaOdrzavanja != null)
                _istorijaDatumaOdrzavanja.AddRange(dog._istorijaDatumaOdrzavanja);
            _datumOdrzavanjaZaTekucuGodinu = new DateTime(dog._datumOdrzavanjaZaTekucuGodinu.Year, dog._datumOdrzavanjaZaTekucuGodinu.Month, dog._datumOdrzavanjaZaTekucuGodinu.Day);
            _listaEtiketa = new List<Etiketa>();
            if (dog._listaEtiketa != null)
                foreach (Etiketa etik in dog._listaEtiketa)
                {
                    Etiketa ddd = new Etiketa(etik);
                    _listaEtiketa.Add(ddd);

                }

            this._vidljiv = dog._vidljiv;
            //if (!sveDrzave.Contains(this._drzava))
            //{
            //    sveDrzave.Add(this._drzava);
            //    gradoviUDrzavi[this._drzava.DrzavaText] = new List<Grad>();
            //}
            //if (!gradoviUDrzavi[this._drzava.DrzavaText].Contains(this._grad))
            //    gradoviUDrzavi[this._drzava.DrzavaText].Add(this._grad);
        }

        public string JedCitOzn
        {
            get { return _jedCitOzn; }
            set 
            {
                if (value != _jedCitOzn)
                {
                    _jedCitOzn = value;
                    OnPropertyChanged("JedCitOzn");
                }
            }
        }

        public string Naziv
        {
            get { return _naziv; }
            set 
            {
                if (value != _naziv)
                {
                    _naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        public string Opis
        {
            get { return _opis; }
            set
            {
                if (value != _opis)
                {
                    _opis = value;
                    OnPropertyChanged("Opis");
                }
            }
        }

        public TIP Tip
        {
            get { return _tip; }
            set
            {
                if (value != _tip)
                {
                    _tip = value;
                    this._putanjaIkonice = slikeFestivalaPutanje[_tip];
                    OnPropertyChanged("PutanjaIkonice");
                    OnPropertyChanged("Tip");
                    OnPropertyChanged("TipIndex");
                }
            }
        }

        public int TipIndex
        {
            get { return (int)_tip; }
            set
            {
                if (value != (int)_tip)
                {
                    _tip = (TIP)value;
                    this._putanjaIkonice = slikeFestivalaPutanje[_tip];
                    OnPropertyChanged("TipIndex");
                    OnPropertyChanged("Tip");
                    OnPropertyChanged("PutanjaIkonice");
                }
            }
        }

        public POSECENOST_DOGADJAJA Posecenost
        {
            get { return _posecenost; }
            set
            {
                if (value != _posecenost)
                {
                    _posecenost = value;
                    OnPropertyChanged("Posecenost");
                }
            }
        }

        //public System.Drawing.Image Ikonica
        //{
        //    get 
        //    { 
        //        if (_ikonicaDogadjaja == null) return slikeFestivala[_tip];
        //        else return _ikonicaDogadjaja;
        //    }
        //    set
        //    {
        //        if (value != _ikonicaDogadjaja)
        //        {
        //            _ikonicaDogadjaja = value;
        //            OnPropertyChanged("Ikonica");
        //        }
        //    }
        //}

        public string PutanjaIkonice
        {
            get
            {
                this._putanjaIkonice = slikeFestivalaPutanje[this._tip];
                return this._putanjaIkonice;
            }
            set
            {
                if (value != this._putanjaIkonice)
                {
                    this._putanjaIkonice = value;
                    OnPropertyChanged("PutanjaIkonice");
                }
            }
        }

        public bool HumanitarniKarakter
        {
            get { return _humKar; }
            set
            {
                if (value != _humKar)
                {
                    _humKar = value;
                    OnPropertyChanged("HumanitarniKarakter");
                }
            }
        }

        public uint Troskovi
        {
            get { return _troskovi; }
            set
            {
                if (value != _troskovi)
                {
                    _troskovi = value;
                    OnPropertyChanged("Troskovi");
                }
            }
        }

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

        public List<DateTime> IstorijaDatumaOdrzavanja
        {
            get { return _istorijaDatumaOdrzavanja; }
            set
            {
                if (value != _istorijaDatumaOdrzavanja)
                {
                    _istorijaDatumaOdrzavanja = value;
                    OnPropertyChanged("IstorijaDatumaOdrzavanja");
                }
            }
        }

        public DateTime DatumOdrzavanja
        {
            get { return _datumOdrzavanjaZaTekucuGodinu; }
            set
            {
                if (value != _datumOdrzavanjaZaTekucuGodinu)
                {
                    _datumOdrzavanjaZaTekucuGodinu = value;
                    OnPropertyChanged("DatumOdrzavanja");
                }
            }
        }

        public List<Etiketa> ListaEtiketa
        {
            get { return _listaEtiketa; }
            set
            {
                if (value != _listaEtiketa)
                {
                    _listaEtiketa = value;
                    OnPropertyChanged("ListaEtiketa");
                }
            }
        }

        public bool Vidljiv
        {
            get { return _vidljiv; }
            set
            {
                if (value != _vidljiv)
                {
                    _vidljiv = value;
                    OnPropertyChanged("Vidljiv");
                }
            }
        }

        public List<POSECENOST_DOGADJAJA> PosecenostDogadjajaEnum
        {
            get
            {
                List<POSECENOST_DOGADJAJA> l = new List<POSECENOST_DOGADJAJA>
                {
                    POSECENOST_DOGADJAJA.DO_1000,
                    POSECENOST_DOGADJAJA.IZMEDJU_1000_I_5000,
                    POSECENOST_DOGADJAJA.IZMEDJU_5000_I_10000,
                    POSECENOST_DOGADJAJA.PREKO_10000
                };
                return l;
            }
        }


        public bool isEqual(Dogadjaj d)
        {
            if (this.JedCitOzn.Equals(d.JedCitOzn)) return true;
            return false;
        }


        public void azurirajDogadjaj(string nJedCitOzn,
                                         string nNaziv,
                                         string nOpis,
                                         TIP nTip,
                                         POSECENOST_DOGADJAJA nPosecenost,
                                         string putanjaIkonice,
                                         bool nHumanitarnogKaraktera,
                                         Drzava nDrzava,
                                         Grad nGrad,
                                         List<DateTime> nIstorijaDatumaOdrzavanja,
                                         DateTime nDatumOdrzavanja,
                                         List<Etiketa> listaEtiketa
            )
        {
            this._jedCitOzn = nJedCitOzn;
            this._naziv = nNaziv;
            this._opis = nOpis;
            this._tip = nTip;
            this._posecenost = nPosecenost;
            this._putanjaIkonice = putanjaIkonice;
            this._humKar = nHumanitarnogKaraktera;
            this._drzava = nDrzava;
            this._grad = nGrad;
            this._istorijaDatumaOdrzavanja = nIstorijaDatumaOdrzavanja;
            this._datumOdrzavanjaZaTekucuGodinu = nDatumOdrzavanja;
            this._listaEtiketa = listaEtiketa;
        }
    }
}
