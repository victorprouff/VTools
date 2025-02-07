using VTools.Models.EnVrac;

namespace VTools.Services.EnVrac;

public interface IEnVracService
{
    List<EnVracItem> GetEnVracItems();
}