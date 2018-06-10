﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GeekCoding.Compilation.Execution
{
    public interface IExecutionFile
    {
        void Execute(string problemName, string userName, string language,string timeLimit, string memoryLimit);
    }
}