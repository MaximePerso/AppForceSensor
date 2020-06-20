/*
 * HotmailSenso.css v1.0.0
 * Fonction style développée pour l'outil "Hotmail"
 * Hotmail, developpement pour communication interne
 *
 * Concepteur & id : AM
 * Devp : DW
 */

	//************************************************************************************************************
	//************************************************************************************************************
	 //Global
	//************************************************************************************************************
	//************************************************************************************************************

	//**************************************************************
	//Fonction pour le slide du drop menu
	//**************************************************************
	$(document).ready(function(){
		$(".dropdown").hover(
			function() {
				$('.dropdown-menu', this).not('.in .dropdown-menu').stop(true,true).slideDown("20");
				$(this).toggleClass('open');
			},
			function() {
				$('.dropdown-menu', this).not('.in .dropdown-menu').stop(true,true).slideUp("5");
				$(this).toggleClass('open');
			});
		});

	//**************************************************************
	//Fonction de reset des checkbox à l'ouverture de la modal
	//**************************************************************
	function UncheckAll()
	{
		var vPassCheckToFalse = document.all.checkbox;

		for (var i = 0; i < vPassCheckToFalse.length; i++)
		{vPassCheckToFalse[i].checked=false;}

		document.getElementById("MySuccessAlert").style.display = "none";
		document.getElementById("MyWarningAlert").style.display = "none";
	}

	//**************************************************************
	//Traitement des checkbox et copy
	//**************************************************************
	function LocalCopy()
	{
		var vCurrentPath = window.document.URL;
		var vLenghtSource =  vCurrentPath.length;
		var vIndexOfSource = vCurrentPath.indexOf("//") + 2;
		var vLastIndexOfSource = vCurrentPath.lastIndexOf("\\");
		var vNameOfFileSource = vCurrentPath.substring(vIndexOfSource,vLastIndexOfSource);

		var vTabOfPath = ["\\pdf\\OTD\\OTD.pdf",
		"\\pdf\\RR\\Archamps\\RR_Site.pdf",
		"\\pdf\\RR\\LVDT\\RR_LVDT.pdf",
		"\\pdf\\RR\\ME\\RR_ME.pdf",
		"\\pdf\\RR\\Inertiel\\RR_Inertiel.pdf"];

		var vPassCheckToFalse = document.all.checkbox;

		document.getElementById("MySuccessAlert").style.display = "none";
		document.getElementById("MyWarningAlert").style.display = "block";
		document.getElementById("MyWarningAlert").innerHTML = "\n\t\t\t\t\t\t\t<a class=\"close\" onclick=\"DisplayNoneAlert()\" href=\"#\">×</a>\n\t\t\t\t\t\t\t<strong>Information! </strong>Lors de la copie il est possible qu'une fenêtre d'avertissement Internet Explorer, s'ouvre, notez que cet demande d'ActiveX provient de Hotmail et qu'il permet la copie de fichier.\n\t\t\t\t";
		//setTimeout(function(){ document.getElementById("MyWarningAlert").style.display = "none"; }, 5000);

		//Recherche du chemin local pour Mes documents
		try
		{
			var wShell   = new ActiveXObject("WScript.Shell");
			var vMyDocPath= wShell.ExpandEnvironmentStrings("%userprofile%\\Documents");
		}
		catch(err)
		{
			document.getElementById("MySuccessAlert").style.display = "none";
			document.getElementById("MyWarningAlert").style.display = "block";
			document.getElementById("MyWarningAlert").innerHTML = "\n\t\t\t\t\t\t\t<a class=\"close\" onclick=\"DisplayNoneAlert()\" href=\"#\">×</a>\n\t\t\t\t\t\t\t<strong>Avertissement! </strong>Votre navigateur, ne permet pas l'utilisation d'objet ActiveX, utilisez IE, ou bien consultez l'administrateur du lien, pour qu'il vous envoie les fichiers en pdf.\n\t\t\t\t";
			/*
			var FileDialog = document.createElement('input');
			FileDialog.setAttribute('type', 'file');
			FileDialog.setAttribute('multiple', 'multiple');
			FileDialog.click();
			*/
			return;
		}

		if (vMyDocPath)
		{
			var vErrCounter = 0;
			for (var i = 0; i < vPassCheckToFalse.length; i++)
			{
				var vSourcePath = vNameOfFileSource + vTabOfPath[i] ;

				var vLenghtDest =  vTabOfPath[i].length;
				var vIndexOfDest = vTabOfPath[i].lastIndexOf("\\");
				var vNameOfFileDest = vTabOfPath[i].substring(vIndexOfDest,vLenghtDest);
				var vDestinationPath = vMyDocPath + vNameOfFileDest;

				try
				{
					if (vPassCheckToFalse[i].checked==true)
					{
						var vFso;
						vFso = new ActiveXObject("Scripting.FileSystemObject");
						vFso.CopyFile (vSourcePath, vDestinationPath);
					}
				}
				catch(err)
				{
					vErrCounter +=1;
				}
			}
			if (vErrCounter==0)
			{
				document.getElementById("MySuccessAlert").style.display = "block";
				document.getElementById("MyWarningAlert").style.display = "none";
				//setTimeout(function(){ document.getElementById("MySuccessAlert").style.display = "none"; }, 5000);
				$(".alert-success").fadeOut(4000);
			}
			else
			{
				document.getElementById("MySuccessAlert").style.display = "none";
				document.getElementById("MyWarningAlert").style.display = "block";
				document.getElementById("MyWarningAlert").innerHTML = "\n\t\t\t\t\t\t\t<a class=\"close\" onclick=\"DisplayNoneAlert()\" href=\"#\">×</a>\n\t\t\t\t\t\t\t<strong>Avertissement!</strong>Un problème est survenu sur les fichiers sources." + " Consultez l'administrateur du lien, pour qu'il vous envoie les fichiers en pdf.\n\t\t\t\t";
			}
		}
		else
		{
			document.getElementById("MySuccessAlert").style.display = "none";
			document.getElementById("MyWarningAlert").style.display = "block";
			document.getElementById("MyWarningAlert").innerHTML = "\n\t\t\t\t\t\t\t<a class=\"close\" onclick=\"DisplayNoneAlert()\" href=\"#\">×</a>\n\t\t\t\t\t\t\t<strong>Avertissement!</strong> La localisation du dossier Documents n'a pas pu être faite." + " Consultez l'administrateur du lien, pour qu'il vous envoie les fichiers en pdf.\n\t\t\t\t";
		}
	}

	//**************************************************************
	//Remise à zéro de l'affichage des alertes
	//**************************************************************
	function DisplayNoneAlert()
	{
		document.getElementById("MySuccessAlert").style.display = "none";
		document.getElementById("MyWarningAlert").style.display = "none";
	}

	//************************************************************************************************************
	//************************************************************************************************************
	 //Quality.html
	//************************************************************************************************************
	//************************************************************************************************************

 	//**************************************************************
	//Chargement des images pour le carrousel
	//**************************************************************
	function RR_Archamps()
	{
		var dir = document.location.pathname;
		var pos = dir.lastIndexOf('/');
		var len = dir.length;

		$(".dropdown-menu").fadeOut("slow");

		ManageChevron();

		dir = dir.substring(0, pos);
		document.getElementById("ImageSlide1").src= "images/RR/Archamps/1.jpg";
		document.getElementById("ImageSlide2").src= "images/RR/Archamps/2.jpg";
		document.getElementById("ImageSlide3").src= "images/RR/Archamps/3.jpg";
	}

	function RR_LVDT()
	{
		var dir = document.location.pathname;
		var pos = dir.lastIndexOf('/');
		var len = dir.length;

		$(".dropdown-menu").fadeOut("slow");

		ManageChevron();

		dir = dir.substring(0, pos);
		document.getElementById("ImageSlide1").src= "images/RR/LVDT/1.jpg";
		document.getElementById("ImageSlide2").src= "images/RR/LVDT/2.jpg";
		document.getElementById("ImageSlide3").src= "images/RR/LVDT/3.jpg";
	}

	function RR_ME()
	{
		var dir = document.location.pathname;
		var pos = dir.lastIndexOf('/');
		var len = dir.length;

		$(".dropdown-menu").fadeOut("slow");

		ManageChevron();

		dir = dir.substring(0, pos);
		document.getElementById("ImageSlide1").src= "images/RR/ME/1.jpg";
		document.getElementById("ImageSlide2").src= "images/RR/ME/2.jpg";
		document.getElementById("ImageSlide3").src= "images/RR/ME/3.jpg";
	}

	function RR_Inertiel()
	{
		var dir = document.location.pathname;
		var pos = dir.lastIndexOf('/');
		var len = dir.length;

		$(".dropdown-menu").fadeOut("slow");

		ManageChevron();

		dir = dir.substring(0, pos);
		document.getElementById("ImageSlide1").src= "images/RR/Inertiel/1.jpg";
		document.getElementById("ImageSlide2").src= "images/RR/Inertiel/2.jpg";
		document.getElementById("ImageSlide3").src= "";
	}







	//************************************************************************************************************
	//************************************************************************************************************
	 //Ventes.html
	//************************************************************************************************************
	//************************************************************************************************************

	//**************************************************************
	//Gestion du support Powerpoint
	//**************************************************************
	function Bilan_Libre()
	{
		var dir = document.location.pathname;
		var pos = dir.lastIndexOf('/');
		var len = dir.length;

		$(".dropdown-menu").fadeOut("slow");

		dir = dir.substring(0, pos);
		document.getElementById("ImagesVentes1").style.display= "block";
		document.getElementById("ImagesVentes1").src= "images/ventes/1.jpg";

	}