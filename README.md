# CharpinEtienneProjetCsharp

Présentation du TP de deuxieme année de F2 devellopement logiciel de C#
- Projet individuel à rendre avant le 30 janvier 2023
- Vous pourrez retrouver le sujet dans le fichier sujetTpnote

## Présentation des fonctionnalitées demandées dans le sujet ainsi que le principe de leur implémentation (Commentaire à retrouver dans le code)


###Socle Applicatif 

#Construire un modèle de données pour gérer des contacts et les ranger dans une structure hiérarchique de dossiers

- Class dossier contenant le nom, la date de creation et la date de modification de ce meme dossier ainsi qu'une liste de dossier et de contact

- Class Contact contenant le nom, le prenom, le courriel, la societe, le lien, la date de creation et la date de modification de ce meme dossier

#Construire une application console pour gérer les contacts

Afficher toute la structure :

- Affichage recursives des structures de tout l'explorateur de fichiers

Gérer les dossiers : création dans un dossier parent : 

- Creation d'une fonction d'ajout dans la liste de dossier dans un premier temps dans le dossier dernierment utilisé

Gérer les contacts : création dans un dossier parent :

- Création d'une fonction d'ajout de contact dans la liste de contact d'un dossier dernierement utilisé

#Pour aller plus loin

gérer le format d’adresse Email saisi :

- Creation d'une fonction de vérification d'addresse email valide grace a "System.Net.Mail"

De base l’application enregistre dossier et contact dans le dernier dossier créé. Le dossier créé en dernier sera le parent et le current. Faites en sorte de pouvoir sélectionner le current :

- Possibilite de passer le dossier courant en paramettre de la création de dossier ou 0 est la racine

###Serialisation


#Implémenter deux versions différentes de gestion de fichier par sérialisation avec le design pattern Factory

- Creation d'une serialisationFactory appelant la création de différents serialiseur en fonction du choix utilisateur


Sérialisation binaire avec BinaryFormatter : 

- Voir Binserialisation.cs


Sérialisation XML avec XmlSerializer

- Voir XMLserialisation.cs


#Se confronter à une erreur de chargement de fichier inexistant et la gérer explicitement dans l’application en affichant un message, sans laisser l’application s’auto-détruire :


- Gestion d'une exception de file not found lors de la serialisation avec un message d'erreur permettant de gerer l'exception

#Pour aller plus loin

utiliser l’identité Windows courante pour déterminer le nom du fichier 
fixer l’emplacement du fichier dans le dossier «Mes Documents» de l’utilisateur courant :


- Récupération du Username courant et ajout dans "C:\Users\Documents\ContactManager" + Environment.UserName + ".db"


Protéger l’accéspar un mot de passe : Si un fichier de DB existe, au chargement de l’application, demandez le mot de passe, si le mot de passe saisi est erroné à la 3èmetentative, supprimez la base :

- Protection du fichier .bd par un mot de passe stocké en local, si l'utilisateur se trompe 3 fois on delete le fichier


###Cryptage

#Protéger l’accès au fichier de contacts par cryptage réversible avec la classe CryptoStream :

- Utilisation d'un flux chiffré lors de la serialisation binaire et xml


#Permettre à l’utilisateur de saisir une clé de cryptage au moment de charger ou d’enregistrer un fichier :

- Choix donnée à l'utilisateur de donner une clé de 8 caractere pour chiffrer et dechiffrer son dossier


#Gérer les erreurs pour ne pas laisser l’application s’auto-détruiresi l’opération de décryptage échoue :

- Gestion de l'erreur de dechiffrage et deserialisation en affichant un message d'erreur et en supprimant le fichier

#Pour aller plus loin

si aucune clé n’est précisée, utiliser l’identifiant interne (SID) de l’identité Windows courante :

- Récupération du SID de l'utilisateur courant caster sur une chaine de 8 caractere pour permettre de setup la cle par defaut




































































