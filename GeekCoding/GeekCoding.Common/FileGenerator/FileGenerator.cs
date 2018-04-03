﻿using GeekCoding.Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GeekCoding.Common
{
    public class FileGenerator : IFileGenerator
    {
        private const string SOURCES = "sources";
        public void GenerateFile(string content, string language, string problemName, string userName)
        {
            var goodDirectory = GetCurrentDirectory();
            if (!Directory.Exists(goodDirectory))
            {
                Directory.CreateDirectory(goodDirectory);
            }

            StringBuilder sb = new StringBuilder();
            string problemFullName = Builder.BuildProblemName(problemName, userName);
            sb.Append(problemFullName).Append(LanguageHelper.GetLanguageExtenstionType(language));
            string sourceName = sb.ToString();

            var fileToCreate = Path.Combine(goodDirectory, sourceName);

            if (!File.Exists(fileToCreate))
            {
                using (FileStream fs = new FileStream(fileToCreate, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None))
                {
                    StreamWriter writer = new StreamWriter(fs, Encoding.UTF8);
                    writer.WriteLine(content);
                    writer.Flush();
                }
            }
        }

        public string GetCurrentDirectory()
        {
            var myDoc = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var goodDirectory = Path.Combine(myDoc, SOURCES);
            return goodDirectory;
        }

        public string GetFileFullName(string problemName, string userName, string language)
        {
            var goodDirectory = GetCurrentDirectory();

            StringBuilder sb = new StringBuilder();
            string problemFullName = Builder.BuildProblemName(problemName, userName);
            sb.Append(problemFullName);
            string sourceName = sb.ToString();

            var fileToCreate = Path.Combine(goodDirectory, sourceName);
            return fileToCreate;
        }
    }
}
