# Sujet du checkpoint 2

## Consignes

- Ce sujet est a réaliser individuellement. La date limite de rendue est fixée au vendredi 03/12/2021 à 17h30.

- Libre à vous de travailler en dehors des horaires prévus (je viendrais pas vérifier à 22h si vous êtes connectés), mais ça ne devrait pas être nécessaire pour arriver au bout.

- Cloner ce repo en local (pas forker)

- Créer une branche avec votre nom pour y déposer votre travail

---

- [Sujet du checkpoint 1](#sujet-du-checkpoint-1)
  - [Consignes](#consignes)
  - [Introduction](#introduction)
  - [Stack Technique](#stack-technique)
    - [C#.Net Classique](#cnet-classique)
    - [IIS](#iis)
    - [Nuget](#nuget)
    - [Visual Studio Community 2019](#visual-studio-community-2019)
    - [Entity Framework](#entity-framework)
    - [Database](#database)
  - [Rappels sur l'application](#rappels-sur-lapplication)
    - [Fonctionnalités générales](#fonctionnalités-générales)
    - [La notion d'Élève](#la-notion-délève)
    - [La notion de Cours](#la-notion-de-cours)
    - [La suppression d'un cours](#la-suppression-dun-cours)
    - [L'ajout d'une note et appréciation](#lajout-dune-note-et-appréciation)
    - [La moyenne](#la-moyenne)
    - [Design](#design)
    - [Objectifs bonus](#objectifs-bonus)

---

## Introduction

L'éducation nationale se modernise (encore), et après le succès retentissant de l'application console NotEdu, la commande d'une version web multi-poste est demandée !

**NotWebEdu** est dans la place !!

Bien entendu, l'application est attendue pour hier, vous êtes donc déjà en retard et il s'agit d'être réactif si on ne veut pas se faire remplacer par un concurrent.

Il faut reprendre les objectifs obligatoires du projet 1 (disponibles ci-dessous), en respéctant les mêmes règles métier.

La différence se fera sur :

- Le stockage des données (base de données SQL Server à la place d'un fichier JSON).
- L'interface utilisateur qui sera au format web (et non plus console).

---

## Stack Technique

### C#.Net Classique 
Le programme sera développé dans le langage C# en utilisant le framework .Net Classique

### IIS
Le site web sera hébergé par un serveur IIS

### Nuget
Les paquets Nuget pourront être utilisés pour l'inclusion de dépendances externes.

### Visual Studio Community 2019
Les développements et compilations seront réalisés sur l'outil Visual Studio 2019 Community

### Entity Framework
La connexion et la manipulation de la base de données sera faite avec Entity Framework en utilisant l'approche DB First.

### Database
L'approche DB First étant demandée, un (ou plusieurs) fichier(s) SQL devront permettre de créer la base de données (structure + potentielles données d'initialisation).

---

## Rappels sur l'application

### Fonctionnalités générales
- Elèves : 
  - Lister les élèves
  - Créer un nouvel élève
  - Consulter un élève existant
  - Ajouter une note et une appréciation pour un cours sur un élève existant
  - Revenir au menu principal

- Cours : 
  - Lister les cours existants
  - Ajouter un nouveau cours au programme
  - Supprimer un cours par son identifiant
  - Revenir au menu principal


### La notion d'Élève
Un élève sera composé des attributs suivants : 
- Un identifiant unique au format numérique
- Un nom au format texte
- Un prénom au format texte
- Une date de naissance
- Un liste de notes (nombre à virgule) et d'appréciation du professeur (texte) pour chaque cours
- La moyenne de ses notes qui sera calculée à la volée et ne sera pas enregistrée dans le fichier

### La notion de Cours
Les cours seront composés des attributs suivants : 
- Un identifiant unique au format numérique
- Un nom au format texte

### La suppression d'un cours
Quand un cours est supprimé du programme, il faut également supprimer les notes et appréciations de ce cours pour tous les élèves.

Une demande de confirmation devra être effectuée afin de s'assurer qu'il ne sagit pas d'une erreur.

### L'ajout d'une note et appréciation
L'appréciation du professeur est facultative. Seule la note est obligatoire.

### La moyenne
La moyenne est une information calculée à la volée. Il ne faut donc pas l'enregistrer dans la base de données.

La moyenne est arrondie à 1 chiffre après la virgule. Exemple : 

- 12 => 12/20
- 12.2 => 12/20
- 12.3 => 12.5/20
- 12.6 => 13/20

### Design

Aucune consigne à ce sujet, laissez-vous guider par votre imagination (***/!\\** mais ne perdez pas trop de temps **/!\\***) !

L'application n'aura pas besoin d'être responsive.

---

### Objectifs bonus

Dans le cas où toutes les demandes fonctionnelles seraient remplies (et, j'insiste, seulement dans ce cas là), on pourra aller plus loin en implémentant la notion de "Promotion" avec les impacts suivants :

- Chaque élève est rattaché à une promotion
- Une page permettant d'afficher la liste des promotions existantes
- Une page permettant d'afficher la liste des élèves dans une promotion (nom/prénom/etc... pas les notes)
- Une page permettant d'afficher la moyenne par cours de tous les élèves d'une promotion donnée
- Une page permettant d'afficher la moyenne de chaque promotion par cours
