using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
     * Ohjelman pääikkuna. Vastaa menujen ja painikkeiden toiminnallisuudesta sekä niiden ja pelialueen
     * sijoittamisesta
     * Graafisten käyttöliittymien ohjelmoinnin harjoitustyö
     * @author Hannes Laukkanen
     * @version 19.8.2016
     */
    public partial class MainWindow : Window
    {        
        private Brush _buttonOffBrush = Brushes.LightBlue;
        private Brush _buttonOnBrush = Brushes.DarkGreen;

        public Brush ButtonOffBorderBrush
        {
            get { return _buttonOffBrush; }
            set { _buttonOffBrush = value;}
        }

        public Brush ButtonOnBorderBrush
        {
            get { return _buttonOnBrush; }
            set { _buttonOnBrush = value; }
        }
        
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Suoritetaan kun insert komento on annettu
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">ei käytetä</param>
        private void InsertCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            gameboard.ClickedInsertPiece();
            insertPieceButton.BorderBrush = ButtonOnBorderBrush;
        }

        /// <summary>
        /// Selvittää gameboardilta onko insert-komennon suorittaminen mahdollista
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">Välitetään tieto voidaanko komento suorittaa</param>
        private void InsertCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (gameboard == null) return;

            if (gameboard.PieceIsMoving)
            {
                e.CanExecute = false;
                return;
            }

            e.CanExecute = gameboard.CanInsert;            
        }

        /// <summary>
        /// Remove komennon antamisen jälkeen kutsutaan gameboardia poistamaan Piecen
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">ei käytetä</param>
        private void RemoveCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            gameboard.RemovePiece();
        }

        /// <summary>
        /// Selvittää gameboardilta, voiko sitä komentaa poistamaan pelinappulan
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">Välitetään tieto voidaanko komento suorittaa</param>
        private void RemoveCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (gameboard == null) return;

            if (gameboard.PieceIsMoving) e.CanExecute = false;
            else if ( gameboard.GetFocusedPiece() != null)
                e.CanExecute = true;                        
        }

        /// <summary>
        /// Suoritetaan kun on komennettu, että pelinappulaa aiotaan liikuttaa
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">ei käytetä</param>
        private void MovePieceCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            gameboard.FocusedPieceIsMoving();

            movePieceButton.BorderBrush = ButtonOnBorderBrush;
        }

        /// <summary>
        /// Selvitetään voiko pelinappulaa liikuttaa
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">välitetään tieto voidaanko suorittaa</param>
        private void MovePieceCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (gameboard != null && gameboard.GetFocusedPiece() != null && gameboard.CanInsert)
            {
                e.CanExecute = true; 
            }                
        }

        /// <summary>
        /// Komennettu avaamaan settings dialogi
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">ei käytetä</param>
        private void Settings_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsDialog sd = new SettingsDialog();
            sd.ShowDialog();

            if (sd.DialogResult == true) Piece.NextPieceColor = sd.ChosenBrush;
        }

        /// <summary>
        /// Avataan About-dialogi kun sitä vastaavaa menuitemiä klikattu
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">ei käytetä </param>
        private void AboutItem_Click(object sender, RoutedEventArgs e)
        {
            new AboutDialog().ShowDialog();
        }

        /// <summary>
        /// Avataan käyttöohjeiden www-sivu
        /// </summary>
        /// <param name="sender">ei käytetä</param>
        /// <param name="e">käytään sivun osoitteen selvittämiseen</param>
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;            
        }

        /// <summary>
        /// Sulkee ohjelman
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandClose_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// kun klikkaus on tullut "kuplana" windowlle asti, window ilmoittaa gameboardille että
        /// jostain on klikattu. Tarpeen lähinnä siksi että gameboard tietää jos on klikattu muualta
        /// kuin sen hallinnoimilta alueilta. Lisäksi palautetaan buttoneille ei-valittuna olemisen
        /// ilmaiseva oletus väri.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            gameboard.EventComplited();

            foreach (var content in buttons.Children)
            {
                Button button = content as Button;
                if (button != null) button.BorderBrush = ButtonOffBorderBrush;
            }
        }
    }


    /// <summary>
    /// Luokka komentojen luomista varten
    /// </summary>
    public class GameCommands
    {
        private static RoutedUICommand insert;
        private static RoutedUICommand remove;
        private static RoutedUICommand movePiece;
        private static RoutedUICommand settings;

        /// <summary>
        /// konstruktori joka alustaa tarvittavat komennot
        /// </summary>
        static GameCommands()
        {
            insert = new RoutedUICommand(
                "Insert Piece",
                "Insert",
                typeof(GameCommands));

            remove = new RoutedUICommand(
                "Remove Piece",
                "Remove",
                typeof(GameCommands));

            movePiece = new RoutedUICommand(
                "Move Piece",
                "MovePiece",
                typeof(GameCommands));

            settings = new RoutedUICommand(
                "Settings",
                "Settings",
                typeof(GameCommands));
        }

        /// <summary>
        /// palauttaa nappuloiden lisäämiseen käytettävän komennon
        /// </summary>
        public static RoutedUICommand Insert
        {
            get { return insert; }
        }

        /// <summary>
        /// palauttaa nappuloiden poistamiseen käytettävän komennon
        /// </summary>
        public static RoutedUICommand Remove
        {
            get { return remove; }
        }

        /// <summary>
        /// palauttaa nappuloiden liikuttamiseen käytettävän komennon
        /// </summary>
        public static RoutedUICommand MovePiece
        {
            get { return movePiece; }
        }

        /// <summary>
        /// palauttaa asetus dialogin käynnistävän komennon
        /// </summary>
        public static RoutedUICommand Settings
        {
            get { return settings; }
        }
    }
}
