using ChairsLib.Models;

namespace ChairsLib
{
    public class ChairsRepositoryList : IChairsRepository
    {
        private int _nextId = 1;
        private readonly List<Chair> _chairs = new();

        public ChairsRepositoryList() { }

        public IEnumerable<Chair> GetAll()
        {
            return new List<Chair>(_chairs);
        }

        public Chair? GetById(int id)
        {
            return _chairs.Find(c => c.Id == id);
        }

        public Chair Add(Chair newChair)
        {
            newChair.Validate();
            newChair.Id = _nextId++;
            _chairs.Add(newChair);
            return newChair;
        }

        public Chair? Update(int id, Chair newChairValues)
        {
            newChairValues.Validate();
            Chair? updatedChair = GetById(id);
            if (updatedChair == null)
            {
                return null;
            }

            updatedChair.Model = newChairValues.Model;
            updatedChair.MaxWeight = newChairValues.MaxWeight;
            updatedChair.HasPillow = newChairValues.HasPillow;
            return updatedChair;
        }

        public Chair? Delete(int id)
        {
            Chair? deletedChair = GetById(id);

            if (deletedChair == null)
            {
                return null;
            }

            _chairs.Remove(deletedChair);
            return deletedChair;
        }
    }
}
