using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;
using WPLClassLibraryTeam13.Tools;

namespace WpfWebshopTeam13.WpfProductenFolder
{
    /// <summary>
    /// Interaction logic for WpfToetsenbordBewerken.xaml
    /// </summary>
    public partial class WpfToetsenbordBewerken : Window
    {
        private int productid;
        Productdetails p = new Productdetails();
        Toetsenbord t = new Toetsenbord();
        Admin admin = new Admin();
        SolidColorBrush rood = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFD6D6"));

        public WpfToetsenbordBewerken(int productnummer, Admin a)
        {
            InitializeComponent();
            productid = productnummer;
            admin = a;
        }

        private void BtnToetsenbordenBewerken_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTextvakken())
            {

                try
                {
                    p.Type = TxtProducttypeToetsenbord.Text;
                    p.Prijs = float.Parse(TxtPrijsToetsenbord.Text);
                    p.Korting = float.Parse(TxtKortingToetsenbord.Text);
                    p.Beschrijving = TxtKleineBeschrijvingToetsenbord.Text;
                    p.Uitgebreide_beschrijving = Txtuitgebr_beschrijvingToetsenbord.Text;
                    p.Kleur = TxtKleurToetsenbord.Text;
                    p.Images = TxtImagesToetsenbord.Text;
                    p.Update();

                    t.ModelAzerty = TxtModel.Text;
                    t.NumeriekKlavier = TxtNumeriekKlavier.Text;
                    t.Verbinding = TxtVerbindingToetsenbord.Text;
                    t.Verlichting = TxtVerlichtingToetsenbord.Text;
                    t.Update();
                    MessageBox.Show("Uw wijzigingen werden opgeslagen.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QueryToetsenbord qt = new QueryToetsenbord();
            DataTable result = qt.getSingelJoin(productid);
            DataRow dr = result.Rows[0];
            UpdateFormToetsenborden(GetProductDetails(dr), GetToetsenbord(dr));
        }
        private void UpdateFormToetsenborden(Productdetails p, Toetsenbord t)
        {
            TxtProducttypeToetsenbord.Text = p.Type;
            TxtPrijsToetsenbord.Text = p.Prijs.ToString() ;
            TxtKortingToetsenbord.Text = p.Korting.ToString();
            TxtKleineBeschrijvingToetsenbord.Text = p.Beschrijving;
            Txtuitgebr_beschrijvingToetsenbord.Text = p.Uitgebreide_beschrijving;
            TxtKleurToetsenbord.Text = p.Kleur;
            TxtVoorraadToetsenbord.Text = p.Voorraad.ToString();
            TxtImagesToetsenbord.Text = p.Images;

            TxtModel.Text = t.ModelAzerty;
            TxtNumeriekKlavier.Text = t.NumeriekKlavier;
            TxtVerbindingToetsenbord.Text = t.Verbinding;
            TxtVerlichtingToetsenbord.Text = t.Verlichting;
            UpdateAfbeeldingenToetsenbord(TxtImagesToetsenbord.Text, admin);
        }
        private Productdetails GetProductDetails(DataRow dr)
        {
            p.Productnummer = productid;
            p.Type = dr[Productdetails.Database.Columns.Type].ToString();
            p.Prijs = float.Parse(dr[Productdetails.Database.Columns.Prijs].ToString());
            p.Korting = float.Parse(dr[Productdetails.Database.Columns.Korting].ToString());
            p.Beschrijving = dr[Productdetails.Database.Columns.Beschrijving].ToString();
            p.Uitgebreide_beschrijving = dr[Productdetails.Database.Columns.Uitgebreide_beschrijving].ToString();
            p.Kleur = dr[Productdetails.Database.Columns.Kleur].ToString();
            p.Voorraad = int.Parse(dr[Productdetails.Database.Columns.Voorraad].ToString());
            p.Images = dr[Productdetails.Database.Columns.Images].ToString();
            p.Categorie = dr[Productdetails.Database.Columns.Categorie].ToString();
            return p;
        }
        private Toetsenbord GetToetsenbord(DataRow dr)
        {
            t.ToetsenBordId = int.Parse(dr[Toetsenbord.Database.Columns.ToetsenbordID].ToString());
            t.ModelAzerty = dr[Toetsenbord.Database.Columns.ModelAzerty].ToString();
            t.NumeriekKlavier = dr[Toetsenbord.Database.Columns.NumeriekKlavier].ToString();
            t.Verbinding = dr[Toetsenbord.Database.Columns.Verbinding].ToString();
            t.Verlichting = dr[Toetsenbord.Database.Columns.Verlichting].ToString();
            
            
            
            return t;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
        }

       

        private bool ValidateTextvakken()
        {
            bool validatie = false;
            
            float pr = 0;
            float kr = 0;

            bool typ, prs, krting, kleinebeschr, grotebescht, kleurke, vrrd, imgs, model, numeriek, verbinding, verlichting, prijzen;


            if (ValidatieHelper.VType(TxtProducttypeToetsenbord.Text) && TxtProducttypeToetsenbord.Text != "")
            {
                typ = true;
                TxtProducttypeToetsenbord.Background = Brushes.Transparent;
            }
            else
            {
                typ = false;
                TxtProducttypeToetsenbord.Background = rood;
            }

            krting = false;
            prijzen = false;

            if (TxtPrijsToetsenbord.Text != "" && float.TryParse(TxtPrijsToetsenbord.Text, out pr) && pr >= 0)
            {
                TxtPrijsToetsenbord.Background = Brushes.Transparent;
                prs = true;
                if (TxtKortingToetsenbord.Text != "" && float.TryParse(TxtKortingToetsenbord.Text, out kr) && kr >= 0)
                {
                    krting = true;
                    TxtKortingToetsenbord.Background = Brushes.Transparent;
                    if (pr > kr)
                    {
                        prijzen = true;
                        p.Prijs = pr;
                        p.Korting = kr;
                        TxtPrijsToetsenbord.Background = Brushes.White;
                        TxtKortingToetsenbord.Background = Brushes.White;
                    }
                    else
                    {
                        prs = false;
                        krting = false;
                        TxtPrijsToetsenbord.Background = rood;
                        TxtKortingToetsenbord.Background = rood;
                    }
                }
                else
                {
                    krting = false;
                    TxtKortingToetsenbord.Background = rood;
                }
            }
            else
            {
                TxtPrijsToetsenbord.Background = rood;
                prs = false;
            }
            if (TxtKleineBeschrijvingToetsenbord.Text != "")
            {
                kleinebeschr = true;

                TxtKleineBeschrijvingToetsenbord.Background = Brushes.Transparent;
            }
            else
            {
                kleinebeschr = false;
                TxtKleineBeschrijvingToetsenbord.Background = rood;
            }

            if (Txtuitgebr_beschrijvingToetsenbord.Text != "")
            {
                grotebescht = true;
                Txtuitgebr_beschrijvingToetsenbord.Background = Brushes.Transparent;
            }
            else
            {
                grotebescht = false;
                Txtuitgebr_beschrijvingToetsenbord.Background = rood;
            }

            int vrd;
            if (TxtVoorraadToetsenbord.Text != "" && int.TryParse(TxtVoorraadToetsenbord.Text, out vrd) && vrd > -1)
            {
                TxtVoorraadToetsenbord.Background = Brushes.Transparent;

                vrrd = true;
            }
            else
            {
                TxtVoorraadToetsenbord.Background = rood;
                vrrd = false;
            }
            if (TxtKleurToetsenbord.Text != "")
            {
                TxtKleurToetsenbord.Background = Brushes.Transparent;
                kleurke = true;
            }
            else
            {
                TxtKleurToetsenbord.Background = rood;
                kleurke = false;
            }


            if (TxtImagesToetsenbord.Text != "")
            {

                imgs = true;
                TxtImagesToetsenbord.Background = Brushes.Transparent;

            }
            else
            {
                imgs = false;
                TxtImagesToetsenbord.Background = rood;
            }

            if (TxtModel.Text == "Azerty" || TxtModel.Text == "Qwerty")
            {
                model = true;
                TxtModel.Background = Brushes.Transparent;
            }
            else
            {
                model = false;
                TxtModel.Background = rood;
            }

            if (TxtNumeriekKlavier.Text == "J" || TxtNumeriekKlavier.Text == "N")
            {
                numeriek = true;
                TxtNumeriekKlavier.Background = Brushes.Transparent;
            }
            else
            {
                numeriek = false;
                TxtNumeriekKlavier.Background = rood;
            }

            if (TxtVerbindingToetsenbord.Text != "")
            {
                verbinding = true;
                TxtVerbindingToetsenbord.Background = Brushes.Transparent;
            }
            else
            {
                verbinding = false;
                TxtVerbindingToetsenbord.Background = rood;
            }

            if (TxtVerlichtingToetsenbord.Text == "J" || TxtVerlichtingToetsenbord.Text == "N")
            {
                verlichting = true;
                TxtVerlichtingToetsenbord.Background = Brushes.Transparent;
            }
            else
            {
                verlichting = false;
                TxtVerlichtingToetsenbord.Background = rood;
            }

            if (typ == true && prs == true && krting == true && kleinebeschr == true && grotebescht == true && kleurke == true && prijzen
                == true && vrrd == true && imgs == true && model == true && numeriek == true && verbinding == true && verlichting == true)
            {
                validatie = true;
            }


            return validatie;
        }

        private void BtnBlader_Click(object sender, RoutedEventArgs e)
        {
            // We hebben de admin nodig omdat zijn pad uniek is aan zijn computer

            if (TxtImagesToetsenbord.Text == "")
            {
                TxtImagesToetsenbord.Text = $"{FileHelper.ZoekEenAfbeelding(admin)}\n";
                UpdateAfbeeldingenToetsenbord(TxtImagesToetsenbord.Text, admin);
            }
            else
            {
                TxtImagesToetsenbord.Text += $"|{FileHelper.ZoekEenAfbeelding(admin)}\n";
                UpdateAfbeeldingenToetsenbord(TxtImagesToetsenbord.Text, admin);
            }
        }
        string[] pathArrayToetsenbord;
        private void UpdateAfbeeldingenToetsenbord(string inhoud, Admin admin)
        {

            ImageHelper.Path = admin.Afbeeldingen_wpf;
            pathArrayToetsenbord = ImageHelper.GetVolledigPad(inhoud, admin);



            if (pathArrayToetsenbord.Length == 0)
            {
                ImgVoorbeeld4.Source = null;
            }
            else if (pathArrayToetsenbord.Length == 1)
            {
                if (File.Exists(pathArrayToetsenbord[0]))
                    ImgVoorbeeld4.Source = new BitmapImage(new Uri(pathArrayToetsenbord[0], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld4.Source = null;

            }
            else
            {
                if (File.Exists(pathArrayToetsenbord[1]))
                    ImgVoorbeeld4.Source = new BitmapImage(new Uri(pathArrayToetsenbord[1], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld4.Source = null;
            }

            // we bepalen hoeveel strings we in onze array hebben, om te weten hoeveel images we moeten opvullen (0 - 7)
            switch (pathArrayToetsenbord.Length)
            {
                case 0:
                    MessageBox.Show("Geen rij geselecteerd.");
                    break;
                case 1:
                    Show_1_AfbeeldingToetsenbord(pathArrayToetsenbord);
                    break;
                case 2:
                    Show_2_AfbeeldingenToetsenbord(pathArrayToetsenbord);
                    break;
                case 3:
                    Show_3_AfbeeldingenToetsenbord(pathArrayToetsenbord);
                    break;
                case 4:
                    Show_4_AfbeeldingenToetsenbord(pathArrayToetsenbord);
                    break;
                case 5:
                    Show_5_AfbeeldingenToetsenbord(pathArrayToetsenbord);
                    break;
                case 6:
                    Show_6_AfbeeldingenToetsenbord(pathArrayToetsenbord);
                    break;
                case 7:
                    Show_7_AfbeeldingenToetsenbord(pathArrayToetsenbord);
                    break;
                default:
                    Show_7_AfbeeldingenToetsenbord(pathArrayToetsenbord);
                    break;
            }

        }
        #region BUTTONCLICKEVENTS VAN IMAGES
        private void Show_1_AfbeeldingToetsenbord(string[] pathArrayToetsenbord)
        {
            if (File.Exists(pathArrayToetsenbord[0]))
                Img22.Content = new BitmapImage(new Uri(pathArrayToetsenbord[0], UriKind.RelativeOrAbsolute));
            else
                Img22.Content = null;

            Img23.Content = null;
            Img24.Content = null;
            Img25.Content = null;
            Img26.Content = null;
            Img27.Content = null;
            Img28.Content = null;

        }
        private void Show_2_AfbeeldingenToetsenbord(string[] pathArrayToetsenbord)
        {
            if (File.Exists(pathArrayToetsenbord[0]))
                Img22.Content = new BitmapImage(new Uri(pathArrayToetsenbord[0], UriKind.RelativeOrAbsolute));
            else
                Img22.Content = null;

            if (File.Exists(pathArrayToetsenbord[1]))
                Img23.Content = new BitmapImage(new Uri(pathArrayToetsenbord[1], UriKind.RelativeOrAbsolute));
            else
                Img23.Content = null;

            Img24.Content = null;
            Img25.Content = null;
            Img26.Content = null;
            Img27.Content = null;
            Img28.Content = null;

        }
        private void Show_3_AfbeeldingenToetsenbord(string[] pathArrayToetsenbord)
        {
            if (File.Exists(pathArrayToetsenbord[0]))
                Img22.Content = new BitmapImage(new Uri(pathArrayToetsenbord[0], UriKind.RelativeOrAbsolute));
            else
                Img22.Content = null;

            if (File.Exists(pathArrayToetsenbord[1]))
                Img23.Content = new BitmapImage(new Uri(pathArrayToetsenbord[1], UriKind.RelativeOrAbsolute));
            else
                Img23.Content = null;

            if (File.Exists(pathArrayToetsenbord[2]))
                Img24.Content = new BitmapImage(new Uri(pathArrayToetsenbord[2], UriKind.RelativeOrAbsolute));
            else
                Img24.Content = null;


            Img25.Content = null;
            Img26.Content = null;
            Img27.Content = null;
            Img28.Content = null;
        }
        private void Show_4_AfbeeldingenToetsenbord(string[] pathArrayToetsenbord)
        {
            if (File.Exists(pathArrayToetsenbord[0]))
                Img22.Content = new BitmapImage(new Uri(pathArrayToetsenbord[0], UriKind.RelativeOrAbsolute));
            else
                Img22.Content = null;

            if (File.Exists(pathArrayToetsenbord[1]))
                Img23.Content = new BitmapImage(new Uri(pathArrayToetsenbord[1], UriKind.RelativeOrAbsolute));
            else
                Img23.Content = null;

            if (File.Exists(pathArrayToetsenbord[2]))
                Img24.Content = new BitmapImage(new Uri(pathArrayToetsenbord[2], UriKind.RelativeOrAbsolute));
            else
                Img24.Content = null;

            if (File.Exists(pathArrayToetsenbord[3]))
                Img25.Content = new BitmapImage(new Uri(pathArrayToetsenbord[3], UriKind.RelativeOrAbsolute));
            else
                Img25.Content = null;


            Img26.Content = null;
            Img27.Content = null;
            Img28.Content = null;
        }
        private void Show_5_AfbeeldingenToetsenbord(string[] pathArrayMuis)
        {
            if (File.Exists(pathArrayToetsenbord[0]))
                Img22.Content = new BitmapImage(new Uri(pathArrayToetsenbord[0], UriKind.RelativeOrAbsolute));
            else
                Img22.Content = null;

            if (File.Exists(pathArrayToetsenbord[1]))
                Img23.Content = new BitmapImage(new Uri(pathArrayToetsenbord[1], UriKind.RelativeOrAbsolute));
            else
                Img23.Content = null;

            if (File.Exists(pathArrayToetsenbord[2]))
                Img24.Content = new BitmapImage(new Uri(pathArrayToetsenbord[2], UriKind.RelativeOrAbsolute));
            else
                Img24.Content = null;

            if (File.Exists(pathArrayToetsenbord[3]))
                Img25.Content = new BitmapImage(new Uri(pathArrayToetsenbord[3], UriKind.RelativeOrAbsolute));
            else
                Img25.Content = null;

            if (File.Exists(pathArrayToetsenbord[4]))
                Img26.Content = new BitmapImage(new Uri(pathArrayToetsenbord[4], UriKind.RelativeOrAbsolute));
            else
                Img26.Content = null;

            Img27.Content = null;
            Img28.Content = null;
        }
        private void Show_6_AfbeeldingenToetsenbord(string[] pathArrayToetsenbord)
        {
            if (File.Exists(pathArrayToetsenbord[0]))
                Img22.Content = new BitmapImage(new Uri(pathArrayToetsenbord[0], UriKind.RelativeOrAbsolute));
            else
                Img22.Content = null;

            if (File.Exists(pathArrayToetsenbord[1]))
                Img23.Content = new BitmapImage(new Uri(pathArrayToetsenbord[1], UriKind.RelativeOrAbsolute));
            else
                Img23.Content = null;

            if (File.Exists(pathArrayToetsenbord[2]))
                Img24.Content = new BitmapImage(new Uri(pathArrayToetsenbord[2], UriKind.RelativeOrAbsolute));
            else
                Img24.Content = null;

            if (File.Exists(pathArrayToetsenbord[3]))
                Img25.Content = new BitmapImage(new Uri(pathArrayToetsenbord[3], UriKind.RelativeOrAbsolute));
            else
                Img25.Content = null;

            if (File.Exists(pathArrayToetsenbord[4]))

                Img26.Content = new BitmapImage(new Uri(pathArrayToetsenbord[4], UriKind.RelativeOrAbsolute));
            else
                Img26.Content = null;

            if (File.Exists(pathArrayToetsenbord[5]))
                Img27.Content = new BitmapImage(new Uri(pathArrayToetsenbord[5], UriKind.RelativeOrAbsolute));
            else
                Img27.Content = null;

            Img28.Content = null;
        }
        private void Show_7_AfbeeldingenToetsenbord(string[] pathArrayMuis)
        {
            if (File.Exists(pathArrayToetsenbord[0]))
                Img22.Content = new BitmapImage(new Uri(pathArrayToetsenbord[0], UriKind.RelativeOrAbsolute));
            else
                Img22.Content = null;

            if (File.Exists(pathArrayToetsenbord[1]))
                Img23.Content = new BitmapImage(new Uri(pathArrayToetsenbord[1], UriKind.RelativeOrAbsolute));
            else
                Img23.Content = null;

            if (File.Exists(pathArrayToetsenbord[2]))
                Img24.Content = new BitmapImage(new Uri(pathArrayToetsenbord[2], UriKind.RelativeOrAbsolute));
            else
                Img24.Content = null;

            if (File.Exists(pathArrayToetsenbord[3]))
                Img25.Content = new BitmapImage(new Uri(pathArrayToetsenbord[3], UriKind.RelativeOrAbsolute));
            else
                Img25.Content = null;

            if (File.Exists(pathArrayToetsenbord[4]))

                Img26.Content = new BitmapImage(new Uri(pathArrayToetsenbord[4], UriKind.RelativeOrAbsolute));
            else
                Img26.Content = null;

            if (File.Exists(pathArrayToetsenbord[5]))
                Img27.Content = new BitmapImage(new Uri(pathArrayToetsenbord[5], UriKind.RelativeOrAbsolute));
            else
                Img27.Content = null;

            if (File.Exists(pathArrayToetsenbord[6]))
                Img28.Content = new BitmapImage(new Uri(pathArrayToetsenbord[6], UriKind.RelativeOrAbsolute));

            else
                Img28.Content = null;
        }
        private void Img22_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld4.Source = new BitmapImage(new Uri(pathArrayToetsenbord[0], UriKind.RelativeOrAbsolute));
        }

        private void Img23_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld4.Source = new BitmapImage(new Uri(pathArrayToetsenbord[1], UriKind.RelativeOrAbsolute));
        }

        private void Img24_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld4.Source = new BitmapImage(new Uri(pathArrayToetsenbord[2], UriKind.RelativeOrAbsolute));
        }


        private void Img25_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld4.Source = new BitmapImage(new Uri(pathArrayToetsenbord[3], UriKind.RelativeOrAbsolute));

        }

        private void Img26_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld4.Source = new BitmapImage(new Uri(pathArrayToetsenbord[4], UriKind.RelativeOrAbsolute));
        }


        private void Img27_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld4.Source = new BitmapImage(new Uri(pathArrayToetsenbord[5], UriKind.RelativeOrAbsolute));

        }

        private void Img28_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld4.Source = new BitmapImage(new Uri(pathArrayToetsenbord[6], UriKind.RelativeOrAbsolute));
        }

    }
}
#endregion
