using CatalogApiLambda.Controllers;
using NUnit.Framework;

namespace CatlogApiLambdaTest
{
    public class Tests
    {


        protected SearchwordController searchwordController;


        [Test]
        public void Test1()
        {
            var result = searchwordController.Get("SAteesh");
            Assert.NotNull(result);
        }
    }
}