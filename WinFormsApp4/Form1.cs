using CefSharp.WinForms;
using CefSharp;
using System.Diagnostics;
using Krypton.Toolkit;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TeamsPlus
{
    public partial class Form1 : KryptonForm
    {
        #region P/Invoke
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpDefault,
            StringBuilder lpReturnedString,
            uint nSize,
            string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern uint WritePrivateProfileString(
            string lpAppName,
            string lpKeyName,
            string lpString,
            string lpFileName);
        #endregion

        string configFile;
        string rootCache;
        ChromiumWebBrowser browser;
        ChromiumWebBrowser sideBrowser;

        bool firstLoad = true;

        public Form1()
        {
            rootCache = Path.Join(Path.GetTempPath(), "TeamsPlusCefRoot");
            configFile = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "teamsplus", "config.ini");
            InitializeComponent();
        }

        private string GetOption(string sectionName, string optionName, string defaultValue)
        {
            StringBuilder sb = new StringBuilder(1024);
            GetPrivateProfileString(sectionName, optionName, defaultValue, sb, (uint)sb.Capacity, configFile);
            return sb.ToString();
        }

        private void SetOption(string sectionName, string optionName, string optionValue)
        {
            WritePrivateProfileString(sectionName, optionName, optionValue, configFile);
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
            //settings.CefCommandLineArgs.Add("use-fake-ui-for-media-stream", "1");
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            if (!File.Exists(configFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(configFile));
                File.WriteAllText(configFile, "[config]\n\n[theme]\n");
            }

            settingsButtonSpec.Click += SettingsClicked;
            debugButtonSpec.Click += DebugClicked;
            aotButtonSpec.Click += aotClicked;
            splitButtonSpec.Click += SplitClicked;
            cloneButtonSpec.Click += CloneClicked;

            AddMainBrowser();
        }

        private void aotClicked(object? sender, EventArgs e)
        {
            TopMost = (aotButtonSpec.Checked == ButtonCheckState.Checked);
        }

        private void SettingsClicked(object? sender, EventArgs e)
        {
            Process.Start("explorer", $"\"{configFile}\"");
        }

        private void DebugClicked(object? sender, EventArgs e)
        {
            browser.ShowDevTools();
        }

        private void SplitClicked(object? sender, EventArgs e)
        {
            SwitchSplit(false);
        }

        private void CloneClicked(object? sender, EventArgs e)
        {
            SwitchSplit(true);
        }

        private void AddMainBrowser()
        {
            IRequestContext ctx = RequestContext.Configure().WithCachePath(Path.Join(rootCache, "cache1")).Create();

            browser = new ChromiumWebBrowser("https://teams.microsoft.com");
            browser.RequestContext = ctx;
            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            kryptonSplitContainer1.Panel1.Controls.Add(browser);
            browser.LoadingStateChanged += Browser_LoadingStateChanged;
            browser.TitleChanged += Browser_TitleChanged;
            //browser.RequestHandler = new RequestHandlerPlus();
            browser.LifeSpanHandler = new LifeSpanHandlerPlus();
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
            string pageURL = browser.GetMainFrame().Url;
            if (!e.IsLoading && (pageURL.StartsWith("https://teams.live.com/") || pageURL.StartsWith("https://teams.microsoft.com/v2/")))
            {
                if (firstLoad)
                {
                    firstLoad = false;
                    Thread.Sleep(1000);
                }

                if (pageURL.StartsWith("https://teams.live.com/"))
                {
                    bool cleanupUI = (GetOption("config", "cleanup", "true") == "true");
                    if (cleanupUI)
                    {
                        browser.ExecuteScriptAsync("document.getElementsByTagName(\"app-bar-help-button\")[0].remove();");
                        browser.ExecuteScriptAsync("document.getElementsByTagName(\"get-app-button\")[0].remove();");
                    }

                    string headerBg = GetOption("theme", "headerbg", "");
                    if (headerBg != "")
                    {
                        if (headerBg[0] == '#')
                            browser.ExecuteScriptAsync($"document.getElementsByTagName(\"app-header-bar\")[0].children[0].style.background = \"{headerBg}\";");
                        else
                            browser.ExecuteScriptAsync($"document.getElementsByTagName(\"app-header-bar\")[0].children[0].style.backgroundImage = \"url({headerBg})\";");
                    }
                }
                else if (pageURL.StartsWith("https://teams.microsoft.com/v2/"))
                {
                    string headerBg = GetOption("theme", "headerbg", "");
                    if (headerBg != "")
                    {
                        if (headerBg[0] == '#')
                            browser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"app-layout-area--title-bar\"]').children[0].children[0].style.background = \"{headerBg}\";");
                        else
                            browser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"app-layout-area--title-bar\"]').children[0].children[0].style.backgroundImage = \"url({headerBg})\";");
                    }
                }
            }
        }

        private void Browser_TitleChanged(object? sender, TitleChangedEventArgs e)
        {
            Invoke(() => Text = e.Title);
        }
    }
}
