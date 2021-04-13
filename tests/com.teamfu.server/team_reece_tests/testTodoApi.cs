using System;
using Xunit;
using Xunit.Assert;
using team_reece.Controllers;
using team_reece.Models;

namespace team_reece_tests
{
    public class testTodoApi
    {
        [Fact]
        public void GetRequestReturnsJSON()
        {
            //Arrange

            var _context = new teamfuContext();
            var testListController = new ListsController(_context);

    
            //Act

            var result = testListController.Get().Count!= 0;

            //Assert

            

           


        

        

        

        }
    }
}
