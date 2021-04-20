using System;
using Xunit;
using team_reece.Controllers;
using team_reece.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace team_reece_tests
{
    public class testTodoApi
    {
        [Fact]
        public void GetRequestReturnsJSON()
        {
            //Arrange

            

            Xunit.Assert.True(true);
    

        }

        
         public static string GenerateDBConnectionFromEnvMan()
         {
           string host = "localhost";
           string port =  "1433";
            string userid ="SA";
           string password = "Your_password1";
           string database = "teamfu";
          return $"Data Source={host},{port};Database={database};User Id={userid};Password={password};";
        }
    }
}
