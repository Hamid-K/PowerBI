/**************************************************************/
/* Copyright (c) Microsoft.  All rights reserved. */
/**************************************************************/
--------------------------------------------------
------------- Upgrade RS Temp DB
--------------------------------------------------

use ReportServerTempDB
GO

--- T.0.9.45 to 100 ---

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

--- 100 to 101 ---

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '101')
BEGIN
    if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetDBVersion]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
    drop procedure [dbo].[GetDBVersion];

    exec('
    CREATE PROCEDURE [dbo].[GetDBVersion]
    @DBVersion nvarchar(32) OUTPUT
    AS
    SET @DBVersion = (select top(1) [DbVersion]  from [dbo].[DBUpgradeHistory])')

    GRANT EXECUTE ON [dbo].[GetDBVersion] TO RSExecRole;

    if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '101')
    BEGIN
        INSERT into [dbo].[DBUpgradeHistory] ([DbVersion]) values ('101');
    END
END

--- 101 to 102 ---

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '102')
BEGIN
    if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '102')
    BEGIN
        INSERT into [dbo].[DBUpgradeHistory] ([DbVersion]) values ('102');
    END
END

--- 102 to 110 ---

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '110')
BEGIN
    BEGIN TRAN
        ALTER TABLE SessionData
        ALTER COLUMN ReportPath nvarchar(464) null

        ALTER TABLE SessionData
        ADD EditSessionID varchar(32) null

        CREATE INDEX [IX_EditSessionID] ON [dbo].[SessionData]([EditSessionID])

        ALTER TABLE ExecutionCache
        ADD LastUsedTime datetime not null default(GETDATE())

        ALTER TABLE SnapshotData
        DROP CONSTRAINT PK_SnapshotData

        CREATE CLUSTERED INDEX IX_SnapshotData
        ON [dbo].[SnapshotData] ([SnapshotDataID], [ParamsHash])

        CREATE INDEX [IX_CacheLookup] ON [dbo].[ExecutionCache] ([ReportID], [AbsoluteExpiration] desc) include ([SnapshotDataID])

        CREATE TABLE [dbo].[TempCatalog]
        (
            EditSessionID varchar(32) not null,
            TempCatalogID uniqueidentifier not null,
            ContextPath nvarchar(425) not null,  -- path to the catalog item
            [Name] nvarchar(425) not null,
            Content varbinary(max),  -- holds the current RDL
            Description nvarchar(max),
            Intermediate uniqueidentifier,  -- pointer to the compiled definition
            IntermediateIsPermanent bit not null default(0),
            Property nvarchar(max),
            Parameter nvarchar(max),
            OwnerID uniqueidentifier not null,
            CreationTime datetime not null,
            ExpirationTime datetime not null,
            DataCacheHash binary(64) null,
            constraint PK_TempCatalog primary key clustered (EditSessionID, ContextPath),
            constraint UNIQ_TempCatalogID unique (TempCatalogID)
        )

        CREATE INDEX [IX_Cleanup] ON [dbo].[TempCatalog] ([ExpirationTime]) ON [PRIMARY]

        GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[TempCatalog] TO RSExecRole

        CREATE TABLE [dbo].[TempDataSources] (
            [DSID] [uniqueidentifier] NOT NULL,
            -- reference to Catalog table if it is a standalone data source or data source embedded in rerport
            [ItemID] uniqueidentifier NOT NULL,
            -- reference to subscirption table if it is a subscription datasource
            [Name] [nvarchar] (260) NULL, -- only for scoped data sources, MUST be NULL for standalone!!!
            [Extension] [nvarchar] (260) NULL,
            [Link] [uniqueidentifier] NULL, -- if not null, we are referencing a shared data source
            [CredentialRetrieval] [int], -- Prompt = 1, Store = 2, Integrated = 3, None = 4
            [Prompt] [ntext],
            [ConnectionString] [image] NULL,
            [OriginalConnectionString] [image] NULL,
            [OriginalConnectStringExpressionBased] [bit] NULL,
            [UserName] [image],
            [Password] [image],
            [Flags] [int],
            [Version] [int] NOT NULL
        ) ON [PRIMARY]

        ALTER TABLE [dbo].[TempDataSources] WITH NOCHECK ADD
            CONSTRAINT [PK_DataSource] PRIMARY KEY CLUSTERED (
                [DSID]
            ) ON [PRIMARY],
            CONSTRAINT [FK_DataSourceItemID] FOREIGN KEY (
                [ItemID]
            ) REFERENCES [dbo].[TempCatalog] (
                [TempCatalogID]
            )

        CREATE INDEX [IX_DataSourceItemID] ON [dbo].[TempDataSources]([ItemID]) ON [PRIMARY]

        GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[TempDataSources] TO RSExecRole

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) values ('110');
    COMMIT
