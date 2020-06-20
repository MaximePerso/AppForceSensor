using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace K2000Rs232App
{
    public partial class FormChart : Form
    {
        #region " Init "
        private string sAppDir = Environment.CurrentDirectory + @"\Resources\HtmlChart\Chart.html";
        public delegate void DelegateLaunching();

        public delegate void DelegateUpdateConsigneLoad(string sLoadMesure);
        public DelegateUpdateConsigneLoad PassInvokeUpdateConsigneLoad{ get; set; }

        public delegate void DelegateUpdateMesurePosition(string sLoadMesure);
        public DelegateUpdateMesurePosition PassInvokeUpdateMesurePosition { get; set; }

        public delegate void DelegateUpdateMesureLoad(string sLoadMesure);
        public DelegateUpdateMesureLoad PassInvokeUpdateMesureLoad { get; set; }

        public FormChart()
        {
            InitializeComponent();
        }

        private void FormChart_Load(object sender, EventArgs e)
        {
            DelegateLaunching delForLaunching = InvokeForLaunching;

            Thread ThreadForWaitAnimation = new Thread(
                () =>
                {
                    InvokeForLaunching();
                });
            ThreadForWaitAnimation.Start();
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
            {WB.Navigate(new Uri(sAppDir));}
            catch (Exception)
            { }
        }
        #endregion

        public void InitChartLoad(System.Data.DataTable dt)
        {

        }

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
                if (WB.Document != null)
                {
                    HtmlDocument doc = WB.Document;
                    Object[] objArray = new Object[1];
                    objArray[0] = (object)sConsigneMesure;
                    WB.Document.InvokeScript("UpdateConsigne", objArray);
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
                if (WB.Document != null)
                {
                    HtmlDocument doc = WB.Document;
                    Object[] objArray = new Object[1];
                    objArray[0] = (Object)sPositionMesure;
                    WB.Document.InvokeScript("UpdatePosition", objArray);
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
                if (WB.Document != null)
                {
                    HtmlDocument doc = WB.Document;
                    Object[] objArray = new Object[1];
                    objArray[0] = (Object)sLoadMesure;
                    WB.Document.InvokeScript("UpdateLoad", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Id1 "
        public void UpdateId1(string sConsigneMesure)
        {
            PassInvokeUpdateConsigneLoad = new DelegateUpdateConsigneLoad(InvokeUpdateId1);
            PassInvokeUpdateConsigneLoad(sConsigneMesure);
        }

        public void InvokeUpdateId1(string sConsigneMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateId1(sConsigneMesure)));
            else
                InternalInvokeUpdateId1(sConsigneMesure);
        }

        private void InternalInvokeUpdateId1(string sConsigneMesure)
        {
            try
            {
                if (WB.Document != null)
                {
                    HtmlDocument doc = WB.Document;
                    Object[] objArray = new Object[1];
                    objArray[0] = (object)sConsigneMesure;
                    WB.Document.InvokeScript("UpdateId1", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Id2 "
        public void UpdateId2(string sConsigneMesure)
        {
            PassInvokeUpdateConsigneLoad = new DelegateUpdateConsigneLoad(InvokeUpdateId2);
            PassInvokeUpdateConsigneLoad(sConsigneMesure);
        }

        public void InvokeUpdateId2(string sConsigneMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateId2(sConsigneMesure)));
            else
                InternalInvokeUpdateId2(sConsigneMesure);
        }

        private void InternalInvokeUpdateId2(string sConsigneMesure)
        {
            try
            {
                if (WB.Document != null)
                {
                    HtmlDocument doc = WB.Document;
                    Object[] objArray = new Object[1];
                    objArray[0] = (object)sConsigneMesure;
                    WB.Document.InvokeScript("UpdateId2", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Id3 "
        public void UpdateId3(string sConsigneMesure)
        {
            PassInvokeUpdateConsigneLoad = new DelegateUpdateConsigneLoad(InvokeUpdateId3);
            PassInvokeUpdateConsigneLoad(sConsigneMesure);
        }

        public void InvokeUpdateId3(string sConsigneMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateId3(sConsigneMesure)));
            else
                InternalInvokeUpdateId3(sConsigneMesure);
        }

        private void InternalInvokeUpdateId3(string sConsigneMesure)
        {
            try
            {
                if (WB.Document != null)
                {
                    HtmlDocument doc = WB.Document;
                    Object[] objArray = new Object[1];
                    objArray[0] = (object)sConsigneMesure;
                    WB.Document.InvokeScript("UpdateId3", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion

        #region " Update Id4 "
        public void UpdateId4(string sConsigneMesure)
        {
            PassInvokeUpdateConsigneLoad = new DelegateUpdateConsigneLoad(InvokeUpdateId4);
            PassInvokeUpdateConsigneLoad(sConsigneMesure);
        }

        public void InvokeUpdateId4(string sConsigneMesure)
        {
            if (this.InvokeRequired)
                BeginInvoke(new System.Action(() => this.InternalInvokeUpdateId4(sConsigneMesure)));
            else
                InternalInvokeUpdateId4(sConsigneMesure);
        }

        private void InternalInvokeUpdateId4(string sConsigneMesure)
        {
            try
            {
                if (WB.Document != null)
                {
                    HtmlDocument doc = WB.Document;
                    Object[] objArray = new Object[1];
                    objArray[0] = (object)sConsigneMesure;
                    WB.Document.InvokeScript("UpdateId4", objArray);
                }
            }
            catch (Exception)
            { }
        }
        #endregion
    }
}
