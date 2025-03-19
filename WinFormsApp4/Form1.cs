using CefSharp.WinForms;
using CefSharp;
using System.Diagnostics;
using Krypton.Toolkit;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TeamsPlus
{
    internal enum SwitchType
    {
        Clone,
        Secondary,
        Temporary,
        Close
    }

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
        bool switchFirstLoad = true;

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
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

            if (!File.Exists(configFile))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(configFile));
                File.WriteAllText(configFile, "[config]\n\n[theme]\n");
            }

            settingsButtonSpec.Click += SettingsClicked;
            debugButtonSpec.Click += DebugClicked;
            chatListButtonSpec.Click += ChatListClicked;
            chatListSecondarySpec.Click += ChatListClicked;
            focusButtonSpec.Click += FocusClicked;
            focusSecondarySpec.Click += FocusClicked;
            aotButtonSpec.Click += aotClicked;
            cloneButtonSpec.Click += CloneClicked;
            secondaryButtonSpec.Click += SecondaryClicked;
            temporaryButtonSpec.Click += TemporaryClicked;
            closeButtonSpec.Click += CloseClicked;

            AddMainBrowser();
        }

        #region Titlebar buttons
        private void SettingsClicked(object? sender, EventArgs e)
        {
            Process.Start("explorer", $"\"{configFile}\"");
        }

        private void DebugClicked(object? sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Shift && !kryptonSplitContainer1.Panel2Collapsed)
                sideBrowser.ShowDevTools();
            else
                browser.ShowDevTools();
        }

        private void ChatListClicked(object? sender, EventArgs e)
        {
            ButtonSpecAny clickedButton = (ButtonSpecAny)sender;
            bool isSecondary = (clickedButton != chatListButtonSpec);

            ChromiumWebBrowser currentBrowser = isSecondary ? sideBrowser : browser;

            if (clickedButton.Checked == ButtonCheckState.Checked)
                currentBrowser.ExecuteScriptAsync("document.querySelector('[data-tid=\"app-layout-area--sub-nav\"]').style.display = \"\";");
            else
                currentBrowser.ExecuteScriptAsync("document.querySelector('[data-tid=\"app-layout-area--sub-nav\"]').style.display = \"none\";");
        }

        private void FocusClicked(object? sender, EventArgs e)
        {
            ButtonSpecAny clickedButton = (ButtonSpecAny)sender;
            bool isSecondary = (clickedButton != focusButtonSpec);

            ChromiumWebBrowser currentBrowser = isSecondary ? sideBrowser : browser;

            if (clickedButton.Checked == ButtonCheckState.Checked)
                currentBrowser.ExecuteScriptAsync("document.getElementsByTagName(\"video\")[0].requestFullscreen();");
            else
                currentBrowser.ExecuteScriptAsync("document.exitFullscreen();");
        }

        private void aotClicked(object? sender, EventArgs e)
        {
            TopMost = (aotButtonSpec.Checked == ButtonCheckState.Checked);
        }

        private void CloneClicked(object? sender, EventArgs e)
        {
            SwitchSplit(SwitchType.Clone);
        }

        private void SecondaryClicked(object? sender, EventArgs e)
        {
            SwitchSplit(SwitchType.Secondary);
        }

        private void TemporaryClicked(object? sender, EventArgs e)
        {
            SwitchSplit(SwitchType.Temporary);
        }

        private void CloseClicked(object? sender, EventArgs e)
        {
            SwitchSplit(SwitchType.Close);
        }
        #endregion

        private void AddMainBrowser()
        {
            IRequestContext ctx = RequestContext.Configure().WithCachePath(Path.Join(rootCache, "cache1")).Create();

            browser = new ChromiumWebBrowser("https://teams.microsoft.com");
            browser.RequestContext = ctx;
            browser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
            kryptonSplitContainer1.Panel1.Controls.Add(browser);
            browser.LoadingStateChanged += Browser_LoadingStateChanged;
            browser.TitleChanged += Browser_TitleChanged;
            browser.LifeSpanHandler = new LifeSpanHandlerPlus();
        }

        private void SwitchSplit(SwitchType switchType)
        {
            if (kryptonSplitContainer1.Panel2Collapsed)
            {
                IRequestContext ctx = null;

                switch (switchType)
                {
                    case SwitchType.Clone:
                        ctx = RequestContext.Configure().WithSharedSettings(browser.GetRequestContext()).Create();
                        break;
                    case SwitchType.Secondary:
                        ctx = RequestContext.Configure().WithCachePath(Path.Join(rootCache, "cache2")).Create();
                        break;
                    case SwitchType.Temporary:
                        ctx = RequestContext.Configure().WithSharedSettings(Cef.GetGlobalRequestContext()).Create();
                        break;
                    default:
                        Debug.WriteLine("This should not happen!");
                        return;
                }

                sideBrowser = new ChromiumWebBrowser("https://teams.microsoft.com");
                sideBrowser.RequestContext = ctx;
                sideBrowser.JavascriptObjectRepository.Settings.LegacyBindingEnabled = true;
                kryptonSplitContainer1.Panel2.Controls.Add(sideBrowser);
                sideBrowser.LoadingStateChanged += Browser_LoadingStateChanged;
                sideBrowser.LifeSpanHandler = new LifeSpanHandlerPlus();

                kryptonSplitContainer1.Panel2Collapsed = false;
                cloneButtonSpec.Visible = false;
                secondaryButtonSpec.Visible = false;
                temporaryButtonSpec.Visible = false;
                closeButtonSpec.Visible = true;
                chatListSecondarySpec.Visible = true;
                focusSecondarySpec.Visible = true;
            }
            else
            {
                if (switchType != SwitchType.Close)
                    Debug.WriteLine("This should not happen!");

                chatListSecondarySpec.Visible = false;
                focusSecondarySpec.Visible = false;
                cloneButtonSpec.Visible = true;
                secondaryButtonSpec.Visible = true;
                temporaryButtonSpec.Visible = true;
                closeButtonSpec.Visible = false;
                kryptonSplitContainer1.Panel2Collapsed = true;
                kryptonSplitContainer1.Panel2.Controls.Clear();
                sideBrowser.Dispose();

                switchFirstLoad = true;
            }
        }

        private void Browser_LoadingStateChanged(object? sender, LoadingStateChangedEventArgs e)
        {
            ChromiumWebBrowser currentBrowser = (ChromiumWebBrowser)sender;
            bool isSecondary = (currentBrowser != browser);

            string pageURL = currentBrowser.GetMainFrame().Url;
            if (!e.IsLoading && (pageURL.StartsWith("https://teams.live.com/v2/") || pageURL.StartsWith("https://teams.microsoft.com/v2/")))
            {
                if (firstLoad && !isSecondary)
                {
                    firstLoad = false;
                    Thread.Sleep(1000);
                }
                else if (switchFirstLoad && isSecondary)
                {
                    switchFirstLoad = false;
                    Thread.Sleep(1000);
                }

                string headerBg = GetOption("theme", "headerbg", "");
                string chatBg = GetOption("theme", "chatbg", "");
                string chatBlend = GetOption("theme", "chatblend", "");

                if (isSecondary)
                {
                    headerBg = GetOption("theme", "headerbg-secondary", "");
                    chatBg = GetOption("theme", "chatbg-secondary", "");
                    chatBlend = GetOption("theme", "chatblend-secondary", "");
                }

                if (pageURL.StartsWith("https://teams.live.com/v2/"))
                {
                    if (headerBg != "")
                    {
                        if (headerBg[0] == '#')
                            currentBrowser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"app-layout-area--title-bar\"]').style.background = \"{headerBg}\";");
                        else
                            currentBrowser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"app-layout-area--title-bar\"]').style.backgroundImage = \"url({headerBg})\";");
                    }

                    if (chatBg != "")
                    {
                        if (chatBg[0] == '#')
                        {
                            currentBrowser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"message-pane-layout\"]').style.background = \"{chatBg}\";");
                        }
                        else
                        {
                            if (chatBlend != "")
                            {
                                currentBrowser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"message-pane-layout\"]').style.background = \"rgba(255,255,255,{chatBlend})\";");
                                currentBrowser.ExecuteScriptAsync("document.querySelector('[data-tid=\"message-pane-layout\"]').style.backgroundBlendMode = \"lighten\";");
                            }

                            currentBrowser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"message-pane-layout\"]').style.backgroundImage = \"url({chatBg})\";");
                        }
                    }
                }
                else if (pageURL.StartsWith("https://teams.microsoft.com/v2/"))
                {
                    if (headerBg != "")
                    {
                        if (headerBg[0] == '#')
                            currentBrowser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"title-bar\"]').style.background = \"{headerBg}\";");
                        else
                            currentBrowser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"title-bar\"]').style.backgroundImage = \"url({headerBg})\";");
                    }

                    if (chatBg != "")
                    {
                        if (chatBg[0] == '#')
                        {
                            currentBrowser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"message-pane-layout\"]').style.background = \"{chatBg}\";");
                        }
                        else
                        {
                            if (chatBlend != "")
                            {
                                currentBrowser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"message-pane-layout\"]').style.background = \"rgba(255,255,255,{chatBlend})\";");
                                currentBrowser.ExecuteScriptAsync("document.querySelector('[data-tid=\"message-pane-layout\"]').style.backgroundBlendMode = \"lighten\";");
                            }

                            currentBrowser.ExecuteScriptAsync($"document.querySelector('[data-tid=\"message-pane-layout\"]').style.backgroundImage = \"url({chatBg})\";");
                        }

                        //currentBrowser.ExecuteScriptAsync("document.getElementById(\"chat-pane-list\").style.background = \"rgba(255,255,255,0)\";");
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
