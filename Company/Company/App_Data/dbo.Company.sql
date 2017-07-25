CREATE TABLE [dbo].[Company] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Name]      NVARCHAR (50) NULL,
    [FK_Parent] INT           NULL DEFAULT 0,
    [MyValue]   INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

