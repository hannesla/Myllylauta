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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ht
{
    /**
     * Kontrolli joka huolehtii pelialueen piirtämiseen ja toimintalogiikkaan liittyvistä 
     * toiminnallisuuksista
     * Graafisten käyttöliittymien ohjelmoinnin harjoitustyö
     * @author Hannes Laukkanen
     * @version 19.8.2016
     */
    public partial class Piece : Grid
    {
        private static Brush _nextPieceColor = Brushes.DarkRed;

        public static Brush NextPieceColor
        {
            get { return _nextPieceColor; }
            set { _nextPieceColor = value; }
        }

        public Brush PieceColor
        {
            get { return piece.Fill; }
            set { piece.Fill = value; }
        }

        /// <summary>
        /// Alustaa uudet pelinappulat sen mukaan, mikä väri on asetuksista valittu, tai jos ei ole valittu
        /// mitään, käyttää oletusta
        /// </summary>
        public Piece()
        {
            InitializeComponent();
            PieceColor = NextPieceColor;            
        }
        
        /// <summary>
        /// Kun Piecea klikataan (vapautetaan hiiren nappi sen kohdalla), se saa fokuksen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void piece_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Focus();
        }

        /// <summary>
        /// Kun Piece on käyttövalmis, se saa fokuksen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Focus();
        }
    }
}
