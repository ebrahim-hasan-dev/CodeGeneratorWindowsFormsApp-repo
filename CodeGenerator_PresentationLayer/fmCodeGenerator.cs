using CodeGenerator_BusinessLayer;
using CodeGenerator_Modules;
using DLMApp_ModulesLayer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


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
        
        string _ExtensionTag { get; } = "// [EXTRA_METHODS_HERE]";
        string _ExtensionTagScript { get; } = "-- [EXTRA_CODE_HERE]";
        
        bool _SpecialParameter { get; set; }

        List<clsTableNameAndSingleName> _ListOfTableNameAndSingleName = new List<clsTableNameAndSingleName>();

        enum enFunctions : byte { All = 0, Add = 1, Updata = 2, IsExist = 3, Find = 4, Delete = 5, GetAll = 6 };

        // ====================================================


        public fmCodeGenerator()
        {
            InitializeComponent();

            clsEventLog.WriteToEventLog("The program has been opened", enLogType.Information);
        }


        void fmCodeGenerator_Load(object sender, EventArgs e)
        {
            // This variable contains a test password, not the real one, as it was changed after the project was completed.
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
                clsEventLog.WriteToEventLog($"Not found data bases in this server name {txtbServerName.Text}", enLogType.Warning);
            }
        }

        async void btConnect_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtbServerName.Text) && !string.IsNullOrWhiteSpace(txtbPassword.Text) &&
                !string.IsNullOrWhiteSpace(txtbUserID.Text))
            {
                _ConnectionString = $"Server={txtbServerName.Text.Trim()};DataBase=master;User ID ={txtbUserID.Text.Trim()};Password={txtbPassword.Text.Trim()};";

                List<string> ListOfDataBaseNames = null;

                try
                {
                    ListOfDataBaseNames = await DataBaseService.GetAllDatabasesAsync(_ConnectionString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                clsEventLog.WriteToEventLog($"Not found tables in this data base {cbDataBaseNames.Text}", enLogType.Warning);
            }
        }

        async Task<List<string>> GetListOfTableOrViewNamesAsync(string ConnectionString)
        {
            List<string> ListOfTableOrViewNames = null;

            if (rbTables.Checked)
            {
                try
                {
                    ListOfTableOrViewNames = await TableService.GetAllTableNamesAsync(ConnectionString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (rbViews.Checked)
            {
                try
                {
                    ListOfTableOrViewNames = await ViewService.GetAllViewNamesAsync(ConnectionString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return ListOfTableOrViewNames;
        }

        async void cbDataBaseNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ConnectionString = $"Server={txtbServerName.Text.Trim()};DataBase={cbDataBaseNames.Text};User ID ={txtbUserID.Text.Trim()};Password={txtbPassword.Text.Trim()};";

            List<string> ListOfTableOrViewNames = await GetListOfTableOrViewNamesAsync(_ConnectionString);

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
                clsEventLog.WriteToEventLog("Not found columns in selected table", enLogType.Warning);
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

        static async Task<List<clsColumnDataModulesLayer>> GetAllColumnsAsync(string TableName, string ConnectionString, bool IsGenerateModulesLayer)
        {
            List<clsColumnDataModulesLayer> ListOfColumns = null;

            try
            {
                ListOfColumns = await ColumnService.GetAllColumnsAsync(TableName, ConnectionString, IsGenerateModulesLayer);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ListOfColumns;
        }

        async void listbTableNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listbTableOrViewNames.Items.Count > 0)
            {
                listbColumns.Items.Clear();

                if (listbTableOrViewNames.SelectedIndex > 0)
                {
                    ShowTableSingleName(listbTableOrViewNames.SelectedItem.ToString());

                    List<clsColumnDataModulesLayer> ListOfColumns = await GetAllColumnsAsync(listbTableOrViewNames.SelectedItem.ToString(), _ConnectionString, rbModuleLayer.Checked);

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
            clsEventLog.WriteToEventLog("The program has been closed", enLogType.Information);
            this.Close();
        }

        void SaveLastSelectedPath(string SelectedPath)
        {
            if (rbModuleLayer.Checked)
            {
                Properties.Settings.Default.LastSeectedPathModulesLayer = SelectedPath;
            }
            else if (rbDataAccessLayer.Checked)
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
            else if (rbDataAccessLayer.Checked)
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

            if (rbModuleLayer.Checked || rbDataAccessLayer.Checked)
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

        async Task<bool> CreateNewFileAndWriteAsync(string FilePath, RuntimeTextTemplateModulesLayer ModuleLayerTemplate)
        {
            string ClassCode = ModuleLayerTemplate.TransformText();

            CaseIsReadOnlyHadle(FilePath);

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    await writer.WriteAsync(ClassCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                return false;
            }

            return true;
        }

        async Task<bool> CreateNewFileAndWriteAsync(string FilePath, RuntimeTextTemplateStoredProceduresScript StoredProceduresScriptTemplate)
        {
            string ClassCode = StoredProceduresScriptTemplate.TransformText();

            CaseIsReadOnlyHadle(FilePath);

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    await writer.WriteAsync(ClassCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                return false;
            }

            return true;
        }

        async Task<bool> CreateNewFileAndWriteAsync(string FilePath, RuntimeTextTemplateStoredProceduresCode StoredProceduresTemplate)
        {
            string ClassCode = StoredProceduresTemplate.TransformText();

            CaseIsReadOnlyHadle(FilePath);

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    await writer.WriteAsync(ClassCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                return false;
            }

            return true;
        }

        async Task<bool> CreateNewFileAndWriteAsync(string FilePath, RuntimeTextTemplateEventLogClass EventLogClass)
        {
            string ClassCode = EventLogClass.TransformText();

            CaseIsReadOnlyHadle(FilePath);

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    await writer.WriteAsync(ClassCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
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

        async Task<bool> GenerateModulesLayerHelperAsync(string TableName, List<clsColumnDataModulesLayer> ListOfColumns)
        {
            RuntimeTextTemplateModulesLayer ModuleLayerTemplate = new RuntimeTextTemplateModulesLayer();

            ModuleLayerTemplate.NamespaceName = _NameSpaceModulesOrDataAccessLayer;
            ModuleLayerTemplate.TableSingleName = TableService.ConvertToSingle(TableName);
            //ModuleLayerTemplate.TableSingleName = GetTableSingleName(TableName);
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

            return await CreateNewFileAndWriteAsync(FilePath, ModuleLayerTemplate);
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
                clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                return false;
            }
        }

        async Task GenerateEventLogClassAsync(string FolderPath, string NameSpaceName)
        {
            string FilePath = Path.Combine(FolderPath, "clsEventLog.cs");

            if (!File.Exists(FilePath))
            {
                RuntimeTextTemplateEventLogClass EventLogClass = new RuntimeTextTemplateEventLogClass();
                EventLogClass.NamespaceName = NameSpaceName;
                await CreateNewFileAndWriteAsync(FilePath, EventLogClass);
            }
        }

        async Task<bool> GenerateModulesLayerAsync()
        {
            List<bool> ListOfResults = new List<bool>();

            CheckFromFolderPath(lbFolderSelectedPath.Text);

            txtbModulesLayerNameSpace.Text = Path.GetFileNameWithoutExtension(lbFolderSelectedPath.Text);

            if (HasWritePermission(lbFolderSelectedPath.Text))
            {
                await GenerateEventLogClassAsync(lbFolderSelectedPath.Text, txtbModulesLayerNameSpace.Text);

                if (listbTableOrViewNames.SelectedIndex == 0)
                {
                    for (short i = 1; i < listbTableOrViewNames.Items.Count; i++)
                    {
                        List<clsColumnDataModulesLayer> ListOfColumns = await GetAllColumnsAsync(listbTableOrViewNames.Items[i].ToString(), _ConnectionString, rbModuleLayer.Checked);

                        if (ListOfColumns != null)
                        {
                            ListOfResults.Add(await GenerateModulesLayerHelperAsync(listbTableOrViewNames.Items[i].ToString(), ListOfColumns));
                        }
                    }
                }
                else
                {
                    for (short i = 0; i < listbTableOrViewNames.SelectedItems.Count; i++)
                    {
                        List<clsColumnDataModulesLayer> ListOfColumns = await GetAllColumnsAsync(listbTableOrViewNames.SelectedItems[i].ToString(), _ConnectionString, rbModuleLayer.Checked);

                        if (ListOfColumns != null)
                        {
                            ListOfResults.Add(await GenerateModulesLayerHelperAsync(listbTableOrViewNames.SelectedItems[i].ToString(), ListOfColumns));
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
            clsColumnDataDataAccessLayer clsColumn = Columns.Find(x => x.Name == ParameterName);

            if (clsColumn != null)
            {
                return clsColumn.Type == "bool";
            }
            else
            {
                // Here, the function must return `true` for the primary key to be selected as the Parameter.
                return true;
            }
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

        string SelectCorrectParameterHelper(RuntimeTextTemplateStoredProceduresScript StoredProceduresScriptTemplate, string PrimaryKeyName, string MethodParameterName)
        {
            string CorrectParameter = "";

            if (!string.IsNullOrWhiteSpace(MethodParameterName) && !IsBoolean(MethodParameterName, StoredProceduresScriptTemplate.SPTemplate.DALTemplate.Columns))
            {
                CorrectParameter = MethodParameterName;
            }
            else
            {
                CorrectParameter = PrimaryKeyName;
            }

            return CorrectParameter;
        }

        string SelectCorrectParameterHelper(RuntimeTextTemplateStoredProceduresCode StoredProceduresTemplate, string PrimaryKeyName, string MethodParameterName)
        {
            string CorrectParameter = "";

            if (!string.IsNullOrWhiteSpace(MethodParameterName) && !IsBoolean(MethodParameterName, StoredProceduresTemplate.DALTemplate.Columns))
            {
                CorrectParameter = MethodParameterName;
            }
            else
            {
                CorrectParameter = PrimaryKeyName;
            }

            return CorrectParameter;
        }

        async Task<bool> SelectCorrectParameterAsync(RuntimeTextTemplateDataAccessLayer DataAccessLayerTemplate, string TableName)
        {
            try
            {
                DataAccessLayerTemplate.Columns = await ColumnService.GetAllColumnsDataAccessLayerAsync(TableName, _ConnectionString, rbModuleLayer.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (DataAccessLayerTemplate.Columns != null)
            {
                string PrimaryKeyName = DataAccessLayerTemplate.Columns.Find(x => x.IsPrimaryKey)?.Name ?? "";

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

        async Task<bool> SelectCorrectParameterAsync(RuntimeTextTemplateStoredProceduresScript StoredProceduresScriptTemplate, string TableName)
        {
            try
            {
                StoredProceduresScriptTemplate.SPTemplate.DALTemplate.Columns = await ColumnService.GetAllColumnsStoredProcedureScriptAsync(TableName, _ConnectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (StoredProceduresScriptTemplate.SPTemplate.DALTemplate.Columns != null)
            {
                string PrimaryKeyName = StoredProceduresScriptTemplate.SPTemplate.DALTemplate.Columns.Find(x => x.IsPrimaryKey)?.Name ?? "";

                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Updata) == true)
                {
                    StoredProceduresScriptTemplate.SPTemplate.DALTemplate.IncludeUpdate = true;

                    StoredProceduresScriptTemplate.SPTemplate.DALTemplate.UpdateParameterName = SelectCorrectParameterHelper(StoredProceduresScriptTemplate, PrimaryKeyName, _UpdateParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.IsExist) == true)
                {
                    StoredProceduresScriptTemplate.SPTemplate.DALTemplate.IncludeExist = true;

                    StoredProceduresScriptTemplate.SPTemplate.DALTemplate.ExistParameterName = SelectCorrectParameterHelper(StoredProceduresScriptTemplate, PrimaryKeyName, _ExistParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Find) == true)
                {
                    StoredProceduresScriptTemplate.SPTemplate.DALTemplate.IncludeFind = true;

                    StoredProceduresScriptTemplate.SPTemplate.DALTemplate.FindParameterName = SelectCorrectParameterHelper(StoredProceduresScriptTemplate, PrimaryKeyName, _FindParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Delete) == true)
                {
                    StoredProceduresScriptTemplate.SPTemplate.DALTemplate.IncludeDelete = true;

                    StoredProceduresScriptTemplate.SPTemplate.DALTemplate.DeleteParameterName = SelectCorrectParameterHelper(StoredProceduresScriptTemplate, PrimaryKeyName, _DeleteParameterName);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        async Task<bool> SelectCorrectParameterAsync(RuntimeTextTemplateStoredProceduresCode StoredProceduresTemplate, string TableName)
        {
            try
            {
                StoredProceduresTemplate.DALTemplate.Columns = await ColumnService.GetAllColumnsDataAccessLayerAsync(TableName, _ConnectionString, rbModuleLayer.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (StoredProceduresTemplate.DALTemplate.Columns != null)
            {
                string PrimaryKeyName = StoredProceduresTemplate.DALTemplate.Columns.Find(x => x.IsPrimaryKey)?.Name ?? "";

                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Updata) == true)
                {
                    StoredProceduresTemplate.DALTemplate.IncludeUpdate = true;

                    StoredProceduresTemplate.DALTemplate.UpdateParameterName = SelectCorrectParameterHelper(StoredProceduresTemplate, PrimaryKeyName, _UpdateParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.IsExist) == true)
                {
                    StoredProceduresTemplate.DALTemplate.IncludeExist = true;

                    StoredProceduresTemplate.DALTemplate.ExistParameterName = SelectCorrectParameterHelper(StoredProceduresTemplate, PrimaryKeyName, _ExistParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Find) == true)
                {
                    StoredProceduresTemplate.DALTemplate.IncludeFind = true;

                    StoredProceduresTemplate.DALTemplate.FindParameterName = SelectCorrectParameterHelper(StoredProceduresTemplate, PrimaryKeyName, _FindParameterName);
                }
                if (chListBoxFunctions.GetItemChecked((byte)enFunctions.Delete) == true)
                {
                    StoredProceduresTemplate.DALTemplate.IncludeDelete = true;

                    StoredProceduresTemplate.DALTemplate.DeleteParameterName = SelectCorrectParameterHelper(StoredProceduresTemplate, PrimaryKeyName, _DeleteParameterName);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        void ShowMarkNotExistMessage()
        {
            MessageBox.Show($"The file already exists, but the tag for the extension has been removed\n\nplease write this comment into the class in the file\n\"{_ExtensionTag}\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            clsEventLog.WriteToEventLog($"The file already exists, but the tag for the extension has been removed\n\nplease write this comment into the class in the file\n\"{_ExtensionTag}\"", enLogType.Warning);
        }

        async Task<bool> CreateNewFileAndWriteAsync(string FilePath, RuntimeTextTemplateDataAccessLayer DataAccessLayerTemplate)
        {
            string ClassCode = DataAccessLayerTemplate.TransformText();

            CaseIsReadOnlyHadle(FilePath);

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    await writer.WriteAsync(ClassCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                return false;
            }

            return true;
        }

        async Task<bool> WriteListIntoFileAsync(string FilePath, List<string> Lines)
        {
            CaseIsReadOnlyHadle(FilePath);

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    // دمج السطور مع وضع "علامة سطر جديد" بين كل سطر والآخر
                    string FullText = string.Join(Environment.NewLine, Lines);
                    
                    await writer.WriteAsync(FullText);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                return false;
            }

            return true;
        }

        async Task<List<string>> ReadFileAndSetItIntoListAsync(string FilePath)
        {
            List<string> Lines = new List<string>();

            try
            {
                string LinesAsString = "";
               
                using (StreamReader reader = new StreamReader(FilePath))
                {
                    LinesAsString = await reader.ReadToEndAsync();

                    // تقسيم النص الكامل إلى سطور بناءً على علامات النزول لسطر جديد، ثم تحويلها لقائمة
                    Lines = LinesAsString?.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)?.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                Lines.Clear();
            }

            return Lines;
        }

        async Task<bool> FileExistHandleAsync(string FilePath, string TableName, RuntimeTextTemplateDataAccessLayer DataAccessLayerTemplate)
        {
            List<string> Lines = await ReadFileAndSetItIntoListAsync(FilePath);

            int Index = Lines.FindIndex(line => line.Contains(_ExtensionTag));

            if (Index != -1)
            {
                if (Lines.Exists(x => x.Contains(" class ")))
                {
                    DataAccessLayerTemplate.IsAppendMode = true;

                    string MethodsCode = DataAccessLayerTemplate.TransformText();

                    Lines.RemoveRange(Index - 1, 2);

                    Lines.Insert(Index, MethodsCode);

                    return await WriteListIntoFileAsync(FilePath, Lines);
                }
                else
                {
                    return await CreateNewFileAndWriteAsync(FilePath, DataAccessLayerTemplate);
                }
            }
            else
            {
                if (Lines.Count == 0)
                {
                    return await CreateNewFileAndWriteAsync(FilePath, DataAccessLayerTemplate);
                }
                else
                {
                    ShowMarkNotExistMessage();
                }
            }

            return false;
        }

        async Task<bool> FileExistHandleAsync(string FilePath, string TableName, RuntimeTextTemplateStoredProceduresScript StoredProceduresScriptTemplate)
        {
            List<string> Lines = await ReadFileAndSetItIntoListAsync(FilePath);

            int Index = Lines.FindIndex(line => line.Contains(_ExtensionTagScript));

            if (Index != -1)
            {
                if (Lines.Exists(x => x.Contains(" class ")))
                {
                    StoredProceduresScriptTemplate.SPTemplate.DALTemplate.IsAppendMode = true;

                    string MethodsCode = StoredProceduresScriptTemplate.TransformText();

                    Lines.RemoveRange(Index - 1, 2);

                    Lines.Insert(Index, MethodsCode);

                    return await WriteListIntoFileAsync(FilePath, Lines);
                }
                else
                {
                    return await CreateNewFileAndWriteAsync(FilePath, StoredProceduresScriptTemplate);
                }
            }
            else
            {
                if (Lines.Count == 0)
                {
                    return await CreateNewFileAndWriteAsync(FilePath, StoredProceduresScriptTemplate);
                }
                else
                {
                    ShowMarkNotExistMessage();
                }
            }

            return false;
        }

        async Task<bool> FileExistHandleAsync(string FilePath, string TableName, RuntimeTextTemplateStoredProceduresCode StoredProceduresTemplate)
        {
            List<string> Lines = await ReadFileAndSetItIntoListAsync(FilePath);

            int Index = Lines.FindIndex(line => line.Contains(_ExtensionTag));

            if (Index != -1)
            {
                if (Lines.Exists(x => x.Contains(" class ")))
                {
                    StoredProceduresTemplate.DALTemplate.IsAppendMode = true;

                    string MethodsCode = StoredProceduresTemplate.TransformText();

                    Lines.RemoveRange(Index - 1, 2);

                    Lines.Insert(Index, MethodsCode);

                    return await WriteListIntoFileAsync(FilePath, Lines);
                }
                else
                {
                    return await CreateNewFileAndWriteAsync(FilePath, StoredProceduresTemplate);
                }
            }
            else
            {
                if (Lines.Count == 0)
                {
                    return await CreateNewFileAndWriteAsync(FilePath, StoredProceduresTemplate);
                }
                else
                {
                    ShowMarkNotExistMessage();
                }
            }

            return false;
        }

        async Task<bool> GenerateNormalQueriesAsync(string TableName)
        {
            RuntimeTextTemplateDataAccessLayer DataAccessLayerTemplate = new RuntimeTextTemplateDataAccessLayer();

            if (await SelectCorrectParameterAsync(DataAccessLayerTemplate, TableName) == true)
            {
                DataAccessLayerTemplate.NamespaceName = _NameSpaceModulesOrDataAccessLayer;
                DataAccessLayerTemplate.TableName = TableName;
                DataAccessLayerTemplate.TableSingleName = TableService.ConvertToSingle(TableName);
                //DataAccessLayerTemplate.TableSingleName = GetTableSingleName(TableName);
                DataAccessLayerTemplate.ModulesLayerNameSpace = txtbModulesLayerNameSpace.Text;

                DataAccessLayerTemplate.IncludeAdd = chListBoxFunctions.GetItemChecked((byte)enFunctions.Add);
                DataAccessLayerTemplate.IncludeGetAll = chListBoxFunctions.GetItemChecked((byte)enFunctions.GetAll);

                string FilePath = Path.Combine(lbFolderSelectedPath.Text, DataAccessLayerTemplate.TableSingleName + "Repository" + ".cs");

                if (File.Exists(FilePath))
                {
                    return await FileExistHandleAsync(FilePath, TableName, DataAccessLayerTemplate);
                }
                else
                {
                    return await CreateNewFileAndWriteAsync(FilePath, DataAccessLayerTemplate);
                }
            }

            return false;
        }

        string GetFolderName(string FolderPath)
        {
            FolderPath = Path.Combine(FolderPath, "Scripts");

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            return FolderPath;
        }

        async Task<bool> GenerateStoredProceduresHelperAsync(RuntimeTextTemplateStoredProceduresScript StoredProceduresScriptTemplate, string TableName)
        {
            if (await SelectCorrectParameterAsync(StoredProceduresScriptTemplate, TableName) == true)
            {
                StoredProceduresScriptTemplate.SPTemplate.DALTemplate.TableName = TableName;
                StoredProceduresScriptTemplate.DatabaseName = cbDataBaseNames.Text;

                StoredProceduresScriptTemplate.SPTemplate.DALTemplate.IncludeAdd = chListBoxFunctions.GetItemChecked((byte)enFunctions.Add);
                StoredProceduresScriptTemplate.SPTemplate.DALTemplate.IncludeGetAll = chListBoxFunctions.GetItemChecked((byte)enFunctions.GetAll);

                string FilePath = Path.Combine(GetFolderName(lbFolderSelectedPathStoredProcedureScriptResult.Text), "Add_SP_OnTable" + StoredProceduresScriptTemplate.SPTemplate.DALTemplate.TableName + ".sql");

                if (File.Exists(FilePath))
                {
                    return await FileExistHandleAsync(FilePath, TableName, StoredProceduresScriptTemplate);
                }
                else
                {
                    return await CreateNewFileAndWriteAsync(FilePath, StoredProceduresScriptTemplate);
                }
            }

            return false;
        }

        async Task<bool> GenerateStoredProceduresHelperAsync(RuntimeTextTemplateStoredProceduresCode StoredProceduresTemplate, string TableName)
        {
            if (await SelectCorrectParameterAsync(StoredProceduresTemplate, TableName) == true)
            {
                StoredProceduresTemplate.DALTemplate.NamespaceName = _NameSpaceModulesOrDataAccessLayer;
                StoredProceduresTemplate.DALTemplate.TableName = TableName;
                StoredProceduresTemplate.DALTemplate.TableSingleName = TableService.ConvertToSingle(TableName);
                //StoredProceduresTemplate.DALTemplate.TableSingleName = GetTableSingleName(TableName);
                StoredProceduresTemplate.DALTemplate.ModulesLayerNameSpace = txtbModulesLayerNameSpace.Text;

                StoredProceduresTemplate.DALTemplate.IncludeAdd = chListBoxFunctions.GetItemChecked((byte)enFunctions.Add);
                StoredProceduresTemplate.DALTemplate.IncludeGetAll = chListBoxFunctions.GetItemChecked((byte)enFunctions.GetAll);

                string FilePath = Path.Combine(lbFolderSelectedPath.Text, StoredProceduresTemplate.DALTemplate.TableSingleName + "Repository" + ".cs");

                if (File.Exists(FilePath))
                {
                    return await FileExistHandleAsync(FilePath, TableName, StoredProceduresTemplate);
                }
                else
                {
                    return await CreateNewFileAndWriteAsync(FilePath, StoredProceduresTemplate);
                }
            }

            return false;
        }

        async Task<bool> GenerateStoredProceduresAsync(string TableName)
        {
            RuntimeTextTemplateStoredProceduresCode StoredProceduresTemplate = new RuntimeTextTemplateStoredProceduresCode();
            RuntimeTextTemplateStoredProceduresScript StoredProceduresScriptTemplate = new RuntimeTextTemplateStoredProceduresScript();
            
            bool Operation1 = await GenerateStoredProceduresHelperAsync(StoredProceduresTemplate, TableName);
            bool Operation2 = await GenerateStoredProceduresHelperAsync(StoredProceduresScriptTemplate, TableName);

            return Operation1 && Operation2;
        }

        async Task<bool> GenerateDataAccessLayerHelperAsync(string TableName)
        {
            if (rbStoredProcedures.Checked)
            {
                return await GenerateStoredProceduresAsync(TableName);
            }
            else if (rbNormalQueries.Checked)
            {
                return await GenerateNormalQueriesAsync(TableName);
            }

            return false;
        }

        async Task<bool> CreateNewFileAndWriteAsync(string FilePath, RuntimeTextTemplateConnectionStringClass ConnectionStringTemplate)
        {
            string ClassCode = ConnectionStringTemplate.TransformText();

            CaseIsReadOnlyHadle(FilePath);

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    await writer.WriteAsync(ClassCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                return false;
            }

            return true;
        }

        async Task GenerateConnectionStringClassAsync(string FolderPath)
        {
            string FilePath = Path.Combine(FolderPath, "clsConnectionString.cs");

            if (!File.Exists(FilePath))
            {
                RuntimeTextTemplateConnectionStringClass ConnectionStringClass = new RuntimeTextTemplateConnectionStringClass();
                ConnectionStringClass.NamespaceName = _NameSpaceModulesOrDataAccessLayer;
                ConnectionStringClass.DatebaseName = cbDataBaseNames.Text;
                await CreateNewFileAndWriteAsync(FilePath, ConnectionStringClass);
            }
        }

        async Task<bool> GenerateDataAccessLayerAsync()
        {
            List<bool> ListOfResults = new List<bool>();

            CheckFromFolderPath(lbFolderSelectedPath.Text);

            if (rbStoredProcedures.Checked)
            {
                CheckFromFolderPath(lbFolderSelectedPathStoredProcedureScriptResult.Text);

                if (!HasWritePermission(lbFolderSelectedPathStoredProcedureScriptResult.Text))
                {
                    return false;
                }
            }

            if (HasWritePermission(lbFolderSelectedPath.Text))
            {
                await GenerateConnectionStringClassAsync(lbFolderSelectedPath.Text);

                if (listbTableOrViewNames.SelectedIndex == 0)
                {
                    for (short i = 1; i < listbTableOrViewNames.Items.Count; i++)
                    {
                        ListOfResults.Add(await GenerateDataAccessLayerHelperAsync(listbTableOrViewNames.Items[i].ToString()));
                    }
                }
                else
                {
                    for (short i = 0; i < listbTableOrViewNames.SelectedItems.Count; i++)
                    {
                        ListOfResults.Add(await GenerateDataAccessLayerHelperAsync(listbTableOrViewNames.SelectedItems[i].ToString()));
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
            clsColumnDataBusinessLayer clsColumn = Columns.Find(x => x.Name == ParameterName);

            if (clsColumn != null)
            {
                return clsColumn.Type == "bool";
            }
            else
            {
                // Here, the function must return `true` for the primary key to be selected as the Parameter.
                return true;
            }
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

        async Task<bool> SelectCorrectParameterAsync(RuntimeTextTemplateBusinessLayer BusinessLayerTemplate, string TableName)
        {
            try
            {
                BusinessLayerTemplate.Columns = await ColumnService.GetAllColumnsBusinessLayerAsync(TableName, _ConnectionString, rbModuleLayer.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (BusinessLayerTemplate.Columns != null)
            {
                string PrimaryKeyName = BusinessLayerTemplate.Columns.Find(x => x.IsPrimaryKey)?.Name ?? "";

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

        async Task<bool> CreateNewFileAndWriteAsync(string FilePath, RuntimeTextTemplateBusinessLayer BusinessLayerTemplate)
        {
            string ClassCode = BusinessLayerTemplate.TransformText();

            CaseIsReadOnlyHadle(FilePath);

            try
            {
                using (StreamWriter writer = new StreamWriter(FilePath))
                {
                    await writer.WriteAsync(ClassCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                clsEventLog.WriteToEventLog(ex.Message, enLogType.Error);
                return false;
            }

            return true;
        }

        async Task<bool> FileExistHandleAsync(string FilePath, string TableName, RuntimeTextTemplateBusinessLayer BusinessLayerTemplate)
        {
            List<string> Lines = await ReadFileAndSetItIntoListAsync(FilePath);

            int Index = Lines.FindIndex(line => line.Contains(_ExtensionTag));

            if (Index != -1)
            {
                if (Lines.Exists(x => x.Contains(" class ")))
                {
                    BusinessLayerTemplate.IsAppendMode = true;

                    string MethodsCode = BusinessLayerTemplate.TransformText();

                    Lines.RemoveRange(Index - 1, 2);

                    Lines.Insert(Index, MethodsCode);

                    return await WriteListIntoFileAsync(FilePath, Lines);
                }
                else
                {
                    return await CreateNewFileAndWriteAsync(FilePath, BusinessLayerTemplate);
                }
            }
            else
            {
                if (Lines.Count == 0)
                {
                    return await CreateNewFileAndWriteAsync(FilePath, BusinessLayerTemplate);
                }
                else
                {
                    ShowMarkNotExistMessage();
                }
            }

            return false;
        }

        async Task<bool> GenerateBusinessLayerHelperAsync(string TableName)
        {
            RuntimeTextTemplateBusinessLayer BusinessLayerTemplate = new RuntimeTextTemplateBusinessLayer();

            if (await SelectCorrectParameterAsync(BusinessLayerTemplate, TableName) == true)
            {
                BusinessLayerTemplate.NamespaceName = _NameSpaceBusinessLayer;
                BusinessLayerTemplate.TableName = TableName;
                BusinessLayerTemplate.TableSingleName = TableService.ConvertToSingle(TableName);
                //BusinessLayerTemplate.TableSingleName = GetTableSingleName(TableName);
                BusinessLayerTemplate.ModulesLayerNameSpace = txtbModulesLayerNameSpace.Text;
                BusinessLayerTemplate.DataAccessLayerNameSpace = _NameSpaceModulesOrDataAccessLayer;

                BusinessLayerTemplate.IncludeAdd = chListBoxFunctions.GetItemChecked((byte)enFunctions.Add);
                BusinessLayerTemplate.IncludeGetAll = chListBoxFunctions.GetItemChecked((byte)enFunctions.GetAll);

                string FilePath = Path.Combine(lbFolderSelectedPathBusinessLayerResult.Text, BusinessLayerTemplate.TableSingleName + "Service" + ".cs");

                if (File.Exists(FilePath))
                {
                    return await FileExistHandleAsync(FilePath, TableName, BusinessLayerTemplate);
                }
                else
                {
                    return await CreateNewFileAndWriteAsync(FilePath, BusinessLayerTemplate);
                }
            }

            return false;
        }

        async Task<bool> GenerateBusinessLayerAsync()
        {
            List<bool> ListOfResults = new List<bool>();

            CheckFromFolderPath(lbFolderSelectedPathBusinessLayerResult.Text);

            if (HasWritePermission(lbFolderSelectedPathBusinessLayerResult.Text))
            {
                if (listbTableOrViewNames.SelectedIndex == 0)
                {
                    for (short i = 1; i < listbTableOrViewNames.Items.Count; i++)
                    {
                        ListOfResults.Add(await GenerateBusinessLayerHelperAsync(listbTableOrViewNames.Items[i].ToString()));
                    }
                }
                else
                {
                    for (short i = 0; i < listbTableOrViewNames.SelectedItems.Count; i++)
                    {
                        ListOfResults.Add(await GenerateBusinessLayerHelperAsync(listbTableOrViewNames.SelectedItems[i].ToString()));
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
            clsEventLog.WriteToEventLog("One of the files was not successfully produced.", enLogType.Error);
        }

        async Task PerformCorrectGenerateLayerAsync()
        {
            if (rbModuleLayer.Checked)
            {
                if (await GenerateModulesLayerAsync())
                {
                    ShowSuccessfullyMessage();
                }
                else
                {
                    ShowFaildMessage();
                }
            }
            else if (rbDataAccessLayer.Checked)
            {
                if (IsAnyFunctionsChecked())
                {
                    if ((chbGenerateBusinessLayer.Checked && lbFolderSelectedPathBusinessLayerResult.Text != "???" &&
                        !string.IsNullOrWhiteSpace(lbFolderSelectedPathBusinessLayerResult.Text)) || !chbGenerateBusinessLayer.Checked)
                    {
                        if (!string.IsNullOrWhiteSpace(txtbModulesLayerNameSpace.Text))
                        {
                            if (rbStoredProcedures.Checked && (lbFolderSelectedPathStoredProcedureScriptResult.Text == "???" || string.IsNullOrWhiteSpace(lbFolderSelectedPathStoredProcedureScriptResult.Text)))
                            {
                                MessageBox.Show("You must select folder path to stored procedure script", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                this.AcceptButton = btBrowseSP;
                                btBrowseSP.Focus();
                                return;
                            }

                            bool ResultDataAccessLayer = await GenerateDataAccessLayerAsync();
                            bool ResultBusinessLayer = false;

                            if (chbGenerateBusinessLayer.Checked)
                            {
                                ResultBusinessLayer = await GenerateBusinessLayerAsync();
                            }
                            
                            if (!ResultDataAccessLayer || (!ResultBusinessLayer && chbGenerateBusinessLayer.Checked))
                            {
                                ShowFaildMessage();
                            }
                            else
                            {
                                ShowSuccessfullyMessage();

                                //chListBoxFunctions.SetItemChecked((byte)enFunctions.All, false);
                                //SetCheckToAllItems(false);
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

        async void btGenerate_Click(object sender, EventArgs e)
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
                await PerformCorrectGenerateLayerAsync();
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
                SetCheckToAllItems(false);
                chListBoxFunctions.Enabled = false;

                lbFolderSelectedPathBusinessLayer.Visible = false;
                lbFolderSelectedPathBusinessLayerResult.Visible = false;
                btBrowseBusinessLayer.Enabled = false;
                btBrowse.Text = "Browse M";
                lbFolderSelectedPathModulesOrDataAccessLayer.Text = "Folder Selected Path Modules Layer :";
                txtbModulesLayerNameSpace.Enabled = false;
                chbGenerateBusinessLayer.Enabled = false;
                panel1.Enabled = false;

                btBrowseSP.Enabled = false;
                lbFolderSelectedPathStoredProcedureScript.Visible = false;
                lbFolderSelectedPathStoredProcedureScriptResult.Visible = false;
            }
            else if (rbDataAccessLayer.Checked)
            {
                chListBoxFunctions.Enabled = true;
                lbFolderSelectedPathBusinessLayer.Visible = true;
                lbFolderSelectedPathBusinessLayerResult.Visible = true;

                btBrowseBusinessLayer.Enabled = true;
                btBrowse.Text = "Browse D";
                lbFolderSelectedPathModulesOrDataAccessLayer.Text = "Folder Selected Path Data Access Layer :";
                txtbModulesLayerNameSpace.Enabled = true;
                chbGenerateBusinessLayer.Enabled = true;
                panel1.Enabled = true;

                chbGenerateBusinessLayer_CheckedChanged(null, null);
                rbStoredProcedures_CheckedChanged(null, null);
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

            rbDataAccessLayer.Checked = false;
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
            }
            else if (rbTables.Checked)
            {
                lbTableorViewNames.Text = "Table Names :";
            }

            if (rbDataAccessLayer.Checked)
            {
                chListBoxFunctions.Enabled = true;
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

        private void fmCodeGenerator_FormClosed(object sender, FormClosedEventArgs e)
        {
            clsEventLog.WriteToEventLog("The program has been closed", enLogType.Information);
        }

        private void chbGenerateBusinessLayer_CheckedChanged(object sender, EventArgs e)
        {
            if (chbGenerateBusinessLayer.Checked)
            {
                btBrowseBusinessLayer.Enabled = true;
                lbFolderSelectedPathBusinessLayer.Visible = true;
                lbFolderSelectedPathBusinessLayerResult.Visible = true;
            }
            else
            {
                lbFolderSelectedPathBusinessLayer.Visible = false;
                lbFolderSelectedPathBusinessLayerResult.Visible = false;
                btBrowseBusinessLayer.Enabled = false;
                lbFolderSelectedPathBusinessLayerResult.Text = "???";
            }
        }

        void SetLastSelectedPathToDialogStoredProcedureScript()
        {
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LastSelectedPathStoredProcedureScript) &&
                Directory.Exists(Properties.Settings.Default.LastSelectedPathStoredProcedureScript))
            {
                folderBrowserDialog1.SelectedPath = Properties.Settings.Default.LastSelectedPathStoredProcedureScript;
            }
        }

        void btBrowseStoredProcedure_Click(object sender, EventArgs e)
        {
            SetLastSelectedPathToDialogStoredProcedureScript();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lbFolderSelectedPathStoredProcedureScriptResult.Text = folderBrowserDialog1.SelectedPath;

                Properties.Settings.Default.LastSelectedPathStoredProcedureScript = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();

                btGenerate.Enabled = true;
                this.AcceptButton = btGenerate;
            }
        }

        private void rbStoredProcedures_CheckedChanged(object sender, EventArgs e)
        {
            if (rbStoredProcedures.Checked)
            {
                btBrowseSP.Enabled = true;
                lbFolderSelectedPathStoredProcedureScript.Visible = true;
                lbFolderSelectedPathStoredProcedureScriptResult.Visible = true;
            }
            else if (rbNormalQueries.Checked)
            {
                btBrowseSP.Enabled = false;
                lbFolderSelectedPathStoredProcedureScript.Visible = false;
                lbFolderSelectedPathStoredProcedureScriptResult.Visible = false;
            }
        }
    }
}
