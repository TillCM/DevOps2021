using Microsoft.VisualStudio.TestTools.UnitTesting;
using team_reece.Controllers;
using team_reece.Models;

namespace testToDoWebApi
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange

            var _context = new teamfuContext();
            ListsController testController = new ListsController(_context);


            //Act
            var result = testController.Get().Count!= null;


            //Assert

            


        }
    }
}
