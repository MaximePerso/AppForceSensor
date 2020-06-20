using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Doli.DoPE;
using static K2000Rs232App.MainWindow;

namespace K2000Rs232App
{
    class Doli : IDisposable
    {
        private Edc MyEdc = null;
        private short MyTan;
        private const string CommandFailedString = "Erreur de commande. Vérifiez que l'EDC est bien initialisé. \n";
        private readonly int SensorId = 0;

        public DelegateStatusErrorRichTextBoxFromMesure PassStatusErrorRichTextBox { get; set; }
        public DelegateTbxPosition PassTbxPosition { get; set; }
        public DelegateTbxForce PassTbxForce { get; set; }
        public DelegateOnPosMsg PassDelegateOnPosMsg { get; set; }
        public DelegateOnCommandError PassDelegateOnCommandError {get; set;}

        public DelegateOnSftMsg PassDelegateOnSftMsg { get; set; }
        public DelegateOnOffsCMsg PassDelegateOnOffsCMsg { get; set; }
        public DelegateOnCheckMsg PassDelegateOnCheckMsg { get; set; }
        public DelegateOnShieldMsg PassDelegateOnShieldMsg { get; set; }
        public DelegateOnRefSignalMsg PassDelegateOnRefSignalMsg { get; set; }
        public DelegateOnSensorMsg PassDelegateOnSensorMsg { get; set; }
        public DelegateOnIoSHaltMsg PassDelegateOnIoSHaltMsg { get; set; }
        public DelegateOnKeyMsg PassDelegateOnKeyMsg { get; set; }
        public DelegateOnRuntimeError PassDelegateOnRuntimeError { get; set; }
        public DelegaeOnOverflow PassDelegateOnOverflow { get; set; }
        public DelegateOnDebugMsg PassDelegateOnDebugMsg { get; set; }
        public DelegateOnSystemMsg PassDelegateOnSystemMsg { get; set; }
        public DelegateOnRmcEvent PassDelegateOnRmcEvent {get; set;}

        public enum DoliControl {Position,Load,Extension};
        public enum DoliVitesseMiseEnApproche { One_mm_min=1,Two_mm_min=2,Three_mm_min=3,Four_mm_min=4,Five_mm_min=5,Ten_mm_s=10,Twenty_mm_s=20};
        public enum DoliPositionMiseEnApproche { One_mm = 1, Two_mm=2, Three_mm=3, Four_mm=4, Five_mm=5,Ten_mm=10,Twenty_mm=20,Fifty_mm=50 };
        public bool bDebug;

        #region " Display "
        private void DisplayError(DoPE.ERR error, string Text)
        {
            if (error != DoPE.ERR.NOERROR)
                Display(Text + " Error: " + error + "\n");
        }

        ///----------------------------------------------------------------------
        /// <summary>Display debug text</summary>
        ///----------------------------------------------------------------------
        private void Display(string Text)
        {
            List<ClassForResult> ListForError = new List<ClassForResult>{new ClassForResult { Id = 0, Measure = Text, Designation = "Doli" }};
            PassStatusErrorRichTextBox(ListForError);
        }
        #endregion

