# QPlanning (Setup manual)

Onderstaand is het stappenplan te vinden om de QPlanning applicatie aan het werk te krijgen. Volg de stappen nauwkeurig op en zorg dat je verifieërd of de stap is gelukt.

## Back-end

1. ### Benodigde software (Installatie handleiding)

   - [ ] Installeer <u>Visual Studio 2022</u> **v17.14** (**Tip**: Zorg dat je Visual studio is voorzien van de laatste update)

     - [ ] Indien al geïnstalleerd update je Visual Studio 2022 via ![image-20250822100040118](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822100040118.png)

   - [ ] Installeer <u>[SQL Server-downloads | Microsoft](https://www.microsoft.com/nl-nl/sql-server/sql-server-downloads)</u> SQL Server Express 2022 is voldoende (Indien al geïnstalleerd kun je deze stap overslaan)

     - [ ] Na de setup is het belangrijk om de connection string die komt te staan in het overzicht goed te bewaren. ![image-20250826080819232](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250826080819232.png)

   - [ ] Installeer <u>[SQL Server Management Studio (SSMS)](https://aka.ms/ssms/21/release/vs_SSMS.exe)</u> 

     - [ ] Open SQL Server Management Studio en maak een connectie door de server naam die in je connectie string staat te gebruiken. ![image-20250826081722840](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250826081722840.png)

   - [ ] Installeer .net SDK [Download .NET 9.0 (Linux, macOS, and Windows) | .NET](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)

     ![image-20250822103029989](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822103029989.png)

2. ### Project (runnen in visual studio)

   - [ ] Pak het .zip bestand QPlanning-Applicatie uit en zet dit in een nieuw aan te maken folder QPlanning_Applicatie
   - [ ] Open de QPlanning.sln die in de folder staat met Visual Studio 2022
   - [ ] Klik met de rechter muisknop op de solution QPlanning en selecter rebuild solution ![image-20250822100959452](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822100959452.png)
   - [ ] Controlleer in de output of alles goed is verlopen.

3. ### Database

   1. #### Intialiseren

      - [ ] Klik rechtsboven bij de menu items in Visual Studio 2022 op Tools en selecteer: <u>Package Manager Console</u>![image-20250822101350049](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822101350049.png)
      - [ ] Pas de connectionstring aan in het volgende bestand zodat deze overeenkomt met de connectionstring die je hebt gekregen na de installatie van SQL Server. Zorg er in ieder geval voor dat de Server goed staat zodat die overeenkomt met die van je installatie. ![image-20250826085057673](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250826085057673.png)![image-20250826085221713](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250826085221713.png) 
      - [ ] Selecteer links onder bij de console output de Package Manager Console en selecteer als default project de QPlanning.Infrastructuur ![image-20250822101703775](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822101703775.png)
      - [ ] Type nu het commando: **Update-Database** en druk op Enter. ![image-20250822101811315](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822101811315.png)
      - [ ] Controleer of de database correct is aangemaakt.
   
   2. #### Account(s) aanmaken
   
      - [ ] Zorg dat je vanuit je tool om de database te bekijken (voorkeur: SQL Server Management Studio) de onderstaande query kunt uitvoeren. 
   
      - [ ] **Let op!** Pas de *<u>gebruikersnaam en het email adres aan in het onderstaande script</u>* zodat deze voor jezelf klopt. De hash voor het wachtwoord komt neer op het wachtwoord: **Test@1234** deze kun je dan ook gebruiken om in te loggen. Zodra je ingelogd bent kun je je wachtwoord zelf aanpassen.
   
        - [ ] Het script kan ook gevonden worden in de folder: <u>Handleiding\Scripts\Account_aanmaken.sql</u> deze kun je vanuit SQL Server Management Studio uitvoeren op de QPlanning database.
        
        
        ```sql
        USE [QPlanning]
        GO
        
        INSERT INTO [dbo].[AspNetUsers]
                   ([UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[SecurityStamp],[ConcurrencyStamp],[PhoneNumber],[PhoneNumberConfirmed],[TwoFactorEnabled],[LockoutEnd],[LockoutEnabled],[AccessFailedCount],[Voornaam],[Achternaam])
             VALUES
                   ('roy.mengelers@zuyd.nl','roy.mengelers@zuyd.nl','roy.mengelers@zuyd.nl','roy.mengelers@zuyd.nl',1,'AQAAAAEAACcQAAAAEKJ/FtPC/HBzXeUeYmNIcugWQFE6DkSPGvJTZYoIwEj68s/WdFy3OaAhopKKBln5Rw==','','fe269abb-4fe0-472b-86fd-df72ea1c1b3f',NULL,0,0,NULL,0,0,'Roy','Mengelers')
        GO
        
        INSERT INTO [dbo].[AspNetUserClaims]
                   ([UserId],[ClaimType],[ClaimValue])
             VALUES
                   (@@IDENTITY,'http://schemas.microsoft.com/ws/2008/06/identity/claims/role','Admin')
        GO
        
        INSERT INTO [dbo].[Medewerker]
                   ([Created],[CreatedBy],[Modified],[ModifiedBy],[Voornaam],[TussenVoegsel],[Achternaam],[Email],[Tarief],[InternTarief],[MedewerkerFunctieId],[TeamId],[IsActief])
             VALUES
                   (GETDATE(),'Script',GETDATE(),'Script','Roy',NULL,'Mengelers','roy.mengelers@zuyd.nl',125,100,2,1,1)
        GO
        ```
        
      - [ ] Controleer of je account correct is toegevoegd zodat je straks kunt inloggen.
   
4. ### Back-end (runnen)

   - [ ] Zorg ervoor dat je back-end project in debug modus draait:![image-20250822105039778](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822105039778.png)
   - [ ] Controleer of je de Swagger API endpoints pagina te zien krijgt. ![image-20250822105319425](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822105319425.png)

## Front-end

1. ### Benodigde software (Installatie handleiding)

   - [ ]  Installeer de laatste versie van <u>Node.js</u> [Node.js — Download Node.js®](https://nodejs.org/en/download)

   - [ ] Installeer Visual Studio Code [Visual Studio Code - Code Editing. Redefined](https://code.visualstudio.com/)

2. ### Project (runnen in visual studio code)

   - [ ] Open de folder vanuit Visual Studio Code: **QPlanning_Frontend-develop** die te vinden is in de folder QPlanning_Applicatie die eerder is aangemaakt. ![image-20250822104430372](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822104430372.png)
   - [ ] Open de terminal in Visual Studio Code. ![image-20250822104524093](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822104524093.png)
   - [ ] Type het volgende commando in de terminal: **npm install**
   - [ ] Controleer of het installeren is gelukt. 
   - [ ] Type het volgende commando in de terminal: **npm install -g angular-cli**
   - [ ] Controleer of deze is geïnstalleerd zonder fouten.
   - [ ] Type nu het volgende commando in de terminal: **npm start**
   - [ ] Controleer of je ziet dat het progamma start door CTRL ingedrukt te houden en op het adres http://localhost:4200/ te klikken. Of open je browser en ga naar  http://localhost:4200/.![image-20250822104822573](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822104822573.png)

## Inloggen

- [ ] Zorg ervoor dat je back-end project in debug modus draait:![image-20250822105039778](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822105039778.png)
- [ ] Controleer of je de Swagger API endpoints pagina te zien krijgt. ![image-20250822105319425](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822105319425.png)
- [ ] Navigeer in je browser naar http://localhost:4200/ ![image-20250822105506577](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822105506577.png)
- [ ] Log nu in met je gebruikersnaam in combinatie met het wachtwoord: Test@1234. Het wachtwoord kun je als je eenmaal ingelogd bent wijzigen naar het wachtwoord dat je graag zou willen hebben.
- [ ] Het is je **gelukt**!! Heel veel plezier in de zoektocht naar de functionaliteiten en bugs.

# Indruk van de applicatie

## Persoonlijk planningsoverzicht

![image-20250822105849599](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822105849599.png)

## Medewerkers beheren

![image-20250822105918653](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822105918653.png)

### 	Medewerker toevoegen/wijzigen

​		![image-20250822110117181](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822110117181.png)

## Klanten beheren

![image-20250822105954922](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822105954922.png)

### 	Klant toevoegen/bewerken

​		 ![image-20250822110207989](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822110207989.png)

### 	Boekjaren toevoegen

​		![image-20250822110248314](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822110248314.png)

## Gebruikers beheren

![image-20250822110037129](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822110037129.png)

## Boekingen overzicht

![image-20250822110519348](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822110519348.png)

### 	Boekingen toevoegen

​		![image-20250822110618193](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822110618193.png)

### 	Exporteren naar Excel

​		Het is mogelijk om het overzicht van de boekingen te exporteren naar Excel.

## Planningsoverzicht van klanten

![image-20250822110337352](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822110337352.png)

## Planningsoverzicht van medewerkers	

![image-20250822110722080](C:\Users\mengelersrmp\AppData\Roaming\Typora\typora-user-images\image-20250822110722080.png)

