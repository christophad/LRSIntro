CREATE TABLE [dbo].[UserType] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (20) NOT NULL,
    [Code]        NCHAR (2)     NOT NULL,
    CONSTRAINT [PK_UserTypeId] PRIMARY KEY CLUSTERED ([Id] ASC),
);

CREATE TABLE [dbo].[UserTitle] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (20) NOT NULL,
    CONSTRAINT [PK_UserTitleId] PRIMARY KEY CLUSTERED ([Id] ASC),
);

CREATE TABLE [dbo].[User] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (20) NULL,
    [Surname]      NVARCHAR (20) NULL,
    [BirthDate]    DATE          NULL,
    [UserTypeId]   INT           NOT NULL,
    [UserTitleId]  INT           NOT NULL,
    [EmailAddress] NVARCHAR (50) NULL,
    [IsActive]     BIT           NULL,
    CONSTRAINT [PK_UserId] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_User_ToUserTitle] FOREIGN KEY ([UserTitleId]) REFERENCES [dbo].[UserTitle] ([Id]),
    CONSTRAINT [FK_User_ToUserType] FOREIGN KEY ([UserTypeId]) REFERENCES [dbo].[UserType] ([Id])
);

INSERT INTO [UserTitle] ([Description]) values ('Mr');
INSERT INTO [UserTitle] ([Description]) values ('Mrs');
INSERT INTO [UserType] ([Description], [Code]) values ('Developer', 'D');
INSERT INTO [UserType] ([Description], [Code]) values ('Manager', 'M')