        #region " Connext to EDC "
        public void ConnectToEdc()
        {
            // tell DoPE which DoPENet.dll and DoPE.dll version we are using
            // THE API CANNOT BE USED WITHOUT THIS CHECK !
            DoPE.CheckApi("2.81");

            Cursor.Current = Cursors.WaitCursor;

            try
            {
                DoPE.ERR error;

                // open the first EDC found on this PC
                MyEdc = new Edc(DoPE.OpenBy.DeviceId, SensorId);

                // hang in event-handler to receive DoPE-events
                MyEdc.Eh.OnLineHdlr += new DoPE.OnLineHdlr(OnLine);
#if ONDATABLOCK
        MyEdc.Eh.OnDataBlockHdlr += new DoPE.OnDataBlockHdlr(OnDataBlock);
        // Set number of samples for OnDataBlock events
        // (with 1 ms data refresh rate this leads to a
        //  display refresh every 300 ms)
        error = MyEdc.Eh.SetOnDataBlockSize(300);
        DisplayError(error, "SetOnDataBlockSize");
#else
                MyEdc.Eh.OnDataHdlr += new DoPE.OnDataHdlr(OnData);
#endif
                MyEdc.Eh.OnCommandErrorHdlr += new DoPE.OnCommandErrorHdlr(OnCommandError);
                MyEdc.Eh.OnPosMsgHdlr += new DoPE.OnPosMsgHdlr(OnPosMsg);
                MyEdc.Eh.OnTPosMsgHdlr += new DoPE.OnTPosMsgHdlr(OnTPosMsg);
                MyEdc.Eh.OnLPosMsgHdlr += new DoPE.OnLPosMsgHdlr(OnLPosMsg);
                MyEdc.Eh.OnSftMsgHdlr += new DoPE.OnSftMsgHdlr(OnSftMsg);
                MyEdc.Eh.OnOffsCMsgHdlr += new DoPE.OnOffsCMsgHdlr(OnOffsCMsg);
                MyEdc.Eh.OnCheckMsgHdlr += new DoPE.OnCheckMsgHdlr(OnCheckMsg);
                MyEdc.Eh.OnShieldMsgHdlr += new DoPE.OnShieldMsgHdlr(OnShieldMsg);
                MyEdc.Eh.OnRefSignalMsgHdlr += new DoPE.OnRefSignalMsgHdlr(OnRefSignalMsg);
                MyEdc.Eh.OnSensorMsgHdlr += new DoPE.OnSensorMsgHdlr(OnSensorMsg);
                MyEdc.Eh.OnIoSHaltMsgHdlr += new DoPE.OnIoSHaltMsgHdlr(OnIoSHaltMsg);
                MyEdc.Eh.OnKeyMsgHdlr += new DoPE.OnKeyMsgHdlr(OnKeyMsg);
                MyEdc.Eh.OnRuntimeErrorHdlr += new DoPE.OnRuntimeErrorHdlr(OnRuntimeError);
                MyEdc.Eh.OnOverflowHdlr += new DoPE.OnOverflowHdlr(OnOverflow);
                MyEdc.Eh.OnSystemMsgHdlr += new DoPE.OnSystemMsgHdlr(OnSystemMsg);
                MyEdc.Eh.OnDebugMsgHdlr += new DoPE.OnDebugMsgHdlr(OnDebugMsg);
                MyEdc.Eh.OnRmcEventHdlr += new DoPE.OnRmcEventHdlr(OnRmcEvent);
                MyEdc.Rmc.Enable(-1, -1);

                // Set UserScale
                DoPE.UserScale userScale = new DoPE.UserScale();
                // set position and extension scale to mm
                userScale[DoPE.SENSOR.SENSOR_S] = 1000;
                userScale[DoPE.SENSOR.SENSOR_E] = 1000;

                // Select machine setup and initialize
                error = MyEdc.Setup.SelSetup(DoPE.SETUP_NUMBER.SETUP_1, userScale, ref MyTan, ref MyTan);
                if (error != DoPE.ERR.NOERROR)
                    DisplayError(error, "SelectSetup");
                else
                    Display("SelectSetup : OK !\n");
            }
            catch (DoPEException ex)
            {
                // During the initialization and the
                // shut-down phase a DoPE Exception can arise.
                // Other errors are reported by the DoPE
                // error return codes.
                Display(string.Format("{0}\n", ex));
            }

            Cursor.Current = Cursors.Default;
        }
        #endregion

        #region "Swtich ON Doli"
        public void DoliOn()
        {
            try
            {
                DoPE.ERR error = MyEdc.Move.On();
                DisplayError(error, "On");

            }
            catch (NullReferenceException)
            {
                Display(CommandFailedString);
            }
        }
        #endregion

        #region "Switch Off Doli "
        public void DoliOff()
        {
            try
            {
                DoPE.ERR error = MyEdc.Move.Off();
                DisplayError(error, "Off");
            }
            catch (NullReferenceException)
            {
                Display(CommandFailedString);
            }
        }
        #endregion

