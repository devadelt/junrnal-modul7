using System;
using System.Text.Json;

namespace tpmodul7_1302204058
{
    class program
    {
        static void Main(string[] args)
        {
            UIconfig obj = new UIconfig();
            Console.WriteLine(obj);
            Console.WriteLine("Please insert the amount of money to transfer:" + obj.config.lang);

            Console.WriteLine("Masukkan jumlah uang yang akan di-transfer:" + obj.config.lang);
            
        }
    }

    public class UIconfig
    {
        public BankTransferConfig config;
        public string filePath = "C:/Users/devar/source/repos/modul7_1302204058/modul7_1302204058" + "/bank_transfer_config.json";
        public UIconfig()
        {
            try
            {
                ReadConfigFile();
            }
            catch (Exception)
            {
                SetDefault();
                WriteNewConfigFile();
            }
        }
        private void SetDefault()
        {
            transfer tf = new transfer(
                25000000,
                6500,
                15000
            );
            List<string> mtd = new List<string>(){
                "RTO (real-time)",
                "SKN",
                "RTGS",
                "BI FAST"
            };

            confirmation cf = new confirmation(
                "yes",
                "ya"
            );
            config = new BankTransferConfig("en", tf, mtd, cf);
        }
        private BankTransferConfig ReadConfigFile()
        {
            String configJsonData = File.ReadAllText(filePath);
            config = JsonSerializer.Deserialize<BankTransferConfig>(configJsonData);
            return config;
        }
        private void WriteNewConfigFile()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            String jsonString = JsonSerializer.Serialize(config, options);
            File.WriteAllText(filePath, jsonString);
        }
    }
    public class BankTransferConfig
    {
        public string lang { get; set; }
        public transfer transfer { get; set; }
        public List<string> methods { get; set; }
        public confirmation confirmation { get; set; }
        public BankTransferConfig(string lang, transfer transfer, List<string> methods, confirmation confirmation)
        {
            this.lang = lang;
            this.transfer = transfer;
            this.methods = methods;
            this.confirmation = confirmation;
        }
    }
    public class transfer
    {
        public int threshold { get; set; }
        public int low_fee { get; set; }
        public int high_fee { get; set; }
        public transfer(int threshold, int low_fee, int high_fee)
        {
            this.threshold = threshold;
            this.low_fee = low_fee;
            this.high_fee = high_fee;
        }
    }
    public class confirmation
    {
        public string en { get; set; }
        public string id { get; set; }
        public confirmation(string en, string id)
        {
            this.en = en;
            this.id = id;
        }
    }
}