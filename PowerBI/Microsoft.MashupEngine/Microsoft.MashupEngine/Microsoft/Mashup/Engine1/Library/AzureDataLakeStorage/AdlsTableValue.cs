using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Library.Uris;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureDataLakeStorage
{
	// Token: 0x02000ED4 RID: 3796
	internal sealed class AdlsTableValue : TableValue, IOptimizedValue
	{
		// Token: 0x060064D3 RID: 25811 RVA: 0x001599EC File Offset: 0x00157BEC
		public AdlsTableValue(IEngineHost host, List<AdlsEndpoint> adlsEndpoints, string resourceKind, OptionsRecord options, bool isHierarchical, AdlsVersions.Version version = null)
		{
			this.host = host;
			this.adlsEndpoints = adlsEndpoints;
			this.options = options;
			this.resourceKind = resourceKind;
			this.isHierarchical = isHierarchical;
			this.version = version;
		}

		// Token: 0x17001D5D RID: 7517
		// (get) Token: 0x060064D4 RID: 25812 RVA: 0x00159A21 File Offset: 0x00157C21
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001D5E RID: 7518
		// (get) Token: 0x060064D5 RID: 25813 RVA: 0x00159A29 File Offset: 0x00157C29
		public string ResourceKind
		{
			get
			{
				return this.resourceKind;
			}
		}

		// Token: 0x17001D5F RID: 7519
		// (get) Token: 0x060064D6 RID: 25814 RVA: 0x00159A31 File Offset: 0x00157C31
		public TextValue FolderUrl
		{
			get
			{
				return TextValue.New(AdlsEndpoint.GetFolderUrl(this.adlsEndpoints));
			}
		}

		// Token: 0x17001D60 RID: 7520
		// (get) Token: 0x060064D7 RID: 25815 RVA: 0x00159A43 File Offset: 0x00157C43
		public OptionsRecord Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x17001D61 RID: 7521
		// (get) Token: 0x060064D8 RID: 25816 RVA: 0x00159A4B File Offset: 0x00157C4B
		public AdlsVersions.Version Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x060064D9 RID: 25817 RVA: 0x00159A54 File Offset: 0x00157C54
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			IEnumerable<IValueReference> enumerable = from vr in this.GetDirectoryListing()
				select AdlsTableRow.New(this.host, this.resourceKind, vr.Value.AsRecord, this.options, this.version, null);
			if (this.version != null)
			{
				IDictionary<string, BinaryValue> replacements = this.version.GetBlobReplacements();
				enumerable = enumerable.Where(delegate(IValueReference r)
				{
					AdlsBinaryValue adlsBinaryValue;
					BinaryValue binaryValue;
					return !r.Value.AsRecord["Content"].TryGetAs<AdlsBinaryValue>(out adlsBinaryValue) || !replacements.TryGetValue(adlsBinaryValue.BlobUrl.AsString, out binaryValue);
				});
				IEnumerable<IValueReference> enumerable2 = from r in replacements
					where r.Value != null
					select AdlsTableRow.New(this.host, this.resourceKind, this.CreateEntry(r.Key), this.options, this.version, r.Value);
				enumerable = enumerable.Concat(enumerable2);
			}
			return enumerable.GetEnumerator();
		}

		// Token: 0x060064DA RID: 25818 RVA: 0x00159AF8 File Offset: 0x00157CF8
		private RecordValue CreateEntry(string blobUrl)
		{
			return RecordValue.New(AdlsHelper.ListEntryRecordKeys, new Value[]
			{
				TextValue.New(UriHelper.GetFileName(blobUrl)),
				TextValue.New(blobUrl),
				Value.Null,
				NumberValue.Zero,
				TextValue.New(UriHelper.GetDirectoryName(blobUrl)),
				Value.Null,
				Value.Null,
				Value.Null,
				LogicalValue.False
			});
		}

		// Token: 0x17001D62 RID: 7522
		// (get) Token: 0x060064DB RID: 25819 RVA: 0x00159B6B File Offset: 0x00157D6B
		public override TypeValue Type
		{
			get
			{
				return FileSystemTableHelper.AddTypeMetadata(FileHelper.FolderResultTypeValue(this.isHierarchical ? FileHelper.FolderOptions.EnumerateFoldersAndFiles : FileHelper.FolderOptions.EnumerateFilesDeep), this.FolderUrl.AsString, "/", this.isHierarchical);
			}
		}

		// Token: 0x17001D63 RID: 7523
		// (get) Token: 0x060064DC RID: 25820 RVA: 0x00159B99 File Offset: 0x00157D99
		public override Query Query
		{
			get
			{
				return new AdlsTableValue.FolderContentsQuery(this, null);
			}
		}

		// Token: 0x17001D64 RID: 7524
		// (get) Token: 0x060064DD RID: 25821 RVA: 0x00159BA4 File Offset: 0x00157DA4
		public override IExpression Expression
		{
			get
			{
				IExpression expression;
				if (((AdlsTableValue.FolderContentsQuery)this.Query).TryGetExpression(out expression))
				{
					return expression;
				}
				return base.Expression;
			}
		}

		// Token: 0x060064DE RID: 25822 RVA: 0x00159BCD File Offset: 0x00157DCD
		private void VerifyActionPermitted()
		{
			this.host.VerifyActionPermitted(Resource.New(this.resourceKind, this.adlsEndpoints[0].BaseEndpoint));
		}

		// Token: 0x060064DF RID: 25823 RVA: 0x00159BF8 File Offset: 0x00157DF8
		private ActionValue InsertRows(TableValue rows, bool countOnlyTable)
		{
			this.VerifyActionPermitted();
			if (countOnlyTable)
			{
				long num = 0L;
				List<IValueReference> list = new List<IValueReference>();
				foreach (IValueReference valueReference in rows)
				{
					num += 1L;
					RecordValue asRecord = valueReference.Value.AsRecord;
					Value value;
					string text;
					if (!asRecord.TryGetValue("Folder Path", out value))
					{
						if (this.adlsEndpoints.Count != 1)
						{
							throw ValueException.NewExpressionError<Message0>(Strings.Storage_NeedsFolderPath, asRecord, null);
						}
						text = AzureBaseHelper.FormatContainerString(this.adlsEndpoints[0].AbsoluteUri);
					}
					else
					{
						text = AzureBaseHelper.FormatContainerString(value.AsString);
						bool flag = false;
						foreach (AdlsEndpoint adlsEndpoint in this.adlsEndpoints)
						{
							if (text.StartsWith(adlsEndpoint.AbsoluteUri, StringComparison.OrdinalIgnoreCase))
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							throw ValueException.NewExpressionError<Message1>(Strings.File_PathMustBeSubPath(this.adlsEndpoints[0].AbsoluteUri), value, null);
						}
					}
					string asString = asRecord["Name"].AsString;
					if (UriHelper.ContainsPathSeparator(asString))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.File_NoPathInName, asRecord, null);
					}
					if (AdlsTableValue.FieldOrDefault(asRecord, "Extension", FileHelper.GetFileExtension(asString).AsString) != FileHelper.GetFileExtension(asString).AsString)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.File_ExtensionMustMatchName, asRecord, null);
					}
					if (asRecord.Keys.Contains("Date accessed") || asRecord.Keys.Contains("Date modified") || asRecord.Keys.Contains("Date created"))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.File_SetDatesNotSupported, asRecord, null);
					}
					Value value2;
					if (asRecord.TryGetValue("Attributes", out value2))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.Storage_NoAttributes, asRecord, null);
					}
					string url = text + asString;
					IResource resource = Resource.New(this.resourceKind, url);
					bool isOneLake = this.options.GetBool("IsOneLake", false);
					BinaryValue binaryValue = BinaryValue.Empty;
					bool flag2 = false;
					Value value3;
					if (asRecord.TryGetValue("Content", out value3))
					{
						if (value3.IsTable)
						{
							flag2 = true;
						}
						else if (!value3.IsNull)
						{
							binaryValue = value3.AsBinary;
						}
					}
					if (flag2)
					{
						if (!this.isHierarchical)
						{
							throw ValueException.NewDataSourceError<Message0>(Strings.Storage_NoDirectoryCreation, this, null);
						}
						if (value3.AsTable.Any<IValueReference>())
						{
							throw ValueException.NewDataSourceError<Message0>(Strings.Storage_EmptyDirectoryOnly, this, null);
						}
						ActionValue actionValue = ActionValue.New(delegate
						{
							Request request = AdlsHelper.CreateRequest(this.host, resource, TextValue.New(url), RecordValue.New(Keys.New("resource"), new Value[] { TextValue.New("directory") }), RecordValue.New(Keys.New("If-None-Match"), new Value[] { TextValue.New("*") }), BinaryValue.Empty, isOneLake);
							request.Method = "PUT";
							request.ContentLength = 0L;
							using (AzureBaseHelper.GetResponse(request, null, null))
							{
							}
							return Value.Null;
						});
						if (this.version != null)
						{
							actionValue = this.version.CreateVersionedAction(actionValue);
						}
						list.Add(actionValue);
					}
					else
					{
						bool flag3 = true;
						if (this.version != null)
						{
							BinaryValue binaryValue2;
							flag3 = !this.version.GetBlobReplacements().TryGetValue(url, out binaryValue2);
						}
						ActionValue actionValue2 = AdlsBinaryValue.New(this.host, resource, TextValue.New(url), this.options, flag3, this.version).Replace(binaryValue);
						list.Add(actionValue2);
					}
				}
				list.Add(ActionModule.Action.Return.Invoke(NumberValue.New(num)));
				list.Add(new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType));
				return ActionValue.New(ListValue.New(list)).ClearCache(this.host);
			}
			throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this, null);
		}

		// Token: 0x060064E0 RID: 25824 RVA: 0x00159FB4 File Offset: 0x001581B4
		private ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector, bool countOnlyTable)
		{
			this.VerifyActionPermitted();
			if (countOnlyTable && columnUpdates.Updates.Count == 1)
			{
				KeyValuePair<int, FunctionValue> keyValuePair = columnUpdates.Updates.Single<KeyValuePair<int, FunctionValue>>();
				if (this.Columns[keyValuePair.Key] == "Name")
				{
					long num = 0L;
					List<IValueReference> list = new List<IValueReference>();
					foreach (IValueReference valueReference in this.SelectRows(selector))
					{
						num += 1L;
						RecordValue asRecord = valueReference.Value.AsRecord;
						Value value = asRecord["Content"];
						Uri uri = new Uri(asRecord["Folder Path"].AsString);
						AdlsBinaryValue adlsBinaryValue;
						if (value.TryGetAs<AdlsBinaryValue>(out adlsBinaryValue))
						{
							TextValue asText = keyValuePair.Value.Invoke(asRecord).AsText;
							Uri uri2 = UriHelper.Combine(uri, asText);
							AdlsBinaryValue adlsBinaryValue2;
							if (AdlsBinaryValue.New(this.host, Resource.New(this.resourceKind, uri2.AbsoluteUri), TextValue.New(uri2.AbsoluteUri), this.options, true, this.version).TryGetAs<AdlsBinaryValue>(out adlsBinaryValue2))
							{
								ActionValue actionValue = new MoveBlobActionValue(adlsBinaryValue2, adlsBinaryValue);
								if (this.version != null)
								{
									actionValue = this.version.CreateVersionedAction(actionValue);
								}
								list.Add(actionValue);
								continue;
							}
						}
						list = null;
						break;
					}
					if (list != null)
					{
						list.Add(ActionModule.Action.Return.Invoke(NumberValue.New(num)));
						list.Add(new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType));
						return ActionValue.New(ListValue.New(list)).ClearCache(this.host);
					}
				}
			}
			throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this, null);
		}

		// Token: 0x060064E1 RID: 25825 RVA: 0x0015A17C File Offset: 0x0015837C
		private ActionValue DeleteRows(FunctionValue selector, bool countOnlyTable)
		{
			this.VerifyActionPermitted();
			if (countOnlyTable)
			{
				long num = 0L;
				List<IValueReference> list = new List<IValueReference>();
				foreach (IValueReference valueReference in this.SelectRows(selector))
				{
					num += 1L;
					RecordValue asRecord = valueReference.Value.AsRecord;
					Value value = asRecord["Content"];
					string asString = asRecord["Folder Path"].AsString;
					string asString2 = asRecord["Name"].AsString;
					ActionValue actionValue = null;
					AdlsBinaryValue adlsBinaryValue;
					AdlsTableValue adlsTableValue;
					if (value.TryGetAs<AdlsBinaryValue>(out adlsBinaryValue) && adlsBinaryValue.Version == this.version)
					{
						actionValue = new DeleteBlobActionValue(adlsBinaryValue);
					}
					else if (value.TryGetAs<AdlsTableValue>(out adlsTableValue) && adlsTableValue.Version == this.version)
					{
						actionValue = new DeleteDirectoryActionValue(adlsTableValue);
					}
					if (actionValue == null)
					{
						list = null;
						break;
					}
					if (this.version != null)
					{
						actionValue = this.version.CreateVersionedAction(actionValue);
					}
					list.Add(actionValue);
				}
				if (list != null)
				{
					list.Add(ActionModule.Action.Return.Invoke(NumberValue.New(num)));
					list.Add(new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType));
					return ActionValue.New(ListValue.New(list)).ClearCache(this.host);
				}
			}
			throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this, null);
		}

		// Token: 0x060064E2 RID: 25826 RVA: 0x0015A2E4 File Offset: 0x001584E4
		private IEnumerable<IValueReference> GetDirectoryListing()
		{
			RecordValue listDirectoryQuery = (this.isHierarchical ? AdlsHelper.ListBlobs : AdlsHelper.ListBlobsRecursive);
			int num3;
			for (int endpointIdx = 0; endpointIdx < this.adlsEndpoints.Count; endpointIdx = num3 + 1)
			{
				AdlsEndpoint adlsEndpoint = this.adlsEndpoints[endpointIdx];
				TextValue resourceUrl = TextValue.New(adlsEndpoint.FileSystemEndpoint);
				IResource resource = Resource.New(this.resourceKind, AzureBaseHelper.FormatContainerString(adlsEndpoint.AbsoluteUri));
				bool isOneLake = this.options.GetBool("IsOneLake", false);
				Value requestQueries = ((adlsEndpoint.Directory == null) ? listDirectoryQuery : listDirectoryQuery.Concatenate(RecordValue.New(Keys.New("directory"), new Value[] { TextValue.New(adlsEndpoint.Directory) })));
				string previousContinuationToken = null;
				int usingSameContinuationTokenCount = 0;
				for (;;)
				{
					Request request = AdlsHelper.CreateRequest(this.host, resource, resourceUrl, requestQueries, null, null, isOneLake);
					if (usingSameContinuationTokenCount != 0)
					{
						request.IsRetry = true;
					}
					string continuation;
					RecordValue asRecord = AdlsHelper.GetResponse(request, null, endpointIdx < this.adlsEndpoints.Count - 1, out continuation).AsRecord;
					ListValue items = asRecord["paths"].AsList;
					int i = 0;
					while ((long)i < items.LargeCount)
					{
						RecordValue asRecord2 = items[i].AsRecord;
						bool flag = asRecord2.Keys.Contains("isDirectory") && asRecord2["isDirectory"].AsString.Equals("true", StringComparison.OrdinalIgnoreCase);
						if (this.isHierarchical || !flag)
						{
							string asString = asRecord2["name"].AsString;
							string text = AzureBaseHelper.FormatContainerString(adlsEndpoint.FileSystemEndpoint) + asString;
							string urlWithSelectEscaping = AzureBaseHelper.GetUrlWithSelectEscaping(AzureBaseHelper.FormatContainerString(adlsEndpoint.AbsoluteUri), text);
							int num2;
							long num = (asRecord2.Keys.TryGetKeyIndex("contentLength", out num2) ? Convert.ToInt64(asRecord2[num2].AsString, CultureInfo.InvariantCulture) : 0L);
							Value value = (asRecord2.Keys.TryGetKeyIndex("group", out num2) ? asRecord2[num2].AsText : Value.Null);
							Value value2 = (asRecord2.Keys.TryGetKeyIndex("owner", out num2) ? asRecord2[num2].AsText : Value.Null);
							Value value3 = (asRecord2.Keys.TryGetKeyIndex("permissions", out num2) ? asRecord2[num2].AsText : Value.Null);
							Value value4 = (asRecord2.Keys.TryGetKeyIndex("lastModified", out num2) ? AzureBaseHelper.ConvertToDateTime(asRecord2[num2].AsString) : Value.Null);
							RecordValue recordValue = RecordValue.New(AdlsHelper.ListEntryRecordKeys, new Value[]
							{
								TextValue.New(asString),
								TextValue.New(urlWithSelectEscaping),
								value4,
								NumberValue.New(num),
								TextValue.New(AzureBaseHelper.FormatContainerString(adlsEndpoint.FileSystemEndpoint)),
								value,
								value2,
								value3,
								LogicalValue.New(flag)
							});
							yield return recordValue;
						}
						num3 = i + 1;
						i = num3;
					}
					if (!string.IsNullOrEmpty(continuation))
					{
						if (string.Equals(continuation, previousContinuationToken, StringComparison.Ordinal))
						{
							using (IHostTrace hostTrace = TracingService.CreateTrace(this.host, "Engine/IO/AzureStorage/GetDirectoryListing/DuplicateToken", TraceEventType.Error, null))
							{
								hostTrace.Add("continuationToken", continuation, true);
								hostTrace.Add("previousContinuationToken", previousContinuationToken, true);
								num3 = usingSameContinuationTokenCount;
								usingSameContinuationTokenCount = num3 + 1;
								if (usingSameContinuationTokenCount == 1)
								{
									throw DataSourceException.NewDataSourceError<Message0>(this.host, Strings.DuplicateContinuationToken, resource, null, null);
								}
								goto IL_046C;
							}
							goto IL_0459;
						}
						goto IL_0459;
						IL_046C:
						requestQueries = requestQueries.Concatenate(RecordValue.New(Keys.New("continuation"), new Value[] { TextValue.New(continuation) }));
						continuation = null;
						items = null;
						continue;
						IL_0459:
						previousContinuationToken = continuation;
						usingSameContinuationTokenCount = 0;
						goto IL_046C;
					}
					break;
				}
				adlsEndpoint = null;
				resourceUrl = null;
				resource = null;
				requestQueries = null;
				previousContinuationToken = null;
				num3 = endpointIdx;
			}
			yield break;
		}

		// Token: 0x060064E3 RID: 25827 RVA: 0x0015A2F4 File Offset: 0x001584F4
		private static string FieldOrDefault(RecordValue record, string field, string defaultString)
		{
			Value value;
			if (record.TryGetValue(field, out value))
			{
				return value.AsString;
			}
			return defaultString;
		}

		// Token: 0x0400372B RID: 14123
		private readonly IEngineHost host;

		// Token: 0x0400372C RID: 14124
		private readonly List<AdlsEndpoint> adlsEndpoints;

		// Token: 0x0400372D RID: 14125
		private readonly string resourceKind;

		// Token: 0x0400372E RID: 14126
		private readonly OptionsRecord options;

		// Token: 0x0400372F RID: 14127
		private readonly bool isHierarchical;

		// Token: 0x04003730 RID: 14128
		private readonly AdlsVersions.Version version;

		// Token: 0x02000ED5 RID: 3797
		private sealed class FolderContentsQuery : DataSourceQuery
		{
			// Token: 0x060064E6 RID: 25830 RVA: 0x0015A372 File Offset: 0x00158572
			public FolderContentsQuery(AdlsTableValue folderContentsTable, List<string> blobUriFilter)
			{
				this.folderContentsTable = folderContentsTable;
				this.blobUriFilter = blobUriFilter;
			}

			// Token: 0x17001D65 RID: 7525
			// (get) Token: 0x060064E7 RID: 25831 RVA: 0x0015A388 File Offset: 0x00158588
			public override Keys Columns
			{
				get
				{
					return this.FolderContentsTable.Columns;
				}
			}

			// Token: 0x060064E8 RID: 25832 RVA: 0x0015A395 File Offset: 0x00158595
			public override TypeValue GetColumnType(int column)
			{
				return this.FolderContentsTable.GetColumnType(column);
			}

			// Token: 0x17001D66 RID: 7526
			// (get) Token: 0x060064E9 RID: 25833 RVA: 0x0015A3A3 File Offset: 0x001585A3
			public override IList<TableKey> TableKeys
			{
				get
				{
					return this.FolderContentsTable.TableKeys;
				}
			}

			// Token: 0x17001D67 RID: 7527
			// (get) Token: 0x060064EA RID: 25834 RVA: 0x0015A3B0 File Offset: 0x001585B0
			public override IList<ComputedColumn> ComputedColumns
			{
				get
				{
					return this.FolderContentsTable.ComputedColumns;
				}
			}

			// Token: 0x17001D68 RID: 7528
			// (get) Token: 0x060064EB RID: 25835 RVA: 0x0015A3BD File Offset: 0x001585BD
			public override TableSortOrder SortOrder
			{
				get
				{
					return this.FolderContentsTable.SortOrder;
				}
			}

			// Token: 0x17001D69 RID: 7529
			// (get) Token: 0x060064EC RID: 25836 RVA: 0x0015A3CA File Offset: 0x001585CA
			public override IEngineHost EngineHost
			{
				get
				{
					return this.folderContentsTable.Host;
				}
			}

			// Token: 0x17001D6A RID: 7530
			// (get) Token: 0x060064ED RID: 25837 RVA: 0x0015A3D7 File Offset: 0x001585D7
			private AdlsTableValue FolderContentsTable
			{
				get
				{
					return this.folderContentsTable;
				}
			}

			// Token: 0x060064EE RID: 25838 RVA: 0x0015A3E0 File Offset: 0x001585E0
			public override bool TryGetExpression(out IExpression expression)
			{
				expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(new AzureDataLakeStorageModule.DataLakeFunctionValue(this.folderContentsTable.host)), new ConstantExpressionSyntaxNode(this.folderContentsTable.FolderUrl), new ConstantExpressionSyntaxNode(this.folderContentsTable.options.AsRecord));
				FunctionValue filterFunction = this.GetFilterFunction();
				if (!filterFunction.Equals(ConstantFunctionValue.EachTrue))
				{
					expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.SelectRows), expression, filterFunction.Expression);
				}
				if (this.FolderContentsTable.version != null)
				{
					expression = expression.AccessVersion(this.FolderContentsTable.version.Identity);
				}
				return true;
			}

			// Token: 0x060064EF RID: 25839 RVA: 0x0015A482 File Offset: 0x00158682
			public override IEnumerable<IValueReference> GetRows()
			{
				return new TableQuery(this.FolderContentsTable, this.EngineHost).SelectRows(this.GetFilterFunction()).GetRows();
			}

			// Token: 0x060064F0 RID: 25840 RVA: 0x0015A4A5 File Offset: 0x001586A5
			public override Query Take(RowCount count)
			{
				if (count.IsZero)
				{
					return new AdlsTableValue.FolderContentsQuery(this.FolderContentsTable, new List<string>());
				}
				return base.Take(count);
			}

			// Token: 0x060064F1 RID: 25841 RVA: 0x0015A4C8 File Offset: 0x001586C8
			public override Query SelectRows(FunctionValue condition)
			{
				int num = this.Columns.IndexOfKey("Name");
				List<QueryExpression> disjunctiveNF = SelectRowsQuery.GetDisjunctiveNF(QueryExpressionBuilder.ToQueryExpression(this, condition));
				List<string> list = new List<string>();
				using (List<QueryExpression>.Enumerator enumerator = disjunctiveNF.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num2;
						BinaryOperator2 binaryOperator;
						Value value;
						if (!enumerator.Current.TryGetColumnComparison(out num2, out binaryOperator, out value) || binaryOperator != BinaryOperator2.Equals || num2 != num || !value.IsText)
						{
							list = null;
							break;
						}
						list.Add(UriHelper.Combine(new Uri(this.FolderContentsTable.FolderUrl.AsString), value.AsText).AbsoluteUri);
					}
				}
				if (list != null)
				{
					if (this.blobUriFilter != null)
					{
						list = this.blobUriFilter.Intersect(list).ToList<string>();
					}
					return new AdlsTableValue.FolderContentsQuery(this.FolderContentsTable, list);
				}
				return base.SelectRows(condition);
			}

			// Token: 0x060064F2 RID: 25842 RVA: 0x0015A5B4 File Offset: 0x001587B4
			public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
			{
				if (function.Equals(Library._Value.VersionIdentity))
				{
					result = ((this.FolderContentsTable.Version != null) ? TextValue.New(this.FolderContentsTable.Version.Identity) : Value.Null);
					return true;
				}
				if (function.Equals(Library._Value.Versions) && arguments.Length == 1 && this.blobUriFilter != null)
				{
					Func<AdlsVersions.Version, Value> tableCtor = (AdlsVersions.Version version) => new AdlsTableValue(this.FolderContentsTable.host, this.FolderContentsTable.adlsEndpoints, this.FolderContentsTable.resourceKind, this.FolderContentsTable.options, this.FolderContentsTable.isHierarchical, version).SelectRows(this.GetFilterFunction());
					Action<AdlsVersions.Version> action = delegate(AdlsVersions.Version version)
					{
						HashSet<string> hashSet = new HashSet<string>(this.blobUriFilter);
						using (IEnumerator<IValueReference> enumerator = tableCtor(version).AsTable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								AdlsBinaryValue adlsBinaryValue;
								if (!enumerator.Current.Value.AsRecord["Content"].TryGetAs<AdlsBinaryValue>(out adlsBinaryValue))
								{
									throw ValueException.NewExpressionError<Message0>(Strings.Value_Versions_NotSupported, null, null);
								}
								TableValue asTable = Library._Value.Versions.Invoke(adlsBinaryValue).AsTable;
								TableValue asTable2 = TableModule.Table.FromColumns.Invoke(ListValue.New(new Value[] { ListValue.New(new Value[] { TextValue.New(version.Identity) }) }), ListValue.New(new Value[] { TextValue.New("Version") })).AsTable;
								ActionModule.TableAction.InsertRows.Invoke(asTable, asTable2).AsAction.Execute();
								hashSet.Remove(adlsBinaryValue.BlobUrl.AsString);
							}
						}
						foreach (string text in hashSet)
						{
							version.TrackUrl(text, null);
						}
					};
					IResource resource = Resource.New(this.FolderContentsTable.ResourceKind, this.FolderContentsTable.FolderUrl.AsString);
					TableValue tableValue;
					if (new AdlsValueVersions(AdlsVersions.Instance, this.FolderContentsTable.host, resource, this.blobUriFilter, action, tableCtor).TryCreateTable(out tableValue))
					{
						result = tableValue;
						return true;
					}
				}
				result = null;
				return false;
			}

			// Token: 0x060064F3 RID: 25843 RVA: 0x0015A6A1 File Offset: 0x001588A1
			public override ActionValue InsertRows(Query rowsToInsert)
			{
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.FolderContentsTable.InsertRows(new QueryTableValue(rowsToInsert), countOnlyTable));
			}

			// Token: 0x060064F4 RID: 25844 RVA: 0x0015A6C6 File Offset: 0x001588C6
			public override ActionValue UpdateRows(ColumnUpdates columnUpdates)
			{
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.FolderContentsTable.UpdateRows(columnUpdates, this.GetFilterFunction(), countOnlyTable));
			}

			// Token: 0x060064F5 RID: 25845 RVA: 0x0015A6EB File Offset: 0x001588EB
			public override ActionValue DeleteRows()
			{
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.FolderContentsTable.DeleteRows(this.GetFilterFunction(), countOnlyTable));
			}

			// Token: 0x060064F6 RID: 25846 RVA: 0x0015A700 File Offset: 0x00158900
			private FunctionValue GetFilterFunction()
			{
				if (this.blobUriFilter == null)
				{
					return ConstantFunctionValue.EachTrue;
				}
				int num = this.Columns.IndexOfKey("Name");
				QueryExpression queryExpression = null;
				foreach (string text in this.blobUriFilter)
				{
					QueryExpression queryExpression2 = new BinaryQueryExpression(BinaryOperator2.Equals, new ColumnAccessQueryExpression(num), new ConstantQueryExpression(TextValue.New(UriHelper.GetFileName(text))));
					if (queryExpression == null)
					{
						queryExpression = queryExpression2;
					}
					else
					{
						queryExpression = new BinaryQueryExpression(BinaryOperator2.Or, queryExpression, queryExpression2);
					}
				}
				if (queryExpression != null)
				{
					return QueryExpressionAssembler.Assemble(this.Columns, queryExpression);
				}
				return ConstantFunctionValue.EachFalse;
			}

			// Token: 0x04003731 RID: 14129
			private readonly AdlsTableValue folderContentsTable;

			// Token: 0x04003732 RID: 14130
			private readonly List<string> blobUriFilter;
		}
	}
}