        #region " Hatl de Doli "
        public void HaltDoli()
        {
            try
            {
                DoPE.ERR error = MyEdc.Move.Halt(DoPE.CTRL.POS, ref MyTan);
                DisplayError(error, "Halt");
            }
            catch (NullReferenceException)
            {Display(CommandFailedString);}
        }
        #endregion

        #region " Move SDoli " 
        public void MoveDoli()
        {
            try
            {
                DoPE.ERR error = MyEdc.Move.FDPoti(DoPE.CTRL.POS, 0, DoPE.SENSOR.SENSOR_DP, 3, DoPE.EXT.SPEED_UP, 2, ref MyTan);
                DisplayError(error, "FDPoti");
            }
            catch (NullReferenceException)
            {
                Display(CommandFailedString);
            }
        }
        #endregion  

        #region DoPE Events
        private int OnLine(DoPE.LineState LineState, object Parameter)
        {
            Display(string.Format("OnLine: {0}\n", LineState));

            return 0;
        }

#if ONDATABLOCK
    private int OnDataBlock(ref DoPE.OnDataBlock Block, object Parameter)
    {
      if (Block.Data.Length > 0)
      {
        // refesh edit controls with the latest sample
        DoPE.Data Sample = Block.Data[Block.Data.Length - 1].Data;
        string text;

        text = String.Format("{0}", Sample.Time.ToString("0.000"));
        guiTime.Text = text;
        text = String.Format("{0}", Sample.Sensor[(int)DoPE.SENSOR.SENSOR_S].ToString("0.000"));
        guiPosition.Text = text;
        text = String.Format("{0}", Sample.Sensor[(int)DoPE.SENSOR.SENSOR_F].ToString("0.000"));
        guiLoad.Text = text;
        text = String.Format("{0}", Sample.Sensor[(int)DoPE.SENSOR.SENSOR_E].ToString("0.000"));
        guiExtension.Text = text;
      }
      return 0;
    }
#else
        private Int32 LastTime = Environment.TickCount;

        private int OnData(ref DoPE.OnData Data, object Parameter)
        {
            if (Data.DoPError == DoPE.ERR.NOERROR)
            {
                DoPE.Data Sample = Data.Data;
                Int32 Time = Environment.TickCount;
                if ((Time - LastTime) >= 200 /*ms*/)
                {
                    LastTime = Time;
                    string text;


                    //POur le debug
                    if (bDebug)
                    {
                        Random rndPos = new Random();
                        string sOutputDataPosition = Convert.ToString(rndPos.Next(-2000, 2000000));
                        PassTbxPosition(sOutputDataPosition);

                        Random rndLoad = new Random();
                        string sOutputDataLoad = Convert.ToString(rndLoad.Next(-2000, 2000000));
                        PassTbxForce(sOutputDataLoad);
                    }
                    else
                    {

                        text = String.Format("{0}", Sample.Sensor[(int)DoPE.SENSOR.SENSOR_S].ToString("0.000"));
                        PassTbxPosition(text);
                        text = String.Format("{0}", Sample.Sensor[(int)DoPE.SENSOR.SENSOR_F].ToString("0.000"));
                        PassTbxForce(text);
                    }
                }
            }
            return 0;
        }
#endif

        private int OnCommandError(ref DoPE.OnCommandError CommandError, object Parameter)
        {
            Display(string.Format("OnCommandError: CommandNumber={0} ErrorNumber={1} usTAN={2} \n",
              CommandError.CommandNumber, CommandError.ErrorNumber, CommandError.usTAN));

            PassDelegateOnCommandError(CommandError.CommandNumber, CommandError.ErrorNumber, CommandError.usTAN);
            return 0;
        }

        private int OnPosMsg(ref DoPE.OnPosMsg PosMsg, object Parameter)
        {
            Display(string.Format("OnPosMsg: DoPError={0} Reached={1} Time={2} Control={3} Position={4} DControl={5} Destination={6} usTAN={7} \n",
              PosMsg.DoPError, PosMsg.Reached, PosMsg.Time, PosMsg.Control, PosMsg.Position, PosMsg.DControl, PosMsg.Destination, PosMsg.usTAN));

            PassDelegateOnPosMsg(PosMsg.DoPError, PosMsg.Reached, PosMsg.Time, PosMsg.Control, PosMsg.Position, PosMsg.DControl, PosMsg.Destination, PosMsg.usTAN);
            return 0;
        }

