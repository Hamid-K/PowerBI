using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine1.BuiltInStrings;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.OData;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.OAuth;

namespace Microsoft.Mashup.Engine1.Library.SharePoint
{
	// Token: 0x020003F8 RID: 1016
	internal sealed class SharePointModule : Module
	{
		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x00060B78 File Offset: 0x0005ED78
		public override string Name
		{
			get
			{
				return "SharePoint";
			}
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x00060B7F File Offset: 0x0005ED7F
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(3, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "SharePoint.Contents";
						case 1:
							return "SharePoint.Files";
						case 2:
							return "SharePoint.Tables";
						default:
							throw new InvalidOperationException(Strings.UnreachableCodePath);
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x00060BBA File Offset: 0x0005EDBA
		public override ResourceKindInfo[] DataSources
		{
			get
			{
				return new ResourceKindInfo[] { SharePointModule.resourceKindInfo };
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x00060BCC File Offset: 0x0005EDCC
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return new SharePointModule.ContentsFunctionValue(hostEnvironment);
				case 1:
					return new SharePointModule.FilesFunctionValue(hostEnvironment);
				case 2:
					return new SharePointModule.TablesFunctionValue(hostEnvironment);
				default:
					throw new InvalidOperationException(Strings.UnreachableCodePath);
				}
			});
		}

