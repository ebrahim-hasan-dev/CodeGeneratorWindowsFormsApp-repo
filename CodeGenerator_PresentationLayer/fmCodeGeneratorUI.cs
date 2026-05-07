using CodeGenerator_BusinessLayer;
using CodeGenerator_Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeGenerator_PresentationLayer
{
    public partial class fmCodeGeneratorUI : Form
    {
        // =================================================================

        string _TableName { get; set; }
        string _TableSingleName { get; set; }
        string _NameSpaceUILayer { get; set; }
        List<clsColumnDataUILayer> _ListOfColumns { get; set; }

        // =================================================================


        public fmCodeGeneratorUI(string TableName, string ConnectionString, string TableSingleName)
        {
            InitializeComponent();

            FillDataGridViewComboBox();

            _TableName = TableName;

            _ListOfColumns = GetListOfColumns(_TableName, ConnectionString);

            if (string.IsNullOrWhiteSpace(TableSingleName))
            {
                _TableSingleName = TableName;
                txtbTableSingleName.Text = TableName;
            }
            else
            {
                txtbTableSingleName.Text = TableSingleName;
                _TableSingleName = TableSingleName;
            }
        }


        static void CaseIsReadOnlyHadle(string DesignerFilePath, string LogicFilePath, string XMLFilePath)
        {
            FileInfo fileInfoD = new FileInfo(DesignerFilePath);
            FileInfo fileInfoL = new FileInfo(LogicFilePath);
            FileInfo fileInfoX = new FileInfo(XMLFilePath);

            if ((fileInfoD.Exists && fileInfoD.IsReadOnly) || (fileInfoL.Exists && fileInfoL.IsReadOnly) || (fileInfoX.Exists && fileInfoX.IsReadOnly))
            {
                fileInfoD.IsReadOnly = false;
                fileInfoL.IsReadOnly = false;
                fileInfoX.IsReadOnly = false;
            }
        }

        bool CreateFilesAndWrite(string DesignerFilePath, string LogicFilePath, RuntimeTextTemplateUiLayerDesigner UITemplateDesigner, RuntimeTextTemplateUiLayerLogic UITemplateLogic)
        {
            string ClassCode = UITemplateDesigner.TransformText();

            CaseIsReadOnlyHadle(DesignerFilePath, LogicFilePath, Path.Combine(lbFolderSelectedPath.Text, rbForm.Checked ? "fm" + _TableSingleName.Replace("_", "") + ".resx" : "uctrl" + _TableSingleName.Replace("_", "") + ".resx"));

            try
            {
                File.WriteAllText(DesignerFilePath, ClassCode);

                ClassCode = UITemplateLogic.TransformText();

                File.WriteAllText(LogicFilePath, ClassCode);

                // هنا هيتم انشاء ملف xml مع امتداد .resx
                using (ResXResourceWriter resx = new ResXResourceWriter(Path.Combine(lbFolderSelectedPath.Text, rbForm.Checked ? "fm" + _TableSingleName.Replace("_", "") + ".resx" : "uctrl" + _TableSingleName.Replace("_", "") + ".resx")))
                { }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        List<clsColumnDataUILayer> GetListOfColumnData()
        {
            List<clsColumnDataUILayer> ListOfColumnData = new List<clsColumnDataUILayer>();

            for (short i = 0; i < dgvColumns.Rows.Count; i++)
            {
                ListOfColumnData.Add(new clsColumnDataUILayer
                {
                    Name = dgvColumns.Rows[i].Cells[0].Value.ToString().Replace(" ", ""),
                    Type = dgvColumns.Rows[i].Cells[1].Value.ToString(),
                    IsNullable = (bool)dgvColumns.Rows[i].Cells[2].Value,
                    IsPrimaryKey = (bool)dgvColumns.Rows[i].Cells[3].Value,
                    IsForeignKey = (bool)dgvColumns.Rows[i].Cells[4].Value,
                    Generate = (bool)dgvColumns.Rows[i].Cells[5].Value,
                    ControlName = dgvColumns.Rows[i].Cells[6].Value.ToString()
                });
            }


            return ListOfColumnData;
        }

        bool GeneratePerform()
        {
            fmCodeGenerator.CheckFromFolderPath(lbFolderSelectedPath.Text);

            if (fmCodeGenerator.HasWritePermission(lbFolderSelectedPath.Text))
            {
                RuntimeTextTemplateUiLayerDesigner UITemplateDesigner = new RuntimeTextTemplateUiLayerDesigner();
                RuntimeTextTemplateUiLayerLogic UITemplateLogic = new RuntimeTextTemplateUiLayerLogic();

                UITemplateDesigner.NameSpace = _NameSpaceUILayer;
                UITemplateDesigner.TableSingleName = _TableSingleName;
                UITemplateDesigner.TableName = _TableName;
                UITemplateDesigner.IsInput = rbInput.Checked == false && rbBoth.Checked == false ? false : true;
                UITemplateDesigner.ListOfColunmData = GetListOfColumnData();
                UITemplateDesigner.IsView = rbView.Checked;

                // لو المتغير دا قيمتة false معناها ان rbUserControl قيمتة true
                UITemplateDesigner.IsForm = rbForm.Checked;

                UITemplateLogic.NameSpace = _NameSpaceUILayer;
                UITemplateLogic.TableSingleName = _TableSingleName;
                UITemplateLogic.TableName = _TableName;
                UITemplateLogic.IsInput = UITemplateDesigner.IsInput;
                UITemplateLogic.ListOfColunmData = UITemplateDesigner.ListOfColunmData;
                UITemplateLogic.IsView = rbView.Checked;

                // لو المتغير دا قيمتة false معناها ان rbUserControl قيمتة true
                UITemplateLogic.IsForm = rbForm.Checked;

                string DesignerFilePath = Path.Combine(lbFolderSelectedPath.Text, rbForm.Checked ? "fm" + _TableSingleName.Replace("_", "") + ".Designer.cs" : "uctrl" + _TableSingleName.Replace("_", "") + ".Designer.cs");

                string LogicFilePath = Path.Combine(lbFolderSelectedPath.Text, rbForm.Checked ? "fm" + _TableSingleName.Replace("_", "") + ".cs" : "uctrl" + _TableSingleName.Replace("_", "") + ".cs");

                // لو ملف المصمم موجود يبقي الملفين التانيين بردو موجودين
                if (File.Exists(DesignerFilePath))
                {
                    if (MessageBox.Show("Are you sure you want to clear all old and write new code with same names?", "These files already exists",
                        MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
                    {
                        return false;
                    }
                }

                return CreateFilesAndWrite(DesignerFilePath, LogicFilePath, UITemplateDesigner, UITemplateLogic);
            }

            return false;
        }

        void GenerateUILayer()
        {
            if (rbForm.Checked || rbUserControl.Checked)
            {
                if (GeneratePerform())
                {
                    fmCodeGenerator.ShowSuccessfullyMessage();
                }
            }
            else
            {
                MessageBox.Show("You must select between ( form or user control )", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void btGenerate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(lbFolderSelectedPath.Text) && lbFolderSelectedPath.Text != "???" && !string.IsNullOrWhiteSpace(_NameSpaceUILayer))
            {
                if (dgvColumns.Rows.Count > 0)
                {
                    GenerateUILayer();
                }
                else
                {
                    MessageBox.Show("Not found any columns", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("One of the requirements is missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void FillDataGridViewComboBox()
        {
            colControl.Items.Add(enControls.TextBox.ToString());
            colControl.Items.Add(enControls.Label.ToString());
            colControl.Items.Add(enControls.MaskedTextBox.ToString());
            colControl.Items.Add(enControls.DateTimePicker.ToString());
            colControl.Items.Add(enControls.CheckBox.ToString());
            colControl.Items.Add(enControls.ComboBox.ToString());
        }

        void ClearThenFillDataGridView(List<clsColumnDataUILayer> ListOfColumns, bool Clear = true)
        {
            if (ListOfColumns != null)
            {
                if (Clear)
                {
                    dgvColumns.Rows.Clear();
                }

                short IndexItem = 0;

                for (short i = 0; i < ListOfColumns.Count; i++)
                {
                    IndexItem = (short)dgvColumns.Rows.Add(ListOfColumns[i].Name, ListOfColumns[i].Type, ListOfColumns[i].IsNullable, ListOfColumns[i].IsPrimaryKey,
                        ListOfColumns[i].IsForeignKey, true);

                    dgvColumns.Rows[IndexItem].Cells[6].Value = ListOfColumns[i].ControlName;
                }
            }
        }

        List<clsColumnDataUILayer> GetListOfColumns(string TableName, string ConnectionString)
        {
            List<clsColumnDataUILayer> ListOfColumns = null;

            try
            {
                ListOfColumns = ColumnService.GetAllColumnsUILayer(TableName, ConnectionString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ListOfColumns = null;
            }

            return ListOfColumns;
        }

        void SetControlColumn(string ControlName)
        {
            for (short i = 0; i < dgvColumns.Rows.Count; i++)
            {
                dgvColumns.Rows[i].Cells[6].Value = ControlName;
            }
        }

        void RadioButtonBothCheckedHandle(List<clsColumnDataUILayer> ListOfColumns)
        {
            if (ListOfColumns != null)
            {
                dgvColumns.Rows.Clear();

                short IndexItem = 0;

                for (short i = 0; i < ListOfColumns.Count; i++)
                {
                    IndexItem = (short)dgvColumns.Rows.Add(ListOfColumns[i].Name, ListOfColumns[i].Type, ListOfColumns[i].IsNullable, ListOfColumns[i].IsPrimaryKey,
                   ListOfColumns[i].IsForeignKey, true);

                    dgvColumns.Rows[IndexItem].Cells[6].Value = enControls.Label.ToString();
                }
            }
        }

        void rbOutput_Or_rbInput_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInput.Checked)
            {
                ClearThenFillDataGridView(_ListOfColumns);
                colGenerate.ReadOnly = false;
            }
            else if (rbOutput.Checked)
            {
                ClearThenFillDataGridView(_ListOfColumns);
                SetControlColumn(enControls.Label.ToString());
                colGenerate.ReadOnly = false;
            }

            lbNumberOfRows.Text = "# " + dgvColumns.Rows.Count + " Rows";
        }

        void rbBoth_CheckedChanged(object sender, EventArgs e)
        {
            if (rbBoth.Checked)
            {
                RadioButtonBothCheckedHandle(_ListOfColumns);
                ClearThenFillDataGridView(_ListOfColumns, false);
                colGenerate.ReadOnly = false;
            }

            lbNumberOfRows.Text = "# " + dgvColumns.Rows.Count + " Rows";
        }

        void SetLastSelectedPathUILayer()
        {
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.LastSelectedPathUILayer))
            {
                folderBrowserDialog1.SelectedPath = Properties.Settings.Default.LastSelectedPathUILayer;
            }
        }

        void btBrowse_Click(object sender, EventArgs e)
        {
            SetLastSelectedPathUILayer();

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                lbFolderSelectedPath.Text = folderBrowserDialog1.SelectedPath;

                _NameSpaceUILayer = Path.GetFileNameWithoutExtension(folderBrowserDialog1.SelectedPath);

                Properties.Settings.Default.LastSelectedPathUILayer = folderBrowserDialog1.SelectedPath;
                Properties.Settings.Default.Save();

                btGenerate.Enabled = true;
                this.AcceptButton = btGenerate;
            }
        }

        void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btChange_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtbTableSingleName.Text))
            {
                _TableSingleName = txtbTableSingleName.Text;

                if (_TableName != _TableSingleName)
                {
                    txtbTableSingleName.BackColor = Color.Red;
                }
            }
        }

        void SetColumnGenerate(bool Value)
        {
            for (short i = 0; i < dgvColumns.Rows.Count; i++)
            {
                dgvColumns.Rows[i].Cells[5].Value = Value;
            }
        }

        void rbView_CheckedChanged(object sender, EventArgs e)
        {
            if (rbView.Checked)
            {
                ClearThenFillDataGridView(_ListOfColumns);
                SetColumnGenerate(false);
                colGenerate.ReadOnly = true;
            }

            lbNumberOfRows.Text = "# " + dgvColumns.Rows.Count + " Rows";
        }




    }
}
