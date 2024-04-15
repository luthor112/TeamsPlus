using CefSharp.WinForms;
using CefSharp;
using System.Diagnostics;
using Krypton.Toolkit;

namespace TeamsPlus
{
    public partial class Form1 : KryptonForm
    {
        string rootCache;
        ChromiumWebBrowser browser;
        ChromiumWebBrowser sideBrowser;

        public Form1()
        {
            rootCache = Path.Join(Path.GetTempPath(), "TeamsPlusCefRoot");
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var settings = new CefSettings()
            {
                RootCachePath = rootCache,
                PersistSessionCookies = true,
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/123.0.0.0 Safari/537.36 Edg/123.0.2420.97"
            };
            settings.CefCommandLineArgs.Add("enable-media-stream");
            settings.CefCommandLineArgs.Add("use-fake-ui-for-media-stream");
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            splitButtonSpec.Click += SplitClicked;
            cloneButtonSpec.Click += CloneClicked;

            AddBrowser();
        }

        private void AddBrowser()
        {
            IRequestContext ctx = RequestContext.Configure().WithCachePath(Path.Join(rootCache, "cache1")).Create();

            browser = new ChromiumWebBrowser("https://teams.microsoft.com");
            browser.RequestContext = ctx;
            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            kryptonSplitContainer1.Panel1.Controls.Add(browser);
            //browser.LoadingStateChanged += Browser_LoadingStateChanged;
            browser.TitleChanged += Browser_TitleChanged;

            //browser.ExecuteScriptAsyncWhenPageLoaded("alert('Hi!');", false);
            //browser.EvaluateScriptAsync("alert('Hi!');");
        }

        private void SplitClicked(object? sender, EventArgs e)
        {
            SwitchSplit(false);
        }

        private void CloneClicked(object? sender, EventArgs e)
        {
            SwitchSplit(true);
        }

        private void SwitchSplit(bool clone)
        {
            if (kryptonSplitContainer1.Panel2Collapsed)
            {
                IRequestContext ctx = null;

                if (clone)
                    ctx = RequestContext.Configure().WithSharedSettings(browser.GetRequestContext()).Create();
                else
                    ctx = RequestContext.Configure().WithSharedSettings(Cef.GetGlobalRequestContext()).Create();

                sideBrowser = new ChromiumWebBrowser("https://teams.microsoft.com");
                sideBrowser.RequestContext = ctx;
                sideBrowser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
                kryptonSplitContainer1.Panel2.Controls.Add(sideBrowser);

                kryptonSplitContainer1.Panel2Collapsed = false;
                cloneButtonSpec.Visible = false;
                splitButtonSpec.Type = PaletteButtonSpecStyle.ArrowRight;
            }
            else
            {
                cloneButtonSpec.Visible = true;
                splitButtonSpec.Type = PaletteButtonSpecStyle.ArrowLeft;
                kryptonSplitContainer1.Panel2Collapsed = true;
                kryptonSplitContainer1.Panel2.Controls.Clear();
                sideBrowser.Dispose();
            }
        }

        private void Browser_LoadingStateChanged(object? sender, LoadingStateChangedEventArgs e)
        {
            
        }

        private void Browser_TitleChanged(object? sender, TitleChangedEventArgs e)
        {
            Invoke(() => Text = e.Title);
        }
    }
}
