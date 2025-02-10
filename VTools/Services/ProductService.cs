using VTools.Services.Interfaces;

namespace VTools.Services;

public class ProductService(IEnumerable<string> products) : IProductService
{
    public IEnumerable<string> GetProducts() => products;
    public long GetMaximumBinaryValue()
    {
        var binaryValue = new string('1', products.Count());
        return Convert.ToInt64(binaryValue);
    }

    public int GetMaximumDecimalValue()
    {
        var binaryValue = new string('1', products.Count());
        return Convert.ToInt32(binaryValue, 2);
    }

    public bool[] GetDefaultBinaryTableValue()
    {
        var result = new bool[products.Count()];
        Array.Fill(result, false);

        return result;
    }
}