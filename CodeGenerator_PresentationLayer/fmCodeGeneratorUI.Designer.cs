namespace CodeGenerator_PresentationLayer
{
    partial class fmCodeGeneratorUI
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
            this.dgvColumns = new System.Windows.Forms.DataGridView();
            this.colColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colColumnType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colIsAllowNull = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsPrimaryKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colIsForeignKey = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colGenerate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colControl = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.rbUserControl = new System.Windows.Forms.RadioButton();
            this.rbForm = new System.Windows.Forms.RadioButton();
            this.rbBoth = new System.Windows.Forms.RadioButton();
            this.rbOutput = new System.Windows.Forms.RadioButton();
            this.rbInput = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbView = new System.Windows.Forms.RadioButton();
            this.btGenerate = new System.Windows.Forms.Button();
            this.btBrowse = new System.Windows.Forms.Button();
            this.btClose = new System.Windows.Forms.Button();
            this.lbFolderSelectedPath = new System.Windows.Forms.Label();
            this.lbFolderSelectedPathPresentationLayer = new System.Windows.Forms.Label();
            this.lbNumberOfRows = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lbTableSingleName = new System.Windows.Forms.Label();
            this.txtbTableSingleName = new System.Windows.Forms.TextBox();
            this.btChange = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvColumns
            // 
            this.dgvColumns.AllowUserToAddRows = false;
            this.dgvColumns.AllowUserToDeleteRows = false;
            this.dgvColumns.AllowUserToOrderColumns = true;
            this.dgvColumns.AllowUserToResizeRows = false;
            this.dgvColumns.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvColumns.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colColumnName,
            this.colColumnType,
            this.colIsAllowNull,
            this.colIsPrimaryKey,
            this.colIsForeignKey,
            this.colGenerate,
            this.colControl});
            this.dgvColumns.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvColumns.Location = new System.Drawing.Point(-1, 219);
            this.dgvColumns.MultiSelect = false;
            this.dgvColumns.Name = "dgvColumns";
            this.dgvColumns.RowHeadersVisible = false;
            this.dgvColumns.RowHeadersWidth = 51;
            this.dgvColumns.RowTemplate.Height = 24;
            this.dgvColumns.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvColumns.Size = new System.Drawing.Size(1211, 501);
            this.dgvColumns.TabIndex = 0;
            // 
            // colColumnName
            // 
            this.colColumnName.HeaderText = "Column Name";
            this.colColumnName.MinimumWidth = 6;
            this.colColumnName.Name = "colColumnName";
            this.colColumnName.Width = 250;
            // 
            // colColumnType
            // 
            this.colColumnType.HeaderText = "Column Type";
            this.colColumnType.MinimumWidth = 6;
            this.colColumnType.Name = "colColumnType";
            this.colColumnType.ReadOnly = true;
            this.colColumnType.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colColumnType.Width = 180;
            // 
            // colIsAllowNull
            // 
            this.colIsAllowNull.HeaderText = "Is Allow Null";
            this.colIsAllowNull.MinimumWidth = 6;
            this.colIsAllowNull.Name = "colIsAllowNull";
            this.colIsAllowNull.ReadOnly = true;
            this.colIsAllowNull.Width = 150;
            // 
            // colIsPrimaryKey
            // 
            this.colIsPrimaryKey.HeaderText = "Is Primary Key";
            this.colIsPrimaryKey.MinimumWidth = 6;
            this.colIsPrimaryKey.Name = "colIsPrimaryKey";
            this.colIsPrimaryKey.ReadOnly = true;
            this.colIsPrimaryKey.Width = 150;
            // 
            // colIsForeignKey
            // 
            this.colIsForeignKey.HeaderText = "Is Foreign Key";
            this.colIsForeignKey.MinimumWidth = 6;
            this.colIsForeignKey.Name = "colIsForeignKey";
            this.colIsForeignKey.ReadOnly = true;
            this.colIsForeignKey.Width = 150;
            // 
            // colGenerate
            // 
            this.colGenerate.HeaderText = "Generate";
            this.colGenerate.MinimumWidth = 6;
            this.colGenerate.Name = "colGenerate";
            this.colGenerate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colGenerate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colGenerate.Width = 125;
            // 
            // colControl
            // 
            this.colControl.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.colControl.HeaderText = "Control";
            this.colControl.MinimumWidth = 6;
            this.colControl.Name = "colControl";
            this.colControl.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colControl.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.colControl.Width = 200;
            // 
            // rbUserControl
            // 
            this.rbUserControl.AutoSize = true;
            this.rbUserControl.Location = new System.Drawing.Point(342, 13);
            this.rbUserControl.Name = "rbUserControl";
            this.rbUserControl.Size = new System.Drawing.Size(144, 26);
            this.rbUserControl.TabIndex = 1;
            this.rbUserControl.TabStop = true;
            this.rbUserControl.Text = "User Control";
            this.rbUserControl.UseVisualStyleBackColor = true;
            // 
            // rbForm
            // 
            this.rbForm.AutoSize = true;
            this.rbForm.Location = new System.Drawing.Point(258, 13);
            this.rbForm.Name = "rbForm";
            this.rbForm.Size = new System.Drawing.Size(76, 26);
            this.rbForm.TabIndex = 2;
            this.rbForm.TabStop = true;
            this.rbForm.Text = "Form";
            this.rbForm.UseVisualStyleBackColor = true;
            // 
            // rbBoth
            // 
            this.rbBoth.AutoSize = true;
            this.rbBoth.Location = new System.Drawing.Point(181, 13);
            this.rbBoth.Name = "rbBoth";
            this.rbBoth.Size = new System.Drawing.Size(72, 26);
            this.rbBoth.TabIndex = 3;
            this.rbBoth.Text = "Both";
            this.rbBoth.UseVisualStyleBackColor = true;
            this.rbBoth.CheckedChanged += new System.EventHandler(this.rbBoth_CheckedChanged);
            // 
            // rbOutput
            // 
            this.rbOutput.AutoSize = true;
            this.rbOutput.Location = new System.Drawing.Point(84, 13);
            this.rbOutput.Name = "rbOutput";
            this.rbOutput.Size = new System.Drawing.Size(91, 26);
            this.rbOutput.TabIndex = 4;
            this.rbOutput.Text = "Output";
            this.rbOutput.UseVisualStyleBackColor = true;
            this.rbOutput.CheckedChanged += new System.EventHandler(this.rbOutput_Or_rbInput_CheckedChanged);
            // 
            // rbInput
            // 
            this.rbInput.AutoSize = true;
            this.rbInput.Location = new System.Drawing.Point(3, 13);
            this.rbInput.Name = "rbInput";
            this.rbInput.Size = new System.Drawing.Size(75, 26);
            this.rbInput.TabIndex = 5;
            this.rbInput.Text = "Input";
            this.rbInput.UseVisualStyleBackColor = true;
            this.rbInput.CheckedChanged += new System.EventHandler(this.rbOutput_Or_rbInput_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbView);
            this.panel1.Controls.Add(this.rbOutput);
            this.panel1.Controls.Add(this.rbBoth);
            this.panel1.Controls.Add(this.rbInput);
            this.panel1.Location = new System.Drawing.Point(669, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(348, 51);
            this.panel1.TabIndex = 6;
            // 
            // rbView
            // 
            this.rbView.AutoSize = true;
            this.rbView.Location = new System.Drawing.Point(259, 13);
            this.rbView.Name = "rbView";
            this.rbView.Size = new System.Drawing.Size(74, 26);
            this.rbView.TabIndex = 6;
            this.rbView.Text = "View";
            this.rbView.UseVisualStyleBackColor = true;
            this.rbView.CheckedChanged += new System.EventHandler(this.rbView_CheckedChanged);
            // 
            // btGenerate
            // 
            this.btGenerate.BackColor = System.Drawing.Color.Silver;
            this.btGenerate.Enabled = false;
            this.btGenerate.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btGenerate.FlatAppearance.BorderSize = 2;
            this.btGenerate.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btGenerate.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btGenerate.Location = new System.Drawing.Point(1095, 728);
            this.btGenerate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btGenerate.Name = "btGenerate";
            this.btGenerate.Size = new System.Drawing.Size(115, 50);
            this.btGenerate.TabIndex = 7;
            this.btGenerate.Text = "Generate";
            this.btGenerate.UseVisualStyleBackColor = false;
            this.btGenerate.Click += new System.EventHandler(this.btGenerate_Click);
            // 
            // btBrowse
            // 
            this.btBrowse.BackColor = System.Drawing.Color.Silver;
            this.btBrowse.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btBrowse.FlatAppearance.BorderSize = 2;
            this.btBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowse.Location = new System.Drawing.Point(977, 728);
            this.btBrowse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(115, 50);
            this.btBrowse.TabIndex = 21;
            this.btBrowse.Text = "Browse";
            this.btBrowse.UseVisualStyleBackColor = false;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // btClose
            // 
            this.btClose.BackColor = System.Drawing.Color.Silver;
            this.btClose.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btClose.FlatAppearance.BorderSize = 2;
            this.btClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btClose.Location = new System.Drawing.Point(859, 728);
            this.btClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(115, 50);
            this.btClose.TabIndex = 22;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // lbFolderSelectedPath
            // 
            this.lbFolderSelectedPath.AutoSize = true;
            this.lbFolderSelectedPath.ForeColor = System.Drawing.Color.Red;
            this.lbFolderSelectedPath.Location = new System.Drawing.Point(13, 105);
            this.lbFolderSelectedPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFolderSelectedPath.Name = "lbFolderSelectedPath";
            this.lbFolderSelectedPath.Size = new System.Drawing.Size(43, 22);
            this.lbFolderSelectedPath.TabIndex = 23;
            this.lbFolderSelectedPath.Text = "???";
            // 
            // lbFolderSelectedPathPresentationLayer
            // 
            this.lbFolderSelectedPathPresentationLayer.AutoSize = true;
            this.lbFolderSelectedPathPresentationLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFolderSelectedPathPresentationLayer.ForeColor = System.Drawing.Color.Black;
            this.lbFolderSelectedPathPresentationLayer.Location = new System.Drawing.Point(13, 82);
            this.lbFolderSelectedPathPresentationLayer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFolderSelectedPathPresentationLayer.Name = "lbFolderSelectedPathPresentationLayer";
            this.lbFolderSelectedPathPresentationLayer.Size = new System.Drawing.Size(362, 20);
            this.lbFolderSelectedPathPresentationLayer.TabIndex = 28;
            this.lbFolderSelectedPathPresentationLayer.Text = "Folder Selected Path Presentation Layer :";
            // 
            // lbNumberOfRows
            // 
            this.lbNumberOfRows.AutoSize = true;
            this.lbNumberOfRows.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumberOfRows.ForeColor = System.Drawing.Color.Red;
            this.lbNumberOfRows.Location = new System.Drawing.Point(-1, 723);
            this.lbNumberOfRows.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNumberOfRows.Name = "lbNumberOfRows";
            this.lbNumberOfRows.Size = new System.Drawing.Size(168, 20);
            this.lbNumberOfRows.TabIndex = 29;
            this.lbNumberOfRows.Text = "# Number Of Rows";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // lbTableSingleName
            // 
            this.lbTableSingleName.AutoSize = true;
            this.lbTableSingleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTableSingleName.Location = new System.Drawing.Point(12, 154);
            this.lbTableSingleName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTableSingleName.Name = "lbTableSingleName";
            this.lbTableSingleName.Size = new System.Drawing.Size(192, 22);
            this.lbTableSingleName.TabIndex = 36;
            this.lbTableSingleName.Text = "Table Single Name :";
            // 
            // txtbTableSingleName
            // 
            this.txtbTableSingleName.BackColor = System.Drawing.SystemColors.Window;
            this.txtbTableSingleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbTableSingleName.Location = new System.Drawing.Point(12, 179);
            this.txtbTableSingleName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtbTableSingleName.Name = "txtbTableSingleName";
            this.txtbTableSingleName.Size = new System.Drawing.Size(406, 28);
            this.txtbTableSingleName.TabIndex = 35;
            // 
            // btChange
            // 
            this.btChange.BackColor = System.Drawing.Color.Silver;
            this.btChange.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btChange.FlatAppearance.BorderSize = 2;
            this.btChange.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btChange.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btChange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btChange.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btChange.Location = new System.Drawing.Point(426, 173);
            this.btChange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btChange.Name = "btChange";
            this.btChange.Size = new System.Drawing.Size(91, 40);
            this.btChange.TabIndex = 37;
            this.btChange.Text = "Change";
            this.btChange.UseVisualStyleBackColor = false;
            this.btChange.Click += new System.EventHandler(this.btChange_Click);
            // 
            // fmCodeGeneratorUI
            // 
            this.AcceptButton = this.btBrowse;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1212, 781);
            this.Controls.Add(this.btChange);
            this.Controls.Add(this.lbTableSingleName);
            this.Controls.Add(this.txtbTableSingleName);
            this.Controls.Add(this.lbNumberOfRows);
            this.Controls.Add(this.lbFolderSelectedPathPresentationLayer);
            this.Controls.Add(this.lbFolderSelectedPath);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.btGenerate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rbForm);
            this.Controls.Add(this.rbUserControl);
            this.Controls.Add(this.dgvColumns);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "fmCodeGeneratorUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Generator UI";
            ((System.ComponentModel.ISupportInitialize)(this.dgvColumns)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvColumns;
        private System.Windows.Forms.RadioButton rbUserControl;
        private System.Windows.Forms.RadioButton rbForm;
        private System.Windows.Forms.RadioButton rbBoth;
        private System.Windows.Forms.RadioButton rbOutput;
        private System.Windows.Forms.RadioButton rbInput;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btGenerate;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Label lbFolderSelectedPath;
        private System.Windows.Forms.Label lbFolderSelectedPathPresentationLayer;
        private System.Windows.Forms.Label lbNumberOfRows;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label lbTableSingleName;
        private System.Windows.Forms.TextBox txtbTableSingleName;
        private System.Windows.Forms.Button btChange;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColumnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colColumnType;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsAllowNull;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsPrimaryKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colIsForeignKey;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colGenerate;
        private System.Windows.Forms.DataGridViewComboBoxColumn colControl;
        private System.Windows.Forms.RadioButton rbView;
    }
}