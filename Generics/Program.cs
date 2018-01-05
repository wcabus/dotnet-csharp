using System.Collections.Generic;
using System.Linq;

namespace Generics
{
    public class Program
    {
        static void Main(string[] args)
        {
            var cats = new List<Cat> {new Cat {Name = "Gizmo"}};
            IEnumerable<Animal> animals = cats; // Works, because IEnumerable is covariant.

            var animalValidator = new AnimalValidator();
            var catValidator = new CatValidator();

            ValidateCats(cats, animalValidator); // Can give an IValidator<Animal> to a method that expects IValidator<Cat> because T is contravariant
            ValidateAnimals(cats, animalValidator); // Of course, you can also validate cats using the ValidateAnimals method because the source is both Cat and Animal and IEnumerable is covariant
            // ValidateAnimals(cats, catValidator); // But you can't validate animals using IValidate<Cat>, even if they're all cats!
            ValidateCats(cats, catValidator);

            Animal convertedCat = catValidator.Convert(cats.First()); // Also works, because IConvertable's source is contravariant and return value is covariant
        }

        static bool ValidateAnimals(IEnumerable<Animal> animals, IValidate<Animal> validator)
        {
            foreach (var animal in animals)
            {
                if (!validator.Validate(animal))
                {
                    return false;
                }
            }

            return true;
        }

        static bool ValidateCats(IEnumerable<Cat> cats, IValidate<Cat> validator)
        {
            foreach (var cat in cats)
            {
                if (!validator.Validate(cat))
                {
                    return false;
                }
            }

            return true;
        }
    }

    // Contravariant T type parameter. You can typically read this as: T is only being used as a parameter (it only goes into methods)
    public interface IValidate<in T>
    {
        bool Validate(T item);
    }

    // Covariant TTo type parameter. You can typically read this as: TTo is only being used as a return type (it only goes out of methods)
    public interface IConvertable<in TFrom, out TTo>
    {
        TTo Convert(TFrom source);
    }

    public class AnimalValidator : IValidate<Animal>
    {
        public bool Validate(Animal animal)
        {
            return !string.IsNullOrWhiteSpace(animal.Classification);
        }
    }

    public class CatValidator : IValidate<Cat>, IConvertable<Animal, Cat>
    {
        public bool Validate(Cat cat)
        {
            return !string.IsNullOrWhiteSpace(cat.Name);
        }

        public Cat Convert(Animal animal)
        {
            if (animal is Cat cat)
            {
                return cat;
            }

            return null;
        }
    }
}
