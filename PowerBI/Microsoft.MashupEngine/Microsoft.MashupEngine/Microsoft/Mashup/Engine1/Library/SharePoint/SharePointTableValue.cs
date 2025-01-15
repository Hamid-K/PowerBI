using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.OData;
using Microsoft.Mashup.Engine1.Library.OData.V3;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.Web;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.SharePoint
{
	// Token: 0x02000406 RID: 1030
	internal sealed class SharePointTableValue : TableValue
	{
		// Token: 0x06002319 RID: 8985 RVA: 0x0006297C File Offset: 0x00060B7C
		private SharePointTableValue(SharePointUrlBuilder urlBuilder, SharePointEnvironment environment)
		{
			this.urlBuilder = urlBuilder;
			this.environment = environment;
		}

		// Token: 0x17000E99 RID: 3737
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x00062994 File Offset: 0x00060B94
		public override TypeValue Type
		{
			get
			{
				if (!FileHelper.EnumerateTables(this.environment.Options))
				{
					return FileHelper.FolderResultTypeValue(this.environment.Options);
				}
				if (this.urlBuilder.Entity == null)
				{
					return NavigationTableServices.AddNavigationTableMetadata(FileHelper.TableResultTypeWithNameKey, TextValue.New("Name"), TextValue.New("Content"));
				}
				return TypeValue.Table;
			}
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x000629F8 File Offset: 0x00060BF8
		public static TableValue Create(SharePointUrlBuilder urlBuilder, SharePointEnvironment environment)
		{
			if (urlBuilder.Entity != null)
			{
				if (urlBuilder.Path == null)
				{
					TableValue tableValue = SharePointTableValue.GetEntityFeed(urlBuilder.SetTop(1L), environment);
					if (!SharePointTableValue.HasDocuments(tableValue))
					{
						tableValue = SharePointTableValue.GetEntityFeed(urlBuilder, environment);
						return new SharePointTableValue.SharePointTableValueWrapper(tableValue);
					}
					if (environment.Options == FileHelper.FolderOptions.EnumerateTables)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.SharePointCannotEnumerateDocumentAsTable, TextValue.New(urlBuilder.FeedUrl), null);
					}
					urlBuilder = SharePointTableValue.AddContentTypeFilter(urlBuilder, environment);
					urlBuilder = SharePointTableValue.SetPathFromFeed(urlBuilder, environment, tableValue);
				}
				else
				{
					string text = SharePointQueryBuilder.CreatePathFilter(urlBuilder.Path, environment.Options);
					urlBuilder = SharePointTableValue.AddContentTypeFilter(urlBuilder, environment);
					urlBuilder = urlBuilder.SetPathFilter(text);
				}
			}
			return new SharePointTableValue(urlBuilder, environment);
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00062AA0 File Offset: 0x00060CA0
		private static RecordValue CreateFeedRecord(TableValue content, string name, string path)
		{
			TextValue textValue;
			if (SharePointTableValue.HasDocuments(content))
			{
				textValue = SharePointTableValue.FolderContentTypeIdText;
			}
			else
			{
				textValue = SharePointTableValue.TableContentTypeIdText;
				content = new SharePointTableValue.SharePointTableValueWrapper(content);
			}
			return RecordValue.New(SharePointTableValue.FeedRecordKeys, new Value[]
			{
				content,
				TextValue.New(name),
				TextValue.New(path),
				textValue,
				Value.Null,
				Value.Null
			});
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x00062B08 File Offset: 0x00060D08
		private static SharePointTableValue CreateFromFeedRecord(SharePointUrlBuilder urlBuilder, SharePointEnvironment environment, RecordValue feedRecord)
		{
			string asString = feedRecord["Name"].AsString;
			Value value;
			if (!feedRecord.TryGetValue("Path", out value))
			{
				value = feedRecord["Folder Path"];
			}
			string text = SharePointUrlBuilder.AppendNodeToPath(value.AsString, asString);
			string text2 = urlBuilder.Entity;
			if (text2 == null)
			{
				text2 = asString;
				text = null;
			}
			string text3 = SharePointQueryBuilder.CreatePathFilter(text, environment.Options);
			string text4 = SharePointQueryBuilder.SetQueryPart(null, "$filter", text3);
			return new SharePointTableValue(SharePointTableValue.AddContentTypeFilter(new SharePointUrlBuilder(urlBuilder.ServiceUrl, text2, text4), environment), environment);
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00062B94 File Offset: 0x00060D94
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			TableValue feed = SharePointTableValue.GetFeed(this.urlBuilder, this.environment);
			IEnumerator<IValueReference> enumerator = null;
			IEnumerator<IValueReference> enumerator2;
			if (FileHelper.EnumerateDeep(this.environment.Options))
			{
				enumerator2 = new SharePointTableValue.SharePointFeedEnumerator(this.urlBuilder, this.environment, feed.GetEnumerator());
				if (this.urlBuilder.Entity == null)
				{
					SharePointEnvironment sharePointEnvironment = this.environment.SetOptions(FileHelper.FolderOptions.EnumeratingSubfolders);
					enumerator = new SharePointTableValue.SharePointFeedEnumerator(this.urlBuilder, sharePointEnvironment, feed.GetEnumerator());
				}
			}
			else
			{
				enumerator2 = new SharePointTableValue.SharePointFeedEnumerator(this.urlBuilder, this.environment, feed.GetEnumerator());
			}
			return new SharePointTableValue.SharePointTableEnumerator(this.urlBuilder, this.environment, enumerator2, enumerator);
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x00062C3C File Offset: 0x00060E3C
		private static TableValue GetEntityFeed(SharePointUrlBuilder urlBuilder, SharePointEnvironment environment)
		{
			HttpResource httpResource = HttpResource.New("SharePoint", urlBuilder.Url, true);
			return ODataResponse.Create(httpResource, TextValue.New(urlBuilder.Url), TextValue.New(urlBuilder.ServiceUrl), Value.Null, environment.FeedCredentials, environment.Host, new ODataSettings(environment.Host, httpResource, new Uri(urlBuilder.Url)), new ODataUserSettings()).Result.AsTable;
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x00062CB0 File Offset: 0x00060EB0
		private static TableValue GetFeed(SharePointUrlBuilder urlBuilder, SharePointEnvironment environment)
		{
			TableValue tableValue;
			if (urlBuilder.Entity == null)
			{
				TextValue textValue = TextValue.New(urlBuilder.ServiceUrl);
				HttpResource httpResource = new SharePointTableValue.SharepointResource(Resource.New("SharePoint", urlBuilder.ServiceUrl));
				TableValue asTable = ODataModule.GetFeed(httpResource, textValue, textValue, Value.Null, environment.Host, environment.FeedCredentials, new ODataSettings(environment.Host, httpResource, ODataUriCommon.ConvertToUri(textValue)), new ODataUserSettings(), true).AsTable;
				List<IValueReference> list = new List<IValueReference>(asTable.Count);
				foreach (IValueReference valueReference in asTable)
				{
					RecordValue recordValue = (RecordValue)valueReference;
					if ((environment.Entity == null || environment.Entity == recordValue["Name"].AsString) && (asTable.Type.Equals(TypeValue.Table) || SharePointTableValue.HasRecords(recordValue["Data"].Type)))
					{
						list.Add(SharePointTableValue.CreateFeedRecord(recordValue["Data"].AsTable, recordValue["Name"].AsString, urlBuilder.SitePath));
					}
				}
				tableValue = ListValue.New(list).ToTable(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles));
			}
			else
			{
				if (urlBuilder.Path == null && !FileHelper.EnumerateDeep(environment.Options))
				{
					tableValue = SharePointTableValue.GetEntityFeed(urlBuilder.SetTop(1L).SetOrderBy("Path"), environment);
					urlBuilder = SharePointTableValue.SetPathFromFeed(urlBuilder, environment, tableValue);
				}
				tableValue = SharePointTableValue.GetEntityFeed(urlBuilder, environment);
			}
			string text;
			if (!FileHelper.EnumerateDeep(environment.Options) && !SharePointQueryBuilder.TryGetQueryPart(urlBuilder.Query, "$orderby", out text))
			{
				tableValue = SharePointTableValue.SortFeedRecords(urlBuilder.Path ?? urlBuilder.SitePath, tableValue.GetEnumerator());
			}
			return tableValue;
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x00062E8C File Offset: 0x0006108C
		private static bool HasDocuments(TableValue feed)
		{
			TypeValue itemType = feed.Type.AsTableType.ItemType;
			if (itemType.TypeKind == ValueKind.Record)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				foreach (string text in itemType.AsRecordType.Fields.Keys)
				{
					if (!(text == "CheckedOutTo"))
					{
						if (!(text == "ContentTypeID"))
						{
							if (!(text == "Name"))
							{
								if (text == "Path")
								{
									flag4 = true;
								}
							}
							else
							{
								flag3 = true;
							}
						}
						else
						{
							flag2 = true;
						}
					}
					else
					{
						flag = true;
					}
					if (flag && flag2 && flag3 && flag4)
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x00062F68 File Offset: 0x00061168
		private static bool HasRecords(TypeValue feedType)
		{
			return feedType.TypeKind == ValueKind.Table && feedType.AsTableType.ItemType.AsRecordType.Fields.Keys.Contains("ContentTypeID");
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x00062F9A File Offset: 0x0006119A
		private static bool IsDocumentTypeId(string contentTypeId)
		{
			return contentTypeId.StartsWith("0x0101", StringComparison.Ordinal);
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x00062FA8 File Offset: 0x000611A8
		private static bool IsFolderTypeId(string contentTypeId)
		{
			return contentTypeId.StartsWith("0x0120", StringComparison.Ordinal);
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00062FB6 File Offset: 0x000611B6
		private static bool IsTableTypeId(string contentTypeId)
		{
			return contentTypeId.StartsWith("0x0130", StringComparison.Ordinal);
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x00062FC4 File Offset: 0x000611C4
		private static SharePointUrlBuilder SetPathFromFeed(SharePointUrlBuilder urlBuilder, SharePointEnvironment environment, TableValue feed)
		{
			try
			{
				IEnumerator<IValueReference> enumerator = feed.GetEnumerator();
				if (enumerator.MoveNext() && enumerator.Current.Value.IsRecord)
				{
					string text = SharePointQueryBuilder.CreatePathFilter(enumerator.Current.Value.AsRecord["Path"].AsString, environment.Options);
					return urlBuilder.SetPathFilter(text);
				}
			}
			catch (ResourceSecurityException)
			{
			}
			return urlBuilder;
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x00063040 File Offset: 0x00061240
		private static SharePointUrlBuilder AddContentTypeFilter(SharePointUrlBuilder urlBuilder, SharePointEnvironment environment)
		{
			List<string> list = new List<string>();
			if (FileHelper.EnumerateFiles(environment.Options))
			{
				list.Add("0x0101");
			}
			if (FileHelper.EnumerateFolders(environment.Options))
			{
				list.Add("0x0120");
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (string text2 in list)
			{
				stringBuilder.Append(text);
				stringBuilder.Append("startswith(");
				stringBuilder.Append("ContentTypeID");
				stringBuilder.Append(", ");
				stringBuilder.Append("'");
				stringBuilder.Append(text2);
				stringBuilder.Append("'");
				stringBuilder.Append(")");
				text = " or ";
			}
			SharePointQueryBuilder sharePointQueryBuilder = new SharePointQueryBuilder(urlBuilder.Query);
			sharePointQueryBuilder.AddQueryPartValue("$filter", stringBuilder.ToString());
			return new SharePointUrlBuilder(urlBuilder.ServiceUrl, urlBuilder.Entity, sharePointQueryBuilder.Query);
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x00063160 File Offset: 0x00061360
		private static TableValue SortFeedRecords(string parentPath, IEnumerator<IValueReference> feedEnumerator)
		{
			SortedDictionary<string, IValueReference> sortedDictionary = new SortedDictionary<string, IValueReference>();
			SortedDictionary<string, IValueReference> sortedDictionary2 = new SortedDictionary<string, IValueReference>();
			SortedDictionary<string, IValueReference> sortedDictionary3 = new SortedDictionary<string, IValueReference>();
			while (feedEnumerator.MoveNext())
			{
				IValueReference valueReference = feedEnumerator.Current;
				RecordValue asRecord = valueReference.Value.AsRecord;
				if (string.Equals(asRecord["Path"].AsString, parentPath, StringComparison.OrdinalIgnoreCase))
				{
					string asString = asRecord["Name"].AsString;
					string asString2 = asRecord["ContentTypeID"].AsString;
					if (SharePointTableValue.IsFolderTypeId(asString2))
					{
						sortedDictionary2.Add(asString, valueReference);
					}
					else if (SharePointTableValue.IsDocumentTypeId(asString2))
					{
						sortedDictionary.Add(asString, valueReference);
					}
					else if (SharePointTableValue.IsTableTypeId(asString2))
					{
						sortedDictionary3.Add(asString, valueReference);
					}
				}
			}
			List<IValueReference> list = new List<IValueReference>(sortedDictionary3.Count + sortedDictionary2.Count + sortedDictionary.Count);
			list.AddRange(sortedDictionary2.Values);
			list.AddRange(sortedDictionary3.Values);
			list.AddRange(sortedDictionary.Values);
			return ListValue.New(list.ToArray()).ToTable(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles));
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x00063270 File Offset: 0x00061470
		public override TableValue SelectRows(FunctionValue condition)
		{
			TableValue tableValue;
			if (this.TrySelectRows(condition, out tableValue))
			{
				return tableValue;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x00063294 File Offset: 0x00061494
		private bool TrySelectRows(FunctionValue condition, out TableValue tableValue)
		{
			if (this.urlBuilder.Top == null && this.urlBuilder.Skip == null)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this.Type.AsTableType.ItemType, condition);
				SharePointQueryCompiler sharePointQueryCompiler = new SharePointQueryCompiler(this.Columns, this.urlBuilder.HostUrl);
				try
				{
					string text;
					string text2;
					if (this.urlBuilder.Entity == null)
					{
						if (FileHelper.EnumerateDeep(this.environment.Options))
						{
							throw new NotSupportedException();
						}
						text = sharePointQueryCompiler.CompileEntityQueryExpression(queryExpression);
						text2 = null;
					}
					else
					{
						text = null;
						text2 = sharePointQueryCompiler.CompileQueryExpression(queryExpression);
					}
					SharePointQueryBuilder sharePointQueryBuilder = new SharePointQueryBuilder(this.urlBuilder.Query);
					sharePointQueryBuilder.AddQueryPartValue("$filter", text2);
					tableValue = new SharePointTableValue(new SharePointUrlBuilder(this.urlBuilder.ServiceUrl, this.urlBuilder.Entity, sharePointQueryBuilder.Query), this.environment.SetEntity(text));
					return true;
				}
				catch (NotSupportedException)
				{
				}
			}
			tableValue = null;
			return false;
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000633B0 File Offset: 0x000615B0
		public override TableValue Skip(RowCount count)
		{
			TableValue tableValue;
			if (this.TrySkip(count, out tableValue))
			{
				return tableValue;
			}
			return base.Skip(count);
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000633D4 File Offset: 0x000615D4
		private bool TrySkip(RowCount count, out TableValue tableValue)
		{
			if (count.IsZero)
			{
				tableValue = this;
				return true;
			}
			if (this.urlBuilder.Entity != null && !count.IsInfinite)
			{
				SharePointQueryBuilder sharePointQueryBuilder = new SharePointQueryBuilder(this.urlBuilder.SetSkip(count.Value).Query);
				tableValue = new SharePointTableValue(new SharePointUrlBuilder(this.urlBuilder.ServiceUrl, this.urlBuilder.Entity, sharePointQueryBuilder.Query), this.environment);
				return true;
			}
			tableValue = null;
			return false;
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x00063458 File Offset: 0x00061658
		public override TableValue Sort(TableSortOrder sortOrder)
		{
			TableValue tableValue;
			if (this.TrySort(sortOrder, out tableValue))
			{
				return tableValue;
			}
			return base.Sort(sortOrder);
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x0006347C File Offset: 0x0006167C
		private bool TrySort(TableSortOrder sortOrder, out TableValue tableValue)
		{
			QueryExpression[] array;
			bool[] array2;
			if (this.urlBuilder.Entity != null && this.urlBuilder.Top == null && this.urlBuilder.Skip == null && SortQuery.TryGetSelectors(sortOrder, this.Type.AsTableType.ItemType, out array, out array2))
			{
				SharePointQueryCompiler sharePointQueryCompiler = new SharePointQueryCompiler(this.Columns, this.urlBuilder.HostUrl);
				try
				{
					string text = sharePointQueryCompiler.CompileOrderByList(array, array2);
					SharePointQueryBuilder sharePointQueryBuilder = new SharePointQueryBuilder(this.urlBuilder.Query);
					sharePointQueryBuilder.AddQueryPartValue("$orderby", text.ToString());
					tableValue = new SharePointTableValue(new SharePointUrlBuilder(this.urlBuilder.ServiceUrl, this.urlBuilder.Entity, sharePointQueryBuilder.Query), this.environment);
					return true;
				}
				catch (NotSupportedException)
				{
					tableValue = null;
					return false;
				}
			}
			tableValue = null;
			return false;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00063584 File Offset: 0x00061784
		public override TableValue Take(RowCount count)
		{
			TableValue tableValue;
			if (this.TryTake(count, out tableValue))
			{
				return tableValue;
			}
			return base.Take(count);
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000635A8 File Offset: 0x000617A8
		private bool TryTake(RowCount count, out TableValue tableValue)
		{
			if (count.IsInfinite)
			{
				tableValue = this;
				return true;
			}
			if (this.urlBuilder.Entity != null && !count.IsZero)
			{
				SharePointQueryBuilder sharePointQueryBuilder = new SharePointQueryBuilder(this.urlBuilder.SetTop(count.Value).Query);
				tableValue = new SharePointTableValue(new SharePointUrlBuilder(this.urlBuilder.ServiceUrl, this.urlBuilder.Entity, sharePointQueryBuilder.Query), this.environment);
				return true;
			}
			tableValue = null;
			return false;
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public override TableValue Unordered()
		{
			return this;
		}

		// Token: 0x04000E12 RID: 3602
		public const string ContentTypeIdKey = "ContentTypeID";

		// Token: 0x04000E13 RID: 3603
		public const string ContentKey = "Content";

		// Token: 0x04000E14 RID: 3604
		public const string CreatedKey = "Created";

		// Token: 0x04000E15 RID: 3605
		public const string ModifiedKey = "Modified";

		// Token: 0x04000E16 RID: 3606
		public const string NameKey = "Name";

		// Token: 0x04000E17 RID: 3607
		public const string PathKey = "Path";

		// Token: 0x04000E18 RID: 3608
		public const string DocumentContentTypeId = "0x0101";

		// Token: 0x04000E19 RID: 3609
		public const string FolderContentTypeId = "0x0120";

		// Token: 0x04000E1A RID: 3610
		public const string TableContentTypeId = "0x0130";

		// Token: 0x04000E1B RID: 3611
		private static readonly TextValue FolderContentTypeIdText = TextValue.New("0x0120");

		// Token: 0x04000E1C RID: 3612
		private static readonly TextValue TableContentTypeIdText = TextValue.New("0x0130");

		// Token: 0x04000E1D RID: 3613
		private static readonly Keys FeedRecordKeys = Keys.New(new string[] { "Content", "Name", "Path", "ContentTypeID", "Modified", "Created" });

		// Token: 0x04000E1E RID: 3614
		private readonly SharePointUrlBuilder urlBuilder;

		// Token: 0x04000E1F RID: 3615
		private readonly SharePointEnvironment environment;

		// Token: 0x02000407 RID: 1031
		private sealed class SharepointResource : HttpResource
		{
			// Token: 0x06002333 RID: 9011 RVA: 0x00063697 File Offset: 0x00061897
			public SharepointResource(IResource resource)
				: base(resource, true)
			{
			}

			// Token: 0x06002334 RID: 9012 RVA: 0x000636A4 File Offset: 0x000618A4
			public override HttpResource NewUrl(string newUrl)
			{
				SharePointUrlBuilder sharePointUrlBuilder = new SharePointUrlBuilder(newUrl, SharePointApiVersion.SP14);
				return new SharePointTableValue.SharepointResource(Microsoft.Mashup.Engine1.Library.Resource.New(base.Kind, sharePointUrlBuilder.SiteUrl));
			}
		}

		// Token: 0x02000408 RID: 1032
		private sealed class SharePointTableEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x06002335 RID: 9013 RVA: 0x000636CF File Offset: 0x000618CF
			public SharePointTableEnumerator(SharePointUrlBuilder urlBuilder, SharePointEnvironment environment, IEnumerator<IValueReference> feedEnumerator, IEnumerator<IValueReference> folderEnumerator)
			{
				this.urlBuilder = urlBuilder;
				this.environment = environment;
				this.feedEnumerator = feedEnumerator;
				this.folderEnumerator = folderEnumerator;
			}

			// Token: 0x17000E9A RID: 3738
			// (get) Token: 0x06002336 RID: 9014 RVA: 0x000636F4 File Offset: 0x000618F4
			public IValueReference Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000E9B RID: 3739
			// (get) Token: 0x06002337 RID: 9015 RVA: 0x000636FC File Offset: 0x000618FC
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002338 RID: 9016 RVA: 0x00063704 File Offset: 0x00061904
			public void Dispose()
			{
				this.current = null;
				this.DisposeFeedEnumerator();
				this.DisposeFolderEnumerator();
			}

			// Token: 0x06002339 RID: 9017 RVA: 0x00063719 File Offset: 0x00061919
			private void DisposeFeedEnumerator()
			{
				if (this.feedEnumerator != null)
				{
					this.feedEnumerator.Dispose();
					this.feedEnumerator = null;
				}
			}

			// Token: 0x0600233A RID: 9018 RVA: 0x00063735 File Offset: 0x00061935
			private void DisposeFolderEnumerator()
			{
				if (this.folderEnumerator != null)
				{
					this.folderEnumerator.Dispose();
					this.folderEnumerator = null;
				}
			}

			// Token: 0x0600233B RID: 9019 RVA: 0x00063754 File Offset: 0x00061954
			public bool MoveNext()
			{
				while (this.feedEnumerator != null)
				{
					if (this.feedEnumerator.MoveNext())
					{
						this.current = this.feedEnumerator.Current.Value.AsRecord;
						return true;
					}
					this.DisposeFeedEnumerator();
					if (FileHelper.EnumerateDeep(this.environment.Options) && this.folderEnumerator != null && this.folderEnumerator.MoveNext())
					{
						RecordValue asRecord = this.folderEnumerator.Current.Value.AsRecord;
						SharePointEnvironment sharePointEnvironment = this.environment.AddOptions(FileHelper.FolderOptions.EnumeratingSubEntries);
						this.feedEnumerator = SharePointTableValue.CreateFromFeedRecord(this.urlBuilder, sharePointEnvironment, asRecord).GetEnumerator();
					}
				}
				this.current = null;
				return false;
			}

			// Token: 0x0600233C RID: 9020 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x04000E20 RID: 3616
			private readonly SharePointUrlBuilder urlBuilder;

			// Token: 0x04000E21 RID: 3617
			private readonly SharePointEnvironment environment;

			// Token: 0x04000E22 RID: 3618
			private RecordValue current;

			// Token: 0x04000E23 RID: 3619
			private IEnumerator<IValueReference> feedEnumerator;

			// Token: 0x04000E24 RID: 3620
			private IEnumerator<IValueReference> folderEnumerator;
		}

		// Token: 0x02000409 RID: 1033
		private sealed class SharePointFeedEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x0600233D RID: 9021 RVA: 0x0006380C File Offset: 0x00061A0C
			public SharePointFeedEnumerator(SharePointUrlBuilder urlBuilder, SharePointEnvironment environment, IEnumerator<IValueReference> feedEnumerator)
			{
				this.urlBuilder = urlBuilder;
				this.environment = environment;
				this.feedEnumerator = feedEnumerator;
			}

			// Token: 0x17000E9C RID: 3740
			// (get) Token: 0x0600233E RID: 9022 RVA: 0x00063829 File Offset: 0x00061A29
			public IValueReference Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17000E9D RID: 3741
			// (get) Token: 0x0600233F RID: 9023 RVA: 0x00063831 File Offset: 0x00061A31
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002340 RID: 9024 RVA: 0x0006383C File Offset: 0x00061A3C
			private RecordValue CreateDocumentRecord(RecordValue feedRecord)
			{
				string asString = feedRecord["Name"].AsString;
				string asString2 = feedRecord["Path"].AsString;
				TextValue textValue = TextValue.New(SharePointUrlBuilder.AppendNodeToPath(this.urlBuilder.HostUrl, asString2) + "/");
				TextValue documentUrl = TextValue.New(SharePointUrlBuilder.AppendNodeToPath(textValue.String, asString));
				IResource resource = Resource.New("SharePoint", documentUrl.String);
				ResourceCredentialCollection documentCredentials;
				HttpServices.VerifyPermissionAndGetCredentials(this.environment.Host, resource, out documentCredentials);
				BinaryValue documentContent = WebContentsBinaryValue.New(new Lazy<Request>(() => this.CreateFileRequest(documentCredentials, documentUrl)), documentCredentials, Value.Null, false, documentUrl.AsString);
				TextValue textValue2;
				try
				{
					textValue2 = FileHelper.GetFileExtension(asString);
				}
				catch (ValueException)
				{
					textValue2 = TextValue.Empty;
				}
				RecordValue recordValue = SharePointTableValue.SharePointFeedEnumerator.CreateAttributesRecord(feedRecord, () => documentContent.MetaValue["Content.Type"].AsText, () => FileHelper.GetFileKind(documentContent.MetaValue["Content.Type"].AsString), () => NumberValue.New(documentContent.Length));
				return RecordValue.New(FileHelper.FileEntryKeys, new Value[]
				{
					documentContent,
					feedRecord["Name"],
					textValue2,
					Value.Null,
					feedRecord["Modified"],
					feedRecord["Created"],
					recordValue,
					textValue
				});
			}

			// Token: 0x06002341 RID: 9025 RVA: 0x000639BC File Offset: 0x00061BBC
			private Request CreateFileRequest(ResourceCredentialCollection credentials, TextValue url)
			{
				if (credentials.Count == 1 && credentials[0] is OAuthCredential)
				{
					url = TextValue.New(SharePointFile.GetCsomFileUrl(this.environment.Host, "SharePoint", url, credentials));
				}
				return Request.Create(this.environment.Host, "SharePoint", url.AsString, url, null, null, null, null, null, null, null, null, new Action<MashupHttpWebResponse, IHostTrace>(SharePointUtil.HandleResponseError), null);
			}

			// Token: 0x06002342 RID: 9026 RVA: 0x00063A30 File Offset: 0x00061C30
			private static RecordValue CreateAttributesRecord(RecordValue feedRecord, Func<TextValue> contentType, Func<Value> kindProvider, Func<Value> sizeProvider)
			{
				RecordValue recordValue = RecordValue.New(Keys.New("Content Type", "Kind", "Size"), delegate(int i)
				{
					Value value;
					try
					{
						switch (i)
						{
						case 0:
							value = contentType();
							break;
						case 1:
							value = kindProvider();
							break;
						case 2:
							value = sizeProvider();
							break;
						default:
							throw new InvalidOperationException();
						}
					}
					catch (ValueException)
					{
						value = Value.Null;
					}
					return value;
				});
				return Library.Record.Combine.Invoke(ListValue.New(new Value[] { recordValue, feedRecord })).AsRecord;
			}

			// Token: 0x06002343 RID: 9027 RVA: 0x00063AA0 File Offset: 0x00061CA0
			private RecordValue CreateFolderRecord(RecordValue feedRecord)
			{
				string asString = feedRecord["Path"].AsString;
				SharePointEnvironment sharePointEnvironment = this.environment.SetOptions(FileHelper.FolderOptions.EnumerateFoldersAndFiles);
				SharePointTableValue sharePointTableValue = SharePointTableValue.CreateFromFeedRecord(this.urlBuilder, sharePointEnvironment, feedRecord);
				TextValue textValue = TextValue.New(SharePointUrlBuilder.AppendNodeToPath(this.urlBuilder.HostUrl, asString) + "/");
				RecordValue recordValue = SharePointTableValue.SharePointFeedEnumerator.CreateAttributesRecord(feedRecord, () => TextValue.Empty, () => FileHelper.FolderKind, () => Value.Null);
				return RecordValue.New(FileHelper.FileEntryKeys, new Value[]
				{
					sharePointTableValue,
					feedRecord["Name"],
					TextValue.Empty,
					Value.Null,
					feedRecord["Modified"],
					feedRecord["Created"],
					recordValue,
					textValue
				});
			}

			// Token: 0x06002344 RID: 9028 RVA: 0x00063BB8 File Offset: 0x00061DB8
			private RecordValue CreateTableRecord(RecordValue feedRecord)
			{
				TableValue asTable = feedRecord["Content"].AsTable;
				string asString = feedRecord["Path"].AsString;
				TextValue textValue = TextValue.New(SharePointUrlBuilder.AppendNodeToPath(this.urlBuilder.HostUrl, asString) + "/");
				return RecordValue.New(FileHelper.TableEntryKeys, new Value[]
				{
					asTable,
					feedRecord["Name"],
					textValue
				});
			}

			// Token: 0x06002345 RID: 9029 RVA: 0x00063C2E File Offset: 0x00061E2E
			public void Dispose()
			{
				this.current = null;
				this.DisposeFeedEnumerator();
			}

			// Token: 0x06002346 RID: 9030 RVA: 0x00063C3D File Offset: 0x00061E3D
			private void DisposeFeedEnumerator()
			{
				if (this.feedEnumerator != null)
				{
					this.feedEnumerator.Dispose();
					this.feedEnumerator = null;
				}
			}

			// Token: 0x06002347 RID: 9031 RVA: 0x00063C5C File Offset: 0x00061E5C
			public bool MoveNext()
			{
				try
				{
					while (this.feedEnumerator.MoveNext())
					{
						try
						{
							RecordValue asRecord = this.feedEnumerator.Current.Value.AsRecord;
							string asString = asRecord["ContentTypeID"].AsString;
							if (SharePointTableValue.IsFolderTypeId(asString) && FileHelper.EnumerateFolders(this.environment.Options))
							{
								this.current = this.CreateFolderRecord(asRecord);
								return true;
							}
							if (SharePointTableValue.IsDocumentTypeId(asString) && FileHelper.EnumerateFiles(this.environment.Options))
							{
								this.current = this.CreateDocumentRecord(asRecord);
								return true;
							}
							if (SharePointTableValue.IsTableTypeId(asString) && FileHelper.EnumerateTables(this.environment.Options))
							{
								this.current = this.CreateTableRecord(asRecord);
								return true;
							}
						}
						catch (ResourceSecurityException)
						{
							if (!FileHelper.EnumeratingSubEntries(this.environment.Options))
							{
								throw;
							}
						}
					}
				}
				catch (ResourceSecurityException)
				{
					if (!FileHelper.EnumeratingSubEntries(this.environment.Options))
					{
						throw;
					}
				}
				this.current = null;
				return false;
			}

			// Token: 0x06002348 RID: 9032 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x04000E25 RID: 3621
			private readonly SharePointUrlBuilder urlBuilder;

			// Token: 0x04000E26 RID: 3622
			private readonly SharePointEnvironment environment;

			// Token: 0x04000E27 RID: 3623
			private RecordValue current;

			// Token: 0x04000E28 RID: 3624
			private IEnumerator<IValueReference> feedEnumerator;
		}

		// Token: 0x0200040D RID: 1037
		private class SharePointTableValueWrapper : TableValue
		{
			// Token: 0x06002355 RID: 9045 RVA: 0x00063E74 File Offset: 0x00062074
			public SharePointTableValueWrapper(TableValue table)
				: this(table, new FunctionValue[0], new int[0])
			{
			}

			// Token: 0x06002356 RID: 9046 RVA: 0x00063E89 File Offset: 0x00062089
			private SharePointTableValueWrapper(TableValue table, FunctionValue[] localFilters, int[] localFilterColumns)
			{
				this.table = table;
				this.localFilters = localFilters;
				this.localFilterColumns = localFilterColumns;
			}

			// Token: 0x17000E9E RID: 3742
			// (get) Token: 0x06002357 RID: 9047 RVA: 0x00063EA6 File Offset: 0x000620A6
			public override TypeValue Type
			{
				get
				{
					return this.table.Type;
				}
			}

			// Token: 0x17000E9F RID: 3743
			// (get) Token: 0x06002358 RID: 9048 RVA: 0x00063EB3 File Offset: 0x000620B3
			public override RecordValue MetaValue
			{
				get
				{
					return this.table.MetaValue;
				}
			}

			// Token: 0x17000EA0 RID: 3744
			// (get) Token: 0x06002359 RID: 9049 RVA: 0x00063EC0 File Offset: 0x000620C0
			public override Keys Columns
			{
				get
				{
					return this.table.Columns;
				}
			}

			// Token: 0x17000EA1 RID: 3745
			// (get) Token: 0x0600235A RID: 9050 RVA: 0x00063ECD File Offset: 0x000620CD
			public override TableSortOrder SortOrder
			{
				get
				{
					return this.table.SortOrder;
				}
			}

			// Token: 0x17000EA2 RID: 3746
			public override Value this[string key]
			{
				get
				{
					return this.table[key];
				}
			}

			// Token: 0x17000EA3 RID: 3747
			public override Value this[Value key]
			{
				get
				{
					return this.table[key];
				}
			}

			// Token: 0x17000EA4 RID: 3748
			public override Value this[int index]
			{
				get
				{
					if (this.localFilters.Length == 0)
					{
						return this.table[index];
					}
					return base[index];
				}
			}

			// Token: 0x17000EA5 RID: 3749
			// (get) Token: 0x0600235E RID: 9054 RVA: 0x00063F15 File Offset: 0x00062115
			public override long LargeCount
			{
				get
				{
					if (this.localFilters.Length != 0)
					{
						return base.LargeCount;
					}
					return this.table.LargeCount;
				}
			}

			// Token: 0x0600235F RID: 9055 RVA: 0x00063F32 File Offset: 0x00062132
			public override Value NewMeta(RecordValue metaValue)
			{
				return this.Wrap(TableValue.New(this, metaValue));
			}

			// Token: 0x06002360 RID: 9056 RVA: 0x00063F41 File Offset: 0x00062141
			public override TableValue Unordered()
			{
				return this.Wrap(this.table.Unordered());
			}

			// Token: 0x06002361 RID: 9057 RVA: 0x00063F54 File Offset: 0x00062154
			public override bool TryGetValue(Value key, out Value value)
			{
				return this.table.TryGetValue(key, out value);
			}

			// Token: 0x06002362 RID: 9058 RVA: 0x00063F63 File Offset: 0x00062163
			public override Value NewType(TypeValue type)
			{
				return this.Wrap(this.table.NewType(type).AsTable);
			}

			// Token: 0x06002363 RID: 9059 RVA: 0x00063F7C File Offset: 0x0006217C
			public override bool TrySelectColumns(ColumnSelection columnSelection, out TableValue table)
			{
				if (this.localFilters.Length == 0)
				{
					table = this.Wrap(this.table.SelectColumns(columnSelection));
					return true;
				}
				table = null;
				return false;
			}

			// Token: 0x06002364 RID: 9060 RVA: 0x00063FA4 File Offset: 0x000621A4
			public override TableValue SelectRows(FunctionValue condition)
			{
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this.Type.AsTableType.ItemType, condition);
				TableValue tableValue = this.table;
				HashSet<int> hashSet = new HashSet<int>(this.localFilterColumns);
				List<FunctionValue> list = new List<FunctionValue>(this.localFilters);
				foreach (QueryExpression queryExpression2 in SelectRowsQuery.GetConjunctiveNF(queryExpression))
				{
					bool accessedDateTime = false;
					List<int> accessedColumns = new List<int>();
					queryExpression2.Rewrite(delegate(QueryExpression qe)
					{
						if (qe.Kind == QueryExpressionKind.ColumnAccess)
						{
							ColumnAccessQueryExpression columnAccessQueryExpression = (ColumnAccessQueryExpression)qe;
							accessedColumns.Add(columnAccessQueryExpression.Column);
							TypeValue asType = this.Type.AsTableType.ItemType.Fields[columnAccessQueryExpression.Column]["Type"].AsType;
							if (asType.TypeKind == ValueKind.DateTime || asType.TypeKind == ValueKind.DateTimeZone)
							{
								accessedDateTime = true;
							}
						}
						return qe;
					});
					FunctionValue functionValue = QueryExpressionAssembler.Assemble(this.Columns, queryExpression2);
					if (accessedDateTime)
					{
						list.Add(functionValue);
						using (List<int>.Enumerator enumerator2 = accessedColumns.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								int num = enumerator2.Current;
								hashSet.Add(num);
							}
							continue;
						}
					}
					tableValue = tableValue.SelectRows(functionValue);
				}
				if (list.Count > this.localFilters.Length)
				{
					int[] array = new int[hashSet.Count];
					hashSet.CopyTo(array);
					return new SharePointTableValue.SharePointTableValueWrapper(tableValue, list.ToArray(), array);
				}
				return this.Wrap(tableValue);
			}

			// Token: 0x06002365 RID: 9061 RVA: 0x00064110 File Offset: 0x00062310
			public override TableValue AddColumns(ColumnsConstructor columnGenerator)
			{
				return this.Wrap(this.table.AddColumns(columnGenerator));
			}

			// Token: 0x06002366 RID: 9062 RVA: 0x00064124 File Offset: 0x00062324
			public override TableValue Group(Grouping grouping)
			{
				return this.Wrap(this.table.Group(grouping));
			}

			// Token: 0x06002367 RID: 9063 RVA: 0x00064138 File Offset: 0x00062338
			public override TableValue Skip(RowCount count)
			{
				if (this.localFilters.Length != 0)
				{
					return base.Skip(count);
				}
				return this.Wrap(this.table.Skip(count));
			}

			// Token: 0x06002368 RID: 9064 RVA: 0x0006415D File Offset: 0x0006235D
			public override TableValue Take(RowCount count)
			{
				if (this.localFilters.Length != 0)
				{
					return base.Take(count);
				}
				return this.Wrap(this.table.Take(count));
			}

			// Token: 0x06002369 RID: 9065 RVA: 0x00064182 File Offset: 0x00062382
			public override TableValue Sort(TableSortOrder sortOrder)
			{
				return this.Wrap(this.table.Sort(sortOrder));
			}

			// Token: 0x0600236A RID: 9066 RVA: 0x00064196 File Offset: 0x00062396
			public override TableValue Distinct(TableDistinct distinctCriteria)
			{
				return this.Wrap(this.table.Distinct(distinctCriteria));
			}

			// Token: 0x0600236B RID: 9067 RVA: 0x000641AC File Offset: 0x000623AC
			public override TableValue NestedJoin(int[] leftKeyColumns, Value rightTable, Keys rightKey, TableTypeAlgebra.JoinKind joinKind, string newColumn, Keys joinKeys, FunctionValue[] keyEqualityComparers)
			{
				return this.Wrap(this.table.NestedJoin(leftKeyColumns, rightTable, rightKey, joinKind, newColumn, joinKeys, keyEqualityComparers));
			}

			// Token: 0x0600236C RID: 9068 RVA: 0x000641D5 File Offset: 0x000623D5
			public override TableValue ExpandListColumn(int columnIndex, bool singleOrDefault)
			{
				return this.Wrap(this.table.ExpandListColumn(columnIndex, singleOrDefault));
			}

			// Token: 0x0600236D RID: 9069 RVA: 0x000641EA File Offset: 0x000623EA
			public override TableValue ExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns)
			{
				return this.Wrap(this.table.ExpandRecordColumn(columnToExpand, fieldsToProject, newColumns));
			}

			// Token: 0x0600236E RID: 9070 RVA: 0x00064200 File Offset: 0x00062400
			public override Value Concatenate(Value value)
			{
				return this.Wrap(this.table.Concatenate(value).AsTable);
			}

			// Token: 0x0600236F RID: 9071 RVA: 0x00064219 File Offset: 0x00062419
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				foreach (IValueReference valueReference in this.table)
				{
					bool flag = true;
					FunctionValue[] array = this.localFilters;
					for (int i = 0; i < array.Length; i++)
					{
						if (!array[i].Invoke(valueReference.Value).AsBoolean)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						yield return valueReference;
					}
				}
				IEnumerator<IValueReference> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06002370 RID: 9072 RVA: 0x00064228 File Offset: 0x00062428
			private TableValue Wrap(TableValue table)
			{
				return new SharePointTableValue.SharePointTableValueWrapper(table, this.localFilters, this.localFilterColumns);
			}

			// Token: 0x04000E34 RID: 3636
			private readonly TableValue table;

			// Token: 0x04000E35 RID: 3637
			private readonly FunctionValue[] localFilters;

			// Token: 0x04000E36 RID: 3638
			private readonly int[] localFilterColumns;
		}
	}
}
