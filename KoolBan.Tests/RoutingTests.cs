using System;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace KoolBan.Tests
{
    [TestFixture]
    public class RoutingTests
    {
        // With inspiration from Chapter 15, "URL Routing", Pro ASP.NET MVC 5, Apress
        private HttpContextBase CreateHttpContext(string targetUrl = null,
            string httpMethod = "GET")
        {
            // Setup mock request 
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath)
                .Returns(targetUrl);
            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            // Setup mock response
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(
                It.IsAny<string>())).Returns<string>(s => s);

            // Setup mock context, using the request and response
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            // Return the mocked context
            return mockContext.Object;
        }

        // With inspiration from Chapter 15, "URL Routing", Pro ASP.NET MVC 5, Apress
        private void TestRouteMatch(string url, string controller, string action,
            object routeProperties = null, string httpMethod = "GET")
        {

            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            RouteData result
                = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller,
                action, routeProperties));
        }
        // With inspiration from Chapter 15, "URL Routing", Pro ASP.NET MVC 5, Apress
        private void TestRouteMismatch(string url, string controller, string action,
            object routeProperties = null, string httpMethod = "GET")
        {

            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            RouteData result
                = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(TestIncomingRouteResult(result, controller,
                action, routeProperties));
        }

        // With inspiration from Chapter 15, "URL Routing", Pro ASP.NET MVC 5, Apress
        private bool TestIncomingRouteResult(RouteData routeResult,
            string controller, string action, object propertySet = null)
        {

            Func<object, object, bool> valCompare = (v1, v2) =>
            {
                return StringComparer.InvariantCultureIgnoreCase
                    .Compare(v1, v2) == 0;
            };

            bool result = valCompare(routeResult.Values["controller"], controller)
                          && valCompare(routeResult.Values["action"], action);

            if (propertySet != null)
            {
                PropertyInfo[] propInfo = propertySet.GetType().GetProperties();
                foreach (PropertyInfo pi in propInfo)
                {
                    if (!(routeResult.Values.ContainsKey(pi.Name)
                          && valCompare(routeResult.Values[pi.Name],
                              pi.GetValue(propertySet, null))))
                    {

                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        // With inspiration from Chapter 15, "URL Routing", Pro ASP.NET MVC 5, Apress
        private void TestRouteFail(string url)
        {
            // Arrange
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            // Act - process the route
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            // Assert
            Assert.IsTrue(result == null || result.Route == null);
        }

        [Test]
        public void Test_Routing_Homepage()
        {
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/", "Home", "Index", new { id = "Demo" });
            TestRouteMatch("~/Home", "Home", "Index", new { id = "Demo" });
            TestRouteMatch("~/Home/Index", "Home", "Index", new { id = "Demo" });
            TestRouteMatch("~/Home/Index/Demo", "Home", "Index", new { id = "Demo" });

            TestRouteMismatch("~/WrongUrl", "Home", "Index", new { id = "Demo"});
            TestRouteMismatch("~/Home/Index/WrongUrl", "Home", "Index", new { id = "Demo"});

            TestRouteFail("~/Home/Index/Demo/WrongUrl");
        }

        [Test]
        public void Test_Routing_Projects()
        {
            TestRouteMatch("~/myproject", "Home", "Index", new { id = "myproject" });
            TestRouteMatch("~/myproject2", "Home", "Index", new { id = "myproject2" });
            TestRouteMismatch("~/WrongUrl", "Home", "Index", new { id = "myproject" });
        }

        [Test]
        public void Test_Routing_Login()
        {
            TestRouteMatch("~/Login", "Home", "Login");
        }

        [Test]
        public void Test_Routing_Demo()
        {
            TestRouteMatch("~/Demo", "Home", "Index", new { id = "Demo"} );
        }

    }
}
