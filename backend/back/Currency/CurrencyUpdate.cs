using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using back.Models;
using AutoMapper;
using Newtonsoft.Json;

namespace back.Currency
{
    public class CurrencyUpdate : ICurrencyUpdate
    {
        static readonly HttpClient client = new HttpClient();
        private readonly IMapper mapper;
        public CurrencyUpdate(IMapper _mapper)
        {
            mapper = _mapper;
        }

        public async Task UpdateCurrency()
        {

            try
            {
                HttpResponseMessage response = await client.GetAsync("https://api.blockchain.com/v3/exchange/tickers/BTC-USD");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<dynamic>(responseBody);

                using (var service = new CryptoContext())
                {
                    CreateBtcModel newData = new CreateBtcModel();
                    newData.Price = data.last_trade_price;
                    newData.Idate = DateTime.Now;
                    service.Btcs.Add(mapper.Map<Btc>(newData));
                    service.SaveChanges();
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nException Caught!");
                Console.WriteLine("Message :{0} ", e.Message);
            }
        }
    }


}