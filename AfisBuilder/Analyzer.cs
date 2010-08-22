﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;

namespace AfisBuilder
{
    class Analyzer
    {
        public static string DatabasePath = "TestDatabase";
        public static float Accuracy;
        public static float Speed;
        public static float ExtractionTime;
        public static float TemplateSize;

        public static void PrepareXmlConfiguration(string sourcePath, string targetPath)
        {
            XDocument document = XDocument.Load(sourcePath);
            document.Root.Element("test-database").SetElementValue("scan", DatabasePath);
            document.Save(targetPath);
        }

        public static void ReadAccuracy()
        {
            XElement root = new XDocument(Command.FixPath(@"Matcher\Accuracy\Standard\Accuracy.xml")).Root;
            Accuracy = Convert.ToSingle(root.Element("AverageError"), CultureInfo.InvariantCulture);
        }

        public static void ReadSpeed()
        {
            XElement root = new XDocument(Command.FixPath(@"Matcher\MatcherTime.xml")).Root;
            Speed = 1 / Convert.ToSingle(root.Element("NonMatching"), CultureInfo.InvariantCulture);
        }

        public static void ReadExtractorStats()
        {
            XElement root = new XDocument(Command.FixPath(@"Extractor\ExtractorReport.xml")).Root;
            ExtractionTime = Convert.ToSingle(root.Element("Time"), CultureInfo.InvariantCulture);
            TemplateSize = Convert.ToSingle(root.Element("TemplateSize"), CultureInfo.InvariantCulture);
        }

        public static void ReportStatistics()
        {
            Console.WriteLine("DatabaseAnalyzer results:");
            Console.WriteLine("    EER: {0:F2}%", Accuracy * 100);
            Console.WriteLine("    Speed: {0:F0} fp/s", Speed);
            Console.WriteLine("    Extraction time: {0:F0}ms", ExtractionTime * 1000);
            Console.WriteLine("    Template size: {0:F1} KB", TemplateSize / 1024);
        }
    }
}
