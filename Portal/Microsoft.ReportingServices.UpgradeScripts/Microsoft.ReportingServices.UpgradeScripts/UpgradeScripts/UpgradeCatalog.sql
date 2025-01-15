/**************************************************************/
/* Copyright (c) Microsoft.  All rights reserved. */
/**************************************************************/
-- !!! This assumes the database is created and the user is either a dbo or is added to the RSExecRole
-- !!! Please run setup to create the database, users, role !!!

--------------------------------------------------
------------- Upgrade RS Catalog
--------------------------------------------------

--- Starting in Kilimanjaro, the catalog version numbers were changed to be just an integer (starting with 100)
--- The old system (C.0.9.45) was last used in Katmai.

use ReportServer
GO

--- C.0.9.45 to 100 ---

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DBUpgradeHistory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
    create table [dbo].[DBUpgradeHistory](
       [UpgradeID] bigint identity(1,1) not null,
       [DbVersion] nvarchar(25),
       [User] nvarchar(128) default System_User,
       [DateTime] datetime default Current_Timestamp
       CONSTRAINT [PK_DBUpgradeHistory] PRIMARY KEY (UpgradeID DESC)
       ) ON [Primary];

    GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[DBUpgradeHistory] TO RSExecRole;

    if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '100')
    BEGIN
        INSERT into [dbo].[DBUpgradeHistory] ([DbVersion]) values ('100');
    END
END
GO

--- 100 to 101 ---

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '101')
BEGIN
    if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetDBVersion]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
    drop procedure [dbo].[GetDBVersion];

    exec('
    CREATE PROCEDURE [dbo].[GetDBVersion]
    @DBVersion nvarchar(32) OUTPUT
    AS
    SET @DBVersion = (select top(1) [DbVersion] from [dbo].[DBUpgradeHistory] ORDER BY [UpgradeID] DESC)')

    GRANT EXECUTE ON [dbo].[GetDBVersion] TO RSExecRole;

    if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '101')
    BEGIN
        INSERT into [dbo].[DBUpgradeHistory] ([DbVersion]) values ('101');
    END
END
GO

--- 101 to 102 ---

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '102')
BEGIN
    if (select count(*) from dbo.syscolumns where id = object_id('Subscriptions') and name = 'ReportZone') = 0
    begin
        ALTER TABLE [dbo].[Subscriptions]
            ADD [ReportZone] int NOT NULL DEFAULT 0
    end

    if (select count(*) from dbo.syscolumns where id = object_id('Notifications') and name = 'ReportZone') = 0
    begin
        ALTER TABLE [dbo].[Notifications]
            ADD [ReportZone] int NOT NULL DEFAULT 0
    end


    if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '102')
    BEGIN
        INSERT into [dbo].[DBUpgradeHistory] ([DbVersion]) values ('102');
    END
END
GO

--- 102 to 103 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '103')
BEGIN
    IF  EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Event]') AND name = N'IX_Event3')
    BEGIN
        DROP INDEX [IX_Event3] ON [dbo].[Event]
    END

    if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '103')
    BEGIN
        INSERT into [dbo].[DBUpgradeHistory] ([DbVersion]) values ('103');
    END
END
GO

--- 110 to 111
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '111')
BEGIN
    BEGIN TRAN
        CREATE TABLE [dbo].[DataSets] (
            [ID] uniqueidentifier NOT NULL,
            -- reference to report in Catalog table
            [ItemID] uniqueidentifier NOT NULL,
            [LinkID] uniqueidentifier NULL,
            [Name] [nvarchar] (260) NOT NULL
        ) ON [PRIMARY]

        ALTER TABLE [dbo].[DataSets] WITH NOCHECK ADD
            CONSTRAINT [PK_DataSet] PRIMARY KEY CLUSTERED (
                [ItemID], [Name]
            ) ON [PRIMARY],
            CONSTRAINT [FK_DataSetItemID] FOREIGN KEY (
                [ItemID]
            ) REFERENCES [dbo].[Catalog] (
                [ItemID]
            ),
            CONSTRAINT [FK_DataSetLinkID] FOREIGN KEY (
                [LinkID]
            ) REFERENCES [dbo].[Catalog] (
                [ItemID]
            )

        CREATE INDEX [IX_DataSetLinkID] ON [dbo].[DataSets]([LinkID]) ON [PRIMARY]

        GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[DataSets] TO RSExecRole

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('111');
    COMMIT
END
GO

--- 111 to 112 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '112')
BEGIN
    BEGIN TRAN
        --CREATE TABLE [dbo].[ComponentMapping] (
            --ComponentID uniqueidentifier not null,
            --ItemID uniqueidentifier not null,
            --constraint PK_ComponentMapping primary key (ComponentID),
            --constraint UNIQ_ItemID unique (ItemID),
            --constraint FK_ItemID foreign key (ItemID) references [Catalog](ItemID)
            --) ;

        ALTER TABLE [dbo].[Catalog] ADD SubType NVARCHAR(128) NULL ;
        ALTER TABLE [dbo].[Catalog] ADD ComponentID UNIQUEIDENTIFIER NULL ;

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('112') ;
    COMMIT
END
GO

--- 112 to 113 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '113')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('113') ;
    COMMIT
END
GO

--- 113 to 114 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '114')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('114') ;
    COMMIT
END
GO

--- 114 to 115 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '115')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('115') ;
    COMMIT
END
GO

--- 115 to 116 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '116')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('116') ;
    COMMIT
END
GO

--- 116 to 117 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '117')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('117') ;
    COMMIT
END
GO

--- 117 to 118 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '118')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('118') ;
    COMMIT
END
GO

--- 118 to 119 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '119')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('119') ;
    COMMIT
END
GO

--- 119 to 120 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '120')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('120');
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '121')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[ExecutionLogStorage] ALTER COLUMN [RequestType] TINYINT NOT NULL

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('121') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '122')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('122') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '123')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('123') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '124')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('124') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '125')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('125') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '126')
BEGIN
    BEGIN TRAN
        CREATE NONCLUSTERED INDEX IX_ComponentLookup ON [Catalog]([Type], [ComponentID])

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('126') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '127')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('127') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '129')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('129') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '140')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[DataSets] DROP CONSTRAINT PK_DataSet ;

        ALTER TABLE [dbo].[DataSets] WITH CHECK ADD
            CONSTRAINT [PK_DataSet] PRIMARY KEY NONCLUSTERED ( [ID] ) ;

        CREATE CLUSTERED INDEX [IX_DataSet_ItemID_Name] ON [dbo].[DataSets] (
            [ItemID], [Name] );

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('140') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '141')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('141') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '142')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('142') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradehistory] where [DbVersion] = '143')
BEGIN
    BEGIN TRAN
        create table [dbo].[ServerUpgradeHistory](
           [UpgradeID] bigint identity(1,1) not null,
           [ServerVersion] nvarchar(25),
           [User] nvarchar(128) default System_User,
           [DateTime] datetime default Current_Timestamp
           CONSTRAINT [PK_ServerUpgradeHistory] PRIMARY KEY (UpgradeID DESC)
           ) ON [Primary];

        GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[ServerUpgradeHistory] TO RSExecRole;

        INSERT into [dbo].[DBUpgradeHistory] ([DbVersion]) values ('143');
        INSERT into [dbo].[ServerUpgradeHistory] ([ServerVersion]) values ('143');

    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '144')
