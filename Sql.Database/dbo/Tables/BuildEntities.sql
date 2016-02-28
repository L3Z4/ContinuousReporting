CREATE TABLE [dbo].[BuildEntities] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [BuildName]  NVARCHAR (MAX) NOT NULL,
    [Definition] NVARCHAR (MAX) NOT NULL,
    [Date]       DATETIME       NOT NULL,
    [Status]     NVARCHAR (MAX) NOT NULL,
    [Reason]     NVARCHAR (MAX) NOT NULL,
    [Link]       NVARCHAR (MAX) NOT NULL,
    [User]       NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_dbo.BuildEntities] PRIMARY KEY CLUSTERED ([Id] ASC)
);

