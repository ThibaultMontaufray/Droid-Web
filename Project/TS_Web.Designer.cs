﻿namespace Droid_web
{
    partial class TS_Web
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
            this._eventLog = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this._eventLog)).BeginInit();
            // 
            // _eventLog
            // 
            this._eventLog.Log = "TS_Web_log";
            this._eventLog.Source = "TS_Web_Source";
            // 
            // TS_Web
            // 
            this.ServiceName = "TS_Web";
            ((System.ComponentModel.ISupportInitialize)(this._eventLog)).EndInit();

        }

        #endregion

        private System.Diagnostics.EventLog _eventLog;
    }
}