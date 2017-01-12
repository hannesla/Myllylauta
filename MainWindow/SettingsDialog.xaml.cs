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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Ht
{
    /**
     * Settingistä voidaan valita pelinappuloiden väri
     * Graafisten käyttöliittymien ohjelmoinnin harjoitustyö
     * @author Hannes Laukkanen
     * @version 19.8.2016
     */
    public partial class SettingsDialog : Window
    {
        public Brush ChosenBrush { get; set; }

        /// <summary>
        /// Konstruktori
        /// </summary>
        public SettingsDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Perumisnappi jättää aikaisemman pelinappulan värin voimaan
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">ei käytetä</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        /// <summary>
        /// Valitsee klikatun nappulan värin lisättävien pelinappuloisen väriksi
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">välittä tiedon valitun nappulan väristä</param>
        private void ColorButton_Click(object sender, RoutedEventArgs e)
        {
            Button b = e.Source as Button;
            ChosenBrush = b.Background;

            DialogResult = true;

            Close();
        }
    }
}