END

--- 110 to 111
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '111')
BEGIN
    BEGIN TRAN
        CREATE TABLE [dbo].[TempDataSets] (
            [ID] uniqueidentifier NOT NULL,
            -- reference to report in Catalog table
            [ItemID] uniqueidentifier NOT NULL,
            [LinkID] uniqueidentifier NULL,
            [Name] [nvarchar] (260) NOT NULL
        ) ON [PRIMARY]

        ALTER TABLE [dbo].[TempDataSets] WITH NOCHECK ADD
            CONSTRAINT [PK_DataSet] PRIMARY KEY CLUSTERED (
                [ItemID], [Name]
            ) ON [PRIMARY],
            CONSTRAINT [FK_DataSetItemID] FOREIGN KEY (
                [ItemID]
            ) REFERENCES [dbo].[TempCatalog] (
                [TempCatalogID]
            )

        CREATE INDEX [IX_DataSetLinkID] ON [dbo].[TempDataSets]([LinkID]) ON [PRIMARY]

        GRANT SELECT, INSERT, UPDATE, DELETE, REFERENCES ON [dbo].[TempDataSets] TO RSExecRole

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('111');
    COMMIT
END

--- 111 to 112 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '112')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('112');
    COMMIT
END

--- 112 to 113 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '113')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('113') ;
    COMMIT
END

--- 113 to 114 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '114')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('114') ;
    COMMIT
END

--- 114 to 115 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '115')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('115') ;
    COMMIT
END

--- 115 to 116 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '116')
BEGIN
    BEGIN TRAN
        DROP INDEX [IX_CacheLookup] ON [dbo].[ExecutionCache] ;
        ALTER TABLE [dbo].[ExecutionCache] ADD [ParamsHash] INT NOT NULL DEFAULT(0) ;

        EXEC('UPDATE [dbo].[ExecutionCache]
        SET [ParamsHash] = [SD].[ParamsHash]
        FROM [dbo].[ExecutionCache] [EC]
        JOIN [dbo].[SnapshotData] [SD] ON ([EC].[SnapshotDataID] = [SD].[SnapshotDataID]);') ;

        CREATE INDEX [IX_CacheLookup] ON [dbo].[ExecutionCache] (
            [ReportID],
            [ParamsHash],
            [AbsoluteExpiration] DESC )
            INCLUDE ( [SnapshotDataID] )

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('116') ;
    COMMIT
END

--- 116 to 117 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '117')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('117') ;
    COMMIT
END

--- 117 to 118 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '118')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[SessionData] ADD [DataSetInfo] varbinary(max) NULL;

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('118') ;
    COMMIT
END

--- 118 to 119 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '119')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('119') ;
    COMMIT
END

--- 119 to 120 ---
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '120')
BEGIN
    BEGIN TRAN
        ALTER TABLE SessionData
        ADD SitePath nvarchar (440) NULL,
            SiteZone int NOT NULL DEFAULT 0;

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) values ('120');
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '121')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('121') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '122')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('122') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '123')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('123') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '124')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('124') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '125')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('125') ;
    COMMIT
END


if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '126')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('126') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '127')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('127') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '129')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('129') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '140')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[TempDataSets] DROP CONSTRAINT PK_DataSet ;

        ALTER TABLE [dbo].[TempDataSets] WITH CHECK ADD
            CONSTRAINT [PK_TempDataSet] PRIMARY KEY NONCLUSTERED ( [ID] ) ;

        CREATE CLUSTERED INDEX [IX_TempDataSet_ItemID_Name] ON [dbo].[TempDataSets] (
            [ItemID], [Name] );

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('140') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '141')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[SessionData] ADD [ReportDefinitionPath] NVARCHAR(464) NULL

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('141') ;
    COMMIT
END
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '142')
BEGIN
    BEGIN TRAN
        ALTER TABLE SessionLock
        ADD LockVersion int NOT NULL DEFAULT 0;

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('142') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '146')
BEGIN
    BEGIN TRAN
        ALTER TABLE [dbo].[TempCatalog] ALTER COLUMN [DataCacheHash] VARBINARY(64) NULL

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('146') ;
    COMMIT
