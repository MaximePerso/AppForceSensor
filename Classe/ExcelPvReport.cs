using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using static K2000Rs232App.MainWindow;

namespace K2000Rs232App
{
    class ExcelPvReport
    {
        public Excel.Application xlAppForFD;
        public Excel.Workbook xlWorkBookForFD;
        public Excel.Worksheet xlWSAnalyse;
        public DelegateFinDeTraitement PassIndicationFinDeTraitement { get; set; }
        public RadioButton rdb = new RadioButton();

        #region " Habillage "
        public static void Habillage(
             int iStartLine,
             int iLastLine,
             int iStartCol,
             int ilastCol,
             Excel.XlPattern xlPt,
             Excel.XlColorIndex xlCi,
             Excel.XlThemeColor xlTc,
             double dTintAndShade,
             double dPatternTintAndShade,
             Excel.Worksheet xlWorkSheet)
        {
            xlWorkSheet.Range[xlWorkSheet.Cells[iStartLine, iStartCol], xlWorkSheet.Cells[iLastLine, ilastCol]].Interior.Pattern = xlPt;
            xlWorkSheet.Range[xlWorkSheet.Cells[iStartLine, iStartCol], xlWorkSheet.Cells[iLastLine, ilastCol]].Interior.PatternColorIndex = xlCi;
            xlWorkSheet.Range[xlWorkSheet.Cells[iStartLine, iStartCol], xlWorkSheet.Cells[iLastLine, ilastCol]].Interior.ThemeColor = xlTc;
            xlWorkSheet.Range[xlWorkSheet.Cells[iStartLine, iStartCol], xlWorkSheet.Cells[iLastLine, ilastCol]].Interior.TintAndShade = dTintAndShade;
            xlWorkSheet.Range[xlWorkSheet.Cells[iStartLine, iStartCol], xlWorkSheet.Cells[iLastLine, ilastCol]].Interior.PatternTintAndShade = dPatternTintAndShade;
            xlWorkSheet.Range[xlWorkSheet.Cells[iStartLine, iStartCol], xlWorkSheet.Cells[iLastLine, ilastCol]].Borders.Weight = Excel.XlBorderWeight.xlThin;
        }
        #endregion

        #region " Feuille résumé " 
        public void WriteAnalysysSheet(
            Excel.Worksheet xlWorkSheet,
            object[,] objTab,
            List<ClassSequence> ListOfId)
        {
            int iSizeOfWritingData = objTab.GetLength(0);

            xlWorkSheet.Activate();

            //**********************************************************************
            //Zone résumé
            //**********************************************************************
            xlWorkSheet.Range["A1"].Value = "Step";
            xlWorkSheet.Range["B1"].Value = "Mode";
            xlWorkSheet.Range["C1"].Value = "Position" + "\r" + "(mm)";
            xlWorkSheet.Range["D1"].Value = "Consigne Temp" + "\r" + "(°C)";
            xlWorkSheet.Range["E1"].Value = "Mesure Temp" + "\r" + "(°C)";
            xlWorkSheet.Range["F1"].Value = "Consigne Load" + "\r" + "(N)";
            xlWorkSheet.Range["G1"].Value = "Mesure Load" + "\r" + "(N)";
            xlWorkSheet.Range["H1"].Value = "Erreur Load" + "\r" + "(N)";
            xlWorkSheet.Range["I1"].Value = "Conversion Load" + "\r" + "(N)";
            xlWorkSheet.Range["J1"].Value = "Min" + "\r" + "(N)";
            xlWorkSheet.Range["K1"].Value = "Max" + "\r" + "(N)";
            xlWorkSheet.Range["L1"].Value = "Lll" + "\r" + "(N)";
            xlWorkSheet.Range["M1"].Value = "Upl" + "\r" + "(N)";

            try
            { xlWorkSheet.Range["N1"].Value = ListOfId.Where(x => x.Id == "1").Select(y => y.Designation).Single().ToString(); }
            catch (Exception)
            { xlWorkSheet.Range["N1"].Value = "NA"; }

            try
            { xlWorkSheet.Range["O1"].Value = ListOfId.Where(x => x.Id == "2").Select(y => y.Designation).Single().ToString(); }
            catch (Exception)
            { xlWorkSheet.Range["O1"].Value = "NA"; }

            try
            { xlWorkSheet.Range["P1"].Value = ListOfId.Where(x => x.Id == "3").Select(y => y.Designation).Single().ToString(); }
            catch (Exception)
            { xlWorkSheet.Range["P1"].Value = "NA"; }

            try
            { xlWorkSheet.Range["Q1"].Value = ListOfId.Where(x => x.Id == "4").Select(y => y.Designation).Single().ToString(); }
            catch (Exception)
            { xlWorkSheet.Range["Q1"].Value = "NA"; }

            xlWorkSheet.Range["A1:Q1"].Borders.Weight = Excel.XlBorderWeight.xlThin;
            //Bleu ciel
            Habillage(1, 1, 1, 17, Excel.XlPattern.xlPatternSolid, Excel.XlColorIndex.xlColorIndexAutomatic, Excel.XlThemeColor.xlThemeColorAccent5, 0.799981688894314, 0, xlWorkSheet);

            //On écris
            xlWorkSheet.Range["A2:Q" + (iSizeOfWritingData)].Value = objTab;

            xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[iSizeOfWritingData, objTab.GetLength(1)]].Borders.Weight = Excel.XlBorderWeight.xlThin;

