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

namespace Solvent.Web.Mvc
{
    public class Route
    {
        private readonly WebRequestDelegate action;
        private readonly string method;
        private readonly string url;

        public Route(string url, string method, WebRequestDelegate action)
        {
            this.url = url;
            this.method = method;
            this.action = action;
        }

        public bool IsMatch(ControllerContext context)
        {
            // TODO: Convert URL to consistent format
            var requestPath = (context.Request.AppRelativeCurrentExecutionFilePath + context.Request.PathInfo);
            return context.Request.HttpMethod == method && requestPath.ToLower() == url.ToLower();
        }

        public ActionResult ExecuteAction(ControllerContext context)
        {
            return action.Invoke(context);
        }
    }
}