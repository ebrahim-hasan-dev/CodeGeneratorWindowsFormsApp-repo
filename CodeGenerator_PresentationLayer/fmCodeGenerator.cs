
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CodeGenerator_Modules;
using CodeGenerator_BusinessLayer;


namespace CodeGenerator_PresentationLayer
{
    public partial class fmCodeGenerator : Form
    {
        class clsTableNameAndSingleName
        {
            public string TableName { get; set; }
            public string TableSingleName { get; set; }
        }

        // ====================================================

        string _ConnectionString { get; set; } = "";
        string _NameSpaceModulesOrDataAccessLayer { get; set; } = "";
        string _NameSpaceBusinessLayer { get; set; } = "";
        string _UpdateParameterName { get; set; } = "";
        string _FindParameterName { get; set; } = "";
        string _ExistParameterName { get; set; } = "";
        string _DeleteParameterName { get; set; } = "";
        bool _SpecialParameter { get; set; }

        List<clsTableNameAndSingleName> _ListOfTableNameAndSingleName = new List<clsTableNameAndSingleName>();

        enum enFunctions : byte { All = 0, Add = 1, Updata = 2, IsExist = 3, Find = 4, Delete = 5, GetAll = 6 };

        // ====================================================


        public fmCodeGenerator()
        {
            InitializeComponent();
        }


        void fmCodeGenerator_Load(object sender, EventArgs e)
        {
            //txtbPassword.Text = "123456";
            //txtbServerName.Text = ".";
            //txtbUserID.Text = "sa";



        }


