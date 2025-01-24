namespace ChairsLib.Models
{
    public class Chair
    {
        public int Id { get; set; }
        public string? Model { get; set; }
        public int MaxWeight { get; set; }
        public bool HasPillow { get; set; }

        public void ValidateModel()
        {
            if (Model == null)
            {
                throw new ArgumentNullException(nameof(Model), "Model cannot be null");
            }

            if (Model.Length < 2)
            {
                throw new ArgumentException("Model must be at least 2 characters long", nameof(Model));
            }
        }

        public void ValidateMaxWeight()
        {
            if (MaxWeight < 50)
            {
                throw new ArgumentOutOfRangeException(nameof(MaxWeight), "MaxWeight must be at least 50");
            }
        }

        public void Validate()
        {
            ValidateModel();
            ValidateMaxWeight();
        }

        public override string ToString()
        {
            return $"Id: {Id}, Model: {Model}, MaxWeight: {MaxWeight}, HasPillow: {HasPillow}";
        }
    }
}
