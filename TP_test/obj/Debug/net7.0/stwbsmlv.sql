IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Images] (
    [ImageID] int NOT NULL IDENTITY,
    [NomImage] nvarchar(max) NOT NULL,
    [ImageData] varbinary(max) NOT NULL,
    [ContentType] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Images] PRIMARY KEY ([ImageID])
);
GO

CREATE TABLE [Produits] (
    [Id] int NOT NULL IDENTITY,
    [Nom] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Marque] nvarchar(max) NOT NULL,
    [Taille] nvarchar(max) NULL,
    [Categorie] int NOT NULL,
    [Quantite] int NOT NULL,
    [ImageID] int NULL,
    CONSTRAINT [PK_Produits] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Produits_Images_ImageID] FOREIGN KEY ([ImageID]) REFERENCES [Images] ([ImageID])
);
GO

CREATE INDEX [IX_Produits_ImageID] ON [Produits] ([ImageID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231105192824_InitialMigration', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Panier] (
    [ID] int NOT NULL IDENTITY,
    [UserGuid] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Panier] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [ProduitPanier] (
    [ID] int NOT NULL IDENTITY,
    [ProduitID] int NOT NULL,
    [PanierID] int NOT NULL,
    [Quantite] int NOT NULL,
    CONSTRAINT [PK_ProduitPanier] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_ProduitPanier_Panier_PanierID] FOREIGN KEY ([PanierID]) REFERENCES [Panier] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_ProduitPanier_Produits_ProduitID] FOREIGN KEY ([ProduitID]) REFERENCES [Produits] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_ProduitPanier_PanierID] ON [ProduitPanier] ([PanierID]);
GO

CREATE INDEX [IX_ProduitPanier_ProduitID] ON [ProduitPanier] ([ProduitID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231108122007_AddPanier', N'7.0.13');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231108134355_Add_panier_Context', N'7.0.13');
GO

COMMIT;
GO

