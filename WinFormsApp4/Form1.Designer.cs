namespace TeamsPlus
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Krypton.Toolkit.ButtonSpecAny separatorSpec1;
            Krypton.Toolkit.ButtonSpecAny separatorSpec2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            splitButtonSpec = new Krypton.Toolkit.ButtonSpecAny();
            kryptonSplitContainer1 = new Krypton.Toolkit.KryptonSplitContainer();
            cloneButtonSpec = new Krypton.Toolkit.ButtonSpecAny();
            debugButtonSpec = new Krypton.Toolkit.ButtonSpecAny();
            settingsButtonSpec = new Krypton.Toolkit.ButtonSpecAny();
            aotButtonSpec = new Krypton.Toolkit.ButtonSpecAny();
            separatorSpec1 = new Krypton.Toolkit.ButtonSpecAny();
            separatorSpec2 = new Krypton.Toolkit.ButtonSpecAny();
            ((System.ComponentModel.ISupportInitialize)kryptonSplitContainer1).BeginInit();
            kryptonSplitContainer1.Panel1.BeginInit();
            kryptonSplitContainer1.Panel2.BeginInit();
            SuspendLayout();
            // 
            // splitButtonSpec
            // 
            splitButtonSpec.Text = "Split";
            splitButtonSpec.Type = Krypton.Toolkit.PaletteButtonSpecStyle.ArrowLeft;
            splitButtonSpec.UniqueName = "8509b9671da64e41908aa4fb1c6a32bc";
            // 
            // kryptonSplitContainer1
            // 
            kryptonSplitContainer1.Dock = DockStyle.Fill;
            kryptonSplitContainer1.Location = new Point(0, 0);
            kryptonSplitContainer1.Name = "kryptonSplitContainer1";
            kryptonSplitContainer1.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlackDarkMode;
            // 
            // 
            // 
            kryptonSplitContainer1.Panel1.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlackDarkMode;
            // 
            // 
            // 
            kryptonSplitContainer1.Panel2.PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlackDarkMode;
            kryptonSplitContainer1.Panel2Collapsed = true;
            kryptonSplitContainer1.SeparatorStyle = Krypton.Toolkit.SeparatorStyle.HighProfile;
            kryptonSplitContainer1.Size = new Size(1119, 675);
            kryptonSplitContainer1.SplitterDistance = 500;
            kryptonSplitContainer1.TabIndex = 1;
            // 
            // cloneButtonSpec
            // 
            cloneButtonSpec.Text = "Clone";
            cloneButtonSpec.Type = Krypton.Toolkit.PaletteButtonSpecStyle.ArrowLeft;
            cloneButtonSpec.UniqueName = "f3dca308ad6248e3ba1fef3969e2e4dc";
            // 
            // debugButtonSpec
            // 
            debugButtonSpec.Text = "Debug";
            debugButtonSpec.Type = Krypton.Toolkit.PaletteButtonSpecStyle.ArrowUp;
            debugButtonSpec.UniqueName = "4f61a1ebf5734e238dd050b0a783cf2b";
            // 
            // separatorSpec1
            // 
            separatorSpec1.Enabled = Krypton.Toolkit.ButtonEnabled.False;
            separatorSpec1.Text = "|";
            separatorSpec1.UniqueName = "130fa65e20fe4b078ae8f485b32f65b4";
            // 
            // settingsButtonSpec
            // 
            settingsButtonSpec.Text = "Settings";
            settingsButtonSpec.Type = Krypton.Toolkit.PaletteButtonSpecStyle.Context;
            settingsButtonSpec.UniqueName = "0803b6234d694ce09244a6f99b7115b5";
            // 
            // separatorSpec2
            // 
            separatorSpec2.Enabled = Krypton.Toolkit.ButtonEnabled.False;
            separatorSpec2.Text = "|";
            separatorSpec2.UniqueName = "bc86696da8cb4fa7aaa7068a0d87413d";
            // 
            // aotButtonSpec
            // 
            aotButtonSpec.Checked = Krypton.Toolkit.ButtonCheckState.Unchecked;
            aotButtonSpec.Text = "AOT";
            aotButtonSpec.ToolTipBody = "Always on Top";
            aotButtonSpec.UniqueName = "f90c677ca2ac421182c97bfa415d1e52";
            // 
            // Form1
            // 
            AllowButtonSpecToolTips = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ButtonSpecs.Add(settingsButtonSpec);
            ButtonSpecs.Add(separatorSpec1);
            ButtonSpecs.Add(debugButtonSpec);
            ButtonSpecs.Add(separatorSpec2);
            ButtonSpecs.Add(aotButtonSpec);
            ButtonSpecs.Add(cloneButtonSpec);
            ButtonSpecs.Add(splitButtonSpec);
            ClientSize = new Size(1119, 675);
            Controls.Add(kryptonSplitContainer1);
            FormTitleAlign = Krypton.Toolkit.PaletteRelativeAlign.Inherit;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            PaletteMode = Krypton.Toolkit.PaletteMode.Office2007BlackDarkMode;
            Text = "Form1";
            Load += Form1_Load;
            kryptonSplitContainer1.Panel1.EndInit();
            kryptonSplitContainer1.Panel2.EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonSplitContainer1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Krypton.Toolkit.ButtonSpecAny splitButtonSpec;
        private Krypton.Toolkit.KryptonSplitContainer kryptonSplitContainer1;
        private Krypton.Toolkit.ButtonSpecAny cloneButtonSpec;
        private Krypton.Toolkit.ButtonSpecAny debugButtonSpec;
        private Krypton.Toolkit.ButtonSpecAny separatorSpec1;
        private Krypton.Toolkit.ButtonSpecAny settingsButtonSpec;
        private Krypton.Toolkit.ButtonSpecAny separatorSpec2;
        private Krypton.Toolkit.ButtonSpecAny aotButtonSpec;
    }
}
