using System;
using Xunit;
using team_reece.Controllers;
using team_reece.Models;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics.CodeAnalysis;

namespace team_reece_tests
{
    public class testTodoApi
    {
        [Fact]
        public void GetRequestReturnsJSON()
        {
            //Arrange

           //var _context = new teamfuContext();

            var DbSetStub = new Mock<DbSet<ToDo>>();
            var DbContextStub = new Mock<teamfuContext>();
            DbContextStub.Setup(s=> s.ToDos).Returns(DbSetStub.Object);
            var testListController = new ListsController(DbContextStub.Object);
            
            //Act

            var result = testListController.Get().Count!= 0;

            //Assert

    


            
        
            

            

            

           


        

        

        

        }
    }
}
