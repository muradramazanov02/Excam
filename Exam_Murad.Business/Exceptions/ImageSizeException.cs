﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Murad.Business.Exceptions;

public class ImageSizeException : Exception
{
    public ImageSizeException(string? message) : base(message)
    {
    }
}
