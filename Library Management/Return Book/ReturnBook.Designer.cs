namespace Library_Management.Return_Book
{
    partial class ReturnBook
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
            this.labelReturn = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelReturnDate = new System.Windows.Forms.Label();
            this.labelText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFine = new System.Windows.Forms.TextBox();
            this.btnCalculateFine = new System.Windows.Forms.Button();
            this.btnReturnBook = new System.Windows.Forms.Button();
            this.comboRetrunDate = new System.Windows.Forms.ComboBox();
            this.checkBoxPayment = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelReturn
            // 
            this.labelReturn.BackColor = System.Drawing.Color.DodgerBlue;
            this.labelReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelReturn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReturn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelReturn.Location = new System.Drawing.Point(12, 9);
            this.labelReturn.Name = "labelReturn";
            this.labelReturn.Size = new System.Drawing.Size(407, 37);
            this.labelReturn.TabIndex = 0;
            this.labelReturn.Text = "Return Book";
            this.labelReturn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(17, 65);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(398, 150);
            this.dataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Member";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Book Name";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Issue Date";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Retrun Date";
            this.Column4.Name = "Column4";
            // 
            // labelReturnDate
            // 
            this.labelReturnDate.AutoSize = true;
            this.labelReturnDate.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReturnDate.Location = new System.Drawing.Point(13, 238);
            this.labelReturnDate.Name = "labelReturnDate";
            this.labelReturnDate.Size = new System.Drawing.Size(94, 20);
            this.labelReturnDate.TabIndex = 2;
            this.labelReturnDate.Text = "Return Date";
            // 
            // labelText
            // 
            this.labelText.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelText.Location = new System.Drawing.Point(13, 284);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(395, 47);
            this.labelText.TabIndex = 4;
            this.labelText.Text = "This book is returned after 5 days. A fine of 100PKR is due.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 366);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Fine Received By";
            // 
            // txtFine
            // 
            this.txtFine.Location = new System.Drawing.Point(139, 366);
            this.txtFine.Multiline = true;
            this.txtFine.Name = "txtFine";
            this.txtFine.Size = new System.Drawing.Size(119, 24);
            this.txtFine.TabIndex = 7;
            // 
            // btnCalculateFine
            // 
            this.btnCalculateFine.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnCalculateFine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalculateFine.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalculateFine.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCalculateFine.Location = new System.Drawing.Point(17, 415);
            this.btnCalculateFine.Name = "btnCalculateFine";
            this.btnCalculateFine.Size = new System.Drawing.Size(187, 27);
            this.btnCalculateFine.TabIndex = 8;
            this.btnCalculateFine.Text = "Calculate Fine";
            this.btnCalculateFine.UseVisualStyleBackColor = false;
            // 
            // btnReturnBook
            // 
            this.btnReturnBook.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnReturnBook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnBook.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnBook.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnReturnBook.Location = new System.Drawing.Point(221, 415);
            this.btnReturnBook.Name = "btnReturnBook";
            this.btnReturnBook.Size = new System.Drawing.Size(187, 27);
            this.btnReturnBook.TabIndex = 9;
            this.btnReturnBook.Text = "Return Book";
            this.btnReturnBook.UseVisualStyleBackColor = false;
            // 
            // comboRetrunDate
            // 
            this.comboRetrunDate.FormattingEnabled = true;
            this.comboRetrunDate.Location = new System.Drawing.Point(17, 261);
            this.comboRetrunDate.Name = "comboRetrunDate";
            this.comboRetrunDate.Size = new System.Drawing.Size(162, 21);
            this.comboRetrunDate.TabIndex = 10;
            // 
            // checkBoxPayment
            // 
            this.checkBoxPayment.AutoSize = true;
            this.checkBoxPayment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.checkBoxPayment.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxPayment.Location = new System.Drawing.Point(17, 334);
            this.checkBoxPayment.Name = "checkBoxPayment";
            this.checkBoxPayment.Size = new System.Drawing.Size(132, 21);
            this.checkBoxPayment.TabIndex = 11;
            this.checkBoxPayment.Text = "Confirm Payment";
            this.checkBoxPayment.UseVisualStyleBackColor = true;
            // 
            // ReturnBook
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(431, 454);
            this.Controls.Add(this.checkBoxPayment);
            this.Controls.Add(this.comboRetrunDate);
            this.Controls.Add(this.btnReturnBook);
            this.Controls.Add(this.btnCalculateFine);
            this.Controls.Add(this.txtFine);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.labelReturnDate);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labelReturn);
            this.Name = "ReturnBook";
            this.Text = "ReturnBook";
            this.Load += new System.EventHandler(this.ReturnBook_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelReturn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label labelReturnDate;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtFine;
        private System.Windows.Forms.Button btnCalculateFine;
        private System.Windows.Forms.Button btnReturnBook;
        private System.Windows.Forms.ComboBox comboRetrunDate;
        private System.Windows.Forms.CheckBox checkBoxPayment;
    }
}