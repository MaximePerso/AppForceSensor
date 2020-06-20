using Doli.DoPE;
using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace K2000Rs232App
{
    public partial class MainWindow : Form
    {
        #region " Init"
        public delegate void DelegateOneShotMeasurementResultTextBoxFromMesure(List<ClassForResult> ListForResult);
        public delegate void DelegateOneShotMeasurementOuputDataTextBox(int iId,int iStep, string sMeasure);
        public delegate void DelegateOneShotMeasurementEtuveBE(int iId,int iStep, string sConsigne, string sMeasure);
        public delegate void DelegateStatusErrorRichTextBoxFromMesure(List<ClassForResult> ListForError);

        public delegate void DelegateStatusErrorRichTextBox();
        public DelegateStatusErrorRichTextBox PassStatusErrorRichTextBox { get; private set; }

        public delegate void DelegateTbxDecompteMesure(string sValue);
        public DelegateTbxDecompteMesure PassTbxDecompteMesure { get; private set; }

        public delegate void DelegateWriteToDocFileData();
        public DelegateWriteToDocFileData PassWriteToDocFileData { get; set; }

        public delegate void DelegateWriteToDocFileAverage();
        public DelegateWriteToDocFileAverage PassWriteToDocFileAverage { get; set; }

        public delegate void DelegateTbxCycle(double iGetNextIdex);
        public DelegateTbxCycle PassTbxCycle { get; set; }

        public delegate void DelegatePasssMessageForStatusLabel(string sMessage);
        public DelegatePasssMessageForStatusLabel PasssMessageForStatusLabel { get; set; }

        public delegate void DelegatePassTimerCount(TimeSpan dt);
        public DelegatePassTimerCount PassTimerCount { get; set; }

        public delegate void DelegatePassInitPalier(DateTime dt);
        public DelegatePassInitPalier PassInitTimePalier { get; set; }

        public delegate void DelegatePassTimePalier();
        public DelegatePassTimePalier PassTimePalier { get; set; }

        public delegate void DelegatePassFirstRowVisibleOnDgv(int iIndex);
        public DelegatePassFirstRowVisibleOnDgv PassFirstRowVisibleOnDgv { get; set; }

        public delegate void DelegatePassCountOfCycle(string sComptageCycle);
        public DelegatePassCountOfCycle PassCountOfCycle { get; set; }

        public delegate void DelegateStopCycle();
        public DelegateStopCycle PassStopCycle { get; private set; }

        public delegate void DelegateStatusLabel(string sValue);
        public DelegateStatusLabel PassStatusLabel { get; private set; }

        public delegate void DelegateEnabledSequencialPartToTrue();
        public DelegateEnabledSequencialPartToTrue PassEnabledSequencialPartToTrue { get; private set; }

        //Chart
        private string sAppDir = Environment.CurrentDirectory + @"\Ressources\Chart.html";
        public delegate void DelegateLaunching();

        public delegate void DelegateLaunchWb();
        public DelegateLaunchWb PassInvokeLaunchWb { get; set; }

        public delegate void DelegateUpdateConsigneLoad(string sLoadMesure);
        public DelegateUpdateConsigneLoad PassInvokeUpdateConsigneLoad { get; set; }

        public delegate void DelegateUpdateMesurePosition(string sLoadMesure);
        public DelegateUpdateMesurePosition PassInvokeUpdateMesurePosition { get; set; }

        public delegate void DelegateUpdateMesureLoad(string sLoadMesure);
        public DelegateUpdateMesureLoad PassInvokeUpdateMesureLoad { get; set; }

        public delegate void DelegateUpdateId1(int iStep,string sLoadMesure);
        public DelegateUpdateId1 PassInvokeUpdateMesureId1 { get; set; }

        public delegate void DelegateUpdateId2(int iStep, string sLoadMesure);
        public DelegateUpdateId2 PassInvokeUpdateMesureId2 { get; set; }

        public delegate void DelegateUpdateId3(int iStep, string sLoadMesure);
        public DelegateUpdateId3 PassInvokeUpdateMesureId3 { get; set; }

        public delegate void DelegateUpdateId4(int iStep, string sLoadMesure);
        public DelegateUpdateId4 PassInvokeUpdateMesureId4 { get; set; }

        public delegate void DelegateUpdateId5(int iStep, string sConsigne,string sMesure);
        public DelegateUpdateId5 PassInvokeUpdateMesureId5 { get; set; }

        public delegate void DelegateIntervalDataLength(string sInterval, string sDataLength);
        public DelegateIntervalDataLength PassInvokeIntervalDataLength { get; set; }

        public delegate void DelegateStopChart();
        public DelegateStopChart PassInvokeStopChart{ get; set; }

        public delegate void DelegateUpdateErrorLoadChart(List<ClassAverageMeasure> lErrorLoad, bool bTest);
        public DelegateUpdateErrorLoadChart PassInvokeUpdateErrorLoadChart { get; set; }

        public delegate void DelegateUpdateSignalChart();
        public DelegateUpdateSignalChart PassInvokeUpdateSignalChart { get; set; }

        public delegate void DelegateFinDeTraitement(bool ReadyForFinish, Excel.Application xlApp, Excel.Worksheet xlWorkSheetAnalyse, Excel.Workbook xlWorkBook);

        //Delegate pour Doli
        public delegate void DelegateTbxPosition(string sMeasure);
        public delegate void DelegateTbxForce(string sMeasure);
        public delegate void DelegateOnPosMsg(DoPE.ERR Error, bool bReached, double dTime, DoPE.CTRL Control, double dPosition, DoPE.CTRL DControl, double dDestination, short UsTan);
        public delegate void DelegateOnCommandError(double CommandNumber, DoPE.CMD_EERROR ErrorNumber, short UsTan);
        public delegate void DelegateOnSftMsg(DoPE.ERR Error, short Upper, double dTime, DoPE.CTRL Control, double dPosition, short usTAN);
        public delegate void DelegateOnOffsCMsg(DoPE.ERR Error, double dTime, double dOffset, short usTAN);
        public delegate void DelegateOnCheckMsg(DoPE.ERR Error, bool bAction, double dTime, DoPE.CHK_ID CheckId, double dPosition, DoPE.SENSOR SensorNo, short usTAN);
        public delegate void DelegateOnShieldMsg(DoPE.ERR Error, bool bAction, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTAN);
        public delegate void DelegateOnRefSignalMsg(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTAN);
        public delegate void DelegateOnSensorMsg(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, short usTAN);
        public delegate void DelegateOnIoSHaltMsg(DoPE.ERR Error, short Upper, double dTime, DoPE.CTRL Control, double dPosition, short usTAN);
        public delegate void DelegateOnKeyMsg(DoPE.ERR Error, double dTime, long Keys, long NewKeys, long GoneKeys, short OemKeys, short NewOemKeys, short GoneOemKeys, short usTAN);
        public delegate void DelegateOnRuntimeError(DoPE.ERR Error, DoPE.RTE ErrorNumber, double dTime, short Device, short Bits, short usTAN);
        public delegate void DelegaeOnOverflow(int iOverflow);
        public delegate void DelegateOnDebugMsg(DoPE.ERR Error, DoPE.DEBUG MsgType, double dTime, string sText);
        public delegate void DelegateOnSystemMsg(DoPE.ERR Error, DoPE.SYSTEM_MSG MsgNumber, double dTime, string sText);
        public delegate void DelegateOnRmcEvent(long Keys, long NewKeys, long GoneKeys, long Leds, long NewLeds, long GoneLeds);

        //delegate vers la form chart
        public delegate void PassErreurLoadLimit(System.Data.DataTable dt);

        //Timers
        public System.Timers.Timer MyElapsedMeasureTimer;
        public System.Timers.Timer MyElapsedCycleTimer;
        public System.Timers.Timer MyElapsedDoliTargetLoad;
        public System.Timers.Timer MyElapsedForWriteToDoc;
        public System.Timers.Timer MyElapsedForMesureEtuveBe;
        public System.Timers.Timer MyElapsedForRbob;

        //Date time
        private DateTime dtLaunchCycleTime = new DateTime();
        private DateTime dtForPalier = new DateTime();
        private DateTime dtTargetTimePalier = new DateTime();

        private DateTime DtDebug = new DateTime();
        private TimeSpan TsDebug = new TimeSpan();
        List<TimeSpan> lTs = new List<TimeSpan>();

        //Boolean
        private bool bFlagInit = false;
        private bool bFlagGo = false;
        bool bFlagForAverageMeasure = false;
        private bool bTargetReach = false;
        private bool bFlagDecompteCycleReady = false;

        //integer
        private int iNbCycleDone = 1;
        private int iNbCycleToDo = 1;
        private int iNbCycleAlreadyDone = 1;
        private string sModeCycle = "";
        public int iDecomptePassageEventForPalier = 1;

        //String
        public string sDefaultPath;
        public string sDefaultPathMeasure;
        public string sNameOfFileCycle = "Cycle.txt";
        public string sNameOfFileMeasure = "Result_Sn_.txt";
        public string sPathForDataCsvFile;
        public string sPathForDataAverageCsvFile;
        private string sXmlFileName = @"\Resources\ConfigData.xml";
        public string sErrorMessage = "";

        //List
        List<EventHandler> delegates = new List<EventHandler>();
        public List<string> ListStoreLoad = null;
        List<ClassForId> ListMesure = new List<ClassForId>();
        List<ClassForId> ListId1Mesure = new List<ClassForId>();
        List<ClassForId> ListId2Mesure = new List<ClassForId>();
        List<ClassForId> ListId3Mesure = new List<ClassForId>();
        List<ClassForId> ListId4Mesure = new List<ClassForId>();
        List<ClassAverageMeasure> ListForAverageMeasure = new List<ClassAverageMeasure>();
        List<string> ListIntersect = new List<string>();
        private List<ClassCycle> FillCycleDgv = new List<ClassCycle>();
        private List<ClassSequence> listForDgvMeasure = new List<ClassSequence>();
        private List<ClassOnPosMasg> ListOfOnPosMsg = new List<ClassOnPosMasg>();
        private List<BlankMeasure> ListOfBlank = new List<BlankMeasure>();
        private List<BlankMeasure> ListOfStepMissing = new List<BlankMeasure>();
        private List<BlankMeasure> ListOfStepDoublons = new List<BlankMeasure>();

        public List<ClassForThreadMeasure> ListForMeasureId1 = new List<ClassForThreadMeasure>();
        public List<ClassForThreadMeasure> ListForMeasureId2 = new List<ClassForThreadMeasure>();
        public List<ClassForThreadMeasure> ListForMeasureId3 = new List<ClassForThreadMeasure>();
        public List<ClassForThreadMeasure> ListForMeasureId4 = new List<ClassForThreadMeasure>();
        public List<ClassForThreadMeasure> ListForMeasureId5 = new List<ClassForThreadMeasure>();
        public List<ClassForThreadMeasure> ListForMeasureId10 = new List<ClassForThreadMeasure>();
        public List<ClassForThreadMeasure> ListForTargetReachId5 = new List<ClassForThreadMeasure>();
        public List<ClassForData> ListForMeasurePos = new List<ClassForData>();

        public List<ClassForData> ListForMeasureEtuveBe = new List<ClassForData>();

        //partie pour la gestion TCP
        TcpClient TcpConnectEtuveBe = new TcpClient();

        //Instance de classe
        public ClassVariablesGlobales newclassOfVG = new ClassVariablesGlobales();
        private ClassConfiguration NewClassConfiguration = new ClassConfiguration();
        private SetUp NewInstanceOfSetUpForm = new SetUp();
        private FormChart NewInstanceOfFormChart = new FormChart();
        public ClassRS232 NewInstanceOfClassRs232ForId1 = new ClassRS232();
        public ClassRS232 NewInstanceOfClassRs232ForId2 = new ClassRS232();
        public ClassRS232 NewInstanceOfClassRs232ForId3 = new ClassRS232();
        public ClassRS232 NewInstanceOfClassRs232ForId4 = new ClassRS232();
        public ClassRS232 NewInstanceOfClassRs232ForId10 = new ClassRS232();

        ClassMesure NewInstanceOfClassMesureForId1 = new ClassMesure();
        ClassMesure NewInstanceOfClassMesureForId2 = new ClassMesure();
        ClassMesure NewInstanceOfClassMesureForId3 = new ClassMesure();
        ClassMesure NewInstanceOfClassMesureForId4 = new ClassMesure();
        ClassMesure NewInstanceOfClassMesureForId10 = new ClassMesure();

        Doli NewClassOfDoli = new Doli();
        TcpClientWithTimeout NewInstanceOfClassTcp = new TcpClientWithTimeout("", 0);

        //Partie Excel
        Excel.Application xlApp = new Excel.Application();
        Excel.Application xlAppFromDelegate = null;
        Worksheet xlWorkSheetAnalyseFromdelegate = null;
        Workbook xlWorkBookFromDelegate = null;
        Workbook xlWorkBook;
        Worksheet xlWorkSheetAnalyse;
        Worksheet xlWorkSheetValue;

        static readonly object LockThreadMyEvent = new object();
        static readonly object LockThread= new object();
        static readonly object LockEtuveBe = new object();
        #endregion

        #region " Form Load "
        public MainWindow()
        {
            InitializeComponent();

            System.Windows.Forms.Application.DoEvents();

            //Test du statut du radiobutton
            Testrdb();

            //Screen aoc;
            //aoc.Show();
            //aoc.Location = Screen.AllScreens[INDEX OF YOUR AVAILABLE SCREENS TARGET].WorkingArea.Location;

            //On charge les combobox avec les données d ezla classe RS232
            Init_BaudRate_comboBox.DataSource = NewInstanceOfClassRs232ForId1.GetBaudRateList();
            Init_BaudRate_comboBox.SelectedIndex = 6;
            Init_DataBits_comboBox.DataSource = NewInstanceOfClassRs232ForId1.GetDataBitsList();
            Init_DataBits_comboBox.SelectedIndex = 3;
            Init_Parity_comboBox.DataSource = NewInstanceOfClassRs232ForId1.GetParityList();
            Init_Parity_comboBox.SelectedIndex = 0;
            Init_StopBits_comboBox.DataSource = NewInstanceOfClassRs232ForId1.GetStopBitsList();
            Init_StopBits_comboBox.SelectedIndex = 0;
            Init_StopBits_comboBox.DataSource = NewInstanceOfClassRs232ForId1.GetStopBitsList();
            Init_StopBits_comboBox.SelectedIndex = 0;
            Init_Handshake_comboBox.DataSource = NewInstanceOfClassRs232ForId1.GetHandshakeList();
            Init_Handshake_comboBox.SelectedIndex = 0;
            CbxTerminaison.DataSource = NewInstanceOfClassRs232ForId1.GetTerminaisonList();
            CbxTerminaison.SelectedIndex = 0;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            //Permet d'imposer le démarrage sur l'autre écran

            //Screen[] screens = Screen.AllScreens;
            //MainWindow.ActiveForm.Location = screens[1].WorkingArea.Location;

            //Init spécifique pour la klif 510510
            Init_Function(0);

            BtnSwitchOnDoli.Enabled = false;
            BtnSwitchOffDoli.Enabled = false;
            BtnOpenChart.Enabled = false;

            BtnSetUp.BackgroundImageLayout = ImageLayout.Stretch;
        }
        #endregion

        #region "Form Closed "
        private void MainWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (RdbBancDeForce.Checked)
                {
                    NewClassOfDoli.HaltDoli();
                    Thread.Sleep(500);

                    NewClassOfDoli.MiseEnApproche((DoPE.CTRL)Doli.DoliControl.Position, Doli.DoliVitesseMiseEnApproche.Twenty_mm_s, Doli.DoliPositionMiseEnApproche.Fifty_mm);
                    Thread.Sleep(5000);

                    NewClassOfDoli.HaltDoli();
                    Thread.Sleep(500);

                    NewClassOfDoli.DoliOff();

                    StopEtuve();
                }
            }
            catch (Exception) { }
        }
        #endregion

        #region " Classe "
        public class ClassForId
        {
            public int Id { get; set; }
            public int Config { get; set; }
            public string Designation { get; set; }
        }

        public class ClassForResult
        {
            public int Id { get; set; }
            public string Measure { get; set; }
            public string Designation { get; set; }
        }

        public class ClassForThreadMeasure
        {
            public int Id { get; set; }
            public string Measure { get; set; }
            public int Step { get; set; }
        }

        public class ClassForData
        {
            public DateTime Dt { get; set; }
            public double Rampe { get; set; }
            public double Palier { get; set; }
            public double TempConsigne { get; set; }
            public double TempMesure { get; set; }
            public double Position { get; set; }
            public double LoadMesure { get; set; }
            public int Step { get; set; }
            public double LoadConsigne { get; set; }
            public double Lll { get; set; }
            public double Upl { get; set; }
        }

        public class ClassForMesureK2000
        {
            public int Id { get; set; }
            public string Designation { get; set; }
            public double Measure { get; set; }
        }

        public class ClassCycle
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

        public class ClassSequence
        {
            public string Id { get; set; }
            public string Type { get; set; }
            public string Designation { get; set; }
        }

        public class ClassOnPosMasg
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

        public class ClassAverageMeasure
        {
            public int Step { get; set; }
            public string Mode { get; set; }
            public double Pos { get; set; }
            public double ConsigneTemp { get; set; }
            public double MeasureTemp { get; set; }
            public double ConsigneLoad { get; set; }
            public double MeasureLoad { get; set; }
            public double ErreurLoad { get; set; }
            public double ConversionLoad { get; set; }
            public double Min { get; set; }
            public double Max { get; set; }
            public double Lll { get; set; }
            public double Ull { get; set; }
            public double Id1 { get; set; }
            public double Id2 { get; set; }
            public double Id3 { get; set; }
            public double Id4 { get; set; }
        }

        public class ClassDynamicalData
        {
            public int Step { get; set; }
            public double Xserie1 { get; set; }
            public double Yserie1 { get; set; }
            public double Xserie2 { get; set; }
            public double Yserie2 { get; set; }
            public double Xserie3 { get; set; }
            public double Yserie3 { get; set; }
        }

        public class ClassToWriteDataToFile
        {
            public long Step { get; set; }
            public double Consigne { get; set; }
            public double Load { get; set; }
            public double Position { get; set; }
            public double MesureId1 { get; set; }
            public double MesureId2 { get; set; }
            public double MesureId3 { get; set; }
            public double MesureId4 { get; set; }
        }

        public class BlankMeasure
        {
            public int Id { get; set; }
            public int Index { get; set; }
            public int Step { get; set; }
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
                bFlagForAverageMeasure = false;

                if (MyElapsedMeasureTimer != null)
                { MyElapsedMeasureTimer.Stop(); }
                if (MyElapsedCycleTimer != null)
                { MyElapsedCycleTimer.Stop(); }

                init_groupBox.Enabled = true;

                tbxFile.Enabled = true;
                btnFile.Enabled = true;

                gbxDoli.Enabled = true;

                tbxFile.Enabled = true;
                btnFile.Enabled = true;
                cbxSn.Enabled = true;
                NudEchantillonageBancDeForce.Enabled = true;

                dgvSequenceMeasure.Enabled = true;
                dgvCycle.Enabled = true;

                RdbBancDeForce.Enabled = true;
                RdbEtuve.Enabled = true;
            }
            catch (NullReferenceException) { }
        }
        #endregion

        #region " Enabled = false " 
        private void CloseAccessToSequencialGroup()
        {
            //On remet a zéro le compteur 
            iDecomptePassageEventForPalier = 1;

            tbxFile.Enabled = false;
            btnFile.Enabled = false;
            init_groupBox.Enabled = false;

            gbxDoli.Enabled = false;

            tbxFile.Enabled = false;
            btnFile.Enabled = false;
            cbxSn.Enabled = false;
            NudEchantillonageBancDeForce.Enabled = false;

            dgvSequenceMeasure.Enabled = false;
            dgvCycle.Enabled = false;

            RdbBancDeForce.Enabled = false;
            RdbEtuve.Enabled = false;
        }
        #endregion

        #region " Bouton Halt Doli "
        private void BtnHaltDoli_Click_1(object sender, EventArgs e)
        {
            try { NewClassOfDoli.HaltDoli(); }
            catch (Exception) { }

            MyElapsedMeasureTimer.Stop();
            MyElapsedDoliTargetLoad.Stop();
            MyElapsedCycleTimer.Stop();
        }
        #endregion

        #region " Bouton Up Doli "
        private void BtnUpDoli_Click_1(object sender, EventArgs e)
        {
            try { NewClassOfDoli.MoveDoli(); }
            catch (Exception) { }
        }
        #endregion   

        #region " Gestion des init port com en cas de sélection en dehors de l'initialisation "
        private void ChkId1_CheckedChanged(object sender, EventArgs e) { TestCheckState(sender, NewInstanceOfClassRs232ForId1); }

        private void ChkId2_CheckedChanged(object sender, EventArgs e) { TestCheckState(sender, NewInstanceOfClassRs232ForId2); }

        private void ChkId3_CheckedChanged(object sender, EventArgs e) { TestCheckState(sender, NewInstanceOfClassRs232ForId3); }

        private void ChkId4_CheckedChanged(object sender, EventArgs e) { TestCheckState(sender, NewInstanceOfClassRs232ForId4); }

        private void ChkId10_CheckedChanged(object sender, EventArgs e) { TestCheckState(sender, NewInstanceOfClassRs232ForId10); }

        private void TestCheckState(object sender, ClassRS232 ClassForId)
        {
            string sMessage = "";
            int iError = 0;
            string sAction = "";

            System.Windows.Forms.CheckBox Chk = sender as System.Windows.Forms.CheckBox;

            int Id = Convert.ToInt32(Chk.Text.Last().ToString());

            if (Chk.Checked == true)
            { Init_Function(Id); sAction = " on init : " + Chk.Text; }
            else
            { iError = ClassForId.ClosePortComById(Id); sAction = " on close : " + Chk.Text; }

            sMessage = ClassForId.GetDLLErrorMessage(iError) + sAction + "\n"; // récupère le statut ou le message d'erreur
            Status_Error_richTextBox.Text = sMessage;
        }
        #endregion

        #region" Initialisation des ports Com "
        private void Init_Function(int iNewPort)
        {
            string sMessage = "";
            string sError = "";
            string sTerminaison = "";

            if (CbxTerminaison.Text != "")
            {
                if (CbxTerminaison.Text == "LF")
                    sTerminaison = "\n";
                else if (CbxTerminaison.Text == "CR")
                    sTerminaison = "\r";
                else if (CbxTerminaison.Text == "LFCR")
                    sTerminaison = "\n\r";
            }

            ClearAllResults();

            if (iNewPort == 0)
            {
                if (chkId1.Checked == true)
                {
                    sError = NewInstanceOfClassRs232ForId1.InitRS232(1,
                        Convert.ToInt32(Init_PortNumberId1.Value),
                        Convert.ToInt32(Init_BaudRate_comboBox.SelectedValue),
                        Convert.ToInt32(Init_DataBits_comboBox.SelectedValue),
                        Convert.ToInt32(Init_Parity_comboBox.SelectedValue),
                        Convert.ToInt32(Init_StopBits_comboBox.SelectedValue),
                        sTerminaison,
                        Convert.ToInt32(Init_Handshake_comboBox.SelectedValue)).ToString();

                    sMessage = NewInstanceOfClassRs232ForId1.GetDLLErrorMessage(Convert.ToInt32(sError)) + " : On init Id1" + "\n"; // récupère le statut ou le message d'erreur
                }

                if (chkId2.Checked == true)
                {
                    sError = NewInstanceOfClassRs232ForId2.InitRS232(2,
                        Convert.ToInt32(Init_PortNumberId2.Value),
                        Convert.ToInt32(Init_BaudRate_comboBox.SelectedValue),
                        Convert.ToInt32(Init_DataBits_comboBox.SelectedValue),
                        Convert.ToInt32(Init_Parity_comboBox.SelectedValue),
                        Convert.ToInt32(Init_StopBits_comboBox.SelectedValue),
                        sTerminaison,
                        Convert.ToInt32(Init_Handshake_comboBox.SelectedValue)).ToString();

                    sMessage = sMessage + NewInstanceOfClassRs232ForId2.GetDLLErrorMessage(Convert.ToInt32(sError)) + " : On init Id2" + "\n";
                }

                if (chkId3.Checked == true)
                {
                    sError = NewInstanceOfClassRs232ForId3.InitRS232(3,
                        Convert.ToInt32(Init_PortNumberId3.Value),
                        Convert.ToInt32(Init_BaudRate_comboBox.SelectedValue),
                        Convert.ToInt32(Init_DataBits_comboBox.SelectedValue),
                        Convert.ToInt32(Init_Parity_comboBox.SelectedValue),
                        Convert.ToInt32(Init_StopBits_comboBox.SelectedValue),
                        sTerminaison,
                        Convert.ToInt32(Init_Handshake_comboBox.SelectedValue)).ToString();

                    sMessage = sMessage + NewInstanceOfClassRs232ForId3.GetDLLErrorMessage(Convert.ToInt32(sError)) + " : On init Id3" + "\n"; // récupère le statut ou le message d'erreur
                }

                if (chkId4.Checked == true)
                {
                    sError = NewInstanceOfClassRs232ForId4.InitRS232(4,
                        Convert.ToInt32(Init_PortNumberId4.Value),
                        Convert.ToInt32(Init_BaudRate_comboBox.SelectedValue),
                        Convert.ToInt32(Init_DataBits_comboBox.SelectedValue),
                        Convert.ToInt32(Init_Parity_comboBox.SelectedValue),
                        Convert.ToInt32(Init_StopBits_comboBox.SelectedValue),
                        sTerminaison,
                        Convert.ToInt32(Init_Handshake_comboBox.SelectedValue)).ToString();

                    sMessage = sMessage + NewInstanceOfClassRs232ForId4.GetDLLErrorMessage(Convert.ToInt32(sError)) + " : On init Id4" + "\n"; // récupère le statut ou le message d'erreur
                }

                if (chkId10.Checked == true)
                {
                    //Pour l'alimentation TTI on doit redéfinir le time out en Read et en Write
                    NewInstanceOfClassRs232ForId10.RS232_READ_TIMEOUT = 5000;
                    NewInstanceOfClassRs232ForId10.RS232_WRITE_TIMEOUT = 5000;
                    //Ainsi que la taille des buffer

                    sError = NewInstanceOfClassRs232ForId10.InitRS232(10,
                        Convert.ToInt32(Init_PortNumberId10.Value),
                        Convert.ToInt32(Init_BaudRate_comboBox.SelectedValue),
                        Convert.ToInt32(Init_DataBits_comboBox.SelectedValue),
                        Convert.ToInt32(Init_Parity_comboBox.SelectedValue),
                        Convert.ToInt32(Init_StopBits_comboBox.SelectedValue),
                        "\n",
                        Convert.ToInt32(Init_Handshake_comboBox.SelectedValue)).ToString();

                    sMessage = sMessage + NewInstanceOfClassRs232ForId10.GetDLLErrorMessage(Convert.ToInt32(sError)) + " : On init Id10" + "\n"; // récupère le statut ou le message d'erreur
                }
            }
            /*
            else
            {
                int iPortNumber = 1;

                if (iNewPort == 1)
                { iPortNumber = Convert.ToInt32(Init_PortNumberId1.Value); }
                else if (iNewPort == 2)
                { iPortNumber = Convert.ToInt32(Init_PortNumberId2.Value); }
                else if (iNewPort == 3)
                { iPortNumber = Convert.ToInt32(Init_PortNumberId3.Value); }
                else if (iNewPort == 4)
                { iPortNumber = Convert.ToInt32(Init_PortNumberId4.Value); }
                else if (iNewPort == 5)
                { iPortNumber = Convert.ToInt32(Init_PortNumberId5.Value); }

                sError = NewInstanceOfClassRs232.InitRS232(iNewPort,
                    Convert.ToInt32(Init_PortNumberId1.Value),
                    Convert.ToInt32(Init_BaudRate_comboBox.SelectedValue),
                    Convert.ToInt32(Init_DataBits_comboBox.SelectedValue),
                    Convert.ToInt32(Init_Parity_comboBox.SelectedValue),
                    Convert.ToInt32(Init_StopBits_comboBox.SelectedValue),
                    sTerminaison,
                    Convert.ToInt32(Init_Handshake_comboBox.SelectedValue)).ToString();

                sMessage = sMessage + NewInstanceOfClassRs232.GetDLLErrorMessage(Convert.ToInt32(sError)) + " : On init Id" + iNewPort; // récupère le statut ou le message d'erreur
            }
            */
            Status_Error_richTextBox.Text = sMessage;
        }
        #endregion

        #region "Clear all result "
        private void ClearAllResults()
        {
            Status_Error_richTextBox.Text = "";
            lblResultId1.Text = "";
            lblResultId2.Text = "";
            lblResultId3.Text = "";
            lblResultId4.Text = "";
            tbxMesureId1.Text = "";
            tbxMesureId2.Text = "";
            tbxMesureId3.Text = "";
            tbxMesureId4.Text = "";
        }
        #endregion

        #region " Group Box Port Com "
        private void Init_button_Click(object sender, EventArgs e) { Init_Function(0); }
        #endregion

        #region "OneShotMeasurementOuputDataTextBoxFromMesure"
        public void UpdateOneShotMeasurementOuputDataTextBox(int iId, int iStep, string sMeasure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOneShotMeasurementOuputDataTextBox(iId, iStep, sMeasure)));
            else
                InternalUpdateOneShotMeasurementOuputDataTextBox(iId, iStep, sMeasure);
        }
        private void InternalUpdateOneShotMeasurementOuputDataTextBox(int iId,int iStep, string sMeasure)
        {
            lock(LockThread)
            {
                if (bFlagForAverageMeasure)
                {
                    //string stest = Sample.Sensor[(int)DoPE.SENSOR.SENSOR_S].ToString("0.000");
                }
                if (iId == 1)
                {
                    if (!Wb.Visible)
                    { tbxMesureId1.Text = sMeasure; }
                    else
                    {
                        //On passe l'info a la form Chart
                        Thread threadForUpdateId1 = new Thread(() => { UpdateId1(iStep, sMeasure); }) { Priority = ThreadPriority.BelowNormal };
                        threadForUpdateId1.Start();
                    }
                }

                if (iId == 2)
                {
                    if (!Wb.Visible)
                    { tbxMesureId2.Text = sMeasure; }
                    else
                    {
                        Thread threadForUpdateId2 = new Thread(() => { UpdateId2(iStep, sMeasure); }) { Priority = ThreadPriority.BelowNormal };
                        threadForUpdateId2.Start();
                    }
                }

                if (iId == 3)
                {
                    if (!Wb.Visible)
                    { tbxMesureId3.Text = sMeasure; }
                    else
                    {
                        Thread threadForUpdateId3 = new Thread(() => { UpdateId3(iStep, sMeasure); }) { Priority = ThreadPriority.BelowNormal };
                        threadForUpdateId3.Start();
                    }
                }

                if (iId == 4)
                {
                    if (!Wb.Visible)
                    { tbxMesureId4.Text = sMeasure; }
                    else
                    {
                        Thread threadForUpdateId4 = new Thread(() => { UpdateId4(iStep, sMeasure); }) { Priority = ThreadPriority.BelowNormal };
                        threadForUpdateId4.Start();
                    }
                }
            }
        }
        #endregion

        #region " OneShotMeasurementOneShotMeasurementEtuveBE "
        public void UpdateOneShotMeasurementEtuveBE(int iId, int iStep, string sConsigne, string sMeasure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOneShotMeasurementEtuveBE(iId, iStep, sConsigne, sMeasure)));
            else
                InternalUpdateOneShotMeasurementEtuveBE(iId, iStep, sConsigne, sMeasure);
        }
        private void InternalUpdateOneShotMeasurementEtuveBE(int iId, int iStep, string sConsigne, string sMeasure)
        {
            if (!Wb.Visible)
            {
                tbxConsigneEtuve.Text = sConsigne;
                tbxMesureEtuve.Text = sMeasure;
            }
            else
            {
                //On passe l'info a la form Chart
                Thread threadForUpdateId5 = new Thread(() => { UpdateId5(iStep,sConsigne, sMeasure); }) { Priority = ThreadPriority.BelowNormal };
                threadForUpdateId5.Start();
            }
        }
        #endregion

        #region "OneShotMeasurementResultTextBoxFromMesure"
        public void UpdateOneShotMeasurementResultTextBoxFromMesure(List<ClassForResult> ListForResult)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOneShotMeasurementResultTextBoxFromMesure(ListForResult)));
            else
                InternalUpdateOneShotMeasurementResultTextBoxFromMesure(ListForResult);
        }
        private void InternalUpdateOneShotMeasurementResultTextBoxFromMesure(List<ClassForResult> ListForResult)
        {
            try
            {
                try
                { lblResultId1.Text = ListForResult.Find(x => x.Id == 1).Measure; }
                catch (NullReferenceException) { }

                try
                { lblResultId2.Text = ListForResult.Find(x => x.Id == 2).Measure; }
                catch (NullReferenceException) { }

                try
                { lblResultId3.Text = ListForResult.Find(x => x.Id == 3).Measure; }
                catch (NullReferenceException) { }

                try
                { lblResultId4.Text = ListForResult.Find(x => x.Id == 4).Measure; }
                catch (NullReferenceException) { }
            }
            catch (NullReferenceException) { }
        }
        #endregion

        #region "StatusErrorRichTextBoxFromMesure"
        public void UpdateStatusErrorRichTextBoxFromMesure(List<ClassForResult> ListForError)
        {
            //lock(LockObjectError)
            //{
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateStatusErrorRichTextBoxFromMesure(ListForError)));
            else
                InternalUpdateStatusErrorRichTextBoxFromMesure(ListForError);
            //}
        }
        private void InternalUpdateStatusErrorRichTextBoxFromMesure(List<ClassForResult> ListForError)
        {
            //L'Id=0 correspond à l'équipement de test
            try
            { sErrorMessage = sErrorMessage + ListForError.Find(x => x.Id == 0).Measure + "\n"; }
            catch (NullReferenceException) { }

            try
            { sErrorMessage = sErrorMessage + ListForError.Find(x => x.Id == 1).Measure + "\n"; }
            catch (NullReferenceException) { }

            try
            { sErrorMessage = sErrorMessage + ListForError.Find(x => x.Id == 2).Measure + "\n"; }
            catch (NullReferenceException) { }

            try
            { sErrorMessage = sErrorMessage + ListForError.Find(x => x.Id == 3).Measure + "\n"; }
            catch (NullReferenceException) { }

            try
            { sErrorMessage = sErrorMessage + ListForError.Find(x => x.Id == 4).Measure + "\n"; }
            catch (NullReferenceException) { }

            try
            {
                Status_Error_richTextBox.AppendText(sErrorMessage);
                Status_Error_richTextBox.ScrollToCaret();
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Delegate issue du banc de force "
        public void UpdateTbxPosition(string sMeasure)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateTbxPosition(sMeasure)));
            else
                InternalUpdateTbxPosition(sMeasure);
        }
        private void InternalUpdateTbxPosition(string sMeasure)
        {
            try
            {
                TbxPosition.Text = sMeasure;

                Thread threadForUpdateMesurePosition = new Thread(() => { UpdateMesurePosition(TbxPosition.Text); })
                { Priority = ThreadPriority.BelowNormal };
                threadForUpdateMesurePosition.Start();
            }
            catch (NullReferenceException) { }
        }

        public void UpdateTbxForce(string sMeasure)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateTbxForce(sMeasure)));
            else
                InternalUpdateTbxForce(sMeasure);
        }
        private void InternalUpdateTbxForce(string sMeasure)
        {
            try
            {
                tbxLoad.Text = sMeasure;
                Thread threadForUpdateMesureLoad = new Thread(() => { UpdateMesureLoad(tbxLoad.Text); })
                { Priority = ThreadPriority.BelowNormal };
                threadForUpdateMesureLoad.Start();

                //OnActivated stock les valeurs au cas ou il y ai un rafraichissement non maitrisé
                ListStoreLoad.Add(sMeasure);
            }
            catch (NullReferenceException) { }
        }

        public void UpdateOnPosMsg(DoPE.ERR Error, bool bReached, double dTime, DoPE.CTRL Control, double dPosition, DoPE.CTRL DControl, double dDestination, short UsTan)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnPosMsg(Error, bReached, dTime, Control, dPosition, DControl, dDestination, UsTan)));
            else
                InternalUpdateOnPosMsg(Error, bReached, dTime, Control, dPosition, DControl, dDestination, UsTan);
        }
        private void InternalUpdateOnPosMsg(DoPE.ERR Error, bool bReached, double dTime, DoPE.CTRL Control, double dPosition, DoPE.CTRL DControl, double dDestination, short UsTan)
        {
            try
            {
                bTargetReach = bReached;
                ListOfOnPosMsg.Add(new ClassOnPosMasg { ErrorToClass = Error, ReachedToClass = bReached, TimeToClass = dTime, ControlToClass = Control, DControlToClass = DControl, DestinationToClass = dDestination, UsTanToClass = UsTan });
            }
            catch (NullReferenceException) { }
        }

        public void UpdateOnCommandError(double CommandNumber, DoPE.CMD_EERROR ErrorNumber, short UsTan)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnCommandError(CommandNumber, ErrorNumber, UsTan)));
            else
                InternalUpdateOnCommandError(CommandNumber, ErrorNumber, UsTan);
        }
        private void InternalUpdateOnCommandError(double CommandNumber, DoPE.CMD_EERROR ErrorNumber, short UsTan)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnCheckMsg(DoPE.ERR Error, bool bAction, double dTime, DoPE.CHK_ID CheckId, double dPosition, DoPE.SENSOR SensorNo, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnCheckMsg(Error, bAction, dTime, CheckId, dPosition, SensorNo, usTAN)));
            else
                InternalUpdateOnCheckMsg(Error, bAction, dTime, CheckId, dPosition, SensorNo, usTAN);
        }
        private void InternalUpdateOnCheckMsg(DoPE.ERR Error, bool bAction, double dTime, DoPE.CHK_ID CheckId, double dPosition, DoPE.SENSOR SensorNo, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnShieldMsg(DoPE.ERR Error, bool bAction, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnShieldMsg(Error, bAction, dTime, SensorNo, dPosition, usTAN)));
            else
                InternalUpdateOnShieldMsg(Error, bAction, dTime, SensorNo, dPosition, usTAN);
        }
        private void InternalUpdateOnShieldMsg(DoPE.ERR Error, bool bAction, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnRefSignalMsg(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnRefSignalMsg(Error, dTime, SensorNo, dPosition, usTAN)));
            else
                InternalUpdateOnRefSignalMsg(Error, dTime, SensorNo, dPosition, usTAN);
        }
        private void InternalUpdateOnRefSignalMsg(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, double dPosition, short usTANsMeasure)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnSensorMsg(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnSensorMsg(Error, dTime, SensorNo, usTAN)));
            else
                InternalUpdateOnSensorMsg(Error, dTime, SensorNo, usTAN);
        }
        private void InternalUpdateOnSensorMsg(DoPE.ERR Error, double dTime, DoPE.SENSOR SensorNo, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnIoSHaltMsg(DoPE.ERR Error, short Upper, double dTime, DoPE.CTRL Control, double dPosition, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnIoSHaltMsg(Error, Upper, dTime, Control, dPosition, usTAN)));
            else
                InternalUpdateOnIoSHaltMsg(Error, Upper, dTime, Control, dPosition, usTAN);
        }
        private void InternalUpdateOnIoSHaltMsg(DoPE.ERR Error, short Upper, double dTime, DoPE.CTRL Control, double dPosition, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnKeyMsg(DoPE.ERR Error, double dTime, long Keys, long NewKeys, long GoneKeys, short OemKeys, short NewOemKeys, short GoneOemKeys, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnKeyMsg(Error, dTime, Keys, NewKeys, GoneKeys, OemKeys, NewOemKeys, GoneOemKeys, usTAN)));
            else
                InternalUpdateOnKeyMsg(Error, dTime, Keys, NewKeys, GoneKeys, OemKeys, NewOemKeys, GoneOemKeys, usTAN);
        }
        private void InternalUpdateOnKeyMsg(DoPE.ERR Error, double dTime, long Keys, long NewKeys, long GoneKeys, short OemKeys, short NewOemKeys, short GoneOemKeys, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnRuntimeError(DoPE.ERR Error, DoPE.RTE ErrorNumber, double dTime, short Device, short Bits, short usTAN)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnRuntimeError(Error, ErrorNumber, dTime, Device, Bits, usTAN)));
            else
                InternalUpdateOnRuntimeError(Error, ErrorNumber, dTime, Device, Bits, usTAN);
        }
        private void InternalUpdateOnRuntimeError(DoPE.ERR Error, DoPE.RTE ErrorNumber, double dTime, short Device, short Bits, short usTAN)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnOverflow(int iOverflow)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnOverflow(iOverflow)));
            else
                InternalUpdateOnOverflow(iOverflow);
        }
        private void InternalUpdateOnOverflow(int iOverflow)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnDebugMsg(DoPE.ERR Error, DoPE.DEBUG MsgType, double dTime, string sText)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnDebugMsg(Error, MsgType, dTime, sText)));
            else
                InternalUpdateOnDebugMsg(Error, MsgType, dTime, sText);
        }
        private void InternalUpdateOnDebugMsg(DoPE.ERR Error, DoPE.DEBUG MsgType, double dTime, string sText)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnSystemMsg(DoPE.ERR Error, DoPE.SYSTEM_MSG MsgNumber, double dTime, string sText)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnSystemMsg(Error, MsgNumber, dTime, sText)));
            else
                InternalUpdateOnSystemMsg(Error, MsgNumber, dTime, sText);
        }
        private void InternalUpdateOnSystemMsg(DoPE.ERR Error, DoPE.SYSTEM_MSG MsgNumber, double dTime, string sText)
        {
            try
            { }
            catch (NullReferenceException) { }
        }

        public void UpdateOnRmcEvent(long Keys, long NewKeys, long GoneKeys, long Leds, long NewLeds, long GoneLeds)
        {

            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOnRmcEvent(Keys, NewKeys, GoneKeys, Leds, NewLeds, GoneLeds)));
            else
                InternalUpdateOnRmcEvent(Keys, NewKeys, GoneKeys, Leds, NewLeds, GoneLeds);
        }
        private void InternalUpdateOnRmcEvent(long Keys, long NewKeys, long GoneKeys, long Leds, long NewLeds, long GoneLeds)
        {
            try
            { }
            catch (NullReferenceException) { }
        }
        #endregion

        #region "StatusErrorRichTextBox"
        public void UpdateStatusErrorRichTextBox()
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateStatusErrorRichTextBox()));
            else
                InternalUpdateStatusErrorRichTextBox();
        }
        private void InternalUpdateStatusErrorRichTextBox()
        {
            try
            { Status_Error_richTextBox.Text = sErrorMessage; sErrorMessage = ""; }
            catch (NullReferenceException) { }
        }
        #endregion

        #region " Decompte le nombre de cycle "
        public void UpdateTbxDecompteMesure(string sDecompte)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateTbxDecompteMesure(sDecompte)));
            else
                InternalUpdateTbxDecompteMesure(sDecompte);
        }
        private void InternalUpdateTbxDecompteMesure(string sDecompte)
        {
            try
            { StatusLabelForCountAmountCycle.Text = sDecompte; }
            catch (NullReferenceException) { }
        }
        #endregion

        #region "StatusLabel"
        public void UpdateStatusLabel(string TextToUpdate)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateStatusLabel(TextToUpdate)));
            else
                InternalUpdateStatusLabel(TextToUpdate);
        }
        private void InternalUpdateStatusLabel(string TextUpdated)
        {
            try
            {
                StatusLabel.Text = TextUpdated;
                //var vCheckErrosStatus = TextUpdated.Split(new[] { ';' });
            }
            catch (NullReferenceException) { }
        }
        #endregion

        #region " refresh dgv "
        private void RefreshDgv()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(sDefaultPath + sNameOfFileCycle);

            object[,] RangeCycle = null;
            object[,] RangeSequence = null;
            FillCycleDgv.Clear();
            listForDgvMeasure.Clear();

            try
            { RangeCycle = xlWorkbook.Names.Item(Index: "Cycle").RefersToRange.Value; }
            catch (Exception)
            { }

            try { RangeSequence = xlWorkbook.Names.Item(Index: "Sequency").RefersToRange.Value; }
            catch (Exception)
            { }

            try { iNbCycleToDo = Convert.ToInt32(xlWorkbook.Names.Item(Index: "NbCycle").RefersToRange.Value); }
            catch (Exception)
            { }

            try { sModeCycle = Convert.ToString(xlWorkbook.Names.Item(Index: "Mode").RefersToRange.Value); }
            catch (Exception)
            { }

            //Conversion de array en list of array
            var RangeCycleConverted = Enumerable.Range(1, RangeCycle.GetLength(0)).Select(x => Enumerable.Range(1, RangeCycle.GetLength(1)).Select(y => RangeCycle[x, y]).ToArray()).ToList();
            var RangeSequenceConverted = Enumerable.Range(1, RangeSequence.GetLength(0)).Select(x => Enumerable.Range(1, RangeSequence.GetLength(1)).Select(y => RangeSequence[x, y]).ToArray()).ToList();

            foreach (var elem in RangeCycleConverted)
            {
                FillCycleDgv.Add(new ClassCycle
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
            dgvCycle.DataSource = new SortableBindingList<ClassCycle>(FillCycleDgv.ToList());

            dgvCycle.Columns["Step"].Width = 50;
            dgvCycle.Columns["Mode"].Width = 90;
            dgvCycle.Columns["Palier"].Width = 50;
            dgvCycle.Columns["Rampe"].Width = 50;
            dgvCycle.Columns["Temp"].Width = 60;
            dgvCycle.Columns["LwrLimit"].Width = 80;
            dgvCycle.Columns["UprLimit"].Width = 80;
            dgvCycle.Columns["Load"].Width = 90;
            dgvCycle.Columns["Pred"].Width = 50;

            //Paramètre générals de la datagridView cycle
            for (int i = 0; i < FillCycleDgv.Take(1).Count(); i++)
            {
                dgvCycle.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            foreach (var elem in RangeSequenceConverted)
            {
                listForDgvMeasure.Add(new ClassSequence
                {
                    Id = Convert.ToString(elem[0]),
                    Type = Convert.ToString(elem[1]),
                    Designation = Convert.ToString(elem[2]),
                });
            }
            dgvSequenceMeasure.DataSource = new SortableBindingList<ClassSequence>(listForDgvMeasure.ToList());

            //Paramètre générals de la datagridView cycle
            for (int i = 0; i < listForDgvMeasure.Take(1).Count(); i++)
            {
                dgvSequenceMeasure.Columns["Id"].Width = 30;
                dgvSequenceMeasure.Columns["Type"].Width = 100;
                dgvSequenceMeasure.Columns["Designation"].Width = 125;
                dgvSequenceMeasure.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

            xlWorkbook.Close(false);
        }
        #endregion

        #region " ContextMenu strip dgvCycle " 
        private void ToolStripMenuItemOuvrir_Click(object sender, EventArgs e)
        {
            FillCycleDgv.Clear();
            listForDgvMeasure.Clear();

            PassStatusLabel = new DelegateStatusLabel(UpdateStatusLabel);

            if (newclassOfVG.bDebug) { openFileDialogCycle.InitialDirectory = @"D:\TéléTravail\K2000\K2000Rs232App\bin\x86\Debug\Cycle"; }
            else
            {
                if (RdbBancDeForce.Checked)
                { openFileDialogCycle.InitialDirectory = @"G:\BE\GENERAL\Etudes\FORCE\286010 (Thick film)\8-Software\Ambiant\cycle"; }
                else
                { openFileDialogCycle.InitialDirectory = @"G:\BE\GENERAL\Etudes\FORCE\286010 (Thick film)\8-Software\Temperature\Cycle"; }
            }


            openFileDialogCycle.Title = "Selectionnez le fichier Excel";
            openFileDialogCycle.Filter = "Fichier Excel (*.xlsx)|*.xlsx|Tous fichier (*.*)|*.*";
            openFileDialogCycle.FilterIndex = 1;
            openFileDialogCycle.RestoreDirectory = true;

            if (openFileDialogCycle.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                sNameOfFileCycle = openFileDialogCycle.SafeFileName;
                sDefaultPath = openFileDialogCycle.FileName.Substring(0, openFileDialogCycle.FileName.LastIndexOf(@"\"));

                sDefaultPath = openFileDialogCycle.FileName.Substring(0, openFileDialogCycle.FileName.Length - sNameOfFileCycle.Length);
                RefreshDgv();

                PassStatusLabel("Chargement fichier : Ok");

                //on en profite pour renseigner le graphque de la form Chart
                //List<T> lOnlyCycle = new List<T>()
                //NewInstanceOfFormChartlOnly;
            }
            else { PassStatusLabel("Pb lors du chargement du fichier de donnée : NOk"); }

            //POur le debuggage
            //GetConfiguration();
            //TraitementFinal();                
        }

        private void ToolStripMenuItemRefresh_Click(object sender, EventArgs e)
        {
            if (dgvCycle.Rows.Count > 0)
            { RefreshDgv(); }
            else
            { MessageBox.Show("La table n'est pas présente !"); }
        }

        private void ToolStripMenuItemSauvegarde_Click(object sender, EventArgs e)
        {
            if (dgvCycle.Rows.Count > 0)
            {
                //// par défaut on écrit dans le réperoire Mes documents
                string sCompletePath = sDefaultPath + sNameOfFileCycle;

                if (!File.Exists(sCompletePath))
                { FileStream MyFile = File.Create(sCompletePath); }

                StreamWriter FileOfMeasure = new StreamWriter(sCompletePath, false);
                using (FileOfMeasure)
                {
                    string sLine = "";

                    foreach (DataGridViewRow item in dgvCycle.Rows)
                    {
                        foreach (DataGridViewColumn itemOnCol in dgvCycle.Columns)
                        { sLine = sLine + itemOnCol.ToString() + ";"; }

                        FileOfMeasure.WriteLine(sLine);
                    }
                }
            }
            else
            { MessageBox.Show("La table n'est pas présente !"); }
        }

        private void EnregistrerSousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvCycle.Rows.Count > 0)
            {
                Stream myStream;

                saveFileDialogCycle.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                saveFileDialogCycle.FilterIndex = 1;
                saveFileDialogCycle.RestoreDirectory = true;

                if (saveFileDialogCycle.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialogCycle.OpenFile()) != null)
                    {
                        sNameOfFileCycle = saveFileDialogCycle.FileName;

                        StreamWriter FileOfMeasure = new StreamWriter(saveFileDialogCycle.FileName, false);
                        using (FileOfMeasure)
                        {
                            string sLine = "";

                            foreach (DataGridViewRow item in dgvCycle.Rows)
                            {
                                foreach (DataGridViewColumn itemOnCol in dgvCycle.Columns)
                                { sLine = sLine + itemOnCol.ToString() + ";"; }

                                FileOfMeasure.WriteLine(sLine);
                            }
                        }
                        myStream.Close();
                    }
                }
            }
            else
            { MessageBox.Show("La table n'est pas présente !"); }
        }
        #endregion

        #region " Selection du type de mesure en mode automatique"
        public void SelAutomatiqueTypeOfMeasure(string[] sIdConfig, int iId, ClassRS232 ClassId)
        {
            switch (Convert.ToInt32(sIdConfig[0]))
            {
                case 1: ClassId.ConfigCurrentAC(iId).ToString(); break;
                case 2: ClassId.ConfigCurrentDC(iId).ToString(); break;
                case 3: ClassId.ConfigVoltageAC(iId).ToString(); break;
                case 4: ClassId.ConfigVoltageDC(iId).ToString(); break;
                case 5: ClassId.ConfigResistance2wire(iId).ToString(); break;
                case 6: ClassId.ConfigResistance4wire(iId).ToString(); break;
                case 7: ClassId.ConfigTemperature(iId).ToString(); break;
                case 8: ClassId.ConfigFrequency(iId).ToString(); break;
                case 9: ClassId.ConfigPeriod(iId).ToString(); break;
            }
        }
        #endregion

        #region "Bouton switch ON doli"
        private void BtnSwitchOnDoli_Click(object sender, EventArgs e)
        {
            SwitchOnDoli();
        }

        private void SwitchOnDoli()
        {
            try { NewClassOfDoli.DoliOn(); }
            catch (Exception) { }

            Thread.Sleep(2000);
            //On en profite pour remettre la tete tout la haut
            try { NewClassOfDoli.MiseEnApproche((DoPE.CTRL)Doli.DoliControl.Position, Doli.DoliVitesseMiseEnApproche.Five_mm_min, Doli.DoliPositionMiseEnApproche.Fifty_mm); } //1mm/min d'approche & 1mm de destination
            catch (Exception) { }

            Thread.Sleep(2000);
        }
        #endregion

        #region "Switch Off Doli"
        private void Button1_Click(object sender, EventArgs e)
        {
            try { NewClassOfDoli.DoliOff(); }
            catch (Exception) { }
        }
        #endregion

        #region " Bouton Emergency "
        private void BtnEmergency_Click(object sender, EventArgs e)
        {
            PassEnabledSequencialPartToTrue = new DelegateEnabledSequencialPartToTrue(UpdateEnabledSequencialPartToTrue);
            PassEnabledSequencialPartToTrue();

            try { MyElapsedMeasureTimer.Enabled = false; } catch (NullReferenceException) { }
            try { MyElapsedDoliTargetLoad.Enabled = false; } catch (NullReferenceException) { }
            try { MyElapsedCycleTimer.Enabled = false; } catch (NullReferenceException) { }

            bTargetReach = false;

            //On stop Doli
            try { NewClassOfDoli.HaltDoli(); } catch (NullReferenceException) { }

            StopEtuve();

            //On en profite pour remettre la tete tout la haut
            try { NewClassOfDoli.MiseEnApproche((DoPE.CTRL)Doli.DoliControl.Position, Doli.DoliVitesseMiseEnApproche.Ten_mm_s, Doli.DoliPositionMiseEnApproche.Fifty_mm); } catch (Exception) { }
            
            //On stop la connection tcp de l'étuve
            TcpConnectEtuveBe.Close();
        }
        #endregion

        #region " Stop via Toolstrip "
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PassEnabledSequencialPartToTrue = new DelegateEnabledSequencialPartToTrue(UpdateEnabledSequencialPartToTrue);
            PassEnabledSequencialPartToTrue();
        }
        #endregion

        #region " Durée du test "
        public void UpdateTime(TimeSpan dt)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateTime(dt)));
            else
                InternalUpdateTime(dt);
        }
        private void InternalUpdateTime(TimeSpan dt)
        {
            StatusLabel.Text = "Durée du cycle en cours : " + dt.Hours.ToString("00") + ":" + dt.Minutes.ToString("00") + ":" + dt.Seconds.ToString("00");
        }
        #endregion

        #region " Initialisation du temps de palier "
        public void InitialisationOfTimePalier(DateTime dt)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInitialisationOfTimePalier(dt)));
            else
                InternalInitialisationOfTimePalier(dt);
        }
        private void InternalInitialisationOfTimePalier(DateTime dt)
        {
            dtTargetTimePalier = dt;
            TimeSpan Ts = dt - DateTime.Now;
            StatusLabelForDecomptePalier.Text = "Décompte du palier en cours : " + Ts.Hours.ToString("00") + ":" + Ts.Minutes.ToString("00") + ":" + Ts.Seconds.ToString("00");
        }
        #endregion

        #region " Durée du palier "
        public void UpdateStatusLabelForDecomptePalier(string sMessage)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateStatusLabelForDecomptePalier(sMessage)));
            else
                InternalUpdateStatusLabelForDecomptePalier(sMessage);
        }
        private void InternalUpdateStatusLabelForDecomptePalier(string sMessage)
        {
            StatusLabelForDecomptePalier.Text = sMessage;
        }
        #endregion

        #region " Décompte du temps de palier "
        public void UpdateOfTimePalier()
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateOfTimePalier()));
            else
                InternalUpdateOfTimePalier();
        }
        private void InternalUpdateOfTimePalier()
        {
            TimeSpan Ts = dtTargetTimePalier - DateTime.Now;
            if (Ts.TotalHours > 0)
            {
                StatusLabelForDecomptePalier.Text = "Décompte du palier en cours : " + Ts.Hours.ToString("00") + ":" + Ts.Minutes.ToString("00") + ":" + Ts.Seconds.ToString("00");
            }
        }
        #endregion

        #region " Event pour tester si la position de Doli est atteinte "
        private void MyEventDoliTarget(object source, ElapsedEventArgs e)
        {
            var vIndex = dgvCycle.SelectedRows;

            Thread th = new Thread(() =>
            {
                try
                {
                    PassInitTimePalier = new DelegatePassInitPalier(InitialisationOfTimePalier);

                    //Mode débug
                    if (newclassOfVG.bDebug) { bTargetReach = true; }

                    if (bTargetReach)
                    {
                        MyElapsedDoliTargetLoad.Stop(); // Et on stop le timer en cours, il sera relancé lorsqu' on activera une nouvelle demande de position
                                                        //On le remet à zéro pour la prochaine fois
                        bTargetReach = false;

                        //On bascule le flag pour les mesures en moyenne durant toute la durée du palier
                        bFlagForAverageMeasure = true;
                        bFlagDecompteCycleReady = true;

                        //On initialise le décompte du cycle par delegate
                        dtForPalier = DateTime.Now.AddSeconds(Convert.ToDouble(vIndex[0].Cells[2].Value));
                        PassInitTimePalier(dtForPalier);

                        MyElapsedCycleTimer = new System.Timers.Timer(Convert.ToDouble(vIndex[0].Cells[2].Value) * 1000)
                        {
                            Enabled = true,
                            AutoReset = false
                        };
                        MyElapsedCycleTimer.Elapsed += MyEventForCycle;
                        MyElapsedCycleTimer.Start();
                    }
                }
                catch (Exception)
                { }
            });
            th.Start();
        }
        #endregion

        #region " Event pour tester si la température de l'étuve atteint la consigne "
        private void MyEventMesureEtuveBE(object source, ElapsedEventArgs e)
        {
            var vIndex = dgvCycle.SelectedRows;

            lock (LockEtuveBe)
            {
                Thread th = new Thread(() =>
                {
                    try
                    {
                        PassInitTimePalier = new DelegatePassInitPalier(InitialisationOfTimePalier);

                        //Pour savoir si la mesure a atteint la valeur de consigne
                        //On fait une moyenne sur 60 mesures avec la totalité des mesures inclus dans un gabarit de 1%
                        double dTarget = Convert.ToDouble(vIndex[0].Cells[4].Value);

                        ListForTargetReachId5.Add(new ClassForThreadMeasure { Id = 5, Measure = ListForMeasureId5.Select(x => x.Measure).Last().ToString(), Step = 0 });

                        int iSeuil = 10;

                        if (ListForTargetReachId5.Count > iSeuil)
                        {
                            NumberFormatInfo nfi = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name).NumberFormat;
                            string sMin = ListForTargetReachId5.Skip(ListForTargetReachId5.Count - iSeuil).Take(iSeuil).Min(x => x.Measure);
                            string sMax = ListForTargetReachId5.Skip(ListForTargetReachId5.Count - iSeuil).Take(iSeuil).Max(x => x.Measure);

                            double dMin = double.Parse(sMin.Replace(",", "."), CultureInfo.InvariantCulture);
                            double dMax = double.Parse(sMax.Replace(",", "."), CultureInfo.InvariantCulture);

                            if (dMin >= dTarget - 0.5 && dMax <= dTarget + 0.5)
                            { bTargetReach = true; }
                        }

                        //Mode débug
                        if (newclassOfVG.bDebug) { bTargetReach = true; }

                        if (bTargetReach)
                        {
                            MyElapsedForMesureEtuveBe.Stop(); // Et on stop le timer en cours, il sera relancé lorsqu' on activera une nouvelle demande de position
                                                              //On le remet à zéro pour la prochaine fois
                            bTargetReach = false;

                            //On bascule le flag pour les mesures en moyenne durant toute la durée du palier
                            bFlagForAverageMeasure = true;
                            bFlagDecompteCycleReady = true;

                            //On initialise le décompte du cycle par delegate
                            dtForPalier = DateTime.Now.AddMinutes(Convert.ToDouble(vIndex[0].Cells[2].Value));
                            PassInitTimePalier(dtForPalier);

                            //En mode Etuve le temmps est exprimé en minutes
                            MyElapsedCycleTimer = new System.Timers.Timer(Convert.ToDouble(vIndex[0].Cells[2].Value) * 1000 * 60)
                            {
                                Enabled = true,
                                AutoReset = false
                            };
                            MyElapsedCycleTimer.Elapsed += MyEventForCycle;
                            MyElapsedCycleTimer.Start();

                            //On vide la liste pour le prochaine fois
                            ListForTargetReachId5.Clear();
                        }
                    }
                    catch (Exception)
                    { }
                });
                th.Start();
            }
        }
        #endregion

        #region " Update First row visible " 
        private void UpdateFirstRowVisibleOnDgv(int iIndex)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateFirstRowVisibleOnDgv(iIndex)));
            else
                InternalUpdateFirstRowVisibleOnDgv(iIndex);
        }
        private void InternalUpdateFirstRowVisibleOnDgv(int iIndex)
        { dgvCycle.FirstDisplayedScrollingRowIndex = iIndex; }

        #endregion

        #region " Update Tbx "
        public void UpdateTbx(double iGetNextIndex)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalUpdateTbx(iGetNextIndex)));
            else
                InternalUpdateTbx(iGetNextIndex);
        }
        private void InternalUpdateTbx(double dGetNextIndex)
        {
            int iLimiteOfRow = dgvCycle.Rows.Count;
            var vIndex = dgvCycle.SelectedRows;

            if (vIndex[0].Index + 1 >= iLimiteOfRow)
            {
                if (iNbCycleDone >= iNbCycleToDo)
                {
                    //On stop le timmer cycle ainsi que le timer mesure
                    //MyElapsedCycleTimer.Stop();
                    dgvCycle.Rows[vIndex[0].Index].Selected = false;

                    PassEnabledSequencialPartToTrue = new DelegateEnabledSequencialPartToTrue(UpdateEnabledSequencialPartToTrue);
                    PassEnabledSequencialPartToTrue();
                }
                else
                {
                    //On incrémente et on replace le curseur sur la 1ère ligne
                    iNbCycleDone += 1;
                    dgvCycle.Rows[0].Selected = true;
                }
            }
        }
        #endregion

        #region " Bouton SetUp en cours ...."
        private void BtnSetUp_Click(object sender, EventArgs e)
        {
            NewInstanceOfSetUpForm.ShowDialog();
        }
        #endregion

        #region " Traitement final "
        private void TraitementFinal()
        {
            string[] sTabDesOnglets = { "Analyse", "Data" };
            Excel.XlWBATemplate xlWBATemplate1 = Excel.XlWBATemplate.xlWBATWorksheet;
            //Permet de n'ouvrir qu'une seule feuille quelque soit le paramétrage de l'utilisateur
            xlApp.Visible = true;
            xlWorkBook = xlApp.Workbooks.Add(xlWBATemplate1);
            xlWorkSheetAnalyse = xlWorkBook.Worksheets[1];
            xlWorkSheetAnalyse.Name = sTabDesOnglets[0];
            xlWorkSheetValue = xlWorkBook.Sheets.Add(After: xlWorkBook.Worksheets[xlWorkBook.Sheets.Count]);
            xlWorkSheetValue.Name = sTabDesOnglets[1];

            //On va checker la liste pour s'assurer qu'il n'y a pas de doublons et de manquant
            ListForMeasurePos = TestListMeasure(ListForMeasurePos);

            object[,] dAllData = new object[ListForMeasurePos.Count() + 1, 15];
            object[,] dAllDataAverage = new object[ListForAverageMeasure.Count() + 1, 17];
            int iLoop = 0;
            int iLoopAverage = 0;

            try
            {
                ExcelPvReport NewClassExcelPvReport = new ExcelPvReport()
                {
                    PassIndicationFinDeTraitement = new DelegateFinDeTraitement(ClapDeFin),
                    xlAppForFD = xlApp,
                    xlWorkBookForFD = xlWorkBook,
                };
                Thread ThreadForFinal = new Thread(
                    () =>
                    {
                        try
                        {
                            //Traitement de la feuille Data
                            foreach (ClassForData elem in ListForMeasurePos)
                            {
                                dAllData[iLoop, 0] = elem.Dt;
                                dAllData[iLoop, 1] = elem.Step;
                                dAllData[iLoop, 2] = elem.Palier;
                                dAllData[iLoop, 3] = elem.Rampe;
                                dAllData[iLoop, 4] = elem.TempConsigne;
                                dAllData[iLoop, 6] = elem.LoadConsigne;
                                dAllData[iLoop, 7] = elem.LoadMesure;
                                dAllData[iLoop, 8] = elem.Position;
                                dAllData[iLoop, 9] = elem.Lll;
                                dAllData[iLoop, 10] = elem.Upl;

                                try
                                {
                                    if (ListForMeasureId1.Count() > 0)
                                    { dAllData[iLoop, 11] = Convert.ToDouble(ListForMeasureId1.Where(x => x.Step == elem.Step).Select(y => y.Measure).Single().ToString()); }
                                    else
                                    { dAllData[iLoop, 11] = ""; }
                                }
                                catch (Exception) { dAllData[iLoop, 11] = ""; }

                                try
                                {
                                    if (ListForMeasureId2.Count() > 0)
                                    { dAllData[iLoop, 12] = Convert.ToDouble(ListForMeasureId2.Where(x => x.Step == elem.Step).Select(y => y.Measure).Single().ToString()); }
                                    else
                                    { dAllData[iLoop, 12] = ""; }
                                }
                                catch (Exception) { dAllData[iLoop, 12] = ""; }

                                try
                                {
                                    if (ListForMeasureId3.Count() > 0)
                                    { dAllData[iLoop, 13] = Convert.ToDouble(ListForMeasureId3.Where(x => x.Step == elem.Step).Select(y => y.Measure).Single().ToString()); }
                                    else
                                    { dAllData[iLoop, 13] = ""; }
                                }
                                catch (Exception) { dAllData[iLoop, 13] = ""; }

                                try
                                {
                                    if (ListForMeasureId4.Count() > 0)
                                    { dAllData[iLoop, 14] = Convert.ToDouble(ListForMeasureId4.Where(x => x.Step == elem.Step).Select(y => y.Measure).Single().ToString()); }
                                    else
                                    { dAllData[iLoop, 14] = ""; }
                                } 
                                catch (Exception) { dAllData[iLoop, 14] = ""; }
                               
                                try
                                {
                                    if (ListForMeasureId5.Count() > 0)
                                    { dAllData[iLoop, 5] = double.Parse(ListForMeasureId5.Where(x => x.Step == elem.Step).Select(y => y.Measure).Single().ToString().Replace(",", "."), CultureInfo.InvariantCulture); }
                                    else
                                    { dAllData[iLoop, 5] = ""; }
                                }
                                catch (Exception) { dAllData[iLoop, 5] = ""; }

                                iLoop = iLoop + 1;
                            }
                        }

                        catch (Exception)
                        { }

                        //Ecriture de la feuille Data
                        NewClassExcelPvReport.WriteValueSheet(xlWorkSheetValue, dAllData, listForDgvMeasure,RdbBancDeForce);

                        try
                        {
                            foreach (ClassAverageMeasure elem in ListForAverageMeasure)
                            {
                                dAllDataAverage[iLoopAverage, 0] = elem.Step;
                                dAllDataAverage[iLoopAverage, 1] = elem.Mode;
                                dAllDataAverage[iLoopAverage, 2] = elem.Pos;
                                dAllDataAverage[iLoopAverage, 3] = elem.ConsigneTemp;
                                dAllDataAverage[iLoopAverage, 4] = elem.MeasureTemp;
                                dAllDataAverage[iLoopAverage, 5] = elem.ConsigneLoad;
                                dAllDataAverage[iLoopAverage, 6] = elem.MeasureLoad;
                                dAllDataAverage[iLoopAverage, 7] = elem.ErreurLoad;
                                dAllDataAverage[iLoopAverage, 8] = elem.ConversionLoad;
                                dAllDataAverage[iLoopAverage, 9] = elem.Min;
                                dAllDataAverage[iLoopAverage, 10] = elem.Max;
                                dAllDataAverage[iLoopAverage, 11] = elem.Lll;
                                dAllDataAverage[iLoopAverage, 12] = elem.Ull;
                                dAllDataAverage[iLoopAverage, 13] = elem.Id1;
                                dAllDataAverage[iLoopAverage, 14] = elem.Id2;
                                dAllDataAverage[iLoopAverage, 15] = elem.Id3;
                                dAllDataAverage[iLoopAverage, 16] = elem.Id4;

                                iLoopAverage += 1;
                            }
                        }
                        catch (Exception)
                        { }

                        NewClassExcelPvReport.WriteAnalysysSheet(xlWorkSheetAnalyse, dAllDataAverage, listForDgvMeasure);

                        NewClassExcelPvReport.Final();
                    });
                ThreadForFinal.Start();

                ThreadForFinal.Join();
                PassEnabledSequencialPartToTrue = new DelegateEnabledSequencialPartToTrue(UpdateEnabledSequencialPartToTrue);
                PassEnabledSequencialPartToTrue();
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Init Mesure " 
        private void ToolStripMenuItemInit_Click(object sender, EventArgs e)
        {
            PassStatusLabel = new DelegateStatusLabel(UpdateStatusLabel);

            //Initialisation du Web browser
            //Wb.Navigate(new Uri(sAppDir));

            if (dgvCycle.Rows.Count > 0)
            {
                bFlagInit = true;
                ClassVariablesGlobales.iCountMeasure = 0;

                //On reset toutes les cells sélectionnées
                foreach (DataGridViewCell elem in dgvCycle.SelectedCells)
                { elem.Selected = false; }

                //id pour la datagrid view sequence
                foreach (DataGridViewCell elem in dgvSequenceMeasure.SelectedCells)
                { elem.Selected = false; }

                if (RdbBancDeForce.Checked)
                {
                    //Partie destinée à la gestion du banc de force
                    try
                    {
                        NewClassOfDoli.PassStatusErrorRichTextBox = new DelegateStatusErrorRichTextBoxFromMesure(UpdateStatusErrorRichTextBoxFromMesure);
                        NewClassOfDoli.PassTbxPosition = new DelegateTbxPosition(UpdateTbxPosition);
                        NewClassOfDoli.PassTbxForce = new DelegateTbxForce(UpdateTbxForce);
                        NewClassOfDoli.PassDelegateOnPosMsg = new DelegateOnPosMsg(UpdateOnPosMsg);
                        NewClassOfDoli.PassDelegateOnCommandError = new DelegateOnCommandError(UpdateOnCommandError);
                        NewClassOfDoli.PassDelegateOnCheckMsg = new DelegateOnCheckMsg(UpdateOnCheckMsg);
                        NewClassOfDoli.PassDelegateOnShieldMsg = new DelegateOnShieldMsg(UpdateOnShieldMsg);
                        NewClassOfDoli.PassDelegateOnRefSignalMsg = new DelegateOnRefSignalMsg(UpdateOnRefSignalMsg);
                        NewClassOfDoli.PassDelegateOnSensorMsg = new DelegateOnSensorMsg(UpdateOnSensorMsg);
                        NewClassOfDoli.PassDelegateOnIoSHaltMsg = new DelegateOnIoSHaltMsg(UpdateOnIoSHaltMsg);
                        NewClassOfDoli.PassDelegateOnKeyMsg = new DelegateOnKeyMsg(UpdateOnKeyMsg);
                        NewClassOfDoli.PassDelegateOnRuntimeError = new DelegateOnRuntimeError(UpdateOnRuntimeError);
                        NewClassOfDoli.PassDelegateOnOverflow = new DelegaeOnOverflow(UpdateOnOverflow);
                        NewClassOfDoli.PassDelegateOnDebugMsg = new DelegateOnDebugMsg(UpdateOnDebugMsg);
                        NewClassOfDoli.PassDelegateOnSystemMsg = new DelegateOnSystemMsg(UpdateOnSystemMsg);
                        NewClassOfDoli.PassDelegateOnRmcEvent = new DelegateOnRmcEvent(UpdateOnRmcEvent);
                        NewClassOfDoli.bDebug = newclassOfVG.bDebug;

                        NewClassOfDoli.ConnectToEdc();

                        //On fait une 1ère mise en approche
                        NewClassOfDoli.MiseEnApproche((DoPE.CTRL)Doli.DoliControl.Position, Doli.DoliVitesseMiseEnApproche.One_mm_min, Doli.DoliPositionMiseEnApproche.Twenty_mm);
                    }
                    catch (Exception)
                    { }
                }
                else
                {
                    try
                    {
                        //Définition de la classe
                        NewInstanceOfClassTcp = new TcpClientWithTimeout(TbxHost1.Text + "." + TbxHost2.Text + "." + TbxHost3.Text + "." + TbxHost4.Text, Convert.ToInt32(TbxHostPortName.Text));

                        //Nouvelle Instance de la ClassTcp
                        NewInstanceOfClassTcp.PassOneShotMeasurementEtuveBE = new DelegateOneShotMeasurementEtuveBE(UpdateOneShotMeasurementEtuveBE);
                        NewInstanceOfClassTcp.ListForMeasure = ListForMeasureId5;
                        NewInstanceOfClassTcp.bDebug = newclassOfVG.bDebug;

                        //Time out de 1000ms
                        TcpConnectEtuveBe = NewInstanceOfClassTcp.Connect(1000);
                        if (!TcpConnectEtuveBe.Connected)
                        {
                            string sMessage = string.Format("TcpClient Etuve BE connection fail");
                            throw new Exception(sMessage);
                        }
                    }
                    catch (SocketException Ex)
                    { Status_Error_richTextBox.Text = Ex.ToString(); }
                }

                CloseAccessToSequencialGroup();

                //Nommage du fichier de sauvegarde csv pour alimenter la form Chart
                string sDate = DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_";
                sPathForDataCsvFile = Environment.CurrentDirectory + @"\Result\" + sDate + "Data" + tbxFile.Text + cbxSn.SelectedItem + ".csv";
                sPathForDataAverageCsvFile = Environment.CurrentDirectory + @"\Result\" + sDate + "Average" + tbxFile.Text + cbxSn.SelectedItem + ".csv";

                foreach (ClassSequence Elem in listForDgvMeasure)
                {
                    if (Elem.Id == "1" && chkId1.Checked == true)
                    {
                        ListMesure.Add(new ClassForId
                        {
                            Id = Convert.ToInt32(Elem.Id),
                            Config = NewInstanceOfClassRs232ForId1.GetMeasureId().First(x => x.Value.Equals(Elem.Type)).Key,
                            Designation = Elem.Designation
                        });

                        //Permet de positionner le Keithley en mode Acquisition continue
                        string[] sTemp = new string[1];
                        sTemp[0] = ListMesure.Where(x => x.Id == Convert.ToInt32(Elem.Id)).Select(y => y.Config).Single().ToString();
                        SelAutomatiqueTypeOfMeasure(sTemp, Convert.ToInt32(Elem.Id), NewInstanceOfClassRs232ForId1);

                        NewInstanceOfClassRs232ForId1.SendCommand(Convert.ToInt32(Elem.Id), "INIT:CONT ON", out string sOutputData).ToString();

                        //Nouvelle Instance de la ClassMesure
                        NewInstanceOfClassMesureForId1 = new  ClassMesure(UpdateOneShotMeasurementOuputDataTextBox,NewInstanceOfClassRs232ForId1, ListForMeasureId1, newclassOfVG.bDebug);
                    }
                    else if (Elem.Id == "2" && chkId2.Checked == true)
                    {
                        ListMesure.Add(new ClassForId
                        {
                            Id = Convert.ToInt32(Elem.Id),
                            Config = NewInstanceOfClassRs232ForId2.GetMeasureId().First(x => x.Value.Equals(Elem.Type)).Key,
                            Designation = Elem.Designation
                        });

                        string[] sTemp = new string[1];
                        sTemp[0] = ListMesure.Where(x => x.Id == Convert.ToInt32(Elem.Id)).Select(y => y.Config).Single().ToString();
                        SelAutomatiqueTypeOfMeasure(sTemp, Convert.ToInt32(Elem.Id), NewInstanceOfClassRs232ForId2);

                        //Permet de positionner le Keithley en mode Acquisition continue
                        NewInstanceOfClassRs232ForId2.SendCommand(Convert.ToInt32(Elem.Id), "INIT:CONT ON", out string sOutputData).ToString();

                        //Nouvelle Instance de la ClassMesure
                        NewInstanceOfClassMesureForId2 = new ClassMesure(UpdateOneShotMeasurementOuputDataTextBox, NewInstanceOfClassRs232ForId2, ListForMeasureId2, newclassOfVG.bDebug);
                    }
                    else if (Elem.Id == "3" && chkId3.Checked == true)
                    {
                        ListMesure.Add(new ClassForId
                        {
                            Id = Convert.ToInt32(Elem.Id),
                            Config = NewInstanceOfClassRs232ForId3.GetMeasureId().First(x => x.Value.Equals(Elem.Type)).Key,
                            Designation = Elem.Designation
                        });

                        //Permet de positionner le Keithley en mode Acquisition continue
                        string[] sTemp = new string[1];
                        sTemp[0] = ListMesure.Where(x => x.Id == Convert.ToInt32(Elem.Id)).Select(y => y.Config).Single().ToString();
                        SelAutomatiqueTypeOfMeasure(sTemp, Convert.ToInt32(Elem.Id), NewInstanceOfClassRs232ForId3);

                        NewInstanceOfClassRs232ForId3.SendCommand(Convert.ToInt32(Elem.Id), "INIT:CONT ON", out string sOutputData).ToString();

                        //Nouvelle Instance de la ClassMesure
                        NewInstanceOfClassMesureForId3 = new ClassMesure(UpdateOneShotMeasurementOuputDataTextBox, NewInstanceOfClassRs232ForId3, ListForMeasureId3, newclassOfVG.bDebug);
                    }
                    else if (Elem.Id == "4" && chkId4.Checked == true)
                    {
                        ListMesure.Add(new ClassForId
                        {
                            Id = Convert.ToInt32(Elem.Id),
                            Config = NewInstanceOfClassRs232ForId4.GetMeasureId().First(x => x.Value.Equals(Elem.Type)).Key,
                            Designation = Elem.Designation
                        });

                        //Permet de positionner le Keithley en mode Acquisition continue
                        string[] sTemp = new string[1];
                        sTemp[0] = ListMesure.Where(x => x.Id == Convert.ToInt32(Elem.Id)).Select(y => y.Config).Single().ToString();
                        SelAutomatiqueTypeOfMeasure(sTemp, Convert.ToInt32(Elem.Id), NewInstanceOfClassRs232ForId4);

                        NewInstanceOfClassRs232ForId4.SendCommand(Convert.ToInt32(Elem.Id), "INIT:CONT ON", out string sOutputData).ToString();

                        //Nouvelle Instance de la ClassMesure
                        NewInstanceOfClassMesureForId4 = new ClassMesure(UpdateOneShotMeasurementOuputDataTextBox, NewInstanceOfClassRs232ForId4, ListForMeasureId4, newclassOfVG.bDebug);
                    }
                    else if (Elem.Id == "10" && chkId10.Checked == true)
                    {
                        string sOutputData;

                        //On fixe les paramètres de l'alimentation TTI
                        //Gamme 15v@5A
                        NewInstanceOfClassRs232ForId10.SendCommand_TTI(Convert.ToInt32(Elem.Id), "RANGE1 0", out sOutputData).ToString();
                        //Courant de limitatipon 5A
                        NewInstanceOfClassRs232ForId10.SendCommand_TTI(Convert.ToInt32(Elem.Id), "I1 5", out sOutputData).ToString();
                        //Tension 0V
                        NewInstanceOfClassRs232ForId10.SendCommand_TTI(Convert.ToInt32(Elem.Id), "V1 0", out sOutputData).ToString();
                        //Sortie On
                        NewInstanceOfClassRs232ForId10.SendCommand_TTI(Convert.ToInt32(Elem.Id), "OP1 1", out sOutputData).ToString();

                        //Nouvelle Instance de la ClassMesure
                        NewInstanceOfClassMesureForId10 = new ClassMesure(UpdateOneShotMeasurementOuputDataTextBox, NewInstanceOfClassRs232ForId10, ListForMeasureId10, newclassOfVG.bDebug);
                    }
                }

                double dEchantillonage = 0;
                if (RdbBancDeForce.Checked)
                { dEchantillonage = Convert.ToDouble(NudEchantillonageBancDeForce.Value); }
                else { dEchantillonage = Convert.ToDouble(nudEchantillonageEtuve.Value); }

                MyElapsedMeasureTimer = new System.Timers.Timer(Convert.ToDouble((1 / dEchantillonage) * 1000))
                {
                    AutoReset = true,
                    Enabled = true
                };
                MyElapsedMeasureTimer.Elapsed += MyEvent;

                if (RdbBancDeForce.Checked)
                {
                    //On Doli
                    SwitchOnDoli();

                    BtnSwitchOnDoli.Enabled = true;
                    BtnSwitchOffDoli.Enabled = true;
                }

                PassStatusLabel("Init : Ok");
            }
            else
            {
                PassStatusLabel("Problème d'initialisation : NOk");
                MessageBox.Show("La table n'est pas présente !");
            }
        }
        #endregion

        #region " GO............ "
        private void BtnGO_Click(object sender, EventArgs e)
        {
            if (dgvCycle.Rows.Count > 0 && bFlagInit == true)
            {
                double sTare = 0;
                bTargetReach = false;

                try { sTare = Convert.ToDouble(tbxLoad.Text); }
                catch (FormatException) { }

                //Permet de rendre visible la fenetre web browser
                //LauchWB();

                bFlagGo = true;
                BtnSetUp.Enabled = false;
                BtnOpenChart.Enabled = true;

                ClassVariablesGlobales.bLaunchMeasurement = true;
                ClassVariablesGlobales.bLaunchAverageMeasurement = true;

                //On remet a zéro le chiffre du cycle en cours
                PassCountOfCycle = new DelegatePassCountOfCycle(UpdateTbxDecompteMesure);
                PassCountOfCycle("Cycle en cours : " + Convert.ToString(iNbCycleAlreadyDone) + "/" + Convert.ToString(iNbCycleToDo));

                //On clear toute précédente sélection de ligne sur la datagridview
                var vRowSelected = dgvCycle.SelectedRows;
                foreach (DataGridViewRow elem in vRowSelected)
                { elem.Selected = false; }

                //On vérifie que le MyElapsedMeasureTimer isEnabled et qu'il n'a pas été coupé par un stop
                if (!MyElapsedMeasureTimer.Enabled)
                { MyElapsedMeasureTimer.Enabled = true; }

                //On selectionne toutes les lignes sur la datagridview sequence en passant par un for et non pas foreach pour
                //eviter la sélection des row vide

                for (int i = 0; i < listForDgvMeasure.Where(x => x.Id != "").Count(); i++)
                {
                    dgvSequenceMeasure.Rows[i].Selected = true;
                }

                //On clear les list 
                ListForMeasurePos.Clear();
                ListForMeasureId1.Clear();
                ListForMeasureId2.Clear();
                ListForMeasureId3.Clear();
                ListForMeasureId4.Clear();
                ListForMeasureId10.Clear();

                dgvCycle.Rows[0].Selected = true;
                dgvCycle.FirstDisplayedScrollingRowIndex = dgvCycle.Rows[0].Index;
                var vIndex = dgvCycle.SelectedRows;
                
                double dTargetLoad = -Convert.ToDouble(vIndex[0].Cells[5].Value);

                if (RdbBancDeForce.Checked)
                {
                    //ON définit les données d'entrées pour le chart html
                    SetChartData(Convert.ToString(NudEchantillonageBancDeForce.Value * 1000), "40000");

                    double dTargetSpeed = Convert.ToDouble(vIndex[0].Cells[3].Value) * 1000;
                    //On fait la tare en Load
                    try { NewClassOfDoli.DoliTareLoad(sTare); }
                    catch (Exception)
                    {
                        //Mettre une action bloquante 
                    }

                    //Mise a l'approche
                    try
                    {
                        NewClassOfDoli.MiseEnApproche(DoPE.CTRL.LOAD, dTargetSpeed, dTargetLoad);

                        //Déclaration des event on timer
                        MyElapsedDoliTargetLoad = new System.Timers.Timer(100)
                        {
                            Enabled = true,
                            AutoReset = true
                        };
                        MyElapsedDoliTargetLoad.Elapsed += MyEventDoliTarget;

                    }
                    catch (Exception) { }

                    //On passe l'info a la form Chart
                    Thread threadForUpdateConsigneLoad = new Thread(() => { UpdateConsigneLoad(Convert.ToString(dTargetLoad)); })
                    {
                        Priority = ThreadPriority.BelowNormal
                    };
                    threadForUpdateConsigneLoad.Start();
                }
                else
                {
                    //On définit les données d'entrées pour le chart html
                    SetChartData(Convert.ToString(nudEchantillonageEtuve.Value * 1000), "40000");

                    double dTargetSpeed = Convert.ToDouble(vIndex[0].Cells[3].Value);
                    double dConsigne = Convert.ToDouble(vIndex[0].Cells[4].Value);

                    //On passe l'info a la form Chart
                    Thread threadForUpdateConsigneLoad = new Thread(() => { UpdateConsigneLoad(Convert.ToString(dConsigne)); })
                    {
                        Priority = ThreadPriority.BelowNormal
                    };
                    threadForUpdateConsigneLoad.Start();

                    //Mise en route de l'étuve en fixant la pente et la consigne
                    try
                    {
                        //Définition de la pente
                        if(!NewInstanceOfClassTcp.TcpCommmand("U " + 
                            dTargetSpeed.ToString("0000.0", CultureInfo.InvariantCulture) + 
                            " " +
                            dTargetSpeed.ToString("0000.0", CultureInfo.InvariantCulture) 
                            + " 0000.0 0000.0", 500))
                        {throw new NullReferenceException("Vitesse de variation Etuve BE : failed");}

                        //définition de la consigne et mise en route de l'étuve
                        if(!NewInstanceOfClassTcp.TcpCommmand("E " + 
                            dConsigne.ToString("0000.0", CultureInfo.InvariantCulture) +
                            " 0100000000000000", 500))
                        { throw new NullReferenceException("Réglage de la consigne Etuve BE : failed"); }

                        //Déclaration de l'event pour tester si la consigne est atteinte
                        MyElapsedForMesureEtuveBe = new System.Timers.Timer(1000)
                        {
                            Enabled = true,
                            AutoReset = true
                        };
                        MyElapsedForMesureEtuveBe.Elapsed += MyEventMesureEtuveBE;

                    }
                    catch (NullReferenceException Ex)
                    {Status_Error_richTextBox.Text = Ex.ToString();}
                    catch(TimeoutException Ex)
                    { Status_Error_richTextBox.Text = Ex.ToString(); }
                }

                dtLaunchCycleTime = DateTime.Now;
            }
            else
            { MessageBox.Show("La table n'est pas présente, ou le cycle d'acquisition n'a pas encore été lancé !"); }
        }
        #endregion

        #region " Sauvegarde config fichier Xml " 
        private void SaveConfiguration()
        {
            string sPath = Environment.CurrentDirectory + sXmlFileName;

            try
            {
                NewClassConfiguration.ListOfXmlId1 = ListForMeasureId1;
                NewClassConfiguration.ListOfXmlId2 = ListForMeasureId2;
                NewClassConfiguration.ListOfXmlId3 = ListForMeasureId3;
                NewClassConfiguration.ListOfXmlId4 = ListForMeasureId4;
                NewClassConfiguration.ListAver = ListForAverageMeasure;

                NewClassConfiguration.ListPos = ListForMeasurePos;

                ClassXml.Serialize(NewClassConfiguration, sPath);
            }
            catch (Exception)
            { MessageBox.Show("Erreur de sérialisation du fichier Configuration.xml", "Configuration", MessageBoxButtons.OK, MessageBoxIcon.Asterisk); }
        }
        #endregion

        #region " Fichier de chargement Xml " 
        private void GetConfiguration()
        {
            string sPath = Environment.CurrentDirectory + sXmlFileName;

            try
            {
                NewClassConfiguration = (ClassConfiguration)ClassXml.Deserialize(NewClassConfiguration.GetType(), sPath);
                ListForMeasureId1 = NewClassConfiguration.ListOfXmlId1;
                ListForMeasureId2 = NewClassConfiguration.ListOfXmlId2;
                ListForMeasureId3 = NewClassConfiguration.ListOfXmlId3;
                ListForMeasureId4 = NewClassConfiguration.ListOfXmlId4;
                ListForMeasurePos = NewClassConfiguration.ListPos;
                ListForAverageMeasure = NewClassConfiguration.ListAver;
            }
            catch
            { NewClassConfiguration = new ClassConfiguration(); }
        }
        #endregion

        #region " Bouton Stop cycle "
        private void BtnStopSequencialAcquisition_Click(object sender, EventArgs e)
        {
            bFlagGo = false;
            bFlagDecompteCycleReady = false;
            BtnSetUp.Enabled = true;

            Wb.Visible = false;

            ListMesure.Clear();
            ListOfStepDoublons.Clear();
            ListOfStepMissing.Clear();
            ListOfBlank.Clear();
            CloseFormChart();

            if (ListForMeasureId1.Count > 0) { ListForMeasureId1.Clear(); }
            if (ListForMeasureId2.Count > 0) { ListForMeasureId1.Clear(); }
            if (ListForMeasureId3.Count > 0) { ListForMeasureId1.Clear(); }
            if (ListForMeasureId4.Count > 0) { ListForMeasureId1.Clear(); }
            if (ListForAverageMeasure.Count > 0) { ListForAverageMeasure.Clear(); }

            PassEnabledSequencialPartToTrue = new DelegateEnabledSequencialPartToTrue(UpdateEnabledSequencialPartToTrue);

            if (MyElapsedMeasureTimer != null) { MyElapsedMeasureTimer.Stop(); }
            if (MyElapsedDoliTargetLoad != null) { MyElapsedDoliTargetLoad.Stop(); }
            if (MyElapsedCycleTimer != null) { MyElapsedCycleTimer.Stop(); }
            //if(MyElapsedForWriteToDoc!= null) { MyElapsedForWriteToDoc.Stop(); }

            foreach (DataGridViewRow elem in dgvCycle.SelectedRows)
            { elem.Selected = false; }

            foreach (DataGridViewRow elem in dgvSequenceMeasure.SelectedRows)
            { elem.Selected = false; }

            PassEnabledSequencialPartToTrue();

            //ON stop la connection Tcp de l'étuve
            TcpConnectEtuveBe.Close();
        }
        #endregion

        #region " Pass delegate to write to doc Data "
        private void MyEventWriteToDoc(object source, ElapsedEventArgs e)
        {
            PassWriteToDocFileData = new DelegateWriteToDocFileData(UpdateWriteToDocFileData);
            PassWriteToDocFileData();
        }
        #endregion

        #region " Write to doc for Data "
        public void UpdateWriteToDocFileData()
        {
            if (InvokeRequired)
                BeginInvoke(new System.Action(() => InternalUpdateWriteToDocFileData()));
            else
                InternalUpdateWriteToDocFileData();
        }
        public void InternalUpdateWriteToDocFileData()
        {
            Thread ThreadToWriteData = new Thread(() =>
            {
                Thread thSignalData = new Thread(() => { UpdateSignalData(); });
                thSignalData.Start();

                /*
                List<int> listToDetermineLastStep = new List<int>();

                List<ClassForThreadMeasure> ListTempId1 = new List<ClassForThreadMeasure>(ListForMeasureId1);
                List<ClassForThreadMeasure> ListTempId2 = new List<ClassForThreadMeasure>(ListForMeasureId2);
                List<ClassForThreadMeasure> ListTempId3 = new List<ClassForThreadMeasure>(ListForMeasureId3);
                List<ClassForThreadMeasure> ListTempId4 = new List<ClassForThreadMeasure>(ListForMeasureId4);

                //Tant qu'on y est on va traiter les erreurs de lecture sur les listes
                //Critère 1 : pas d'écart entre deux mesures > 50%
                //Critère 2 : Pas de blanc
                List<ClassToWriteDataToFile> ListCompleteForData = new List<ClassToWriteDataToFile>();

                if (ListTempId1.Count > 0){listToDetermineLastStep.Add(ListTempId1.Max(x => x.Step));}
                if (ListTempId2.Count > 0){listToDetermineLastStep.Add(ListTempId2.Max(x => x.Step));}
                if (ListTempId3.Count > 0){listToDetermineLastStep.Add(ListTempId3.Max(x => x.Step));}
                if (ListTempId4.Count > 0){listToDetermineLastStep.Add(ListTempId4.Max(x => x.Step));}

                //Etant donné qu'il y a un doute sur le step vu par chaque list, logiquement à un step pret.
                //Par sécurité on prendra le contenu sauf pour le dernier step...Si ça c'est pas de la sécurité:)
                if (lLastStepCaught != 0) { lFirstStepCaught = lLastStepCaught; }
                else { lFirstStepCaught = ListForMeasurePos.Min(x => x.Step); }

                lLastStepCaught = listToDetermineLastStep.Min(x => x) - 1;

                ListTempId1 = TestListMeasure(ListTempId1);
                ListTempId2 = TestListMeasure(ListTempId2);
                ListTempId3 = TestListMeasure(ListTempId3);
                ListTempId4 = TestListMeasure(ListTempId4);


                string sLine = "";

                using (StreamWriter FileOfMeasure = new StreamWriter(sPathForDataCsvFile))
                {
                    if (ClassVariablesGlobales.bLaunchMeasurement== true)
                    {
                        var vIndex = dgvSequenceMeasure.Columns[2];

                        FileOfMeasure.WriteLine("Nouvelle acquisition du " + DateTime.Now.ToLocalTime());

                        string sDesignationnId1 = "NA";
                        string sDesignationnId2 = "NA";
                        string sDesignationnId3 = "NA";
                        string sDesignationnId4 = "NA";

                        if (chkId1.Checked)
                        { sDesignationnId1 = listForDgvMeasure.Where(x => x.Id == "1").Select(y => y.Designation).Single().ToString(); }

                        if (chkId2.Checked)
                        { sDesignationnId2 = listForDgvMeasure.Where(x => x.Id == "2").Select(y => y.Designation).Single().ToString(); }

                        if (chkId3.Checked)
                        { sDesignationnId3 = listForDgvMeasure.Where(x => x.Id == "3").Select(y => y.Designation).Single().ToString(); }

                        if (chkId4.Checked)
                        { sDesignationnId4 = listForDgvMeasure.Where(x => x.Id == "4").Select(y => y.Designation).Single().ToString(); }

                        FileOfMeasure.WriteLine(
                            "Step" + ";" +
                            "Load Consigne" + ";" +
                            "Load Measure" + ";" +
                            "Position Measure" + ";" +
                            sDesignationnId1 + ";" +
                            sDesignationnId2 + ";" +
                            sDesignationnId3 + ";" +
                            sDesignationnId4
                            );
                        ClassVariablesGlobales.bLaunchMeasurement = false;
                    }

                    //A ce stade on doit avoir tous les step, on peux donc balayer la liste entièrement
                    for (int iLoop = ListTempId1.Min(x=>x.Step); iLoop <= ListTempId1.Max(x => x.Step); iLoop++)
                    {
                        try
                        {
                            sLine =
                            iLoop + ";" +
                            Convert.ToDouble(ListForMeasurePos.Where(x => x.Step == iLoop).Select(x => x.LoadConsigne).Single()) + ";" +
                            Convert.ToDouble(ListForMeasurePos.Where(x => x.Step == iLoop).Select(x => x.Load).Single()) + ";" +
                            Convert.ToDouble(ListForMeasurePos.Where(x => x.Step == iLoop).Select(x => x.Position).Single());

                            if (ListTempId1.Count > 0)
                            {
                                try { sLine = sLine + ";" + Convert.ToDouble(ListTempId1.Where(x => x.Step == iLoop).Select(x => x.Measure).Single()); }
                                catch (Exception){sLine = sLine + ";";}
                            }

                            if (ListTempId2.Count > 0)
                            {
                                try { sLine = sLine + ";" + Convert.ToDouble(ListTempId2.Where(x => x.Step == iLoop).Select(x => x.Measure).Single()); }
                                catch (Exception) { sLine = sLine + ";"; }
                            }

                            if (ListTempId3.Count > 0)
                            {
                                try { sLine = sLine + ";" + Convert.ToDouble(ListTempId3.Where(x => x.Step == iLoop).Select(x => x.Measure).Single()); }
                                catch (Exception) { sLine = sLine + ";"; }
                            }
                            if (ListTempId4.Count > 0)
                            {
                                try { sLine = sLine + ";" + Convert.ToDouble(ListTempId4.Where(x => x.Step == iLoop).Select(x => x.Measure).Single()); }
                                catch (Exception) { }
                            }

                            //sLine = sLine + "\n";
                            FileOfMeasure.WriteLine(sLine);
                        }
                        catch (InvalidOperationException){ sLine = "InvalidOperationException"; }
                        catch (FormatException) { sLine = "FormatException"; }
                        catch (OverflowException){ sLine = "OverflowException"; }
                    }
                    FileOfMeasure.Close();
                }      
                */
            })
            {
                Priority = ThreadPriority.BelowNormal
            };
            ThreadToWriteData.Start();
        }
        #endregion

        #region " Test des listes de mesures avant écriture "
        private List<ClassForData> TestListMeasure(List<ClassForData> lToScreen)
        {
            for (int iloop = lToScreen.Min(x => x.Step); iloop < lToScreen.Max(x => x.Step); iloop++)
            {
                if (lToScreen.Exists(x => x.Step == iloop))
                {
                    if (lToScreen.Where(x => x.Step == iloop).Count() > 1)
                    {
                        List<int> lDoublons = new List<int>(lToScreen.Select((value, index) => new { value, index }).Where(x => x.value.Step == iloop).Select(x => x.index).ToList());
                        lDoublons = lToScreen.Select((value, index) => new { value, index }).Where(x => x.value.Step == iloop).Select(x => x.index).ToList();

                        //On supprime tous les doublons sauf le dernier.....Parti pris
                        for (int iDoublons = 0; iDoublons < lToScreen.Where(x => x.Step == iloop).Count() - 1; iDoublons++)
                        { ListOfStepDoublons.Add(new BlankMeasure { Index = lDoublons[iDoublons] }); }
                    }
                }
                else
                { ListOfStepMissing.Add(new BlankMeasure { Step = iloop }); }
            }

            foreach (BlankMeasure elem in ListOfStepMissing)
            { lToScreen.Add(new ClassForData { Step = elem.Step }); }

            //On supprime les doublons
            foreach (BlankMeasure elem in ListOfStepDoublons)
            {
                ListOfStepDoublons = ListOfStepDoublons.OrderBy(x => x.Index).ToList();
                lToScreen.RemoveAt(elem.Index);
            }

            lToScreen = lToScreen.OrderBy(x => x.Step).ToList();

            return lToScreen;
        }
        #endregion

        #region " My Event "
        private void MyEvent(object source, ElapsedEventArgs e)
        {
            PassTimerCount = new DelegatePassTimerCount(UpdateTime);

            //On block l'accés, le threads une fois libéré permettra le travail 
            //en background et notamment lors de l'event du palier
            lock (LockThreadMyEvent)
            {
                newclassOfVG.lStep += 1;
                var vIndex = dgvCycle.SelectedRows;

                if (vIndex.Count > 0)
                {
                    //A chaque event on stock les informations
                    ListForMeasurePos.Add(new ClassForData
                    {
                        Dt = DateTime.Now,
                        Rampe = Convert.ToDouble(vIndex[0].Cells[3].Value),
                        Palier = Convert.ToDouble(vIndex[0].Cells[2].Value),
                        TempConsigne = Convert.ToDouble(vIndex[0].Cells[4].Value),
                        LoadConsigne = Convert.ToDouble(vIndex[0].Cells[5].Value),
                        LoadMesure = Convert.ToDouble(tbxLoad.Text),
                        Position = Convert.ToDouble(TbxPosition.Text),
                        Lll = Convert.ToDouble(vIndex[0].Cells[6].Value),
                        Upl = Convert.ToDouble(vIndex[0].Cells[7].Value),
                        Step = newclassOfVG.lStep
                    });
                }

                if (chkId1.Checked)
                {
                    Thread LaunchMLesureId1 = new Thread(() => { NewInstanceOfClassMesureForId1.Mesure(1, newclassOfVG.lStep); })
                    {
                        Priority = ThreadPriority.AboveNormal
                    };
                    LaunchMLesureId1.Start();
                }

                if (chkId2.Checked)
                {
                    Thread LaunchMLesureId2 = new Thread(() => { NewInstanceOfClassMesureForId2.Mesure(2, newclassOfVG.lStep); })
                    {
                        Priority = ThreadPriority.AboveNormal
                    };
                    LaunchMLesureId2.Start();
                }

                if (chkId3.Checked)
                {
                    Thread LaunchMLesureId3 = new Thread(() => { NewInstanceOfClassMesureForId3.Mesure(3, newclassOfVG.lStep); })
                    {
                        Priority = ThreadPriority.AboveNormal
                    };
                    LaunchMLesureId3.Start();
                }

                if (chkId4.Checked)
                {
                    Thread LaunchMLesureId4 = new Thread(() => { NewInstanceOfClassMesureForId4.Mesure(4, newclassOfVG.lStep); })
                    {
                        Priority = ThreadPriority.AboveNormal
                    };
                    LaunchMLesureId4.Start();
                }

                if (chkId10.Checked)
                {
                    Thread LaunchMLesureId10 = new Thread(() => { NewInstanceOfClassMesureForId10.Mesure_TTI(10, newclassOfVG.lStep); })
                    {
                        Priority = ThreadPriority.AboveNormal
                    };
                    LaunchMLesureId10.Start();
                }

                if (RdbEtuve.Checked)
                {
                    Thread LaunchMesureEtuveBe = new Thread(() => { NewInstanceOfClassTcp.Mesure(5, newclassOfVG.lStep); })
                    {
                        Priority = ThreadPriority.AboveNormal
                    };
                    LaunchMesureEtuveBe.Start();
                }

                if (bFlagGo)
                {
                    Thread ThreadToCountCycleDuration = new Thread(() => { PassTimerCount(DateTime.Now - dtLaunchCycleTime); });
                    ThreadToCountCycleDuration.Start();
                }

                if (bFlagDecompteCycleReady)
                {
                    PassTimePalier = new DelegatePassTimePalier(UpdateOfTimePalier);
                    PassTimePalier();
                }
            }
        }
        #endregion

        #region " Event a la fin du palier "
        private void MyEventForCycle(object source, ElapsedEventArgs e)
        {
            Thread ThreadForNewPalier = new Thread(() =>
            {
                double dLoad = 0;
                double dPos = 0;
                double dId1 = 0;
                double dId2 = 0;
                double dId3 = 0;
                double dId4 = 0;
                double dId5 = 0;
                double dCalculErrorLoad = 0;

                PassCountOfCycle = new DelegatePassCountOfCycle(UpdateTbxDecompteMesure);

                try
                {
                    var vIndex = dgvCycle.SelectedRows;

                    if ((vIndex[0].Index + 1) < dgvCycle.Rows.Count)
                    {
                        //Pour le debug
                        DtDebug = DateTime.Now;

                        bFlagForAverageMeasure = false;
                        bFlagDecompteCycleReady = false;

                        if (vIndex[0].Cells[1].Value.ToString() == "Cycle")
                        {
                            //On fait une moyenne des 10 dernières mesures effectuées
                            if (ListForMeasurePos.Count > 10)
                            {
                                try
                                {
                                    dLoad = ListForMeasurePos.Skip(ListForMeasurePos.Count - 10).Take(10).Select(x => x.LoadMesure).Average();
                                    dPos = ListForMeasurePos.Skip(ListForMeasurePos.Count - 10).Take(10).Select(x => x.Position).Average();
                                }
                                catch (Exception) { }
                            }

                            if (ListForMeasureId1.Count > 10)
                            {
                                try
                                { dId1 = ListForMeasureId1.Skip(ListForMeasureId1.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                catch (Exception) { }
                            }

                            if (ListForMeasureId2.Count > 10)
                            {
                                try
                                { dId2 = ListForMeasureId2.Skip(ListForMeasureId2.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                catch (Exception) { }
                            }

                            if (ListForMeasureId3.Count > 10)
                            {
                                try
                                { dId3 = ListForMeasureId3.Skip(ListForMeasureId3.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                catch (Exception) { }
                            }

                            if (ListForMeasureId4.Count > 10)
                            {
                                try
                                { dId4 = ListForMeasureId4.Skip(ListForMeasureId4.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                catch (Exception) { }
                            }

                            if (ListForMeasureId5.Count > 10)
                            {
                                try
                                { dId5 = ListForMeasureId5.Skip(ListForMeasureId5.Count - 10).Take(10).Select(x => double.Parse(x.Measure.Replace(",", "."), CultureInfo.InvariantCulture)).Average(); }
                                catch (Exception) { }

                            }

                            if (sModeCycle.Equals("3310"))
                            { dCalculErrorLoad = Convert.ToDouble((((dId1 - 0.004) * 53378.659) / 0.016) - Convert.ToDouble(vIndex[0].Cells[5].Value)); }

                            ListForAverageMeasure.Add(new ClassAverageMeasure
                            {
                                Step = iDecomptePassageEventForPalier,
                                Mode = vIndex[0].Cells[1].Value.ToString(),
                                Pos = dPos,
                                ConsigneTemp = Convert.ToDouble(vIndex[0].Cells[4].Value),
                                MeasureTemp = dId5,
                                ConsigneLoad = Convert.ToDouble(vIndex[0].Cells[5].Value),
                                MeasureLoad = dLoad,
                                ErreurLoad = dCalculErrorLoad,
                                ConversionLoad = ((dId1 - 0.004) * 53378.659) / 0.016,
                                Min = Convert.ToDouble(vIndex[0].Cells[5].Value) + Convert.ToDouble(vIndex[0].Cells[6].Value),
                                Max = Convert.ToDouble(vIndex[0].Cells[5].Value) + Convert.ToDouble(vIndex[0].Cells[7].Value),
                                Lll = Convert.ToDouble(vIndex[0].Cells[6].Value),
                                Ull = Convert.ToDouble(vIndex[0].Cells[7].Value),
                                Id1 = dId1,
                                Id2 = dId2,
                                Id3 = dId3,
                                Id4 = dId4
                            });
                            iDecomptePassageEventForPalier += 1;

                            //PassWriteToDocFileAverage = new DelegateWriteToDocFileAverage(UpdateWriteToDocFileAverage);
                            //PassWriteToDocFileAverage();

                            UpdateErrorLoad(ListForAverageMeasure, false);
                        }

                        dgvCycle.Rows[vIndex[0].Index].Selected = false;
                        int iIndex = vIndex[0].Index + 1;
                        dgvCycle.Rows[iIndex].Selected = true;

                        PassFirstRowVisibleOnDgv = new DelegatePassFirstRowVisibleOnDgv(UpdateFirstRowVisibleOnDgv);
                        PassFirstRowVisibleOnDgv(iIndex);

                        double dTimePalier = Convert.ToDouble(dgvCycle.SelectedRows[0].Cells[2].Value);
                        double dTargetLoad = -Convert.ToDouble(dgvCycle.SelectedRows[0].Cells[5].Value);

                        if (RdbBancDeForce.Checked)
                        {
                            double dTargetSpeed = Convert.ToDouble(dgvCycle.SelectedRows[0].Cells[3].Value) * 1000;
                            NewClassOfDoli.MiseEnApproche(DoPE.CTRL.LOAD, dTargetSpeed, dTargetLoad);

                            MyElapsedDoliTargetLoad.Start();

                            Thread threadForUpdateConsigneLoad = new Thread(() => { UpdateConsigneLoad(Convert.ToString(dTargetLoad)); });
                            threadForUpdateConsigneLoad.Start();
                        }
                        else
                        {
                            double dTargetSpeed = Convert.ToDouble(dgvCycle.SelectedRows[0].Cells[3].Value);
                            double dConsigne = Convert.ToDouble(dgvCycle.SelectedRows[0].Cells[4].Value);

                            Thread threadForUpdateConsigneLoad = new Thread(() => { UpdateConsigneLoad(Convert.ToString(dTargetLoad)); });
                            threadForUpdateConsigneLoad.Start();

                            //On fixe la consigne
                            try
                            {
                                //Définition de la pente
                                if (!NewInstanceOfClassTcp.TcpCommmand("U " +
                                    dTargetSpeed.ToString("0000.0", CultureInfo.InvariantCulture) +
                                    " " +
                                    dTargetSpeed.ToString("0000.0", CultureInfo.InvariantCulture)
                                    + " 0000.0 0000.0", 500))
                                { throw new NullReferenceException("Vitesse de variation Etuve BE : failed"); }

                                //définition de la consigne et mise en route de l'étuve
                                if (!NewInstanceOfClassTcp.TcpCommmand("E " +
                                    dConsigne.ToString("0000.0", CultureInfo.InvariantCulture) +
                                    " 0100000000000000", 500))
                                { throw new NullReferenceException("Réglage de la consigne Etuve BE : failed"); }

                                MyElapsedForMesureEtuveBe.Start();
                            }
                            catch (NullReferenceException Ex)
                            { Status_Error_richTextBox.Text = Ex.ToString(); }
                            catch (TimeoutException Ex)
                            { Status_Error_richTextBox.Text = Ex.ToString(); }
                        }

                        //Pour le debug
                        lTs.Add(DateTime.Now - DtDebug);
                    }
                    else
                    {
                        if (iNbCycleAlreadyDone < iNbCycleToDo)
                        {
                            iNbCycleAlreadyDone += 1;
                            PassCountOfCycle("Cycle en cours : " + Convert.ToString(iNbCycleAlreadyDone));

                            bFlagForAverageMeasure = false;

                            if (vIndex[0].Cells[1].Value.ToString() == "Cycle")
                            {
                                //On fait une moyenne des 10 dernières mesures effectuées
                                if (ListForMeasurePos.Count > 10)
                                {
                                    try
                                    {
                                        dLoad = ListForMeasurePos.Skip(ListForMeasurePos.Count - 10).Take(10).Select(x => x.LoadMesure).Average();
                                        dPos = ListForMeasurePos.Skip(ListForMeasurePos.Count - 10).Take(10).Select(x => x.Position).Average();
                                    }
                                    catch (Exception) { }
                                }

                                if (ListForMeasureId1.Count > 10)
                                {
                                    try
                                    { dId1 = ListForMeasureId1.Skip(ListForMeasureId1.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                    catch (Exception) { }
                                }

                                if (ListForMeasureId2.Count > 10)
                                {
                                    try
                                    { dId2 = ListForMeasureId2.Skip(ListForMeasureId2.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                    catch (Exception) { }
                                }

                                if (ListForMeasureId3.Count > 10)
                                {
                                    try
                                    { dId3 = ListForMeasureId3.Skip(ListForMeasureId3.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                    catch (Exception) { }
                                }

                                if (ListForMeasureId4.Count > 10)
                                {
                                    try
                                    { dId4 = ListForMeasureId4.Skip(ListForMeasureId4.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                    catch (Exception) { }
                                }

                                if (ListForMeasureId5.Count > 10)
                                {
                                    try
                                    { dId5 = ListForMeasureId5.Skip(ListForMeasureId5.Count - 10).Take(10).Select(x => double.Parse(x.Measure.Replace(",", "."), CultureInfo.InvariantCulture)).Average(); }
                                    catch (Exception) { }

                                }

                                if (sModeCycle.Equals("3310"))
                                { dCalculErrorLoad = Convert.ToDouble((((dId1 - 0.004) * 53378.659) / 0.016) - Convert.ToDouble(vIndex[0].Cells[5].Value)); }

                                ListForAverageMeasure.Add(new ClassAverageMeasure
                                {
                                    Step = iDecomptePassageEventForPalier,
                                    Mode = vIndex[0].Cells[1].Value.ToString(),
                                    Pos = dPos,
                                    ConsigneTemp = Convert.ToDouble(vIndex[0].Cells[4].Value),
                                    MeasureTemp = dId5,
                                    ConsigneLoad = Convert.ToDouble(vIndex[0].Cells[5].Value),
                                    MeasureLoad = dLoad,
                                    ErreurLoad = dCalculErrorLoad,
                                    ConversionLoad = Convert.ToDouble(((dId1 - 0.004) * 53378.659) / 0.016),
                                    Min = Convert.ToDouble(vIndex[0].Cells[5].Value) + Convert.ToDouble(vIndex[0].Cells[6].Value),
                                    Max = Convert.ToDouble(vIndex[0].Cells[5].Value) + Convert.ToDouble(vIndex[0].Cells[7].Value),
                                    Lll = Convert.ToDouble(vIndex[0].Cells[6].Value),
                                    Ull = Convert.ToDouble(vIndex[0].Cells[7].Value),
                                    Id1 = dId1,
                                    Id2 = dId2,
                                    Id3 = dId3,
                                    Id4 = dId4
                                });
                                iDecomptePassageEventForPalier += 1;

                                //PassWriteToDocFileAverage = new DelegateWriteToDocFileAverage(UpdateWriteToDocFileAverage);
                                //PassWriteToDocFileAverage();

                                UpdateErrorLoad(ListForAverageMeasure, false);
                            }

                            dgvCycle.Rows[vIndex[0].Index].Selected = false;
                            int iIndex = Convert.ToInt32(vIndex[0].Cells[8].Value) - 1;
                            dgvCycle.Rows[iIndex].Selected = true;

                            PassFirstRowVisibleOnDgv = new DelegatePassFirstRowVisibleOnDgv(UpdateFirstRowVisibleOnDgv);
                            PassFirstRowVisibleOnDgv(iIndex);

                            double dTimePalier = Convert.ToDouble(dgvCycle.SelectedRows[0].Cells[2].Value);
                            double dTargetLoad = -Convert.ToDouble(dgvCycle.SelectedRows[0].Cells[5].Value);

                            if (RdbBancDeForce.Checked)
                            {
                                double dTargetSpeed = Convert.ToDouble(dgvCycle.SelectedRows[0].Cells[3].Value) * 1000;
                                NewClassOfDoli.MiseEnApproche(DoPE.CTRL.LOAD, dTargetSpeed, dTargetLoad);

                                MyElapsedDoliTargetLoad.Start();

                                Thread threadForUpdateConsigneLoad = new Thread(() => { UpdateConsigneLoad(Convert.ToString(dTargetLoad)); })
                                {
                                    Priority = ThreadPriority.BelowNormal
                                };
                                threadForUpdateConsigneLoad.Start();
                            }
                            else
                            {
                                double dTargetSpeed = Convert.ToDouble(dgvCycle.SelectedRows[0].Cells[3].Value);
                                double dConsigne = Convert.ToDouble(dgvCycle.SelectedRows[0].Cells[4].Value);

                                Thread threadForUpdateConsigneLoad = new Thread(() => { UpdateConsigneLoad(Convert.ToString(dConsigne)); })
                                {
                                    Priority = ThreadPriority.BelowNormal
                                };
                                threadForUpdateConsigneLoad.Start();

                                //On fixe la consigne
                                try
                                {
                                    //Définition de la pente
                                    if (!NewInstanceOfClassTcp.TcpCommmand("U " +
                                        dTargetSpeed.ToString("0000.0", CultureInfo.InvariantCulture) +
                                        " " +
                                        dTargetSpeed.ToString("0000.0", CultureInfo.InvariantCulture)
                                        + " 0000.0 0000.0", 500))
                                    { throw new NullReferenceException("Vitesse de variation Etuve BE : failed"); }

                                    //définition de la consigne et mise en route de l'étuve
                                    if (!NewInstanceOfClassTcp.TcpCommmand("E " +
                                        dConsigne.ToString("0000.0", CultureInfo.InvariantCulture) +
                                        " 0100000000000000", 500))
                                    { throw new NullReferenceException("Réglage de la consigne Etuve BE : failed"); }

                                    MyElapsedForMesureEtuveBe.Start();
                                }
                                catch (NullReferenceException Ex)
                                { Status_Error_richTextBox.Text = Ex.ToString(); }
                                catch (TimeoutException Ex)
                                { Status_Error_richTextBox.Text = Ex.ToString(); }
                            }

                            iDecomptePassageEventForPalier += 1;
                        }
                        else
                        {
                            if (vIndex[0].Cells[1].Value.ToString() == "Cycle")
                            {
                                //On stop le timer del lecture des K2000
                                MyElapsedMeasureTimer.Stop();
                                //Ainsi que celui de l'écriture des data
                                //MyElapsedForWriteToDoc.Stop();

                                //On va tout de même vérifier l'état du stockage des data pour vérifier que tout a été écit dans le fichier csv
                                //PassWriteToDocFileData = new DelegateWriteToDocFileData(UpdateWriteToDocFileData);
                                //PassWriteToDocFileData();

                                PasssMessageForStatusLabel = new DelegatePasssMessageForStatusLabel(UpdateStatusLabelForDecomptePalier);
                                PasssMessageForStatusLabel("Durée total du cycle : " + Convert.ToString(iNbCycleAlreadyDone));

                                PassCountOfCycle = new DelegatePassCountOfCycle(UpdateTbxDecompteMesure);
                                PassCountOfCycle("Nombre total de cycle éfectué : " + Convert.ToString(iNbCycleAlreadyDone));

                                //On fait une moyenne des 10 dernières mesures effectuées
                                if (ListForMeasurePos.Count > 10)
                                {
                                    try
                                    {
                                        dLoad = ListForMeasurePos.Skip(ListForMeasurePos.Count - 10).Take(10).Select(x => x.LoadMesure).Average();
                                        dPos = ListForMeasurePos.Skip(ListForMeasurePos.Count - 10).Take(10).Select(x => x.Position).Average();
                                    }
                                    catch (Exception) { }
                                }

                                if (ListForMeasureId1.Count > 10)
                                {
                                    try
                                    { dId1 = ListForMeasureId1.Skip(ListForMeasureId1.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                    catch (Exception) { }
                                }

                                if (ListForMeasureId2.Count > 10)
                                {
                                    try
                                    { dId2 = ListForMeasureId2.Skip(ListForMeasureId2.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                    catch (Exception) { }
                                }

                                if (ListForMeasureId3.Count > 10)
                                {
                                    try
                                    { dId3 = ListForMeasureId3.Skip(ListForMeasureId3.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                    catch (Exception) { }
                                }

                                if (ListForMeasureId4.Count > 10)
                                {
                                    try
                                    { dId4 = ListForMeasureId4.Skip(ListForMeasureId4.Count - 10).Take(10).Select(x => Convert.ToDouble(x.Measure)).Average(); }
                                    catch (Exception) { }
                                }

                                if (ListForMeasureId5.Count > 10)
                                {
                                    try
                                    { dId5 = ListForMeasureId5.Skip(ListForMeasureId5.Count - 10).Take(10).Select(x => double.Parse(x.Measure.Replace(",", "."), CultureInfo.InvariantCulture)).Average(); }
                                    catch (Exception) { }

                                }

                                if (sModeCycle.Equals("3310"))
                                { dCalculErrorLoad = Convert.ToDouble((((dId1 - 0.004) * 53378.659) / 0.016) - Convert.ToDouble(vIndex[0].Cells[5].Value)); }

                                ListForAverageMeasure.Add(new ClassAverageMeasure
                                {
                                    Step = iDecomptePassageEventForPalier,
                                    Mode = vIndex[0].Cells[1].Value.ToString(),
                                    Pos = dPos,
                                    ConsigneTemp = Convert.ToDouble(vIndex[0].Cells[4].Value),
                                    MeasureTemp = dId5,
                                    ConsigneLoad = Convert.ToDouble(vIndex[0].Cells[5].Value),
                                    MeasureLoad = dLoad,
                                    ErreurLoad = dCalculErrorLoad,
                                    ConversionLoad = Convert.ToDouble(((dId1 - 0.004) * 53378.659) / 0.016),
                                    Min = Convert.ToDouble(vIndex[0].Cells[5].Value) + Convert.ToDouble(vIndex[0].Cells[6].Value),
                                    Max = Convert.ToDouble(vIndex[0].Cells[5].Value) + Convert.ToDouble(vIndex[0].Cells[7].Value),
                                    Lll = Convert.ToDouble(vIndex[0].Cells[6].Value),
                                    Ull = Convert.ToDouble(vIndex[0].Cells[7].Value),
                                    Id1 = dId1,
                                    Id2 = dId2,
                                    Id3 = dId3,
                                    Id4 = dId4
                                });
                                iDecomptePassageEventForPalier += 1;

                                //PassWriteToDocFileAverage = new DelegateWriteToDocFileAverage(UpdateWriteToDocFileAverage);
                                //PassWriteToDocFileAverage();

                                UpdateErrorLoad(ListForAverageMeasure, false);

                                if (RdbEtuve.Checked)
                                { StopEtuve(); }
                            }

                            dgvCycle.Rows[vIndex[0].Index].Selected = false;

                            //On informe que le cycle est terminé
                            bFlagGo = false;

                            StopChart();

                            TraitementFinal();
                            SaveConfiguration();
                        }
                    }
                }
                catch (Exception)
                { }
            })
            {
                Priority = ThreadPriority.Lowest
            };
            ThreadForNewPalier.Start();

        }
        #endregion

        #region " Arret de l'étuve "
        private void StopEtuve()
        {
            //On stop l'essai et on remet une consigne de 25 par défaut
            double dTargetSpeed = 10;
            double dConsigne = 25;

            //On fixe la consigne
            try
            {
                //Définition de la pente
                if (!NewInstanceOfClassTcp.TcpCommmand("U " +
                    dTargetSpeed.ToString("0000.0", CultureInfo.InvariantCulture) +
                    " " +
                    dTargetSpeed.ToString("0000.0", CultureInfo.InvariantCulture)
                    + " 0000.0 0000.0", 500))
                { throw new NullReferenceException("Vitesse de variation Etuve BE : failed"); }

                //On fixe d'abord la consigne à 25° pour paramètre et ensuite on coupe l'étuve
                if (!NewInstanceOfClassTcp.TcpCommmand("E " +
                    dConsigne.ToString("0000.0", CultureInfo.InvariantCulture) +
                    " 0100000000000000", 500))
                { throw new NullReferenceException("Réglage de la consigne Etuve BE : failed"); }

                if (!NewInstanceOfClassTcp.TcpCommmand("E " +
                    dConsigne.ToString("0000.0", CultureInfo.InvariantCulture) +
                    " 0000000000000000", 500))
                { throw new NullReferenceException("Réglage de la consigne Etuve BE : failed"); }
            }
            catch (NullReferenceException Ex)
            { Status_Error_richTextBox.Text = Ex.ToString(); }
            catch (TimeoutException Ex)
            { Status_Error_richTextBox.Text = Ex.ToString(); }
        }
        #endregion

        #region " Clap de fin "
        public void ClapDeFin(bool DataReady, Excel.Application xlApp, Worksheet xlWSAnalyse, Workbook xlWorkBook)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalClapDeFin(DataReady, xlApp, xlWSAnalyse, xlWorkBook)));
            else
                InternalClapDeFin(DataReady, xlApp, xlWSAnalyse, xlWorkBook);
        }

        private void InternalClapDeFin(bool DataReady, Excel.Application xlApp, Worksheet xlWSAnalyse, Workbook xlWorkBook)
        {
            try
            {
                xlAppFromDelegate = xlApp;
                xlWorkBookFromDelegate = xlWorkBook;
                xlWorkSheetAnalyseFromdelegate = xlWSAnalyse;

                SaveExcel();
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Form Chart "
        private void BtnChart_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(() => { OpenChartForm(); }) { Priority = ThreadPriority.Lowest };
            th.Start();
        }

        private void OpenChartForm()
        {
            if (InvokeRequired)
                BeginInvoke(new System.Action(() => InternalOpenChartForm()));
            else
                InternalOpenChartForm();
        }
        private void InternalOpenChartForm()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            try
            {
                FormChart NewOneInstanceOfFormChart = new FormChart();
                NewInstanceOfFormChart = NewOneInstanceOfFormChart;

                PassErreurLoadLimit DelegateToPassErreurLoadLimit = new PassErreurLoadLimit(NewInstanceOfFormChart.InitChartLoad);
                DelegateToPassErreurLoadLimit(dt);
                NewInstanceOfFormChart.Show();
            }
            catch (Exception)
            {
                FormChart NewOneInstanceOfFormChart = new FormChart();
                NewInstanceOfFormChart = NewOneInstanceOfFormChart;

                PassErreurLoadLimit DelegateToPassErreurLoadLimit = new PassErreurLoadLimit(NewInstanceOfFormChart.InitChartLoad);
                DelegateToPassErreurLoadLimit(dt);
                NewInstanceOfFormChart.Show();
            }
        }

        private void CloseFormChart()
        {
            NewInstanceOfFormChart.Close();
        }
        #endregion

        #region " Sauvegarde du fichier Excel "
        private void SaveExcel()
        {
            string sPathForSaveDocExcel = sDefaultPath + @"\" + Convert.ToString(DateTime.Now).Replace("/", "_") + "_" + tbxFile.Text + "_" + cbxSn.SelectedValue + ".xlsx";
            try
            {
                xlApp.DisplayAlerts = false;
                xlWorkBookFromDelegate.SaveAs(sPathForSaveDocExcel, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Excel.XlSaveAsAccessMode.xlNoChange,
                true, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                xlWorkBookFromDelegate.Close();
                xlAppFromDelegate.Quit();

                xlAppFromDelegate = null;
                xlWorkSheetAnalyseFromdelegate = null;
                xlWorkBookFromDelegate = null;
            }
            catch (Exception)
            {
            }
        }
        #endregion

        #region " Write to doc for Data average "
        public void UpdateWriteToDocFileAverage()
        {
            if (InvokeRequired)
                BeginInvoke(new System.Action(() => InternalUpdateWriteToDocFileAverage()));
            else
                InternalUpdateWriteToDocFileAverage();
        }
        public void InternalUpdateWriteToDocFileAverage()
        {
            Thread ThreadToWriteDataAverage = new Thread(() =>
            {
                string sLine = "";

                using (StreamWriter FileOfMeasure = File.AppendText(sPathForDataAverageCsvFile))
                {

                    if (ClassVariablesGlobales.bLaunchAverageMeasurement)
                    {
                        FileOfMeasure.WriteLine("Nouvelle acquisition du " + DateTime.Now.ToLocalTime());

                        FileOfMeasure.WriteLine(
                            "Ull" + ";" +
                            "Lll" + ";" +
                            "Error Load"
                            );

                        //On rebascule pour bloquer cette ligne d'information
                        ClassVariablesGlobales.bLaunchAverageMeasurement = false;
                    }

                    try
                    {
                        sLine =
                        ListForAverageMeasure.Select(x => x.Ull).Last().ToString() + ";" +
                        ListForAverageMeasure.Select(x => x.Lll).Last().ToString() + ";" +
                        ListForAverageMeasure.Select(x => x.ErreurLoad).Last().ToString();

                        FileOfMeasure.WriteLine(sLine);
                    }
                    catch (System.InvalidOperationException)
                    {
                        //De temps en temps il y a cette exception qui apparait, sans trop savoir pourquoi
                    }
                }
            })
            {
                Priority = ThreadPriority.BelowNormal
            };
            ThreadToWriteDataAverage.Start();
        }
        #endregion

        #region " Bouton open Chart "
        private void BtnOpenChart_Click(object sender, EventArgs e)
        {
            LauchWB();
        }

        private void LauchWB()
        {
            PassInvokeLaunchWb = new DelegateLaunchWb(InvokeLaunchWb);
            PassInvokeLaunchWb();
        }

        private void InvokeLaunchWb()
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeLaunchWb()));
            else
                InternalInvokeLaunchWb();
        }

        private void InternalInvokeLaunchWb()
        {
            if (Wb.Visible == false)
            {
                Wb.Visible = true;
                Wb.Width = Sequencial_GroupBox.Width;
                Wb.Height = Sequencial_GroupBox.Height;
                Wb.BringToFront();

                if (bFlagGo)
                {
                    var vIndex = dgvCycle.SelectedRows;
                    double dTargetLoad = -Convert.ToDouble(vIndex[0].Cells[4].Value);
                    //On passe l'info a la form Chart
                    Thread threadForUpdateConsigneLoad = new Thread(() => { UpdateConsigneLoad(Convert.ToString(dTargetLoad)); })
                    {
                        Priority = ThreadPriority.BelowNormal
                    };
                    threadForUpdateConsigneLoad.Start();
                }
            }
            else
            { Wb.Visible = false; }
        }
        #endregion

        #region " On passe le delegate pour lancer l'animation html "
        public void InvokeForLaunching()
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeForLaunching()));
            else
                InternalInvokeForLaunching();
        }

        private void InternalInvokeForLaunching()
        {
            try
            {
                //Wb.Navigate(new Uri(sAppDir));

                if (ListForAverageMeasure.Count() > 0)
                { UpdateErrorLoad(ListForAverageMeasure, true); }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Consigne Load "
        public void UpdateConsigneLoad(string sConsigneMesure)
        {
            PassInvokeUpdateConsigneLoad = new DelegateUpdateConsigneLoad(InvokeUpdateConsigneLoad);
            PassInvokeUpdateConsigneLoad(sConsigneMesure);
        }

        public void InvokeUpdateConsigneLoad(string sConsigneMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateConsigneLoad(sConsigneMesure)));
            else
                InternalInvokeUpdateConsigneLoad(sConsigneMesure);
        }

        private void InternalInvokeUpdateConsigneLoad(string sConsigneMesure)
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;
                    Object[] objArray = new Object[1];
                    objArray[0] = (object)sConsigneMesure;
                    Wb.Document.InvokeScript("UpdateConsigne", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update mesure position "
        public void UpdateMesurePosition(string sPositionMesure)
        {
            PassInvokeUpdateMesurePosition = new DelegateUpdateMesurePosition(InvokeUpdateMesurePosition);
            PassInvokeUpdateMesurePosition(sPositionMesure);
        }

        public void InvokeUpdateMesurePosition(string sPositionMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateMesurePosition(sPositionMesure)));
            else
                InternalInvokeUpdateMesurePosition(sPositionMesure);
        }

        private void InternalInvokeUpdateMesurePosition(string sPositionMesure)
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;
                    Object[] objArray = new Object[1];
                    objArray[0] = (Object)sPositionMesure;
                    Wb.Document.InvokeScript("UpdatePosition", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Mesure Load "
        public void UpdateMesureLoad(string sLoadMesure)
        {
            PassInvokeUpdateMesureLoad = new DelegateUpdateMesureLoad(InvokeUpdateMesureLoad);
            PassInvokeUpdateMesureLoad(sLoadMesure);
        }

        public void InvokeUpdateMesureLoad(string sLoadMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateMesureLoad(sLoadMesure)));
            else
                InternalInvokeUpdateMesureLoad(sLoadMesure);
        }

        private void InternalInvokeUpdateMesureLoad(string sLoadMesure)
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;
                    Object[] objArray = new Object[1];
                    objArray[0] = (Object)sLoadMesure;
                    Wb.Document.InvokeScript("UpdateLoad", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Id1 "
        public void UpdateId1(int iStep,string sConsigneMesure)
        {
            PassInvokeUpdateMesureId1 = new DelegateUpdateId1(InvokeUpdateId1);
            PassInvokeUpdateMesureId1(iStep,sConsigneMesure);
        }

        public void InvokeUpdateId1(int iStep,string sMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateId1(iStep, sMesure)));
            else
                InternalInvokeUpdateId1(iStep, sMesure);
        }

        private void InternalInvokeUpdateId1(int xVal, string sMesure)
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;
                    Object[] objArray = new Object[2];
                    objArray[0] = (object)xVal;
                    objArray[1] = (object)sMesure;
                    Wb.Document.InvokeScript("UpdateId1", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Id2 "
        public void UpdateId2(int iStep,string sConsigneMesure)
        {
            PassInvokeUpdateMesureId2 = new DelegateUpdateId2(InvokeUpdateId2);
            PassInvokeUpdateMesureId2(iStep,sConsigneMesure);
        }

        public void InvokeUpdateId2(int iStep,string sMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateId2(iStep,sMesure)));
            else
                InternalInvokeUpdateId2(iStep,sMesure);
        }

        private void InternalInvokeUpdateId2(int xVal, string sMesure)
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;
                    Object[] objArray = new Object[2];
                    objArray[0] = (object)xVal;
                    objArray[1] = (object)sMesure;
                    Wb.Document.InvokeScript("UpdateId2", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Id3 "
        public void UpdateId3(int iStep,string sConsigneMesure)
        {
            PassInvokeUpdateMesureId3 = new DelegateUpdateId3(InvokeUpdateId3);
            PassInvokeUpdateMesureId3(iStep,sConsigneMesure);
        }

        public void InvokeUpdateId3(int iStep,string sMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateId3(iStep,sMesure)));
            else
                InternalInvokeUpdateId3(iStep,sMesure);
        }

        private void InternalInvokeUpdateId3(int xVal,string sMesure)
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;
                    Object[] objArray = new Object[2];
                    objArray[0] = (object)xVal;
                    objArray[1] = (object)sMesure;
                    Wb.Document.InvokeScript("UpdateId3", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Id4 "
        public void UpdateId4(int iStep,string sConsigneMesure)
        {
            PassInvokeUpdateMesureId4 = new DelegateUpdateId4(InvokeUpdateId4);
            PassInvokeUpdateMesureId4(iStep,sConsigneMesure);
        }

        public void InvokeUpdateId4(int iStep,string sMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateId4(iStep, sMesure)));
            else
                InternalInvokeUpdateId4(iStep, sMesure);
        }

        private void InternalInvokeUpdateId4(int xVal,string sMesure)
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;
                    Object[] objArray = new Object[2];
                    objArray[0] = (object)xVal;
                    objArray[1] = (object)sMesure;
                    Wb.Document.InvokeScript("UpdateId4", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Id5 "
        public void UpdateId5(int iStep,string sConsigne,string sMesure)
        {
            PassInvokeUpdateMesureId5 = new DelegateUpdateId5(InvokeUpdateId5);
            PassInvokeUpdateMesureId5(iStep,sConsigne,sMesure);
        }

        public void InvokeUpdateId5(int iStep,string sConsigne, string sMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateId5(iStep, sConsigne,sMesure)));
            else
                InternalInvokeUpdateId5(iStep,sConsigne,sMesure);
        }

        private void InternalInvokeUpdateId5(int xVal, string sConsigne,string sMesure)
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;
                    Object[] objArray = new Object[3];
                    objArray[0] = (object)xVal;
                    objArray[1] = (object)sConsigne;
                    objArray[2] = (object)sMesure;
                    Wb.Document.InvokeScript("UpdateId5", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Set Interval & data length to html chart "
        public void SetChartData(string sInterval, string sDataLength)
        {
            PassInvokeIntervalDataLength = new DelegateIntervalDataLength(InvokeIntervalDataLength);
            PassInvokeIntervalDataLength(sInterval, sDataLength);
        }

        public void InvokeIntervalDataLength(string sInterval, string sDataLength)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeIntervalDataLength(sInterval, sDataLength)));
            else
                InternalInvokeIntervalDataLength(sInterval, sDataLength);
        }

        private void InternalInvokeIntervalDataLength(string sInterval, string sDataLength)
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;
                    Object[] objArray = new Object[2];
                    objArray[0] = (object)sInterval;
                    objArray[1] = (object)sDataLength;
                    Wb.Document.InvokeScript("SetInterval_Length", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Stop Chart html "
        public void StopChart()
        {
            PassInvokeStopChart = new DelegateStopChart(InvokeStopChart);
            PassInvokeStopChart();
        }

        public void InvokeStopChart()
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeStopChart()));
            else
                InternalInvokeStopChart();
        }

        private void InternalInvokeStopChart()
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;
                    Wb.Document.InvokeScript("StopChart");
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update ErrorLoad "
        public void UpdateErrorLoad(List<ClassAverageMeasure> lErrorLoad, bool bInProgress)
        {
            PassInvokeUpdateErrorLoadChart = new DelegateUpdateErrorLoadChart(InvokeUpdateErrorLoad);
            PassInvokeUpdateErrorLoadChart(lErrorLoad, bInProgress);
        }

        public void InvokeUpdateErrorLoad(List<ClassAverageMeasure> lErrorLoad, bool bInProgress)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateErrorLoad(lErrorLoad, bInProgress)));
            else
                InternalInvokeUpdateErrorLoad(lErrorLoad, bInProgress);
        }

        private void InternalInvokeUpdateErrorLoad(List<ClassAverageMeasure> lErrorLoad, bool bInProgress)
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;

                    if (bInProgress)
                    {
                        foreach (ClassAverageMeasure elem in lErrorLoad)
                        { InvokeErrorPush(elem, doc); }
                    }
                    else
                    {
                        ClassAverageMeasure ListToPass = lErrorLoad.Last();
                        InvokeErrorPush(ListToPass, doc);
                        ;
                    }

                    doc.InvokeScript("LaunchErrorChart");
                }
            }
            catch (Exception)
            { }
        }

        private void InvokeErrorPush(ClassAverageMeasure ListToPass, HtmlDocument doc)
        {
            object[] obj = new object[4];

            string step = Convert.ToInt32(Convert.ToDouble(ListToPass.ConsigneLoad.ToString())).ToString();
            string ull = ListToPass.Ull.ToString();
            string lll = ListToPass.Lll.ToString();
            string errorload = ListToPass.ErreurLoad.ToString();

            obj[0] = (object)step;
            obj[1] = (object)errorload;
            obj[2] = (object)ull;
            obj[3] = (object)lll;

            doc.InvokeScript("GenerateErrorData", obj);
        }
        #endregion

        #region " Update Signal Data "
        public void UpdateSignalData()
        {
            PassInvokeUpdateSignalChart = new DelegateUpdateSignalChart(InvokeUpdateSignalData);
            PassInvokeUpdateSignalChart();
        }

        public void InvokeUpdateSignalData()
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateSignalData()));
            else
                InternalInvokeUpdateSignalData();
        }

        private void InternalInvokeUpdateSignalData()
        {
            try
            {
                if (Wb.Document != null)
                {
                    HtmlDocument doc = Wb.Document;

                    doc.InvokeScript("GenerateDataSignal");
                    doc.InvokeScript("LaunchSignalChart");
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Gestion du radio button "
        private void RdbEtuve_CheckedChanged(object sender, EventArgs e)
        {
            Testrdb();
        }

        private void Testrdb()
        {
            if (RdbBancDeForce.Checked)
            {
                tbxConsigneEtuve.Text = "0";
                tbxMesureEtuve.Text = "0";
                TbxPosition.Text = "0";
                tbxLoad.Text = "0";
            }
            else
            {
                TbxPosition.Text = "0";
                tbxLoad.Text = "0";
                tbxConsigneEtuve.Text = "0";
                tbxMesureEtuve.Text = "0";
            }
        }

        private void BtnResetHostData_Click(object sender, EventArgs e)
        {
            if (TbxHost1.Enabled)
            {
                //On libére les datas pour permettre de changer l'adresse Ip
                TbxHost1.Enabled = false;
                TbxHost2.Enabled = false;
                TbxHost3.Enabled = false;
                TbxHost4.Enabled = false;
                TbxHostPortName.Enabled = false;
            }
            else
            {
                TbxHost1.Enabled = true;
                TbxHost2.Enabled = true;
                TbxHost3.Enabled = true;
                TbxHost4.Enabled = true;
                TbxHostPortName.Enabled = true;
            }
        }
        #endregion
    }
}

