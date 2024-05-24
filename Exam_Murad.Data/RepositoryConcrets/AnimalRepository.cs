using Exam_Murad.Core.Abstract;
using Exam_Murad.Core.Models;
using Exam_Murad.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Murad.Data.RepositoryAbstract;

public class AnimalRepository : GenericRepository<Animal>, IAnimalRepository
{
    public AnimalRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }
}