BEGIN
    if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetDBVersion]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
    drop procedure [dbo].[GetDBVersion];

    exec('
    CREATE PROCEDURE [dbo].[GetDBVersion]
    @DBVersion nvarchar(32) OUTPUT
    AS
    SET @DBVersion = (select top(1) [ServerVersion] from [dbo].[ServerUpgradeHistory] ORDER BY [UpgradeID] DESC)')

    GRANT EXECUTE ON [dbo].[GetDBVersion] TO RSExecRole;

    if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '144')
    BEGIN
        INSERT into [dbo].[DBUpgradeHistory] ([DbVersion]) values ('144');
    END
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '146')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('146') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '147')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('147') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '148')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('148') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '149')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('149') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '150')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('150') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '151')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('151') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '152')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('152') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '153')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('153') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '155')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('155') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '156')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('156') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '157')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('157') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '158')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('158') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '159')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('159') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '161')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('161') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '162')
BEGIN
   INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('162') ;
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '163')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[Users]
        ADD [ServiceToken] [ntext] NULL;

    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('163') ;
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '164')
BEGIN
    BEGIN TRAN
        CREATE TABLE [dbo].[SubscriptionResults] (
            [SubscriptionResultID] uniqueidentifier NOT NULL,
            [SubscriptionID] uniqueidentifier NOT NULL,
            [ExtensionSettingsHash] int NOT NULL,
            [ExtensionSettings] nvarchar(max) NOT NULL,
            [SubscriptionResult] nvarchar(260) NULL
        ) ON [PRIMARY]

        GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[SubscriptionResults] TO RSExecRole

        ALTER TABLE [dbo].[SubscriptionResults] WITH NOCHECK ADD
            CONSTRAINT [PK_SubscriptionResults] PRIMARY KEY CLUSTERED (
                [SubscriptionResultID]
            )  ON [PRIMARY],
            CONSTRAINT [FK_SubscriptionResults_Subscriptions] FOREIGN KEY (
                [SubscriptionID]
            ) REFERENCES [dbo].[Subscriptions] (
                [SubscriptionID]
            ) ON DELETE CASCADE

        CREATE INDEX [IX_SubscriptionResults] ON [dbo].[SubscriptionResults]([SubscriptionID], [ExtensionSettingsHash]) ON [PRIMARY]

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('164');
    COMMIT
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '165')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[Users]
        ADD [Setting] [ntext] NULL;

    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('165') ;
    COMMIT
END
GO

-- Take care of SQL11_SP2 Version 163 (Hotfix 4111094) and SQL16 Version 163.
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '166')
BEGIN
    if COL_LENGTH('[dbo].[Users]', 'ServiceToken') IS NULL
    BEGIN
        BEGIN TRAN
            ALTER TABLE [dbo].[Users]
            ADD [ServiceToken] [ntext] NULL;
        COMMIT
    END
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('166');
END
GO

--New stored procedure for the KpiUpdateData event
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '167')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('167');
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '168')
BEGIN
    BEGIN TRAN
        CREATE TABLE [dbo].[Favorites](
            [ItemID] [uniqueidentifier] NOT NULL,
            [UserID] [uniqueidentifier] NOT NULL,
        CONSTRAINT [PK_Favorites] PRIMARY KEY NONCLUSTERED
        (
            [ItemID] ASC,
            [UserID] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY]

        GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[Favorites] TO RSExecRole

        CREATE CLUSTERED INDEX [IX_Favorites_UserID] ON [dbo].[Favorites]
        (
            [UserID] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

        ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Favorites_Catalog] FOREIGN KEY([ItemID])
        REFERENCES [dbo].[Catalog] ([ItemID])

        ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK_Favorites_Catalog]

        ALTER TABLE [dbo].[Favorites]  WITH CHECK ADD  CONSTRAINT [FK_Favorites_Users] FOREIGN KEY([UserID])
        REFERENCES [dbo].[Users] ([UserID])

        ALTER TABLE [dbo].[Favorites] CHECK CONSTRAINT [FK_Favorites_Users]

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('168');
    COMMIT
END
GO

-- Add System Resources folder
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '169')
BEGIN
    BEGIN TRAN
        DECLARE @Now DateTime;
        SET @Now = GETDATE();

        -- Get root folder and policy IDs
        DECLARE @RootFolderID as uniqueidentifier;
        DECLARE @RootPolicyID as uniqueidentifier;
        SELECT
            @RootFolderID = [ItemID],
            @RootPolicyID = [PolicyID]
        FROM [dbo].[Catalog]
        WHERE Path = N'';

        -- Create System Resource catalog entry
        DECLARE @SysResItemID as uniqueidentifier;
        SET @SysResItemID = newid();
        EXEC [dbo].[CreateObject]
           @ItemID = @SysResItemID,
           @Name = N'System Resources', -- Constant defined in ReportingServicesLibrary.SystemResource
           @Path = N'/68f0607b-9378-4bbb-9e70-4da3d7d66838', -- Constant defined in ReportingServicesLibrary.SystemResource
           @ParentID = @RootFolderID,
           @Type = 1,
           @Content = null,
           @Intermediate = null,
           @LinkSourceID = null,
           @Property = null,
           @Description = null,
           @CreatedBySid = 0x010100000000000512000000, -- local system
           @CreatedByName = N'NT AUTHORITY\SYSTEM',
           @AuthType = 1,
           @CreationDate = @Now,
           @ModificationDate = @Now,
           @MimeType = null,
           @SnapshotLimit = null,
           @PolicyRoot = 0, -- false, inherit root policy
           @PolicyID = @RootPolicyID

        -- Bump database version
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('169');
    COMMIT
END
GO

--- 169 to 170; for updating PROCEDURE [dbo].[CleanOrphanedSnapshots] during upgrades. Remember to update Common\DBUtils.cs as well.
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '170')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('170') ;
    COMMIT
END
GO

--- 172 to 173; for updating PROCEDURE [dbo].[CleanUpExpiredSessions] during upgrades.
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '173')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('173') ;
    COMMIT
END
GO

--173 to 174; for adding procedure [dbo].[UpdateUsernameFromSID]
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '174')
BEGIN
    BEGIN TRAN
    ALTER TABLE [dbo].[Users]
    ADD [ModifiedDate] DATETIME NULL;

    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('174') ;

    COMMIT
END
GO

