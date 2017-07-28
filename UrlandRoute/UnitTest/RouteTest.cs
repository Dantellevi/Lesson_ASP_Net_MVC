using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Reflection;
using System.Web;
using System.Web.Routing;
using UrlandRoute;

namespace UnitTest
{
    [TestClass]
    public class RouteTest
    {
        private HttpContextBase CreateHttpContext(string targetUrl = null,
            string httpMethod = "GET")
        {
            //Создать имитированный запрос
            Mock<HttpRequestBase> mockRequest = new Mock<HttpRequestBase>();
            mockRequest.Setup(m => m.AppRelativeCurrentExecutionFilePath).Returns(targetUrl);

            mockRequest.Setup(m => m.HttpMethod).Returns(httpMethod);

            //создать имитированный ответ
            Mock<HttpResponseBase> mockResponse = new Mock<HttpResponseBase>();
            mockResponse.Setup(m => m.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(s => s);

            //создать имитированный контекст, используя запрос и ответ
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();
            mockContext.Setup(m => m.Request).Returns(mockRequest.Object);
            mockContext.Setup(m => m.Response).Returns(mockResponse.Object);

            //возвратить имитированный контекст
            return mockContext.Object;

        }


        private void TestRouteMatch(string url,string controller,string action, object routeProperties=null, string httpMethod = "GET")
        {
            //Организация
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Действие- обработка маршрута
            RouteData result = routes.GetRouteData(CreateHttpContext(url, httpMethod));

            //Утверждение
            Assert.IsNotNull(result);
            Assert.IsTrue(TestIncomingRouteResult(result, controller, action, routeProperties));

        }

        private bool TestIncomingRouteResult(RouteData Routeresult, string controller, string action, object propertySet=null)
        {
            Func<object, object, bool> valcompare = (v1, v2) => { return StringComparer.InvariantCultureIgnoreCase.Compare(v1, v2) == 0; };
            bool result = valcompare(Routeresult.Values["controller"], controller)&&valcompare(Routeresult.Values["action"],action);

            if(propertySet!=null)
            {
                PropertyInfo[] proinfo = propertySet.GetType().GetProperties();
                foreach(PropertyInfo pi in proinfo)
                {
                    if (!(Routeresult.Values.ContainsKey(pi.Name) && valcompare(Routeresult.Values[pi.Name], pi.GetValue(propertySet, null))))
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;


        }


        private void TestRouteFail(string url)
        {
            //Организация
            RouteCollection routes = new RouteCollection();
            RouteConfig.RegisterRoutes(routes);

            //Действие-обработка маршрута
            RouteData result = routes.GetRouteData(CreateHttpContext(url));

            //Утверждение
            Assert.IsTrue(result == null || result.Route == null);

        }

        [TestMethod]
        public void TestIncomingRoutes()
        {
            //проверить URL,  который мы надеемся получить
            TestRouteMatch("~/Admin/Index", "Admin", "Index");

            //Проверить значения, получаемые  из сегментов
            TestRouteMatch("~/Once/Two", "Once", "Two");

            //удостовериться , что слишкоам много или слишком мало сегментов  не приводят к совпаделию
            TestRouteFail("~/Admin/Index/Segment");
            TestRouteFail("~/Admin");

        }

        [TestMethod]
        public void TestIncomingRoutess()
        {
            TestRouteMatch("~/", "Home", "Index");
            TestRouteMatch("~/Customer", "Customer", "Index");
            TestRouteMatch("~/Customer/List", "Customer", "List");
            TestRouteMatch("~/Customer/List/All",null,null);
            TestRouteMatch("~/Shop/Index", "Home", "Index");

        }
    }
}
