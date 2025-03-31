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
CREATE TABLE [CodeValues] (
    [Id] int NOT NULL IDENTITY,
    [Code] int NOT NULL,
    [Value] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_CodeValues] PRIMARY KEY CLUSTERED ([Id])
);

CREATE NONCLUSTERED INDEX [IX_CodeValues_Code] ON [CodeValues] ([Code]);

CREATE NONCLUSTERED INDEX [IX_CodeValues_Value] ON [CodeValues] ([Value]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250331184320_InitialCreate', N'9.0.3');

COMMIT;
GO

