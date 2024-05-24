using Exam_Murad.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Murad.Business.Service.Abstract;

public interface IAnimalService 
{
    Task AddAnimal(Animal animal);
    void DeleteAnimal(int id);
    void Update (int id, Animal newAnimal);
    Animal GetAnimal(Func<Animal, bool>? func = null);
    List<Animal> GetAllAnimals(Func<Animal, bool>? func = null);
}
