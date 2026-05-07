namespace CodeGenerator_PresentationLayer
{
    partial class fmCodeGenerator
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
            this.components = new System.ComponentModel.Container();
            this.cbDataBaseNames = new System.Windows.Forms.ComboBox();
            this.txtbServerName = new System.Windows.Forms.TextBox();
            this.txtbPassword = new System.Windows.Forms.TextBox();
            this.txtbUserID = new System.Windows.Forms.TextBox();
            this.EnterServerName = new System.Windows.Forms.Label();
            this.EnterUserID = new System.Windows.Forms.Label();
            this.EnterPassword = new System.Windows.Forms.Label();
            this.btGenerate = new System.Windows.Forms.Button();
            this.btConnect = new System.Windows.Forms.Button();
            this.chListBoxFunctions = new System.Windows.Forms.CheckedListBox();
            this.listbTableOrViewNames = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.generateCodeUiForSelectedTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listbColumns = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setSelectedColumnAsParametrForSelectedFacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbTableorViewNames = new System.Windows.Forms.Label();
            this.lbColumns = new System.Windows.Forms.Label();
            this.btClose = new System.Windows.Forms.Button();
            this.btBrowse = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.lbFolderSelectedPath = new System.Windows.Forms.Label();
            this.rbModuleLayer = new System.Windows.Forms.RadioButton();
            this.rbBusinessAndDataAccessLayer = new System.Windows.Forms.RadioButton();
            this.lbAllDataBases = new System.Windows.Forms.Label();
            this.lbFolderSelectedPathBusinessLayerResult = new System.Windows.Forms.Label();
            this.lbFolderSelectedPathBusinessLayer = new System.Windows.Forms.Label();
            this.lbFolderSelectedPathModulesOrDataAccessLayer = new System.Windows.Forms.Label();
            this.btBrowseBusinessLayer = new System.Windows.Forms.Button();
            this.txtbModulesLayerNameSpace = new System.Windows.Forms.TextBox();
            this.lbEnterModulesLayerNameSpace = new System.Windows.Forms.Label();
            this.btReset = new System.Windows.Forms.Button();
            this.txtbTableSingleName = new System.Windows.Forms.TextBox();
            this.lbTableSingleName = new System.Windows.Forms.Label();
            this.btChange = new System.Windows.Forms.Button();
            this.rbTables = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbViews = new System.Windows.Forms.RadioButton();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbDataBaseNames
            // 
            this.cbDataBaseNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDataBaseNames.FormattingEnabled = true;
            this.cbDataBaseNames.Location = new System.Drawing.Point(12, 138);
            this.cbDataBaseNames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cbDataBaseNames.Name = "cbDataBaseNames";
            this.cbDataBaseNames.Size = new System.Drawing.Size(437, 33);
            this.cbDataBaseNames.TabIndex = 5;
            this.cbDataBaseNames.SelectedIndexChanged += new System.EventHandler(this.cbDataBaseNames_SelectedIndexChanged);
            // 
            // txtbServerName
            // 
            this.txtbServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbServerName.Location = new System.Drawing.Point(216, 2);
            this.txtbServerName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtbServerName.Name = "txtbServerName";
            this.txtbServerName.Size = new System.Drawing.Size(129, 28);
            this.txtbServerName.TabIndex = 0;
            // 
            // txtbPassword
            // 
            this.txtbPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbPassword.Location = new System.Drawing.Point(216, 62);
            this.txtbPassword.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtbPassword.Name = "txtbPassword";
            this.txtbPassword.Size = new System.Drawing.Size(129, 28);
            this.txtbPassword.TabIndex = 2;
            this.txtbPassword.UseSystemPasswordChar = true;
            // 
            // txtbUserID
            // 
            this.txtbUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbUserID.Location = new System.Drawing.Point(216, 32);
            this.txtbUserID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtbUserID.Name = "txtbUserID";
            this.txtbUserID.Size = new System.Drawing.Size(129, 28);
            this.txtbUserID.TabIndex = 1;
            // 
            // EnterServerName
            // 
            this.EnterServerName.AutoSize = true;
            this.EnterServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnterServerName.Location = new System.Drawing.Point(12, 5);
            this.EnterServerName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EnterServerName.Name = "EnterServerName";
            this.EnterServerName.Size = new System.Drawing.Size(192, 22);
            this.EnterServerName.TabIndex = 11;
            this.EnterServerName.Text = "Enter Server Name :";
            // 
            // EnterUserID
            // 
            this.EnterUserID.AutoSize = true;
            this.EnterUserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnterUserID.Location = new System.Drawing.Point(12, 35);
            this.EnterUserID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EnterUserID.Name = "EnterUserID";
            this.EnterUserID.Size = new System.Drawing.Size(143, 22);
            this.EnterUserID.TabIndex = 12;
            this.EnterUserID.Text = "Enter User ID :";
            // 
            // EnterPassword
            // 
            this.EnterPassword.AutoSize = true;
            this.EnterPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnterPassword.Location = new System.Drawing.Point(12, 65);
            this.EnterPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.EnterPassword.Name = "EnterPassword";
            this.EnterPassword.Size = new System.Drawing.Size(163, 22);
            this.EnterPassword.TabIndex = 13;
            this.EnterPassword.Text = "Enter Password :";
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
            this.btGenerate.Location = new System.Drawing.Point(903, 730);
            this.btGenerate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btGenerate.Name = "btGenerate";
            this.btGenerate.Size = new System.Drawing.Size(115, 50);
            this.btGenerate.TabIndex = 4;
            this.btGenerate.Text = "Generate";
            this.btGenerate.UseVisualStyleBackColor = false;
            this.btGenerate.Click += new System.EventHandler(this.btGenerate_Click);
            // 
            // btConnect
            // 
            this.btConnect.BackColor = System.Drawing.Color.Silver;
            this.btConnect.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btConnect.FlatAppearance.BorderSize = 2;
            this.btConnect.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btConnect.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btConnect.Location = new System.Drawing.Point(546, 730);
            this.btConnect.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btConnect.Name = "btConnect";
            this.btConnect.Size = new System.Drawing.Size(115, 50);
            this.btConnect.TabIndex = 3;
            this.btConnect.Text = "Connect";
            this.btConnect.UseVisualStyleBackColor = false;
            this.btConnect.Click += new System.EventHandler(this.btConnect_Click);
            // 
            // chListBoxFunctions
            // 
            this.chListBoxFunctions.CheckOnClick = true;
            this.chListBoxFunctions.Enabled = false;
            this.chListBoxFunctions.FormattingEnabled = true;
            this.chListBoxFunctions.Items.AddRange(new object[] {
            "All",
            "Add",
            "Update",
            "Is Exist",
            "Find",
            "Delete",
            "Get All"});
            this.chListBoxFunctions.Location = new System.Drawing.Point(541, 317);
            this.chListBoxFunctions.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.chListBoxFunctions.Name = "chListBoxFunctions";
            this.chListBoxFunctions.Size = new System.Drawing.Size(122, 179);
            this.chListBoxFunctions.TabIndex = 10;
            this.chListBoxFunctions.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.chListBoxFunctions_ItemCheck);
            // 
            // listbTableOrViewNames
            // 
            this.listbTableOrViewNames.ContextMenuStrip = this.contextMenuStrip2;
            this.listbTableOrViewNames.FormattingEnabled = true;
            this.listbTableOrViewNames.HorizontalScrollbar = true;
            this.listbTableOrViewNames.ItemHeight = 25;
            this.listbTableOrViewNames.Location = new System.Drawing.Point(12, 317);
            this.listbTableOrViewNames.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listbTableOrViewNames.Name = "listbTableOrViewNames";
            this.listbTableOrViewNames.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listbTableOrViewNames.Size = new System.Drawing.Size(256, 279);
            this.listbTableOrViewNames.TabIndex = 15;
            this.listbTableOrViewNames.SelectedIndexChanged += new System.EventHandler(this.listbTableNames_SelectedIndexChanged);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generateCodeUiForSelectedTableToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(316, 28);
            this.contextMenuStrip2.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip2_Opening);
            // 
            // generateCodeUiForSelectedTableToolStripMenuItem
            // 
            this.generateCodeUiForSelectedTableToolStripMenuItem.Name = "generateCodeUiForSelectedTableToolStripMenuItem";
            this.generateCodeUiForSelectedTableToolStripMenuItem.Size = new System.Drawing.Size(315, 24);
            this.generateCodeUiForSelectedTableToolStripMenuItem.Text = "Generate code ui for selected table ";
            this.generateCodeUiForSelectedTableToolStripMenuItem.Click += new System.EventHandler(this.generateCodeUiForSelectedTableToolStripMenuItem_Click);
            // 
            // listbColumns
            // 
            this.listbColumns.ContextMenuStrip = this.contextMenuStrip1;
            this.listbColumns.FormattingEnabled = true;
            this.listbColumns.HorizontalScrollbar = true;
            this.listbColumns.ItemHeight = 25;
            this.listbColumns.Location = new System.Drawing.Point(274, 317);
            this.listbColumns.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.listbColumns.Name = "listbColumns";
            this.listbColumns.Size = new System.Drawing.Size(256, 279);
            this.listbColumns.TabIndex = 16;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setSelectedColumnAsParametrForSelectedFacToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(343, 28);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // setSelectedColumnAsParametrForSelectedFacToolStripMenuItem
            // 
            this.setSelectedColumnAsParametrForSelectedFacToolStripMenuItem.Name = "setSelectedColumnAsParametrForSelectedFacToolStripMenuItem";
            this.setSelectedColumnAsParametrForSelectedFacToolStripMenuItem.Size = new System.Drawing.Size(342, 24);
            this.setSelectedColumnAsParametrForSelectedFacToolStripMenuItem.Text = "Set as a parameter for selected function";
            this.setSelectedColumnAsParametrForSelectedFacToolStripMenuItem.Click += new System.EventHandler(this.setSelectedColumnAsParametrForSelectedFacToolStripMenuItem_Click);
            // 
            // lbTableorViewNames
            // 
            this.lbTableorViewNames.AutoSize = true;
            this.lbTableorViewNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTableorViewNames.Location = new System.Drawing.Point(12, 293);
            this.lbTableorViewNames.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTableorViewNames.Name = "lbTableorViewNames";
            this.lbTableorViewNames.Size = new System.Drawing.Size(140, 22);
            this.lbTableorViewNames.TabIndex = 17;
            this.lbTableorViewNames.Text = "Table Names :";
            // 
            // lbColumns
            // 
            this.lbColumns.AutoSize = true;
            this.lbColumns.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbColumns.Location = new System.Drawing.Point(274, 293);
            this.lbColumns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbColumns.Name = "lbColumns";
            this.lbColumns.Size = new System.Drawing.Size(156, 22);
            this.lbColumns.TabIndex = 18;
            this.lbColumns.Text = "Column Names :";
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
            this.btClose.Location = new System.Drawing.Point(308, 730);
            this.btClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btClose.Name = "btClose";
            this.btClose.Size = new System.Drawing.Size(115, 50);
            this.btClose.TabIndex = 19;
            this.btClose.Text = "Close";
            this.btClose.UseVisualStyleBackColor = false;
            this.btClose.Click += new System.EventHandler(this.btClose_Click);
            // 
            // btBrowse
            // 
            this.btBrowse.BackColor = System.Drawing.Color.Silver;
            this.btBrowse.Enabled = false;
            this.btBrowse.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btBrowse.FlatAppearance.BorderSize = 2;
            this.btBrowse.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btBrowse.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowse.Location = new System.Drawing.Point(784, 730);
            this.btBrowse.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btBrowse.Name = "btBrowse";
            this.btBrowse.Size = new System.Drawing.Size(115, 50);
            this.btBrowse.TabIndex = 20;
            this.btBrowse.Text = "Browse M";
            this.btBrowse.UseVisualStyleBackColor = false;
            this.btBrowse.Click += new System.EventHandler(this.btBrowse_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Choose the folder where you want to save the files";
            this.folderBrowserDialog1.SelectedPath = "D:\\Visual Studio 2022 Projects\\CodeGenerator_AppWindowsForms\\CodeGenerator_Presen" +
    "tationLayer";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // lbFolderSelectedPath
            // 
            this.lbFolderSelectedPath.AutoSize = true;
            this.lbFolderSelectedPath.ForeColor = System.Drawing.Color.Red;
            this.lbFolderSelectedPath.Location = new System.Drawing.Point(12, 630);
            this.lbFolderSelectedPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFolderSelectedPath.Name = "lbFolderSelectedPath";
            this.lbFolderSelectedPath.Size = new System.Drawing.Size(48, 25);
            this.lbFolderSelectedPath.TabIndex = 21;
            this.lbFolderSelectedPath.Text = "???";
            this.lbFolderSelectedPath.Visible = false;
            // 
            // rbModuleLayer
            // 
            this.rbModuleLayer.AutoSize = true;
            this.rbModuleLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbModuleLayer.Location = new System.Drawing.Point(671, 317);
            this.rbModuleLayer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbModuleLayer.Name = "rbModuleLayer";
            this.rbModuleLayer.Size = new System.Drawing.Size(151, 26);
            this.rbModuleLayer.TabIndex = 22;
            this.rbModuleLayer.TabStop = true;
            this.rbModuleLayer.Text = "Module Layer";
            this.rbModuleLayer.UseVisualStyleBackColor = true;
            this.rbModuleLayer.CheckedChanged += new System.EventHandler(this.Layers_CheckedChanged);
            // 
            // rbBusinessAndDataAccessLayer
            // 
            this.rbBusinessAndDataAccessLayer.AutoSize = true;
            this.rbBusinessAndDataAccessLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbBusinessAndDataAccessLayer.Location = new System.Drawing.Point(671, 352);
            this.rbBusinessAndDataAccessLayer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.rbBusinessAndDataAccessLayer.Name = "rbBusinessAndDataAccessLayer";
            this.rbBusinessAndDataAccessLayer.Size = new System.Drawing.Size(327, 26);
            this.rbBusinessAndDataAccessLayer.TabIndex = 23;
            this.rbBusinessAndDataAccessLayer.TabStop = true;
            this.rbBusinessAndDataAccessLayer.Text = "Business And Data Access Layer";
            this.rbBusinessAndDataAccessLayer.UseVisualStyleBackColor = true;
            this.rbBusinessAndDataAccessLayer.CheckedChanged += new System.EventHandler(this.Layers_CheckedChanged);
            // 
            // lbAllDataBases
            // 
            this.lbAllDataBases.AutoSize = true;
            this.lbAllDataBases.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAllDataBases.Location = new System.Drawing.Point(12, 114);
            this.lbAllDataBases.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAllDataBases.Name = "lbAllDataBases";
            this.lbAllDataBases.Size = new System.Drawing.Size(154, 22);
            this.lbAllDataBases.TabIndex = 24;
            this.lbAllDataBases.Text = "All Data Bases :";
            // 
            // lbFolderSelectedPathBusinessLayerResult
            // 
            this.lbFolderSelectedPathBusinessLayerResult.AutoSize = true;
            this.lbFolderSelectedPathBusinessLayerResult.ForeColor = System.Drawing.Color.Red;
            this.lbFolderSelectedPathBusinessLayerResult.Location = new System.Drawing.Point(12, 686);
            this.lbFolderSelectedPathBusinessLayerResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFolderSelectedPathBusinessLayerResult.Name = "lbFolderSelectedPathBusinessLayerResult";
            this.lbFolderSelectedPathBusinessLayerResult.Size = new System.Drawing.Size(48, 25);
            this.lbFolderSelectedPathBusinessLayerResult.TabIndex = 25;
            this.lbFolderSelectedPathBusinessLayerResult.Text = "???";
            this.lbFolderSelectedPathBusinessLayerResult.Visible = false;
            // 
            // lbFolderSelectedPathBusinessLayer
            // 
            this.lbFolderSelectedPathBusinessLayer.AutoSize = true;
            this.lbFolderSelectedPathBusinessLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFolderSelectedPathBusinessLayer.ForeColor = System.Drawing.Color.Black;
            this.lbFolderSelectedPathBusinessLayer.Location = new System.Drawing.Point(12, 661);
            this.lbFolderSelectedPathBusinessLayer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFolderSelectedPathBusinessLayer.Name = "lbFolderSelectedPathBusinessLayer";
            this.lbFolderSelectedPathBusinessLayer.Size = new System.Drawing.Size(353, 22);
            this.lbFolderSelectedPathBusinessLayer.TabIndex = 26;
            this.lbFolderSelectedPathBusinessLayer.Text = "Folder Selected Path Business Layer :";
            this.lbFolderSelectedPathBusinessLayer.Visible = false;
            // 
            // lbFolderSelectedPathModulesOrDataAccessLayer
            // 
            this.lbFolderSelectedPathModulesOrDataAccessLayer.AutoSize = true;
            this.lbFolderSelectedPathModulesOrDataAccessLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFolderSelectedPathModulesOrDataAccessLayer.ForeColor = System.Drawing.Color.Black;
            this.lbFolderSelectedPathModulesOrDataAccessLayer.Location = new System.Drawing.Point(12, 605);
            this.lbFolderSelectedPathModulesOrDataAccessLayer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbFolderSelectedPathModulesOrDataAccessLayer.Name = "lbFolderSelectedPathModulesOrDataAccessLayer";
            this.lbFolderSelectedPathModulesOrDataAccessLayer.Size = new System.Drawing.Size(384, 22);
            this.lbFolderSelectedPathModulesOrDataAccessLayer.TabIndex = 27;
            this.lbFolderSelectedPathModulesOrDataAccessLayer.Text = "Folder Selected Path Data Access Layer :";
            this.lbFolderSelectedPathModulesOrDataAccessLayer.Visible = false;
            // 
            // btBrowseBusinessLayer
            // 
            this.btBrowseBusinessLayer.BackColor = System.Drawing.Color.Silver;
            this.btBrowseBusinessLayer.Enabled = false;
            this.btBrowseBusinessLayer.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btBrowseBusinessLayer.FlatAppearance.BorderSize = 2;
            this.btBrowseBusinessLayer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btBrowseBusinessLayer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btBrowseBusinessLayer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btBrowseBusinessLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btBrowseBusinessLayer.Location = new System.Drawing.Point(665, 730);
            this.btBrowseBusinessLayer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btBrowseBusinessLayer.Name = "btBrowseBusinessLayer";
            this.btBrowseBusinessLayer.Size = new System.Drawing.Size(115, 50);
            this.btBrowseBusinessLayer.TabIndex = 28;
            this.btBrowseBusinessLayer.Text = "Browse B";
            this.btBrowseBusinessLayer.UseVisualStyleBackColor = false;
            this.btBrowseBusinessLayer.Click += new System.EventHandler(this.btBrowseBusinessLayer_Click);
            // 
            // txtbModulesLayerNameSpace
            // 
            this.txtbModulesLayerNameSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbModulesLayerNameSpace.Location = new System.Drawing.Point(541, 541);
            this.txtbModulesLayerNameSpace.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtbModulesLayerNameSpace.Name = "txtbModulesLayerNameSpace";
            this.txtbModulesLayerNameSpace.Size = new System.Drawing.Size(437, 28);
            this.txtbModulesLayerNameSpace.TabIndex = 29;
            this.txtbModulesLayerNameSpace.Visible = false;
            // 
            // lbEnterModulesLayerNameSpace
            // 
            this.lbEnterModulesLayerNameSpace.AutoSize = true;
            this.lbEnterModulesLayerNameSpace.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEnterModulesLayerNameSpace.ForeColor = System.Drawing.Color.Black;
            this.lbEnterModulesLayerNameSpace.Location = new System.Drawing.Point(541, 518);
            this.lbEnterModulesLayerNameSpace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbEnterModulesLayerNameSpace.Name = "lbEnterModulesLayerNameSpace";
            this.lbEnterModulesLayerNameSpace.Size = new System.Drawing.Size(319, 22);
            this.lbEnterModulesLayerNameSpace.TabIndex = 30;
            this.lbEnterModulesLayerNameSpace.Text = "Enter Modules Layer NameSpace :";
            this.lbEnterModulesLayerNameSpace.Visible = false;
            // 
            // btReset
            // 
            this.btReset.BackColor = System.Drawing.Color.Silver;
            this.btReset.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btReset.FlatAppearance.BorderSize = 2;
            this.btReset.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.btReset.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.btReset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btReset.Location = new System.Drawing.Point(427, 730);
            this.btReset.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btReset.Name = "btReset";
            this.btReset.Size = new System.Drawing.Size(115, 50);
            this.btReset.TabIndex = 32;
            this.btReset.Text = "Reset";
            this.btReset.UseVisualStyleBackColor = false;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // txtbTableSingleName
            // 
            this.txtbTableSingleName.BackColor = System.Drawing.SystemColors.Window;
            this.txtbTableSingleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbTableSingleName.Location = new System.Drawing.Point(12, 213);
            this.txtbTableSingleName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.txtbTableSingleName.Name = "txtbTableSingleName";
            this.txtbTableSingleName.Size = new System.Drawing.Size(406, 28);
            this.txtbTableSingleName.TabIndex = 33;
            // 
            // lbTableSingleName
            // 
            this.lbTableSingleName.AutoSize = true;
            this.lbTableSingleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTableSingleName.Location = new System.Drawing.Point(12, 188);
            this.lbTableSingleName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTableSingleName.Name = "lbTableSingleName";
            this.lbTableSingleName.Size = new System.Drawing.Size(192, 22);
            this.lbTableSingleName.TabIndex = 34;
            this.lbTableSingleName.Text = "Table Single Name :";
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
            this.btChange.Location = new System.Drawing.Point(424, 207);
            this.btChange.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btChange.Name = "btChange";
            this.btChange.Size = new System.Drawing.Size(91, 40);
            this.btChange.TabIndex = 35;
            this.btChange.Text = "Change";
            this.btChange.UseVisualStyleBackColor = false;
            this.btChange.Click += new System.EventHandler(this.btChange_Click);
            // 
            // rbTables
            // 
            this.rbTables.AutoSize = true;
            this.rbTables.Checked = true;
            this.rbTables.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbTables.Location = new System.Drawing.Point(3, 3);
            this.rbTables.Name = "rbTables";
            this.rbTables.Size = new System.Drawing.Size(92, 26);
            this.rbTables.TabIndex = 36;
            this.rbTables.TabStop = true;
            this.rbTables.Text = "Tables";
            this.rbTables.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbViews);
            this.panel1.Controls.Add(this.rbTables);
            this.panel1.Location = new System.Drawing.Point(12, 259);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 31);
            this.panel1.TabIndex = 37;
            // 
            // rbViews
            // 
            this.rbViews.AutoSize = true;
            this.rbViews.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbViews.Location = new System.Drawing.Point(130, 3);
            this.rbViews.Name = "rbViews";
            this.rbViews.Size = new System.Drawing.Size(84, 26);
            this.rbViews.TabIndex = 37;
            this.rbViews.Text = "Views";
            this.rbViews.UseVisualStyleBackColor = true;
            this.rbViews.CheckedChanged += new System.EventHandler(this.rbViews_Or_rbTables_CheckedChanged);
            // 
            // fmCodeGenerator
            // 
            this.AcceptButton = this.btConnect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 781);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btChange);
            this.Controls.Add(this.lbTableSingleName);
            this.Controls.Add(this.txtbTableSingleName);
            this.Controls.Add(this.btReset);
            this.Controls.Add(this.lbEnterModulesLayerNameSpace);
            this.Controls.Add(this.txtbModulesLayerNameSpace);
            this.Controls.Add(this.btBrowseBusinessLayer);
            this.Controls.Add(this.lbFolderSelectedPathModulesOrDataAccessLayer);
            this.Controls.Add(this.lbFolderSelectedPathBusinessLayer);
            this.Controls.Add(this.lbFolderSelectedPathBusinessLayerResult);
            this.Controls.Add(this.lbAllDataBases);
            this.Controls.Add(this.rbBusinessAndDataAccessLayer);
            this.Controls.Add(this.rbModuleLayer);
            this.Controls.Add(this.lbFolderSelectedPath);
            this.Controls.Add(this.btBrowse);
            this.Controls.Add(this.btClose);
            this.Controls.Add(this.lbColumns);
            this.Controls.Add(this.lbTableorViewNames);
            this.Controls.Add(this.listbColumns);
            this.Controls.Add(this.listbTableOrViewNames);
            this.Controls.Add(this.chListBoxFunctions);
            this.Controls.Add(this.btConnect);
            this.Controls.Add(this.btGenerate);
            this.Controls.Add(this.EnterPassword);
            this.Controls.Add(this.EnterUserID);
            this.Controls.Add(this.EnterServerName);
            this.Controls.Add(this.txtbUserID);
            this.Controls.Add(this.txtbPassword);
            this.Controls.Add(this.txtbServerName);
            this.Controls.Add(this.cbDataBaseNames);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MaximizeBox = false;
            this.Name = "fmCodeGenerator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Code Generator";
            this.Load += new System.EventHandler(this.fmCodeGenerator_Load);
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDataBaseNames;
        private System.Windows.Forms.TextBox txtbServerName;
        private System.Windows.Forms.TextBox txtbPassword;
        private System.Windows.Forms.TextBox txtbUserID;
        private System.Windows.Forms.Label EnterServerName;
        private System.Windows.Forms.Label EnterUserID;
        private System.Windows.Forms.Label EnterPassword;
        private System.Windows.Forms.Button btGenerate;
        private System.Windows.Forms.Button btConnect;
        private System.Windows.Forms.CheckedListBox chListBoxFunctions;
        private System.Windows.Forms.ListBox listbTableOrViewNames;
        private System.Windows.Forms.ListBox listbColumns;
        private System.Windows.Forms.Label lbTableorViewNames;
        private System.Windows.Forms.Label lbColumns;
        private System.Windows.Forms.Button btClose;
        private System.Windows.Forms.Button btBrowse;
        private System.Windows.Forms.Label lbFolderSelectedPath;
        private System.Windows.Forms.RadioButton rbModuleLayer;
        private System.Windows.Forms.RadioButton rbBusinessAndDataAccessLayer;
        private System.Windows.Forms.Label lbAllDataBases;
        private System.Windows.Forms.Label lbFolderSelectedPathBusinessLayerResult;
        private System.Windows.Forms.Label lbFolderSelectedPathBusinessLayer;
        private System.Windows.Forms.Label lbFolderSelectedPathModulesOrDataAccessLayer;
        private System.Windows.Forms.Button btBrowseBusinessLayer;
        private System.Windows.Forms.TextBox txtbModulesLayerNameSpace;
        private System.Windows.Forms.Label lbEnterModulesLayerNameSpace;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setSelectedColumnAsParametrForSelectedFacToolStripMenuItem;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.TextBox txtbTableSingleName;
        private System.Windows.Forms.Label lbTableSingleName;
        private System.Windows.Forms.Button btChange;
        private System.Windows.Forms.RadioButton rbTables;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbViews;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem generateCodeUiForSelectedTableToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

