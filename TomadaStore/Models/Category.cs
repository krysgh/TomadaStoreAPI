namespace TomadaStore.Models.Models
{
    public class Category
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public Category(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}