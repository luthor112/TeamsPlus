using CefSharp;
using CefSharp.Handler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsPlus
{
    internal class RequestHandlerPlus : RequestHandler
    {
        protected override bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
        {
            if (userGesture)
            {
                Uri requestURL = new Uri(request.Url);
                if (requestURL.IsAbsoluteUri && !(requestURL.Host.EndsWith(".microsoft.com") || requestURL.Host.EndsWith(".onmicrosoft.com")))
                {
                    Process.Start("explorer", $"\"{request.Url}\"");
                    return true;
                }
            }

            return false;
        }
    }
}