-- BUG Fix: Upgrade to current from SP1 is broken due to version collision (174 with different code)
-- IMPORTANT: Don't port this to SQL16 SP branches.
IF EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] WHERE [DbVersion] = '174')
   AND NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ModifiedDate' AND OBJECT_ID = OBJECT_ID(N'[dbo].[Users]'))
BEGIN
    BEGIN TRAN
    ALTER TABLE [dbo].[Users]
    ADD [ModifiedDate] DATETIME NULL;
    COMMIT
END
GO

--174 to 175; for adding tables to support comments [Comments]
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] WHERE [DbVersion] = '175')
BEGIN
    BEGIN TRAN
        CREATE TABLE [dbo].[Comments] (
            [CommentID]  bigint identity(1,1) NOT NULL,
            -- reference to catalog item this is attached to
            [ItemID] uniqueidentifier NOT NULL,
            -- reference to the user who made this comment
            [UserID] uniqueidentifier NOT NULL,
            -- reference to the parent comment (threading)
            [ThreadID]  bigint NULL,
            [Text] nvarchar(2048) NOT NULL,
            [CreatedDate] datetime NOT NULL,
            [ModifiedDate] datetime NULL,
        ) ON [PRIMARY]

        GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[Comments] TO RSExecRole

        ALTER TABLE [dbo].[Comments] WITH NOCHECK ADD
            CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED (
                [CommentID]
            )  ON [PRIMARY],
            CONSTRAINT [FK_Comments_Catalog] FOREIGN KEY (
                [ItemID]
            ) REFERENCES [dbo].[Catalog] (
                [ItemID]
            ) ON DELETE CASCADE,
            CONSTRAINT [FK_Comments_Users] FOREIGN KEY (
                [UserID]
            ) REFERENCES [dbo].[Users] (
                [UserID]
            ) ON DELETE CASCADE,
            CONSTRAINT [FK_Comments_Comments] FOREIGN KEY (
                [ThreadID]
            ) REFERENCES [dbo].[Comments] (
                [CommentID]
            ) ON DELETE NO ACTION -- only option is NO ACTION else SS rejects it for being capable of casuing loops. Have to manually make any adjustments.

        CREATE INDEX [IX_Comments_Item] ON [dbo].[Comments]([ItemID]) ON [PRIMARY]
        CREATE INDEX [IX_Comments_User] ON [dbo].[Comments]([UserID]) ON [PRIMARY]
        CREATE INDEX [IX_Comments_Thread] ON [dbo].[Comments]([ThreadID]) ON [PRIMARY]

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('175') ;
    COMMIT
END
GO

-- BUG Fix: Upgrade to current from SP1 is broken due to version collision (version 175 with different code)
--     Lack of this fix will cause 210 below to fail
-- IMPORTANT: Code inside must be in sync with code above all the time.
IF EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] WHERE [DbVersion] = '175')
   AND NOT EXISTS (SELECT * FROM  dbo.sysobjects where id = object_id(N'[dbo].[Comments]'))
BEGIN
    BEGIN TRAN
        CREATE TABLE [dbo].[Comments] (
            [CommentID]  bigint identity(1,1) NOT NULL,
            -- reference to catalog item this is attached to
            [ItemID] uniqueidentifier NOT NULL,
            -- reference to the user who made this comment
            [UserID] uniqueidentifier NOT NULL,
            -- reference to the parent comment (threading)
            [ThreadID]  bigint NULL,
            [Text] nvarchar(2048) NOT NULL,
            [CreatedDate] datetime NOT NULL,
            [ModifiedDate] datetime NULL,
        ) ON [PRIMARY]

        GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[Comments] TO RSExecRole

        ALTER TABLE [dbo].[Comments] WITH NOCHECK ADD
            CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED (
                [CommentID]
            )  ON [PRIMARY],
            CONSTRAINT [FK_Comments_Catalog] FOREIGN KEY (
                [ItemID]
            ) REFERENCES [dbo].[Catalog] (
                [ItemID]
            ) ON DELETE CASCADE,
            CONSTRAINT [FK_Comments_Users] FOREIGN KEY (
                [UserID]
            ) REFERENCES [dbo].[Users] (
                [UserID]
            ) ON DELETE CASCADE,
            CONSTRAINT [FK_Comments_Comments] FOREIGN KEY (
                [ThreadID]
            ) REFERENCES [dbo].[Comments] (
                [CommentID]
            ) ON DELETE NO ACTION -- only option is NO ACTION else SS rejects it for being capable of casuing loops. Have to manually make any adjustments.

        CREATE INDEX [IX_Comments_Item] ON [dbo].[Comments]([ItemID]) ON [PRIMARY]
        CREATE INDEX [IX_Comments_User] ON [dbo].[Comments]([UserID]) ON [PRIMARY]
        CREATE INDEX [IX_Comments_Thread] ON [dbo].[Comments]([ThreadID]) ON [PRIMARY]
    COMMIT
END
GO

--175 to 180; adding logical lock for nightly cleanup (preventing contention in scalout envs) [Comments]
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] WHERE [DbVersion] = '180')
BEGIN
    BEGIN TRAN

        IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[CleanupLock]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
        DROP TABLE [dbo].[CleanupLock]

        CREATE TABLE [dbo].[CleanupLock] (
            [ID] INT,
            [MachineName] NVARCHAR(256) NULL,
            [LockDate] [DATETIME] NOT NULL,
            CONSTRAINT [PK_ID] PRIMARY KEY (ID)
        ) ON [PRIMARY]

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('180') ;
    COMMIT
END
GO

--- 200; for updating PROCEDURE [dbo].[FindfavoriteableItems] during upgrades.
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '200')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('200') ;
    COMMIT
END
GO

--- 210; for updating comments table and related procedures
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '210')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[Comments]
        ADD [AttachmentID] uniqueidentifier NULL;

        ALTER TABLE [dbo].[Comments] WITH NOCHECK ADD
        CONSTRAINT [FK_Comments_CatalogResource] FOREIGN KEY (
                [AttachmentID]
            ) REFERENCES [dbo].[Catalog] (
                [ItemID]
                -- could cause multiple cascade paths. It should only be deleted when the comment is.
            ) ON DELETE NO ACTION

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('210') ;
    COMMIT
END
GO

--- 220; for removing PROCEDURE [dbo].[SetClientSecret] and adding PROCEDURE [dbo].[SetConfigurationInfoValue] and updating PROCEDURE [dbo].[ListInfoForReencryption] during upgrades.
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '220')
BEGIN
    BEGIN TRAN
        CREATE TYPE [dbo].[EncryptedConfigList]
        AS TABLE
            (
                ConfigName NVARCHAR(256)
            )

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('220') ;
    COMMIT
END
GO

