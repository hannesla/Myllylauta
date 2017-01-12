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
     * Dialogi joka näyttää taustatietoja ohjelmasta
     * Graafisten käyttöliittymien ohjelmoinnin harjoitustyö
     * @author Hannes Laukkanen
     * @version 19.8.2016
     */
    public partial class AboutDialog : Window
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ok-button sulkee dialogin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
