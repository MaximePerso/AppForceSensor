using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using static K2000Rs232App.MainWindow;

namespace K2000Rs232App
{
    class TcpClientWithTimeout:IDisposable
    {
        protected string sHostNameAttchToClass;
        protected int iPortnameAttachToClass;

        protected TcpClient tcpConnection;
        protected bool bConnected;
        protected Exception Ex;

        //Init pour le dispose
        bool bDisposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        protected string sStxAscii2 = "$";
        protected string sEtxAscii2 = "\r";
        protected string sAdress = "01";
        protected int iMesureTimeOut = 100;

        public List<ClassForThreadMeasure> ListForMeasure = new List<ClassForThreadMeasure>();

        public DelegateOneShotMeasurementEtuveBE PassOneShotMeasurementEtuveBE { get; set; }
        public bool bDebug;

        //Commande spécifique
        protected string sCommandMesure = "I";

        #region " Init "
        public TcpClientWithTimeout(string sHostName, int iPortname)
        {
            sHostNameAttchToClass = sHostName;
            iPortnameAttachToClass = iPortname;
        }
        #endregion

        #region " Connect Tcp "
        public TcpClient Connect(int iTimeOutAttachToClass)
        {
            bConnected = false;
            Ex = null;
            Thread thread = new Thread(new ThreadStart(BeginConnect))
            { IsBackground = true };

            thread.Start();

            // On attend la fin du time out ou la fin de la tache
            thread.Join(iTimeOutAttachToClass);

            return ManageException(thread);

        }
        #endregion

        #region " Begin Connect " 
        protected void BeginConnect()
        {
            try
            {
                tcpConnection = new TcpClient(sHostNameAttchToClass, iPortnameAttachToClass);
                bConnected = true;
            }
            catch (Exception ex)
            { Ex = ex; }
        }
        #endregion

        #region " Dispose " 
        void IDisposable.Dispose()
        {
            tcpConnection.Dispose();
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (bDisposed)
            { return; }
                
            if (disposing)
            {  handle.Dispose(); }

            bDisposed = true;
        }
        #endregion

        #region " Manage Exception with Time out "
        private TcpClient ManageException(Thread th)
        {
            if (bConnected == true)
            {
                th.Abort();
                return tcpConnection;
            }
            if (Ex != null)
            {
                th.Abort();
                throw Ex;
            }
            else
            {
                th.Abort();
                string message = string.Format("TcpClient connection to {0}:{1} timed out",
                  sHostNameAttchToClass, iPortnameAttachToClass);
                throw new TimeoutException(message);
            }
        }
        #endregion

        #region " Command "
        public bool TcpCommmand(string sCommand,int iTimeOut)
        {
            ASCIIEncoding AsciiEnc = new ASCIIEncoding();
            List<string> lReturn = new List<string>();

            Stream StmWriteToTcp = tcpConnection.GetStream();
            byte[] SendByte = AsciiEnc.GetBytes(sStxAscii2 + sAdress + sCommand + sEtxAscii2);
            StmWriteToTcp.Write(SendByte, 0, SendByte.Length);

            Stream StmReadToTcp = tcpConnection.GetStream();
            byte[] ReadByte = new byte[1024];

            StmReadToTcp.ReadTimeout = iTimeOut;
            int iDataAvailable = 0;
            try { iDataAvailable = StmReadToTcp.Read(ReadByte, 0, ReadByte.Length); }
            catch(IOException)
            { throw new TimeoutException("TimeOut Read Data Etuve BE"); }

            string sPackage = "";

            for (int i = 0; i < iDataAvailable; i++)
            { sPackage = sPackage + Convert.ToChar(ReadByte[i]); }

            string[] sOutputData = sPackage.Split(' ');

            if(sOutputData[0].Equals("0"))
            { return true; }
            else
            {throw new TimeoutException("TimeOut Read Data Etuve BE");}
        }
        #endregion

        #region " Mesure "
        public bool Mesure(int iId, int lStepToTag)
        {
            ASCIIEncoding AsciiEnc = new ASCIIEncoding();
            List<string> lReturn = new List<string>();

            try
            {
                Stream StmWriteToTcp = tcpConnection.GetStream();
                byte[] SendByte = AsciiEnc.GetBytes(sStxAscii2 + sAdress + sCommandMesure + sEtxAscii2);
                StmWriteToTcp.Write(SendByte, 0, SendByte.Length);
            }
            catch(Exception)
            { return false; }

            try
            {
                Stream StmReadToTcp = tcpConnection.GetStream();
                byte[] ReadByte = new byte[1024];

                StmReadToTcp.ReadTimeout = iMesureTimeOut;
                int iDataAvailable = StmReadToTcp.Read(ReadByte, 0, ReadByte.Length);

                string sPackage = "";

                for (int i = 0; i < iDataAvailable; i++)
                { sPackage = sPackage + Convert.ToChar(ReadByte[i]); }

                string[] sOutputData = sPackage.Split(' ');

                ListForMeasure.Add(new ClassForThreadMeasure { Id = iId, Measure = sOutputData[1], Step = lStepToTag });
                PassOneShotMeasurementEtuveBE(iId, lStepToTag, sOutputData[0], sOutputData[1]);
            }
            catch(Exception)
            {
                PassOneShotMeasurementEtuveBE(iId, lStepToTag, "0", "0");
                return false;
            }

            return true;
        }
        #endregion

        private void ReadTcp()
        {
        /*
            int iWidthOfWord = SteamToTcp.Read(ReadByte, 0, ReadByte.Length);
            for (int i = 0; i < iWidthOfWord; i++)
            {
                lReturn.Add(Convert.ToString(ReadByte[i]));
            }
            */
        }        
    }
}
