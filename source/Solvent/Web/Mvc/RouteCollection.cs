//	Copyright(c) 2011 Code Monkey Labs - http://codemonkeylabs.com/
//
//	Licensed under the Apache License, Version 2.0 (the "License"); 
//	you may not use this file except in compliance with the License. 
//	You may obtain a copy of the License at 
//
//	http://www.apache.org/licenses/LICENSE-2.0 
//
//	Unless required by applicable law or agreed to in writing, software 
//	distributed under the License is distributed on an "AS IS" BASIS, 
//	WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//	See the License for the specific language governing permissions and 
//	limitations under the License. 

using System;
using System.Collections.Generic;

namespace Solvent.Web.Mvc
{
    public class RouteCollection
    {
        private readonly List<Route> routes = new List<Route>();

        public int Count
        {
            get { return routes.Count; }
        }

        public void AddRoute(string method, string url, WebRequestDelegate action)
        {
            routes.Add(new Route(url, method, action));
        }

        public Route GetRoute(ControllerContext context)
        {
            foreach (var route in routes)
                if (route.IsMatch(context))
                    return route;

            return null;
        }
    }
}