--- 225; for updating roles for comment security
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '225')
BEGIN
    BEGIN TRAN
        -- Append 0's to all TaskMask's for site roles up to exactly 9 entries.
        -- we treat missing values in this mask as 0's, and used to fix this in code.
        UPDATE [dbo].[Roles]
        SET [TaskMask] = '' + [TaskMask] + REPLICATE('0', 9 - len([TaskMask]))
        where RoleFlags = 1 AND len([TaskMask]) < 9

        -- Append 0's to all TaskMask's for folders roles up to exactly 18 entries.
        -- we used to in code append missing values as 0's but no longer do.
        UPDATE [dbo].[Roles]
        SET [TaskMask] = '' + [TaskMask] + REPLICATE('0', 18 - len([TaskMask]))
        where RoleFlags = 0 AND len([TaskMask]) < 18
    COMMIT
END
GO

--- 230; for converting Pbix files saved as resources to PbixCatalogItemType
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '230')
BEGIN
    BEGIN TRAN
        UPDATE [dbo].[Catalog]
        SET [Type] = 13
        WHERE Type = 3 AND Name LIKE '%.pbix'
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('230');
    COMMIT
END
GO

--- RenderMobileReport
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '235')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('235');
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '236')
BEGIN
    BEGIN TRAN
        ALTER TABLE Datasource ADD DSIDNum BIGINT IDENTITY(1,1) UNIQUE;
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('236');
    COMMIT
END
GO

--- version bump for ItemPath update in GetCommentByCommentID Stored Procedure
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '240')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('240');
END
GO

--- version bump for Content Cache in Temp DB and Stored procedures
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '245')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('245');
END
GO

--- version bump for removing content cache when the cache is disabled
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '250')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('250');
END
GO

--- version bump for update on GetChildrenBeforeDelete stored proc
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '255')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('255');
END
GO

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '260')
BEGIN
    BEGIN TRAN
        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Policy' and object_id = OBJECT_ID(N'[dbo].[Catalog]'))
        BEGIN
            CREATE INDEX [IX_Policy] ON [dbo].[Catalog]([PolicyID]) ON [PRIMARY]
        END
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('260');
    COMMIT
END
GO

--- version bump to introduce comments feature role permissions and updating the policies
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '270')
BEGIN
    BEGIN TRAN
        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_SecData_NtSecDescState' and object_id = OBJECT_ID(N'[dbo].[SecData]'))
        BEGIN
            ALTER TABLE dbo.SecData ADD NtSecDescState int NOT NULL CONSTRAINT DF_SecData_NtSecDescState DEFAULT (0)

            CREATE NONCLUSTERED INDEX IX_SecData_NtSecDescState ON dbo.SecData (NtSecDescState) ON [PRIMARY]
        END

        if exists (SELECT * FROM [dbo].[UpgradeInfo] where [Item] = 'EnableComments')
			UPDATE [dbo].[UpgradeInfo] SET [Status] = 'False' WHERE [Item] = 'EnableComments'
		else
			INSERT INTO [dbo].[UpgradeInfo] ([Item],[Status]) VALUES ('EnableComments', 'False')

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('270');
    COMMIT
END
GO

--- version bump to create [ProductInfoHistory] table
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '274')
BEGIN
    IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[ProductInfoHistory]') AND OBJECTPROPERTY(id, N'IsUserTable') = 1)
        DROP TABLE [dbo].[ProductInfoHistory];

    BEGIN TRAN
        CREATE TABLE [dbo].[ProductInfoHistory] (
            [DateTime] datetime default Current_Timestamp,
            [DbSchemaHash] varchar(128) NOT NULL,
            [Sku] varchar(25) NOT NULL,
            [BuildNumber] varchar(25) NOT NULL
        ) ON [PRIMARY]

        ALTER TABLE [dbo].[ProductInfoHistory] ADD CONSTRAINT IX_ProductInfoHistory_DateTime UNIQUE(DateTime);

        GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[ProductInfoHistory] TO RSExecRole;

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('274');
    COMMIT
END
GO

--- last version entry into the ServerUpgradeHistory table to let older RS versions (2016 and before) know this catalog has been upgraded
--- the table is not used by PBIRS and SSRS 2017
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'Post2016')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('Post2016') ;

        INSERT into [dbo].[ServerUpgradeHistory] ([ServerVersion]) values ('2017');
    COMMIT
END
GO

--- Database versioning has changed as of 4/2017. Please read the following for details...
---
--- 1) Any schema changes should still be guarded with the "if not exists..." pattern, so that it can be run multiple times.
--- 2) We are no longer using numeric versions, so instead of a numeric version, you should use a meaningful identifier,
---    such as "EnableCommentsByDefault".
--- 3) The new system uses a hash of all the database scripts (UpgradeCatalog, UpgradeStoredProcedures, UpgradeTempDB) to determine whether it should
---    run the scripts to do an upgrade, so there is no need to do anything other than edit the scripts to trigger an upgrade.

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'AddCommentAlertingTables')
BEGIN
    BEGIN TRAN
        CREATE TABLE [dbo].[AlertSubscribers](
            [AlertSubscriptionID] [bigint] IDENTITY(1,1) NOT NULL,
            [AlertType] [nvarchar](50) NOT NULL,
            [UserID] [uniqueidentifier] NOT NULL,
            [ItemID] [uniqueidentifier] NOT NULL
        ) ON [PRIMARY];

        GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[AlertSubscribers] TO RSExecRole;

		ALTER TABLE [dbo].[AlertSubscribers] WITH CHECK ADD CONSTRAINT [FK_AlertSubscribers_Users] FOREIGN KEY([UserID])
		REFERENCES [dbo].[Users] ([UserID]);

		ALTER TABLE [dbo].[AlertSubscribers] WITH CHECK ADD CONSTRAINT [FK_AlertSubscribers_Catalog] FOREIGN KEY([ItemID])
		REFERENCES [dbo].[Catalog] ([ItemID]);

        CREATE TABLE [dbo].[UserContactInfo](
            [UserID] [uniqueidentifier] NOT NULL,
            [DefaultEmailAddress] [nvarchar](256) NULL
        ) ON [PRIMARY];

        GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[UserContactInfo] TO RSExecRole;

		ALTER TABLE [dbo].[UserContactInfo] WITH CHECK ADD CONSTRAINT [FK_UserContactInfo_Users] FOREIGN KEY([UserID])
		REFERENCES [dbo].[Users] ([UserID]);

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('AddCommentAlertingTables');
    COMMIT
END
GO

-- Changes in ExecutionLog3 view to accomodate Excel ReportActions.
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'ExcelExecutionLogView')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('ExcelExecutionLogView') ;
    COMMIT
END
GO

