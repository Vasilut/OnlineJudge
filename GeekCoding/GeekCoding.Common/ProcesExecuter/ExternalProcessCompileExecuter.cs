﻿using GeekCoding.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace GeekCoding.Common.ProcesExecuter
{
    //implement like singleton
    public sealed class ExternalProcessCompileExecuter
    {
        private List<string> _errorPosibilities = new List<string> { "error", "fatal error " };
        private static readonly ExternalProcessCompileExecuter instance = new ExternalProcessCompileExecuter();

        static ExternalProcessCompileExecuter()
        {

        }

        private ExternalProcessCompileExecuter()
        {

        }

        public static ExternalProcessCompileExecuter Instance
        {
            get
            {
                return instance;
            }
        }

        public Tuple<Verdict, string> GetVerdictForFile(string argument,string workingDirectory)
        {
            Process process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = $"-c \"{argument}\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.Start();
            //* Read the output (or the error)

            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            string err = process.StandardError.ReadToEnd();
            Console.WriteLine(err);
            process.WaitForExit();
            process.Close();

            Verdict verdict = Verdict.SUCCESS;
            StringBuilder sb = new StringBuilder();

            if(!string.IsNullOrEmpty(err))
            {
                bool b = _errorPosibilities.Any(s => err.Contains(s));
                if (b)
                {
                    verdict = Verdict.ERROR;
                    sb.Append("CompilationErrors: ").Append(err);
                }
                else
                {
                    verdict = Verdict.SUCCESS;
                    sb.Append("Output: ").Append(err);
                }
            }
            else
            {
                sb.Append("Output: ").Append(output);
            }
            
            return new Tuple<Verdict, string>(verdict, sb.ToString());

        }

        public string SandboxOperation(string argument, string workingDirectory)
        {
            Process process = new Process();
            process.StartInfo.FileName = "/bin/bash";
            process.StartInfo.Arguments = $"-c \"{argument}\"";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WorkingDirectory = workingDirectory;
            process.Start();

            string output = process.StandardOutput.ReadToEnd();

            Console.WriteLine("");
            process.WaitForExit();
            process.Close();

            return output;
        }
    }
}