        private int OnTPosMsg(ref DoPE.OnPosMsg PosMsg, object Parameter)
        {
            Display(string.Format("OnTPosMsg: DoPError={0} Reached={1} Time={2} Control={3} Position={4} DControl={5} Destination={6} usTAN={7} \n",
              PosMsg.DoPError, PosMsg.Reached, PosMsg.Time, PosMsg.Control, PosMsg.Position, PosMsg.DControl, PosMsg.Destination, PosMsg.usTAN));

            PassDelegateOnPosMsg(PosMsg.DoPError, PosMsg.Reached, PosMsg.Time, PosMsg.Control, PosMsg.Position, PosMsg.DControl, PosMsg.Destination, PosMsg.usTAN);
            return 0;
        }

        private int OnLPosMsg(ref DoPE.OnPosMsg PosMsg, object Parameter)
        {
            Display(string.Format("OnLPosMsg: DoPError={0} Reached={1} Time={2} Control={3} Position={4} DControl={5} Destination={6} usTAN={7} \n",
              PosMsg.DoPError, PosMsg.Reached, PosMsg.Time, PosMsg.Control, PosMsg.Position, PosMsg.DControl, PosMsg.Destination, PosMsg.usTAN));

            PassDelegateOnPosMsg(PosMsg.DoPError, PosMsg.Reached, PosMsg.Time, PosMsg.Control, PosMsg.Position, PosMsg.DControl, PosMsg.Destination, PosMsg.usTAN);
            return 0;
        }

        private int OnSftMsg(ref DoPE.OnSftMsg SftMsg, object Parameter)
        {
            Display(string.Format("OnSftMsg: DoPError={0} Upper={1} Time={2} Control={3} Position={4} usTAN={5} \n",
              SftMsg.DoPError, SftMsg.Upper, SftMsg.Time, SftMsg.Control, SftMsg.Position, SftMsg.usTAN));

            PassDelegateOnSftMsg(SftMsg.DoPError, SftMsg.Upper, SftMsg.Time, SftMsg.Control, SftMsg.Position, SftMsg.usTAN);
            return 0;
        }

        private int OnOffsCMsg(ref DoPE.OnOffsCMsg OffsCMsg, object Parameter)
        {
            Display(string.Format("OnOffsCMsg: DoPError={0} Time={1} Offset={2} usTAN={3} \n",
              OffsCMsg.DoPError, OffsCMsg.Time, OffsCMsg.Offset, OffsCMsg.usTAN));

            PassDelegateOnOffsCMsg(OffsCMsg.DoPError, OffsCMsg.Time, OffsCMsg.Offset, OffsCMsg.usTAN);
            return 0;
        }

        private int OnCheckMsg(ref DoPE.OnCheckMsg CheckMsg, object Parameter)
        {
            Display(string.Format("OnCheckMsg: DoPError={0} Action={1} Time={2} CheckId={3} Position={4} SensorNo={5} usTAN={6} \n",
              CheckMsg.DoPError, CheckMsg.Action, CheckMsg.Time, CheckMsg.CheckId, CheckMsg.Position, CheckMsg.SensorNo, CheckMsg.usTAN));

            PassDelegateOnCheckMsg(CheckMsg.DoPError, CheckMsg.Action, CheckMsg.Time, CheckMsg.CheckId, CheckMsg.Position, CheckMsg.SensorNo, CheckMsg.usTAN);
            return 0;
        }

        private int OnShieldMsg(ref DoPE.OnShieldMsg ShieldMsg, object Parameter)
        {
            Display(string.Format("OnShieldMsg: DoPError={0} Action={1} Time={2} SensorNo={3} Position={4} usTAN={5} \n",
              ShieldMsg.DoPError, ShieldMsg.Action, ShieldMsg.Time, ShieldMsg.SensorNo, ShieldMsg.Position, ShieldMsg.usTAN));

            PassDelegateOnShieldMsg(ShieldMsg.DoPError, ShieldMsg.Action, ShieldMsg.Time, ShieldMsg.SensorNo, ShieldMsg.Position, ShieldMsg.usTAN);
            return 0;
        }

