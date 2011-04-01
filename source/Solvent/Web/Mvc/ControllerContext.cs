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
using System.Web;
using NVelocity;

namespace Solvent.Web.Mvc
{
    public class ControllerContext : VelocityContext
    {
        private readonly HttpContext httpContext;

        private ControllerContext(HttpContext httpContext)
        {
            this.httpContext = httpContext;
        }

        public HttpContext HttpContext
        {
            get { return httpContext; }
        }

        public object this[string key]
        {
            get { return Get(key); }
            set { Put(key, value); }
        }

        public HttpRequest Request
        {
            get { return httpContext.Request; }
        }

        public HttpResponse Response
        {
            get { return httpContext.Response; }
        }

        public static ControllerContext Create(HttpContext httpContext)
        {
            return new ControllerContext(httpContext);
        }
    }
}