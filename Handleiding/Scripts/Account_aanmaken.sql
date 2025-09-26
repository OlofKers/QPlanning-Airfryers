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