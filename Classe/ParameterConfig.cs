using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K2000Rs232Dll
{
    /// <summary>
    /// Classe permettant de construire les commandes (au format string) à envoyer au K2000 pour configurer un mode de lecture.
    /// Le principe est d'utiliser un identifiant permettant de retrouver à la fois la commande de base et la valeur de paramètre.
    /// La construction s'effectue avec les deux dictionnaires sous la forme ":COMMANDE_DE_BASE VALEUR".
    /// </summary>
    public class ParameterConfig
    {
        /// <summary>
        /// Dictionnaire ParamètreId/Valeur
        /// </summary>
        private Dictionary<int, string> ParameterKeyValue = new Dictionary<int, string>()
        {
            {1, ""},
            {2, ""},
            {3, ""},
            {4, ""},
            {5, ""},
            {6, ""},
            {7, ""},
            {8, ""},
            {9, ""},
            {10, ""},
            {11, ""},
            {12, ""},
            {13, ""},
            {14, ""},
            {15, ""},
            {16, ""},
            {17, ""}
        };

        /// <summary>
        /// Dictionnaire ParamètreId/Commande de base (c'est-à-dire la commande spécifique à ce paramètre SANS la valeur.
        /// La valeur sera concatenée ensuite à l'aide du dictionnaire "ParameterKeyValue").
        /// </summary>
        private Dictionary<int, string> ParameterKeyCommand = new Dictionary<int, string>()
        {
            {1, ":NPLC "},
            {2, ":RANG:AUTO "},
            {3, ":RANG:UPP "},
            {4, ":THR:VOLT:RANG "},
            {5, ":REF:STAT "},
            {6, ":REF:"},
            {7, ":REF "},
            {8, ":DIG "},
            {9, ":AVER:STAT "},
            {10, ":AVER:TCON "},
            {11, ":AVER:COUNT "},
            {12, ":TC:TYPE "},
            {13, ":TC:RJUN "},
            {14, ":TC:RJUN:SIM "},
            {15, ":TC:RJUN:REAL:TCO "},
            {16, ":TC:RJUN:REAL:OFFSET "},
            {17, ":DET:BAND "}
        };

        /// <summary>
        /// Méthode permettant de renseigner le dictionnaire "ParameterKeyValue" avec les valeurs passées en paramètres.
        /// Cette méthode fait aussi appelle à d'autres méthodes servant à convertir des valeurs de type double en string pour pouvoir formater la commande à envoyer.
        /// </summary>
        /// <param name="ParameterList">Un dictionnaire dont la clé est de type int et la valeur de type double contenant les identifiant et les valeurs
        /// des paramètres à modifier.</param>
        /// <returns>0 si pas d'erreur, code d'erreur sinon.</returns>
        public int buildParameterList(Dictionary<int, double> ParameterList)
        {
            int result = -1;

            for (int i = 0; i < ParameterList.Count; i++)
            {
                if (ParameterList.ElementAt(i).Key == 2)
                {
                    result = this.SetAutoRange(ParameterList.ElementAt(i).Value);
                }
                else if (ParameterList.ElementAt(i).Key == 5)
                {
                    result = this.SetReferenceState(ParameterList.ElementAt(i).Value);
                }
                else if (ParameterList.ElementAt(i).Key == 6)
                {
                    result = this.SetInputSignalAsReference(ParameterList.ElementAt(i).Value);
                }
                else if (ParameterList.ElementAt(i).Key == 9)
                {
                    result = this.SetFilterState(ParameterList.ElementAt(i).Value);
                }
                else if (ParameterList.ElementAt(i).Key == 10)
                {
                    result = this.SetFilterType(ParameterList.ElementAt(i).Value);
                }
                else if (ParameterList.ElementAt(i).Key == 12)
                {
                    result = this.SetThermocoupleType(ParameterList.ElementAt(i).Value);
                }
                else if (ParameterList.ElementAt(i).Key == 13)
                {
                    result = this.SetReferenceJunctionType(ParameterList.ElementAt(i).Value);
                }
                else
                {
                    ParameterKeyValue[ParameterList.ElementAt(i).Key] = ParameterList.ElementAt(i).Value.ToString();
                    result = 0;
                }

                if (result !=0)
                {
                    return result;
                }
            }

            return result;
        }

        /// <summary>
        /// Renvoie le dictionnaire contenant les identifiants avec les commandes associées (sans les valeurs)
        /// </summary>
        /// <returns>Le dictionnaire contenant les identifiants avec les commandes associées.</returns>
        public Dictionary<int, string> GetParamenterKeyCommand()
        {
            return this.ParameterKeyCommand;
        }

        /// <summary>
        /// Méthode qui renvoit la commande au format string.
        /// </summary>
        /// <param name="ParameterKey">L'identifiant du paramètre dont on veut la commande.</param>
        /// <returns>La commande au format string.</returns>
        public string GetCommand(int ParameterKey)
        {
            return ParameterKeyCommand[ParameterKey].ToString() + ParameterKeyValue[ParameterKey].ToString();
        }

        /// <summary>
        /// Méthode convertissant le paramètre de type double en string suivant sa valeur.
        /// </summary>
        /// <param name="value">La valeur du paramètre.</param>
        /// <returns>0 si pas d'erreur, code d'erreur sinon.</returns>
        private int SetAutoRange(double value)
        {
            if (value == 1)
            {
                ParameterKeyValue[2] = "ON";
                return 0;
            }
            else if (value == 0)
            {
                ParameterKeyValue[2] = "OFF";
                return 0;
            }
            else
            {
                return -6;
            }
        }

        /// <summary>
        /// Méthode convertissant le paramètre de type double en string suivant sa valeur.
        /// </summary>
        /// <param name="value">La valeur du paramètre.</param>
        /// <returns>0 si pas d'erreur, code d'erreur sinon.</returns>
        private int SetReferenceState(double value)
        {
            if (value == 1)
            {
                ParameterKeyValue[5] = "ON";
                return 0;
            }
            else if (value == 0)
            {
                ParameterKeyValue[5] = "OFF";
                return 0;
            }
            else
            {
                return -6;
            }
        }

        /// <summary>
        /// Méthode convertissant le paramètre de type double en string suivant sa valeur.
        /// </summary>
        /// <param name="value">La valeur du paramètre.</param>
        /// <returns>0 si pas d'erreur, code d'erreur sinon.</returns>
        private int SetInputSignalAsReference(double value)
        {
            if (value == 1)
            {
                ParameterKeyValue[6] = "ACQ";
                return 0;
            }
            else if (value == 0)
            {
                return 0;
            }
            else
            {
                return -6;
            }
        }

        /// <summary>
        /// Méthode convertissant le paramètre de type double en string suivant sa valeur.
        /// </summary>
        /// <param name="value">La valeur du paramètre.</param>
        /// <returns>0 si pas d'erreur, code d'erreur sinon.</returns>
        private int SetFilterState(double value)
        {
            if (value == 1)
            {
                ParameterKeyValue[9] = "ON";
                return 0;
            }
            else if (value == 0)
            {
                ParameterKeyValue[9] = "OFF";
                return 0;
            }
            else
            {
                return -6;
            }
        }

        /// <summary>
        /// Méthode convertissant le paramètre de type double en string suivant sa valeur.
        /// </summary>
        /// <param name="value">La valeur du paramètre.</param>
        /// <returns>0 si pas d'erreur, code d'erreur sinon.</returns>
        private int SetFilterType(double value)
        {
            if (value == 1)
            {
                ParameterKeyValue[10] = "MOV";
                return 0;
            }
            else if (value == 2)
            {
                ParameterKeyValue[10] = "REP";
                return 0;
            }
            else
            {
                return -6;
            }
        }

        /// <summary>
        /// Méthode convertissant le paramètre de type double en string suivant sa valeur.
        /// </summary>
        /// <param name="value">La valeur du paramètre.</param>
        /// <returns>0 si pas d'erreur, code d'erreur sinon.</returns>
        private int SetThermocoupleType(double value)
        {
            if (value == 1)
            {
                ParameterKeyValue[12] = "J";
                return 0;
            }
            else if (value == 2)
            {
                ParameterKeyValue[12] = "K";
                return 0;
            }
            else if (value == 3)
            {
                ParameterKeyValue[12] = "T";
                return 0;
            }
            else
            {
                return -6;
            }
        }

        /// <summary>
        /// Méthode convertissant le paramètre de type double en string suivant sa valeur.
        /// </summary>
        /// <param name="value">La valeur du paramètre.</param>
        /// <returns>0 si pas d'erreur, code d'erreur sinon.</returns>
        private int SetReferenceJunctionType(double value)
        {
            if (value == 1)
            {
                ParameterKeyValue[13] = "SIM";
                return 0;
            }
            else if (value == 2)
            {
                ParameterKeyValue[13] = "REAL";
                return 0;
            }
            else
            {
                return -6;
            }
        }
    }
}