            xlWorkSheet.Columns["A:BB"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlWorkSheet.Columns["A:BB"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            xlWorkSheet.Columns["A:BB"].WrapText = false;
            xlWorkSheet.Columns["A:BB"].Orientation = 0;
            xlWorkSheet.Columns["A:BB"].AddIndent = false;
            xlWorkSheet.Columns["A:BB"].IndentLevel = 0;
            xlWorkSheet.Columns["A:BB"].ShrinkToFit = false;
            xlWorkSheet.Columns["A:BB"].Font.Size = 11;
            xlWorkSheet.Columns["A:BB"].EntireColumn.AutoFit();

            //A la fin on place les graphiques
            //*****************************************************************************************************************************
            //Graphique de la courbe d'erreur
            //*****************************************************************************************************************************
            int iLastLineData = xlWorkSheet.Cells[xlWorkSheet.Rows.Count, "A"].End[Excel.XlDirection.xlUp].Row;

            string sRange = "S2:AH28";
            Excel.ChartObjects xlError = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);

            Excel.ChartObject coError = (Excel.ChartObject)xlError.Add(xlWorkSheet.Range[sRange].Left,
                xlWorkSheet.Range[sRange].Top,
                xlWorkSheet.Range[sRange].Width,
                xlWorkSheet.Range[sRange].Height);
            Excel.Chart chartPageError = coError.Chart;
            coError.Select();

            chartPageError.ChartType = Excel.XlChartType.xlXYScatterSmoothNoMarkers;
            chartPageError.Legend.Position = Excel.XlLegendPosition.xlLegendPositionTop;
            chartPageError.Refresh();
            Excel.SeriesCollection seriesCollectionError = chartPageError.SeriesCollection();

            string[] objRep = new string[(iLastLineData - 1) + 1];
            double[] objErrorMin = new double[(iLastLineData - 1) + 1];
            double[] objErrorlMax = new double[(iLastLineData - 1) + 1];

            Excel.Series seriesErrorMin = seriesCollectionError.NewSeries();
            seriesErrorMin.Name = xlWorkSheet.Range["L1"].Value;
            seriesErrorMin.XValues = xlWorkSheet.get_Range("F2", "F" + iLastLineData);
            seriesErrorMin.Values = xlWorkSheet.get_Range("L2", "L" + iLastLineData);

            Excel.Series seriesErrorMax = seriesCollectionError.NewSeries();
            seriesErrorMax.Name = xlWorkSheet.Range["M1"].Value;
            seriesErrorMax.XValues = xlWorkSheet.get_Range("F2", "F" + iLastLineData);
            seriesErrorMax.Values = xlWorkSheet.get_Range("M2", "M" + iLastLineData);

            Excel.Series seriesErrorSensor = seriesCollectionError.NewSeries();
            seriesErrorSensor.Name = xlWorkSheet.Range["H1"].Value;
            seriesErrorSensor.XValues = xlWorkSheet.get_Range("F2", "F" + iLastLineData);
            seriesErrorSensor.Values = xlWorkSheet.get_Range("H2", "H" + iLastLineData);;

            //*****************************************************************************************************************************
            //Graphique de la courbe d'erreur en continue
            //*****************************************************************************************************************************
            sRange = "S30:AH57";
            Excel.ChartObjects xlCycle = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);

