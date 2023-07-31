using System;
using System.Text.Json;

namespace AssetTrackingWeek30
{
	public class Exchange
	{
        static string json;

        public Exchange()
        {
            string strApiKey = "HC92mBwUX8ie0LTII7aDlfgI1GyrzhZs";          
            string apiUrl = $"https://api.apilayer.com/currency_data/live?apikey={strApiKey}&base=USD";

            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = httpClient.GetAsync(apiUrl).GetAwaiter().GetResult(); ;

                    if (response.IsSuccessStatusCode)
                    {
                        json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();                                            
                    }
                    else
                    {
                        throw new Exception($"API request failed. Status Code: {response.StatusCode}");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error connecting to the API: {ex.Message}");
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error parsing the API response: {ex.Message}");
            }
        }

        public decimal GetExchangeRate(string baseCurrency, string targetCurrency, decimal amount)
        {
            decimal exchangeRate =  GetExchangeRate(baseCurrency, targetCurrency);
            return amount * exchangeRate;
        }

        private static decimal GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            if (baseCurrency == targetCurrency)
                return 1;

            try
            {
                // Parse the JSON string using JsonDocument to access properties that don't match the class structure
                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;
                    bool success = root.GetProperty("success").GetBoolean();

                    if (success)
                    {
                        // Access the exchange rate for the target currency directly
                        decimal exchangeRate = root.GetProperty("quotes").GetProperty($"USD{targetCurrency}").GetDecimal();
                        return exchangeRate;
                    }
                    else
                    {
                        throw new Exception("API request failed: Success is false.");
                    }
                }                    
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error connecting to the API: {ex.Message}");
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error parsing the API response: {ex.Message}");
            }
        }
    }
    public class Exchange1
    {
        public async Task<decimal> GetExchangeRate(string baseCurrency, string targetCurrency, decimal amount)
        {
            decimal exchangeRate = await GetExchangeRate(baseCurrency, targetCurrency);
            return amount * exchangeRate;
        }

        private static async Task<decimal> GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            string strApiKey = "HC92mBwUX8ie0LTII7aDlfgI1GyrzhZs";
            string apiUrl = $"https://api.apilayer.com/currency_data/live?apikey={strApiKey}&base=USD&symbols=EUR&amount=50";
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        ExchangeRateResponse exchangeRateData = JsonSerializer.Deserialize<ExchangeRateResponse>(json);

                        // Parse the JSON string using JsonDocument to access properties that don't match the class structure
                        using (JsonDocument document = JsonDocument.Parse(json))
                        {
                            JsonElement root = document.RootElement;
                            bool success = root.GetProperty("success").GetBoolean();

                            if (success)
                            {
                                // Access the exchange rate for the target currency directly
                                decimal exchangeRate = root.GetProperty("quotes").GetProperty($"USD{targetCurrency}").GetDecimal();
                                return exchangeRate;
                            }
                            else
                            {
                                throw new Exception("API request failed: Success is false.");
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"API request failed. Status Code: {response.StatusCode}");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error connecting to the API: {ex.Message}");
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error parsing the API response: {ex.Message}");
            }
        }
    }
}


