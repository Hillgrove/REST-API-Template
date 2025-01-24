using ChairsLib.Models;

namespace ChairsLib
{
    public class ChairsRepositoryDB : IChairsRepository
    {
        private readonly ChairsDbContext _context;

        public ChairsRepositoryDB(ChairsDbContext context)
        {
            _context = context;
        }

        public Chair Add(Chair newChair)
        {
            newChair.Validate();
            newChair.Id = 0;
            _context.Chairs.Add(newChair);
            _context.SaveChanges();
            return newChair;
        }

        public Chair? Delete(int id)
        {
            Chair? deletedChair = GetById(id);
            if (deletedChair is null)
            {
                return null;
            }

            _context.Chairs.Remove(deletedChair);
            _context.SaveChanges();
            return deletedChair;
        }

        public IEnumerable<Chair> GetAll()
        {
            return _context.Chairs.ToList();
        }

        public Chair? GetById(int id)
        {
            return _context.Chairs.FirstOrDefault(c => c.Id == id);
        }

        public Chair? Update(int id, Chair newChairValues)
        {
            var updatedChair = GetById(id);
            if (updatedChair is null)
            {
                return null;
            }

            updatedChair.Model = newChairValues.Model;
            updatedChair.MaxWeight = newChairValues.MaxWeight;
            updatedChair.HasPillow = newChairValues.HasPillow;

            _context.SaveChanges();
            return updatedChair;
        }
    }
}
