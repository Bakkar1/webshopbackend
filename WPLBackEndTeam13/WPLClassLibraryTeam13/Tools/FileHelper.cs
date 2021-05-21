using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using System.Windows.Forms;
using WPLClassLibraryTeam13.Entity;
using WPLClassLibraryTeam13.Query;
using WPLClassLibraryTeam13.Data;
using System.Data;

namespace WPLClassLibraryTeam13.Tools
{
    public static class FileHelper
    {
        public enum CsvBestand
        {
            ProductNummer = 0,

            Producttype = 1,
            Prijs = 2,
            Korting = 3,
            KleineBeschrijving = 4,
            UitgebrBeschrijving = 5,
            Kleur = 6,
            Voorraad = 7,
            Images = 8,
            DiameterDrivers = 10,
            InklapbareMicro = 11,
            VerbindingH = 12,
            VerlichtingH = 13,
            OS = 15,
            Processor = 16,
            Memory = 17,
            Graphics = 18,
            Storage = 19,
            Display = 20,
            Keyboard = 21,
            Connectivity = 22,
            BatteryAndAdaptor = 23,
            Touchpad = 24,
            InputAndOutput = 25,
            Audio = 26,
            AdditionalFeatures = 27,
            Finish = 28,
            Dimensions = 29,
            Weight = 30,
            Dpi = 32,
            InstelbaarDpi = 33,
            AantalKnoppen = 34,
            VerbindingM = 35,
            VerlichtingM = 36,
            ToetsenbordId = 37,
            ModelAzerty = 38,
            NumeriekKlavier = 39,
            VerbindingT = 40,
            VerlichtingT = 41,
            Categorie = 42
        }
        public static FileResult OpenFile(ref int aantalRijen)
        {
            FileResult result = new FileResult();
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.InitialDirectory = @"C:\Oefeningen";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                int productId;
                const string headset = "headset";
                const string laptop = "laptop";
                const string muis = "muis";
                const string toetsenbord = "toetsenbord";

                string[] fileLines = File.ReadAllLines(ofd.FileName);
                string categorie;
                string type;
                aantalRijen = 0;

                ValidatieCSVfile(fileLines);

                //De eerste twee rijen hebben we niet nodig
                for (int i = 2; i < fileLines.Length; i++)
                {
                    string[] data = fileLines[i].Split(';');

                    //Enkel als de rij 43 elementen of meer bevat, voegt hij de rij toe. Dit is voor het vermijden van crashes door teweinig velden.
                    if (data.Length >= 43)
                    {
                        //Nakijken of het type wel uniek is. Als dat niet is voegt hij ze niet toe aan de databank.
                        categorie = data[(int)CsvBestand.Categorie];
                        type = data[(int)CsvBestand.Producttype];
                        QueryProductDetails qpd = new QueryProductDetails();
                        SQLCommandResult result2 = qpd.GetData(type);
                        if (result2.DataTable.Rows.Count == 0)
                        {
                            productId = ConvertToProductDetails(data, categorie);
                            if (categorie == headset)
                                ConvertToHeadsetDetails(data, productId);
                            if (categorie == laptop)
                                ConvertToLaptopDetails(data, productId);
                            if (categorie == muis)
                                ConvertToMuisDetails(data, productId);
                            if (categorie == toetsenbord)
                                ConvertToToetsenbordDetails(data, productId);
                            aantalRijen++;
                        }
                    }

                }

            }
            return result;
        }
        private static void ValidatieCSVfile(string[] fileLines)
        {
            int rijFoutief = 0;
            List<int> lstTeveelcellen = new List<int>();
            List<int> lstTeweinigcellen = new List<int>();

            foreach (var line in fileLines)
            {
                rijFoutief++;
                string[] data1 = line.Split(';');
                if (data1.Length > 43) { lstTeveelcellen.Add(rijFoutief); }
                if (data1.Length < 43) { lstTeweinigcellen.Add(rijFoutief); }
            }

            string teveelcellenarray = string.Join(", ", lstTeveelcellen.ToArray());
            string teweinigcellenarray = string.Join(", ", lstTeweinigcellen.ToArray());
            //if (lstTeveelcellen.Count != 0) { MessageBox.Show($"De regels {teveelcellenarray} bevatten teveel cellen, deze rijen kunnen niet worden toegevoegd."); }
            if (lstTeweinigcellen.Count != 0) { MessageBox.Show($"De regels {teweinigcellenarray} bevatten teweinig cellen. deze rijen kunnen niet worden toegevoegd."); }
        }
        private static int ConvertToProductDetails(string[] data, string categorie)
        {
            Productdetails p = new Productdetails();

            p.Type = data[(int)CsvBestand.Producttype];
            double prijs = -1;
            double.TryParse((data[(int)CsvBestand.Prijs]), out prijs);
            p.Prijs = prijs;

            double korting = -1;
            double.TryParse((data[(int)CsvBestand.Korting]), out korting);
            p.Korting = korting;

            p.Beschrijving = data[(int)CsvBestand.KleineBeschrijving];
            p.Uitgebreide_beschrijving = data[(int)CsvBestand.UitgebrBeschrijving];
            p.Kleur = data[(int)CsvBestand.Kleur];

            int voorraad = -1;
            int.TryParse(data[(int)CsvBestand.Voorraad], out voorraad);
            p.Voorraad = voorraad;
            p.Images = data[(int)CsvBestand.Images];
            p.Categorie = categorie;


            var result = p.Insert();


            return result.NewId;
        }
        private static void ConvertToHeadsetDetails(string[] data, int productId)
        {
            Headset h = new Headset();
            h.ProductNummer = productId;
            h.DiameterDrivers = data[(int)CsvBestand.DiameterDrivers];
            h.InklapbareMicro = data[(int)CsvBestand.InklapbareMicro];
            h.Verbinding = data[(int)CsvBestand.VerbindingH];
            h.Verlichting = data[(int)CsvBestand.VerlichtingH];
            h.Insert();

        }
        private static void ConvertToLaptopDetails(string[] data, int productId)
        {
            Laptop l = new Laptop();
            l.ProductNummer = productId;
            l.Os = data[(int)CsvBestand.OS];
            l.Processor = data[(int)CsvBestand.Processor];
            l.Memory = data[(int)CsvBestand.Memory];
            l.Graphics = data[(int)CsvBestand.Graphics];
            l.Storage = data[(int)CsvBestand.Storage];
            l.Display = data[(int)CsvBestand.Display];
            l.Keyboard = data[(int)CsvBestand.Keyboard];
            l.Connectivity = data[(int)CsvBestand.Connectivity];
            l.Battery_and_adaptor = data[(int)CsvBestand.BatteryAndAdaptor];
            l.Touchpad = data[(int)CsvBestand.Touchpad];
            l.Input_and_output = data[(int)CsvBestand.InputAndOutput];
            l.Audio = data[(int)CsvBestand.Audio];
            l.Additional_features = data[(int)CsvBestand.AdditionalFeatures];
            l.Finish = data[(int)CsvBestand.Finish];
            l.Dimensions = data[(int)CsvBestand.Dimensions];
            l.Weight = data[(int)CsvBestand.Weight];
            l.Insert();
        }
        private static void ConvertToMuisDetails(string[] data, int productId)
        {
            Muis m = new Muis();
            m.ProductNummer = productId;
            int dpi = -1;
            int.TryParse(data[(int)CsvBestand.Dpi], out dpi);
            m.Dpi = dpi;
            m.Instelbaarh_dpi = data[(int)CsvBestand.InstelbaarDpi];
            int aantalknoppen = 0;
            int.TryParse(data[(int)CsvBestand.AantalKnoppen], out aantalknoppen);
            m.AantalKnoppen = aantalknoppen;
            m.Verbinding = data[(int)CsvBestand.VerbindingM];
            m.Verlichting = data[(int)CsvBestand.VerlichtingM];
            m.Insert();
        }
        private static void ConvertToToetsenbordDetails(string[] data, int productId)
        {
            Toetsenbord t = new Toetsenbord();
            t.ProductNummer = productId;
            t.ModelAzerty = data[(int)CsvBestand.ModelAzerty];
            t.NumeriekKlavier = data[(int)CsvBestand.NumeriekKlavier];
            t.Verbinding = data[(int)CsvBestand.VerbindingT];
            t.Verlichting = data[(int)CsvBestand.VerlichtingT];
            t.Insert();
        }

