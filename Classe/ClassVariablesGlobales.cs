using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K2000Rs232App
{
    public class ClassVariablesGlobales
    {
        public static int iCountMeasure;
        public static bool bLaunchMeasurement = false;
        public static bool bLaunchAverageMeasurement = false;
        public int lStep=0;

        public static int iCountMeascureSetUp;
        public static bool bLaunchMeasurementSetUp = false;
        public int lStepSetUp = 0;

        public bool bDebug = false;
    }
}