        void ShowMissingMessage()
        {
            MessageBox.Show("One of the requirements is missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ShowSuccessfullyMessage()
        {
            MessageBox.Show("The code has been generated successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void FillComboBoxDataBaseNames(List<string> ListOfDataBaseNames)
        {
            if (ListOfDataBaseNames.Count > 0)
            {
                cbDataBaseNames.Items.Clear();

                for (short i = 0; i < ListOfDataBaseNames.Count; i++)
                {
                    cbDataBaseNames.Items.Add(ListOfDataBaseNames[i]);
                }

                cbDataBaseNames.SelectedIndex = 0;
            }
            else
            {
                btBrowse.Enabled = false;

                MessageBox.Show($"Not found data bases in this server name {txtbServerName.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void btConnect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtbServerName.Text) && !string.IsNullOrWhiteSpace(txtbPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtbUserID.Text))
            {
                _ConnectionString = $"Server={txtbServerName.Text.Trim()};DataBase=master;User ID ={txtbUserID.Text.Trim()};Password={txtbPassword.Text.Trim()};";

                List<string> ListOfDataBaseNames = null;

                try
                {
                    ListOfDataBaseNames = DataBaseService.GetAllDatabases(_ConnectionString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ListOfDataBaseNames = null;
                }

                if (ListOfDataBaseNames != null)
                {
                    btBrowse.Enabled = true;
                    this.AcceptButton = btBrowse;

                    FillComboBoxDataBaseNames(ListOfDataBaseNames);
                }
            }
            else
            {
                ShowMissingMessage();
            }
        }

        void FillListOfTableNameAndSingleName(List<string> ListOfTableName)
        {
            if (_ListOfTableNameAndSingleName != null)
            {
                _ListOfTableNameAndSingleName.Clear();
            }
            else
            {
                _ListOfTableNameAndSingleName = new List<clsTableNameAndSingleName>();
            }

            for (short i = 0; i < ListOfTableName.Count; i++)
            {
                _ListOfTableNameAndSingleName.Add(new clsTableNameAndSingleName { TableName = ListOfTableName[i], TableSingleName = ListOfTableName[i] });
            }
        }

        void FillListBoxTableOrViewNames(List<string> ListOfTableName)
        {
            if (ListOfTableName.Count > 0)
            {
                listbTableOrViewNames.Items.Clear();
                listbTableOrViewNames.Items.Add("All");

                for (short i = 0; i < ListOfTableName.Count; i++)
                {
                    listbTableOrViewNames.Items.Add(ListOfTableName[i]);
                }

                listbTableOrViewNames.SelectedIndex = 0;

                FillListOfTableNameAndSingleName(ListOfTableName);
            }
            else
            {
                MessageBox.Show($"Not found tables in this data base {cbDataBaseNames.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        List<string> GetListOfTableOrViewNames(string ConnectionString)
        {
            List<string> ListOfTableOrViewNames = null;

            try
            {
                if (rbTables.Checked)
                {
                    ListOfTableOrViewNames = TableService.GetAllTableNames(ConnectionString);
                }
                else if (rbViews.Checked)
                {
                    ListOfTableOrViewNames = ViewService.GetAllViewNames(ConnectionString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ListOfTableOrViewNames = null;
            }

            return ListOfTableOrViewNames;
        }

        void cbDataBaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ConnectionString = $"Server={txtbServerName.Text.Trim()};DataBase={cbDataBaseNames.Text};User ID ={txtbUserID.Text.Trim()};Password={txtbPassword.Text.Trim()};";

            List<string> ListOfTableOrViewNames = GetListOfTableOrViewNames(_ConnectionString);

            if (ListOfTableOrViewNames != null)
            {
                FillListBoxTableOrViewNames(ListOfTableOrViewNames);
            }

            txtbModulesLayerNameSpace.Clear();
        }

        void FillListBoxColumnNames(List<clsColumnDataModulesLayer> ListOfColumnNames)
        {
            if (ListOfColumnNames.Count > 0)
            {
                listbColumns.Items.Clear();

                for (short i = 0; i < ListOfColumnNames.Count; i++)
                {
                    listbColumns.Items.Add(ListOfColumnNames[i].Name);
                }

                listbColumns.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Not found columns in selected table", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void ShowTableSingleName(string TableName)
        {
            if (_ListOfTableNameAndSingleName != null)
            {
                short Index = (short)_ListOfTableNameAndSingleName.FindIndex(x => x.TableName == TableName);

                if (Index != -1)
                {
                    txtbTableSingleName.Text = _ListOfTableNameAndSingleName[Index].TableSingleName;
                }
            }
        }

        List<clsColumnDataModulesLayer> GetListOfColumns(string TableName, string ConnectionString, bool IsGenerateModulesLayer)
        {
            List<clsColumnDataModulesLayer> ListOfColumns = null;

            try
            {
                ListOfColumns = ColumnService.GetAllColumns(TableName, ConnectionString, IsGenerateModulesLayer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ListOfColumns = null;
            }

            return ListOfColumns;
        }

        void listbTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listbTableOrViewNames.Items.Count > 0)
            {
                listbColumns.Items.Clear();

                if (listbTableOrViewNames.SelectedIndex > 0)
                {
                    ShowTableSingleName(listbTableOrViewNames.SelectedItem.ToString());

                    List<clsColumnDataModulesLayer> ListOfColumns = GetListOfColumns(listbTableOrViewNames.SelectedItem.ToString(), _ConnectionString, rbModuleLayer.Checked);

                    if (ListOfColumns != null)
                    {
                        FillListBoxColumnNames(ListOfColumns);
                    }

                    txtbTableSingleName.Focus();
                }
            }

            txtbTableSingleName.BackColor = Color.White;
        }

        void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void SaveLastSelectedPath(string SelectedPath)
        {
            if (rbModuleLayer.Checked)
            {
                Properties.Settings.Default.LastSeectedPathModulesLayer = SelectedPath;
            }
            else if (rbBusinessAndDataAccessLayer.Checked)
            {
                Properties.Settings.Default.LastSelectedPathDataAccessLayer = SelectedPath;
            }

            Properties.Settings.Default.Save();
        }

        void SetLastSelectedPathBusinessAndDataAccessLayer()
        {
            if (rbModuleLayer.Checked)
            {
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LastSeectedPathModulesLayer) &&
                    Directory.Exists(Properties.Settings.Default.LastSeectedPathModulesLayer))
                {
                    folderBrowserDialog1.SelectedPath = Properties.Settings.Default.LastSeectedPathModulesLayer;
                }
            }
            else if (rbBusinessAndDataAccessLayer.Checked)
            {
                if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LastSelectedPathDataAccessLayer) &&
                    Directory.Exists(Properties.Settings.Default.LastSelectedPathDataAccessLayer))
                {
                    folderBrowserDialog1.SelectedPath = Properties.Settings.Default.LastSelectedPathDataAccessLayer;
                }
            }
        }

        void btBrowse_Click(object sender, EventArgs e)
        {
            SetLastSelectedPathBusinessAndDataAccessLayer();

            if (rbModuleLayer.Checked || rbBusinessAndDataAccessLayer.Checked)
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    _NameSpaceModulesOrDataAccessLayer = Path.GetFileNameWithoutExtension(folderBrowserDialog1.SelectedPath);

                    lbFolderSelectedPath.Text = folderBrowserDialog1.SelectedPath;

                    SaveLastSelectedPath(folderBrowserDialog1.SelectedPath);

                    btGenerate.Enabled = true;
                    this.AcceptButton = btGenerate;
                }
            }
            else
            {
                ShowMustChoicelayerMessage();
            }
        }

        public static void CaseIsReadOnlyHadle(string FilePath)
        {
            FileInfo fileInfo = new FileInfo(FilePath);

            if (fileInfo.Exists && fileInfo.IsReadOnly)
            {
                fileInfo.IsReadOnly = false;
            }
        }

        bool CreateNewFileAndWrite(string FilePath, RuntimeTextTemplateModulesLayer ModuleLayerTemplate)
        {
            string ClassCode = ModuleLayerTemplate.TransformText();

            CaseIsReadOnlyHadle(FilePath);

            try
            {
                File.WriteAllText(FilePath, ClassCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        string GetTableSingleName(string TableName)
        {
            if (_ListOfTableNameAndSingleName != null)
            {
                short Index = (short)_ListOfTableNameAndSingleName.FindIndex(x => x.TableName == TableName);

                if (Index != -1)
                {
                    return _ListOfTableNameAndSingleName[Index].TableSingleName;
                }
            }

            return TableName;
        }

        bool GenerateModulesLayerHelper(string TableName, List<clsColumnDataModulesLayer> ListOfColumns)
        {
            RuntimeTextTemplateModulesLayer ModuleLayerTemplate = new RuntimeTextTemplateModulesLayer();

            ModuleLayerTemplate.NamespaceName = _NameSpaceModulesOrDataAccessLayer;
            ModuleLayerTemplate.TableSingleName = GetTableSingleName(TableName);
            ModuleLayerTemplate.Columns = ListOfColumns;

            string FilePath = Path.Combine(lbFolderSelectedPath.Text, "cls" + ModuleLayerTemplate.TableSingleName + ".cs");

            if (File.Exists(FilePath))
            {
                if (MessageBox.Show("Are you sure you want to clear the old one and write a new one with same name?", "This file already exists", 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                {
                    return false;
                }
            }

            return CreateNewFileAndWrite(FilePath, ModuleLayerTemplate);
        }

        public static bool HasWritePermission(string FolderSelectedPath)
        {
            try
            {
                string TestFile = Path.Combine(FolderSelectedPath, Guid.NewGuid().ToString() + ".txt");
                File.WriteAllText(TestFile, "test");
                File.Delete(TestFile);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        bool GenerateModulesLayer()
        {
            List<bool> ListOfResults = new List<bool>();

            CheckFromFolderPath(lbFolderSelectedPath.Text);

            txtbModulesLayerNameSpace.Text = Path.GetFileNameWithoutExtension(lbFolderSelectedPath.Text);

            if (HasWritePermission(lbFolderSelectedPath.Text))
            {
                if (listbTableOrViewNames.SelectedIndex == 0)
                {
                    for (short i = 1; i < listbTableOrViewNames.Items.Count; i++)
                    {
                        List<clsColumnDataModulesLayer> ListOfColumns = GetListOfColumns(listbTableOrViewNames.Items[i].ToString(), _ConnectionString, rbModuleLayer.Checked);

                        if (ListOfColumns != null)
                        {
                            ListOfResults.Add(GenerateModulesLayerHelper(listbTableOrViewNames.Items[i].ToString(), ListOfColumns));
                        }
                    }
                }
                else
                {
                    for (short i = 0; i < listbTableOrViewNames.SelectedItems.Count; i++)
                    {
                        List<clsColumnDataModulesLayer> ListOfColumns = GetListOfColumns(listbTableOrViewNames.SelectedItems[i].ToString(), _ConnectionString, rbModuleLayer.Checked);

                        if (ListOfColumns != null)
                        {
                            ListOfResults.Add(GenerateModulesLayerHelper(listbTableOrViewNames.SelectedItems[i].ToString(), ListOfColumns));
                        }
                    }
                }
            }
            else
            {
                return false;
            }

            return !ListOfResults.Exists(x => x == false);
        }

        public static void CheckFromFolderPath(string FolderSelectedPath)
        {
            if (!Directory.Exists(FolderSelectedPath))
            {
                Directory.CreateDirectory(FolderSelectedPath);
            }
        }

        bool IsBoolean(string ParameterName, List<clsColumnDataDataAccessLayer> Columns)
        {
            return Columns.Find(x => x.Name == ParameterName).Type == "bool";
        }

        string SelectCorrectParameterHelper(RuntimeTextTemplateDataAccessLayer DataAccessLayerTemplate, string PrimaryKeyName, string MethodParameterName)
        {
            string CorrectParameter = "";

            if (!string.IsNullOrWhiteSpace(MethodParameterName) && !IsBoolean(MethodParameterName, DataAccessLayerTemplate.Columns))
            {
                CorrectParameter = MethodParameterName;
            }
            else
            {
                CorrectParameter = PrimaryKeyName;
            }

            return CorrectParameter;
        }

        bool SelectCorrectParameter(RuntimeTextTemplateDataAccessLayer DataAccessLayerTemplate, string TableName)
        {
            string PrimaryKeyName = "";

            try
            {
                DataAccessLayerTemplate.Columns = ColumnService.GetAllColumnsDataAccessLayer(TableName, ref PrimaryKeyName, _ConnectionString, rbModuleLayer.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                DataAccessLayerTemplate.Columns = null;
            }

            if (DataAccessLayerTemplate.Columns != null)
            {
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Updata) == true)
                {
                    DataAccessLayerTemplate.IncludeUpdate = true;

                    DataAccessLayerTemplate.UpdateParameterName = SelectCorrectParameterHelper(DataAccessLayerTemplate, PrimaryKeyName, _UpdateParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.IsExist) == true)
                {
                    DataAccessLayerTemplate.IncludeExist = true;

                    DataAccessLayerTemplate.ExistParameterName = SelectCorrectParameterHelper(DataAccessLayerTemplate, PrimaryKeyName, _ExistParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Find) == true)
                {
                    DataAccessLayerTemplate.IncludeFind = true;

                    DataAccessLayerTemplate.FindParameterName = SelectCorrectParameterHelper(DataAccessLayerTemplate, PrimaryKeyName, _FindParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Delete) == true)
                {
                    DataAccessLayerTemplate.IncludeDelete = true;

                    DataAccessLayerTemplate.DeleteParameterName = SelectCorrectParameterHelper(DataAccessLayerTemplate, PrimaryKeyName, _DeleteParameterName);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        RuntimeTextTemplateDataAccessLayerAppendMethods SetTamplateInfo(RuntimeTextTemplateDataAccessLayer DataAccessLayerTemplate)
        {
            RuntimeTextTemplateDataAccessLayerAppendMethods RuntimeTextTemplateDataAccessLayerAppendMethods = new RuntimeTextTemplateDataAccessLayerAppendMethods();

            RuntimeTextTemplateDataAccessLayerAppendMethods.IncludeAdd = DataAccessLayerTemplate.IncludeAdd;
            RuntimeTextTemplateDataAccessLayerAppendMethods.IncludeFind = DataAccessLayerTemplate.IncludeFind;
            RuntimeTextTemplateDataAccessLayerAppendMethods.IncludeUpdate = DataAccessLayerTemplate.IncludeUpdate;
            RuntimeTextTemplateDataAccessLayerAppendMethods.IncludeExist = DataAccessLayerTemplate.IncludeExist;
            RuntimeTextTemplateDataAccessLayerAppendMethods.IncludeGetAll = DataAccessLayerTemplate.IncludeGetAll;
            RuntimeTextTemplateDataAccessLayerAppendMethods.IncludeDelete = DataAccessLayerTemplate.IncludeDelete;

            RuntimeTextTemplateDataAccessLayerAppendMethods.UpdateParameterName = DataAccessLayerTemplate.UpdateParameterName;
            RuntimeTextTemplateDataAccessLayerAppendMethods.DeleteParameterName = DataAccessLayerTemplate.DeleteParameterName;
            RuntimeTextTemplateDataAccessLayerAppendMethods.FindParameterName = DataAccessLayerTemplate.FindParameterName;
            RuntimeTextTemplateDataAccessLayerAppendMethods.ExistParameterName = DataAccessLayerTemplate.ExistParameterName;

            RuntimeTextTemplateDataAccessLayerAppendMethods.TableName = DataAccessLayerTemplate.TableName;
            RuntimeTextTemplateDataAccessLayerAppendMethods.TableSingleName = DataAccessLayerTemplate.TableSingleName;
            RuntimeTextTemplateDataAccessLayerAppendMethods.ModulesLayerNameSpace = DataAccessLayerTemplate.ModulesLayerNameSpace;
            RuntimeTextTemplateDataAccessLayerAppendMethods.Columns = DataAccessLayerTemplate.Columns;
            RuntimeTextTemplateDataAccessLayerAppendMethods.NamespaceName = DataAccessLayerTemplate.NamespaceName;

            return RuntimeTextTemplateDataAccessLayerAppendMethods;
        }

        void ShowMarkNotExistMessage()
        {
            MessageBox.Show("The file already exists, but the tag for the extension has been removed\n\nplease write this comment into the class in the file\n\"// [EXTRA_METHODS_HERE]\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        bool CreateNewFileAndWrite(string FilePath, RuntimeTextTemplateDataAccessLayer DataAccessLayerTemplate)
        {
            string ClassCode = DataAccessLayerTemplate.TransformText();

            CaseIsReadOnlyHadle(FilePath);

            try
            {
                File.WriteAllText(FilePath, ClassCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        bool WriteListIntoFile(string FilePath, List<string> Lines)
        {
            CaseIsReadOnlyHadle(FilePath);

            try
            {
                File.WriteAllLines(FilePath, Lines);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        List<string> ReadFileAndSetItIntoList(string FilePath)
        {
            List<string> Lines;

            try
            {
                Lines = File.ReadAllLines(FilePath).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Lines = new List<string>();
            }

            return Lines;
        }

        bool FileExistHandle(string FilePath, string TableName, RuntimeTextTemplateDataAccessLayer DataAccessLayerTemplate)
        {
            List<string> Lines = ReadFileAndSetItIntoList(FilePath);

            int Index = Lines.FindIndex(line => line.Contains("// [EXTRA_METHODS_HERE]"));

            if (Index != -1)
            {
                if (Lines.Exists(x => x.Contains(" class ")))
                {
                    RuntimeTextTemplateDataAccessLayerAppendMethods RuntimeTextTemplateDataAccessLayerAppendMethods = SetTamplateInfo(DataAccessLayerTemplate);

                    string MethodsCode = RuntimeTextTemplateDataAccessLayerAppendMethods.TransformText();

                    Lines.RemoveRange(Index - 1, 2);

                    Lines.Insert(Index, MethodsCode);

                    return WriteListIntoFile(FilePath, Lines);
                }
                else
                {
                    return CreateNewFileAndWrite(FilePath, DataAccessLayerTemplate);
                }
            }
            else
            {
                if (Lines.Count == 0)
                {
                    return CreateNewFileAndWrite(FilePath, DataAccessLayerTemplate);
                }
                else
                {
                    ShowMarkNotExistMessage();
                }
            }

            return false;
        }

        bool GenerateDataAccessLayerHelper(string TableName)
        {
            RuntimeTextTemplateDataAccessLayer DataAccessLayerTemplate = new RuntimeTextTemplateDataAccessLayer();

            if (SelectCorrectParameter(DataAccessLayerTemplate, TableName) == true)
            {
                DataAccessLayerTemplate.NamespaceName = _NameSpaceModulesOrDataAccessLayer;
                DataAccessLayerTemplate.TableName = TableName;
                DataAccessLayerTemplate.TableSingleName = GetTableSingleName(TableName);
                DataAccessLayerTemplate.ModulesLayerNameSpace = txtbModulesLayerNameSpace.Text;

                DataAccessLayerTemplate.IncludeAdd = chListBoxFunctions.GetItemChecked((byte)enFunctions.Add);
                DataAccessLayerTemplate.IncludeGetAll = chListBoxFunctions.GetItemChecked((byte)enFunctions.GetAll);

                string FilePath = Path.Combine(lbFolderSelectedPath.Text, DataAccessLayerTemplate.TableSingleName + "Repository" + ".cs");

                if (File.Exists(FilePath))
                {
                    return FileExistHandle(FilePath, TableName, DataAccessLayerTemplate);
                }
                else
                {
                    return CreateNewFileAndWrite(FilePath, DataAccessLayerTemplate);
                }
            }

            return false;
        }

        bool GenerateDataAccessLayer()
        {
            List<bool> ListOfResults = new List<bool>();

            CheckFromFolderPath(lbFolderSelectedPath.Text);

            if (HasWritePermission(lbFolderSelectedPath.Text))
            {
                if (listbTableOrViewNames.SelectedIndex == 0)
                {
                    for (short i = 1; i < listbTableOrViewNames.Items.Count; i++)
                    {
                        ListOfResults.Add(GenerateDataAccessLayerHelper(listbTableOrViewNames.Items[i].ToString()));
                    }
                }
                else
                {
                    for (short i = 0; i < listbTableOrViewNames.SelectedItems.Count; i++)
                    {
                        ListOfResults.Add(GenerateDataAccessLayerHelper(listbTableOrViewNames.SelectedItems[i].ToString()));
                    }
                }
            }
            else
            {
                return false;
            }

            return !ListOfResults.Exists(x => x == false);
        }

        bool IsAnyFunctionsChecked()
        {
            return chListBoxFunctions.CheckedItems.Count > 0;
        }

        bool IsBoolean(string ParameterName, List<clsColumnDataBusinessLayer> Columns)
        {
            return Columns.Find(x => x.Name == ParameterName).Type == "bool";
        }

        string SelectCorrectParameterHelper(RuntimeTextTemplateBusinessLayer BusinessLayerTemplate, string PrimaryKeyName, string MethodParameterName)
        {
            string CorrectParameter = "";

            if (!string.IsNullOrWhiteSpace(MethodParameterName) && !IsBoolean(MethodParameterName, BusinessLayerTemplate.Columns))
            {
                CorrectParameter = MethodParameterName;
            }
            else
            {
                CorrectParameter = PrimaryKeyName;
            }

            return CorrectParameter;
        }

        bool SelectCorrectParameter(RuntimeTextTemplateBusinessLayer BusinessLayerTemplate, string TableName)
        {
            string PrimaryKeyName = "";

            try
            {
                BusinessLayerTemplate.Columns = ColumnService.GetAllColumnsBusinessLayer(TableName, ref PrimaryKeyName, _ConnectionString, rbModuleLayer.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                BusinessLayerTemplate.Columns = null;
            }

            if (BusinessLayerTemplate.Columns != null)
            {
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Updata) == true)
                {
                    BusinessLayerTemplate.IncludeUpdate = true;

                    BusinessLayerTemplate.UpdateParameterName = SelectCorrectParameterHelper(BusinessLayerTemplate, PrimaryKeyName, _UpdateParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.IsExist) == true)
                {
                    BusinessLayerTemplate.IncludeExist = true;

                    BusinessLayerTemplate.ExistParameterName = SelectCorrectParameterHelper(BusinessLayerTemplate, PrimaryKeyName, _ExistParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Find) == true)
                {
                    BusinessLayerTemplate.IncludeFind = true;

                    BusinessLayerTemplate.FindParameterName = SelectCorrectParameterHelper(BusinessLayerTemplate, PrimaryKeyName, _FindParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Delete) == true)
                {
                    BusinessLayerTemplate.IncludeDelete = true;

                    BusinessLayerTemplate.DeleteParameterName = SelectCorrectParameterHelper(BusinessLayerTemplate, PrimaryKeyName, _DeleteParameterName);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        RuntimeTextTemplateBusinessLayerAppendMethods SetTamplateInfo(RuntimeTextTemplateBusinessLayer BusinessLayerTemplate)
        {
            RuntimeTextTemplateBusinessLayerAppendMethods RuntimeTextTemplateBusinessLayerAppendMethods = new RuntimeTextTemplateBusinessLayerAppendMethods();

            RuntimeTextTemplateBusinessLayerAppendMethods.IncludeAdd = BusinessLayerTemplate.IncludeAdd;
            RuntimeTextTemplateBusinessLayerAppendMethods.IncludeFind = BusinessLayerTemplate.IncludeFind;
            RuntimeTextTemplateBusinessLayerAppendMethods.IncludeUpdate = BusinessLayerTemplate.IncludeUpdate;
            RuntimeTextTemplateBusinessLayerAppendMethods.IncludeExist = BusinessLayerTemplate.IncludeExist;
            RuntimeTextTemplateBusinessLayerAppendMethods.IncludeGetAll = BusinessLayerTemplate.IncludeGetAll;
            RuntimeTextTemplateBusinessLayerAppendMethods.IncludeDelete = BusinessLayerTemplate.IncludeDelete;

            RuntimeTextTemplateBusinessLayerAppendMethods.UpdateParameterName = BusinessLayerTemplate.UpdateParameterName;
            RuntimeTextTemplateBusinessLayerAppendMethods.DeleteParameterName = BusinessLayerTemplate.DeleteParameterName;
            RuntimeTextTemplateBusinessLayerAppendMethods.FindParameterName = BusinessLayerTemplate.FindParameterName;
            RuntimeTextTemplateBusinessLayerAppendMethods.ExistParameterName = BusinessLayerTemplate.ExistParameterName;

            RuntimeTextTemplateBusinessLayerAppendMethods.TableName = BusinessLayerTemplate.TableName;
            RuntimeTextTemplateBusinessLayerAppendMethods.TableSingleName = BusinessLayerTemplate.TableSingleName;
            RuntimeTextTemplateBusinessLayerAppendMethods.ModulesLayerNameSpace = BusinessLayerTemplate.ModulesLayerNameSpace;
            RuntimeTextTemplateBusinessLayerAppendMethods.DataAccessLayerNameSpace = BusinessLayerTemplate.DataAccessLayerNameSpace;
            RuntimeTextTemplateBusinessLayerAppendMethods.Columns = BusinessLayerTemplate.Columns;
            RuntimeTextTemplateBusinessLayerAppendMethods.NamespaceName = BusinessLayerTemplate.NamespaceName;

            return RuntimeTextTemplateBusinessLayerAppendMethods;
        }

        bool CreateNewFileAndWrite(string FilePath, RuntimeTextTemplateBusinessLayer BusinessLayerTemplate)
        {
            string ClassCode = BusinessLayerTemplate.TransformText();

            CaseIsReadOnlyHadle(FilePath);

            try
            {
                File.WriteAllText(FilePath, ClassCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        bool FileExistHandle(string FilePath, string TableName, RuntimeTextTemplateBusinessLayer BusinessLayerTemplate)
        {
            List<string> Lines = ReadFileAndSetItIntoList(FilePath);

            int Index = Lines.FindIndex(line => line.Contains("// [EXTRA_METHODS_HERE]"));

            if (Index != -1)
            {
                if (Lines.Exists(x => x.Contains(" class ")))
                {
                    RuntimeTextTemplateBusinessLayerAppendMethods RuntimeTextTemplateBusinessLayerAppendMethods = SetTamplateInfo(BusinessLayerTemplate);

                    string MethodsCode = RuntimeTextTemplateBusinessLayerAppendMethods.TransformText();

                    Lines.RemoveRange(Index - 1, 2);

                    Lines.Insert(Index, MethodsCode);

                    return WriteListIntoFile(FilePath, Lines);
                }
                else
                {
                    return CreateNewFileAndWrite(FilePath, BusinessLayerTemplate);
                }
            }
            else
            {
                if (Lines.Count == 0)
                {
                    return CreateNewFileAndWrite(FilePath, BusinessLayerTemplate);
                }
                else
                {
                    ShowMarkNotExistMessage();
                }
            }

            return false;
        }

        bool GenerateBusinessLayerHelper(string TableName)
        {
            RuntimeTextTemplateBusinessLayer BusinessLayerTemplate = new RuntimeTextTemplateBusinessLayer();

            if (SelectCorrectParameter(BusinessLayerTemplate, TableName) == true)
            {
                BusinessLayerTemplate.NamespaceName = _NameSpaceBusinessLayer;
                BusinessLayerTemplate.TableName = TableName;
                BusinessLayerTemplate.TableSingleName = GetTableSingleName(TableName);
                BusinessLayerTemplate.ModulesLayerNameSpace = txtbModulesLayerNameSpace.Text;
                BusinessLayerTemplate.DataAccessLayerNameSpace = _NameSpaceModulesOrDataAccessLayer;

                BusinessLayerTemplate.IncludeAdd = chListBoxFunctions.GetItemChecked((byte)enFunctions.Add);
                BusinessLayerTemplate.IncludeGetAll = chListBoxFunctions.GetItemChecked((byte)enFunctions.GetAll);

                string FilePath = Path.Combine(lbFolderSelectedPathBusinessLayerResult.Text, BusinessLayerTemplate.TableSingleName + "Service" + ".cs");

                if (File.Exists(FilePath))
                {
                    return FileExistHandle(FilePath, TableName, BusinessLayerTemplate);
                }
                else
                {
                    return CreateNewFileAndWrite(FilePath, BusinessLayerTemplate);
                }
            }

            return false;
        }

        bool GenerateBusinessLayer()
        {
            List<bool> ListOfResults = new List<bool>();

            CheckFromFolderPath(lbFolderSelectedPathBusinessLayerResult.Text);

            if (HasWritePermission(lbFolderSelectedPathBusinessLayerResult.Text))
            {
                if (listbTableOrViewNames.SelectedIndex == 0)
                {
                    for (short i = 1; i < listbTableOrViewNames.Items.Count; i++)
                    {
                        ListOfResults.Add(GenerateBusinessLayerHelper(listbTableOrViewNames.Items[i].ToString()));
                    }
                }
                else
                {
                    for (short i = 0; i < listbTableOrViewNames.SelectedItems.Count; i++)
                    {
                        ListOfResults.Add(GenerateBusinessLayerHelper(listbTableOrViewNames.SelectedItems[i].ToString()));
                    }
                }
            }
            else
            {
                return false;
            }

            return !ListOfResults.Exists(x => x == false);
        }

        void ShowFaildMessage()
        {
            MessageBox.Show("One of the files was not successfully produced.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void PerformCorrectGenerateLayer()
        {
            if (rbModuleLayer.Checked)
            {
                if (GenerateModulesLayer())
                {
                    ShowSuccessfullyMessage();
                }
                else
                {
                    ShowFaildMessage();
                }
            }
            else if (rbBusinessAndDataAccessLayer.Checked)
            {
                if (IsAnyFunctionsChecked())
                {
                    if (lbFolderSelectedPathBusinessLayerResult.Text != "???" &&
                        !string.IsNullOrWhiteSpace(lbFolderSelectedPathBusinessLayerResult.Text))
                    {
                        if (!string.IsNullOrWhiteSpace(txtbModulesLayerNameSpace.Text))
                        {
                            bool ResultDataAccessLayer = GenerateDataAccessLayer();
                            bool ResultBusinessLayer = GenerateBusinessLayer();

                            if (!ResultDataAccessLayer || !ResultBusinessLayer)
                            {
                                ShowFaildMessage();
                            }
                            else
                            {
                                ShowSuccessfullyMessage();

                                chListBoxFunctions.SetItemChecked((byte)enFunctions.All, false);
                                SetCheckToAllItems(false);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You must enter modules layer namespace", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtbModulesLayerNameSpace.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("You must select folder path to business layer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        this.AcceptButton = btBrowseBusinessLayer;
                        btBrowseBusinessLayer.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("You must choice one or more function(s)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    chListBoxFunctions.Focus();
                }
            }
            else
            {
                ShowMustChoicelayerMessage();
            }
        }

        void ShowMustChoicelayerMessage()
        {
            MessageBox.Show("You must choice a layer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void btGenerate_Click(object sender, EventArgs e)
        {
            if (_SpecialParameter == true)
            {
                if (listbTableOrViewNames.SelectedItems.Count > 1)
                {
                    MessageBox.Show("You must select one table only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    _SpecialParameter = false;
                }
            }

            if (!string.IsNullOrWhiteSpace(_NameSpaceModulesOrDataAccessLayer) && listbTableOrViewNames.Items.Count > 0 &&
                !string.IsNullOrWhiteSpace(lbFolderSelectedPath.Text) &&
                lbFolderSelectedPath.Text != "???")
            {
                PerformCorrectGenerateLayer();
            }
            else
            {
                ShowMissingMessage();
            }
        }

        void Layers_CheckedChanged(object sender, EventArgs e)
        {
            lbFolderSelectedPath.Visible = true;
            lbFolderSelectedPathModulesOrDataAccessLayer.Visible = true;

            lbFolderSelectedPath.Text = "???";

            if (rbModuleLayer.Checked)
            {
                chListBoxFunctions.Enabled = false;

                lbFolderSelectedPathBusinessLayer.Visible = false;
                lbFolderSelectedPathBusinessLayerResult.Visible = false;
                btBrowseBusinessLayer.Enabled = false;
                btBrowse.Text = "Browse M";
                lbFolderSelectedPathModulesOrDataAccessLayer.Text = "Folder Selected Path Modules Layer :";
                lbEnterModulesLayerNameSpace.Visible = false;
                txtbModulesLayerNameSpace.Visible = false;
            }
            else if (rbBusinessAndDataAccessLayer.Checked)
            {
                if (rbTables.Checked)
                {
                    chListBoxFunctions.Enabled = true;
                }

                lbFolderSelectedPathBusinessLayer.Visible = true;
                lbFolderSelectedPathBusinessLayerResult.Visible = true;

                btBrowseBusinessLayer.Enabled = true;
                btBrowse.Text = "Browse D";
                lbFolderSelectedPathModulesOrDataAccessLayer.Text = "Folder Selected Path Data Access Layer :";
                lbEnterModulesLayerNameSpace.Visible = true;
                txtbModulesLayerNameSpace.Visible = true;
            }
        }

        void SetLastSelectedPathToDialogBusinessLayer()
        {
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LastSelectedPathBusinessLayer) &&
                Directory.Exists(Properties.Settings.Default.LastSelectedPathBusinessLayer))
            {
                folderBrowserDialog1.SelectedPath = Properties.Settings.Default.LastSelectedPathBusinessLayer;
            }
        }

        void btBrowseBusinessLayer_Click(object sender, EventArgs e)
        {
            SetLastSelectedPathToDialogBusinessLayer();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                _NameSpaceBusinessLayer = Path.GetFileNameWithoutExtension(folderBrowserDialog1.SelectedPath);

                lbFolderSelectedPathBusinessLayerResult.Text = folderBrowserDialog1.SelectedPath;

                Properties.Settings.Default.LastSelectedPathBusinessLayer = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();

                btGenerate.Enabled = true;
                this.AcceptButton = btGenerate;
            }
        }

        void SetCheckToAllItems(bool Check)
        {
            for (byte i = 1; i < chListBoxFunctions.Items.Count; i++)
            {
                chListBoxFunctions.SetItemChecked(i, Check);
            }
        }

        void ResetParameterName(ItemCheckEventArgs e)
        {
            if (e.Index == (byte)enFunctions.Updata && e.CurrentValue == CheckState.Checked)
            {
                _UpdateParameterName = "";
            }
            else if (e.Index == (byte)enFunctions.IsExist && e.CurrentValue == CheckState.Checked)
            {
                _ExistParameterName = "";
            }
            else if (e.Index == (byte)enFunctions.Find && e.CurrentValue == CheckState.Checked)
            {
                _FindParameterName = "";
            }
            else if (e.Index == (byte)enFunctions.Delete && e.CurrentValue == CheckState.Checked)
            {
                _DeleteParameterName = "";
            }
        }

        void chListBoxFunctions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.Index == (byte)enFunctions.All)
            {
                if (e.CurrentValue == CheckState.Unchecked)
                {
                    SetCheckToAllItems(true);
                }
            }
            else
            {
                chListBoxFunctions.SetItemChecked((byte)enFunctions.All, false);
            }

            ResetParameterName(e);
        }

        void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (listbTableOrViewNames.Items.Count > 0 && listbTableOrViewNames.SelectedIndex > 0 && listbTableOrViewNames.SelectedItems.Count == 1 &&
               chListBoxFunctions.SelectedIndex > (byte)enFunctions.Add && chListBoxFunctions.SelectedIndex < (byte)enFunctions.GetAll &&
               chListBoxFunctions.GetItemChecked(chListBoxFunctions.SelectedIndex) == true &&
               listbColumns.Items.Count > 0 && listbColumns.SelectedItems.Count == 1)
            {
                setSelectedColumnAsParametrForSelectedFacToolStripMenuItem.Enabled = true;
            }
            else
            {
                setSelectedColumnAsParametrForSelectedFacToolStripMenuItem.Enabled = false;
            }
        }

        void setSelectedColumnAsParametrForSelectedFacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _SpecialParameter = true;

            if (chListBoxFunctions.SelectedIndex == (byte)enFunctions.Updata)
            {
                _UpdateParameterName = listbColumns.SelectedItem.ToString();
            }
            else if (chListBoxFunctions.SelectedIndex == (byte)enFunctions.IsExist)
            {
                _ExistParameterName = listbColumns.SelectedItem.ToString();
            }
            else if (chListBoxFunctions.SelectedIndex == (byte)enFunctions.Find)
            {
                _FindParameterName = listbColumns.SelectedItem.ToString();
            }
            else if (chListBoxFunctions.SelectedIndex == (byte)enFunctions.Delete)
            {
                _DeleteParameterName = listbColumns.SelectedItem.ToString();
            }
        }

        void btReset_Click(object sender, EventArgs e)
        {
            _ConnectionString = "";
            _NameSpaceModulesOrDataAccessLayer = "";
            _NameSpaceBusinessLayer = "";
            _UpdateParameterName = "";
            _FindParameterName = "";
            _ExistParameterName = "";
            _DeleteParameterName = "";
            _SpecialParameter = false;

            txtbPassword.Clear();
            txtbServerName.Clear();
            txtbUserID.Clear();
            txtbModulesLayerNameSpace.Clear();

            listbTableOrViewNames.Items.Clear();
            listbColumns.Items.Clear();

            rbBusinessAndDataAccessLayer.Checked = false;
            rbModuleLayer.Checked = false;

            cbDataBaseNames.Items.Clear();

            lbFolderSelectedPathBusinessLayerResult.Text = "???";
            lbFolderSelectedPath.Text = "???";
            lbFolderSelectedPathModulesOrDataAccessLayer.Text = "Folder Selected Path Modules Layer :";

            lbFolderSelectedPathModulesOrDataAccessLayer.Visible = false;
            lbFolderSelectedPath.Visible = false;
            lbFolderSelectedPathBusinessLayerResult.Visible = false;
            lbFolderSelectedPathBusinessLayer.Visible = false;
            lbEnterModulesLayerNameSpace.Visible = false;
            txtbModulesLayerNameSpace.Visible = false;

            btBrowse.Text = "Browse M";

            btGenerate.Enabled = false;
            btBrowse.Enabled = false;
            btBrowseBusinessLayer.Enabled = false;

            chListBoxFunctions.SetItemChecked((byte)enFunctions.All, false);
            SetCheckToAllItems(false);
            chListBoxFunctions.Enabled = false;
        }

        void btChange_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtbTableSingleName.Text) && listbTableOrViewNames.SelectedIndex > 0 && _ListOfTableNameAndSingleName != null)
            {
                short Index = (short)_ListOfTableNameAndSingleName.FindIndex(x => x.TableName == listbTableOrViewNames.SelectedItem.ToString());

                if (Index != -1)
                {
                    _ListOfTableNameAndSingleName[Index].TableSingleName = txtbTableSingleName.Text;

                    if (_ListOfTableNameAndSingleName[Index].TableSingleName != _ListOfTableNameAndSingleName[Index].TableName)
                    {
                        txtbTableSingleName.BackColor = Color.Red;
                    }
                }
            }
        }

        void rbViews_Or_rbTables_CheckedChanged(object sender, EventArgs e)
        {
            if (rbViews.Checked)
            {
                lbTableorViewNames.Text = "View Names :";
                chListBoxFunctions.SetItemChecked((byte)enFunctions.All, false);
                SetCheckToAllItems(false);
                chListBoxFunctions.SetItemChecked((byte)enFunctions.GetAll, true);
                chListBoxFunctions.SelectedIndex = (byte)enFunctions.GetAll;
                chListBoxFunctions.Enabled = false;
            }
            else if (rbTables.Checked)
            {
                lbTableorViewNames.Text = "Table Names :";

                if (rbBusinessAndDataAccessLayer.Checked)
                {
                    chListBoxFunctions.Enabled = true;
                }
            }

            if (cbDataBaseNames.Items.Count > 0)
            {
                cbDataBaseNames_SelectedIndexChanged(null, null);
            }
        }

        private void contextMenuStrip2_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (listbTableOrViewNames.SelectedIndex > 0 && listbTableOrViewNames.SelectedItems.Count == 1)
            {
                generateCodeUiForSelectedTableToolStripMenuItem.Enabled = true;
            }
            else
            {
                generateCodeUiForSelectedTableToolStripMenuItem.Enabled = false;
            }
        }

        void generateCodeUiForSelectedTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string TableSingleName = _ListOfTableNameAndSingleName.Find(x => x.TableName == listbTableOrViewNames.SelectedItem.ToString())?.TableSingleName;

            fmCodeGeneratorUI codeGeneratorUI = new fmCodeGeneratorUI(listbTableOrViewNames.SelectedItem.ToString(), _ConnectionString, TableSingleName);
            codeGeneratorUI.ShowDialog();
        }

       







    }
}
