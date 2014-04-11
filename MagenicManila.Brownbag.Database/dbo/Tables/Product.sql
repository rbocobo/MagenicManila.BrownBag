CREATE TABLE [dbo].[Product](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Brand] [varchar](50) NOT NULL,
	[Price] [money] NOT NULL,
	[SKU] [varchar](50) NOT NULL, 
	 CONSTRAINT [PK_Product] PRIMARY KEY ([Id])
) ON [PRIMARY]

