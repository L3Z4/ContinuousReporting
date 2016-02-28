CREATE TABLE [dbo].[ModuleChallengeCoverages] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Module]             NVARCHAR (MAX) NOT NULL,
    [DeltaCoverage]      FLOAT (53)     NOT NULL,
    [Blocks]             INT            NOT NULL,
    [ChallengeEntity_Id] INT            NULL,
    CONSTRAINT [PK_dbo.ModuleChallengeCoverages] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.ModuleChallengeCoverages_dbo.ChallengeEntities_ChallengeEntity_Id] FOREIGN KEY ([ChallengeEntity_Id]) REFERENCES [dbo].[ChallengeEntities] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ChallengeEntity_Id]
    ON [dbo].[ModuleChallengeCoverages]([ChallengeEntity_Id] ASC);

