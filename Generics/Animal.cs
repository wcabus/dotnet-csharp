namespace Generics
{
    public abstract class Animal
    {
        protected Animal(string classification)
        {
            Classification = classification;
        }

        public string Classification { get; } // Amphibian, mammal, reptile, ...
    }

    public class Cat : Animal
    {
        public Cat() : base("Mammal")
        {
        }

        public string Name { get; set; }

        public void Purr() { }
    }
}