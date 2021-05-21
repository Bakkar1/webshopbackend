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
    /// Interaction logic for WpfMuisBewerken.xaml
    /// </summary>
    public partial class WpfMuisBewerken : Window
    {
        Admin admin = new Admin();
        private int productid;
        Productdetails p = new Productdetails();
        Muis m = new Muis();
        SolidColorBrush rood = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFD6D6"));
        public WpfMuisBewerken(int productnummer, Admin a)
        {
            InitializeComponent();
            productid = productnummer;
            admin = a;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QueryMuis qm = new QueryMuis();
            DataTable result = qm.getSingelJoin(productid);
            DataRow dr = result.Rows[0];
            UpdateFormMuizen(GetProductDetails(dr), GetMuis(dr));
        }

        private void BtnMuizenBewerken_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTextvakkenMuis())
            {
                try
                {
                    p.Update();
                    m.Update();
                    MessageBox.Show("Uw wijzigingen werden opgeslagen.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }


        }
        private bool ValidateTextvakkenMuis()
        {
            bool typ, prs, uitgebr, kleinebeschr, krting, kleur, vrrd, imgmuis, dpi, instdpi, prijzen,
                aantalknop, verb, verl;
            bool valideren = false;
            float pr = 0;
            float kr = 0;
            if (ValidatieHelper.VType(TxtProducttypeMuizen.Text) && TxtProducttypeMuizen.Text != "")
            {
                p.Type = TxtProducttypeMuizen.Text;
                typ = true;
                TxtProducttypeMuizen.Background = Brushes.Transparent;
            }
            else
            {
                typ = false;
                TxtProducttypeMuizen.Background = Brushes.IndianRed;
            }

            if (TxtVerlichtingMuis.Text != "")
            {
                TxtVerlichtingMuis.Background = Brushes.Transparent;
                verl = true;
                m.Verlichting = TxtVerlichtingMuis.Text.ToUpper();
            }
            else
            {
                verl = false;
                TxtVerlichtingMuis.Background = Brushes.IndianRed;
            }

            if (TxtVerbindingMuis.Text != "")
            {
                verb = true;
                TxtVerbindingMuis.Background = Brushes.Transparent;
                m.Verbinding = TxtVerbindingMuis.Text;
            }
            else
            {
                TxtVerbindingMuis.Background = Brushes.IndianRed;
                verb = false;
            }


            krting = false;
            prijzen = false;

            if (TxtPrijsMuizen.Text != "" && float.TryParse(TxtPrijsMuizen.Text, out pr) && pr >= 0)
            {
                TxtPrijsMuizen.Background = Brushes.Transparent;
                prs = true;
                if (TxtKortingMuizen.Text != "" && float.TryParse(TxtKortingMuizen.Text, out kr) && kr >= 0)
                {
                    krting = true;
                    TxtKortingMuizen.Background = Brushes.Transparent;
                    if (pr > kr)
                    {
                        prijzen = true;
                        p.Prijs = pr;
                        p.Korting = kr;
                        TxtPrijsMuizen.Background = Brushes.White;
                        TxtKortingMuizen.Background = Brushes.White;
                    }
                    else
                    {
                        prs = false;
                        krting = false;
                        TxtPrijsMuizen.Background = Brushes.IndianRed;
                        TxtKortingMuizen.Background = Brushes.IndianRed;
                    }
                }
                else
                {
                    krting = false;
                    TxtKortingMuizen.Background = Brushes.IndianRed;
                }
            }
            else
            {
                TxtPrijsMuizen.Background = Brushes.IndianRed;
                prs = false;
            }

            if (TxtKleineBeschrijvingMuizen.Text != "")
            {
                kleinebeschr = true;
                p.Beschrijving = TxtKleineBeschrijvingMuizen.Text;
                TxtKleineBeschrijvingMuizen.Background = Brushes.Transparent;
            }
            else
            {
                kleinebeschr = false;
                TxtKleineBeschrijvingMuizen.Background = Brushes.IndianRed;
            }
            if (Txtuitgebr_beschrijvingMuizen.Text != "")
            {
                uitgebr = true;
                p.Uitgebreide_beschrijving = Txtuitgebr_beschrijvingMuizen.Text;
                Txtuitgebr_beschrijvingMuizen.Background = Brushes.Transparent;

            }
            else
            {
                uitgebr = false;
                Txtuitgebr_beschrijvingMuizen.Background = Brushes.IndianRed;
            }
            int vrd;
            if (TxtVoorraadMuizen.Text != "" && int.TryParse(TxtVoorraadMuizen.Text, out vrd) && vrd > -1)
            {
                TxtVoorraadMuizen.Background = Brushes.Transparent;
                p.Voorraad = vrd;
                vrrd = true;
            }
            else
            {
                TxtVoorraadMuizen.Background = Brushes.IndianRed;
                vrrd = false;
            }
            if (TxtKleurMuizen.Text != "")
            {
                TxtKleurMuizen.Background = Brushes.Transparent;
                kleur = true;
                p.Kleur = TxtKleurMuizen.Text;
            }
            else
            {
                TxtKleurMuizen.Background = Brushes.IndianRed;
                kleur = false;
            }


            if (TxtImagesMuizen.Text != "")
            {
                p.Images = TxtImagesMuizen.Text;
                imgmuis = true;
                TxtImagesMuizen.Background = Brushes.Transparent;

            }
            else
            {
                imgmuis = false;
                TxtImagesMuizen.Background = Brushes.IndianRed;
            }
            int dpii = 0;
            if (TxtDPI.Text != "" && int.TryParse(TxtDPI.Text, out dpii))
            {
                m.Dpi = dpii;
                dpi = true;
                TxtDPI.Background = Brushes.Transparent;
            }
            else
            {
                dpi = false;
                TxtDPI.Background = Brushes.IndianRed;
            }
            int aantal = 0;
            if (TxtAantalKnoppen.Text != "" && int.TryParse(TxtAantalKnoppen.Text, out aantal))
            {
                m.AantalKnoppen = aantal;
                aantalknop = true;
                TxtAantalKnoppen.Background = Brushes.Transparent;

            }
            else
            {
                aantalknop = false;
                TxtAantalKnoppen.Background = Brushes.IndianRed;
            }
            if( TxtInstelbaarheidDpi.Text != "")
            {
                m.Instelbaarh_dpi = TxtInstelbaarheidDpi.Text;
                instdpi = true;
                TxtInstelbaarheidDpi.Background = Brushes.Transparent;

            }
            else
            {
                instdpi = false;
                TxtInstelbaarheidDpi.Background = Brushes.IndianRed;
            }
            if (typ == true && dpi == true && aantalknop == true && verl == true && prijzen == true && verb == true && prs == true && krting == true && kleinebeschr == true &&
                uitgebr == true && kleur == true && vrrd == true && imgmuis == true && instdpi == true)
            {
                valideren = true;
            }
            return valideren;
        }
    
        private void UpdateFormMuizen(Productdetails p, Muis m)
        {
            TxtProducttypeMuizen.Text = p.Type;
            TxtPrijsMuizen.Text = p.Prijs.ToString();
            Txtuitgebr_beschrijvingMuizen.Text = p.Uitgebreide_beschrijving;
            TxtKleineBeschrijvingMuizen.Text = p.Beschrijving;
            TxtKortingMuizen.Text = p.Korting.ToString();
            TxtKleurMuizen.Text = p.Kleur;
            TxtVoorraadMuizen.Text = p.Voorraad.ToString();
            TxtImagesMuizen.Text = p.Images;

            TxtDPI.Text = m.Dpi.ToString();
            TxtInstelbaarheidDpi.Text = m.Instelbaarh_dpi;
            TxtAantalKnoppen.Text = m.AantalKnoppen.ToString();
            TxtVerbindingMuis.Text = m.Verbinding;
            TxtVerlichtingMuis.Text = m.Verlichting;
            UpdateAfbeeldingenMuis(TxtImagesMuizen.Text, admin);

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
        private Muis GetMuis(DataRow dr)
        {
            m.MuisId = int.Parse(dr[Muis.Database.Columns.MuisID].ToString());
            m.Dpi = int.Parse(dr[Muis.Database.Columns.DPI].ToString());
            m.Instelbaarh_dpi = dr[Muis.Database.Columns.Instelbaarh_DPI].ToString();
            m.AantalKnoppen = int.Parse(dr[Muis.Database.Columns.AantalKnoppen].ToString());
            m.Verbinding = dr[Muis.Database.Columns.Verbinding].ToString();
            m.Verlichting = dr[Muis.Database.Columns.Verlichting].ToString();
            return m;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnBlader_Click(object sender, RoutedEventArgs e)
        {
            // We hebben de admin nodig omdat zijn pad uniek is aan zijn computer

            if (TxtImagesMuizen.Text == "")
            {
                TxtImagesMuizen.Text = $"{FileHelper.ZoekEenAfbeelding(admin)}\n";
                UpdateAfbeeldingenMuis(TxtImagesMuizen.Text, admin);
            }
            else
            {
                TxtImagesMuizen.Text += $"|{FileHelper.ZoekEenAfbeelding(admin)}\n";
                UpdateAfbeeldingenMuis(TxtImagesMuizen.Text, admin);
            }
        }
        string[] pathArrayMuis;
        private void UpdateAfbeeldingenMuis(string inhoud, Admin admin)
        {

            ImageHelper.Path = admin.Afbeeldingen_wpf;
            pathArrayMuis = ImageHelper.GetVolledigPad(inhoud, admin);



            if (pathArrayMuis.Length == 0)
            {
                ImgVoorbeeld2.Source = null;
            }
            else if (pathArrayMuis.Length == 1)
            {
                if (File.Exists(pathArrayMuis[0]))
                    ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld2.Source = null;

            }
            else
            {
                if (File.Exists(pathArrayMuis[1]))
                    ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayMuis[1], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld2.Source = null;
            }

            // we bepalen hoeveel strings we in onze array hebben, om te weten hoeveel images we moeten opvullen (0 - 7)
            switch (pathArrayMuis.Length)
            {
                case 0:
                    MessageBox.Show("Geen rij geselecteerd.");
                    break;
                case 1:
                    Show_1_Afbeelding(pathArrayMuis);
                    break;
                case 2:
                    Show_2_Afbeeldingen(pathArrayMuis);
                    break;
                case 3:
                    Show_3_Afbeeldingen(pathArrayMuis);
                    break;
                case 4:
                    Show_4_Afbeeldingen(pathArrayMuis);
                    break;
                case 5:
                    Show_5_Afbeeldingen(pathArrayMuis);
                    break;
                case 6:
                    Show_6_Afbeeldingen(pathArrayMuis);
                    break;
                case 7:
                    Show_7_Afbeeldingen(pathArrayMuis);
                    break;
                default:
                    Show_7_Afbeeldingen(pathArrayMuis);
                    break;
            }

        }
        #region BUTTONCLICKEVENTS VAN IMAGES
        private void Show_1_Afbeelding(string[] pathArrayMuis)
        {
            if (File.Exists(pathArrayMuis[0]))
                Img15.Content = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
            else
                Img15.Content = null;

            Img16.Content = null;
            Img17.Content = null;
            Img18.Content = null;
            Img19.Content = null;
            Img20.Content = null;
            Img21.Content = null;

        }
        private void Show_2_Afbeeldingen(string[] pathArrayMuis)
        {
            if (File.Exists(pathArrayMuis[0]))
                Img15.Content = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
            else
                Img15.Content = null;

            if (File.Exists(pathArrayMuis[1]))
                Img16.Content = new BitmapImage(new Uri(pathArrayMuis[1], UriKind.RelativeOrAbsolute));
            else
                Img16.Content = null;

            Img17.Content = null;
            Img18.Content = null;
            Img19.Content = null;
            Img20.Content = null;
            Img21.Content = null;

        }
        private void Show_3_Afbeeldingen(string[] pathArrayMuis)
        {
            if (File.Exists(pathArrayMuis[0]))
                Img15.Content = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
            else
                Img15.Content = null;

            if (File.Exists(pathArrayMuis[1]))
                Img16.Content = new BitmapImage(new Uri(pathArrayMuis[1], UriKind.RelativeOrAbsolute));
            else
                Img16.Content = null;

            if (File.Exists(pathArrayMuis[2]))
                Img17.Content = new BitmapImage(new Uri(pathArrayMuis[2], UriKind.RelativeOrAbsolute));
            else
                Img17.Content = null;


            Img18.Content = null;
            Img19.Content = null;
            Img20.Content = null;
            Img21.Content = null;
        }
        private void Show_4_Afbeeldingen(string[] pathArrayMuis)
        {
            if (File.Exists(pathArrayMuis[0]))
                Img15.Content = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
            else
                Img15.Content = null;

            if (File.Exists(pathArrayMuis[1]))
                Img16.Content = new BitmapImage(new Uri(pathArrayMuis[1], UriKind.RelativeOrAbsolute));
            else
                Img16.Content = null;

            if (File.Exists(pathArrayMuis[2]))
                Img17.Content = new BitmapImage(new Uri(pathArrayMuis[2], UriKind.RelativeOrAbsolute));
            else
                Img17.Content = null;

            if (File.Exists(pathArrayMuis[3]))
                Img18.Content = new BitmapImage(new Uri(pathArrayMuis[3], UriKind.RelativeOrAbsolute));
            else
                Img18.Content = null;


            Img19.Content = null;
            Img20.Content = null;
            Img21.Content = null;
        }
        private void Show_5_Afbeeldingen(string[] pathArrayMuis)
        {
            if (File.Exists(pathArrayMuis[0]))
                Img15.Content = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
            else
                Img15.Content = null;

            if (File.Exists(pathArrayMuis[1]))
                Img16.Content = new BitmapImage(new Uri(pathArrayMuis[1], UriKind.RelativeOrAbsolute));
            else
                Img16.Content = null;

            if (File.Exists(pathArrayMuis[2]))
                Img17.Content = new BitmapImage(new Uri(pathArrayMuis[2], UriKind.RelativeOrAbsolute));
            else
                Img17.Content = null;

            if (File.Exists(pathArrayMuis[3]))
                Img18.Content = new BitmapImage(new Uri(pathArrayMuis[3], UriKind.RelativeOrAbsolute));
            else
                Img18.Content = null;

            if (File.Exists(pathArrayMuis[4]))
                Img19.Content = new BitmapImage(new Uri(pathArrayMuis[4], UriKind.RelativeOrAbsolute));
            else
                Img19.Content = null;

            Img20.Content = null;
            Img21.Content = null;
        }
        private void Show_6_Afbeeldingen(string[] pathArrayMuis)
        {
            if (File.Exists(pathArrayMuis[0]))
                Img15.Content = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
            else
                Img15.Content = null;

            if (File.Exists(pathArrayMuis[1]))
                Img16.Content = new BitmapImage(new Uri(pathArrayMuis[1], UriKind.RelativeOrAbsolute));
            else
                Img16.Content = null;

            if (File.Exists(pathArrayMuis[2]))
                Img17.Content = new BitmapImage(new Uri(pathArrayMuis[2], UriKind.RelativeOrAbsolute));
            else
                Img17.Content = null;

            if (File.Exists(pathArrayMuis[3]))
                Img18.Content = new BitmapImage(new Uri(pathArrayMuis[3], UriKind.RelativeOrAbsolute));
            else
                Img18.Content = null;

            if (File.Exists(pathArrayMuis[4]))

                Img19.Content = new BitmapImage(new Uri(pathArrayMuis[4], UriKind.RelativeOrAbsolute));
            else
                Img19.Content = null;

            if (File.Exists(pathArrayMuis[5]))
                Img20.Content = new BitmapImage(new Uri(pathArrayMuis[5], UriKind.RelativeOrAbsolute));
            else
                Img20.Content = null;

            Img21.Content = null;
        }
        private void Show_7_Afbeeldingen(string[] pathArrayMuis)
        {
            if (File.Exists(pathArrayMuis[0]))
                Img15.Content = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
            else
                Img15.Content = null;

            if (File.Exists(pathArrayMuis[1]))
                Img16.Content = new BitmapImage(new Uri(pathArrayMuis[1], UriKind.RelativeOrAbsolute));
            else
                Img16.Content = null;

            if (File.Exists(pathArrayMuis[2]))
                Img17.Content = new BitmapImage(new Uri(pathArrayMuis[2], UriKind.RelativeOrAbsolute));
            else
                Img17.Content = null;

            if (File.Exists(pathArrayMuis[3]))
                Img18.Content = new BitmapImage(new Uri(pathArrayMuis[3], UriKind.RelativeOrAbsolute));
            else
                Img18.Content = null;

            if (File.Exists(pathArrayMuis[4]))

                Img19.Content = new BitmapImage(new Uri(pathArrayMuis[4], UriKind.RelativeOrAbsolute));
            else
                Img19.Content = null;

            if (File.Exists(pathArrayMuis[5]))
                Img20.Content = new BitmapImage(new Uri(pathArrayMuis[5], UriKind.RelativeOrAbsolute));
            else
                Img20.Content = null;

            if (File.Exists(pathArrayMuis[6]))
                Img21.Content = new BitmapImage(new Uri(pathArrayMuis[6], UriKind.RelativeOrAbsolute));

            else
                Img21.Content = null;
        }
        private void Img15_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
        }

        private void Img16_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayMuis[1], UriKind.RelativeOrAbsolute));
        }

        private void Img17_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayMuis[2], UriKind.RelativeOrAbsolute));
        }


        private void Img18_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayMuis[3], UriKind.RelativeOrAbsolute));

        }

        private void Img19_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayMuis[4], UriKind.RelativeOrAbsolute));
        }


        private void Img20_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayMuis[5], UriKind.RelativeOrAbsolute));

        }

        private void Img21_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayMuis[6], UriKind.RelativeOrAbsolute));
        }


        private void BtnRefreshMuis_Click(object sender, RoutedEventArgs e)
        {
            UpdateAfbeeldingenMuis(TxtImagesMuizen.Text, admin);
        }

        private void TxtInstelbaarheidDpi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.J || e.Key == Key.N)
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }

        private void TxtVerlichtingMuis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.J || e.Key == Key.N)
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }
    }
#endregion
}