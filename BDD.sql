DROP TABLE [dbo].[Echeance] ;
DROP TABLE [dbo].[Type];
DROP TABLE [dbo].[Mouvement];
DROP TABLE [dbo].[Organisme] ;
DROP TABLE [dbo].[Compte] ;
DROP TABLE [dbo].[Categorie] ;
DROP TABLE [dbo].[User];


CREATE TABLE [dbo].[Organisme] (
    [Id]         VARCHAR(100) PRIMARY KEY  NOT NULL,
    [Nom]              VARCHAR (100) NOT NULL
);

CREATE TABLE [dbo].[Compte] (
    [Id]         VARCHAR(100) PRIMARY KEY  NOT NULL,
    [Nom]              VARCHAR (100) NOT NULL
);

CREATE TABLE [dbo].[Categorie] (
    [Id]         VARCHAR(100) PRIMARY KEY  NOT NULL,
    [Nom]              VARCHAR (100) NOT NULL
);

CREATE TABLE [dbo].[Type] (
    [Id]         VARCHAR(100) PRIMARY KEY  NOT NULL,
    [Nom]              VARCHAR (100) NOT NULL
);

CREATE TABLE [dbo].[Echeance] (
    [Id]         VARCHAR(100) PRIMARY KEY  NOT NULL,
    [Date]        DATETIME      NOT NULL,
	[TypeId]         VARCHAR (100)  NOT NULL,
	CONSTRAINT fk_type FOREIGN KEY(TypeId) REFERENCES Type(Id) 
);

CREATE TABLE [dbo].[Mouvement] (
    [Id]         VARCHAR(100) PRIMARY KEY  NOT NULL,
    [Nom]              VARCHAR (100) NOT NULL,
	[OrganismeId]         VARCHAR (100)  NOT NULL,
    [CompteId]         VARCHAR (100)  NOT NULL,
    [EcheanceId]         VARCHAR (100)  NOT NULL,
    [CategorieId]         VARCHAR (100) NOT NULL ,
	CONSTRAINT fk_organisme FOREIGN KEY(OrganismeId) REFERENCES Organisme(Id), 
	CONSTRAINT fk_compte FOREIGN KEY(CompteId) REFERENCES Compte(Id), 
	CONSTRAINT fk_categorie FOREIGN KEY(CategorieId) REFERENCES Categorie(Id) ,
	CONSTRAINT fk_echeance FOREIGN KEY(EcheanceId) REFERENCES Echeance(Id) 
);


CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](250) NOT NULL,
	[Password] [varchar](250) NOT NULL,
	[Role] [varchar](250) NULL,
	[Token] [varchar](250) NULL
) ON [PRIMARY]
