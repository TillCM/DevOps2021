using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.IO;

namespace team_reece
{
    public static class DotEnv 
    {
        public static void Load (string filepath)
        {
            if(!File.Exists(filepath))
            {
                System.Console.WriteLine("Env file not found ");
                return ;
            }

            foreach (var line in File.ReadAllLines(filepath))
            {
                var parts = line .Split('=',StringSplitOptions.RemoveEmptyEntries);
                if(parts.Length != 2)
                continue;

                Environment.SetEnvironmentVariable(parts[0],parts[1]);
            }
        }


    }

}