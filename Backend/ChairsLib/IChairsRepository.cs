using ChairsLib.Models;

namespace ChairsLib
{
    public interface IChairsRepository
    {
        Chair Add(Chair newChair);
        Chair? Delete(int id);
        IEnumerable<Chair> GetAll();
        Chair? GetById(int id);
        Chair? Update(int id, Chair newChairValues);
    }
}