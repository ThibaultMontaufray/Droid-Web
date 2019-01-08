namespace Droid.Web.UI.View
{
    partial class ViewBrowser
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewBrowser));
            this.tabControlCustom1 = new Tools.Utilities.UI.TabControlCustom.TabControlCustom();
            this.imageListFavicon = new System.Windows.Forms.ImageList(this.components);
            this.buttonAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabControlCustom1
            // 
            this.tabControlCustom1.DisplayStyle = Tools.Utilities.UI.TabControlCustom.TabStyle.Chrome;
            // 
            // 
            // 
            this.tabControlCustom1.DisplayStyleProvider.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.tabControlCustom1.DisplayStyleProvider.BorderColorHot = System.Drawing.SystemColors.ControlDark;
            this.tabControlCustom1.DisplayStyleProvider.BorderColorSelected = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(157)))), ((int)(((byte)(185)))));
            this.tabControlCustom1.DisplayStyleProvider.CloserColor = System.Drawing.Color.DarkGray;
            this.tabControlCustom1.DisplayStyleProvider.CloserColorActive = System.Drawing.Color.White;
            this.tabControlCustom1.DisplayStyleProvider.FocusTrack = false;
            this.tabControlCustom1.DisplayStyleProvider.HotTrack = true;
            this.tabControlCustom1.DisplayStyleProvider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tabControlCustom1.DisplayStyleProvider.Opacity = 1F;
            this.tabControlCustom1.DisplayStyleProvider.Overlap = 16;
            this.tabControlCustom1.DisplayStyleProvider.Padding = new System.Drawing.Point(7, 5);
            this.tabControlCustom1.DisplayStyleProvider.Radius = 16;
            this.tabControlCustom1.DisplayStyleProvider.ShowTabCloser = true;
            this.tabControlCustom1.DisplayStyleProvider.TextColor = System.Drawing.SystemColors.ControlText;
            this.tabControlCustom1.DisplayStyleProvider.TextColorDisabled = System.Drawing.SystemColors.ControlDark;
            this.tabControlCustom1.DisplayStyleProvider.TextColorSelected = System.Drawing.SystemColors.ControlText;
            this.tabControlCustom1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCustom1.HotTrack = true;
            this.tabControlCustom1.ImageList = this.imageListFavicon;
            this.tabControlCustom1.Location = new System.Drawing.Point(0, 0);
            this.tabControlCustom1.Name = "tabControlCustom1";
            this.tabControlCustom1.SelectedIndex = 0;
            this.tabControlCustom1.Size = new System.Drawing.Size(1196, 601);
            this.tabControlCustom1.TabIndex = 0;
            // 
            // imageListFavicon
            // 
            this.imageListFavicon.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListFavicon.ImageStream")));
            this.imageListFavicon.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListFavicon.Images.SetKeyName(0, "default");
            //
            // buttonAdd
            //
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.Text = "add";
            this.buttonAdd.Top = 4;
            this.buttonAdd.Left = 200;
            // 
            // WebBrowserControl
            // 
            this.KeyDown += WebBrowserControl_KeyDown;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControlCustom1);
            this.Controls.Add(this.buttonAdd);
            this.Name = "WebBrowserControl";
            this.Size = new System.Drawing.Size(1196, 601);
            this.ResumeLayout(false);

        }
        #endregion

        private Tools.Utilities.UI.TabControlCustom.TabControlCustom tabControlCustom1;
        private System.Windows.Forms.ImageList imageListFavicon;
        private System.Windows.Forms.Button buttonAdd;
    }
}
