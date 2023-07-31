using System;
using System.Globalization;
using System.Threading.Tasks;


namespace AssetTrackingWeek30
{
	public class AssetTracking
	{
        ConsoleStyler consoleStyler = new ConsoleStyler();
        string type = string.Empty;
        string brand = string.Empty;
        string model = string.Empty;
        string office = string.Empty;
        DateTime purchaseDate;
        decimal priceInUSD = 0;
        string currency = string.Empty;

        public AssetTracking()
		{
		}

        public void Run()
		{
            try
            {
                while (true)
                {
                    ConsoleUtils.ClearScreen();
                    DisplayMainMenu();

                    string choice = GetUserChoiceOfAction();
                    switch (choice)
                    {
                        case "A" or "a": //Add new Asset
                            if (!GetItemDetails())
                                break;
                            SaveProduct();
                            break;
                        case "L" or "l": // Find Listing Assets
                            PrintSavedItems();
                            break;
                        case "C" or "c": // Clean screenExit
                            ConsoleUtils.ClearScreen();
                            break;
                        case "E" or "e": // Quit
                            return;
                        default:
                            consoleStyler.StyleText("Invalid choice. Please try again.\n", TextType.Error);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                consoleStyler.StyleText("\ne.Message\n", TextType.Error);
            }
        }

        private void DisplayMainMenu()
        {
            consoleStyler.StyleText("Main Menu", TextType.MenuHeader);
            consoleStyler.StyleText("To Add a new Asset - enter 'A' \nTo List saved Asset - enter: 'L' " +
                "\nTo Exit application - enter: 'E' \nTo Clear window - enter: 'C'\n\n", TextType.InstructionBlue);
        }

        private string GetUserChoiceOfAction()
        {
             string strChoice = string.Empty;
            while (true)
            {
                consoleStyler.StyleText("Make a Choice: ", TextType.Input);
                strChoice = Console.ReadLine();
                if (ConsoleUtils.IsNotEmpty(strChoice))
                {
                    break;
                }
            }

            return strChoice;
        }

        private void SaveProduct()
        {

            List<Asset> items = new List<Asset>();
            Asset item = new Asset(type, brand, model,office,purchaseDate,priceInUSD,currency);
            items.Add(item);

            FileHandler fileHandler = new FileHandler();
            fileHandler.SaveItemsToFile(items);

            consoleStyler.StyleText("The Product was successfully added!", TextType.SuccessMessage);
            Console.ReadLine();
        }

        private bool GetItemDetails()
        {
            //Ask for Type until valid input is given.
            while (true)
            {
                consoleStyler.StyleText("Enter a Type: ", TextType.Input);
                type = Console.ReadLine();

                if (type.ToUpper() == "Q")
                    return false;

                if (ConsoleUtils.IsNotEmpty(type))
                {
                    break;
                }
            }

            //Ask for Brand until valid input is given.
            while (true)
            {
                consoleStyler.StyleText("Enter a Brand: ", TextType.Input);
                brand = Console.ReadLine();

                if (brand.ToUpper() == "Q")
                    return false;

                if (ConsoleUtils.IsNotEmpty(brand))
                {
                    break;
                }
            }

            //Ask for Model until valid input is given.
            while (true)
            {
                consoleStyler.StyleText("Enter a Model: ", TextType.Input);
                model = Console.ReadLine();

                if (model.ToUpper() == "Q")
                    return false;

                if (ConsoleUtils.IsNotEmpty(model))
                    break;
            }

            //Ask for Office location until valid input is given.
            while (true)
            {
                consoleStyler.StyleText("Enter a Office: ", TextType.Input);
                office = Console.ReadLine();

                if (office.ToUpper() == "Q")
                    return false;

                if (ConsoleUtils.IsNotEmpty(office))
                {
                    office = ConsoleUtils.CapitalizeFirstLetterOfWords(office);
                    break;
                }
            }

            //Ask for Purchase Date Format: MM/dd/yyyy
            while (true)
            {
                consoleStyler.StyleText($"Enter a Purchase Date in Format {ConstantVariable.globalDefaultDateFormat}: ", TextType.Input);
                string strPurchaseDate = Console.ReadLine();

                if (strPurchaseDate.ToUpper() == "Q")
                    return false;

                if (ConsoleUtils.IsDateValid(strPurchaseDate, ConstantVariable.globalDefaultDateFormat, out purchaseDate))
                {                   
                    break;
                }
            }

            // Ask for Amount until valid input is given.
            while (true)
            {
                consoleStyler.StyleText("Enter a Price in USD: ", TextType.Input);
                string stringpriceInUSD = Console.ReadLine();

                if (stringpriceInUSD.ToUpper() == "Q")
                    return false;

                bool boolNotEmpty = ConsoleUtils.IsNotEmpty(stringpriceInUSD);
                bool boolValidDecimal = ConsoleUtils.GetValidDecimal(stringpriceInUSD, out priceInUSD);
                if (boolNotEmpty && boolValidDecimal)
                    break;
            }

            //Ask for Currency until valid input is given.
            while (true)
            {
                consoleStyler.StyleText("Enter a Currency: ", TextType.Input);
                currency = Console.ReadLine().ToUpper();

                if (currency.ToUpper() == "Q")
                    return false;

                if (ConsoleUtils.IsNotEmpty(currency))
                    break;
            }

            return true;
        }

        private async void PrintSavedItems(string strSearchItem = "")
        {
            List<Asset> items = new List<Asset>();
            FileHandler fileHandler = new FileHandler();
            items = fileHandler.LoadItemsFromFile();

            Exchange exchange = new Exchange();
            
            var sortedData = items.OrderBy(item => item.Office).ThenBy(item=> item.PurchaseDate).ToList();

            Console.WriteLine("\n");

            string[] headers = { "Type", "Brand", "Model", "Office", "Purchase Date", "Price in USD", "Currency", "Local Price today"};
            consoleStyler.PrintHeader(headers);

            decimal decimalSavedItemTotal = 0;
            DateTime todaysDate = DateTime.Today;

            TextType textType = new TextType();
            

            foreach (Asset item in sortedData)
            {
                int ageOfAssetInYears = ConsoleUtils.GetMonthsDifference(item.PurchaseDate, todaysDate);

                if (ageOfAssetInYears >= ConstantVariable.globalAgeLimitOfAssetInMonths)
                    textType = TextType.TableDataRed;
                else if (ageOfAssetInYears >= ConstantVariable.globalAssetAgeWarningInMonth)
                {
                    textType = TextType.TableDataYellow;
                }
                else
                    textType = TextType.TableData;

                // Access and work with each item
                if (strSearchItem != string.Empty && (item.Type.ToUpper().Contains(strSearchItem.ToUpper())//For search implementation. Not implemented yet.
                    || item.Brand.ToUpper().Contains(strSearchItem.ToUpper())
                    || item.Model.ToUpper().Contains(strSearchItem.ToUpper())))
                {
                    consoleStyler.StyleText($"\n{item.Type}", TextType.TableSearchResult);
                    consoleStyler.StyleText(item.Brand, TextType.TableSearchResult);
                    consoleStyler.StyleText($"{item.Model}", TextType.TableSearchResult);
                }
                else
                {
                    consoleStyler.StyleText($"{item.Type}", textType);
                    consoleStyler.StyleText(item.Brand, textType);
                    consoleStyler.StyleText($"{item.Model}", textType);
                    consoleStyler.StyleText($"{item.Office}", textType);
                    consoleStyler.StyleText($"{item.PurchaseDate.ToString(ConstantVariable.globalDefaultDateFormat)}", textType);
                    consoleStyler.StyleText(item.PriceInUSD.ToString(), textType);
                    consoleStyler.StyleText($"{item.Currency}", textType);
                    string baseCurrency = "USD";
                    decimal localPriceToday =  exchange.GetExchangeRate(baseCurrency, item.Currency, item.PriceInUSD);
                    consoleStyler.StyleText(localPriceToday.ToString(), textType);
                    Console.WriteLine();                    
                }

                decimalSavedItemTotal += item.PriceInUSD;
            }

            consoleStyler.StyleText($"\n", TextType.TableData);
            consoleStyler.StyleText($"Total Amount: {decimalSavedItemTotal} USD\n".PadLeft(90), TextType.TableData);
            Console.ReadKey();
        }       
    }
}

