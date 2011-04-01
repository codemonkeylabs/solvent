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

namespace Solvent.Web.Mvc
{
    /// <summary>
    /// A handler for a web request.
    /// </summary>
    /// <param name="context">The current <see cref="HttpContext"/>.</param>
    /// <returns>The name of the view to render.</returns>
    public delegate ActionResult WebRequestDelegate(ControllerContext context);
}