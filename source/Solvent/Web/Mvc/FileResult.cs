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
using System.Net.Mime;

namespace Solvent.Web.Mvc
{
    public class FileResult : ActionResult
    {
        private readonly byte[] content;
        private readonly string contentType;
        private readonly string filename;

        public FileResult(byte[] content, string contentType) : this(content, contentType, null)
        {
        }

        public FileResult(byte[] content, string contentType, string filename)
        {
            this.content = content;
            this.contentType = contentType;
            this.filename = filename;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.Response.ClearContent();
            context.Response.ContentType = contentType;
            if (!String.IsNullOrEmpty(filename))
                context.Response.AddHeader("Content-Disposition", new ContentDisposition { FileName = filename }.ToString());
            context.Response.OutputStream.Write(content, 0, content.Length);
        }
    }
}