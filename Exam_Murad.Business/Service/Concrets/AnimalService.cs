using Exam_Murad.Business.Exceptions;
using Exam_Murad.Business.Service.Abstract;
using Exam_Murad.Core.Abstract;
using Exam_Murad.Core.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Murad.Business.Service.Concrets;

public class AnimalService : IAnimalService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly IWebHostEnvironment _env;
    public AnimalService(IAnimalRepository animalRepository, IWebHostEnvironment env)
    {
        _animalRepository = animalRepository;
        _env = env;
    }

    public async Task AddAnimal(Animal animal)
    {
        if (animal.FileImage.ContentType != "img/png" && animal.FileImage.ContentType != "img/jpeg")
            throw new ImageContentException("format duz deyil");
        if (animal.FileImage.Length > 2097152) throw new ImageSizeException("sikilin olcusu 2 mb ola biler");
        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(animal.FileImage.FileName);
        string path = _env.WebRootPath +"\\uploads\\animal\\"+ fileName;
        using(FileStream fileStream = new FileStream(path, FileMode.Create))
        {
            animal.FileImage.CopyTo(fileStream);
        }
        animal.ImageUrl = fileName;
        await _animalRepository.AddAsync(animal);
        await _animalRepository.CommitAsync();
    }

    public void DeleteAnimal(int id)
    {
        var existAnimal =_animalRepository.Get(x => x.Id == id);
        if (existAnimal != null) throw new EntityNotFoundException("id tapilmadi");

        string path = _env.WebRootPath + "\\uploads\\animal\\" + existAnimal.FileImage;
         File.Delete(path);
        _animalRepository.Delete(existAnimal);
        _animalRepository.Commit();
    }

    public List<Animal> GetAllAnimals(Func<Animal, bool>? func = null)
    {
        return _animalRepository.GetAll(func);
    }

    public Animal GetAnimal(Func<Animal, bool>? func = null)
    {
        return _animalRepository.Get(func);
    }

    public void Update(int id, Animal newAnimal)
    {
        var existAnimal = _animalRepository.Get(x => x.Id == id);
        if (existAnimal != null) throw new EntityNotFoundException("id tapilmadi");

        if (newAnimal.FileImage != null)
        {
            if (newAnimal.FileImage.ContentType != "img/jpeg" && newAnimal.FileImage.ContentType != "img/jpeg")
                throw new ImageContentException("fayl format movcud deyil");
            if (newAnimal.FileImage.Length > 2097152) throw new ImageSizeException("sekil olcusu 2 mb ola biler");
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(newAnimal.FileImage.FileName);
            string path = _env.WebRootPath + "\\uploads\\product\\" + fileName;
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                newAnimal.FileImage.CopyTo(fileStream);
            }
            string oldPath = _env.WebRootPath + "\\iploads\\product\\" + existAnimal.FileImage;
            File.Delete(oldPath);
            _animalRepository.Delete(existAnimal);
            existAnimal.ImageUrl = oldPath;
        }
        _animalRepository.Get().Name = existAnimal.Name; 
        _animalRepository.Get().Price = existAnimal.Price;
        _animalRepository.Get().Description = existAnimal.Description;
        _animalRepository.Commit();
    }

}
