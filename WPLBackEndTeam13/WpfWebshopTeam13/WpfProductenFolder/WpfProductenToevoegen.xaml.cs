using System;
using System.Collections.Generic;
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
using static WpfWebshopTeam13.MainWindow;


namespace WpfWebshopTeam13
{
    /// <summary>
    /// Interaction logic for WpfProductenToevoegen.xaml
    /// </summary>
    public partial class WpfProductenToevoegen : Window
    {
        Admin admin = new Admin();
        SolidColorBrush rood = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFD6D6"));
        public WpfProductenToevoegen(Admin a)
        {
            InitializeComponent();
            //LblIngelogdeUser.Content = $"Ingelogde gebruiker: {Naam.Name}";
            admin = a;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Owner.Visibility = Visibility.Visible;
            //Door wpfinsertdata terug opnieuw op te roepen, is hij meteen geupdate
            WpfInsertData wpf = new WpfInsertData(admin);
            wpf.ShowDialog();
        }


        //Methode vooor de gebruiker te laten weten dat die met succes een product heeft toegevoed
        private void ProductToegevoegd(int productnummer, string productType)
        {
            MessageBox.Show($"Je hebt succesvol het Product '{productType}' met productnummer {productnummer} toegevoegd");
        }

        
        #region Headsets

        private void BtnHeadsetsToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (ValidatieTekstVakkenHeadsets())
            {
                //table Productdetails invullen met de gegegevens in de textboxen
                Productdetails product = new Productdetails();
                product.Type = TxtProducttype.Text;
                product.Prijs = float.Parse(TxtPrijs.Text);
                product.Korting = float.Parse(TxtKorting.Text);
                product.Beschrijving = TxtKleineBeschrijving.Text;
                product.Uitgebreide_beschrijving = Txtuitgebr_beschrijving.Text;
                product.Kleur = TxtKleur.Text;
                product.Voorraad = int.Parse(TxtVoorraad.Text);
                product.Images = TxtImages.Text;
                product.Categorie = "headset";
                var result = product.Insert();

                //table headsets invullen met de gegevens in de textboxen
                Headset headset = new Headset();
                headset.ProductNummer = result.NewId;
                headset.DiameterDrivers = TxtDiameterDrivers.Text;
                headset.InklapbareMicro = TxtInklapbareMicrofoon.Text.ToUpper();
                headset.Verbinding = TxtVerbinding.Text;
                headset.Verlichting = TxtVerlichting.Text.ToUpper();
                headset.Insert();

                //methodes oproepen voor het leeg maken van de textboxen en de gebruiker laten weten dat die een product heeft toegevoegd
                UpdateHeadsetForm();
                ProductToegevoegd(result.NewId, "headset");
            }
        }
        private void UpdateHeadsetForm()
        {
            TxtProducttype.Clear();
            TxtPrijs.Clear();
            TxtKorting.Clear();
            TxtKleineBeschrijving.Clear();
            Txtuitgebr_beschrijving.Clear();
            TxtKleur.Clear();
            TxtVoorraad.Clear();
            TxtImages.Clear();

            TxtDiameterDrivers.Clear();
            TxtInklapbareMicrofoon.Clear();
            TxtVerbinding.Clear();
            TxtVerlichting.Clear();
            Img1.Content = null;
            Img2.Content = null;
            Img3.Content = null;
            Img4.Content = null;
            Img5.Content = null;
            Img6.Content = null;
            Img7.Content = null;
            ImgVoorbeeld1.Source = null;
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
        private void BtnTerug1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnRefreshH_Click(object sender, RoutedEventArgs e)
        {
            UpdateAfbeeldingen(TxtImages.Text, admin);
        }
        private bool ValidatieTekstVakkenHeadsets()
        {

            bool typ, diam, inklap, licht, verbind, prs, prijzen, krtng, kleinebeschr, grotebeschr, kleurke, vrrd, imgs, valideren;
            float pr = 0;
            float kr = 0;
            valideren = false;
            if (ValidatieHelper.VType(TxtProducttype.Text) && TxtProducttype.Text != "")
            {

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

            }
            else
            {
                TxtVerbinding.Background = rood;
                verbind = false;
            }



            if (TxtPrijs.Text != "" && float.TryParse(TxtPrijs.Text, out pr))
            {
                TxtPrijs.Background = Brushes.Transparent;
                prs = true;
            }
            else
            {
                TxtPrijs.Background = rood;
                prs = false;
            }

            if (TxtKorting.Text != "" && float.TryParse(TxtKorting.Text, out kr))
            {
                krtng = true;
                TxtKorting.Background = Brushes.Transparent;
            }
            else
            {
                krtng = false;
                TxtKorting.Background = rood;
            }
            if (pr > kr)
            {
                prijzen = true;

                TxtPrijs.Background = Brushes.White;
                TxtKorting.Background = Brushes.White;
            }
            else
            {
                prijzen = false ;
                TxtPrijs.Background = rood;
                TxtKorting.Background = rood;
            }

            if (TxtKleineBeschrijving.Text != "")
            {
                kleinebeschr = true;

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

            }
            else
            {
                TxtKleur.Background = rood;
                kleurke = false;
            }


            if (TxtImages.Text != "")
            {

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

        #region afbeeldingen
        string[] pathArray;
        private void UpdateAfbeeldingen(string inhoud, Admin admin)
        {

            ImageHelper.Path = admin.Afbeeldingen_wpf;
            pathArray = ImageHelper.GetVolledigPad(inhoud, admin);



            if (pathArray.Length == 0)
            {
                ImgVoorbeeld1.Source = null;
            }
            else if (pathArray.Length == 1)
            {
                if (File.Exists(pathArray[0]))
                    ImgVoorbeeld1.Source = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld1.Source = null;

            }
            else
            {
                if (File.Exists(pathArray[1]))
                    ImgVoorbeeld1.Source = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld1.Source = null;
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
            ImgVoorbeeld1.Source = new BitmapImage(new Uri(pathArray[0], UriKind.RelativeOrAbsolute));
        }

        private void Img2_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld1.Source = new BitmapImage(new Uri(pathArray[1], UriKind.RelativeOrAbsolute));
        }

        private void Img3_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld1.Source = new BitmapImage(new Uri(pathArray[2], UriKind.RelativeOrAbsolute));
        }

        private void Img4_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld1.Source = new BitmapImage(new Uri(pathArray[3], UriKind.RelativeOrAbsolute));
        }

        private void Img5_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld1.Source = new BitmapImage(new Uri(pathArray[4], UriKind.RelativeOrAbsolute));
        }