            Excel.ChartObject coCycle = (Excel.ChartObject)xlCycle.Add(xlWorkSheet.Range[sRange].Left,
                xlWorkSheet.Range[sRange].Top,
                xlWorkSheet.Range[sRange].Width,
                xlWorkSheet.Range[sRange].Height);
            Excel.Chart chartPageCycle = coCycle.Chart;
            coCycle.Select();

            chartPageCycle.ChartType = Excel.XlChartType.xlXYScatterSmoothNoMarkers;

            chartPageCycle.Legend.Position = Excel.XlLegendPosition.xlLegendPositionTop;
            chartPageCycle.Refresh();
            Excel.SeriesCollection seriesCollectionCycle = chartPageCycle.SeriesCollection();

            Excel.Series seriesCycleJ1 = seriesCollectionCycle.NewSeries();
            seriesCycleJ1.Name = xlWorkSheet.Range["N1"].Value;
            seriesCycleJ1.XValues = xlWorkSheet.get_Range("F2", "F" + iLastLineData);
            seriesCycleJ1.Values = xlWorkSheet.get_Range("N2", "N" + iLastLineData);

            Excel.Series seriesCycleJ2= seriesCollectionCycle.NewSeries();
            seriesCycleJ2.Name = xlWorkSheet.Range["O1"].Value;
            seriesCycleJ2.XValues = xlWorkSheet.get_Range("F2", "F" + iLastLineData);
            seriesCycleJ2.Values = xlWorkSheet.get_Range("O2", "O" + iLastLineData); ;

            Excel.Series seriesCycleJ3 = seriesCollectionCycle.NewSeries();
            seriesCycleJ3.Name = xlWorkSheet.Range["P1"].Value;
            seriesCycleJ3.XValues = xlWorkSheet.get_Range("F2", "F" + iLastLineData);
            seriesCycleJ3.Values = xlWorkSheet.get_Range("P2", "P" + iLastLineData); ;

            Excel.Series seriesCycleJ4 = seriesCollectionCycle.NewSeries();
            seriesCycleJ4.Name = xlWorkSheet.Range["Q1"].Value;
            seriesCycleJ4.XValues = xlWorkSheet.get_Range("F2", "F" + iLastLineData);
            seriesCycleJ4.Values = xlWorkSheet.get_Range("Q2", "Q" + iLastLineData); ;

            //*****************************************************************************************************************************
            //Graphique de la courbe d'erreur en continue
            //*****************************************************************************************************************************
            sRange = "S60:AH87";
            Excel.ChartObjects xlCycleContinue = (Excel.ChartObjects)xlWorkSheet.ChartObjects(Type.Missing);

            Excel.ChartObject coCycleContinue = (Excel.ChartObject)xlError.Add(xlWorkSheet.Range[sRange].Left,
                xlWorkSheet.Range[sRange].Top,
                xlWorkSheet.Range[sRange].Width,
                xlWorkSheet.Range[sRange].Height);
            Excel.Chart chartPageCycleContinue = coCycleContinue.Chart;
            coCycleContinue.Select();

            chartPageCycleContinue.ChartType = Excel.XlChartType.xlXYScatterSmoothNoMarkers;
            chartPageCycleContinue.Legend.Position = Excel.XlLegendPosition.xlLegendPositionTop;
            chartPageCycleContinue.Refresh();
            Excel.SeriesCollection seriesCollectionCycleContinue = chartPageCycleContinue.SeriesCollection();

            Excel.Series seriesCycleContinueErroMin = seriesCollectionCycleContinue.NewSeries();
            seriesCycleContinueErroMin.Name = xlWorkSheet.Range["J1"].Value;
            seriesCycleContinueErroMin.XValues = xlWorkSheet.get_Range("A2", "A" + iLastLineData);
            seriesCycleContinueErroMin.Values = xlWorkSheet.get_Range("J2", "J" + iLastLineData);

            Excel.Series seriesCycleContinueErroMax = seriesCollectionCycleContinue.NewSeries();
            seriesCycleContinueErroMax.Name = xlWorkSheet.Range["K1"].Value;
            seriesCycleContinueErroMax.XValues = xlWorkSheet.get_Range("A2", "A" + iLastLineData);
            seriesCycleContinueErroMax.Values = xlWorkSheet.get_Range("K2", "K" + iLastLineData);