		// Token: 0x060022BB RID: 8891 RVA: 0x00060C00 File Offset: 0x0005EE00
		private static TableValue CreateTableValue(IEngineHost host, TextValue url, FileHelper.FolderOptions enumeration, Value options)
		{
			SharePointModule.ValidateUrl(url);
			SharePointUserSettings sharePointUserSettings = SharePointUserSettings.New(options, enumeration);
			SharePointUrlBuilder builder = new SharePointUrlBuilder(url.String, sharePointUserSettings.ApiVersion);
			IResource resource = Resource.New("SharePoint", builder.SiteUrl);
			ResourceCredentialCollection credentials;
			HttpServices.VerifyPermissionAndGetCredentials(host, resource, out credentials);
			TableValue tableValue;
			if (sharePointUserSettings.IsImplementationTwo)
			{
				tableValue = SharePointModule.CreateSharePointImpl2TableValue(host, resource, url.String, credentials, options);
			}
			else
			{
				SharePointApiVersion sharePointApiVersion = sharePointUserSettings.ApiVersion;
				if (sharePointUserSettings.ApiVersion == SharePointApiVersion.Auto)
				{
					sharePointApiVersion = SharePointServerVersion.GetServerVersion(host, builder.Url, credentials, enumeration == FileHelper.FolderOptions.EnumerateFilesDeep);
					builder = new SharePointUrlBuilder(url.String, sharePointApiVersion);
				}
				if (sharePointApiVersion == SharePointApiVersion.SP15)
				{
					try
					{
						tableValue = SharePointModule.CreateSharePoint15TableValue(host, resource, builder.Url, enumeration, credentials, sharePointApiVersion == sharePointUserSettings.ApiVersion);
						goto IL_01EC;
					}
					catch (ValueException ex)
					{
						Value value;
						if (sharePointUserSettings.ApiVersion == SharePointApiVersion.Auto && ex.Value.TryGetValue("Reason", out value) && value == ValueException.DataSourceNotFound)
						{
							builder = new SharePointUrlBuilder(url.String, SharePointApiVersion.SP14);
							tableValue = SharePointModule.CreateSharePoint14TableValue(host, builder, enumeration, credentials, false);
							goto IL_01EC;
						}
						throw ex;
					}
				}
				tableValue = new SharePointModule.TestConnectionTableWrapper(SharePointModule.CreateSharePoint14TableValue(host, builder, enumeration, credentials, sharePointApiVersion == sharePointUserSettings.ApiVersion), delegate
				{
					builder = new SharePointUrlBuilder(url.String, SharePointApiVersion.SP15);
					return SharePointModule.CreateSharePoint15TableValue(host, resource, builder.Url, enumeration, credentials, false);
				});
				IL_01EC:
				if ((enumeration & FileHelper.FolderOptions.EnumerateTables) == (FileHelper.FolderOptions)0)
				{
					tableValue = new SharePointModule.TestConnectionTableWrapper(tableValue, () => SharePointModule.CreateTableValue(host, url, FileHelper.FolderOptions.EnumerateTables, options));
				}
			}
			return tableValue;
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x00060E28 File Offset: 0x0005F028
		private static TableValue CreateSharePoint14TableValue(IEngineHost host, SharePointUrlBuilder builder, FileHelper.FolderOptions enumeration, ResourceCredentialCollection credentials, bool versionMatch)
		{
			SharePointEnvironment sharePointEnvironment = new SharePointEnvironment(host, credentials, enumeration);
			TableValue tableValue = SharePointTableValue.Create(builder, sharePointEnvironment);
			if (!versionMatch)
			{
				return BinaryOperator.AddMeta.Invoke(tableValue, SharePointModule.Version14Metadata).AsTable;
			}
			return tableValue;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x00060E64 File Offset: 0x0005F064
		private static TableValue CreateSharePoint15TableValue(IEngineHost host, IResource resource, string url, FileHelper.FolderOptions enumeration, ResourceCredentialCollection credentials, bool versionMatch)
		{
			string text = "{0}(\"{1}\")";
			IEngineHost engineHost = new SharePointModule.SharePointEngineHost(host, resource, credentials);
			TableValue tableValue;
			if (enumeration != FileHelper.FolderOptions.EnumerateFilesDeep)
			{
				if (enumeration != FileHelper.FolderOptions.EnumerateFoldersAndFiles)
				{
					if (enumeration != FileHelper.FolderOptions.EnumerateTables)
					{
						throw new InvalidOperationException();
					}
					tableValue = SharePointModule.EvaluateSP15(string.Format(CultureInfo.InvariantCulture, text, "SharePoint15.Tables", url), engineHost);
				}
				else
				{
					tableValue = SharePointModule.EvaluateSP15(string.Format(CultureInfo.InvariantCulture, text, "SharePoint15.Contents", url), engineHost);
				}
			}
			else
			{
				tableValue = SharePointModule.EvaluateSP15(string.Format(CultureInfo.InvariantCulture, text, "SharePoint15.Files", url), engineHost);
			}
			if (!versionMatch)
			{
				return BinaryOperator.AddMeta.Invoke(tableValue, SharePointModule.Version15Metadata).AsTable;
			}
			return tableValue;
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x00060EFC File Offset: 0x0005F0FC
		private static TableValue CreateSharePointImpl2TableValue(IEngineHost host, IResource resource, string url, ResourceCredentialCollection credentials, Value options)
		{
			string text = "{0}(\"{1}\", {2})";
			IEngineHost engineHost = new SharePointModule.SharePointEngineHost(host, resource, credentials);
			return SharePointModule.EvaluateSPImpl2(string.Format(CultureInfo.InvariantCulture, text, "SharePointImpl2.Tables", url, options.AsRecord.PrimitiveAndRecordToString(1)), engineHost);
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x00060F3C File Offset: 0x0005F13C
		private static RecordValue Version15Metadata
		{
			get
			{
				if (SharePointModule.version15Metadata == null)
				{
					SharePointModule.version15Metadata = RecordValue.New(Keys.New("ApiVersion"), new Value[] { NumberValue.New(15) });
				}
				return SharePointModule.version15Metadata;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x00060F6E File Offset: 0x0005F16E
		private static RecordValue Version14Metadata
		{
			get
			{
				if (SharePointModule.version14Metadata == null)
				{
					SharePointModule.version14Metadata = RecordValue.New(Keys.New("ApiVersion"), new Value[] { NumberValue.New(14) });
				}
				return SharePointModule.version14Metadata;
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x00060FA0 File Offset: 0x0005F1A0
		private static Module SharePoint15Module
		{
			get
			{
				if (SharePointModule.sharePoint15Module == null)
				{
					SharePointModule.sharePoint15Module = Modules.Compile("// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n\r\nsection SharePoint15;\r\n\r\nshared SharePoint15.Tables = (url as text) as table =>\r\n    let\r\n        minProps = { \"Id\", \"Title\", \"Items\" },\r\n        lists = OData.Feed(url & \"/lists?$select=\" & Text.Combine(minProps, \",\")),\r\n        listsTable = Table.SelectColumns(lists, minProps),\r\n        result = ToNavigationTable(listsTable, {\"Id\"}, \"Title\", \"Items\", false)\r\n    in\r\n        result;\r\n\r\n shared SharePoint15.Contents = (url as text) as table =>\r\n    let\r\n        webUrl = url & \"/Folders\",\r\n        source = OData.Feed(webUrl),\r\n        folders = CreateContent(source, url, true)\r\n    in\r\n        Value.ReplaceType(folders, ContentSchema(DateTimeZone.Type, null, Any.Type meta [Preview.Delay=\"Table\"]));\r\n\r\nshared SharePoint15.Files = (url as text) as table =>\r\n    let\r\n        webUrl = url & \"/lists/?$filter=\" & Uri.EscapeDataString(\"Hidden eq false and BaseType eq 1 and ItemCount gt 0 and IsCatalog eq false and IsApplicationList eq false\"),\r\n        source = OData.Feed(webUrl),\r\n        files = CreateFiles(source, url)\r\n    in\r\n        files;\r\n\r\n//\r\n// Transformations\r\n//\r\n\r\nContentSchemaWithZone = ContentSchema(DateTimeZone.Type);\r\n\r\nCreateContent = (table as table, siteUrl as text, isTable as logical) as table =>\r\n    let\r\n        withSiteUrl = Table.TransformRows(table, (r) => Record.AddField(r, \"SiteUrl\", siteUrl)),\r\n        transformation = if isTable then TransformToFolder else TransformToFile,\r\n        result = Table.FromRecords(List.Transform(withSiteUrl, transformation), ContentSchemaWithZone)\r\n    in\r\n        result;\r\n\r\nCreateFiles = (table as table, apiUrl as text) as table =>\r\n    let\r\n        tablesList = List.Transform(Table.ToRecords(table), each TransformToFiles(_, apiUrl)),\r\n        files = Table.Combine(tablesList),\r\n        result = if Table.ColumnCount(files) = 0 then files else Value.ReplaceType(files, ContentSchema(DateTime.Type, {\"Name\", \"Folder Path\"}, Binary.Type meta [Preview.Delay=\"Binary\"]) meta [Table.Folder = true])\r\n    in\r\n        result;\r\n\r\nContentSchema = (dateType as type, optional keyColumns as list, optional contentType as type) =>\r\n    let\r\n        keys = if keyColumns = null then {\"Name\"} else keyColumns,\r\n        content = if contentType <> null then contentType else Any.Type,\r\n        schema = type table\r\n            [\r\n                Content=content,\r\n                Name=text,\r\n                Extension=text,\r\n                Date accessed=dateType,\r\n                Date modified=dateType,\r\n                Date created=dateType,\r\n                Attributes=record,\r\n                Folder Path=text\r\n            ]\r\n    in\r\n        Type.AddTableKey(schema, keys, true) meta [NavigationTable.NameColumn = \"Name\", NavigationTable.DataColumn = \"Content\"];\r\n\r\nCreateFolderContent = (row as record) as table =>\r\n    let\r\n        siteUrl = row[SiteUrl],\r\n        serverRelativeUrl = row[ServerRelativeUrl],\r\n        getRows = () => CreateContent(row[Folders], siteUrl, true) & CreateContent(row[Files], siteUrl, false)\r\n    in\r\n        Table.View(null,\r\n            [\r\n                GetRows = getRows,\r\n                GetType = () => ContentSchemaWithZone,\r\n                OnInsertRows = (rowsToInsert) =>\r\n                    let\r\n                        currentRows = getRows(),\r\n                        namesInserted = List.Transform(Table.ToRecords(rowsToInsert), each [Name]),\r\n                        targetTable = Table.SelectRows(currentRows, each List.Contains(namesInserted, [Name], Comparer.OrdinalIgnoreCase))\r\n                    in\r\n                        Action.Sequence(CreateActionsForRows(siteUrl, serverRelativeUrl, ValidatedFolderContentToInsert(rowsToInsert)) & { Action.Return(targetTable) })\r\n            ]);\r\n\r\nValidatedFolderContentToInsert = (rowsToInsert as table) as table =>\r\n    let\r\n        ValidateRowName = (rowName as text) as logical =>\r\n            (rowName is text and rowName = Text.Trim(rowName) and rowName <> \"\" and (not Text.Contains(rowName, \"/\"))) or\r\n                (error \"Invalid name in row \" & rowName),\r\n        ValidateRowContent = (rowContent as any) as logical =>\r\n            ((rowContent is table and @ValidatedFolderContentToInsert(rowContent) <> null) or (rowContent is binary)) or\r\n                (error \"Invalid row content, should be either a table or binary.\"),\r\n        OneValidatedRowToInsert = (row as record) as logical =>\r\n            (ValidateRowName(row[Name]) and ValidateRowContent(row[Content])) or\r\n                (error \"Row is invalid for insert.\"),\r\n        ValidatedRowsToInsert = (rows as list) as logical =>\r\n            List.Accumulate(rows, true, (result, row) => result and OneValidatedRowToInsert(row)) or\r\n                (error \"Rows have invalid content for insert.\")\r\n    in\r\n        if Table.ColumnNames(rowsToInsert) = {\"Name\", \"Content\"} and ValidatedRowsToInsert(Table.ToRecords(rowsToInsert))\r\n            then rowsToInsert\r\n            else error \"Invalid column names for rows being inserted.\";\r\n\r\nCreateFilesContent = (files as list, tableType as type, authority as text) as table =>\r\n    let\r\n        withSiteUrl = List.Transform(files, (r) => Record.AddField(r, \"SiteUrl\", authority)),\r\n        results = Table.FromRecords(List.Transform(withSiteUrl, TransfromFromFileList), tableType)\r\n    in\r\n        results;\r\n\r\nAsDateTimeZone = (x) => if x is null then null else if x is datetime then DateTime.AddZone(x, 0, 0) else DateTimeZone.From(x);\r\n\r\nTransformToFolder = (row as record) as record =>\r\n    let\r\n        ParentFolder = GetParentFolder(row[ServerRelativeUrl]),\r\n        FolderPath = NormalizeFolderPath(GetUrlAuthority(row[SiteUrl]) & ParentFolder),\r\n        Result = [\r\n            Content = CreateFolderContent(row),\r\n            Name = row[Name],\r\n            Extension = \"\",\r\n            Date accessed = null,\r\n            Date modified = AsDateTimeZone(row[TimeLastModified]),\r\n            Date created = AsDateTimeZone(row[TimeCreated]),\r\n            Attributes = [Size = null, Content Type = null, Kind = \"Folder\"],\r\n            Folder Path = FolderPath]\r\n    in\r\n        Result;\r\n\r\nTransformToFile = (row as record) as record =>\r\n    let\r\n        FilePath = row[SiteUrl] & \"/getfilebyserverrelativeurl('\" & Text.Replace(row[ServerRelativeUrl], \"'\", \"''\") & \"')\",\r\n        FileContentPath = FilePath & \"/$value\",\r\n        FolderPath = GetFileDirectory(GetUrlAuthority(row[SiteUrl]), row[ServerRelativeUrl]),\r\n        Content = Web.Contents(FileContentPath),\r\n        temp = Record.AddField(\r\n            [Size = row[Length]],\r\n            \"Content Type\",\r\n            () => Value.Metadata(Content)[Content.Type],\r\n            true),\r\n        Attributes = Record.AddField(\r\n            temp,\r\n            \"Kind\",\r\n            () => GetFileKind(Value.Metadata(Content)[Content.Type]),\r\n            true),\r\n        Result = [\r\n            Content = Binary.View(null,\r\n                [\r\n                    GetStream = () => Content,\r\n                    OnReplace = (value) => UpdateFileContentAction(value, FilePath)\r\n                ]) meta Value.Metadata(Content),\r\n            Name = row[Name],\r\n            Extension = GetFileExtension(row[Name]),\r\n            Date accessed = null,\r\n            Date modified = AsDateTimeZone(row[TimeLastModified]),\r\n            Date created = AsDateTimeZone(row[TimeCreated]),\r\n            Attributes = Attributes,\r\n            Folder Path = FolderPath]\r\n    in\r\n        Result;\r\n\r\nTransfromFromFileList = (row as record) as record =>\r\n    let\r\n        FilePath = row[SiteUrl] & row[FileRef],\r\n        FolderPath = GetFileDirectory(row[SiteUrl], row[FileRef]),\r\n        Content = Web.Contents(FilePath),\r\n        temp = Record.AddField(\r\n            [Size = row[File_x0020_Size]],\r\n            \"Content Type\",\r\n            () => Value.Metadata(Content)[Content.Type],\r\n            true),\r\n        Attributes = Record.AddField(\r\n            temp,\r\n            \"Kind\",\r\n            () => GetFileKind(Value.Metadata(Content)[Content.Type]),\r\n            true),\r\n        extension = NormalizeExtension(row[FileLeafRef.Suffix]),\r\n        Result = [\r\n            Content = Content,\r\n            Name = row[FileLeafRef],\r\n            Extension = extension,\r\n            Date accessed = null,\r\n            Date modified = DateTime.From(Record.FieldOrDefault(row, \"Modified.\", row[Modified])),\r\n            Date created = DateTime.From(Record.FieldOrDefault(row, \"Created.\", row[Created])),\r\n            Attributes = Attributes,\r\n            Folder Path = FolderPath]\r\n    in\r\n        Result;\r\n\r\n//\r\n// New files and folders\r\n//\r\n\r\nCreateActionsForRows = (siteUrl as text, serverRelativeUrl as text, rowsToInsert as table) as list =>\r\n    List.Combine(\r\n        Table.TransformRows(rowsToInsert, (r) =>\r\n            if r[Content] is table\r\n                then CreateNewFolderAction(siteUrl, serverRelativeUrl, r)\r\n                else CreateNewFileAction(siteUrl, serverRelativeUrl, r)));\r\n\r\nCreateNewFileAction = (siteUrl as text, serverRelativeUrl as text, row as record) =>\r\n    let\r\n        NormalizedServerRelativeUrl = NormalizeFolderPath(serverRelativeUrl),\r\n        CreateFileReq = siteUrl & \"/getfolderbyserverrelativeurl('\" & Text.Replace(NormalizedServerRelativeUrl, \"'\", \"''\") & \"')/files/add(url='\" & Text.Replace(row[Name], \"'\", \"''\") & \"',overwrite=false)\",\r\n        Headers = [\r\n            #\"Accept\" = \"application/json;odata=minimalmetadata\"\r\n        ]\r\n    in\r\n        { WebAction.Request(WebMethod.Post, CreateFileReq, [Headers=Headers, Content=row[Content]]) };\r\n\r\nCreateNewFolderAction = (siteUrl as text, serverRelativeUrl as text, row as record) =>\r\n    let\r\n        NormalizedServerRelativeUrl = NormalizeFolderPath(serverRelativeUrl),\r\n        CreateFolderReq = siteUrl & \"/getfolderbyserverrelativeurl('\" & Text.Replace(NormalizedServerRelativeUrl, \"'\", \"''\") & \"')/folders/add('\" & Text.Replace(row[Name], \"'\", \"''\") & \"')\",\r\n        Headers = [\r\n            #\"Accept\" = \"application/json;odata=minimalmetadata\"\r\n        ]\r\n    in\r\n        { WebAction.Request(WebMethod.Post, CreateFolderReq, [Headers=Headers, Content=#binary({})]) }\r\n        & CreateActionsForRows(siteUrl, NormalizedServerRelativeUrl & row[Name], row[Content]? ?? []);\r\n\r\nChunkSize = 1024 * 1024 * 4; // 4MB\r\n\r\nUpdateFileContentAction = (value as binary, filePath as text) =>\r\n    let\r\n        uploadId = Text.NewGuid(),\r\n        getMetadataAction = WebAction.Request(WebMethod.Get, filePath, [Headers=[Accept=\"application/json\"]]),\r\n        startUploadAction = Action.Sequence({\r\n            getMetadataAction,\r\n            (metadata) =>\r\n                let\r\n                    etag = Json.Document(metadata)[ETag],\r\n                    reqUrl = filePath & \"/startupload(uploadId=guid'\" & uploadId & \"')\"\r\n                in\r\n                    Action.Sequence({\r\n                        WebAction.Request(WebMethod.Post, reqUrl, [Content=#binary({})]),\r\n                        Action.Return(etag)\r\n                    })\r\n        }),\r\n        cancelUploadUrl = filePath & \"/cancelupload(uploadId=guid'\" & uploadId & \"')\",\r\n        cancelUploadAction = Action.Sequence({\r\n            WebAction.Request(WebMethod.Post, cancelUploadUrl, [Content=#binary({})]),\r\n            Action.Return(error \"ETag did not match.\")\r\n        }),\r\n        continueUploadAction = (chunk, offset) =>\r\n            let\r\n                continueUploadUrl = filePath & \"/continueupload(uploadId=guid'\" & uploadId & \"',fileOffset=\" & Number.ToText(offset) & \")\"\r\n            in\r\n                WebAction.Request(WebMethod.Post, continueUploadUrl, [Content=chunk]),\r\n        finishUploadAction = (offset) =>\r\n            let\r\n                completeUploadUrl = filePath & \"/finishupload(uploadId=guid'\" & uploadId & \"',fileOffset=\" & Number.ToText(offset) & \")\"\r\n            in\r\n                WebAction.Request(WebMethod.Post, completeUploadUrl, [Content=#binary({})]),\r\n        uploadedValueAction = WebAction.Request(WebMethod.Get, filePath & \"/$value\")\r\n    in\r\n        Action.Sequence({\r\n            startUploadAction,\r\n            (etag) =>\r\n                let\r\n                    next = (offset) => Action.Sequence({\r\n                        getMetadataAction,\r\n                        (metadata) =>\r\n                            let\r\n                                currentEtag = Json.Document(metadata)[ETag]\r\n                            in\r\n                                if etag <> currentEtag\r\n                                    then cancelUploadAction\r\n                                    else Action.Return(Binary.Range(value, offset, ChunkSize)),\r\n                        (chunk) =>\r\n                            if Binary.Length(chunk) = 0\r\n                                then Action.Sequence({\r\n                                    finishUploadAction(offset),\r\n                                    uploadedValueAction\r\n                                })\r\n                                else Action.Sequence({\r\n                                    continueUploadAction(chunk, offset),\r\n                                    @next(offset + Binary.Length(chunk))\r\n                                })\r\n                    })\r\n                in\r\n                    next(0)\r\n        });\r\n\r\n//\r\n// Paging\r\n//\r\n\r\nSinglePageSize = 1000;\r\n\r\nGetAllPagesByNextLink = (url as text) as table =>\r\n    Table.GenerateByPage((previous) =>\r\n        let\r\n            // if previous is null, then this is our first page of data\r\n            nextLink = if null = previous then url else Value.Metadata(previous)[NextLink]?,\r\n            // if NextLink was set to null by the previous call, we know we have no more data\r\n            page = if null = nextLink then null else GetPage(nextLink)\r\n        in\r\n            page\r\n    );\r\n\r\nTable.GenerateByPage = (getNextPage as function, optional tableType as type) as table =>\r\n    let\r\n        listOfPages = List.Generate(\r\n            () => getNextPage(null),\r\n            (lastPage) => lastPage <> null,\r\n            (lastPage) => getNextPage(lastPage)\r\n        ),\r\n        tableOfPages = Table.FromList(listOfPages, Splitter.SplitByNothing(), {\"Column1\"}),\r\n        firstRow = tableOfPages{0}?,\r\n        keys = if tableType = null then Table.ColumnNames(firstRow[Column1])\r\n            else Record.FieldNames(Type.RecordFields(Type.TableRow(tableType))),\r\n        appliedType = if tableType = null then Value.Type(firstRow[Column1]) else tableType\r\n    in\r\n        if tableType = null and firstRow = null then\r\n            Table.FromRows({})\r\n        else\r\n            Value.ReplaceType(\r\n                Table.ExpandTableColumn(tableOfPages, \"Column1\", keys),\r\n                appliedType);\r\n\r\nGetFormDigest = (url as text) as text =>\r\n    let\r\n        contextUrl = Text.BeforeDelimiter(url, \"/_api/\") & \"/_api/contextinfo\",\r\n        // SharePoint 2013 will return xml instead of json unless we specify odata=verbose in Accept header\r\n        requestHeaders = [#\"Content-Type\" = \"application/json;odata=verbose\", Accept = \"application/json; odata=verbose\" ],\r\n        requestBody = Text.ToBinary(\"{}\"),\r\n        request = [Headers = requestHeaders, Content = requestBody],\r\n        contents = Web.Contents(contextUrl, request),\r\n        jsonResponse = Json.Document(contents),\r\n        contextinfo = \r\n        [\r\n            FormDigestValue = jsonResponse[d][GetContextWebInformation][FormDigestValue],\r\n            LibraryVersion = jsonResponse[d][GetContextWebInformation][LibraryVersion]\r\n        ],\r\n        digest = if Text.StartsWith(contextinfo[LibraryVersion], \"15.\") \r\n                 then error Error.Record(\"DataSource.Error\", UICulture.GetString((\"SharePointNotSupportedApi\")))\r\n                 else contextinfo[FormDigestValue]\r\n    in\r\n        digest;\r\n\r\nGetPage = (url as text) as table =>\r\n    let\r\n        digestHeader = if (Extension.CurrentCredential()[AuthenticationKind] = \"Windows\") then [#\"X-RequestDigest\" = GetFormDigest(url)] else [],\r\n        requestHeaders = [#\"Content-Type\" = \"application/json;odata=verbose\"] & digestHeader,\r\n        requestBody = Text.ToBinary(\"{'parameters': {'__metadata': { 'type': 'SP.RenderListDataParameters' }, 'ViewXml': '<View Scope=\"\"RecursiveAll\"\"><Query><Where><Eq><FieldRef Name=\"\"FSObjType\"\" /><Value Type=\"\"Integer\"\">0</Value></Eq></Where></Query><RowLimit Paged=\"\"TRUE\"\">\"& Number.ToText(SinglePageSize) &\"</RowLimit><ViewFields><FieldRef Name=\"\"FSObjType\"\" /><FieldRef Name=\"\"LinkFilename\"\" /><FieldRef Name=\"\"Modified\"\" /><FieldRef Name=\"\"BaseName\"\" /><FieldRef Name=\"\"FileSizeDisplay\"\" /><FieldRef Name=\"\"Created\"\" /><FieldRef Name=\"\"FileLeafRef.Suffix\"\" /><FieldRef Name=\"\"FileRef\"\" /></ViewFields></View>', 'RenderOptions': 2}}\"),\r\n        request = [Headers = requestHeaders, Content = requestBody],\r\n        contents = Web.Contents(url, request),\r\n        jsonResponse = Json.Document(contents),\r\n        nextLink = GetNextLink(url, jsonResponse),\r\n        tableType = ContentSchema(DateTime.Type, {\"Name\", \"Folder Path\"}, Binary.Type),\r\n        data = CreateFilesContent(jsonResponse[Row], tableType, GetUrlAuthority(url)),\r\n        dataWithNextLink = data meta [NextLink = nextLink]\r\n    in\r\n        dataWithNextLink;\r\n\r\n// Utiliy function which recreates uri from its parts. The opposite of Uri.Parts(...)\r\nUri.FromParts = (parts) =>\r\n    let\r\n        port = if (parts[Scheme] = \"https\" and parts[Port] = 443) or (parts[Scheme] = \"http\" and parts[Port] = 80) then \"\" else \":\" & Text.From(parts[Port]),\r\n        div1 = if Record.FieldCount(parts[Query]) > 0 then \"?\" else \"\",\r\n        div2 = if Text.Length(parts[Fragment]) > 0 then \"#\" else \"\",\r\n        uri = Text.Combine({parts[Scheme], \"://\", parts[Host], port, parts[Path], div1, Uri.BuildQueryString(parts[Query]), div2, parts[Fragment]})\r\n    in\r\n        uri;\r\n\r\n// the method does not perform any sanity checks.\r\n// It is assumed that the input nextHref will have an View=00000000-0000-0000-0000-000000000000 as part of query string\r\nRemoveNullGuidFromQueryParam = (nextHref as text) as text =>\r\n    let\r\n        uriParts = Uri.Parts(nextHref),\r\n        queryRecord = uriParts[Query],\r\n        withRemovedQueryFromUriParts = Record.RemoveFields(uriParts, \"Query\"),\r\n        withRemovedViewIdFromQuery = Record.RemoveFields(queryRecord, \"View\", MissingField.Ignore),\r\n        withQueryAddedToUriParts = Record.AddField(withRemovedQueryFromUriParts , \"Query\", withRemovedViewIdFromQuery),\r\n        result = Uri.FromParts(withQueryAddedToUriParts)\r\n    in\r\n        result ;\r\n\r\nGetNextLink = (url as text, response as record) as nullable text =>\r\n    let\r\n        // SharePoint returns NextHref as a query string to replace the last queried URL\r\n        nextHref = response[NextHref]?,\r\n        // Note: Null Guids dont work with pagination for pre-2016 SP versions, even though the NextUrl returned by the source has it.\r\n        // So remove it from original query string\r\n        viewGuidNull = \"View=00000000-0000-0000-0000-000000000000\",\r\n        urlWithoutQuery = Text.BeforeDelimiter(url, \"?\"),\r\n        nextLink = if null = nextHref then null else urlWithoutQuery & nextHref,\r\n        nextLinkWithRemovedNullGuid = try   // Adding a ty guard incase the parsing fails because SP sends a malformed uri, we should send the uri as is.\r\n            if nextLink <> null and Text.PositionOf(nextHref, viewGuidNull) >= 0 then RemoveNullGuidFromQueryParam(nextLink) else nextLink\r\n            otherwise nextLink\r\n    in\r\n        nextLinkWithRemovedNullGuid;\r\n\r\nTransformToFiles = (row as record, apiUrl as text) as table =>\r\n    let\r\n        id = row[Id],\r\n        url = apiUrl & \"/lists/getbyid('\" & id & \"')/RenderListDataAsStream\",\r\n        tableType = ContentSchema(DateTime.Type, {\"Name\", \"Folder Path\"}, Binary.Type meta [Preview.Delay=\"Binary\"]),\r\n        Result = Table.View(null,\r\n        [\r\n            GetRows = () => GetAllPagesByNextLink(url),\r\n            GetType = () => tableType\r\n        ])\r\n    in\r\n        Result;\r\n\r\nGetFileExtension = (fil[...string is too long...]");
					List<Module> list = new List<Module>();
					list.Add(Modules.All);
					list.Add(Modules.Extensibility);
					list.Add(Modules.Action);
					list.Add(new BuiltInStringsModule());
					list.Add(SharePointModule.SharePoint15Module);
					SharePointModule.sharePoint15Module = Linker.Link(list, delegate(IError entry)
					{
						throw new InvalidOperationException();
					}, LinkOptions.None);
				}
				return SharePointModule.sharePoint15Module;
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x00061030 File Offset: 0x0005F230
		private static Module SharePointImp2Module
		{
			get
			{
				if (SharePointModule.sharePointImpl2Module == null)
				{
					SharePointModule.sharePointImpl2Module = Modules.Compile("// Copyright (c) Microsoft Corporation.  All rights reserved.\r\n\r\nsection SharePointImpl2;\r\n\r\nshared SharePointImpl2.Tables = (url as text, optional options as record) as table =>\r\nlet\r\n    validatedOptions = ValidateOptions(ValidOptionsMap, options),\r\n    response = GetResponse(GetListsUrl(url), RequestHeaderGet),\r\n    valueList = response[value],\r\n    isListEmpty = List.IsEmpty(valueList),\r\n    valueTable = Table.FromList(valueList, Splitter.SplitByNothing(), null, null, ExtraValues.Error),\r\n    keyColumns = {\"Id\"},\r\n    listsTable = if (not isListEmpty) then Table.ExpandRecordColumn(valueTable, \"Column1\", Record.FieldNames(valueTable[Column1]{0})) else #table(keyColumns, {}),\r\n    normalizedUrl = NormalizeUrl(url),\r\n    uriParts = Uri.Parts(normalizedUrl),\r\n    uriPathComponent = if (uriParts[Path] = \"/\") then \"/\" else \":/\" & uriParts[Path] & \":/\",\r\n    v2UriPrefix = uriParts[Scheme] & \"://\" & uriParts[Host] & \"/_api/v2.0/sites/\" & uriParts[Host] & uriPathComponent & \"lists(guid'\",\r\n    v2UriSuffix =  \"')/items?$expand=fields&$stop=\" & Text.From(Constants[V2SinglePageSize]),\r\n    listsTableWithData = Table.AddColumn(listsTable, \"Items\", each PredicatePushDownView(NormalizeUrl([odata.id]), v2UriPrefix & [Id] & v2UriSuffix, validatedOptions[ViewMode], validatedOptions[DisableAppendNoteColumns])),\r\n    listsTableWithIsLeaf = Table.AddColumn(listsTableWithData, \"IsLeaf\", each true),\r\n    listsTableWithItemKind = Table.AddColumn(listsTableWithIsLeaf, \"ItemKind\", each \"Table\"),\r\n    listsTableWithItemName = Table.AddColumn(listsTableWithItemKind, \"ItemName\", each \"Table\"),\r\n    navTable = Table.ToNavigationTable(listsTableWithItemName, keyColumns, \"Title\", \"Items\", \"ItemKind\", \"ItemName\", \"IsLeaf\")\r\nin\r\n    // force evaluation to validate options\r\n    if (validatedOptions <> null) then navTable else navTable;\r\n\r\nPredicatePushDownView = (url as text, v2Url as text, viewMode as nullable text, DisableAppendNoteColumns as nullable logical) as table =>\r\nlet\r\n    // delta token consists of two required, and 1 optional pieces of information\r\n    // 1. 'LastChangeToken' from Glcst api, this can be roughly thought of as a timepoint that sharepoint maintains.\r\n    // 2. 'LastInsertedItemId' is the Id value of the last inserted list item.\r\n    // LastInsertedItemId helps to determine the newly inserted items when querying delta (newly inserted items will have their id values greater than this value).\r\n    // 3. 'SchemaChange' is optional. It is set only when a schema change is detected. In the initial tag this will never happen.\r\n    InitialDeltaTag =\r\n    let\r\n        invariantCulture = Constants[InvariantCulture],\r\n        requestBody = [\r\n            query = [\r\n                ChangeToken = \"\",\r\n                RowLimit = \"1\",\r\n                ViewFields = \"<ViewFields><FieldRef Name='ID' /></ViewFields>\",\r\n                Query = \"<Query><OrderBy><FieldRef Name='ID' Ascending='FALSE' /></OrderBy></Query>\"]],\r\n        response = GetResponse(url & \"/GetListItemChangesSinceToken\", null, requestBody, true),\r\n        normalizedResponse = NormalizeGlcstResponse(response),\r\n        changeTableAttributes = normalizedResponse{[ListItems.Name=\"Changes\"]}[ListItems.Attributes],\r\n        lastChangeToken = changeTableAttributes{[Name = \"LastChangeToken\"]}[Value],\r\n        changeSetCount = Number.FromText(normalizedResponse{[ListItems.Name=\"data\"]}[ListItems.Attributes]{[Name=\"ItemCount\"]}[Value], invariantCulture),\r\n        dataTable = normalizedResponse{[ListItems.Name=\"data\"]}[ListItems.Value],\r\n        attributesPrefixedNames = List.Transform(Table.ColumnNames(dataTable[Attributes]{0}), each \"Attributes.\" & _),\r\n        dataTableWithExpandedAttributes = Table.ExpandTableColumn(dataTable, \"Attributes\", Table.ColumnNames(dataTable[Attributes]{0}), attributesPrefixedNames),\r\n        lastInsertedItemId = if (changeSetCount = 0) then 0 else Number.FromText(dataTableWithExpandedAttributes{[Attributes.Name = \"ows_ID\"]}[Attributes.Value], invariantCulture),\r\n        deltaTag = [LastChangeToken = lastChangeToken, LastInsertedItemId = lastInsertedItemId]\r\n    in\r\n        deltaTag,\r\n\r\n    // builds the delta table for all the changes that has happened since the delta tag of lastDeltaTag.\r\n    // returns an empty delta table when lastDeltaTag is null.\r\n    // The method queries Glcst api for to fetch only the Id values of the items in the delta. Subsequently,\r\n    // it queries the Rlds api to fetch the actual data for these ids. This is done to maintain the data format consistency\r\n    // of the connector between full enumeration mode and delta mode.\r\n    GetDeltaTable = (state as record, lastDeltaTag as nullable record) as table =>\r\n    let\r\n        // Glcst api to get only the Ids and ChangeType for those Ids\r\n        changeSetIdsTable = Table.Pagination.Glcst(url, lastDeltaTag),\r\n        // Rlds api to get the actual data for the Ids.\r\n        changeSetValues = GetDeltaValues(state, changeSetIdsTable),\r\n        newDeltaTag = if (lastDeltaTag = null) then InitialDeltaTag else Value.Metadata(changeSetIdsTable)[Delta.Tag],\r\n        deltaTable = if (lastDeltaTag = null) then #table({\"Path\", \"Value\"}, {}) else changeSetValues\r\n    in\r\n        deltaTable meta [Delta.Tag = newDeltaTag],\r\n\r\n    // takes in the table of changed ids, and constructs the delta table for them\r\n    GetDeltaValues = (state as record, changeSetIds as table) as table =>\r\n    let\r\n        changeSetIdsForInsert = Table.SelectRows(changeSetIds, each [ChangeType] = \"INS\")[ID],\r\n        changeSetIdsForUpdate = Table.SelectRows(changeSetIds, each [ChangeType] = \"UPD\")[ID],\r\n        changeSetIdsForDelete = Table.SelectRows(changeSetIds, each [ChangeType] = \"DEL\")[ID],\r\n        emptyDeltaTable = #table({\"Path\", \"Value\"}, {}),\r\n        deltaTableWithInsert = if (List.Count(changeSetIdsForInsert) = 0) then emptyDeltaTable else GetDeltaValuesInsert(state, changeSetIdsForInsert),\r\n        deltaTableWithUpdate = if (List.Count(changeSetIdsForUpdate) = 0) then deltaTableWithInsert else Table.Combine({deltaTableWithInsert, GetDeltaValuesUpdate(state, changeSetIdsForUpdate)}),\r\n        deltaTableWithDelete = if (List.Count(changeSetIdsForDelete) = 0) then deltaTableWithUpdate else Table.Combine({deltaTableWithUpdate, GetDeltaValuesDelete(state, changeSetIdsForDelete)})\r\n    in\r\n        deltaTableWithDelete,\r\n\r\n    GetDeltaValuesInsert = (state as record, insertedIds as list) as table =>\r\n    let\r\n        schemaType = GetSchemaTypeFromState(state),\r\n        result = GetRldsData(state & [ChangeSetIds = insertedIds]),\r\n        displayNames = Record.FieldNames(state[DisplayNameToInternalName]),\r\n        internalNames = Record.FieldValues(state[DisplayNameToInternalName]),\r\n        resultWithDisplayName = Table.RenameColumns(result, List.Zip({internalNames, displayNames}), MissingField.Ignore),\r\n        deltaTable = Table.FromRecords(Table.TransformRows(resultWithDisplayName, (row) =>\r\n            let\r\n                path = {{}},\r\n                value = Table.FromRecords({row}, schemaType)\r\n            in\r\n                [\r\n                    Path = path,\r\n                    Value = value\r\n                ]),\r\n            type table [Path = any, Value = any])\r\n    in\r\n        deltaTable,\r\n\r\n    GetDeltaValuesUpdate = (state as record, updatedIds as list) as table =>\r\n    let\r\n        schemaType = GetSchemaTypeFromState(state),\r\n        result = GetRldsData(state & [ChangeSetIds = updatedIds]),\r\n        displayNames = Record.FieldNames(state[DisplayNameToInternalName]),\r\n        internalNames = Record.FieldValues(state[DisplayNameToInternalName]),\r\n        resultWithDisplayName = Table.RenameColumns(result, List.Zip({internalNames, displayNames}), MissingField.Ignore),\r\n        deltaTable = Table.FromRecords(Table.TransformRows(resultWithDisplayName, (row) =>\r\n            let\r\n                path = {{row[ID]}},\r\n                value = Table.FromRecords({row}, schemaType)\r\n            in\r\n                [\r\n                    Path = path,\r\n                    Value = value\r\n                ]),\r\n            type table [Path = any, Value = any])\r\n    in\r\n        deltaTable,\r\n\r\n    GetDeltaValuesDelete = (state as record, deletedIds as list) as table =>\r\n    let\r\n        schemaType = GetSchemaTypeFromState(state),\r\n        transformedDeletedIds = List.Transform(deletedIds, each {{_}}),\r\n        deltaTable = Table.AddColumn(Table.FromList(transformedDeletedIds, Splitter.SplitByNothing(), {\"Path\"}, null, null), \"Value\", each #table(schemaType, {}))\r\n    in\r\n        deltaTable,\r\n\r\n    GetCamlFilter = (changeSetIds as nullable list) as text =>\r\n    let\r\n        // RLDS CAML doesn't support more then 500 items inside <In>.\r\n        // Workaround is to wrap groups of values within <Or> tags. e.g.\r\n        // <Or>\r\n        //      <Or>\r\n        //          <In>\r\n        //              <FieldRef Name=\\\"ID\\\" />\r\n        //              <Values>\r\n        //                  <Value Type=\\\"Counter\\\">0</Value>\r\n        //                  ...\r\n        //                  <Value Type=\\\"Counter\\\">499</Value>\r\n        //              </Values>\r\n        //          </In>\r\n        //          <In>\r\n        //              <FieldRef Name=\\\"ID\\\" />\r\n        //              <Values>\r\n        //                  <Value Type=\\\"Counter\\\">500</Value>\r\n        //                  ...\r\n        //                  <Value Type=\\\"Counter\\\">999</Value>\r\n        //              </Values>\r\n        //          </In>\r\n        //      </Or>\r\n        //      <In>\r\n        //          <FieldRef Name=\\\"ID\\\" />\r\n        //          <Values>\r\n        //              <Value Type=\\\"Counter\\\">1000</Value>\r\n        //              ...\r\n        //              <Value Type=\\\"Counter\\\">N</Value>\r\n        //          </Values>\r\n        //      </In>\r\n        // </Or>\r\n        threshold = 500,\r\n        transformedList = List.Transform(changeSetIds, each \"<Value Type=\"\"Counter\"\">\" & Number.ToText(_) & \"</Value>\"),\r\n        splitList = List.Split(transformedList, threshold),\r\n        combinedElements = List.Transform(splitList, each \"<In><FieldRef Name=\"\"ID\"\" /><Values>\" & Text.Combine(_) & \"</Values></In>\"),\r\n        filterClause = \"<Where>\" & List.Accumulate(combinedElements, \"\", (state, page) => if (state = \"\") then page else \"<Or>\" & state & page & \"</Or>\") & \"</Where>\",\r\n        caml = if (changeSetIds <> null and List.Count(changeSetIds) > 0) then \"<Query>\" & filterClause & \"</Query>\" else \"\"\r\n    in\r\n        caml,\r\n\r\n    GetCamlSelect = (columns as list) as text =>\r\n    let\r\n        transformed =  List.Transform(columns, each \"<FieldRef Name=\"\"\" & _ & \"\"\" />\"),\r\n        caml = \"<ViewFields>\"& Text.Combine(transformed) &\"</ViewFields>\"\r\n    in\r\n        caml,\r\n\r\n    // utility method that fetches data from Rlds api.\r\n    // it is used both during full-list-enumeration (Rlds) and delta modes (Glcst).\r\n    GetRldsData = (state as record) as table =>\r\n    let\r\n        schemaTable = GetSchemaTableFromState(state),\r\n        displayNameToInternalNameRecord = state[DisplayNameToInternalName],\r\n        displayNames = Record.FieldNames(displayNameToInternalNameRecord),\r\n        internalNames = Record.FieldValues(displayNameToInternalNameRecord),\r\n        // change the display names to real field names before data is fetched.\r\n        finalSchemaTable = Table.RenameColumns(schemaTable, List.Zip({displayNames, internalNames}), MissingField.Ignore),\r\n        // state should contain caml qrueries for individal operations, like select, filter, except for RowLimit.\r\n        // The caml for RowLimit is constructed downstream at the pagination step.\r\n        camlSelect = GetCamlSelect(Table.ColumnNames(finalSchemaTable)),\r\n        camlFilter = GetCamlFilter(state[ChangeSetIds]?),\r\n        finalState = state & [SchemaTable = finalSchemaTable, SpoLocale = SpoLocale, CamlSelect = camlSelect, CamlFilter = camlFilter],\r\n        result = Table.Pagination.Rlds(url & \"/RenderListDataAsStream\", v2Url, finalState, DisableAppendNoteColumns ?? false),\r\n        // change the real field names back to display names.\r\n        resultWithDisplayName = Table.RenameColumns(result, List.Zip({internalNames, displayNames}), MissingField.Ignore)\r\n    in\r\n        resultWithDisplayName,\r\n\r\n    // The function returns a record having the schema table of the spo list, display name to real field name mapping, and spo site locale.\r\n    // When displaying table columns in PowerBI, column names are rendered as display name.\r\n    // When sending caml query to sharepoint, display names are translated to the mapped RealFieldName names.\r\n    //\r\n    // For any list column, RLDS metadata has four types of 'Name' fields. In most scenarios these names will be same.The only name that can be modified by the user\r\n    // is the display name.\r\n    // 1. StaticName: This name can be ignored. This exists in the api for historical reasons.\r\n    // 2. RealFieldName: RealfieldName can be thought of as the actual underlying column that stores the data for other computed type columns to use. The computed columns depend\r\n    // on these columns for their actual data. For e.g. 'Title' column. There are three separately named columns 'LinkTitle', 'Title' 'LinkTitleNoMenu'.\r\n    // They point to the same RealFieldName, Title, and render the underlying data differently.\r\n    // 3. Name: Name is the internal name of the column. This is unique for every column within a list.\r\n    // 4. DisplayName: Display name is the column name that is displayed to the user. Multiple columns within a list can have the same display name. SPO's Web UI does not allow\r\n    // creation of multiple columns with same display name, but it can be done using SharePoint's APIs directly.\r\n    SpoListMetadata = \r\n    let\r\n        attachmentsFacet = Type.ReplaceFacets(Text.Type, Type.Facets(Text.Type) & [NativeTypeName=\"Attachments\"]),\r\n        choiceFacet = Type.ReplaceFacets(Text.Type, Type.Facets(Text.Type) & [NativeTypeName=\"Choice\"]),\r\n        computedFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"Computed\"]),\r\n        currencyFacet = Type.ReplaceFacets(Currency.Type, Type.Facets(Currency.Type) & [NativeTypeName=\"Currency\"]),\r\n        dateTimeFacet = Type.ReplaceFacets(DateTime.Type, Type.Facets(DateTime.Type) & [NativeTypeName=\"DateTime\"]),\r\n        geoLocationFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"GeoLocation\"]),\r\n        int64Facet = Type.ReplaceFacets(Int64.Type, Type.Facets(Int64.Type) & [NativeTypeName=\"Int64\"]),\r\n        locationFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"Location\"]),\r\n        logicalFacet = Type.ReplaceFacets(Logical.Type, Type.Facets(Logical.Type) & [NativeTypeName=\"Logical\"]),\r\n        lookupFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"Lookup\"]),\r\n        lookupmultiFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"LookupMulti\"]),\r\n        multichoiceFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"MultiChoice\"]),\r\n        noteFacet = Type.ReplaceFacets(Text.Type, Type.Facets(Text.Type) & [NativeTypeName=\"Note\"]),\r\n        numberFacet = Type.ReplaceFacets(Number.Type, Type.Facets(Number.Type) & [NativeTypeName=\"Number\"]),\r\n        taxonomyfieldtypeFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"TaxonomyFieldType\"]),\r\n        taxonomyfieldtypemultiFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"TaxonomyFieldTypeMulti\"]),\r\n        textFacet = Type.ReplaceFacets(Text.Type, Type.Facets(Text.Type) & [NativeTypeName=\"Text\"]),\r\n        thumbnailFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"Thumbnail\"]),\r\n        urlFacet = Type.ReplaceFacets(Uri.Type, Type.Facets(Uri.Type) & [NativeTypeName=\"Url\"]),\r\n        userFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"User\"]),\r\n        userMultiFacet = Type.ReplaceFacets(Any.Type, Type.Facets(Any.Type) & [NativeTypeName=\"UserMulti\"]),\r\n        TypeMap = [\r\n            attachments = type nullable attachmentsFacet,\r\n            boolean = type nullable logicalFacet,\r\n            choice = type nullable choiceFacet,\r\n            computed = type nullable computedFacet,\r\n            counter = type nullable int64Facet,\r\n            currency = type nullable currencyFacet,\r\n            datetime = type nullable dateTimeFacet,\r\n            geolocation = type nullable geoLocationFacet,\r\n            location = type nullable locationFacet,\r\n            lookup = type nullable lookupFacet,\r\n            lookupmulti = type nullable lookupmultiFacet,\r\n            multichoice = type nullable multichoiceFacet,\r\n            note = type nullable noteFacet,\r\n            number = type nullable numberFacet,\r\n            taxonomyfieldtype = type nullable taxonomyfieldtypeFacet,\r\n            taxonomyfieldtypemulti = type nullable taxonomyfieldtypemultiFacet,\r\n            text = type nullable textFacet,\r\n            thumbnail = type nullable thumbnailFacet,\r\n            url = type nullable urlFacet,\r\n            user = type nullable userFacet,\r\n            usermulti = type nullable userMultiFacet],\r\n        requestBody = [\r\n            parameters = [\r\n                AddAllFields = if (viewMode = \"Default\") then \"FALSE\" else \"TRUE\",\r\n                RenderOptions = 5707527]],\r\n        source = GetResponse(url & \"/RenderListDataAsStream\", null, requestBody),\r\n        spoLocale = source[ListSchema][Locale],\r\n        fieldlist = source[ListSchema][Field],\r\n        removeBadFields = List.Select(fieldlist, (x) => Record.HasFields(x, {\"RealFieldName\"})),\r\n        basetable =  Table.FromList(removeBadFields, Splitter.SplitByNothing(), null, null, ExtraValues.Error),\r\n        tableWithExpandedColumn = Table.ExpandRecordColumn(basetable, \"Column1\", {\"RealFieldName\", \"role\", \"DisplayName\"}),\r\n        // Remove multiple computed fields. This is in accordance with older spo connnector. Computed types are not the same as calculated types.\r\n        tableWithDistinctRealName = Table.Distinct(tableWithExpandedColumn, \"RealFieldName\"),\r\n        // ID column will be added, so we need to account for it when cleaning up duplicate display names\r\n        isIdColumnPresent = Table.Contains(tableWithDistinctRealName, [RealFieldName=\"ID\"]),\r\n        // Remove duplicate field names by replacing display names with RealFieldNames (which are always unique)\r\n        removeDuplicateFields = (fieldTable as table, idColumnPresent as logical) as table =>\r\n            let\r\n                countFields = Table.AddColumn(fieldTable, \"NameCount\", (row) => Table.RowCount(Table.SelectRows(fieldTable, each [DisplayName] = row[DisplayName]))),\r\n                addDisplayNames = Table.AddColumn(countFields, \"DisplayName1\", each if ([NameCount] = 1 and (idColumnPresent or not ([DisplayName] = \"ID\")) and [DisplayName] <> null and Text.Trim([DisplayName]) <> \"\") then [DisplayName] else [RealFieldName]),\r\n                RemoveOldNames = Table.RemoveColumns(addDisplayNames, {\"DisplayName\", \"NameCount\"}),\r\n                RenameColumn = Table.RenameColumns(RemoveOldNames, {{\"DisplayName1\", \"DisplayName\"}})\r\n            in\r\n                RenameColumn,\r\n        removeDuplicateFieldsCycle = (fieldTable as table, idColumnPresent as logical) as table =>\r\n            let\r\n                cycleTable = removeDuplicateFields(fieldTable, idColumnPresent)\r\n            in\r\n                if (Table.MatchesAllRows(cycleTable, each [DisplayName] = fieldTable{[RealFieldName=[RealFieldName]]}[DisplayName])) then cycleTable else @removeDuplicateFieldsCycle(cycleTable, idColumnPresent),\r\n        \r\n        tableWithActualDisplayNames = removeDuplicateFieldsCycle(tableWithDistinctRealName, isIdColumnPresent),\r\n        tableWithRenamedColumns = Table.RenameColumns(tableWithActualDisplayNames, {{\"RealFieldName\", \"Value\"}, {\"role\", \"SpType\"}, {\"DisplayName\", \"N[...string is too long...]");
					List<Module> list = new List<Module>();
					list.Add(Modules.All);
					list.Add(Modules.Extensibility);
					list.Add(new BuiltInStringsModule());
					list.Add(SharePointModule.SharePointImp2Module);
					SharePointModule.sharePointImpl2Module = Linker.Link(list, delegate(IError entry)
					{
						throw new InvalidOperationException();
					}, LinkOptions.None);
				}
				return SharePointModule.sharePointImpl2Module;
			}
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000610B3 File Offset: 0x0005F2B3
		private static TableValue EvaluateSP15(string m, IEngineHost host)
		{
			return LanguageLibrary.Evaluate(m, RecordValue.Empty, host, new Module[] { SharePointModule.SharePoint15Module }).AsTable;
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000610D4 File Offset: 0x0005F2D4
		private static TableValue EvaluateSPImpl2(string m, IEngineHost host)
		{
			return LanguageLibrary.Evaluate(m, RecordValue.Empty, host, new Module[] { SharePointModule.SharePointImp2Module }).AsTable;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000610F8 File Offset: 0x0005F2F8
		private static void ValidateUrl(TextValue url)
		{
			Uri uri = ODataUriCommon.ConvertToUri(url);
			if (!string.IsNullOrEmpty(uri.Query) || !string.IsNullOrEmpty(uri.Fragment))
			{
				throw ValueException.NewDataFormatError<Message0>(Strings.SharePointInvalidUrl, url, null);
			}
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x00061134 File Offset: 0x0005F334
		private static bool TryGetLocation(IExpression expression, ref IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
		{
			Value[] argumentValues = ExpressionAnalysis.GetArgumentValues(expression);
			if (argumentValues != null && argumentValues.Length != 0 && argumentValues[0].IsText)
			{
				location.Address = new Dictionary<string, object> { 
				{
					"url",
					new SharePointUrlBuilder(argumentValues[0].AsString, SharePointApiVersion.SP14).SiteUrl
				} };
				Value value = ((argumentValues.Length > 1) ? argumentValues[1] : Value.Null);
				if (value.IsRecord)
				{
					foundOptions = ExpressionAnalysis.RemovePlaceholders(value.AsRecord, out unknownOptions);
				}
				else if (value.IsNull)
				{
					foundOptions = RecordValue.Empty;
					unknownOptions = Keys.Empty;
				}
				else
				{
					foundOptions = null;
					unknownOptions = null;
				}
				return true;
			}
			location = null;
			foundOptions = null;
			unknownOptions = null;
			return false;
		}

		// Token: 0x04000DC2 RID: 3522
		private static Module sharePoint15Module;

		// Token: 0x04000DC3 RID: 3523
		private static Module sharePointImpl2Module;

		// Token: 0x04000DC4 RID: 3524
		private static RecordValue version15Metadata;

		// Token: 0x04000DC5 RID: 3525
		private static RecordValue version14Metadata;

		// Token: 0x04000DC6 RID: 3526
		public const string ContentsFunctionName = "SharePoint.Contents";

		// Token: 0x04000DC7 RID: 3527
		public const string FilesFunctionName = "SharePoint.Files";

		// Token: 0x04000DC8 RID: 3528
		public const string TablesFunctionName = "SharePoint.Tables";

		// Token: 0x04000DC9 RID: 3529
		internal static readonly OptionRecordDefinition FilesOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("ApiVersion", TypeValue.Any, NumberValue.New(14), OptionItemOption.None, null, "SharePoint"),
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, LogicalValue.False, OptionItemOption.ForDsrRoundTripOnly, null, null)
		});

		// Token: 0x04000DCA RID: 3530
		internal static readonly OptionRecordDefinition TablesOptionRecord = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("ApiVersion", TypeValue.Any, NumberValue.New(14), OptionItemOption.None, null, "SharePoint"),
			new OptionItem("Implementation", NullableTypeValue.Text, null, OptionItemOption.None, null, "SharePoint"),
			new OptionItem("ViewMode", TypeValue.Text, TextValue.New("All"), OptionItemOption.None, null, "SharePoint"),
			new OptionItem("DisableAppendNoteColumns", NullableTypeValue.Logical, null, OptionItemOption.None, null, "SharePoint")
		});

		// Token: 0x04000DCB RID: 3531
		private static readonly ResourceKindInfo resourceKindInfo = new UriResourceKindInfo("SharePoint", null, new AuthenticationInfo[]
		{
			ResourceHelpers.AnonymousAuth,
			ResourceHelpers.WindowsAuthAlternateCredentials,
			new AadAuthenticationInfo
			{
				ClientApplicationType = OAuthClientApplicationType.Optional,
				ProviderFactory = new OAuthFactory((OAuthServices services, string url) => AadOAuthProvider.CreateResourceForUrl(services, url, null), (OAuthServices services, OAuthClientApplication app, string url) => new AadOAuthProvider(services, app, url, null))
			}
		}, null, false, false, false, null, new DataSourceLocationFactory[]
		{
			SharePointFolderDataSourceLocation.Factory,
			SharePointListDataSourceLocation.Factory
		});

		// Token: 0x04000DCC RID: 3532
		private Keys exportKeys;

		// Token: 0x020003F9 RID: 1017
		private enum Exports
		{
			// Token: 0x04000DCE RID: 3534
			Contents,
			// Token: 0x04000DCF RID: 3535
			Files,
			// Token: 0x04000DD0 RID: 3536
			Tables,
			// Token: 0x04000DD1 RID: 3537
			Count
		}

		// Token: 0x020003FA RID: 1018
		private sealed class ContentsFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x060022C9 RID: 8905 RVA: 0x00061335 File Offset: 0x0005F535
			public ContentsFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles), 1, "url", TypeValue.Text, "options", SharePointModule.ContentsFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x060022CA RID: 8906 RVA: 0x0006135F File Offset: 0x0005F55F
			public override TableValue TypedInvoke(TextValue url, Value options)
			{
				return SharePointModule.CreateTableValue(this.host, url, FileHelper.FolderOptions.EnumerateFoldersAndFiles, options);
			}

			// Token: 0x17000E92 RID: 3730
			// (get) Token: 0x060022CB RID: 8907 RVA: 0x00060B78 File Offset: 0x0005ED78
			public override string PrimaryResourceKind
			{
				get
				{
					return "SharePoint";
				}
			}

			// Token: 0x060022CC RID: 8908 RVA: 0x00061370 File Offset: 0x0005F570
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				location = new SharePointFolderDataSourceLocation();
				bool flag = SharePointModule.TryGetLocation(expression, ref location, out foundOptions, out unknownOptions);
				if (foundOptions != null)
				{
					foundOptions = foundOptions.Concatenate(RecordValue.New(Keys.New("HierarchicalNavigation"), new Value[] { LogicalValue.True })).AsRecord;
				}
				return flag;
			}

			// Token: 0x04000DD2 RID: 3538
			private static readonly TypeValue optionsType = SharePointModule.FilesOptionRecord.CreateRecordType().Nullable;

			// Token: 0x04000DD3 RID: 3539
			private readonly IEngineHost host;
		}

		// Token: 0x020003FB RID: 1019
		private sealed class FilesFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x060022CE RID: 8910 RVA: 0x000613D5 File Offset: 0x0005F5D5
			public FilesFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFilesDeep), 1, "url", TypeValue.Text, "options", SharePointModule.FilesFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x060022CF RID: 8911 RVA: 0x000613FF File Offset: 0x0005F5FF
			public override TableValue TypedInvoke(TextValue url, Value options)
			{
				return SharePointModule.CreateTableValue(this.host, url, FileHelper.FolderOptions.EnumerateFilesDeep, options);
			}

			// Token: 0x17000E93 RID: 3731
			// (get) Token: 0x060022D0 RID: 8912 RVA: 0x00060B78 File Offset: 0x0005ED78
			public override string PrimaryResourceKind
			{
				get
				{
					return "SharePoint";
				}
			}

			// Token: 0x060022D1 RID: 8913 RVA: 0x00061410 File Offset: 0x0005F610
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				location = new SharePointFolderDataSourceLocation();
				bool flag = SharePointModule.TryGetLocation(expression, ref location, out foundOptions, out unknownOptions);
				if (foundOptions != null)
				{
					foundOptions = foundOptions.Concatenate(RecordValue.New(Keys.New("HierarchicalNavigation"), new Value[] { LogicalValue.False })).AsRecord;
				}
				return flag;
			}

			// Token: 0x04000DD4 RID: 3540
			private static readonly TypeValue optionsType = SharePointModule.FilesOptionRecord.CreateRecordType().Nullable;

			// Token: 0x04000DD5 RID: 3541
			private readonly IEngineHost host;
		}

		// Token: 0x020003FC RID: 1020
		private sealed class TablesFunctionValue : NativeFunctionValue2<TableValue, TextValue, Value>
		{
			// Token: 0x060022D3 RID: 8915 RVA: 0x00061475 File Offset: 0x0005F675
			public TablesFunctionValue(IEngineHost host)
				: base(TypeValue.Table, 1, "url", TypeValue.Text, "options", SharePointModule.TablesFunctionValue.optionsType)
			{
				this.host = host;
			}

			// Token: 0x060022D4 RID: 8916 RVA: 0x0006149E File Offset: 0x0005F69E
			public override TableValue TypedInvoke(TextValue url, Value options)
			{
				return SharePointModule.CreateTableValue(this.host, url, FileHelper.FolderOptions.EnumerateTables, options);
			}

			// Token: 0x17000E94 RID: 3732
			// (get) Token: 0x060022D5 RID: 8917 RVA: 0x00060B78 File Offset: 0x0005ED78
			public override string PrimaryResourceKind
			{
				get
				{
					return "SharePoint";
				}
			}

			// Token: 0x060022D6 RID: 8918 RVA: 0x000614AE File Offset: 0x0005F6AE
			public override bool TryGetLocation(IExpression expression, out IDataSourceLocation location, out RecordValue foundOptions, out Keys unknownOptions)
			{
				location = new SharePointListDataSourceLocation();
				return SharePointModule.TryGetLocation(expression, ref location, out foundOptions, out unknownOptions);
			}

			// Token: 0x04000DD6 RID: 3542
			private static readonly TypeValue optionsType = SharePointModule.TablesOptionRecord.CreateRecordType().Nullable;

			// Token: 0x04000DD7 RID: 3543
			private readonly IEngineHost host;
		}

		// Token: 0x020003FD RID: 1021
		private class TestConnectionTableWrapper : WrappingTableValue
		{
			// Token: 0x060022D8 RID: 8920 RVA: 0x000614D7 File Offset: 0x0005F6D7
			public TestConnectionTableWrapper(TableValue table, Func<TableValue> fallback)
				: base(table)
			{
				this.fallbackHandler = fallback;
			}

			// Token: 0x060022D9 RID: 8921 RVA: 0x000614E7 File Offset: 0x0005F6E7
			protected override TableValue New(TableValue table)
			{
				return new SharePointModule.TestConnectionTableWrapper(table, null);
			}

			// Token: 0x060022DA RID: 8922 RVA: 0x000614F0 File Offset: 0x0005F6F0
			public override void TestConnection()
			{
				try
				{
					base.TestConnection();
				}
				catch (ValueException)
				{
					if (this.fallbackHandler != null)
					{
						this.fallbackHandler().TestConnection();
					}
				}
			}

			// Token: 0x04000DD8 RID: 3544
			private readonly Func<TableValue> fallbackHandler;
		}

		// Token: 0x020003FE RID: 1022
		private sealed class SharePointEngineHost : IEngineHost
		{
			// Token: 0x060022DB RID: 8923 RVA: 0x00061530 File Offset: 0x0005F730
			public SharePointEngineHost(IEngineHost host, IResource resource, ResourceCredentialCollection credentials)
			{
				this.host = host;
				this.services = new Dictionary<Type, object> { 
				{
					typeof(IExtensibilityService),
					new SharePointModule.SharePointExtensibilityService(credentials, resource)
				} };
			}

			// Token: 0x060022DC RID: 8924 RVA: 0x00061564 File Offset: 0x0005F764
			public T QueryService<T>() where T : class
			{
				object obj;
				if (this.services.TryGetValue(typeof(T), out obj))
				{
					return (T)((object)obj);
				}
				return this.host.QueryService<T>();
			}

			// Token: 0x04000DD9 RID: 3545
			private readonly IEngineHost host;

			// Token: 0x04000DDA RID: 3546
			private readonly Dictionary<Type, object> services;
		}

		// Token: 0x020003FF RID: 1023
		private sealed class SharePointExtensibilityService : IExtensibilityService
		{
			// Token: 0x060022DD RID: 8925 RVA: 0x0006159C File Offset: 0x0005F79C
			public SharePointExtensibilityService(ResourceCredentialCollection credentials, IResource resource)
			{
				this.credentials = credentials;
				this.resource = resource;
			}

			// Token: 0x17000E95 RID: 3733
			// (get) Token: 0x060022DE RID: 8926 RVA: 0x000615B2 File Offset: 0x0005F7B2
			public IResource CurrentResource
			{
				get
				{
					return this.resource;
				}
			}

			// Token: 0x17000E96 RID: 3734
			// (get) Token: 0x060022DF RID: 8927 RVA: 0x000615BA File Offset: 0x0005F7BA
			public ResourceCredentialCollection CurrentCredentials
			{
				get
				{
					return this.credentials;
				}
			}

			// Token: 0x17000E97 RID: 3735
			// (get) Token: 0x060022E0 RID: 8928 RVA: 0x00002139 File Offset: 0x00000339
			public bool ImpersonateResource
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060022E1 RID: 8929 RVA: 0x0000336E File Offset: 0x0000156E
			public void RefreshCredential(bool forceRefresh)
			{
			}

			// Token: 0x04000DDB RID: 3547
			private readonly ResourceCredentialCollection credentials;

			// Token: 0x04000DDC RID: 3548
			private readonly IResource resource;
		}
	}
}
