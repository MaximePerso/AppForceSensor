using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Threading;
using static K2000Rs232App.MainWindow;

namespace K2000Rs232App
{
    public class ClassMesure
    {
        #region " Init "
        private DelegateOneShotMeasurementOuputDataTextBox PassOneShotMeasurementOuputDataTextBox { get; set; }
        private ClassRS232 NewclassK2000ForThreadMeasure = new ClassRS232();
        private List<ClassForThreadMeasure> ListForMeasure;
        private bool bDebug;
        #endregion

        #region " Constructeur "
        public ClassMesure(
            DelegateOneShotMeasurementOuputDataTextBox PassOneShotMeasurementOuputDataTextBoxFromExternal,
            ClassRS232 NewclassK2000ForThreadMeasureFromExtrnal,
            List<ClassForThreadMeasure> ListFromExternal, 
            bool bDebugFromClass)
        {
            PassOneShotMeasurementOuputDataTextBox = PassOneShotMeasurementOuputDataTextBoxFromExternal;
            NewclassK2000ForThreadMeasure = NewclassK2000ForThreadMeasureFromExtrnal;
            ListForMeasure = ListFromExternal;
            bDebug = bDebugFromClass;
        }

        public ClassMesure()
        {
        }
        #endregion

        #region " Mesure "
        public void Mesure(int iId , int lStepToTag)
        {
            string fOutputData = "";
            try
            {
                //POur le debug
                if (bDebug)
                {
                    Random rnd = new Random();
                    fOutputData = Convert.ToString(rnd.NextDouble()*iId);
                }
                else
                {
                    NewclassK2000ForThreadMeasure.WriteToPortCom(NewclassK2000ForThreadMeasure.diK2000Id[iId], ":FETC?");//La méthode fetch permet de relie la dernière lecture du buffer
                    NewclassK2000ForThreadMeasure.ReadPortCom(NewclassK2000ForThreadMeasure.diK2000Id[iId], out fOutputData);
                }

                //On stock brut la donnée pour éviter de perdre du temmps en ntraitement
                ListForMeasure.Add(new ClassForThreadMeasure { Id = iId, Measure = fOutputData, Step = lStepToTag });
                PassOneShotMeasurementOuputDataTextBox(iId, lStepToTag, fOutputData);
            }
            catch(Exception)
            { }                     
        }

        public void Mesure_TTI(int iId, int lStepToTag)
        {
            try
            {
                NewclassK2000ForThreadMeasure.SendCommand_TTI(iId, "V1?", out string sOutputData).ToString();

                //On stock brut la donnée pour éviter de perdre du temmps en ntraitement
                ListForMeasure.Add(new ClassForThreadMeasure { Id = iId, Measure = sOutputData, Step = lStepToTag });
            }
            catch (Exception)
            { }
        }

        public void EndOfCycle()
        {
            //On passe la liste
        }
        #endregion
    }
}
