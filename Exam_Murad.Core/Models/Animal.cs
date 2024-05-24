using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Murad.Core.Models;

public class Animal : BaseEntity
{
    public string Name {  get; set; }
    public double Price {  get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    [NotMapped]
    public IFormFile? FileImage { get; set; }
}