END


if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '147')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('147') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '148')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('148') ;
    COMMIT
END


if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '149')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('149') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '150')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('150') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '151')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('151') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '152')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('152') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '153')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('153') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '155')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('155') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '156')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('156') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '157')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('157') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '158')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('158') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '159')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('159') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '160')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('160') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '161')
BEGIN
    BEGIN TRAN
        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('161') ;
    COMMIT
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '162')
BEGIN
   INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('162') ;
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '163')
BEGIN
   INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('163') ;
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '164')
BEGIN
   INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('164') ;
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '165')
BEGIN
   INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('165') ;
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '166')
BEGIN
   INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('166') ;
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '167')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('167');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '168')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('168');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '169')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('169');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '170')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('170');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '171')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('171');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '172')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('172');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '173')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('173');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '174')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('174');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '175')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('175');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '180')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('180');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '200')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('200');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '210')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('210');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '220')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('220');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '225')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('225');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '230')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('230');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '235')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('235');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '236')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('236');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '240')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('240');
END

--- version bump for Content Cache (Note this feature might be ported to SQL_16_SP1) so need to account it exists in an older version
IF NOT EXISTS (SELECT * FROM [dbo].[DBUpgradeHistory] WHERE [DbVersion] = '245')
BEGIN
    BEGIN TRAN
        IF NOT EXISTS (SELECT * FROM  dbo.sysobjects where id = object_id(N'[dbo].[ContentCache]'))
        BEGIN
       CREATE TABLE [dbo].[ContentCache](
            [ContentCacheID] bigint identity(1,1) NOT NULL,
            [CatalogItemID] [uniqueidentifier] NOT NULL,
            [CreatedDate] [datetime] NOT NULL,
            [ParamsHash] [int] NULL,
            [EffectiveParams] [nvarchar](max) NULL,
            [ContentType] [nvarchar](256) NULL,
            [ExpirationDate] [datetime] NOT NULL,
            [Version] [smallint] NULL,
            [Content] [varbinary](max) NULL,
            CONSTRAINT [PK_ContentCache] PRIMARY KEY NONCLUSTERED ([ContentCacheID] ASC )
        ) ON [PRIMARY]

        CREATE INDEX [IX_ContentCache] ON [dbo].[ContentCache]
        (
            [CatalogItemID] ASC,
            [ParamsHash] ASC,
            [ContentType] ASC
        )

        CREATE INDEX [IX_ContentCache_ExpirationDate] ON [dbo].[ContentCache]
        (
            [ExpirationDate]
        )
        END

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('245');
    COMMIT
END

--- version bump for removing content cache when the cache is disabled
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '250')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('250');
END

--- version bump for update on GetChildrenBeforeDelete stored proc
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '255')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('255');
END

if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '260')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('260');
END

--- version bump for adding RequireInTune and EnableCustomVisuals properties
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '265')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('265');
END

--- version bump to introduce comments feature role permissions and updating the policies
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '270')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('270');
END

--- version bump to create ProductInfo table
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '271')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('271');
END

--- version bump to allow for ConfigurationInfo SiteName property to be defined by the server if the value is set to "Default"
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '272')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('272');
END

--- version bump to recreate ProductInfo table
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '274')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('274');
END

--- version bump to add TrustedFileFormat
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '275')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('275');
END

--- version bump for adding EnablePBIExportToCsvByDefault property
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = '276')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('276');
END

--- last version entry into the ServerUpgradeHistory table to let older RS versions (2016 and before) know this catalog has been upgraded
--- the table is not used by PBIRS and SSRS 2017
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'Post2016')
BEGIN
    INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('Post2016');
END

-- Add index to reduce likelyhood of deadlocks when refreshing KPI
if not exists (select * from [dbo].[DBUpgradeHistory] where [DbVersion] = 'ExecutionCacheLastUsed')
BEGIN
    BEGIN TRAN
        CREATE INDEX IX_ExecutionCacheLastUsed on [dbo].[ExecutionCache] (ReportID,AbsoluteExpiration,LastUsedTime,ExecutionCacheID)

        INSERT INTO [dbo].[DBUpgradeHistory] ([DbVersion]) VALUES ('ExecutionCacheLastUsed') ;
    COMMIT
END