-- Create table for Power BI embedded data sources.
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'DataModelDataSource')
BEGIN
    BEGIN TRAN
        CREATE TABLE [dbo].[DataModelDataSource] (
            [DSID] bigint identity(1,1) NOT NULL,
            [ItemId] uniqueidentifier NOT NULL,
            [DSType] int NOT NULL,
            [DSKind] int NOT NULL,
            [AuthType] int NOT NULL,
            [ConnectionString] varbinary(max) NULL,
            [Username] varbinary(max) NULL,
            [Password] varbinary(max) NULL
        ) ON [PRIMARY]

        GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[DataModelDataSource] TO RSExecRole

        ALTER TABLE [dbo].[DataModelDataSource] WITH NOCHECK ADD
            CONSTRAINT [PK_DataModelDataSource] PRIMARY KEY CLUSTERED (
                [DSID]
            ) ON [PRIMARY],
            CONSTRAINT [FK_DataModelDataSource_Catalog] FOREIGN KEY (
                [ItemId]
            ) REFERENCES [dbo].[Catalog] (
                [ItemID]
            ) ON DELETE CASCADE

        CREATE INDEX [IX_DataModelDataSource] ON [dbo].[DataModelDataSource]([ItemID]) ON [PRIMARY]

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('DataModelDataSource');
    COMMIT
END
GO

-- Add index to reduce likelyhood of deadlocks when refreshing KPI
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'ExecutionCacheLastUsed')
BEGIN
    BEGIN TRAN
        CREATE  INDEX [IX_DatasetLookup] ON [dbo].[Catalog]([Type], ItemID) 

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('ExecutionCacheLastUsed') ;
    COMMIT
END
GO

-- Upgrade [Catalog].[Content] data type from IMAGE to VARBINARY(MAX) .
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'UpgradeCatalogContent')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[Catalog] ALTER COLUMN [Content] VARBINARY(MAX)

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('UpgradeCatalogContent') ;
    COMMIT
END
GO

-- Add ModelConnectionName to DataModelDataSource
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'ModelConnectionName')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[DataModelDataSource]  ADD ModelConnectionName VARCHAR(260) NULL;  

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('ModelConnectionName') ;
    COMMIT
END
GO

-- Create table for storing extended content for catalog items
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'ExtendedContent')
BEGIN
    BEGIN TRAN
        CREATE TABLE [dbo].[CatalogItemExtendedContent]
        (
            Id BIGINT IDENTITY(1,1),
            ItemId UNIQUEIDENTIFIER,
            ContentType VARCHAR(50),
            Content VARBINARY(MAX),
            ModifiedDate DATETIME
        )

         ALTER TABLE [dbo].[CatalogItemExtendedContent] WITH NOCHECK ADD
            CONSTRAINT PK_CatalogItemExtendedContent PRIMARY KEY CLUSTERED (
                Id
            ) ON [PRIMARY],
            CONSTRAINT [FK_CatalogItemExtendedContent_Catalog] FOREIGN KEY (
                ItemId
            ) REFERENCES [dbo].[Catalog] (
                ItemID
            ) ON DELETE CASCADE

        CREATE INDEX [IX_ItemId_CatalogItemExtendedContent] ON [dbo].[CatalogItemExtendedContent]([ItemID],[ContentType]) ON [PRIMARY]

        GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[CatalogItemExtendedContent] TO RSExecRole
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('ExtendedContent') ;
    COMMIT
END
GO

-- Add new columns to the DataModelDataSource table
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] WHERE [DbVersion] = 'UpdateDataModelDataSource')
BEGIN
    BEGIN TRAN
        IF (SELECT count(*) FROM dbo.syscolumns WHERE id = object_id('DataModelDataSource') and name = 'CreatedByID') = 0
            BEGIN
                ALTER TABLE [dbo].[DataModelDataSource] ADD [CreatedByID] UNIQUEIDENTIFIER NULL
            END

        IF (SELECT count(*) FROM dbo.syscolumns WHERE id = object_id('DataModelDataSource') and name = 'CreatedDate') = 0
            BEGIN
                ALTER TABLE [dbo].[DataModelDataSource] ADD [CreatedDate] DATETIME default Current_Timestamp NOT NULL
            END

        IF (SELECT count(*) FROM dbo.syscolumns WHERE id = object_id('DataModelDataSource') and name = 'ModifiedByID') = 0
            BEGIN
                ALTER TABLE [dbo].[DataModelDataSource] ADD [ModifiedByID] UNIQUEIDENTIFIER NULL
            END

        IF (SELECT count(*) FROM dbo.syscolumns WHERE id = object_id('DataModelDataSource') and name = 'ModifiedDate') = 0
            BEGIN
                ALTER TABLE [dbo].[DataModelDataSource] ADD [ModifiedDate] DATETIME default Current_Timestamp NOT NULL
            END
        
        IF (SELECT count(*) FROM dbo.syscolumns WHERE id = object_id('DataModelDataSource') and name = 'DataSourceID') = 0
            BEGIN
                ALTER TABLE [dbo].[DataModelDataSource] ADD [DataSourceID] UNIQUEIDENTIFIER DEFAULT newsequentialid() NOT null
                CREATE INDEX [IX_DataModelDataSource_DataSourceID] ON [dbo].[DataModelDataSource]([DataSourceID]) ON [PRIMARY]
            END

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('UpdateDataModelDataSource') ;
    COMMIT
END
GO

-- Set Maximum catalog item size to 1024 MB
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'MaxCatalogFileSizeMb')
BEGIN
    BEGIN TRAN
        IF NOT EXISTS (SELECT * FROM [dbo].[ConfigurationInfo]   WHERE [Name] = 'MaxFileSizeMb')
        BEGIN
            INSERT INTO [dbo].[ConfigurationInfo] VALUES (newid(), 'MaxFileSizeMb', '1000') ;
        END

        UPDATE [dbo].[ConfigurationInfo] SET [Value] = '1000' WHERE [Name] = 'MaxFileSizeMb'

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('MaxCatalogFileSizeMb') ;
    COMMIT
END
GO

-- Remove Portal2 by default for the first time
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'RemovedUsePortalV2')
BEGIN
    BEGIN TRAN
        DELETE FROM [dbo].[ConfigurationInfo] WHERE [Name] = 'UsePortalV2';
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('RemovedUsePortalV2') ;
    COMMIT
END
GO

-- Update EnablePowerBIReportMigrate by default for the first time
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'UpdateMigrationFlag')
BEGIN
    BEGIN TRAN
        IF NOT EXISTS (SELECT * FROM [dbo].[ConfigurationInfo]   WHERE [Name] = 'EnablePowerBIReportMigrate')
        BEGIN
            INSERT INTO [dbo].[ConfigurationInfo] VALUES (newid(), 'EnablePowerBIReportMigrate', 'True') ;
        END

        UPDATE [dbo].[ConfigurationInfo] SET [Value] = 'True' WHERE [Name] = 'EnablePowerBIReportMigrate'

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('UpdateMigrationFlag') ;
    COMMIT
END

