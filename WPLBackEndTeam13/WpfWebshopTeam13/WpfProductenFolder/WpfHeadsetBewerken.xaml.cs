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
using WPLClassLibraryTeam13.Data;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;
using WPLClassLibraryTeam13.Tools;
using static WPLClassLibraryTeam13.Tools.ValidatieHelper;

namespace WpfWebshopTeam13
{
    /// <summary>
    /// De pagina om aanpassingen te doen aan de headsetcategorie
    /// </summary>
    public partial class WpfHeadsetBewerken : Window
    {
        private int productid;
        Headset h = new Headset();
        Productdetails p = new Productdetails();
        Admin admin = new Admin();
        SolidColorBrush rood = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFD6D6"));
        public WpfHeadsetBewerken(int productnummer, Admin a)
        {
            InitializeComponent();
            productid = productnummer;
            admin = a;
        }



        #region Load
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            QueryHeadset qh = new QueryHeadset();
            //Productdetails p = new Productdetails();
            //var result = qh.getSingelJoinResult(productnummer);
            DataTable result = qh.getSingelJoin(productid);
            DataRow dr = result.Rows[0];

            UpdateFormHeadsets(GetProductDetails(dr), GetHeadset(dr));

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Owner.Visibility = Visibility.Visible;

            //Door wpfinsertdata terug opnieuw op te roepen, is hij meteen geupdate
            WpfInsertData wpf = new WpfInsertData(admin);
            wpf.ShowDialog();

        }
        private Headset GetHeadset(DataRow dr)
        {

            h.DiameterDrivers = dr[Headset.Database.Columns.Diameter_Drivers].ToString();
            h.InklapbareMicro = dr[Headset.Database.Columns.InklapbareMic].ToString();
            h.HeadsetId = int.Parse(dr[Headset.Database.Columns.HeadsetID].ToString());
            h.Verbinding = dr[Headset.Database.Columns.Verbinding].ToString();
            h.Verlichting = dr[Headset.Database.Columns.Verlichting].ToString();
            return h;
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
        private void UpdateFormHeadsets(Productdetails p, Headset h)
        {


            TxtProducttype.Text = p.Type;
            TxtPrijs.Text = p.Prijs.ToString();
            Txtuitgebr_beschrijving.Text = p.Uitgebreide_beschrijving.ToString();
            TxtKorting.Text = p.Korting.ToString();
            TxtKleineBeschrijving.Text = p.Beschrijving.ToString();
            TxtKleur.Text = p.Kleur.ToString();
            TxtVoorraad.Text = p.Voorraad.ToString();
            TxtImages.Text = p.Images.ToString();


            TxtDiameterDrivers.Text = h.DiameterDrivers.ToString();
            TxtInklapbareMicrofoon.Text = h.InklapbareMicro.ToString();
            TxtVerbinding.Text = h.Verbinding.ToString();
            TxtVerlichting.Text = h.Verlichting.ToString();
            UpdateAfbeeldingen(TxtImages.Text, admin);
        }
        #endregion

        #region buttonclickevents
        private void BtnHeadsetsBewerken_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTextvakken())
            {
                try
                {
                    h.Update();
                    p.Update();
                    MessageBox.Show("Uw wijzigingen werden opgeslagen.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
        private void TxtInklapbareMicrofoon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.J || e.Key == Key.N)
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }
        private void TxtVerlichting_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.J || e.Key == Key.N)
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }
        private bool ValidateTextvakken()
        {
            bool typ, diam, inklap, licht, verbind, prs, krtng, kleinebeschr, grotebeschr, kleurke, vrrd, imgs, valideren, prijzen;
            float pr = 0;
            float kr = 0;
            valideren = false;
            if (VType(TxtProducttype.Text) && TxtProducttype.Text != "")
            {
                p.Type = TxtProducttype.Text;
                typ = true;
                TxtProducttype.Background = Brushes.Transparent;
            }
            else
            {
                typ = false;
                TxtProducttype.Background = rood;
            }
            if (TxtDiameterDrivers.Text != "")
            {
                diam = true;
                TxtDiameterDrivers.Background = Brushes.Transparent;
                h.DiameterDrivers = TxtDiameterDrivers.Text;
            }
            else
            {
                TxtDiameterDrivers.Background = rood;
                diam = false;
            }

            if (TxtInklapbareMicrofoon.Text != "")
            {
                inklap = true;
                TxtInklapbareMicrofoon.Background = Brushes.Transparent;
                h.InklapbareMicro = (TxtInklapbareMicrofoon.Text).ToUpper();
            }
            else
            {
                TxtInklapbareMicrofoon.Background = rood;
                inklap = false;
            }
            if (TxtVerlichting.Text != "")
            {
                TxtVerlichting.Background = Brushes.Transparent;
                licht = true;
                h.Verlichting = TxtVerlichting.Text.ToUpper();
            }
            else
            {
                licht = false;
                TxtVerlichting.Background = rood;
            }

            if (TxtVerbinding.Text != "")
            {
                verbind = true;
                TxtVerbinding.Background = Brushes.Transparent;
                h.Verbinding = TxtVerbinding.Text;
            }
            else
            {
                TxtVerbinding.Background = rood;
                verbind = false;
            }

            krtng = false;
            prijzen = false;

            if (TxtPrijs.Text != "" && float.TryParse(TxtPrijs.Text, out pr) && pr >= 0)
            {
                TxtPrijs.Background = Brushes.Transparent;
                prs = true;
                if (TxtKorting.Text != "" && float.TryParse(TxtKorting.Text, out kr) && kr>=0)
                {
                    krtng = true;
                    TxtKorting.Background = Brushes.Transparent;
                    if (pr > kr)
                    {
                        prijzen = true;
                        p.Prijs = pr;
                        p.Korting = kr;
                        TxtPrijs.Background = Brushes.White;
                        TxtKorting.Background = Brushes.White;
                    }
                    else
                    {
                        prs = false;
                        krtng = false;
                        TxtPrijs.Background = rood;
                        TxtKorting.Background = rood;
                    }
                }
                else
                {
                    krtng = false;
                    TxtKorting.Background = rood;
                }
            }
            else
            {
                TxtPrijs.Background = rood;
                prs = false;
            }

           
            

            if (TxtKleineBeschrijving.Text != "")
            {
                kleinebeschr = true;
                p.Beschrijving = TxtKleineBeschrijving.Text;
                TxtKleineBeschrijving.Background = Brushes.Transparent;
            }
            else
            {
                kleinebeschr = false;
                TxtKleineBeschrijving.Background = rood;
            }
            if (Txtuitgebr_beschrijving.Text != "")
            {
                grotebeschr = true;
                p.Uitgebreide_beschrijving = Txtuitgebr_beschrijving.Text;
                Txtuitgebr_beschrijving.Background = Brushes.Transparent;

            }
            else
            {
                grotebeschr = false;
                Txtuitgebr_beschrijving.Background = rood;
            }
            int vrd;
            if (TxtVoorraad.Text != "" && int.TryParse(TxtVoorraad.Text, out vrd) && vrd > -1)
            {
                TxtVoorraad.Background = Brushes.Transparent;
                p.Voorraad = vrd;
                vrrd = true;
            }
            else
            {
                TxtVoorraad.Background = rood;
                vrrd = false;
            }
            if (TxtKleur.Text != "")
            {
                TxtKleur.Background = Brushes.Transparent;
                kleurke = true;
                p.Kleur = TxtKleur.Text;
            }
            else
            {
                TxtKleur.Background = rood;
                kleurke = false;
            }


            if (TxtImages.Text != "")
            {
                p.Images = TxtImages.Text;
                imgs = true;
                TxtImages.Background = Brushes.Transparent;

            }
            else
            {
                imgs = false;
                TxtImages.Background = rood;
            }

            if (typ == true && diam == true && inklap == true && licht == true && verbind == true && prs == true && krtng == true && kleinebeschr == true &&
                grotebeschr == true && kleurke == true && vrrd == true && imgs == true && prijzen == true)
            {
                valideren = true;
            }
            return valideren;
        }

        private void BtnTerug_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            
        }

        private void BtnBlader_Click(object sender, RoutedEventArgs e)
        {
            // We hebben de admin nodig omdat zijn pad uniek is aan zijn computer
            
            if (TxtImages.Text == "")
            {
                TxtImages.Text = $"{FileHelper.ZoekEenAfbeelding(admin)}\n";
                UpdateAfbeeldingen(TxtImages.Text, admin);
            }
            else
            {
                TxtImages.Text += $"|{FileHelper.ZoekEenAfbeelding(admin)}\n";
                UpdateAfbeeldingen(TxtImages.Text, admin);
            }

        }
        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateAfbeeldingen(TxtImages.Text, admin);
            ValidateTextvakken();
        }

        #endregion
        #region afbeeldingen
        string[] pathArray;
        private void UpdateAfbeeldingen(string inhoud, Admin admin)
        {

            ImageHelper.Path = admin.Afbeeldingen_wpf;
            pathArray = ImageHelper.GetVolledigPad(inhoud, admin);



            if (pathArray.Length == 0)
            {
                ImgVoorbeeld.Source = null;
            }
            else if (pathArray.Length == 1)
            {
                if (File.Exists(pathArray[0]))
                    ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld.Source = null;

            }
            else
            {
                if (File.Exists(pathArray[1]))
                    ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld.Source = null;
            }

            // we bepalen hoeveel strings we in onze array hebben, om te weten hoeveel images we moeten opvullen (0 - 7)
            switch (pathArray.Length)
            {
                case 0:
                    MessageBox.Show("Geen rij geselecteerd.");
                    break;
                case 1:
                    Show_1_Afbeelding(pathArray);
                    break;
                case 2:
                    Show_2_Afbeeldingen(pathArray);
                    break;
                case 3:
                    Show_3_Afbeeldingen(pathArray);
                    break;
                case 4:
                    Show_4_Afbeeldingen(pathArray);
                    break;
                case 5:
                    Show_5_Afbeeldingen(pathArray);
                    break;
                case 6:
                    Show_6_Afbeeldingen(pathArray);
                    break;
                case 7:
                    Show_7_Afbeeldingen(pathArray);
                    break;
                default:
                    Show_7_Afbeeldingen(pathArray);
                    break;
            }

        }
        #region BUTTONCLICKEVENTS VAN IMAGES
        private void Show_1_Afbeelding(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            Img2.Content = null;
            Img3.Content = null;
            Img4.Content = null;
            Img5.Content = null;
            Img6.Content = null;
            Img7.Content = null;

        }
        private void Show_2_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            Img3.Content = null;
            Img4.Content = null;
            Img5.Content = null;
            Img6.Content = null;
            Img7.Content = null;

        }
        private void Show_3_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            if (File.Exists(pathArray[2]))
                Img3.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img3.Content = null;


            Img4.Content = null;
            Img5.Content = null;
            Img6.Content = null;
            Img7.Content = null;
        }
        private void Show_4_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            if (File.Exists(pathArray[2]))
                Img3.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img3.Content = null;

            if (File.Exists(pathArray[3]))
                Img4.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img4.Content = null;


            Img5.Content = null;
            Img6.Content = null;
            Img7.Content = null;
        }
        private void Show_5_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            if (File.Exists(pathArray[2]))
                Img3.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img3.Content = null;

            if (File.Exists(pathArray[3]))
                Img4.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img4.Content = null;

            if (File.Exists(pathArray[4]))
                Img5.Content = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
            else
                Img5.Content = null;

            Img6.Content = null;
            Img7.Content = null;
        }
        private void Show_6_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            if (File.Exists(pathArray[2]))
                Img3.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img3.Content = null;

            if (File.Exists(pathArray[3]))
                Img4.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img4.Content = null;

            if (File.Exists(pathArray[4]))

                Img5.Content = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
            else
                Img5.Content = null;

            if (File.Exists(pathArray[5]))
                Img6.Content = new BitmapImage(new Uri(pathArray[5], UriKind.RelativeOrAbsolute));
            else
                Img6.Content = null;

            Img7.Content = null;
        }
        private void Show_7_Afbeeldingen(string[] pathArray)
        {
            if (File.Exists(pathArray[0]))
                Img1.Content = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
            else
                Img1.Content = null;

            if (File.Exists(pathArray[1]))
                Img2.Content = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
            else
                Img2.Content = null;

            if (File.Exists(pathArray[2]))
                Img3.Content = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
            else
                Img3.Content = null;

            if (File.Exists(pathArray[3]))
                Img4.Content = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
            else
                Img4.Content = null;

            if (File.Exists(pathArray[4]))

                Img5.Content = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
            else
                Img5.Content = null;

            if (File.Exists(pathArray[5]))
                Img6.Content = new BitmapImage(new Uri(pathArray[5], UriKind.RelativeOrAbsolute));
            else
                Img6.Content = null;

            if (File.Exists(pathArray[6]))
                Img7.Content = new BitmapImage(new Uri(pathArray[6], UriKind.RelativeOrAbsolute));

            else
                Img7.Content = null;
        }

        private void Img1_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
        }

        private void Img2_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
        }

        private void Img3_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
        }

        private void Img4_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
        }

        private void Img5_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
        }

        private void Img6_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[5], UriKind.RelativeOrAbsolute));
        }

        private void Img7_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld.Source = new BitmapImage(new Uri(pathArray[6], UriKind.RelativeOrAbsolute));
        }



        #endregion

        #endregion
        #region GotFocus events
        private void TxtProducttype_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtProducttype.Background = Brushes.Transparent;
        }

        private void TxtPrijs_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtPrijs.Background = Brushes.Transparent;
        }

        private void TxtKorting_GotFocus(object sender, RoutedEventArgs e)
        {
           TxtKorting.Background = Brushes.Transparent;
        }

        private void Txtuitgebr_beschrijving_GotFocus(object sender, RoutedEventArgs e)
        {
            Txtuitgebr_beschrijving.Background = Brushes.Transparent;
        }

        private void TxtKleineBeschrijving_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtKleineBeschrijving.Background = Brushes.Transparent;
        }

        private void TxtKleur_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtKleur.Background = Brushes.Transparent;
        }

        private void TxtVoorraad_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtVoorraad.Background = Brushes.Transparent;
        }

        private void TxtImages_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtImages.Background = Brushes.Transparent;
        }

        private void TxtDiameterDrivers_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtDiameterDrivers.Background = Brushes.Transparent;
        }

        private void TxtInklapbareMicrofoon_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtInklapbareMicrofoon.Background = Brushes.Transparent;
        }

        private void TxtVerbinding_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtVerbinding.Background = Brushes.Transparent;
        }

        private void TxtVerlichting_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtVerlichting.Background = Brushes.Transparent;
        }
        #endregion
    }

}
