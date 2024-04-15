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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            splitButtonSpec = new Krypton.Toolkit.ButtonSpecAny();
            kryptonSplitContainer1 = new Krypton.Toolkit.KryptonSplitContainer();
            cloneButtonSpec = new Krypton.Toolkit.ButtonSpecAny();
            ((System.ComponentModel.ISupportInitialize)kryptonSplitContainer1).BeginInit();
            kryptonSplitContainer1.Panel1.BeginInit();
            kryptonSplitContainer1.Panel2.BeginInit();
            SuspendLayout();
            // 
            // splitButtonSpec
            // 
            splitButtonSpec.Text = "Split";
            splitButtonSpec.ToolTipBody = "Yolo Button";
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
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
    }
}