            Excel.Series seriesCycleContinueValue = seriesCollectionCycleContinue.NewSeries();
            seriesCycleContinueValue.Name = xlWorkSheet.Range["I1"].Value;
            seriesCycleContinueValue.XValues = xlWorkSheet.get_Range("A2", "A" + iLastLineData);
            seriesCycleContinueValue.Values = xlWorkSheet.get_Range("I2", "I" + iLastLineData);

            Final();
        }
        #endregion

        #region " Feuille Data "
        public void WriteValueSheet(Excel.Worksheet xlWorkSheet, object[,] objTab,List<ClassSequence> ListOfId,RadioButton rdb)
        {
            int iSizeOfWritingData = objTab.GetLength(0) + 1;
            xlWorkSheet.Activate();

            xlWorkSheet.Range["A1"].Value = "Date";
            xlWorkSheet.Range["B1"].Value = "Step";
            if(rdb.Checked)
            {
                xlWorkSheet.Range["C1"].Value = "Palier" + "\r" + "(s)";
                xlWorkSheet.Range["D1"].Value = "Rampe" + "\r" + "(Kn/s)";
            }
            else
            {
                xlWorkSheet.Range["C1"].Value = "Palier" + "\r" + "(min)";
                xlWorkSheet.Range["D1"].Value = "Rampe" + "\r" + "(°C/min)";
            }           
            xlWorkSheet.Range["E1"].Value = "Consigne temp" + "\r" + "(°C)";
            xlWorkSheet.Range["F1"].Value = "Mesure temp" + "\r" + "(°C)";
            xlWorkSheet.Range["G1"].Value = "Consigne load" + "\r" + "(N)";
            xlWorkSheet.Range["H1"].Value = "Mesure load" + "\r" + "(N)";
            xlWorkSheet.Range["I1"].Value = "Position" + "\r" + "(mm)";
            xlWorkSheet.Range["J1"].Value = "Lll" + "\r" + "(N)";
            xlWorkSheet.Range["K1"].Value = "Ull" + "\r" + "(N)";

            try
            {xlWorkSheet.Range["L1"].Value = ListOfId.Where(x => x.Id == "1").Select(y => y.Designation).Single().ToString();}
            catch (Exception)
            { xlWorkSheet.Range["L1"].Value = "NA"; }

            try
            { xlWorkSheet.Range["M1"].Value = ListOfId.Where(x => x.Id == "2").Select(y => y.Designation).Single().ToString(); }
            catch (Exception)
            { xlWorkSheet.Range["M1"].Value = "NA"; }

            try
            { xlWorkSheet.Range["N1"].Value = ListOfId.Where(x => x.Id == "3").Select(y => y.Designation).Single().ToString(); }
            catch (Exception)
            { xlWorkSheet.Range["N1"].Value = "NA"; }

            try
            { xlWorkSheet.Range["O1"].Value = ListOfId.Where(x => x.Id == "4").Select(y => y.Designation).Single().ToString(); }
            catch (Exception)
            { xlWorkSheet.Range["O1"].Value = "NA"; }

            xlWorkSheet.Rows[1].Font.Bold = true;
            xlWorkSheet.Range["A:BB"].EntireColumn.NumberFormat = "General";
            //On écris
            xlWorkSheet.Range[xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[iSizeOfWritingData, objTab.GetLength(1)]].Value = objTab;

            xlWorkSheet.Range[xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[iSizeOfWritingData, objTab.GetLength(1)]].Borders.Weight = Excel.XlBorderWeight.xlThin;

            xlWorkSheet.Columns["A:BB"].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            xlWorkSheet.Columns["A:BB"].VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            xlWorkSheet.Columns["A:BB"].WrapText = false;
            xlWorkSheet.Columns["A:BB"].Orientation = 0;
            xlWorkSheet.Columns["A:BB"].AddIndent = false;
            xlWorkSheet.Columns["A:BB"].IndentLevel = 0;
            xlWorkSheet.Columns["A:BB"].ShrinkToFit = false;
            xlWorkSheet.Columns["A:BB"].Font.Size = 11;
            xlWorkSheet.Columns["A:BB"].EntireColumn.AutoFit();
        }
        #endregion

        #region " Final "
        public void Final()
        {
            //Pass du delegate
            PassIndicationFinDeTraitement(true, xlAppForFD, xlWSAnalyse, xlWorkBookForFD);
        }
        #endregion
    }
}
