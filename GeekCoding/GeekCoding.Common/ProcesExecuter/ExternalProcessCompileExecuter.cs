﻿using GeekCoding.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace GeekCoding.Common.ProcesExecuter
{
    //implement like singleton
    public sealed class ExternalProcessCompileExecuter
    {
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

        public Tuple<Verdict, string> GetVerdictForFile(string argument)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"/c {argument}";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.Start();
            //* Read the output (or the error)

            string output = process.StandardOutput.ReadToEnd();
            Console.WriteLine(output);
            string err = process.StandardError.ReadToEnd();
            Console.WriteLine(err);
            process.WaitForExit();
            process.Close();

            Verdict verdict = Verdict.SUCCESS;

            if(!string.IsNullOrEmpty(err))
            {
                verdict = Verdict.ERROR;
            }

            StringBuilder sb = new StringBuilder("Output: ").Append(output);
            if(verdict == Verdict.ERROR)
            {
                sb.AppendLine().Append("CompilationErrors: ").Append(err);
            }

            return new Tuple<Verdict, string>(verdict, sb.ToString());

        }
    }
}
