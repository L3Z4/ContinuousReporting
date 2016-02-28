CREATE TABLE [dbo].[CoverageEntities] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Name]             NVARCHAR (MAX) NOT NULL,
    [BlocksCovered]    INT            NOT NULL,
    [BlocksNotCovered] INT            NOT NULL,
    [ComputedAverage]  INT            NOT NULL,
    [BuildID]          INT            NOT NULL,
    CONSTRAINT [PK_dbo.CoverageEntities] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.CoverageEntities_dbo.BuildEntities_BuildID] FOREIGN KEY ([BuildID]) REFERENCES [dbo].[BuildEntities] ([Id]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_BuildID]
    ON [dbo].[CoverageEntities]([BuildID] ASC);

