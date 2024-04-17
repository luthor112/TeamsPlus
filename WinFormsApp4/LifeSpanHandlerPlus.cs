using CefSharp;
using CefSharp.DevTools.Network;
using CefSharp.Handler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamsPlus
{
    internal class LifeSpanHandlerPlus : LifeSpanHandler
    {
        protected override bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition,
            bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            if (userGesture && !string.IsNullOrEmpty(targetUrl))
            {
                Uri requestURL = new Uri(targetUrl);
                if (requestURL.IsAbsoluteUri && !(requestURL.Host.EndsWith(".microsoft.com") || requestURL.Host.EndsWith(".onmicrosoft.com")))
                {
                    Process.Start("explorer", $"\"{targetUrl}\"");
                    newBrowser = null;
                    return true;
                }
            }

            return base.OnBeforePopup(chromiumWebBrowser, browser, frame, targetUrl, targetFrameName, targetDisposition, userGesture, popupFeatures, windowInfo, browserSettings, ref noJavascriptAccess, out newBrowser);
        }
    }
}
