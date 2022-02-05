using System.Threading.Tasks;

namespace back.Currency
{
    public interface ICurrencyUpdate
    {
        public Task UpdateCurrency();
    }
}