-- Add ContentSize field to Catalog table
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'AddCatalogContentSize')
BEGIN
    BEGIN TRAN
        IF (SELECT count(*) FROM dbo.syscolumns WHERE id = object_id('Catalog') and name = 'ContentSize') = 0
            BEGIN
                ALTER TABLE [dbo].[Catalog] ADD [ContentSize] BIGINT NULL
            END

        EXEC ('UPDATE [dbo].[Catalog] SET [ContentSize] = DATALENGTH([Content])') ;

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('AddCatalogContentSize') ;
    COMMIT
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] WHERE [DbVersion] = 'ChangeDataModelDsToString')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[DataModelDataSource] ALTER COLUMN DSType VARCHAR (100)
        ALTER TABLE [dbo].[DataModelDataSource] ALTER COLUMN DSKind VARCHAR (100)
        ALTER TABLE [dbo].[DataModelDataSource] ALTER COLUMN AuthType VARCHAR (100)

        UPDATE [dbo].[DataModelDataSource] SET DSType ='Unknown' WHERE DSType = '0'
        UPDATE [dbo].[DataModelDataSource] SET DSType ='Live' WHERE DSType = '1'
        UPDATE [dbo].[DataModelDataSource] SET DSType ='DirectQuery' WHERE DSType = '2'
        UPDATE [dbo].[DataModelDataSource] SET DSType ='Import' WHERE DSType = '3'

        UPDATE [dbo].[DataModelDataSource] SET DSKind ='UnknownFunction' WHERE DSKind = '0'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='AnalysisServices' WHERE DSKind = '1'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='SQL' WHERE DSKind = '2'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='File' WHERE DSKind = '3'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='AzureBlobs' WHERE DSKind = '4'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Oracle' WHERE DSKind = '5'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Folder' WHERE DSKind = '6'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='MySql' WHERE DSKind = '7'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Teradata' WHERE DSKind = '8'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Web' WHERE DSKind = '9'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='OData' WHERE DSKind = '10'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='HDInsight' WHERE DSKind = '11'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='AzureTables' WHERE DSKind = '12'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='SharePoint' WHERE DSKind = '13'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='DataMarket' WHERE DSKind = '14'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='PostgreSQL' WHERE DSKind = '15'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Sybase' WHERE DSKind = '16'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='DB2' WHERE DSKind = '17'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Informix' WHERE DSKind = '18'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='MQ' WHERE DSKind = '19'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='ActiveDirectory' WHERE DSKind = '20'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='CurrentWorkbook' WHERE DSKind = '21'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Exchange' WHERE DSKind = '22'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Facebook' WHERE DSKind = '23'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Hdfs' WHERE DSKind = '24'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='SapBusinessObjects' WHERE DSKind = '25'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Salesforce' WHERE DSKind = '26'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='Odbc' WHERE DSKind = '27'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='GoogleAnalytics' WHERE DSKind = '28'
        UPDATE [dbo].[DataModelDataSource] SET DSKind ='SapBusinessWarehouse' WHERE DSKind = '29'

        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'Unknown' WHERE AuthType = '0'
        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'Anonymous' WHERE AuthType = '1'
        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'Integrated' WHERE AuthType = '2'
        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'Windows' WHERE AuthType = '3'
        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'UsernamePassword' WHERE AuthType = '4'
        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'Key' WHERE AuthType = '5'
        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'UsernamePassword' WHERE AuthType = '6'
        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'Impersonate' WHERE AuthType = '7'

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('ChangeDataModelDsToString');
    COMMIT
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] WHERE [DbVersion] = 'UpdateDataModelStrings')
BEGIN
    BEGIN TRAN
        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'Anonymous' WHERE AuthType = 'None'
        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'UsernamePassword' WHERE AuthType = 'Sql'
        UPDATE [dbo].[DataModelDataSource] SET AuthType = 'UsernamePassword' WHERE AuthType = 'Basic'

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('UpdateDataModelStrings');
    COMMIT
END
GO

-- Migrate Excel and PowerBI content to the extended content table
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'MoveExcelPowerBIContent')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[CatalogItemExtendedContent] (ItemId, ContentType, Content, ModifiedDate)
        SELECT ItemId, 'CatalogItem' as ContentType, Content, ModifiedDate
        FROM [dbo].[Catalog]
        WHERE (type=13 OR type=14) AND Content IS NOT NULL

        UPDATE [dbo].[Catalog]
        SET Content=null
        WHERE (type=13 OR type=14) AND Content IS NOT NULL

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('MoveExcelPowerBIContent') ;
    COMMIT
END
GO

-- Migrate PBI Data Sources
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] WHERE [DbVersion] = 'MigratePBIDataSources')
BEGIN
    BEGIN TRAN
        -- Inserting data sources into DataModelDataSource table
        -- DataSource Table:
            -- Credential Retrieval: 2 is Stored
            -- Flags: 4 is Impersonate, 8 is WindowsCredential
        INSERT INTO [dbo].[DataModelDataSource] (ItemId, DSType, DSKind, AuthType, ConnectionString, Username, Password, CreatedByID, CreatedDate, ModifiedByID, ModifiedDate, DataSourceID)
        SELECT 
            c.ItemId,
            'Live' AS DSType,
            'AnalysisServices' AS DSKind,
            CASE WHEN ds.CredentialRetrieval = 2 THEN
                CASE WHEN (ds.Flags & 4) <> 0 THEN
                    'Impersonate'
                ELSE
                    CASE WHEN (ds.Flags & 8) <> 0 THEN
                        'Windows'
                    ELSE
                        'UsernamePassword'
                    END
                END
            ELSE
                'Integrated'
            END AS AuthType,
            ds.ConnectionString,
            CASE WHEN ds.CredentialRetrieval = 2 THEN ds.Username ELSE NULL END AS Username,
            CASE WHEN ds.CredentialRetrieval = 2 THEN ds.Password ELSE NULL END AS Password,
            c.CreatedByID,
            c.CreationDate,
            c.ModifiedByID,
            c.ModifiedDate,
            ds.DSID AS DataSourceID
        FROM 
            [dbo].[DataSource] ds JOIN [dbo].[Catalog] c ON ds.ItemID = c.ItemID
        WHERE
            c.Type = 13 -- 13 is Power BI Report

        -- deleting data sources from old table
        DELETE FROM [dbo].[DataSource] WHERE ItemId IN (SELECT ItemID FROM [dbo].[Catalog] WHERE Type = 13)

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('MigratePBIDataSources') ;
    COMMIT
END
GO

