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
using System.IO;
using System.Net.Mime;
using System.Reflection;
using Graffiti.Core;

namespace Solvent.Web.Mvc
{
    public class ViewResult : ActionResult
    {
        private readonly string contentType;
        private readonly string filename;
        private readonly string name;

        public ViewResult(string name) : this(name, "text/html")
        {
        }

        public ViewResult(string name, string contentType) : this(name, contentType, null)
        {
        }

        public ViewResult(string name, string contentType, string filename)
        {
            this.name = name;
            this.contentType = contentType;
            this.filename = filename;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            foreach (var resource in Assembly.GetExecutingAssembly().GetManifestResourceNames())
            {
                if (resource.ToLower().EndsWith((name + ".view").ToLower()))
                {
                    string view;
                    using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
                    using (var reader = new StreamReader(stream))
                        view = reader.ReadToEnd();

                    RenderView(view, context);
                }
            }
        }

        /// <summary>
        /// Renders the view.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="context">The context.</param>
        private void RenderView(string view, ControllerContext context)
        {
            context["request"] = context.Request;
            context["response"] = context.Response;
            context["url"] = context.Request.RawUrl;
            context["urls"] = new Urls();
            context["util"] = new UtilWrapper();
            context["macros"] = new Macros();
            context["data"] = new Data();
            context["site"] = SiteSettings.Get();

            context.Response.ClearContent();
            context.Response.ContentType = contentType;
            if (!String.IsNullOrEmpty(filename))
                context.Response.AddHeader("Content-Disposition", new ContentDisposition { FileName = filename }.ToString());
            TemplateEngine.Evaluate(context.Response.Output, view, context);
            context.Response.End();
        }
    }
}