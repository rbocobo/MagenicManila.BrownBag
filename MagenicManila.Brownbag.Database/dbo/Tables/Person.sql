CREATE TABLE [dbo].[Person] (
    [Id]         INT          IDENTITY (1, 1) NOT NULL,
    [FirstName]  VARCHAR (50) NOT NULL,
    [MiddleName] VARCHAR (50) NULL,
    [LastName]   VARCHAR (50) NOT NULL,
    [Gender]     VARCHAR (50) NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([Id] ASC)
);