        public static string ConvertDataForFile()
        {

            DataTable dt = new DataTable();
            QueryProductDetails qpd = new QueryProductDetails();
            dt = qpd.SelectAllRecords();

            StringBuilder sb = new StringBuilder();
            var rows = dt.AsEnumerable().Select(r => string.Format("{0}", string.Join(";", r.ItemArray)));
            foreach (var row in rows)
            {
                sb.AppendLine(row);
            }
            string output = sb.ToString();
            return output;

            // var output = string.Format("[{0}]", string.Join(";", rows.ToArray()));
            //MessageBox.Show(output);

        }
        public static void VerwerkBestand(string bestandsnaam, string output)
        {
            StreamWriter sw;
            try
            {
                sw = new StreamWriter(bestandsnaam);
                sw.Write(output);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Bestand is open, sluit het bestand.", ex);
            }
        }
        public static string ZoekEenAfbeelding(Admin admin)
        {
            string pad;
            string eerstedeel = admin.Afbeeldingen_wpf;
            string laatstedeel;
            //int aantalKaraketers = eerstedeel.Length;
            pad = Volledigpadzoeken(admin);
            laatstedeel = pad.Replace($"{eerstedeel}", "");
            //folder = Path.GetDirectoryName(pad);
            //DoetErMaarDeHelftVanaf(ref pad);
            return laatstedeel;
        }
        private static string Volledigpadzoeken(Admin admin)
        {
            string volledigPad = "";
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = admin.Afbeeldingen_wpf;
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                volledigPad = ofd.FileName;
            }
            return volledigPad;
        }
        private static void DoetErMaarDeHelftVanaf(ref string pad)
        {

        }
    }

 

    public class FileResult
    {
        private List<CsvTestData> objectRows = new List<CsvTestData>();
        public bool Succeeded { get; set; }
        public IEnumerable<CsvTestData> CsvObjectRows => objectRows;
        public void AddObject(CsvTestData csvData)
        {
            objectRows.Add(csvData);
        }
    }

    public class CsvTestData
    {
        public string ChildData { get; set; }
        public string OtherChildData { get; set; }

    }


}








