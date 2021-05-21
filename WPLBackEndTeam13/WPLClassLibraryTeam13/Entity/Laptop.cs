using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WPLClassLibraryTeam13.Data;

namespace WPLClassLibraryTeam13.Entity
{
    public class Laptop : ProjectFramework
    {
        public static class Database
        {
            public static string TableName = "tbllaptopDetails";
            public static string PrimaryKey = Columns.LaptopID;

            public static class Columns
            {
                public static string Productnummer = "productnummer";
                public static string LaptopID = "laptopid";
                public static string Os = "os";
                public static string Processor = "Processor";
                public static string Memory = "Memory";
                public static string Graphics = "Graphics";
                public static string Storage = "Storage";
                public static string Display = "Display";
                public static string Keyboard = "Keyboard";
                public static string Connectivity = "Connectivity";
                public static string Battery_and_adaptor = "Battery_and_adaptor";
                public static string Touchpad = "Touchpad";
                public static string Input_and_output = "Input_and_output";
                public static string Audio = "Audio";
                public static string Additional_features = "Additional_features";
                public static string Finish = "Finish";
                public static string Dimensions = "Dimensions";
                public static string Weight = "Weightlp";
            }
        }

        public Laptop() : base(Database.TableName, Database.PrimaryKey)
        {

        }

        private int productnummer;
        private int laptopId;
        private string os;
        private string processor;
        private string memory;
        private string graphics;
        private string storage;
        private string display;
        private string keyboard;
        private string connectivity;
        private string battery_and_adaptor;
        private string touchpad;
        private string input_and_output;
        private string audio;
        private string additional_features;
        private string finish;
        private string dimensions;
        private string weight;


        public int ProductNummer
        {
            get { return ProductNummer; }
            set
            {
                productnummer = value;
                UpdateSqlParameters(Database.Columns.Productnummer, productnummer);
            }
        }


        public int LaptopId
        {
            get { return LaptopId; }
            set
            {
                laptopId = value;
                UpdateSqlParameters(Database.Columns.LaptopID, laptopId);
            }
        }

        public string Os
        {
            get { return os; }
            set
            {
                os = value;
                UpdateSqlParameters(Database.Columns.Os, os);
            }
        }

        public string Processor
        {
            get { return processor; }
            set
            {
                processor = value;
                UpdateSqlParameters(Database.Columns.Processor, Processor);
            }
        }

        public string Memory
        {
            get { return memory; }
            set
            {
                memory = value;
                UpdateSqlParameters(Database.Columns.Memory, memory);
            }
        }

        public string Graphics
        {
            get { return graphics; }
            set
            {
                graphics = value;
                UpdateSqlParameters(Database.Columns.Graphics, graphics);
            }
        }

        public string Storage
        {
            get { return storage; }
            set
            {
                storage = value;
                UpdateSqlParameters(Database.Columns.Storage, storage);
            }
        }
        public string Display
        {
            get { return display; }
            set
            {
                display = value;
                UpdateSqlParameters(Database.Columns.Display, display);
            }
        }
        public string Keyboard
        {
            get { return keyboard; }
            set
            {
                keyboard = value;
                UpdateSqlParameters(Database.Columns.Keyboard, keyboard);
            }
        }
        public string Connectivity
        {
            get { return connectivity; }
            set
            {
                connectivity = value;
                UpdateSqlParameters(Database.Columns.Connectivity, connectivity);
            }
        }
        public string Battery_and_adaptor
        {
            get { return battery_and_adaptor; }
            set
            {
                battery_and_adaptor = value;
                UpdateSqlParameters(Database.Columns.Battery_and_adaptor, battery_and_adaptor);
            }
        }
        public string Touchpad
        {
            get { return touchpad; }
            set
            {
                touchpad = value;
                UpdateSqlParameters(Database.Columns.Touchpad, touchpad);
            }
        }
        public string Input_and_output
        {
            get { return input_and_output; }
            set
            {
                input_and_output = value;
                UpdateSqlParameters(Database.Columns.Input_and_output, input_and_output);
            }
        }
        public string Audio
        {
            get { return audio; }
            set
            {
                audio = value;
                UpdateSqlParameters(Database.Columns.Audio, audio);
            }
        }
        public string Additional_features
        {
            get { return additional_features; }
            set
            {
                additional_features = value;
                UpdateSqlParameters(Database.Columns.Additional_features, additional_features);
            }
        }
        public string Finish
        {
            get { return finish; }
            set
            {
                finish = value;
                UpdateSqlParameters(Database.Columns.Finish, finish);
            }
        }
        public string Dimensions
        {
            get { return dimensions; }
            set
            {
                dimensions = value;
                UpdateSqlParameters(Database.Columns.Dimensions, dimensions);
            }
        }
        public string Weight
        {
            get { return weight; }
            set
            {
                weight = value;
                UpdateSqlParameters(Database.Columns.Weight, weight);
            }
        }

        public static Laptop GetEntity(int id)
        {
            Laptop laptop = new Laptop();
            var result = laptop.Read(id);
            var dt = result.DataTable;
            if (dt.Rows.Count == 1)
            {
                laptop = MapDataRow(dt.Rows[0]);
            }
            return laptop;
        }

        private static Laptop MapDataRow(DataRow dr)
        {
            Laptop laptop = new Laptop();
            laptop.ProductNummer = Convert.ToInt32(dr[Laptop.Database.Columns.Productnummer]);
            laptop.LaptopId = Convert.ToInt32(dr[Laptop.Database.Columns.LaptopID]);
            laptop.Os = dr[Laptop.Database.Columns.Os].ToString();
            laptop.Processor = dr[Laptop.Database.Columns.Processor].ToString();
            laptop.Memory = dr[Laptop.Database.Columns.Memory].ToString();
            laptop.Graphics = dr[Laptop.Database.Columns.Graphics].ToString();
            laptop.Storage = dr[Laptop.Database.Columns.Storage].ToString();
            return laptop;
        }
    }
}
