﻿using EventMap.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EventMap.Pages
{
    /// <summary>
    /// Interaction logic for HelpPage.xaml
    /// </summary>
    public partial class HelpPage : Page
    {
        public HelpPage()
        {
            InitializeComponent();
            //
            //
        }

        private void prikaziTutorijal_Click(object sender, RoutedEventArgs e)
        {
            PrikaziTutorijalDialog d = new PrikaziTutorijalDialog();
            this.Effect = new BlurEffect();
            d.ShowDialog();
            this.Effect = null;
            d.Close();
        }
    }
}
