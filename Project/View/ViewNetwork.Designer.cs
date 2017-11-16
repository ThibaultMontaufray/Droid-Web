namespace Droid_web
{
    partial class ViewNetwork
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
            this.panelScrollable = new Tools4Libraries.PanelScrollable();
            this.SuspendLayout();
            // 
            // panelScrollable
            // 
            this.panelScrollable.AutoScroll = true;
            this.panelScrollable.AutoScrollHorizontalMaximum = 100;
            this.panelScrollable.AutoScrollHorizontalMinimum = 0;
            this.panelScrollable.AutoScrollHPos = 0;
            this.panelScrollable.AutoScrollVerticalMaximum = 100;
            this.panelScrollable.AutoScrollVerticalMinimum = 0;
            this.panelScrollable.AutoScrollVPos = 0;
            this.panelScrollable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScrollable.EnableAutoScrollHorizontal = true;
            this.panelScrollable.EnableAutoScrollVertical = true;
            this.panelScrollable.Location = new System.Drawing.Point(0, 0);
            this.panelScrollable.Name = "panelScrollable";
            this.panelScrollable.Size = new System.Drawing.Size(1010, 500);
            this.panelScrollable.TabIndex = 0;
            this.panelScrollable.VisibleAutoScrollHorizontal = false;
            this.panelScrollable.VisibleAutoScrollVertical = true;
            // 
            // ViewNetwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.panelScrollable);
            this.Name = "ViewNetwork";
            this.Size = new System.Drawing.Size(1010, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private Tools4Libraries.PanelScrollable panelScrollable;
    }
}
