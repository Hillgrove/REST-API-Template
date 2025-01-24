using ChairsLib.Models;


namespace ChairsLibTests
{
    [TestClass()]
    public class ChairTests
    {
        private readonly Chair _chair = new() { Id = 1, Model = "A2", MaxWeight = 50, HasPillow = true };
        private readonly Chair _nullModel = new() { Id = 2, MaxWeight = 50, HasPillow = true };
        private readonly Chair _shortModel = new() { Id = 3, Model = "A", MaxWeight = 50, HasPillow = true };
        private readonly Chair _lowMaxWeight = new() { Id = 4, Model = "A2", MaxWeight = 49, HasPillow = true };

        [TestMethod()]
        public void ValidateModelTest()
        {
            _chair.ValidateModel();
            Assert.ThrowsException<ArgumentNullException>(() => _nullModel.ValidateModel());
            Assert.ThrowsException<ArgumentException>(() => _shortModel.ValidateModel());
        }

        [TestMethod()]
        public void ValidateMaxWeightTest()
        {
            _chair.ValidateMaxWeight();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _lowMaxWeight.ValidateMaxWeight());
        }

        [TestMethod()]
        public void ValidateTest()
        {
            _chair.Validate();
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("Id: 1, Model: A2, MaxWeight: 50, HasPillow: True", _chair.ToString());
        }
    }
}