-- Create table for storing subscription execution history
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'SubscriptionHistory')
BEGIN
    BEGIN TRAN
      CREATE TABLE [dbo].[SubscriptionHistory](
            [SubscriptionHistoryID] BIGINT IDENTITY(1,1) NOT NULL,
            [SubscriptionID] UNIQUEIDENTIFIER NOT NULL,
            [Type] TINYINT NULL,
            [StartTime] DATETIME NULL,
            [EndTime] DATETIME NULL,
            [Status] TINYINT NULL,
            [Message] NVARCHAR(1500) NULL,
            [Details] NVARCHAR(4000) NULL,
            CONSTRAINT [PK_SubscriptionHistory] PRIMARY KEY CLUSTERED 
            (
                [SubscriptionHistoryID] ASC
            ))

        ALTER TABLE [dbo].[SubscriptionHistory]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionHistory_Subscriptions] FOREIGN KEY([SubscriptionID])
        REFERENCES [dbo].[Subscriptions] ([SubscriptionID])

        ALTER TABLE [dbo].[SubscriptionHistory] CHECK CONSTRAINT [FK_SubscriptionHistory_Subscriptions]

        CREATE INDEX [IX_SubscriptionHistorySubscriptionID] ON [dbo].[SubscriptionHistory]([SubscriptionID])

        GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[SubscriptionHistory] TO RSExecRole
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('SubscriptionHistory') ;
    COMMIT
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'SubscriptionHistoryDelete')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[SubscriptionHistory] DROP CONSTRAINT [FK_SubscriptionHistory_Subscriptions]

        ALTER TABLE [dbo].[SubscriptionHistory]  WITH CHECK ADD  CONSTRAINT [FK_SubscriptionHistory_Subscriptions] FOREIGN KEY([SubscriptionID])
        REFERENCES [dbo].[Subscriptions] ([SubscriptionID])
        ON DELETE CASCADE

        ALTER TABLE [dbo].[SubscriptionHistory] CHECK CONSTRAINT [FK_SubscriptionHistory_Subscriptions]

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('SubscriptionHistoryDelete') ;
    COMMIT
END
GO

IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'UpdatePBIDefinition')
BEGIN
    BEGIN TRAN;
        WITH PbiReportDefinition
        AS  
        (  
            SELECT ItemId FROM CatalogItemExtendedContent WHERE ContentType = 'PowerBIReportDefinition' 
        ),
        PbiReportsWithoutDefinition AS
        (
            SELECT DS.ItemId FROM 
            DataModelDataSource DS 
            LEFT JOIN PbiReportDefinition ON (DS.ItemId=PbiReportDefinition.ItemId)
            WHERE PbiReportDefinition.ItemId IS NULL
        )
        INSERT INTO CatalogItemExtendedContent(ItemId, Content, ContentType, ModifiedDate)
            SELECT ItemId, Content, 'PowerBIReportDefinition' AS ContentType, ModifiedDate FROM CatalogItemExtendedContent
            WHERE ItemId IN (SELECT ItemId FROM PbiReportsWithoutDefinition)
            AND ContentType='CatalogItem'
            
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('UpdatePBIDefinition') ;
    COMMIT
END
GO

-- Changes in GetAllProperties stored procedure to also retrieve ParentID.
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'ParentIDGetAllProperties')
BEGIN
 INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('ParentIDGetAllProperties') ;
END
GO

-- Add cover index to avoid locking in listTask
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'IX_ScheduleForListTask')
BEGIN
    BEGIN TRAN;
        IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Schedule]') AND name = N'IX_ScheduleForListTask')
        BEGIN
            CREATE INDEX IX_ScheduleForListTask ON [Schedule]
            (
                [Type],
                [Path],
                [CreatedById]
            )
            INCLUDE
            (
                [Name],
                [StartDate],
                [Flags],
                [NextRunTime],
                [LastRunTime],
                [EndDate],
                [RecurrenceType],
                [MinutesInterval],
                [DaysInterval],
                [WeeksInterval],
                [DaysOfWeek],
                [DaysOfMonth],
                [Month],
                [MonthlyWeek],
                [State],
                [LastRunStatus],
                [ScheduledRunTimeout],
                [EventType],
                [EventData]
            )
        END
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('IX_ScheduleForListTask') ;
    COMMIT TRAN
END
GO

-- 3/9/2018 - Adding a new line for triggering the new SP [RemoveConfigurationInfoValue]

-- Changes in GetAllProperties stored procedure returns liked report properties from original.
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'GetAllPropertiesLinked')
BEGIN
 INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('GetAllPropertiesLinked') ;
END
GO

-- Create table for Power BI RLS (Role Level Security) roles.
if not exists (SELECT * FROM [dbo].[DBUpgradeHistory] WHERE [DbVersion] = 'PowerBIRowLevelSecurity')
BEGIN
    BEGIN TRAN
        -- DataModelRole table
        CREATE TABLE [dbo].[DataModelRole] (
            [DataModelRoleID] bigint identity(1,1) NOT NULL,
            [ItemID] uniqueidentifier NOT NULL,
            [ModelRoleID] uniqueidentifier NOT NULL,
            [ModelRoleName] NVARCHAR(255) NOT NULL
        ) ON [PRIMARY]

        GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[DataModelRole] TO RSExecRole

        ALTER TABLE [dbo].[DataModelRole] WITH NOCHECK ADD
            CONSTRAINT [PK_DataModelRole] PRIMARY KEY CLUSTERED (
                [DataModelRoleID]
            ) ON [PRIMARY],
            CONSTRAINT [FK_DataModelRole_Catalog] FOREIGN KEY (
                [ItemID]
            ) REFERENCES [dbo].[Catalog] (
                [ItemID]
            ) ON DELETE CASCADE

        CREATE INDEX [IX_DataModelRole_ItemID] ON [dbo].[DataModelRole]([ItemID]) ON [PRIMARY]

        -- UserDataModelRole
        CREATE TABLE [dbo].[UserDataModelRole] (
            [UserID] uniqueidentifier NOT NULL,
            [DataModelRoleID] bigint NOT NULL
        ) ON [PRIMARY]

        GRANT SELECT, UPDATE, INSERT, DELETE, REFERENCES ON [dbo].[UserDataModelRole] TO RSExecRole

            ALTER TABLE [dbo].[UserDataModelRole] WITH NOCHECK ADD
            CONSTRAINT [PK_UserDataModelRole] PRIMARY KEY CLUSTERED (
                [UserID],
                [DataModelRoleID]
            ) ON [PRIMARY],
            CONSTRAINT [FK_UserDataModelRole_Users] FOREIGN KEY (
                [UserID]
            ) REFERENCES [dbo].[Users] (
                [UserID]
            ) ON DELETE CASCADE,
            CONSTRAINT [FK_UserDataModelRole_DataModelRole] FOREIGN KEY (
                [DataModelRoleID]
            ) REFERENCES [dbo].[DataModelRole] (
                [DataModelRoleID]
            ) ON DELETE CASCADE

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('PowerBIRowLevelSecurity');
    COMMIT
END
GO

-- Added [GetCatalogExtendedContentLastUpdate] stored procedure 
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'ExtendedContentLastUpdate')
BEGIN
 INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('ExtendedContentLastUpdate') ;
END

-- Changes in GetAllProperties stored procedure returns liked report properties from original and override properties from linked
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'GetAllPropertiesLinked2')
BEGIN
 INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('GetAllPropertiesLinked2') ;
END
GO

