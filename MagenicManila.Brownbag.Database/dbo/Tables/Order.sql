CREATE TABLE [dbo].[Order] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [PersonId]     INT           NOT NULL,
    [ProductId] INT NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Order_PersonId] FOREIGN KEY ([PersonId]) REFERENCES [dbo].[Person] ([Id]),
    CONSTRAINT [FK_Order_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);

