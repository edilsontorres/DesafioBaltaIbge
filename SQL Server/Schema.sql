DROP TABLE [IBGE]
CREATE TABLE [IBGE]
(    
    [Id] CHAR(7) NOT NULL,    
    [State] CHAR(2) NULL,    
	[City] NVARCHAR(80) NULL,
    CONSTRAINT [PK_IBGE] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_IBGE_Id] ON [IBGE] ([Id]);
GO

CREATE INDEX [IX_IBGE_City] ON [IBGE] ([City]);
GO

CREATE INDEX [IX_IBGE_State] ON [IBGE] ([State]);
GO