# Todo

**[Version française](#scénario)**

## Scenario
Welcome to the TODO application, where users are able to create lists, rename them, add items to those lists, and mark each item as completed. 

Recently users have reported that they have been unable to mark items as completed. A new test has been introduced that demonstrates the issue (see [ListsControllerTests.cs - Line 153](./todo.api.test/ListsControllerTests.cs)).

A significant amount of feedback has been received requesting the ability to delete todo lists, and to be able to remove items from those lists. A couple of tests have also been introduced in [ListControllerTests.cs](./todo.web.test/ListControllerTests.cs) to help implement the new features. Unlike the case for deleting lists, the UI has not been updated to accomodate deleting individual todo items.

**Running `dotnet test` will reveal three failing tests. Each needs to be resolved without altering the contents of the existing unit test. The [list razor page](./todo.web/Views/List/List.cshtml) should also be updated to allow deleting individual todo items.**

The exam is independent, but intended to be completed in a short amount of time. Try to make minimal changes to the existing code to pass the failing tests. Be sure to consider the following while making your changes:

* You are not expected to extensively refactor any element of the code, however opportunities for improvement should be noted.
* The unit tests should remain as-written. If you feel a test could be improved upon, make note of it as something to be discussed.
* When todo.api is started, a SQLite database is created at ./AppData/todo.db. When running tests, the database is created in memory.
* Test data is being generated in the `todo.data.TodoContext.Seed()` for both the automated testing and when the application is running. Each test is isolated and generates a fresh set of data.

## Getting Started
1. (Optionally) [Download visual studio code (VSCode)](https://code.visualstudio.com/download)
1. [Download .NET 6 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
1. Clone this project, or [download the source](https://github.com/tc-ca/todo/archive/refs/heads/main.zip) and extract its contents
1. (Optionally) Open VSCode and open the top-level folder (file -> open folder)
1. In a terminal at the root of the project, run the following commands (note that the `dotnet run` commands must be executed in separate terminal instances)

``` PowerShell
dotnet dev-certs https --trust
dotnet run --project todo.api/todo.api.csproj
dotnet run --project todo.web/todo.web.csproj
```

1. View todo.api swagger documentation at https://localhost:7180/swagger
1. Browse the web application at https://localhost:7172/

## Running xUnit tests
* Follow the steps in [Getting Started](#getting-started)
* In the integrated terminal, run `dotnet test`

## Submitting Your Exam
You may fork the repository on github, or submit the modified source files as an email attachment to &lt;contact info here&gt;.

**Important:** You must send an email to the identified contact prior to the deadline indicated in your invitation, the email must contain your submission or a link to your github repository. The date/time on the email or github file timestamps will be used to ensure the files were submitted before the deadline.

## Scénario
Bienvenue dans l’application TODO, où les utilisateurs peuvent créer des listes, les renommer, ajouter des éléments à ces listes et marquer chaque élément comme terminé. 

Récemment, les utilisateurs ont signalé qu’ils n’ont pas été en mesure de marquer les éléments comme terminés. Un nouveau test a été introduit qui démontre le problème (voir [ListsControllerTests.cs - Line 153](./todo.api.test/ListsControllerTests.cs)).

Une quantité importante de commentaires ont été reçu demandant la possibilité de supprimer des listes todo et de pouvoir supprimer des éléments de ces listes. Quelques tests ont également été introduits dans [ListControllerTests.cs](./todo.web.test/ListControllerTests.cs) pour aider à mettre en œuvre les nouvelles fonctionnalités. Contrairement au cas de la suppression de listes, l’interface utilisateur n’a pas été mise à jour pour permettre la suppression d’éléments de todo individuels.

**L’exécution de la commande `dotnet test` révélera trois tests avec des échecs. Chacun doit être résolu sans modifier le contenu du test existant. La [page de la liste des razor](./todo.web/Views/List/List.cshtml) doit également être mise à jour pour permettre la suppression d’éléments de todo individuels.**

L’examen est indépendant, mais destiné à être complété dans un court laps de temps. Essayez d’apporter un minimum de modifications au code existant pour réussir les tests d’échec. Assurez-vous de tenir compte des éléments suivants lorsque vous effectuez vos modifications :

* On ne s’attend pas à ce que vous refactorisiez considérablement les éléments du code, mais les possibilités d’amélioration doivent être notées.
* Les tests unitaires doivent rester tels qu’ils sont écrits. Si vous pensez qu’un test pourrait être amélioré, notez-le comme quelque chose à discuter.
* Lorsque todo.api est démarré, une base de données SQLite est créée à l’adresse ./AppData/todo.db. Lors de l’exécution de tests, la base de données est créée en mémoire.
* Les données de test sont générées dans `todo.data.TodoContext.Seed()` pour les tests automatisés et lorsque l’application est en cours d’exécution. Chaque test est isolé et génère un nouvel.

## Pour débuter
1. (Facultatif) [Télécharger visual studio code (VSCode)](https://code.visualstudio.com/download)
1. [Télécharger le SDK .NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
1. Cloner ce projet, ou [télécharger le code source](https://github.com/tc-ca/todo/archive/refs/heads/main.zip) et en extraire le contenu 
1. (Facultatif) Ouvrez VSCode et ouvrez le dossier de niveau supérieur (fichier -> dossier ouvert) 
1. Dans un terminal à la racine du projet, exécutez les commandes suivantes (notez que les commandes `dotnet run` doivent être exécutées dans des instances de terminal distinctes) 

``` PowerShell
dotnet dev-certs https --trust
dotnet run --project todo.api/todo.api.csproj
dotnet run --project todo.web/todo.web.csproj
```

1. Consultez la documentation du swagger todo.api à l’adresse https://localhost:7180/swagger
1. Parcourez l’application Web à https://localhost:7172/

## Exécution de tests xUnit 
* Suivez les étapes de la rubrique [Pour débuter](#pour-débuter)
* Dans le terminal intégré, exécutez `dotnet test`

## Soumission de votre examen
Vous pouvez bifurquer le dépôt sur github, ou soumettre les fichiers sources modifiés en tant que pièce jointe à < informations de contact ici>. 

**Important**: Vous devez envoyer un courriel au contact identifié avant la date limite indiquée dans votre invitation, le courriel doit contenir votre soumission ou un lien vers votre dépôt github. La date/heure du courriel ou du fichier github sera utilisée pour s’assurer que les fichiers ont été soumis avant la date limite. 

