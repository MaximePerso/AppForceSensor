using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace K2000Rs232App
{
    public class ClassRS232
    {
        #region " init "
        //Constante
        public int RS232_WRITE_TIMEOUT = 1000; //1s
        public int RS232_READ_TIMEOUT = 1000; //1s

        //List
        private readonly List<int> lParity = new List<int>() { { 0 }, { 1 }, { 2 } };
        private readonly List<int> lStopBits = new List<int>() { { 1 }, { 2 } };
        private readonly List<int> lDataBits = new List<int>() { { 5 }, { 6 }, { 7 }, { 8 } };
        private readonly List<int> lHandshake = new List<int>() { { 0 }, { 1 }, { 2 }, { 3 } };
        private readonly List<int> lBaudRate = new List<int>() { { 110 }, { 300 }, { 600 }, { 1200 }, { 2400 }, { 4800 }, { 9600 }, { 14400 }, { 19200 }, { 38400 }, { 56000 }, { 57600 }, { 115200 } };
        private readonly List<string> lTerminaison = new List<string>() { { "CR" }, { "LF" }, { "CRLF" } };
        public List<int> GetBaudRateList(){return lBaudRate;}
        public List<int> GetDataBitsList(){return lDataBits;}
        public List<int> GetParityList(){return lParity;}
        public List<int> GetStopBitsList(){return lStopBits;}
        public List<int> GetHandshakeList() { return lHandshake; }
        public List<string> GetTerminaisonList() { return lTerminaison; }
        public string GetDLLErrorMessage(int iErrorId){return this.diErrorId[iErrorId];}

        //Dictionnaire
        private Dictionary<int, SerialPort> diOpenedSerialPort = new Dictionary<int, SerialPort>();
        public Dictionary<int, SerialPort> diK2000Id = new Dictionary<int, SerialPort>();
        public Dictionary<int, SerialPort> GetdiOpenedSerialPort() { return diOpenedSerialPort; }
        public Dictionary<int, string> GetMeasureId() { return this.diMeasureId; }
        public Dictionary<int, string> diErrorId = new Dictionary<int, string>()
        {
            {-18, "La terminaison n'a pas été retrouvée."},
            {-17, "K2000 non initialisé."},
            {-16, "Commande envoyée vide."},
            {-15, "Problème de lecture sur le port RS232."},
            {-14, "Problème de timeout de lecture."},
            {-13, "Problème d'écriture sur le port RS232."},
            {-12, "Problème de timeout d'écriture."},
            {-11, "Problème dans la méthode ReadXtimesAverage()."},
            {-10, "Problème dans la méthode ReadXtimes()."},
            {9, "K2000 ou port COM inexistant ou non initialisé."},
            {-8, "Impossible d'effectuer un reset du K2000.\nVeuillez vérifier que le numéro de K2000 existe et qu'il est attribué à un port COM."},
            {-7, "Erreur lors de l'initialisation d'une connexion."},
            {-6, "Valeur de paramètre non valide pour l'un des paramètres configurés avec la méthode Config…()."},
            {-5, "Erreur inconnue lors de l'envoie d'une commande (méthode SendCommand)."},
            {-4, "Mauvais port COM ou port COM fermé (lors de l'envoi d'une commande)."},
            {-3, "Au moins un des paramètres renseignés pour l'initialisation contient une valeur non valide."},
            {-2, "L'identifiant diMeasureId n'existe pas dans la liste."},
            {-1, "Message d'erreur non défini."},
            {0, "No error"},
            {1, "Le K2000 n'est pas prêt.\nVérifiez s'il est sous tension, branché à un port COM et en état de fonctionnement."},
            {2, "Port COM déjà ouvert, veuillez le fermer ou en utiliser un autre."},
            {3, "K2000 déjà initialisé."},
            {4, "Le port COM demandé n'existe pas."},
            {5, "Le port COM demandé n'existe pas ou n'est pas ouvert."},
            {6, "La version de la Dll n’est pas affichée correctement. "}
        };

        private readonly Dictionary<int, string> diMeasureId = new Dictionary<int, string>()
        {
            {1, ":CURR:AC"},   // courant AC
            {2, ":CURR:DC"},   // courant DC
            {3, ":VOLT:AC"},   // tension AC
            {4, ":VOLT:DC"},   // tension DC
            {5, ":RES"},       // résistance 2 fils
            {6, ":FRES"},      // résistance 4 fils
            {7, ":TEMP"},      // température
            {8, ":FREQ"},      // fréquence
            {9, ":PER"}        // période
            
        };
        #endregion

        #region " Init du port COM "
        public int InitRS232(int Id,int iPortComNumber, int iBaudRate, int iDataBits, int iParity, int iStopBits, string sTerminaison, int iHandshake)
        {
            SerialPort spComPort = new SerialPort();

            // Vérifie si le K2000 est déjà attribué à un port.
            if (diK2000Id.ContainsKey(Id))
            {
                return -1;
            }
            else
            {
                if (!SerialPort.GetPortNames().Contains("COM" + iPortComNumber))
                {
                    return 4;
                }
                else if (diOpenedSerialPort.ContainsKey(iPortComNumber))
                {
                    return 2;
                }
                else if (!lBaudRate.Contains(iBaudRate) || !lDataBits.Contains(iDataBits) || !lParity.Contains(iParity) || !lStopBits.Contains(iStopBits) || !lHandshake.Contains(iHandshake))
                {
                    return -3;
                }
                else
                {
                    /* on configure les paramètres */
                    spComPort.PortName = "COM" + iPortComNumber;  // formatage de la propriété "PortName" de l'objet "CompPort" avec le numéro de port choisi pour pouvoir l'utiliser.
                    spComPort.BaudRate = iBaudRate;
                    spComPort.DataBits = iDataBits;
                    spComPort.Parity = (Parity)iParity;
                    spComPort.StopBits = (StopBits)iStopBits;
                    spComPort.Handshake = (Handshake)iHandshake;
                    spComPort.NewLine = sTerminaison;
                    spComPort.ReadBufferSize = Convert.ToInt32(Math.Pow(2,16)); // Par défaut cette valeur est de 4096
                    spComPort.WriteBufferSize = Convert.ToInt32(Math.Pow(2, 16)); // Par défaut cette valeur est de 2048

                    spComPort.ReadTimeout = RS232_READ_TIMEOUT;
                    spComPort.WriteTimeout = RS232_WRITE_TIMEOUT;

                    spComPort.Open();

                    GetdiOpenedSerialPort().Add(iPortComNumber, spComPort); // ajoute le port série initialisé et ouvert au dictionnaire de ports série disponibles
                    diK2000Id.Add(Id, spComPort); // association n° K2000 -> PortCOM

                    if (Id != 10)
                    { return ClearAndResetK2000(Id); }
                    else
                    { return 0; }
                }
            }
        }
        #endregion

        #region " Close PortCom "
        public int ClosePortComById(int Id)
        {
            int iResult = 0;
            int iPosrtCom;

            try
            {
                iPosrtCom = Convert.ToInt32(diK2000Id[Id].PortName.Last().ToString());
                GetdiOpenedSerialPort()[iPosrtCom].Close();

                //On le supprime des différents dictionnaire
                GetdiOpenedSerialPort().Remove(iPosrtCom);
                diK2000Id.Remove(Id);

                return iResult;
            }
            catch (Exception) { return -1; }
        }
        #endregion

        #region " Clear and Reset des K2000 "
        public int ClearAndResetK2000(int Id)
        {
            int iResult = 0;

            iResult = SendCommand(Id, ":STAT:QUE:CLE", out string sOutputData); // Clear Buffer
            if (iResult != 0) return iResult;

            iResult = SendCommand(Id, "*rst\r\n", out sOutputData); // reset (paramètres par défaut du K2000)
            if (iResult != 0) return iResult;

            return iResult;
        }
        #endregion

        #region " Clear Buffer des K2000 "
        public int ClearBuffer(int Id)
        {
            int iResult = 0;

            iResult = SendCommand(Id, ":STAT:QUE:CLE", out string sOutputData); // Clear Buffer
            if (iResult != 0) return iResult;

            return iResult;
        }
        #endregion

        #region " Reset Buffer des K2000 "
        public int Reset(int Id)
        {
            int iResult = 0;

            iResult = SendCommand(Id, "*rst\r\n", out string sOutputData); // reset (paramètres par défaut du K2000)
            if (iResult != 0) return iResult;

            return iResult;
        }
        #endregion

        #region " ConfigCurrentAC "
        public int ConfigCurrentAC(int Id)
        {
            int iResult = -1;

            iResult = SendCommand(Id, ":CONF" + this.GetMeasureId()[1], out string sOutputData);

            return iResult;
        }
        #endregion

        #region " ConfigCurrentDC "
        public int ConfigCurrentDC(int Id)
        {
            int iResult = -1;

            iResult = this.SendCommand(Id, ":CONF" + this.GetMeasureId()[2], out string sOutputData);

            return iResult;
        }
        #endregion

        #region " ConfigVoltageAC "
        public int ConfigVoltageAC(int Id)
        {
            int iResult = -1;

            iResult = this.SendCommand(Id, ":CONF" + this.GetMeasureId()[3], out string sOutputData);

            return iResult;
        }
        #endregion

        #region " ConfigVoltageDC "
        public int ConfigVoltageDC(int Id)
        {
            int iResult = -1;

            iResult = this.SendCommand(Id, ":CONF" + this.GetMeasureId()[4], out string sOutputData);

            return iResult;
        }
        #endregion

        #region " ConfigResistance2wire " 
        public int ConfigResistance2wire(int Id)
        {
            int iResult = -1;

            iResult = this.SendCommand(Id, ":CONF" + this.GetMeasureId()[5], out string sOutputData);

            return iResult;
        }
        #endregion

        #region " ConfigResistance4wire "
        public int ConfigResistance4wire(int Id)
        {
            int iResult = -1;

            iResult = this.SendCommand(Id, ":CONF" + this.GetMeasureId()[6], out string sOutputData);

            return iResult;
        }
        #endregion

        #region " ConfigTemperature "
        public int ConfigTemperature(int Id)
        {
            int iResult = -1;

            iResult = this.SendCommand(Id, ":CONF" + this.GetMeasureId()[7], out string sOutputData);

            return iResult;
        }
        #endregion

        #region " ConfigFrequency "
        public int ConfigFrequency(int iK2000Number)
        {
            int iResult = -1;

            iResult = this.SendCommand(iK2000Number, ":CONF" + this.GetMeasureId()[8], out string sOutputData);

            return iResult;
        }
        #endregion

        #region " ConfigPeriod "
        public int ConfigPeriod(int iK2000Number)
        {
            int iResult = -1;

            iResult = this.SendCommand(iK2000Number, ":CONF" + this.GetMeasureId()[9], out string sOutputData);

            return iResult;
        }
        #endregion

        #region " Send Command "
        public int SendCommand(int Id, string sCommand, out string sFeedBackData)
        {
            int iResult = 0;
            sFeedBackData = "";

            try
            {
                iResult = WriteToPortCom(diK2000Id[Id], sCommand);

                if (sCommand.Last().Equals('?')) // vérifie si on s'attend à un retour
                {
                    iResult = ReadPortCom(diK2000Id[Id], out sFeedBackData);
                }

                return iResult;
            }
            catch(Exception)
            { return -1; }
        }
        #endregion

        #region " Read from port Com "
        public int ReadPortCom(SerialPort SpPortCom,out string sDataReturned)
        {
            int iResult=0;
            sDataReturned = "";
            string sOutputData = "";

            try
            {
                sOutputData = SpPortCom.ReadLine();
                sDataReturned = double.Parse(sOutputData.Replace("\u0013", "").Replace("\u0011", "").ToString(),CultureInfo.InvariantCulture).ToString();
                return iResult;
            }
            catch (TimeoutException) { return -1; }
            catch (IndexOutOfRangeException) { return -2; }
            catch (ArgumentOutOfRangeException) { return -3; }
            catch (OverflowException) { return -4; }
            catch (FormatException){ return -5; }
        }
        #endregion

        #region " Send Command for alim TTI "
        public int SendCommand_TTI(int Id, string sCommand, out string sFeedBackData)
        {
            int iResult = 0;
            sFeedBackData = "";

            try
            {
                iResult = WriteToPortCom(diK2000Id[Id], sCommand);

                if (sCommand.Last().Equals('?')) // vérifie si on s'attend à un retour
                {
                    iResult = ReadPortCom_TTI(diK2000Id[Id], out sFeedBackData);
                }

                return iResult;
            }
            catch (Exception)
            { return -1; }
        }
        #endregion

        #region " Read from port Com for alim TTI "
        public int ReadPortCom_TTI(SerialPort SpPortCom, out string sDataReturned)
        {
            int iResult = 0;
            sDataReturned = "";
            string sOutputData = "";

            try
            {
                sOutputData = SpPortCom.ReadLine();
                var vReturnChain = sOutputData.Split(' ');
                sDataReturned = double.Parse(vReturnChain[1].Replace("\r", "").ToString(), CultureInfo.InvariantCulture).ToString();
                return iResult;
            }
            catch (TimeoutException) { return -1; }
            catch (IndexOutOfRangeException) { return -2; }
            catch (ArgumentOutOfRangeException) { return -3; }
            catch (OverflowException) { return -4; }
            catch (FormatException) { return -5; }
        }
        #endregion

        #region " Write to port Com "
        public int WriteToPortCom(SerialPort SpPortCom,string sCommand)
        {
            int iResult = 0;

            try
            {
                SpPortCom.WriteLine(sCommand);
                return iResult;
            }
            catch (TimeoutException) { return -1; }
            catch (IndexOutOfRangeException) { return -2; }
            catch (ArgumentOutOfRangeException) { return -3; }
            catch (OverflowException) { return -4; }
        }
        #endregion
    }
}