        private int OnRefSignalMsg(ref DoPE.OnRefSignalMsg RefSignalMsg, object Parameter)
        {
            Display(string.Format("OnRefSignalMsg: DoPError={0} Time={1} SensorNo={2} Position={3} usTAN={4} \n",
              RefSignalMsg.DoPError, RefSignalMsg.Time, RefSignalMsg.SensorNo, RefSignalMsg.Position, RefSignalMsg.usTAN));

            PassDelegateOnRefSignalMsg(RefSignalMsg.DoPError, RefSignalMsg.Time, RefSignalMsg.SensorNo, RefSignalMsg.Position, RefSignalMsg.usTAN);
            return 0;
        }

        private int OnSensorMsg(ref DoPE.OnSensorMsg SensorMsg, object Parameter)
        {
            Display(string.Format("OnSensorMsg: DoPError={0} Time={1} SensorNo={2} usTAN={3} \n",
              SensorMsg.DoPError, SensorMsg.Time, SensorMsg.SensorNo, SensorMsg.usTAN));

            PassDelegateOnSensorMsg(SensorMsg.DoPError, SensorMsg.Time, SensorMsg.SensorNo, SensorMsg.usTAN);
            return 0;
        }

        private int OnIoSHaltMsg(ref DoPE.OnIoSHaltMsg IoSHaltMsg, object Parameter)
        {
            Display(string.Format("OnIoSHaltMsg: DoPError={0} Upper={1} Time={2} Control={3} Position={4} usTAN={5} \n",
              IoSHaltMsg.DoPError, IoSHaltMsg.Upper, IoSHaltMsg.Time, IoSHaltMsg.Control, IoSHaltMsg.Position, IoSHaltMsg.usTAN));

            PassDelegateOnIoSHaltMsg(IoSHaltMsg.DoPError, IoSHaltMsg.Upper, IoSHaltMsg.Time, IoSHaltMsg.Control, IoSHaltMsg.Position, IoSHaltMsg.usTAN);
            return 0;
        }

        private int OnKeyMsg(ref DoPE.OnKeyMsg KeyMsg, object Parameter)
        {
            Display(string.Format("OnKeyMsg: DoPError={0} Time={1} Keys={2} NewKeys={3} GoneKeys={4} OemKeys={5} NewOemKeys={6} GoneOemKeys={7} usTAN={8} \n",
              KeyMsg.DoPError, KeyMsg.Time, KeyMsg.Keys, KeyMsg.NewKeys, KeyMsg.GoneKeys, KeyMsg.OemKeys, KeyMsg.NewOemKeys, KeyMsg.GoneOemKeys, KeyMsg.usTAN));

            PassDelegateOnKeyMsg(KeyMsg.DoPError, KeyMsg.Time, KeyMsg.Keys, KeyMsg.NewKeys, KeyMsg.GoneKeys, KeyMsg.OemKeys, KeyMsg.NewOemKeys, KeyMsg.GoneOemKeys, KeyMsg.usTAN);
            return 0;
        }

        private int OnRuntimeError(ref DoPE.OnRuntimeError RuntimeError, object Parameter)
        {
            Display(string.Format("OnRuntimeError: DoPError={0} ErrorNumber={1} Time={2} Device={3} Bits={4} usTAN={5} \n",
              RuntimeError.DoPError, RuntimeError.ErrorNumber, RuntimeError.Time, RuntimeError.Device, RuntimeError.Bits, RuntimeError.usTAN));

            PassDelegateOnRuntimeError(RuntimeError.DoPError, RuntimeError.ErrorNumber, RuntimeError.Time, RuntimeError.Device, RuntimeError.Bits, RuntimeError.usTAN);
            return 0;
        }

        private int OnOverflow(int Overflow, object Parameter)
        {
            Display(string.Format("OnOverflow: Overflow={0} \n", Overflow));

            PassDelegateOnOverflow(Overflow);
            return 0;
        }

