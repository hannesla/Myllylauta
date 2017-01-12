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
    public partial class Gameboard : Viewbox
    {
        private bool readyForInsert;
        private bool readyForMoving;
        private List<Piece> pieces = new List<Piece>();
        private Piece lastFocused;

        /// <summary>
        /// Palauttaa tiedon voiko laudalle vielä lisätä pelinappuloita
        /// </summary>
        public bool CanInsert
        {
            get { return (pieces.Count < 24); }
        }

        /// <summary>
        /// Palauttaa tiedon siitä, onko pelinappulan liikuttamistila käynnissä
        /// </summary>
        public bool PieceIsMoving
        {
            get { return readyForMoving; }
        }

        /// <summary>
        /// Konstruktori
        /// </summary>
        public Gameboard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ottaa vastaan tiedon, että kohta asetetaan uusi pelinappula ja valmistautuu siihen.
        /// </summary>
        internal void ClickedInsertPiece()
        {
            readyForInsert = true;           
        }

        /// <summary>
        /// Toteuttaa tilanteen mukaan pelinappulan liikuttamis- tai luomiskomennon klikattuun paikkaan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Ellipse elli = e.Source as Ellipse;
            int row = Grid.GetRow(elli);
            int column = Grid.GetColumn(elli);

            Piece pieceInHandling;

            if (readyForInsert) pieceInHandling = CreateNewPiece();
            else if (readyForMoving) pieceInHandling = GetFocusedPiece();
            else return;

            Grid.SetColumn(pieceInHandling, column);
            Grid.SetRow(pieceInHandling, row);

            lastFocused = pieceInHandling;
        }

        /// <summary>
        /// Luo uuden pelinappulan
        /// </summary>
        /// <returns>Luotu pelinappula</returns>
        private Piece CreateNewPiece()
        {
            Piece newPiece = new Piece();
            gameGrid.Children.Add(newPiece);
            pieces.Add(newPiece);

            return newPiece;
        }

        /// <summary>
        /// Ottaa vastaan tiedon, että kohdistuksessa olevaa nappulaa aiotaan liikuttaa ja valmistautuu
        /// siihen
        /// </summary>
        internal void FocusedPieceIsMoving()
        {
            readyForMoving = true;
        }

        /// <summary>
        /// Poistaa kohdistetun nappulan
        /// </summary>
        internal void RemovePiece()
        {
            pieces.Remove(lastFocused);
            gameGrid.Children.Remove(lastFocused);
        }

        /// <summary>
        /// Etsii nappulan jolla on focus
        /// </summary>
        /// <returns>Nappula jolla focus</returns>
        internal Piece GetFocusedPiece()
        {
            foreach (Piece piece in pieces)
            {
                if (piece.IsFocused)
                {
                    lastFocused = piece;
                    return piece;
                }
            }

            return null;
        }

        /// <summary>
        /// Metodi jota kutsumalla MainWindow ilmoittaa Gameboardille, että MouseUp on suoritettu jossain
        /// kohti ikkunaa ja liikkumis/lisäämisvalmius voidaan lopettaa.
        /// </summary>
        internal void EventComplited()
        {
            readyForMoving = false;
            readyForInsert = false;
        }
    }
}
