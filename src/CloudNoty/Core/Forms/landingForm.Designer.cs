namespace CloudNoty.Core.Forms
{
    partial class landingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listNoty = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listNoty
            // 
            this.listNoty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listNoty.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listNoty.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listNoty.GridLines = true;
            this.listNoty.Location = new System.Drawing.Point(12, 12);
            this.listNoty.MultiSelect = false;
            this.listNoty.Name = "listNoty";
            this.listNoty.Size = new System.Drawing.Size(739, 331);
            this.listNoty.TabIndex = 0;
            this.listNoty.UseCompatibleStateImageBehavior = false;
            this.listNoty.View = System.Windows.Forms.View.Details;
            this.listNoty.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listNoty_MouseDoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID Noty";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Title Noty";
            this.columnHeader2.Width = 250;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Last Edit";
            this.columnHeader3.Width = 225;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Creation Date";
            this.columnHeader4.Width = 225;
            // 
            // landingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(763, 467);
            this.Controls.Add(this.listNoty);
            this.Name = "landingForm";
            this.Text = "landingForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.landingForm_FormClosing);
            this.Load += new System.EventHandler(this.landingForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listNoty;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
    }
}