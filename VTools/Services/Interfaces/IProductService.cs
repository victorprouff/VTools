namespace VTools.Services.Interfaces;

public interface IProductService
{
    public IEnumerable<string> GetProducts();
    public long GetMaximumBinaryValue();
    public int GetMaximumDecimalValue();

    public bool[] GetDefaultBinaryTableValue();
}