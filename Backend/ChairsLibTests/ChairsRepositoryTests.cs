using ChairsLib;
using ChairsLib.Models;
using Microsoft.EntityFrameworkCore;


namespace ChairsLibTests
{
    [TestClass()]
    public class ChairsRepositoryTests
    {
        private IChairsRepository _repository;
        private IEnumerable<Chair> _initialChairs;
        private ChairsDbContext? _context;
        private readonly bool useDatabase = false; // Set this to true to use the database repository

        [TestInitialize]
        public void Setup()
        {
            _initialChairs = new List<Chair>
            {
                new Chair() { Model = "Test Chair 1", MaxWeight = 100, HasPillow = true },
                new Chair() { Model = "Test Chair 2", MaxWeight = 120, HasPillow = false },
                new Chair() { Model = "Test Chair 3", MaxWeight = 150, HasPillow = true }
            };

            if (useDatabase)
            {
                var options = new DbContextOptionsBuilder<ChairsDbContext>()
                    .UseInMemoryDatabase(databaseName: "ChairsTestDb")
                    .Options;

                _context = new ChairsDbContext(options);
                _repository = new ChairsRepositoryDB(_context);
            }
            else
            {
                _repository = new ChairsRepositoryList();
            }

            foreach (Chair chair in _initialChairs)
            {
                _repository.Add(chair);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (_context != null)
            {
                _context.Database.EnsureDeleted();
                _context.Dispose();
            }
        }

        [TestMethod()]
        public void GetAll_ShouldReturnAllChairs()
        {
            // Act
            var chairs = _repository.GetAll();
            int expected = _initialChairs.Count();
            int result = chairs.Count();

            // Assert
            Assert.AreEqual(expected, result);
            CollectionAssert.AreEqual(_initialChairs.Select(c => c.Model).ToList(), chairs.Select(c => c.Model).ToList());
        }

        [TestMethod()]
        public void GetById_ValidId_ShouldReturnChair()
        {
            // Act
            var chair = _repository.GetById(1);

            // Assert
            Assert.IsNotNull(chair);
        }

        [TestMethod()]
        public void GetById_InvalidId_ShouldReturnNull()
        {
            // Act
            var chair = _repository.GetById(-1);

            // Assert
            Assert.IsNull(chair);
        }

        [TestMethod()]
        public void Add_ValidChair_ShouldAddChair()
        {
            // Arrange
            string newModel = "NewModel";
            int newMaxWeight = 200;

            var newChair = new Chair() { Model = newModel, MaxWeight = newMaxWeight, HasPillow = true };

            // Act
            var addedChair = _repository.Add(newChair);
            var chairs = _repository.GetAll();

            // Assert
            int result = chairs.Count();
            Assert.AreEqual(_initialChairs.Count() + 1, result);
            Assert.AreEqual(_initialChairs.Count() + 1, addedChair.Id);
            Assert.AreEqual(newModel, addedChair.Model);
            Assert.AreEqual(newMaxWeight, addedChair.MaxWeight);
            Assert.IsTrue(addedChair.HasPillow);
        }

        [TestMethod()]
        public void Update_ValidId_ShouldUpdateChair()
        {
            // Arrange
            string updatedModel = "UpdatedModel";
            int updatedMaxWeight = 200;
            var updatedChair = new Chair() { Model = updatedModel, MaxWeight = updatedMaxWeight, HasPillow = false };

            // Act
            var result = _repository.Update(1, updatedChair);
            var chair = _repository.GetById(1)!;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(updatedModel, chair.Model);
            Assert.AreEqual(updatedMaxWeight, chair.MaxWeight);
            Assert.IsFalse(chair.HasPillow);
        }

        [TestMethod()]
        public void Update_InvalidId_ShouldReturnNull()
        {
            // Arrange
            var updatedChair = new Chair() { Model = "UpdatedModel", MaxWeight = 200, HasPillow = false };

            // Act
            var result = _repository.Update(-1, updatedChair);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod()]
        public void Delete_ValidId_ShouldRemoveChair()
        {
            // Act
            var deletedChair = _repository.Delete(1);
            var chairs = _repository.GetAll();

            // Assert
            int expected = _initialChairs.Count() - 1;
            Assert.IsNotNull(deletedChair);
            Assert.AreEqual(1, deletedChair.Id);
            Assert.AreEqual(expected, chairs.Count());
        }

        [TestMethod()]
        public void Delete_InvalidId_ShouldReturnNull()
        {
            // Act
            var deletedChair = _repository.Delete(-1);

            // Assert
            Assert.IsNull(deletedChair);
        }

        [TestMethod()]
        public void EmptyRepository_ShouldHandleGracefully()
        {
            // Arrange
            _repository = new ChairsRepositoryList();

            // Act
            var chairs = _repository.GetAll();

            // Assert
            Assert.AreEqual(0, chairs.Count());
        }
    }
}