        private int OnDebugMsg(ref DoPE.OnDebugMsg DebugMsg, object Parameter)
        {
            Display(string.Format("OnDebugMsg: DoPError={0} MsgType={1} Time={2} Text={3} \n",
              DebugMsg.DoPError, DebugMsg.MsgType, DebugMsg.Time, DebugMsg.Text));

            PassDelegateOnDebugMsg(DebugMsg.DoPError, DebugMsg.MsgType, DebugMsg.Time, DebugMsg.Text);
            return 0;
        }

        private int OnSystemMsg(ref DoPE.OnSystemMsg SystemMsg, object Parameter)
        {
            Display(string.Format("OnSystemMsg: DoPError={0} MsgNumber={1} Time={2} Text={3} \n",
              SystemMsg.DoPError, SystemMsg.MsgNumber, SystemMsg.Time, SystemMsg.Text));

            PassDelegateOnSystemMsg(SystemMsg.DoPError, SystemMsg.MsgNumber, SystemMsg.Time, SystemMsg.Text);
            return 0;
        }

        private int OnRmcEvent(ref DoPE.OnRmcEvent RmcEvent, object Parameter)
        {
            Display(string.Format("OnRmcEvent: Keys={0} NewKeys={1} GoneKeys={2} Leds={3} NewLeds={4} GoneLeds={5} \n",
              RmcEvent.Keys, RmcEvent.NewKeys, RmcEvent.GoneKeys, RmcEvent.Leds, RmcEvent.NewLeds, RmcEvent.GoneLeds));

            PassDelegateOnRmcEvent(RmcEvent.Keys, RmcEvent.NewKeys, RmcEvent.GoneKeys, RmcEvent.Leds, RmcEvent.NewLeds, RmcEvent.GoneLeds);
            return 0;
        }
        #endregion

        #region " Tare en Load "
        public void DoliTareLoad (double LoadValue)
        {
            //On fait la tare en load
            DoPE.ERR TestErreur = MyEdc.Tare.SetTare(DoPE.SENSOR.SENSOR_F, LoadValue);
            DisplayError(TestErreur, "Tare");
        }
        #endregion

        #region " Tare en position "
        public void DoliTarePos(double LoadValue)
        {
            //On fait la tare en position
            DoPE.ERR TestErreur = MyEdc.Tare.SetTare(DoPE.SENSOR.SENSOR_S, LoadValue);
            DisplayError(TestErreur, "Tare");
        }
        #endregion

        #region " MIse a l'approche en phase "
        public void MiseEnApproche(DoPE.CTRL ModeOfControl, DoliVitesseMiseEnApproche Speed, DoliPositionMiseEnApproche Destination)
        {
            try
            {
                Int32 i = MyEdc.DoPEDllHdl;
                //Reset des limites préalables avant la mise en place d'une limite de sécurité.
                MyEdc.Check.ClrCheckLimit();
                MyEdc.Check.SetCheckLimit(DoPE.SENSOR.SENSOR_F, -300, 0, 0, 0);

                DoPE.ERR error = MyEdc.Move.Pos((DoPE.CTRL)ModeOfControl, Convert.ToDouble(Speed), Convert.ToDouble(Destination), ref MyTan);
                DisplayError(error, "Pos");
            }
            catch (NullReferenceException)
            {
                Display(CommandFailedString);
            }
        }
        #endregion

        #region " MIse a l'approche en phase "
        public void MiseEnApproche(DoPE.CTRL ModeOfControl, double dSpeed, double dDestination)
        {
            try
            {
                Int32 i = MyEdc.DoPEDllHdl;
                //Reset des limites préalables avant la mise en place d'une limite de sécurité.
                MyEdc.Check.ClrCheckLimit();
                MyEdc.Check.SetCheckLimit(DoPE.SENSOR.SENSOR_F, -300, 0, 0, 0);

                DoPE.ERR error = MyEdc.Move.Pos((DoPE.CTRL)ModeOfControl, dSpeed, dDestination, ref MyTan);
                DisplayError(error, "Pos");
            }
            catch (NullReferenceException)
            {
                Display(CommandFailedString);
            }
        }
        #endregion

        public void Dispose()
        {
            ((IDisposable)MyEdc).Dispose();
        }
    }
}
