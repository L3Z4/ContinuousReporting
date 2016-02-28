CREATE TABLE [dbo].[ChallengeEntities] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [User]     NVARCHAR (MAX) NOT NULL,
    [Points]   FLOAT (53)     NOT NULL,
    [Build_Id] INT            NULL,
    CONSTRAINT [PK_dbo.ChallengeEntities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ChallengeEntities_dbo.BuildEntities_Build_Id] FOREIGN KEY ([Build_Id]) REFERENCES [dbo].[BuildEntities] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Build_Id]
    ON [dbo].[ChallengeEntities]([Build_Id] ASC);