        private void Img6_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld1.Source = new BitmapImage(new Uri(pathArray[5], UriKind.RelativeOrAbsolute));
        }

        private void Img7_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld1.Source = new BitmapImage(new Uri(pathArray[6], UriKind.RelativeOrAbsolute));
        }


        #endregion

        #endregion
        #endregion
        #region Laptops
        private void UpdateLaptopForm()
        {
            TxtProducttypeLaptops.Clear();
            TxtPrijsLaptops.Clear();
            TxtKortingLaptops.Clear();
            TxtKleineBeschrijvingLaptops.Clear();
            TxtUitgebreideBeschrijvingLaptops.Clear();
            TxtKleurLaptops.Clear();
            TxtVoorraadLaptops.Clear();
            TxtImagesLaptops.Clear();

            TxtOS.Clear();
            TxtProcessor.Clear();
            TxtMemory.Clear();
            TxtGraphics.Clear();
            TxtStorage.Clear();
            TxtDisplay.Clear();
            TxtKeyboard.Clear();
            TxtConnectivity.Clear();
            TxtBattery_and_adaptor.Clear();
            TxtTouchpad.Clear();
            TxtInput_and_output.Clear();
            TxtAudio.Clear();
            TxtAdditional_features.Clear();
            TxtFinish.Clear();
            TxtDimensions.Clear();
            TxtWeightlp.Clear();
        }
        private void BtnLaptopToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTekstvakkenlaptops())
            {
                //table Productdetails invullen met de gegegevens in de textboxen
                Productdetails pl = new Productdetails();
                pl.Type = TxtProducttypeLaptops.Text;
                pl.Prijs = float.Parse(TxtPrijsLaptops.Text);
                pl.Korting = float.Parse(TxtKortingLaptops.Text);
                pl.Beschrijving = TxtKleineBeschrijvingLaptops.Text;
                pl.Uitgebreide_beschrijving = TxtUitgebreideBeschrijvingLaptops.Text;
                pl.Kleur = TxtKleurLaptops.Text;
                pl.Voorraad = int.Parse(TxtVoorraadLaptops.Text);
                pl.Images = TxtImagesLaptops.Text;
                pl.Categorie = "laptop";
                var result = pl.Insert();

                //table laptops invullen met de gegevens in de textboxen
                Laptop laptop = new Laptop();
                laptop.ProductNummer = result.NewId;
                laptop.Os = TxtOS.Text;
                laptop.Processor = TxtProcessor.Text;
                laptop.Memory = TxtMemory.Text;
                laptop.Graphics = TxtGraphics.Text;
                laptop.Storage = TxtStorage.Text;
                laptop.Display = TxtDisplay.Text;
                laptop.Keyboard = TxtKeyboard.Text;
                laptop.Connectivity = TxtConnectivity.Text;
                laptop.Battery_and_adaptor = TxtBattery_and_adaptor.Text;
                laptop.Touchpad = TxtTouchpad.Text;
                laptop.Input_and_output = TxtInput_and_output.Text;
                laptop.Audio = TxtAudio.Text;
                laptop.Additional_features = TxtAdditional_features.Text;
                laptop.Finish = TxtFinish.Text;
                laptop.Dimensions = TxtDimensions.Text;
                laptop.Weight = TxtWeightlp.Text;
                laptop.Insert();

                //methodes oproepen voor het leeg maken van de textboxen en de gebruiker laten weten dat die een product heeft toegevoegd
                UpdateLaptopForm();
                ProductToegevoegd(result.NewId, "laptop");
            }
  
        }
        private void BtnRefreshLaptop_Click(object sender, RoutedEventArgs e)
        {
            UpdateAfbeeldingenlaptop(TxtImagesLaptops.Text, admin);
        }
        private bool ValidateTekstvakkenlaptops()
        {
            bool typ, prs, krtng, kleinebeschr, kleurke, vrrd, imgs, valideren, os, processor, geheugen, prijzen,
                opslagruimte, scherm, toetsenbord, connectiviteit, accu, touchpad, poorten, audio, bijkomende, finish, afmetingen, gewicht, videokaart;
            float pr = 0;
            float kr = 0;
            valideren = false;
            if (TxtMemory.Text != "")
            {
                geheugen = true;
                TxtMemory.Background = Brushes.Transparent;
            }
            else { 
                TxtMemory.Background = rood;
                geheugen = false;
            }
            if (ValidatieHelper.VType(TxtProducttypeLaptops.Text) && TxtProducttypeLaptops.Text != "")
            {
                
                typ = true;
                TxtProducttypeLaptops.Background = Brushes.Transparent;
            }
            else
            {
                typ = false;
                TxtProducttypeLaptops.Background = rood;
            }





            krtng = false;
            prijzen = false;

            if (TxtPrijsLaptops.Text != "" && float.TryParse(TxtPrijsLaptops.Text, out pr) && pr >= 0)
            {
                TxtPrijsLaptops.Background = Brushes.Transparent;
                prs = true;
                if (TxtKortingLaptops.Text != "" && float.TryParse(TxtKortingLaptops.Text, out kr) && kr >= 0)
                {
                    krtng = true;
                    TxtKortingLaptops.Background = Brushes.Transparent;
                    if (pr > kr)
                    {
                        prijzen = true;
                       
                        TxtPrijsLaptops.Background = Brushes.White;
                        TxtKortingLaptops.Background = Brushes.White;
                    }
                    else
                    {
                        prs = false;
                        krtng = false;
                        TxtPrijsLaptops.Background = rood;
                        TxtKortingLaptops.Background = rood;
                    }
                }
                else
                {
                    krtng = false;
                    TxtKortingLaptops.Background = rood;
                }
            }
            else
            {
                TxtPrijsLaptops.Background = rood;
                prs = false;
            }

            if (TxtKleineBeschrijvingLaptops.Text != "")
            {
                kleinebeschr = true;
                
                TxtKleineBeschrijvingLaptops.Background = Brushes.Transparent;
            }
            else
            {
                kleinebeschr = false;
                TxtKleineBeschrijvingLaptops.Background = rood;
            }
            if( TxtMemory.Text != "")
            {
                geheugen = true;
                TxtMemory.Background = Brushes.Transparent;
            }
            else
            {
                geheugen = false;
                TxtMemory.Background = rood;
            }
            int vrd;
            if (TxtVoorraadLaptops.Text != "" && int.TryParse(TxtVoorraadLaptops.Text, out vrd) && vrd > -1)
            {
                TxtVoorraadLaptops.Background = Brushes.Transparent;
                
                vrrd = true;
            }
            else
            {
                TxtVoorraadLaptops.Background = rood;
                vrrd = false;
            }
            if (TxtKleurLaptops.Text != "")
            {
                TxtKleurLaptops.Background = Brushes.Transparent;
                kleurke = true;
               
            }
            else
            {
                TxtKleurLaptops.Background = rood;
                kleurke = false;
            }


            if (TxtImagesLaptops.Text != "")
            {
               
                imgs = true;
                TxtImagesLaptops.Background = Brushes.Transparent;

            }
            else
            {
                imgs = false;
                TxtImagesLaptops.Background = rood;
            }
            if (TxtOS.Text != "")
            {
                os = true;
                TxtOS.Background = Brushes.Transparent;
               
            }
            else
            {
                os = false;
                TxtOS.Background = rood;
            }
            if (TxtMemory.Text != "")
            {
                TxtMemory.Background = Brushes.Transparent;
                geheugen = true;
               
            }
            else
            {
                TxtMemory.Background = rood;
                geheugen = false;
            }
            if (TxtProcessor.Text != "")
            {
                TxtProcessor.Background = Brushes.Transparent;
                processor = true;
                
            }
            else
            {
                TxtProcessor.Background = rood;
                processor = false;
            }
            if (TxtGraphics.Text != "")
            {
                TxtGraphics.Background = Brushes.Transparent;
                videokaart = true;
                
            }
            else
            {
                TxtGraphics.Background = rood;
                videokaart = false;
            }
            if (TxtStorage.Text != "")
            {
                TxtStorage.Background = Brushes.Transparent;
                opslagruimte = true;
               
            }
            else
            {
                TxtStorage.Background = rood;
                opslagruimte = false;
            }
            if (TxtDisplay.Text != "")
            {
                TxtDisplay.Background = Brushes.Transparent;
                scherm = true;
                
            }
            else
            {
                TxtDisplay.Background = rood;
                scherm = false;
            }
            if (TxtKeyboard.Text != "")
            {
                TxtKeyboard.Background = Brushes.Transparent;
                toetsenbord = true;
                
            }
            else
            {
                TxtKeyboard.Background = rood;
                toetsenbord = false;
            }
            if (TxtConnectivity.Text != "")
            {
                TxtConnectivity.Background = Brushes.Transparent;
                connectiviteit = true;
            }
            else
            {
                TxtConnectivity.Background = rood;
                connectiviteit = false;
            }
            if (TxtBattery_and_adaptor.Text != "")
            {
                TxtBattery_and_adaptor.Background = Brushes.Transparent;
                accu = true;
                
            }
            else
            {
                TxtBattery_and_adaptor.Background = rood;
                accu = false;
            }
            if (TxtTouchpad.Text != "")
            {
                TxtTouchpad.Background = Brushes.Transparent;
                touchpad = true;
                
            }
            else
            {
                TxtTouchpad.Background = rood;
                touchpad = false;
            }
            if (TxtInput_and_output.Text != "")
            {
                TxtInput_and_output.Background = Brushes.Transparent;
                poorten = true;
               
            }
            else
            {
                TxtInput_and_output.Background = rood;
                poorten = false;
            }
            if (TxtAudio.Text != "")
            {
                TxtAudio.Background = Brushes.Transparent;
                audio = true;
                
            }
            else
            {
                TxtAudio.Background = rood;
                audio = false;
            }
            if (TxtAdditional_features.Text != "")
            {
                TxtAdditional_features.Background = Brushes.Transparent;
                bijkomende = true;
                
            }
            else
            {
                TxtAdditional_features.Background = rood;
                bijkomende = false;
            }
            if (TxtFinish.Text != "")
            {
                TxtFinish.Background = Brushes.Transparent;
                finish = true;
                
            }
            else
            {
                TxtFinish.Background = rood;
                finish = false;
            }
            if (TxtDimensions.Text != "")
            {
                TxtDimensions.Background = Brushes.Transparent;
                afmetingen = true;
                
            }
            else
            {
                TxtDimensions.Background = rood;
                afmetingen = false;
            }
            if (TxtWeightlp.Text != "")
            {
                TxtWeightlp.Background = Brushes.Transparent;
                gewicht = true;
               
            }
            else
            {
                TxtWeightlp.Background = rood;
                gewicht = false;
            }

            if (typ == true && processor == true && videokaart == true && os == true && opslagruimte == true && prs == true && krtng == true && geheugen == true && kleinebeschr == true &&
                kleurke == true && vrrd == true && imgs == true && scherm == true && toetsenbord == true && connectiviteit == true && accu == true && prijzen == true 
                && poorten == true && touchpad == true && audio == true && bijkomende == true && finish == true && afmetingen == true && gewicht == true)
            {
                valideren = true;
            }
            return valideren;
        }

        private void BtnTerug4_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnBlader4_Click(object sender, RoutedEventArgs e)
        {
            // We hebben de admin nodig omdat zijn pad uniek is aan zijn computer

            if (TxtImagesLaptops.Text == "")
            {
                TxtImagesLaptops.Text = $"{FileHelper.ZoekEenAfbeelding(admin)}\n";
                UpdateAfbeeldingenlaptop(TxtImagesLaptops.Text, admin);
            }
            else
            {
                TxtImagesLaptops.Text += $"|{FileHelper.ZoekEenAfbeelding(admin)}\n";
                UpdateAfbeeldingenlaptop(TxtImagesLaptops.Text, admin);
            }
        }
        #region Afbeeldingen Laptop
        string[] pathArrayL;
        private void UpdateAfbeeldingenlaptop(string inhoud, Admin admin)
        {

            ImageHelper.Path = admin.Afbeeldingen_wpf;
            pathArrayL = ImageHelper.GetVolledigPad(inhoud, admin);



            if (pathArrayL.Length == 0)
            {
                ImgVoorbeeld2.Source = null;
            }
            else if (pathArrayL.Length == 1)
            {
                if (File.Exists(pathArrayL[0]))
                    ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayL[0], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld2.Source = null;

            }
            else
            {
                if (File.Exists(pathArrayL[1]))
                    ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayL[1], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld2.Source = null;
            }

            // we bepalen hoeveel strings we in onze array hebben, om te weten hoeveel images we moeten opvullen (0 - 7)
            switch (pathArrayL.Length)
            {
                case 0:
                    MessageBox.Show("Geen rij geselecteerd.");
                    break;
                case 1:
                    Show_1_Afbeeldinglaptop(pathArrayL);
                    break;
                case 2:
                    Show_2_Afbeeldingenlaptop(pathArrayL);
                    break;
                case 3:
                    Show_3_Afbeeldingenlaptop(pathArrayL);
                    break;
                case 4:
                    Show_4_Afbeeldingenlaptop(pathArrayL);
                    break;
                case 5:
                    Show_5_Afbeeldingenlaptop(pathArrayL);
                    break;
                case 6:
                    Show_6_Afbeeldingenlaptop(pathArrayL);
                    break;
                case 7:
                    Show_7_Afbeeldingenlaptop(pathArrayL);
                    break;
                default:
                    Show_7_Afbeeldingenlaptop(pathArrayL);
                    break;
            }

        }
        #region BUTTONCLICKEVENTS VAN IMAGES
        private void Show_1_Afbeeldinglaptop(string[] pathArrayL)
        {
            if (File.Exists(pathArrayL[0]))
                Img8.Content = new BitmapImage(new Uri(pathArrayL[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            Img9.Content = null;
            Img10.Content = null;
            Img11.Content = null;
            Img12.Content = null;
            Img13.Content = null;
            Img14.Content = null;

        }
     
        private void Show_2_Afbeeldingenlaptop(string[] pathArrayL)
        {
            if (File.Exists(pathArrayL[0]))
                Img8.Content = new BitmapImage(new Uri(pathArrayL[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArrayL[1]))
                Img9.Content = new BitmapImage(new Uri(pathArrayL[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            Img10.Content = null;
            Img11.Content = null;
            Img12.Content = null;
            Img13.Content = null;
            Img14.Content = null;

        }
        private void Show_3_Afbeeldingenlaptop(string[] pathArrayL)
        {
            if (File.Exists(pathArrayL[0]))
                Img8.Content = new BitmapImage(new Uri(pathArrayL[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArrayL[1]))
                Img9.Content = new BitmapImage(new Uri(pathArrayL[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            if (File.Exists(pathArrayL[2]))
                Img10.Content = new BitmapImage(new Uri(pathArrayL[2], UriKind.RelativeOrAbsolute));
            else
                Img10.Content = null;


            Img11.Content = null;
            Img12.Content = null;
            Img13.Content = null;
            Img14.Content = null;
        }
        private void Show_4_Afbeeldingenlaptop(string[] pathArrayL)
        {
            if (File.Exists(pathArrayL[0]))
                Img8.Content = new BitmapImage(new Uri(pathArrayL[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArrayL[1]))
                Img9.Content = new BitmapImage(new Uri(pathArrayL[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            if (File.Exists(pathArrayL[2]))
                Img10.Content = new BitmapImage(new Uri(pathArrayL[2], UriKind.RelativeOrAbsolute));
            else
                Img10.Content = null;

            if (File.Exists(pathArrayL[3]))
                Img11.Content = new BitmapImage(new Uri(pathArrayL[3], UriKind.RelativeOrAbsolute));
            else
                Img11.Content = null;


            Img12.Content = null;
            Img13.Content = null;
            Img14.Content = null;
        }
        private void Show_5_Afbeeldingenlaptop(string[] pathArrayL)
        {
            if (File.Exists(pathArrayL[0]))
                Img8.Content = new BitmapImage(new Uri(pathArrayL[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArrayL[1]))
                Img9.Content = new BitmapImage(new Uri(pathArrayL[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            if (File.Exists(pathArrayL[2]))
                Img10.Content = new BitmapImage(new Uri(pathArrayL[2], UriKind.RelativeOrAbsolute));
            else
                Img10.Content = null;

            if (File.Exists(pathArrayL[3]))
                Img11.Content = new BitmapImage(new Uri(pathArrayL[3], UriKind.RelativeOrAbsolute));
            else
                Img11.Content = null;

            if (File.Exists(pathArrayL[4]))
                Img12.Content = new BitmapImage(new Uri(pathArrayL[4], UriKind.RelativeOrAbsolute));
            else
                Img12.Content = null;

            Img13.Content = null;
            Img14.Content = null;
        }
        private void Show_6_Afbeeldingenlaptop(string[] pathArrayL)
        {
            if (File.Exists(pathArrayL[0]))
                Img8.Content = new BitmapImage(new Uri(pathArrayL[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArrayL[1]))
                Img9.Content = new BitmapImage(new Uri(pathArrayL[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            if (File.Exists(pathArrayL[2]))
                Img10.Content = new BitmapImage(new Uri(pathArrayL[2], UriKind.RelativeOrAbsolute));
            else
                Img10.Content = null;

            if (File.Exists(pathArrayL[3]))
                Img11.Content = new BitmapImage(new Uri(pathArrayL[3], UriKind.RelativeOrAbsolute));
            else
                Img11.Content = null;

            if (File.Exists(pathArrayL[4]))

                Img12.Content = new BitmapImage(new Uri(pathArrayL[4], UriKind.RelativeOrAbsolute));
            else
                Img12.Content = null;

            if (File.Exists(pathArrayL[5]))
                Img13.Content = new BitmapImage(new Uri(pathArrayL[5], UriKind.RelativeOrAbsolute));
            else
                Img13.Content = null;

            Img14.Content = null;
        }
        private void Show_7_Afbeeldingenlaptop(string[] pathArrayL)
        {
            if (File.Exists(pathArrayL[0]))
                Img8.Content = new BitmapImage(new Uri(pathArrayL[0], UriKind.RelativeOrAbsolute));
            else
                Img8.Content = null;

            if (File.Exists(pathArrayL[1]))
                Img9.Content = new BitmapImage(new Uri(pathArrayL[1], UriKind.RelativeOrAbsolute));
            else
                Img9.Content = null;

            if (File.Exists(pathArrayL[2]))
                Img10.Content = new BitmapImage(new Uri(pathArrayL[2], UriKind.RelativeOrAbsolute));
            else
                Img10.Content = null;

            if (File.Exists(pathArrayL[3]))
                Img11.Content = new BitmapImage(new Uri(pathArrayL[3], UriKind.RelativeOrAbsolute));
            else
                Img11.Content = null;

            if (File.Exists(pathArrayL[4]))

                Img12.Content = new BitmapImage(new Uri(pathArrayL[4], UriKind.RelativeOrAbsolute));
            else
                Img12.Content = null;

            if (File.Exists(pathArrayL[5]))
                Img13.Content = new BitmapImage(new Uri(pathArrayL[5], UriKind.RelativeOrAbsolute));
            else
                Img13.Content = null;

            if (File.Exists(pathArrayL[6]))
                Img14.Content = new BitmapImage(new Uri(pathArrayL[6], UriKind.RelativeOrAbsolute));

            else
                Img14.Content = null;
        }

        private void Img8_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayL[0], UriKind.RelativeOrAbsolute));
        }

        private void Img9_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayL[1], UriKind.RelativeOrAbsolute));
        }

        private void Img10_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayL[2], UriKind.RelativeOrAbsolute));
        }

        private void Img11_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayL[3], UriKind.RelativeOrAbsolute));
        }

        private void Img12_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayL[4], UriKind.RelativeOrAbsolute));
        }

        private void Img13_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayL[5], UriKind.RelativeOrAbsolute));
        }

        private void Img14_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld2.Source = new BitmapImage(new Uri(pathArrayL[6], UriKind.RelativeOrAbsolute));
        }


        #endregion

        #endregion
        #endregion
        #region Muizen

        private void BtnMuizenToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTekstvakkenMuizen())
            {
                //table Productdetails invullen met de gegegevens in de textboxen
                Productdetails product = new Productdetails();
                product.Type = TxtProducttypeMuizen.Text;
                product.Prijs = float.Parse(TxtPrijsMuizen.Text);
                product.Korting = float.Parse(TxtKortingMuizen.Text);
                product.Beschrijving = TxtKleineBeschrijvingMuizen.Text;
                product.Uitgebreide_beschrijving = Txtuitgebr_beschrijvingMuizen.Text;
                product.Kleur = TxtKleurMuizen.Text;
                product.Voorraad = int.Parse(TxtVoorraadMuizen.Text);
                product.Images = TxtImagesMuizen.Text;
                product.Categorie = "muis";
                var result = product.Insert();

                //table muizen invullen met de gegevens in de textboxen
                Muis muis = new Muis();
                muis.ProductNummer = result.NewId;
                muis.Dpi = int.Parse(TxtDPI.Text);
                muis.Instelbaarh_dpi = TxtInstelbaarheidDpi.Text;
                muis.AantalKnoppen = int.Parse(TxtAantalKnoppen.Text);
                muis.Verbinding = TxtVerbindingMuis.Text;
                muis.Verlichting = TxtVerlichtingMuis.Text;
                muis.Insert();

                //methodes oproepen voor het leeg maken van de textboxen en de gebruiker laten weten dat die een product heeft toegevoegd
                UpdateMuizenForm();
                ProductToegevoegd(result.NewId, "muis");
            }
        }
        private void UpdateMuizenForm()
        {
            TxtProducttypeMuizen.Clear();
            TxtPrijsMuizen.Clear();
            TxtKortingMuizen.Clear();
            TxtKleineBeschrijvingMuizen.Clear();
            TxtKleurMuizen.Clear();
            TxtVoorraadMuizen.Clear();
            TxtImagesMuizen.Clear();

            TxtDPI.Clear();
            TxtAantalKnoppen.Clear();
            TxtVerbindingMuis.Clear();
            TxtVerlichtingMuis.Clear();
        }

        private void BtnBladerM_Click(object sender, RoutedEventArgs e)
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
        private void BtnTerug2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnRefreshM_Click(object sender, RoutedEventArgs e)
        {
            UpdateAfbeeldingenMuis(TxtImagesMuizen.Text, admin);
            ValidateTekstvakkenMuizen();
        }
        private bool ValidateTekstvakkenMuizen()
        {
            bool valideren = false;

            float pr = 0;
            float kr = 0;

            bool typ, prs, krtng, kleinebeschr, grotebescht, kleurke, prijzen, vrrd, imgs, dpi, instelbaarheidDpi, aantalKnoppen, verbinding, verlichting;

            if (ValidatieHelper.VType(TxtProducttypeMuizen.Text) && TxtProducttypeMuizen.Text != "")
            {

                typ = true;
                TxtProducttypeMuizen.Background = Brushes.Transparent;
            }
            else
            {
                typ = false;
                TxtProducttypeMuizen.Background = rood;
            }


            krtng = false;
            prijzen = false;

            if (TxtPrijsMuizen.Text != "" && float.TryParse(TxtPrijsMuizen.Text, out pr) && pr >= 0)
            {
                TxtPrijsMuizen.Background = Brushes.Transparent;
                prs = true;
                if (TxtKortingMuizen.Text != "" && float.TryParse(TxtKortingMuizen.Text, out kr) && kr >= 0)
                {
                    krtng = true;
                    TxtKortingMuizen.Background = Brushes.Transparent;
                    if (pr > kr)
                    {
                        prijzen = true;
                        TxtPrijsMuizen.Background = Brushes.White;
                        TxtKortingMuizen.Background = Brushes.White;
                    }
                    else
                    {
                        prs = false;
                        krtng = false;
                        TxtPrijsMuizen.Background = rood;
                        TxtKortingMuizen.Background = rood;
                    }
                }
                else
                {
                    krtng = false;
                    TxtKortingMuizen.Background = rood;
                }
            }
            else
            {
                TxtPrijsMuizen.Background = rood;
                prs = false;
            }

            if (TxtKleineBeschrijvingMuizen.Text != "")
            {
                kleinebeschr = true;

                TxtKleineBeschrijvingMuizen.Background = Brushes.Transparent;
            }
            else
            {
                kleinebeschr = false;
                TxtKleineBeschrijvingMuizen.Background = rood;
            }

            if (Txtuitgebr_beschrijvingMuizen.Text != "")
            {
                grotebescht = true;
                Txtuitgebr_beschrijvingMuizen.Background = Brushes.Transparent;
            }
            else
            {
                grotebescht = false;
                Txtuitgebr_beschrijvingMuizen.Background = rood;
            }

            int vrd;
            if (TxtVoorraadMuizen.Text != "" && int.TryParse(TxtVoorraadMuizen.Text, out vrd) && vrd > -1)
            {
                TxtVoorraadMuizen.Background = Brushes.Transparent;

                vrrd = true;
            }
            else
            {
                TxtVoorraadMuizen.Background = rood;
                vrrd = false;
            }
            if (TxtKleurMuizen.Text != "")
            {
                TxtKleurMuizen.Background = Brushes.Transparent;
                kleurke = true;
            }
            else
            {
                TxtKleurMuizen.Background = rood;
                kleurke = false;
            }


            if (TxtImagesMuizen.Text != "")
            {

                imgs = true;
                TxtImagesMuizen.Background = Brushes.Transparent;

            }
            else
            {
                imgs = false;
                TxtImagesMuizen.Background = rood;
            }

            if (TxtDPI.Text != "")
            {
                dpi = true;
                TxtDPI.Background = Brushes.Transparent;
            }
            else
            {
                dpi = false;
                TxtDPI.Background = rood;
            }

            if (TxtInstelbaarheidDpi.Text == "J" || TxtInstelbaarheidDpi.Text == "N")
            {
                instelbaarheidDpi = true;
                TxtInstelbaarheidDpi.Background = Brushes.Transparent;
            }
            else
            {
                instelbaarheidDpi = false;
                TxtInstelbaarheidDpi.Background = rood;
            }
            
            if (TxtAantalKnoppen.Text != "")
            {
                aantalKnoppen = true;
                TxtAantalKnoppen.Background = Brushes.Transparent;
            }
            else
            {
                aantalKnoppen = false;
                TxtAantalKnoppen.Background = rood;
            }

            if (TxtVerbindingMuis.Text != "")
            {
                verbinding = true;
                TxtVerbindingMuis.Background = Brushes.Transparent;
            }
            else
            {
                verbinding = false;
                TxtVerbindingMuis.Background = rood;
            }

            if (TxtVerlichtingMuis.Text == "J" || TxtVerlichtingMuis.Text == "N")
            {
                verlichting = true;
                TxtVerlichtingMuis.Background = Brushes.Transparent;
            }
            else
            {
                verlichting = false;
                TxtVerlichtingMuis.Background = rood;
            }

            if (typ == true && prs == true && krtng == true && kleinebeschr == true && grotebescht == true && kleurke == true && prijzen == true && 
                vrrd == true && imgs == true && dpi == true && instelbaarheidDpi == true && aantalKnoppen == true && verbinding == true && verlichting == true)
            {
                valideren = true;
            }

            return valideren;
        }
        string[] pathArrayMuis;
        private void UpdateAfbeeldingenMuis(string inhoud, Admin admin)
        {

            ImageHelper.Path = admin.Afbeeldingen_wpf;
            pathArrayMuis = ImageHelper.GetVolledigPad(inhoud, admin);



            if (pathArrayMuis.Length == 0)
            {
                ImgVoorbeeld3.Source = null;
            }
            else if (pathArrayMuis.Length == 1)
            {
                if (File.Exists(pathArrayMuis[0]))
                    ImgVoorbeeld3.Source = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld3.Source = null;

            }
            else
            {
                if (File.Exists(pathArrayMuis[1]))
                    ImgVoorbeeld3.Source = new BitmapImage(new Uri(pathArrayMuis[1], UriKind.RelativeOrAbsolute));
                else
                    ImgVoorbeeld3.Source = null;
            }

            // we bepalen hoeveel strings we in onze array hebben, om te weten hoeveel images we moeten opvullen (0 - 7)
            switch (pathArrayMuis.Length)
            {
                case 0:
                    MessageBox.Show("Geen rij geselecteerd.");
                    break;
                case 1:
                    Show_1_AfbeeldingMuis(pathArrayMuis);
                    break;
                case 2:
                    Show_2_AfbeeldingenMuis(pathArrayMuis);
                    break;
                case 3:
                    Show_3_AfbeeldingenMuis(pathArrayMuis);
                    break;
                case 4:
                    Show_4_AfbeeldingenMuis(pathArrayMuis);
                    break;
                case 5:
                    Show_5_AfbeeldingenMuis(pathArrayMuis);
                    break;
                case 6:
                    Show_6_AfbeeldingenMuis(pathArrayMuis);
                    break;
                case 7:
                    Show_7_AfbeeldingenMuis(pathArrayMuis);
                    break;
                default:
                    Show_7_AfbeeldingenMuis(pathArrayMuis);
                    break;
            }

        }
        #region BUTTONCLICKEVENTS VAN IMAGES
        private void Show_1_AfbeeldingMuis(string[] pathArrayMuis)
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
        private void Show_2_AfbeeldingenMuis(string[] pathArrayMuis)
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
        private void Show_3_AfbeeldingenMuis(string[] pathArrayMuis)
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
        private void Show_4_AfbeeldingenMuis(string[] pathArrayMuis)
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
        private void Show_5_AfbeeldingenMuis(string[] pathArrayMuis)
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
        private void Show_6_AfbeeldingenMuis(string[] pathArrayMuis)
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
        private void Show_7_AfbeeldingenMuis(string[] pathArrayMuis)
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
            ImgVoorbeeld3.Source = new BitmapImage(new Uri(pathArrayMuis[0], UriKind.RelativeOrAbsolute));
        }

        private void Img16_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld3.Source = new BitmapImage(new Uri(pathArrayMuis[1], UriKind.RelativeOrAbsolute));
        }

        private void Img17_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld3.Source = new BitmapImage(new Uri(pathArrayMuis[2], UriKind.RelativeOrAbsolute));
        }


        private void Img18_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld3.Source = new BitmapImage(new Uri(pathArrayMuis[3], UriKind.RelativeOrAbsolute));

        }

        private void Img19_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld3.Source = new BitmapImage(new Uri(pathArrayMuis[4], UriKind.RelativeOrAbsolute));
        }


        private void Img20_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld3.Source = new BitmapImage(new Uri(pathArrayMuis[5], UriKind.RelativeOrAbsolute));

        }

        private void Img21_Click(object sender, RoutedEventArgs e)
        {
            ImgVoorbeeld3.Source = new BitmapImage(new Uri(pathArrayMuis[6], UriKind.RelativeOrAbsolute));
        }


        private void BtnRefreshMuis_Click(object sender, RoutedEventArgs e)
        {
            UpdateAfbeeldingenMuis(TxtImagesMuizen.Text, admin);
        }
        #endregion
        #endregion
        #region Toetsenborden

        private void BtnTerug3_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void UpdateToetsenbordenForm()
        {
            TxtProducttypeToetsenbord.Clear();
            TxtPrijsToetsenbord.Clear();
            TxtKortingToetsenbord.Clear();
            TxtKleineBeschrijvingToetsenbord.Clear();
            Txtuitgebr_beschrijvingToetsenbord.Clear();
            TxtKleurToetsenbord.Clear();
            TxtVoorraadToetsenbord.Clear();
            TxtImagesToetsenbord.Clear();

            TxtModel.Clear();
            TxtNumeriekKlavier.Clear();
            TxtVerbindingToetsenbord.Clear();
            TxtVerlichtingToetsenbord.Clear();
        }
        private void BtnToetsenbordenToevoegen_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateTekstvakkenToetsenbord())
            {
                //table Productdetails invullen met de gegegevens in de textboxen
                Productdetails product = new Productdetails();
                product.Type = TxtProducttypeToetsenbord.Text;
                product.Prijs = Convert.ToDouble(TxtPrijsToetsenbord.Text);
                product.Korting = Convert.ToDouble(TxtKortingToetsenbord.Text);
                product.Beschrijving = TxtKleineBeschrijvingToetsenbord.Text;
                product.Uitgebreide_beschrijving = Txtuitgebr_beschrijvingToetsenbord.Text;
                product.Kleur = TxtKleurToetsenbord.Text;
                product.Voorraad = Convert.ToInt32(TxtVoorraadToetsenbord.Text);
                product.Images = TxtImagesToetsenbord.Text;
                product.Categorie = "toetsenbord";
                var result = product.Insert();

                //table toetsenborden invullen met de gegevens in de textboxen
                Toetsenbord toetsenbord = new Toetsenbord();
                toetsenbord.ProductNummer = result.NewId;
                toetsenbord.ModelAzerty = TxtModel.Text;
                toetsenbord.NumeriekKlavier = TxtNumeriekKlavier.Text;
                toetsenbord.Verbinding = TxtVerbindingToetsenbord.Text;
                toetsenbord.Verlichting = TxtVerlichtingToetsenbord.Text;
                toetsenbord.Insert();

                //methodes oproepen voor het leeg maken van de textboxen en de gebruiker laten weten dat die een product heeft toegevoegd
                UpdateToetsenbordenForm();
                ProductToegevoegd(result.NewId, "toetsenbord");
            }
        }

        private bool ValidateTekstvakkenToetsenbord()
        {
            bool valideren = false;

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
                valideren = true;
            }

            return valideren;
        }
        private void BtnBladerT_Click(object sender, RoutedEventArgs e)
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


        #endregion

        #endregion


    }
}