-- Added ModifiedData as included column in the index to avoid being hit by lock 
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'ExtendedContentIndexWithDate')
BEGIN
    BEGIN TRAN
       DROP INDEX [IX_ItemId_CatalogItemExtendedContent] ON [dbo].[CatalogItemExtendedContent];

       CREATE INDEX [IX_ItemId_CatalogItemExtendedContent] ON [dbo].[CatalogItemExtendedContent]([ItemID],[ContentType]) INCLUDE([ModifiedDate]) ON [PRIMARY] ;
    COMMIT
END
GO

-- Added IX_ItemId_ModelConnectionName  and GetDataModelDataSourcesByItemIdAndModel stored proc
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'GetDMDSByItemIdModel')
BEGIN
    BEGIN TRAN
        CREATE INDEX [IX_ItemId_ModelConnectionName] ON [dbo].[DataModelDataSource]([ItemID],[ModelConnectionName]) ON [PRIMARY] ;
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('GetDMDSByItemIdModel') ;
    COMMIT
END
GO

-- Added Get and Update powerbi parameters stored procs
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'DataModelParameters')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('DataModelParameters') ;
    COMMIT
END
GO

-- Updated Stored proc GetItemProperties to return parameters
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'GetItemWithParameters')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('GetItemWithParameters') ;
    COMMIT
END
GO

-- Added Stored proc UpdateDataModelDataSourceConnectionName 
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'UpdateModelConnectionName')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('UpdateModelConnectionName') ;
    COMMIT
END
GO

-- Updated Stored proc UpdateDataModelDataSourceConnectionName to update connectionstring
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'UpdateModelConnectionStr')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('UpdateModelConnectionStr') ;
    COMMIT
END
GO

-- Converted mobile reports to resources
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'RemoveMobileReports')
BEGIN
    BEGIN TRAN
        -- Removed comments related to the mobile reports
        DELETE
        FROM [dbo].[Comments]
        FROM [dbo].[Comments] INNER JOIN 
        [dbo].[Catalog] ON [dbo].[Comments].ItemID = [dbo].[Catalog].ItemID
        WHERE [dbo].[Catalog].[Type] = 12 AND [dbo].[Comments].[ThreadID] IS NOT NULL

		DELETE
        FROM [dbo].[Comments]
        FROM [dbo].[Comments] INNER JOIN 
        [dbo].[Catalog] ON [dbo].[Comments].ItemID = [dbo].[Catalog].ItemID
        WHERE [dbo].[Catalog].[Type] = 12

        -- Removed files associated with deleted comments
		DELETE
        FROM AtachmentCommentResource
        FROM [dbo].[Catalog] AtachmentCommentResource INNER JOIN 
        [dbo].[Catalog] MobileReportItem ON AtachmentCommentResource.ParentID = MobileReportItem.ItemID
        WHERE MobileReportItem.[Type] = 12 AND AtachmentCommentResource.SubType IS NULL

        -- Updated entries of favorite mobile reports
        UPDATE Favorites
        SET [ItemID] = MobileReportFile.ItemID
        FROM [dbo].[Catalog] MobileReportElement 
		INNER Join [dbo].[Catalog] MobileReportFile ON MobileReportElement.ItemID = MobileReportFile.ParentID
		INNER Join [dbo].[Favorites] Favorites ON Favorites.ItemID = MobileReportElement.ItemID
        WHERE MobileReportElement.[Type] = 12 AND MobileReportFile.[Type] = 3
        
        -- Removed the link between mobile reports and date sets
        DELETE
        FROM [dbo].[DataSets]
        FROM [dbo].[DataSets] INNER JOIN 
        [dbo].[Catalog] ON [dbo].[DataSets].ItemID = [dbo].[Catalog].ItemID
        WHERE [dbo].[Catalog].[Type] = 12

        -- Removed the link between mobile reports and KPI
        DECLARE @xml xml
        SELECT @xml = cast([Property] as xml)
        FROM [dbo].[Catalog]
        WHERE [Type] = 11 AND CONVERT(xml, [Property]).value('(/Properties/DrillthroughTarget.Type)[1]', 'int') = 1

        IF @xml IS NOT NULL
		BEGIN
			SET @xml.modify('delete /Properties/DrillthroughTarget.Type')
			SET @xml.modify('delete /Properties/DrillthroughTarget.Id')
			SET @xml.modify('delete /Properties/DrillthroughTarget.Path')
			SET @xml.modify('delete /Properties/DrillthroughTarget.CatalogItemType')

			UPDATE [dbo].[Catalog]
			SET [Property] = CAST(CAST(@xml as VARCHAR(MAX)) as ntext) 
			WHERE [Type] = 11 AND CONVERT(xml, [Property]).value('(/Properties/DrillthroughTarget.Type)[1]', 'int') = 1
		END

        -- Removed resources from the contents folder in the mobile report
        DELETE FROM ContentsResources
        FROM [dbo].[Catalog] MobileReportElement INNER Join [dbo].[Catalog] ContentFolders ON MobileReportElement.ItemID = ContentFolders.ParentID
        INNER JOIN [dbo].[Catalog] ContentsResources ON ContentFolders.ItemID = ContentsResources.ParentID
        WHERE MobileReportElement.[Type] = 12 AND ContentFolders.[Type] = 1

        -- Removed contents folder
        DELETE FROM ContentFolders
        FROM [dbo].[Catalog] MobileReportElement INNER Join [dbo].[Catalog] ContentFolders ON MobileReportElement.ItemID = ContentFolders.ParentID
        WHERE MobileReportElement.[Type] = 12 AND ContentFolders.[Type] = 1

        -- Mobile report file with the resource type has replaced the mobile report element with the mobile report type
        UPDATE MobileReportFile
        SET [ParentID] = MobileReportElement.ParentID,
        [Path] = CONCAT(MobileReportElement.[Path], '.rsmobile'),
        [SubType] = NULL
        FROM [dbo].[Catalog] MobileReportElement INNER Join [dbo].[Catalog] MobileReportFile ON MobileReportElement.ItemID = MobileReportFile.ParentID
        WHERE MobileReportElement.[Type] = 12 AND MobileReportFile.[Type] = 3

        -- Removed the mobile report element with the mobile report type
        DELETE 
        FROM [dbo].[Catalog] 
        WHERE [Type] = 12

        -- Added mobile reports to allowed resources
        UPDATE [ConfigurationInfo]
        SET  [ConfigurationInfo].[Value] = CONCAT([ConfigurationInfo].[Value], ',*.rsmobile') 
        FROM [dbo].[ConfigurationInfo]
        WHERE [ConfigurationInfo].[Name] = 'AllowedResourceExtensionsForUpload' AND [ConfigurationInfo].[Value] NOT LIKE '%*.rsmobile%'

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('RemoveMobileReports') ;
    COMMIT
END
GO

-- Updated Stored proc 'CleanExpiredSessions' and 'CleanOrphanedSnapshots' to use clean up parameters instead of hardcoded values.
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] where [DbVersion] = 'UpdateCleanProcedures')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('UpdateCleanProcedures') ;
    COMMIT
END
GO
