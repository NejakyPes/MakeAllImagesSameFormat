using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeAllImagesSameFormat
{
    public static class Variables
    {
        public static string FileLocation { get; private set; }

        public static string LogLocation { get; private set; }

        static Variables()
        {
            FileLocation = ConfigurationManager.AppSettings.Get("fileLocation");

            LogLocation = ConfigurationManager.AppSettings.Get("logLocation");
            Directory.CreateDirectory(LogLocation);
        }

    }
}
