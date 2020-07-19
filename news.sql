CREATE TABLE [dbo].[User] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Username] VARCHAR (100) NOT NULL,
    [Password] VARCHAR (255) NOT NULL,
    [Type]     INT           NOT NULL,
    [Fname]    VARCHAR (50)  NOT NULL,
    [Lname]    VARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    UNIQUE NONCLUSTERED ([Username] ASC)
);


CREATE TABLE [dbo].[Article] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Headline]  VARCHAR (255) NOT NULL,
    [Content]   TEXT          NOT NULL,
    [CreatedAt] DATE          NOT NULL,
    [Author]    INT           NOT NULL,
    [Image]     IMAGE         NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Author_Fkey] FOREIGN KEY ([Author]) REFERENCES [dbo].[User] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[Comment] (
    [Id]        INT  IDENTITY (1, 1) NOT NULL,
    [UserId]    INT  NOT NULL,
    [Content]   TEXT NOT NULL,
    [PostDate]  DATE NOT NULL,
    [ArticleId] INT  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [CommentArticle_Fkey] FOREIGN KEY ([ArticleId]) REFERENCES [dbo].[Article] ([Id]) ON DELETE CASCADE
);








