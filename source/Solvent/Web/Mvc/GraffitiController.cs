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
using Graffiti.Core;

namespace Solvent.Web.Mvc
{
    /// <summary>
    /// A simple web engine for extending Graffiti CMS.
    /// </summary>
    public abstract class GraffitiController : GraffitiEvent
    {
        #region Delegates

        #endregion

        private readonly RouteCollection routes = new RouteCollection();

        public FileResult File(byte[] content, string contentType)
        {
            return new FileResult(content, contentType);
        }

        public FileResult File(byte[] content, string contentType, string filename)
        {
            return new FileResult(content, contentType, filename);
        }

        /// <summary>
        /// Initializes the <see cref="GraffitiEvent"/>.
        /// </summary>
        /// <param name="application">The application.</param>
        public override void Init(GraffitiApplication application)
        {
            RegisterRoutes(routes);
            if (routes.Count > 0)
                application.BeginRequest += OnBeginRequest;
        }

        public abstract void RegisterRoutes(RouteCollection routes);

        public ViewResult View(string name)
        {
            return new ViewResult(name);
        }

        public ViewResult View(string name, string contentType)
        {
            return new ViewResult(name, contentType);
        }

        public ViewResult View(string name, string contentType, string filename)
        {
            return new ViewResult(name, contentType, filename);
        }

        /// <summary>
        /// Called at the beginning of a request.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnBeginRequest(object sender, EventArgs e)
        {
            var context = ControllerContext.Create(HttpContext.Current);
            var route = routes.GetRoute(context);
            if (route == null)
                return;

            var result = route.ExecuteAction(context);
            if (result != null)
                result.ExecuteResult(context);
        }
    }
}