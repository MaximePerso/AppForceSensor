using Doli.DoPE;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace K2000Rs232App
{
    public partial class SetUp : Form
    {
        #region " Init "
        private List<ClassCycleSetUp> FillCycleDgvSetUp = new List<ClassCycleSetUp>();
        private int iNbCycleSetUpToDo = 1;
        public string sDefaultPathSetUp;
        public string sNameOfFileCycleSetUp = "Cycle.txt";
        public delegate void DelegateStatusLabelSetUp(string sValue);
        public DelegateStatusLabelSetUp PassStatusLabelSetUp { get; private set; }
        List<ClassForIdSetUp> ListMesureSetUp = new List<ClassForIdSetUp>();
        private List<ClassSequenceSetUp> listForDgvMeasureSetUp = new List<ClassSequenceSetUp>();
        public ClassRS232 NewInstanceOfClassRs232SetUp = new ClassRS232();
        ClassMesure NewInstanceOfClassMesureForSetUp = new ClassMesure();
        public List<ClassForThreadMeasureSetUp> ListForMeasureSetUp = new List<ClassForThreadMeasureSetUp>();
        public ClassVariablesGlobales newclassOfVGSetUp = new ClassVariablesGlobales();
        public List<ClassForMesureDOliSetUp> ListForMeasurePosSetUp = new List<ClassForMesureDOliSetUp>();
        //private bool bFlagForAverageMeasureSetUp = false;

        public string sErrorMessageSetUp = "";

        private bool bTargetReachSetUp = false;
        private List<ClassOnPosMasgSetUp> ListOfOnPosMsgSetUp = new List<ClassOnPosMasgSetUp>();
        //Timers
        public System.Timers.Timer MyElapsedMeasureTimerSetUp;
        public System.Timers.Timer MyElapsedCycleTimerSetUp;
        public System.Timers.Timer MyElapsedDoliTargetLoadSetUp;
        public System.Timers.Timer MyElapsedLaunchingSetUp;
        public System.Timers.Timer MyElapsedPalierSetUp;

        public delegate void DelegatePassTimePalierSetUp(DateTime dt);
        public DelegatePassTimePalierSetUp PassTimePalierSetUp { get; set; }

        public delegate void DelegatePassTimerCountSetUp(TimeSpan dt);
        public DelegatePassTimerCountSetUp PassTimerCountSetUp { get; set; }

        //Date time
        private DateTime dtLaunchCycleTimeSetUp = new DateTime();
        private DateTime dtForPalierSetUp = new DateTime();

        public delegate void DelegateStatusErrorRichTextBoxFromMesure(List<ClassForResultSetUp> ListForError);
        public delegate void DelegateTbxPosition(string sMeasure);
        public delegate void DelegateTbxForce(string sMeasure);
        public delegate void DelegateTbxExtension(string sMeasure);
        public delegate void DelegateOnPosMsg(DoPE.ERR Error, bool bReached, double dTime, DoPE.CTRL Control, double dPosition, DoPE.CTRL DControl, double dDestination, short UsTan);
        public delegate void DelegateOnCommandErrorSetUp(double CommandNumber, DoPE.CMD_EERROR ErrorNumber, short UsTan);
        public delegate void DelegateOnSftMsgSetUp(DoPE.ERR Error, short Upper, double dTime, DoPE.CTRL Control, double dPosition, short usTAN);
        public delegate void DelegateOnOffsCMsgSetUp(DoPE.ERR Error, double dTime, double dOffset, short usTAN);
        public delegate void DelegateOnCheckMsgSetUp(DoPE.ERR Error, bool bAction, double dTime, DoPE.CHK_ID CheckId, double dPosition, DoPE.SENSOR SensorNo, short usTAN);
        public delegate void DelegateOnShieldMsgSetUp(DoPE.ERR Error, bool bAction, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTAN);
        public delegate void DelegateOnRefSignalMsgSetUp(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTAN);
        public delegate void DelegateOnSensorMsgSetUp(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, short usTAN);
        public delegate void DelegateOnIoSHaltMsgSetUp(DoPE.ERR Error, short Upper, double dTime, DoPE.CTRL Control, double dPosition, short usTAN);
        public delegate void DelegateOnKeyMsgSetUp(DoPE.ERR Error, double dTime, long Keys, long NewKeys, long GoneKeys, short OemKeys, short NewOemKeys, short GoneOemKeys, short usTAN);
        public delegate void DelegateOnRuntimeErrorSetUp(DoPE.ERR Error, DoPE.RTE ErrorNumber, double dTime, short Device, short Bits, short usTAN);
        public delegate void DelegaeOnOverflowSetUp(int iOverflow);
        public delegate void DelegateOnDebugMsgSetUp(DoPE.ERR Error, DoPE.DEBUG MsgType, double dTime, string sText);
        public delegate void DelegateOnSystemMsgSetUp(DoPE.ERR Error, DoPE.SYSTEM_MSG MsgNumber, double dTime, string sText);
        public delegate void DelegateOnRmcEventSetUp(long Keys, long NewKeys, long GoneKeys, long Leds, long NewLeds, long GoneLeds);

        Doli NewClassOfDoliSetUp = new Doli();

        private List<ClassDynamicalData> LDynamicalData = new List<ClassDynamicalData>();

        public SetUp()
        {
            InitializeComponent();
            CloseAccessToTools();

            DefinitionChart();
            RefreshChart();
        }
        #endregion

        #region " Enabled = true (via delegate) " 
        public void UpdateEnabledSequencialPartToTrue()
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateEnabledSequencialPartToTrue()));
            else
                InternalUpdateEnabledSequencialPartToTrue();
        }
        private void InternalUpdateEnabledSequencialPartToTrue()
        {
            try
            {
                BtnLaunchAcquisition.Enabled = true;
            }
            catch (NullReferenceException) { }
        }
        #endregion

        #region " Enabled = false " 
        private void CloseAccessToTools()
        {
            //On remet a zéro le compteur
            BtnLaunchAcquisition.Enabled = false;
            BtnStartSetUp.Enabled = false;
            BtnValidateSetUp.Enabled = false;
        }
        #endregion

        #region " Classe "
        public class ClassDynamicalData
        {
            public double Xserie1 { get; set; }
            public double Yserie1 { get; set; }
            public double Xserie2 { get; set; }
            public double Yserie2 { get; set; }
            public double Xserie3 { get; set; }
            public double Yserie3 { get; set; }
        }

        public class ClassForIdSetUp
        {
            public int Id { get; set; }
            public int Config { get; set; }
            public string Designation { get; set; }
        }
        public class ClassCycleSetUp
        {
            public string Step { get; set; }
            public string Mode { get; set; }
            public string Palier { get; set; }
            public string Rampe { get; set; }
            public string Temp { get; set; }
            public string Load { get; set; }
            public string LwrLimit { get; set; }
            public string UprLimit { get; set; }
            public string Pred { get; set; }
        }

        public class ClassSequenceSetUp
        {
            public string Id { get; set; }
            public string Type { get; set; }
            public string Designation { get; set; }
        }

        public class ClassForResultSetUp
        {
            public int Id { get; set; }
            public string Measure { get; set; }
            public string Designation { get; set; }
        }

        public class ClassOnPosMasgSetUp
        {
            public DoPE.ERR ErrorToClass { get; set; }
            public bool ReachedToClass { get; set; }
            public double TimeToClass { get; set; }
            public DoPE.CTRL ControlToClass { get; set; }
            public double PositionToClass { get; set; }
            public DoPE.CTRL DControlToClass { get; set; }
            public double DestinationToClass { get; set; }
            public short UsTanToClass { get; set; }
        }

        public class ClassForThreadMeasureSetUp
        {
            public int Id { get; set; }
            public string Measure { get; set; }
            public long Step { get; set; }
        }

        public class ClassForMesureDOliSetUp
        {
            public DateTime Dt { get; set; }
            public double Rampe { get; set; }
            public double Palier { get; set; }
            public double Temp { get; set; }
            public double Position { get; set; }
            public double Load { get; set; }
            public long Step { get; set; }
            public double LoadConsigne { get; set; }
            public double Lll { get; set; }
            public double Upl { get; set; }
        }
        #endregion

        #region " Refresh Table "
        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (dgvSetUp.Rows.Count > 0)
            { RefreshDgvSetUp(); }
            else
            { MessageBox.Show("La table n'est pas présente !"); }
        }
        #endregion

        #region " StatusLabelSetUp "
        public void UpdateStatusLabelSetUp(string TextToUpdate)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateStatusLabelSetUp(TextToUpdate)));
            else
                InternalUpdateStatusLabelSetUp(TextToUpdate);
        }
        private void InternalUpdateStatusLabelSetUp(string TextUpdated)
        {
            try
            {
                StatusLabelSetUp.Text = TextUpdated;
                var vCheckErrosStatus = TextUpdated.Split(new[] { ';' });
            }
            catch (NullReferenceException) { }
        }
        #endregion

        #region " Definition Chart "
        private void DefinitionChart()
        {

            ChartSetUp.Series.Add("Lll");
            ChartSetUp.Series.Add("Ull");
            ChartSetUp.Series.Add("ErrorLoad");

            ChartSetUp.Series["Lll"].XValueType = ChartValueType.Double;
            ChartSetUp.Series["Lll"].ChartType = SeriesChartType.Spline;
            ChartSetUp.Series["Lll"].XValueMember = "Xserie1";
            ChartSetUp.Series["Lll"].YValueMembers = "Yserie1";

            ChartSetUp.Series["Ull"].XValueType = ChartValueType.Double;
            ChartSetUp.Series["Ull"].ChartType = SeriesChartType.Spline;
            ChartSetUp.Series["Ull"].XValueMember = "Xserie2";
            ChartSetUp.Series["Ull"].YValueMembers = "Yserie2";

            ChartSetUp.Series["ErrorLoad"].XValueType = ChartValueType.Double;
            ChartSetUp.Series["ErrorLoad"].ChartType = SeriesChartType.Spline;
            ChartSetUp.Series["ErrorLoad"].XValueMember = "Xserie3";
            ChartSetUp.Series["ErrorLoad"].YValueMembers = "Yserie3";

            ChartSetUp.DataSource = LDynamicalData;

        }
        #endregion

        private void RefreshChart()
        {
            ChartSetUp.DataBind();
        }

        #region " Open File "
        private void OuvrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FillCycleDgvSetUp.Clear();

            PassStatusLabelSetUp = new DelegateStatusLabelSetUp(UpdateStatusLabelSetUp);

            openFileDialogSetUp.InitialDirectory = @"G:\BE\GENERAL\Etudes\FORCE\286010 (Thick film)\8 - Software\Ambiant";
            openFileDialogSetUp.Title = "Selectionnez le fichier Excel";
            openFileDialogSetUp.Filter = "Fichier Excel (*.xlsx)|*.xlsx|Tous fichier (*.*)|*.*";
            openFileDialogSetUp.FilterIndex = 1;
            openFileDialogSetUp.RestoreDirectory = true;

            if (openFileDialogSetUp.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                sNameOfFileCycleSetUp = openFileDialogSetUp.SafeFileName;
                sDefaultPathSetUp = openFileDialogSetUp.FileName.Substring(0, openFileDialogSetUp.FileName.LastIndexOf(@"\"));

                sDefaultPathSetUp = openFileDialogSetUp.FileName.Substring(0, openFileDialogSetUp.FileName.Length - sNameOfFileCycleSetUp.Length);
                RefreshDgvSetUp();

                PassStatusLabelSetUp("Chargement fichier : Ok");
                BtnLaunchAcquisition.Enabled = true;


            }
            else { PassStatusLabelSetUp("Pb lors du chargement du fichier de SetUp : NOk"); }  
        }
        #endregion

        #region " refresh dgv "
        private void RefreshDgvSetUp()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(sDefaultPathSetUp + sNameOfFileCycleSetUp);

            object[,] RangeCycle = null;
            object[,] RangeSequence = null;
            FillCycleDgvSetUp.Clear();

            try
            { RangeCycle = xlWorkbook.Names.Item(Index: "Cycle").RefersToRange.Value; }
            catch (Exception)
            { }

            try { RangeSequence = xlWorkbook.Names.Item(Index: "Sequency").RefersToRange.Value; }
            catch (Exception)
            { }

            try { iNbCycleSetUpToDo = Convert.ToInt32(xlWorkbook.Names.Item(Index: "NbCycle").RefersToRange.Value); }
            catch (Exception)
            { }

            //Conversion de array en list of array
            var RangeCycleConverted = Enumerable.Range(1, RangeCycle.GetLength(0)).Select(x => Enumerable.Range(1, RangeCycle.GetLength(1)).Select(y => RangeCycle[x, y]).ToArray()).ToList();
            var RangeSequenceConverted = Enumerable.Range(1, RangeSequence.GetLength(0)).Select(x => Enumerable.Range(1, RangeSequence.GetLength(1)).Select(y => RangeSequence[x, y]).ToArray()).ToList();

            foreach (var elem in RangeCycleConverted)
            {
                FillCycleDgvSetUp.Add(new ClassCycleSetUp
                {
                    Step = Convert.ToString(elem[0]),
                    Mode = Convert.ToString(elem[1]),
                    Palier = Convert.ToString(elem[2]),
                    Rampe = Convert.ToString(elem[3]),
                    Temp = Convert.ToString(elem[4]),
                    Load = Convert.ToString(elem[5]),
                    LwrLimit = Convert.ToString(elem[6]),
                    UprLimit = Convert.ToString(elem[7]),
                    Pred = Convert.ToString(elem[8])
                });
            }
            dgvSetUp.DataSource = new SortableBindingList<ClassCycleSetUp>(FillCycleDgvSetUp.ToList());

            dgvSetUp.Columns["Step"].Width = 50;
            dgvSetUp.Columns["Mode"].Width = 90;
            dgvSetUp.Columns["Palier"].Width = 50;
            dgvSetUp.Columns["Rampe"].Width = 50;
            dgvSetUp.Columns["Temp"].Width = 60;
            dgvSetUp.Columns["LwrLimit"].Width = 80;
            dgvSetUp.Columns["UprLimit"].Width = 80;
            dgvSetUp.Columns["Load"].Width = 90;
            dgvSetUp.Columns["Pred"].Width = 50;

            //Paramètre générals de la datagridView cycle
            for (int i = 0; i < FillCycleDgvSetUp.Take(1).Count(); i++)
            {
                dgvSetUp.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            xlWorkbook.Close(false);
        }
        #endregion

        #region " Selection du type de mesure en mode automatique"
        public void SelAutomatiqueTypeOfMeasureSetUp(string[] sIdConfig, int iId)
        {
            switch (Convert.ToInt32(sIdConfig[0]))
            {
                case 1:
                    NewInstanceOfClassRs232SetUp.ConfigCurrentAC(iId).ToString();
                    break;
                case 2:
                    NewInstanceOfClassRs232SetUp.ConfigCurrentDC(iId).ToString(); 
                    break;
                case 3:
                    NewInstanceOfClassRs232SetUp.ConfigVoltageAC(iId).ToString(); 
                    break;
                case 4:
                    NewInstanceOfClassRs232SetUp.ConfigVoltageDC(iId).ToString(); 
                    break;
                case 5:
                    NewInstanceOfClassRs232SetUp.ConfigResistance2wire(iId).ToString(); 
                    break;
                case 6:
                    NewInstanceOfClassRs232SetUp.ConfigResistance4wire(iId).ToString(); 
                    break;
                case 7:
                    NewInstanceOfClassRs232SetUp.ConfigTemperature(iId).ToString(); 
                    break;
                case 8:
                    NewInstanceOfClassRs232SetUp.ConfigFrequency(iId).ToString(); 
                    break;
                case 9:
                    NewInstanceOfClassRs232SetUp.ConfigPeriod(iId).ToString(); 
                    break;
            }
        }
        #endregion

        #region " switch ON doli "
        private void SwitchOnDoli()
        {
            try { NewClassOfDoliSetUp.DoliOn(); }
            catch (Exception) { }

            Thread.Sleep(2000);
            //On en profite pour remettre la tete tout la haut
            try { NewClassOfDoliSetUp.MiseEnApproche((DoPE.CTRL)Doli.DoliControl.Position, Doli.DoliVitesseMiseEnApproche.Five_mm_min, Doli.DoliPositionMiseEnApproche.Ten_mm); } //1mm/min d'approche & 1mm de destination
            catch (Exception) { }

            Thread.Sleep(2000);
        }
        #endregion

        #region "Switch Off Doli"
        private void Button1_Click(object sender, EventArgs e)
        {
            try { NewClassOfDoliSetUp.DoliOff(); }
            catch (Exception) { }
        }
        #endregion

        #region "StatusErrorRichTextBoxFromMesure"
        public void UpdateStatusErrorRichTextBoxFromMesureSetUp(List<ClassForResultSetUp> ListForError)
        {
            //lock(LockObjectError)
            //{
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateStatusErrorRichTextBoxFromMesureSetUp(ListForError)));
            else
                InternalUpdateStatusErrorRichTextBoxFromMesureSetUp(ListForError);
            //}
        }
        private void InternalUpdateStatusErrorRichTextBoxFromMesureSetUp(List<ClassForResultSetUp> ListForError)
        {
            //L'Id=0 correspond à l'équipement de test
            try
            {
                //sErrorMessageSetUp = sErrorMessageSetUp + ListForError.sMeasure + "\n"; 
            }
            catch (NullReferenceException) { }

            Status_Error_richTextBoxSetUp.AppendText(sErrorMessageSetUp);
            Status_Error_richTextBoxSetUp.ScrollToCaret();
        }
        #endregion

        #region " Delegate issue du banc de force "
        public void UpdateTbxPositionSetUp(string sMeasure)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateTbxPositionSetUp(sMeasure)));
            else
                InternalUpdateTbxPositionSetUp(sMeasure);
        }
        private void InternalUpdateTbxPositionSetUp(string sMeasure)
        {
            try
            { tbxPositionSetUp.Text = sMeasure; }
            catch (NullReferenceException) { }
        }

        public void UpdateTbxForceSetUp(string sMeasure)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateTbxForceSetUp(sMeasure)));
            else
                InternalUpdateTbxForceSetUp(sMeasure);
        }
        private void InternalUpdateTbxForceSetUp(string sMeasure)
        {
            try
            {tbxLoadSetUp.Text = sMeasure;}
            catch (NullReferenceException) { }
        }

        public void UpdateTbxExtensionSetUp(string sMeasure)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateTbxExtensionSetUp(sMeasure)));
            else
                InternalUpdateTbxExtensionSetUp(sMeasure);
        }
        private void InternalUpdateTbxExtensionSetUp(string sMeasure)
        {
            try
            { tbxExtensionSetUp.Text = sMeasure; }
            catch (NullReferenceException) { }
        }

        public void UpdateOnPosMsgSetUp(DoPE.ERR Error, bool bReached, double dTime, DoPE.CTRL Control, double dPosition, DoPE.CTRL DControl, double dDestination, short UsTan)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnPosMsgSetUp(Error, bReached, dTime, Control, dPosition, DControl, dDestination, UsTan)));
            else
                InternalUpdateOnPosMsgSetUp(Error, bReached, dTime, Control, dPosition, DControl, dDestination, UsTan);
        }
        private void InternalUpdateOnPosMsgSetUp(DoPE.ERR Error, bool bReached, double dTime, DoPE.CTRL Control, double dPosition, DoPE.CTRL DControl, double dDestination, short UsTan)
        {
            try
            {
                bTargetReachSetUp = bReached;
                ListOfOnPosMsgSetUp.Add(new ClassOnPosMasgSetUp { ErrorToClass = Error, ReachedToClass = bReached, TimeToClass = dTime, ControlToClass = Control, DControlToClass = DControl, DestinationToClass = dDestination, UsTanToClass = UsTan });
            }
            catch (NullReferenceException) { }
        }

        public void UpdateOnCommandErrorSetUp(double CommandNumber, DoPE.CMD_EERROR ErrorNumber, short UsTan)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnCommandErrorSetUp(CommandNumber, ErrorNumber, UsTan)));
            else
                InternalUpdateOnCommandErrorSetUp(CommandNumber, ErrorNumber, UsTan);
        }
        private void InternalUpdateOnCommandErrorSetUp(double CommandNumber, DoPE.CMD_EERROR ErrorNumber, short UsTan)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnCheckMsgSetUp(DoPE.ERR Error, bool bAction, double dTime, DoPE.CHK_ID CheckId, double dPosition, DoPE.SENSOR SensorNo, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnCheckMsgSetUp(Error, bAction, dTime, CheckId, dPosition, SensorNo, usTAN)));
            else
                InternalUpdateOnCheckMsgSetUp(Error, bAction, dTime, CheckId, dPosition, SensorNo, usTAN);
        }
        private void InternalUpdateOnCheckMsgSetUp(DoPE.ERR Error, bool bAction, double dTime, DoPE.CHK_ID CheckId, double dPosition, DoPE.SENSOR SensorNo, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnShieldMsgSetUp(DoPE.ERR Error, bool bAction, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnShieldMsgSetUp(Error, bAction, dTime, SensorNo, dPosition, usTAN)));
            else
                InternalUpdateOnShieldMsgSetUp(Error, bAction, dTime, SensorNo, dPosition, usTAN);
        }
        private void InternalUpdateOnShieldMsgSetUp(DoPE.ERR Error, bool bAction, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnRefSignalMsgSetUp(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnRefSignalMsgSetUp(Error, dTime, SensorNo, dPosition, usTAN)));
            else
                InternalUpdateOnRefSignalMsgSetUp(Error, dTime, SensorNo, dPosition, usTAN);
        }
        private void InternalUpdateOnRefSignalMsgSetUp(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTANsMeasure)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnSensorMsgSetUp(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnSensorMsgSetUp(Error, dTime, SensorNo, usTAN)));
            else
                InternalUpdateOnSensorMsgSetUp(Error, dTime, SensorNo, usTAN);
        }
        private void InternalUpdateOnSensorMsgSetUp(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnIoSHaltMsgSetUp(DoPE.ERR Error, short Upper, double dTime, DoPE.CTRL Control, double dPosition, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnIoSHaltMsgSetUp(Error, Upper, dTime, Control, dPosition, usTAN)));
            else
                InternalUpdateOnIoSHaltMsgSetUp(Error, Upper, dTime, Control, dPosition, usTAN);
        }
        private void InternalUpdateOnIoSHaltMsgSetUp(DoPE.ERR Error, short Upper, double dTime, DoPE.CTRL Control, double dPosition, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnKeyMsgSetUp(DoPE.ERR Error, double dTime, long Keys, long NewKeys, long GoneKeys, short OemKeys, short NewOemKeys, short GoneOemKeys, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnKeyMsgSetUp(Error, dTime, Keys, NewKeys, GoneKeys, OemKeys, NewOemKeys, GoneOemKeys, usTAN)));
            else
                InternalUpdateOnKeyMsgSetUp(Error, dTime, Keys, NewKeys, GoneKeys, OemKeys, NewOemKeys, GoneOemKeys, usTAN);
        }
        private void InternalUpdateOnKeyMsgSetUp(DoPE.ERR Error, double dTime, long Keys, long NewKeys, long GoneKeys, short OemKeys, short NewOemKeys, short GoneOemKeys, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnRuntimeErrorSetUp(DoPE.ERR Error, DoPE.RTE ErrorNumber, double dTime, short Device, short Bits, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnRuntimeErrorSetUp(Error, ErrorNumber, dTime, Device, Bits, usTAN)));
            else
                InternalUpdateOnRuntimeErrorSetUp(Error, ErrorNumber, dTime, Device, Bits, usTAN);
        }
        private void InternalUpdateOnRuntimeErrorSetUp(DoPE.ERR Error, DoPE.RTE ErrorNumber, double dTime, short Device, short Bits, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnOverflowSetUp(int iOverflow)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnOverflowSetUp(iOverflow)));
            else
                InternalUpdateOnOverflowSetUp(iOverflow);
        }
        private void InternalUpdateOnOverflowSetUp(int iOverflow)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnDebugMsgSetUp(DoPE.ERR Error, DoPE.DEBUG MsgType, double dTime, string sText)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnDebugMsgSetUp(Error, MsgType, dTime, sText)));
            else
                InternalUpdateOnDebugMsgSetUp(Error, MsgType, dTime, sText);
        }
        private void InternalUpdateOnDebugMsgSetUp(DoPE.ERR Error, DoPE.DEBUG MsgType, double dTime, string sText)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnSystemMsgSetUp(DoPE.ERR Error, DoPE.SYSTEM_MSG MsgNumber, double dTime, string sText)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnSystemMsgSetUp(Error, MsgNumber, dTime, sText)));
            else
                InternalUpdateOnSystemMsgSetUp(Error, MsgNumber, dTime, sText);
        }
        private void InternalUpdateOnSystemMsgSetUp(DoPE.ERR Error, DoPE.SYSTEM_MSG MsgNumber, double dTime, string sText)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnRmcEventSetUp(long Keys, long NewKeys, long GoneKeys, long Leds, long NewLeds, long GoneLeds)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnRmcEventSetUp(Keys, NewKeys, GoneKeys, Leds, NewLeds, GoneLeds)));
            else
                InternalUpdateOnRmcEventSetUp(Keys, NewKeys, GoneKeys, Leds, NewLeds, GoneLeds);
        }
        private void InternalUpdateOnRmcEventSetUp(long Keys, long NewKeys, long GoneKeys, long Leds, long NewLeds, long GoneLeds)
        {
            try
            { }
            catch (NullReferenceException) { }
        }
        #endregion

        #region " Init Mesure " 
        private void ToolStripMenuItemInit_Click(object sender, EventArgs e)
        {
            PassStatusLabelSetUp = new DelegateStatusLabelSetUp(UpdateStatusLabelSetUp);
            /*
            if (dgvSetUp.Rows.Count > 0)
            {
                ClassVariablesGlobales.iCountMeasureSetUp = 0;
                ClassVariablesGlobales.bLaunhMeasurement = true;

                   //Partie destinée à la gestion du banc de force
                    try
                    {
                        NewClassOfDoliSetUp.PassStatusErrorRichTextBox = new DelegateStatusErrorRichTextBoxFromMesure(UpdateStatusErrorRichTextBoxFromMesureSetUp);
                        NewClassOfDoliSetUp.PassTbxPosition = new DelegateTbxPosition(UpdateTbxPositionSetUp);
                        NewClassOfDoliSetUp.PassTbxForce = new DelegateTbxForce(UpdateTbxForceSetUp);
                        NewClassOfDoliSetUp.PassTbxExtension = new DelegateTbxExtension(UpdateTbxExtensionSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnPosMsg = new DelegateOnPosMsgSetUp(UpdateOnPosMsgSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnCommandError = new DelegateOnCommandErrorSetUp(UpdateOnCommandErrorSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnCheckMsg = new DelegateOnCheckMsgSetUp(UpdateOnCheckMsgSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnShieldMsg = new DelegateOnShieldMsgSetUp(UpdateOnShieldMsgSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnRefSignalMsg = new DelegateOnRefSignalMsgSetUp(UpdateOnRefSignalMsgSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnSensorMsg = new DelegateOnSensorMsgSetUp(UpdateOnSensorMsgSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnIoSHaltMsg = new DelegateOnIoSHaltMsgSetUp(UpdateOnIoSHaltMsgSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnKeyMsg = new DelegateOnKeyMsgSetUp(UpdateOnKeyMsgSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnRuntimeError = new DelegateOnRuntimeErrorSetUp(UpdateOnRuntimeErrorSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnOverflow = new DelegaeOnOverflowSetUp(UpdateOnOverflowSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnDebugMsg = new DelegateOnDebugMsgSetUp(UpdateOnDebugMsgSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnSystemMsg = new DelegateOnSystemMsgSetUp(UpdateOnSystemMsgSetUp);
                        NewClassOfDoliSetUp.PassDelegateOnRmcEvent = new DelegateOnRmcEventSetUp(UpdateOnRmcEventSetUp);

                        NewClassOfDoliSetUp.ConnectToEdc();
                    }
                    catch (Exception Ex)
                    { }

                CloseAccessToTools();

                foreach (ClassSequenceSetUp Elem in listForDgvMeasureSetUp)
                {
                    if (Elem.Id == "1")
                    {
                        ListMesureSetUp.Add(new ClassForIdSetUp
                        {
                            Id = Convert.ToInt32(Elem.Id),
                            Config = NewclassK2000ForMainSetUp.GetMeasureId().First(x => x.Value.Equals(Elem.Type)).Key,
                            sDesignation = Elem.Designation
                        });

                        //Permet de positionner le Keithley en mode Acquisition continue
                        string[] sTemp = new string[1];
                        sTemp[0] = ListMesureSetUp.Where(x => x.Id == 1).Select(y => y.Config).Single().ToString();
                        SelAutomatiqueTypeOfMeasureSetUp(sTemp, 1);

                        string sOutputData;
                        NewclassK2000ForMainSetUp.SendCommand(1, "INIT:CONT ON", out sOutputData, Convert.ToInt32(1000)).ToString();

                        //Nouvelle Instance de la ClassMesure
                        NewInstanceOfClassMesureForSetUp.NewclassK2000ForThreadMeasure = NewclassK2000ForMainSetUp;
                        NewInstanceOfClassMesureForSetUp.PassOneShotMeasurementOuputDataTextBox = new DelegateOneShotMeasurementOuputDataTextBox(UpdateOneShotMeasurementOuputDataTextBox);
                        NewInstanceOfClassMesureForSetUp.ListForMeasure = ListForMeasureSetUp;
                    }
                }

                MyElapsedMeasureTimerSetUp = new System.Timers.Timer(Convert.ToDouble((1 / 2) * 1000)); //2Hz

                MyElapsedMeasureTimerSetUp.AutoReset = true;
                MyElapsedMeasureTimerSetUp.Enabled = true;
                MyElapsedMeasureTimerSetUp.Elapsed += myEventSetUp;

                //On Doli
                SwitchOnDoli();

                PassStatusLabelSetUp("Init : Ok");
            }
            else
            {
                PassStatusLabelSetUp("Problème d'initialisation : NOk");
                MessageBox.Show("La table n'est pas présente !");
            }
            */
        }
        #endregion

        #region " My Event "
        private void MyEventSetUp(object source, ElapsedEventArgs e)
        {
            newclassOfVGSetUp.lStep += 1;

            var vIndex = dgvSetUp.SelectedRows;

            Thread ThreadMyEventSetUp = new Thread(() =>
            {
                if (vIndex.Count > 0)
                {
                    //A chaque event on stock les informations
                    ListForMeasurePosSetUp.Add(new ClassForMesureDOliSetUp
                    {
                        Dt = DateTime.Now,
                        Rampe = Convert.ToDouble(vIndex[0].Cells[3].Value),
                        Palier = Convert.ToDouble(vIndex[0].Cells[2].Value),
                        Temp = Convert.ToDouble(vIndex[0].Cells[3].Value),
                        LoadConsigne = Convert.ToDouble(vIndex[0].Cells[5].Value),
                        Load = Convert.ToDouble(tbxLoadSetUp.Text),
                        Position = Convert.ToDouble(tbxPositionSetUp.Text),
                        Lll = Convert.ToDouble(vIndex[0].Cells[6].Value),
                        Upl = Convert.ToDouble(vIndex[0].Cells[7].Value),
                        Step = newclassOfVGSetUp.lStep
                    });
                }

                Thread LaunchMLesureSetUp = new Thread(() => { NewInstanceOfClassMesureForSetUp.Mesure(1, newclassOfVGSetUp.lStep); });
                LaunchMLesureSetUp.Start();

            })
            {
                Priority = ThreadPriority.AboveNormal
            };
            ThreadMyEventSetUp.Start();
        }
        #endregion

        #region " GO............ "
        private void BtnLaunchAcquisition_Click(object sender, EventArgs e)
        {
            if (dgvSetUp.Rows.Count > 0)
            {
                //On fait la tare en Load et en Position
                try { NewClassOfDoliSetUp.DoliTareLoad(Convert.ToDouble(tbxLoadSetUp.Text)); }
                catch (Exception)
                {
                    //Mettre une action bloquante 
                }

                //Mise a l'approche
                try { NewClassOfDoliSetUp.MiseEnApproche((DoPE.CTRL)Doli.DoliControl.Position, Doli.DoliVitesseMiseEnApproche.One_mm_min, Doli.DoliPositionMiseEnApproche.One_mm); } //1mm/min d'approche & 1mm de destination
                catch (Exception) { }

                Thread.Sleep(2000);
                bTargetReachSetUp = false;

                dgvSetUp.Rows[0].Selected = true;
                dgvSetUp.FirstDisplayedScrollingRowIndex = dgvSetUp.Rows[0].Index;
                var vIndex = dgvSetUp.SelectedRows;

                //Déclaration des event on timer
                MyElapsedLaunchingSetUp = new System.Timers.Timer(1000);
                MyElapsedDoliTargetLoadSetUp = new System.Timers.Timer(100)
                {
                    Enabled = true
                };
                MyElapsedLaunchingSetUp.Enabled = true;

                MyElapsedDoliTargetLoadSetUp.AutoReset = true;
                MyElapsedLaunchingSetUp.AutoReset = true;

                MyElapsedDoliTargetLoadSetUp.Elapsed += MyEventDoliTargetSetUp;
                MyElapsedLaunchingSetUp.Elapsed += MyEventForTimerFromLaunchingSetUp;
                //MyElapsedCycleTimer.Elapsed += myEventForCycle;

                double dTimePalier = -Convert.ToDouble(vIndex[0].Cells[2].Value);
                double dTargetSpeed = Convert.ToDouble(vIndex[0].Cells[3].Value) * 1000;
                double dTargetLoad = -Convert.ToDouble(vIndex[0].Cells[5].Value);

                dtLaunchCycleTimeSetUp = DateTime.Now;
                NewClassOfDoliSetUp.MiseEnApproche(DoPE.CTRL.LOAD, dTargetSpeed, dTargetLoad); 
            }
            else
            { MessageBox.Show("La table n'est pas présente, ou le cycle d'acquisition n'a pas encore été lancé !"); }
        }
        #endregion

        #region " Event pour tester si la position de Doli est atteinte "
        private void MyEventDoliTargetSetUp(object source, ElapsedEventArgs e)
        {
            try
            {
                if (bTargetReachSetUp)
                {
                    MyElapsedDoliTargetLoadSetUp.Stop(); // Et on stop  le timer en cours
                    //On le remet à zéro pour la prochaine fois
                    bTargetReachSetUp = false;
                    var vIndex = dgvSetUp.SelectedRows;

                    //On basclue le flag pour les mesures en moyenne durant toute la durée du palier
                    //bFlagForAverageMeasureSetUp = true;

                    MyElapsedPalierSetUp = new System.Timers.Timer(1000)
                    {
                        Enabled = true,
                        AutoReset = true
                    };
                    dtForPalierSetUp = DateTime.Now.AddSeconds(Convert.ToDouble(vIndex[0].Cells[2].Value));
                    MyElapsedPalierSetUp.Elapsed += delegate (object source2, ElapsedEventArgs e2) { MyEventForCountPalierSetUp(source, e, dtForPalierSetUp); };

                    MyElapsedCycleTimerSetUp = new System.Timers.Timer(Convert.ToDouble(vIndex[0].Cells[2].Value) * 1000)
                    {
                        Enabled = true,
                        AutoReset = false
                    };
                    MyElapsedCycleTimerSetUp.Elapsed += MyEventSetUp;
                    MyElapsedCycleTimerSetUp.Start();
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " My event pour le décompte des paliers"
        private void MyEventForCountPalierSetUp(object source, ElapsedEventArgs e, DateTime dt)
        {
            PassTimePalierSetUp = new DelegatePassTimePalierSetUp(UpdateTimePalierSetUp);
            PassTimePalierSetUp(dt);
        }

        public void UpdateTimePalierSetUp(DateTime dt)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateTimePalierSetUp(dt)));
            else
                InternalUpdateTimePalierSetUp(dt);
        }
        private void InternalUpdateTimePalierSetUp(DateTime dt)
        {
            TimeSpan Ts = dt - DateTime.Now;
            if ((Convert.ToDouble(dt.ToOADate()) - Convert.ToDouble(DateTime.Today.ToOADate())) > 0)
            { lblTimePalierSetUp.Text = Ts.Hours.ToString("00") + ":" + Ts.Minutes.ToString("00") + ":" + Ts.Seconds.ToString("00"); }
        }
        #endregion

        #region " Event pour affichier le comtage en temps "
        private void MyEventForTimerFromLaunchingSetUp(object source, ElapsedEventArgs e)
        {
            PassTimerCountSetUp = new DelegatePassTimerCountSetUp(UpdateTimeSetUp);
            PassTimerCountSetUp(DateTime.Now - dtLaunchCycleTimeSetUp);
        }

        public void UpdateTimeSetUp(TimeSpan dt)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateTimeSetUp(dt)));
            else
                InternalUpdateTimeSetUp(dt);
        }
        private void InternalUpdateTimeSetUp(TimeSpan dt)
        {
            lblTimeSetUp.Text = dt.Hours.ToString("00") + ":" + dt.Minutes.ToString("00") + ":" + dt.Seconds.ToString("00");
        }
        #endregion
    }
}
