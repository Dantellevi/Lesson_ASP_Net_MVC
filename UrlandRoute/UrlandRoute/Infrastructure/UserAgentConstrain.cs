using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace UrlandRoute.Infrastructure
{
    public class UserAgentConstrain : IRouteConstraint
    {

        private string requireUserAgent;

        public UserAgentConstrain(string agentParam)
        {
            requireUserAgent = agentParam;
        }

        //метод используется для указания системе маршрутизации, удовлетворено 
        //ли ограничение .Параметры метода предоставляют доступ к запросу,
        //поступившему от клиента, проверяемому маршруту, имени параметра ограничения , 
        //переменных сегментов , которые извлечены из URL,
        //и признаку того, какой URL проверяет запрос входящий или исходящий.
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return httpContext.Request.UserAgent != null &&
                 httpContext.Request.UserAgent.Contains(requireUserAgent);
        }
    }
}