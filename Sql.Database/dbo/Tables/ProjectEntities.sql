CREATE TABLE [dbo].[ProjectEntities] (
    [Id]     INT            IDENTITY (1, 1) NOT NULL,
    [Name]   NVARCHAR (MAX) NULL,
    [Blocks] INT            NOT NULL,
    CONSTRAINT [PK_dbo.ProjectEntities] PRIMARY KEY CLUSTERED ([Id] ASC)
);

