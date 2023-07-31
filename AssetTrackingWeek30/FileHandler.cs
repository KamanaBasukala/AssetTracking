using System;
using System.Runtime.InteropServices;

namespace AssetTrackingWeek30
{
    //This file handles text file operations.
    public class FileHandler
    {
        private char separator = ';'; // Define the separator character
        static string[] locationFolders = new string[5] { "Documents", "Lexicon", "AssetTrackingWeek30", "Data", "DataFile.txt" };
        string fileLocation = string.Empty;

        public FileHandler()
        {
            bool isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
            bool isMacOS = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

            if (isWindows)
            {
                Console.WriteLine("Windows Environment");
            }
            else if (isMacOS)
            {
                string documentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string filePath = Path.Combine(locationFolders);
                fileLocation = Path.Combine(documentsFolder, filePath);
            }
            else
            {
                Console.WriteLine("Unknown Environment");
            }

            IsFileExist(fileLocation);
        }

        public bool IsFileExist(string path)
        {
            var fileinfo = new System.IO.FileInfo(path);

            if (!fileinfo.Exists)
            {
                //Create directory if it doesn't exist
                System.IO.Directory.CreateDirectory(fileinfo.Directory.FullName);
            }

            return true;
        }

        public List<Asset> LoadItemsFromFile()
        {
            List<Asset> items = new List<Asset>();

            try
            {
                using (StreamReader reader = new StreamReader(fileLocation))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(separator);

                        if (parts.Length == 7)
                        {
                            //"Type", "Brand", "Model", "Office", "Purchase Date", "Price in USD", "Currency", "Local Price today"
                            string type = parts[0];
                            string brand = parts[1];
                            string model = parts[2];
                            string office = parts[3];
                            string strPurchaseDate = parts[4];
                            DateTime purchaseDate;
                            string strPriceInUSD = parts[5];
                            string currency = parts[6];
                            ConsoleUtils.IsDateValid(strPurchaseDate, ConstantVariable.globalDefaultDateFormat, out purchaseDate);

                            decimal priceInUSD = decimal.Parse(strPriceInUSD);

                            Asset item = new Asset(type, brand, model,office, purchaseDate, priceInUSD,currency);
                            items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading items from file: " + ex.Message);
            }

            return items;
        }

        public void SaveItemsToFile(List<Asset> items)//, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileLocation, append: true))
            {
                foreach (Asset item in items)
                {
                    string line = $"{item.Type}{separator}{item.Brand}{separator}{item.Model}" +
                        $"{separator}{item.Office}{separator}{item.PurchaseDate.ToString(ConstantVariable.globalDefaultDateFormat)}" +
                        $"{separator}{item.PriceInUSD}{separator}{item.Currency}";
                    writer.WriteLine(line);
                }
            }
        }

    }
}

