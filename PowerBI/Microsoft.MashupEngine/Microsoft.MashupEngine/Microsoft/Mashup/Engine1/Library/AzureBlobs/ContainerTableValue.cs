using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.AzureBase;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.AzureBlobs
{
	// Token: 0x02000EF6 RID: 3830
	internal sealed class ContainerTableValue : TableValue
	{
		// Token: 0x0600658C RID: 25996 RVA: 0x0015CF00 File Offset: 0x0015B100
		public ContainerTableValue(IEngineHost host, TextValue containerUrl, string resourceKind, OptionsRecord options, RecordValue queryParameters, bool runLocalFilter = false, bool oldAttributes = false)
		{
			this.host = host;
			this.containerUrl = containerUrl;
			this.options = options;
			this.queryParameters = queryParameters;
			this.runLocalFilter = runLocalFilter;
			this.resource = Resource.New(resourceKind, containerUrl.String);
			this.oldAttributes = oldAttributes;
		}

		// Token: 0x17001D88 RID: 7560
		// (get) Token: 0x0600658D RID: 25997 RVA: 0x0015CF53 File Offset: 0x0015B153
		public override TypeValue Type
		{
			get
			{
				return FileSystemTableHelper.AddTypeMetadata(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFilesDeep), this.containerUrl.AsString, "/", false);
			}
		}

		// Token: 0x17001D89 RID: 7561
		// (get) Token: 0x0600658E RID: 25998 RVA: 0x0015CF71 File Offset: 0x0015B171
		public override Query Query
		{
			get
			{
				return new ContainerTableValue.ContainerTableQuery(this);
			}
		}

		// Token: 0x0600658F RID: 25999 RVA: 0x0015CF7C File Offset: 0x0015B17C
		public override bool TryGetValue(Value key, out Value value)
		{
			if (key.IsRecord)
			{
				RecordValue asRecord = key.AsRecord;
				Value value2;
				Value value3;
				if (asRecord.TryGetValue("Folder Path", out value2) && value2.IsText && asRecord.TryGetValue("Name", out value3) && value3.IsText && value2.AsString.StartsWith(this.containerUrl.AsString, StringComparison.Ordinal) && asRecord.Keys.Length == 2)
				{
					TextValue textValue = TextValue.New(AzureBaseHelper.FormatContainerString(value2.AsString) + AzureBaseHelper.EscapeBlobName(value3.AsString));
					string text;
					try
					{
						text = Path.GetExtension(textValue.AsString);
					}
					catch (ArgumentException)
					{
						text = string.Empty;
					}
					BinaryValue binaryValue = this.NewBinaryValue(textValue, null);
					value = RecordValue.New(FileHelper.FileEntryKeys, new IValueReference[]
					{
						binaryValue,
						value3,
						TextValue.New(text),
						Value.Null,
						new ContainerTableValue.DateModifiedValueReference(this, textValue),
						Value.Null,
						new ContainerTableValue.AttributeValueReference(this, textValue),
						value2
					});
					return true;
				}
			}
			return base.TryGetValue(key, out value);
		}

		// Token: 0x06006590 RID: 26000 RVA: 0x0015D0B0 File Offset: 0x0015B2B0
		public override TableValue SelectRows(FunctionValue condition)
		{
			TableValue tableValue;
			if (!this.runLocalFilter && this.TrySelectRows(condition, out tableValue))
			{
				return tableValue;
			}
			return base.SelectRows(condition);
		}

		// Token: 0x06006591 RID: 26001 RVA: 0x0015D0DC File Offset: 0x0015B2DC
		public override TableValue Take(RowCount count)
		{
			RecordValue asRecord = this.queryParameters.Concatenate(RecordValue.New(Keys.New("maxresults"), new Value[] { TextValue.New(count.Value.ToString(CultureInfo.InvariantCulture)) })).AsRecord;
			return new QueryTableValue(this.NewContainerTableValue(asRecord, this.runLocalFilter));
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x0015D140 File Offset: 0x0015B340
		private bool TrySelectRows(FunctionValue condition, out TableValue tableValue)
		{
			QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this.Type.AsTableType.ItemType, condition);
			InvocationQueryExpression invocationQueryExpression = queryExpression as InvocationQueryExpression;
			if (invocationQueryExpression != null)
			{
				ConstantQueryExpression constantQueryExpression = invocationQueryExpression.Function as ConstantQueryExpression;
				if (constantQueryExpression != null && constantQueryExpression.Value.Equals(Library.Text.StartsWith))
				{
					string text;
					if (this.resource.Kind == "HDInsight" && this.TryGetColumnText("Folder Path", invocationQueryExpression.Arguments[0], invocationQueryExpression.Arguments[1], out text) && AzureBaseHelper.IsValidPrefix(text, this.containerUrl.String))
					{
						text = Uri.UnescapeDataString(text.Substring(this.containerUrl.String.Length));
						bool flag = text.EndsWith("/", StringComparison.OrdinalIgnoreCase);
						if (flag)
						{
							tableValue = this.GetTableValueForSelectRows(text, false);
							return true;
						}
						if (text.IndexOf('/') >= 0 && !flag)
						{
							text = text.Substring(0, text.LastIndexOf('/') + 1);
						}
						tableValue = this.GetTableValueForSelectRows(text, true).SelectRows(condition);
						return true;
					}
					else if (this.TryGetColumnText("Name", invocationQueryExpression.Arguments[0], invocationQueryExpression.Arguments[1], out text) && this.resource.Kind == "AzureBlobs")
					{
						tableValue = this.GetTableValueForSelectRows(text, false);
						return true;
					}
				}
			}
			BinaryQueryExpression binaryQueryExpression = queryExpression as BinaryQueryExpression;
			if (binaryQueryExpression != null && binaryQueryExpression.Operator == BinaryOperator2.Equals)
			{
				string text;
				if (this.resource.Kind == "HDInsight" && this.TryGetColumnText("Folder Path", binaryQueryExpression.Left, binaryQueryExpression.Right, out text) && AzureBaseHelper.IsValidPrefix(text, this.containerUrl.String))
				{
					text = Uri.UnescapeDataString(text.Substring(this.containerUrl.String.Length));
					tableValue = this.GetTableValueForSelectRows(text, true).SelectRows(condition);
					return true;
				}
				if (this.TryGetColumnText("Name", binaryQueryExpression.Left, binaryQueryExpression.Right, out text) && this.resource.Kind == "AzureBlobs")
				{
					tableValue = this.GetTableValueForSelectRows(text, true).SelectRows(condition);
					return true;
				}
			}
			tableValue = null;
			return false;
		}

		// Token: 0x06006593 RID: 26003 RVA: 0x0015D380 File Offset: 0x0015B580
		private TableValue GetTableValueForSelectRows(string prefixValue, bool hasLocalFilter)
		{
			if (prefixValue.Length == 0)
			{
				return this;
			}
			RecordValue asRecord = this.queryParameters.Concatenate(RecordValue.New(Keys.New("prefix"), new Value[] { TextValue.New(prefixValue) })).AsRecord;
			return this.NewContainerTableValue(asRecord, hasLocalFilter);
		}

		// Token: 0x06006594 RID: 26004 RVA: 0x0015D3D0 File Offset: 0x0015B5D0
		private bool TryGetColumnText(string columnName, QueryExpression query1, QueryExpression query2, out string text)
		{
			ColumnAccessQueryExpression columnAccessQueryExpression = query1 as ColumnAccessQueryExpression;
			if (columnAccessQueryExpression != null && this.Columns[columnAccessQueryExpression.Column] == columnName)
			{
				ConstantQueryExpression constantQueryExpression = query2 as ConstantQueryExpression;
				if (constantQueryExpression != null && constantQueryExpression.Value.IsText)
				{
					text = constantQueryExpression.Value.AsString;
					return true;
				}
			}
			text = null;
			return false;
		}

		// Token: 0x06006595 RID: 26005 RVA: 0x0015D42C File Offset: 0x0015B62C
		private void VerifyActionPermitted()
		{
			this.host.VerifyActionPermitted(this.resource);
		}

		// Token: 0x06006596 RID: 26006 RVA: 0x0015D43F File Offset: 0x0015B63F
		public override IEnumerator<IValueReference> GetEnumerator()
		{
			return new ContainerTableValue.BlobListEnumerator(this.host, this.containerUrl, this.resource.Kind, this.queryParameters, this.options, this.oldAttributes);
		}

		// Token: 0x06006597 RID: 26007 RVA: 0x0015D46F File Offset: 0x0015B66F
		private Request CreateRequest(TextValue blobUri, Value query = null)
		{
			return BlobsHelper.CreateRequest(this.host, this.resource, blobUri, query, null, null);
		}

		// Token: 0x06006598 RID: 26008 RVA: 0x0015D488 File Offset: 0x0015B688
		private BinaryValue NewBinaryValue(TextValue blobUri, Value contentType = null)
		{
			return BlobBinaryValue.New(this.host, this.resource, blobUri, this.options, contentType, null, null, null);
		}

		// Token: 0x06006599 RID: 26009 RVA: 0x0015D4B9 File Offset: 0x0015B6B9
		private ContainerTableValue NewContainerTableValue(RecordValue queryOptions, bool hasLocalFilter)
		{
			return new ContainerTableValue(this.host, this.containerUrl, this.resource.Kind, this.options, queryOptions, hasLocalFilter, this.oldAttributes);
		}

		// Token: 0x0600659A RID: 26010 RVA: 0x0015D4E8 File Offset: 0x0015B6E8
		private ContainerTableValue.BlobProperties GetBlobProperties(TextValue blobUrl)
		{
			Request request = this.CreateRequest(blobUrl, null);
			request.Method = "HEAD";
			ContainerTableValue.BlobProperties blobProperties;
			using (Response response = AzureBaseHelper.GetResponse(request, new Request.SecurityExceptionCreator(BlobsHelper.TryCreateSecurityException), null))
			{
				blobProperties = new ContainerTableValue.BlobProperties(response.Headers["Last-Modified"], response.ContentType, response.ContentLength);
			}
			return blobProperties;
		}

		// Token: 0x040037BA RID: 14266
		private readonly IEngineHost host;

		// Token: 0x040037BB RID: 14267
		private readonly TextValue containerUrl;

		// Token: 0x040037BC RID: 14268
		private readonly IResource resource;

		// Token: 0x040037BD RID: 14269
		private readonly OptionsRecord options;

		// Token: 0x040037BE RID: 14270
		private readonly RecordValue queryParameters;

		// Token: 0x040037BF RID: 14271
		private readonly bool runLocalFilter;

		// Token: 0x040037C0 RID: 14272
		private readonly bool oldAttributes;

		// Token: 0x02000EF7 RID: 3831
		private class BlobListEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
		{
			// Token: 0x0600659B RID: 26011 RVA: 0x0015D55C File Offset: 0x0015B75C
			public BlobListEnumerator(IEngineHost host, TextValue containerUrl, string resourceKind, RecordValue queryParameters, OptionsRecord options, bool oldAttributes = false)
			{
				this.host = host;
				this.containerUrl = containerUrl;
				this.resource = Resource.New(resourceKind, containerUrl.String);
				this.queryParameters = queryParameters;
				this.options = options;
				this.oldAttributes = oldAttributes;
				this.Initialize();
			}

			// Token: 0x17001D8A RID: 7562
			// (get) Token: 0x0600659C RID: 26012 RVA: 0x0015D5AD File Offset: 0x0015B7AD
			public IValueReference Current
			{
				get
				{
					return this.current;
				}
			}

			// Token: 0x17001D8B RID: 7563
			// (get) Token: 0x0600659D RID: 26013 RVA: 0x0015D5B5 File Offset: 0x0015B7B5
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600659E RID: 26014 RVA: 0x0015D5BD File Offset: 0x0015B7BD
			public void Dispose()
			{
				this.current = null;
				if (this.listEnumerator != null)
				{
					this.listEnumerator.Dispose();
					this.listEnumerator = null;
				}
			}

			// Token: 0x0600659F RID: 26015 RVA: 0x0015D5E0 File Offset: 0x0015B7E0
			public bool MoveNext()
			{
				while (this.listEnumerator != null)
				{
					if (this.listEnumerator.MoveNext())
					{
						RecordValue asRecord = this.listEnumerator.Current.Value.AsRecord;
						this.current = ContainerRecordValue.New(this.host, this.resource, asRecord, this.options, this.oldAttributes);
						return true;
					}
					this.Dispose();
					this.TryInitialize();
				}
				return false;
			}

			// Token: 0x060065A0 RID: 26016 RVA: 0x0000EE09 File Offset: 0x0000D009
			public void Reset()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060065A1 RID: 26017 RVA: 0x0015D650 File Offset: 0x0015B850
			private ListValue LoadBlobsList(out RecordValue newQueryParameters)
			{
				long num = -1L;
				RecordValue recordValue = BlobsHelper.ListBlobs;
				if (this.queryParameters != null)
				{
					foreach (NamedValue namedValue in this.queryParameters.GetFields())
					{
						if (namedValue.Key == "maxresults")
						{
							num = Convert.ToInt64(namedValue.Value.AsString, CultureInfo.InvariantCulture);
							if (num <= 5000L && num > 0L)
							{
								recordValue = recordValue.Concatenate(RecordValue.New(Keys.New("maxresults"), new Value[] { namedValue.Value })).AsRecord;
							}
						}
						else
						{
							recordValue = recordValue.Concatenate(RecordValue.New(Keys.New(namedValue.Key), new Value[] { namedValue.Value })).AsRecord;
						}
					}
				}
				Request request = BlobsHelper.CreateRequest(this.host, this.resource, this.containerUrl, recordValue, null, null);
				List<Value> list = new List<Value>();
				using (XmlReader xmlReader = XmlHelperUtility.XmlReaderCreate(AzureBaseHelper.GetResponse(request, new Request.SecurityExceptionCreator(BlobsHelper.TryCreateSecurityException), null).GetResponseStream()))
				{
					XmlDocument xmlDocument = XmlHelperUtility.CreateXmlDocument();
					try
					{
						xmlDocument.Load(xmlReader);
					}
					catch (XmlException ex)
					{
						Message3 message = Strings.HDInsightFailedXmlException(this.resource.Kind, request.InitialUri, ex.Message);
						throw HttpServices.NewDataSourceError<Message3>(this.host, message, this.resource, request.InitialUri);
					}
					catch (IOException)
					{
						Message2 message2 = Strings.AzureStorageIOException(this.resource.Kind, request.InitialUri);
						throw HttpServices.NewDataSourceError<Message2>(this.host, message2, this.resource, request.InitialUri);
					}
					BlobsHelper.CheckResponseForErrors(this.host, xmlDocument, this.resource);
					XmlNodeList elementsByTagName = xmlDocument.GetElementsByTagName("Blob");
					XmlNodeList elementsByTagName2 = xmlDocument.GetElementsByTagName("NextMarker");
					for (int i = 0; i < elementsByTagName.Count; i++)
					{
						string innerText = elementsByTagName[i]["Name"].InnerText;
						string text = AzureBaseHelper.FormatContainerString(this.containerUrl.String) + AzureBaseHelper.EscapeBlobName(innerText);
						XmlElement xmlElement = elementsByTagName[i]["Properties"]["Etag"];
						TextValue textValue = BlobsHelper.FormatEtag((xmlElement != null) ? xmlElement.InnerText : null);
						XmlElement xmlElement2 = elementsByTagName[i]["Properties"]["Content-Encoding"];
						Value value = TextValue.NewOrNull((xmlElement2 != null) ? xmlElement2.InnerText : null);
						RecordValue recordValue2 = RecordValue.New(BlobsHelper.BlobRecordKeys, new Value[]
						{
							TextValue.New(innerText),
							TextValue.New(text),
							AzureBaseHelper.ConvertToDateTime(elementsByTagName[i]["Properties"]["Last-Modified"].InnerText),
							NumberValue.New(Convert.ToInt64(elementsByTagName[i]["Properties"]["Content-Length"].InnerText, CultureInfo.InvariantCulture)),
							TextValue.New(elementsByTagName[i]["Properties"]["Content-Type"].InnerText),
							this.containerUrl,
							textValue,
							value
						});
						list.Add(recordValue2);
					}
					newQueryParameters = null;
					if (elementsByTagName2.Count > 0 && elementsByTagName2[0].FirstChild != null && (num > (long)list.Count || num == -1L))
					{
						newQueryParameters = RecordValue.New(Keys.New("marker"), new Value[] { TextValue.New(elementsByTagName2[0].FirstChild.InnerText) });
						if (num > (long)list.Count)
						{
							num -= (long)list.Count;
						}
						if (this.queryParameters != null)
						{
							foreach (NamedValue namedValue2 in this.queryParameters.GetFields())
							{
								if (namedValue2.Key != "marker")
								{
									if (namedValue2.Key == "maxresults")
									{
										newQueryParameters = newQueryParameters.Concatenate(RecordValue.New(Keys.New("maxresults"), new Value[] { TextValue.New(num.ToString(CultureInfo.InvariantCulture)) })).AsRecord;
									}
									else
									{
										newQueryParameters = newQueryParameters.Concatenate(RecordValue.New(Keys.New(namedValue2.Key), new Value[] { namedValue2.Value })).AsRecord;
									}
								}
							}
						}
					}
				}
				return ListValue.New(list.ToArray());
			}

			// Token: 0x060065A2 RID: 26018 RVA: 0x0015DB98 File Offset: 0x0015BD98
			private void Initialize()
			{
				RecordValue recordValue;
				this.listEnumerator = this.LoadBlobsList(out recordValue).GetEnumerator();
				this.queryParameters = recordValue;
			}

			// Token: 0x060065A3 RID: 26019 RVA: 0x0015DBC0 File Offset: 0x0015BDC0
			private void TryInitialize()
			{
				Value value;
				if (this.queryParameters != null && this.queryParameters.TryGetValue("marker", out value))
				{
					this.Initialize();
				}
			}

			// Token: 0x040037C1 RID: 14273
			private readonly IEngineHost host;

			// Token: 0x040037C2 RID: 14274
			private readonly TextValue containerUrl;

			// Token: 0x040037C3 RID: 14275
			private readonly IResource resource;

			// Token: 0x040037C4 RID: 14276
			private readonly OptionsRecord options;

			// Token: 0x040037C5 RID: 14277
			private readonly bool oldAttributes;

			// Token: 0x040037C6 RID: 14278
			private RecordValue current;

			// Token: 0x040037C7 RID: 14279
			private IEnumerator<IValueReference> listEnumerator;

			// Token: 0x040037C8 RID: 14280
			private RecordValue queryParameters;
		}

		// Token: 0x02000EF8 RID: 3832
		private sealed class ContainerTableQuery : FilteredTableQuery
		{
			// Token: 0x060065A4 RID: 26020 RVA: 0x0015DBEF File Offset: 0x0015BDEF
			public ContainerTableQuery(ContainerTableValue table)
				: base(table, table.host)
			{
			}

			// Token: 0x17001D8C RID: 7564
			// (get) Token: 0x060065A5 RID: 26021 RVA: 0x0015DBFE File Offset: 0x0015BDFE
			private ContainerTableValue ContainerTable
			{
				get
				{
					return (ContainerTableValue)base.Table;
				}
			}

			// Token: 0x060065A6 RID: 26022 RVA: 0x0015DC0C File Offset: 0x0015BE0C
			public override bool TryGetExpression(out IExpression expression)
			{
				expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(new AzureBlobsModule.AccountContainersFunctionValue(this.ContainerTable.host)), new ConstantExpressionSyntaxNode(this.ContainerTable.containerUrl), new ConstantExpressionSyntaxNode(this.ContainerTable.options.AsRecord));
				return true;
			}

			// Token: 0x060065A7 RID: 26023 RVA: 0x0015DC5B File Offset: 0x0015BE5B
			public override ActionValue InsertRows(Query rowsToInsert)
			{
				this.ContainerTable.VerifyActionPermitted();
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.InsertRows(rowsToInsert, countOnlyTable));
			}

			// Token: 0x060065A8 RID: 26024 RVA: 0x0015DC8C File Offset: 0x0015BE8C
			private ActionValue InsertRows(Query rowsToInsert, bool countOnlyTable)
			{
				if (!countOnlyTable)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
				}
				long num = 0L;
				List<IValueReference> list = new List<IValueReference>();
				foreach (IValueReference valueReference in rowsToInsert.GetRows())
				{
					num += 1L;
					RecordValue asRecord = valueReference.Value.AsRecord;
					BinaryValue asBinary = asRecord["Content"].AsBinary;
					TextValue name = asRecord["Name"].AsText;
					TextValue textValue = null;
					if (ContainerTableValue.ContainerTableQuery.FieldOrDefault(asRecord, "Extension", FileHelper.GetFileExtension(name.AsString).AsString) != FileHelper.GetFileExtension(name.AsString).AsString)
					{
						throw ValueException.NewExpressionError<Message0>(Strings.File_ExtensionMustMatchName, asRecord, null);
					}
					if (asRecord.Keys.Contains("Folder Path"))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.File_SetFolderPathNotSupported, asRecord, null);
					}
					if (asRecord.Keys.Contains("Date accessed") || asRecord.Keys.Contains("Date modified") || asRecord.Keys.Contains("Date created"))
					{
						throw ValueException.NewExpressionError<Message0>(Strings.File_SetDatesNotSupported, asRecord, null);
					}
					int num2 = 127;
					Value value;
					if (asRecord.TryGetValue("Attributes", out value))
					{
						string text = "Content Type";
						using (Keys.StringKeysEnumerator enumerator2 = value.AsRecord.Keys.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								if (enumerator2.Current != text)
								{
									throw ValueException.NewExpressionError<Message1>(Strings.File_SetAttributesNotSupported_But(text), asRecord, null);
								}
							}
						}
						textValue = value[text].AsText;
						if (textValue != null && textValue.Length > num2)
						{
							throw ValueException.NewDataFormatError<Message0>(Strings.File_SetAttributeContentTypeTooLarge, NumberValue.New(textValue.Length), null);
						}
					}
					list.Add(ActionValue.New(delegate
					{
						if (this.ContainerTable.SelectRows(ContainerTableValue.ContainerTableQuery.NewNameEquals(name)).Any<IValueReference>())
						{
							throw ValueException.NewDataSourceError<Message1>(Strings.File_FileExists(name.AsString), new QueryTableValue(this), null);
						}
						return Value.Null;
					}));
					string text2 = AzureBaseHelper.FormatContainerString(this.ContainerTable.containerUrl.String) + AzureBaseHelper.EscapeBlobName(name.AsString);
					BinaryValue binaryValue = this.ContainerTable.NewBinaryValue(TextValue.New(text2), textValue);
					list.Add(binaryValue.Replace(asBinary));
				}
				list.Add(ActionModule.Action.Return.Invoke(NumberValue.New(num)));
				list.Add(new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType));
				return ActionValue.New(ListValue.New(list)).ClearCache(this.ContainerTable.host);
			}

			// Token: 0x060065A9 RID: 26025 RVA: 0x0015DF70 File Offset: 0x0015C170
			public override ActionValue DeleteRows(FunctionValue selector)
			{
				this.ContainerTable.VerifyActionPermitted();
				return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.DeleteRows(selector, countOnlyTable));
			}

			// Token: 0x060065AA RID: 26026 RVA: 0x0015DFA0 File Offset: 0x0015C1A0
			private ActionValue DeleteRows(FunctionValue selector, bool countOnlyTable)
			{
				if (!countOnlyTable)
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, new QueryTableValue(this), null);
				}
				long num = 0L;
				List<IValueReference> list = new List<IValueReference>();
				foreach (IValueReference valueReference in base.Table.SelectRows(selector))
				{
					num += 1L;
					TextValue asText = valueReference.Value.AsRecord["Name"].AsText;
					string blobUrl = AzureBaseHelper.FormatContainerString(this.ContainerTable.containerUrl.String) + AzureBaseHelper.EscapeBlobName(asText.AsString);
					list.Add(ActionValue.New(delegate
					{
						Request request = this.ContainerTable.CreateRequest(TextValue.New(blobUrl), null);
						request.Method = "DELETE";
						request.UseCache = false;
						AzureBaseHelper.ExecuteRequest(request, new Request.SecurityExceptionCreator(BlobsHelper.TryCreateSecurityException), null);
						return Value.Null;
					}));
				}
				list.Add(ActionModule.Action.Return.Invoke(NumberValue.New(num)));
				list.Add(new ReturnTypedTableFromCountFunctionValue(new QueryTableValue(this).Type.AsTableType));
				return ActionValue.New(ListValue.New(list)).ClearCache(this.ContainerTable.host);
			}

			// Token: 0x060065AB RID: 26027 RVA: 0x0015E0C8 File Offset: 0x0015C2C8
			private static string FieldOrDefault(RecordValue record, string field, string defaultString)
			{
				Value value;
				if (record.TryGetValue(field, out value))
				{
					return value.AsString;
				}
				return defaultString;
			}

			// Token: 0x060065AC RID: 26028 RVA: 0x0015E0E8 File Offset: 0x0015C2E8
			private static FunctionValue NewNameEquals(TextValue name)
			{
				IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(Microsoft.Mashup.Engine1.Language.Query.QueryHelpers.EachFunctionType, BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore), Identifier.New("Name")), new ConstantExpressionSyntaxNode(name), TokenRange.Null));
				return new Compiler(CompileOptions.None).ToFunction(functionExpression);
			}
		}

		// Token: 0x02000EFD RID: 3837
		private sealed class BlobProperties
		{
			// Token: 0x060065B5 RID: 26037 RVA: 0x0015E20A File Offset: 0x0015C40A
			public BlobProperties(string lastDateModified, string contentType, long length)
			{
				this.lastDateModified = lastDateModified;
				this.contentType = contentType;
				this.length = length;
			}

			// Token: 0x17001D8D RID: 7565
			// (get) Token: 0x060065B6 RID: 26038 RVA: 0x0015E227 File Offset: 0x0015C427
			public string LastDateModified
			{
				get
				{
					return this.lastDateModified;
				}
			}

			// Token: 0x17001D8E RID: 7566
			// (get) Token: 0x060065B7 RID: 26039 RVA: 0x0015E22F File Offset: 0x0015C42F
			public string ContentType
			{
				get
				{
					return this.contentType;
				}
			}

			// Token: 0x17001D8F RID: 7567
			// (get) Token: 0x060065B8 RID: 26040 RVA: 0x0015E237 File Offset: 0x0015C437
			public long Length
			{
				get
				{
					return this.length;
				}
			}

			// Token: 0x040037D1 RID: 14289
			private readonly string lastDateModified;

			// Token: 0x040037D2 RID: 14290
			private readonly string contentType;

			// Token: 0x040037D3 RID: 14291
			private readonly long length;
		}

		// Token: 0x02000EFE RID: 3838
		private sealed class DateModifiedValueReference : IValueReference
		{
			// Token: 0x060065B9 RID: 26041 RVA: 0x0015E23F File Offset: 0x0015C43F
			public DateModifiedValueReference(ContainerTableValue table, TextValue uri)
			{
				this.table = table;
				this.uri = uri;
			}

			// Token: 0x17001D90 RID: 7568
			// (get) Token: 0x060065BA RID: 26042 RVA: 0x0015E255 File Offset: 0x0015C455
			public bool Evaluated
			{
				get
				{
					return this.value != null;
				}
			}

			// Token: 0x17001D91 RID: 7569
			// (get) Token: 0x060065BB RID: 26043 RVA: 0x0015E260 File Offset: 0x0015C460
			public Value Value
			{
				get
				{
					if (this.value == null)
					{
						ContainerTableValue.BlobProperties blobProperties = this.table.GetBlobProperties(this.uri);
						this.value = AzureBaseHelper.ConvertToDateTime(blobProperties.LastDateModified);
					}
					return this.value;
				}
			}

			// Token: 0x040037D4 RID: 14292
			private readonly ContainerTableValue table;

			// Token: 0x040037D5 RID: 14293
			private readonly TextValue uri;

			// Token: 0x040037D6 RID: 14294
			private Value value;
		}

		// Token: 0x02000EFF RID: 3839
		private sealed class AttributeValueReference : IValueReference
		{
			// Token: 0x060065BC RID: 26044 RVA: 0x0015E29E File Offset: 0x0015C49E
			public AttributeValueReference(ContainerTableValue table, TextValue uri)
			{
				this.table = table;
				this.uri = uri;
			}

			// Token: 0x17001D92 RID: 7570
			// (get) Token: 0x060065BD RID: 26045 RVA: 0x0015E2B4 File Offset: 0x0015C4B4
			public bool Evaluated
			{
				get
				{
					return this.value != null;
				}
			}

			// Token: 0x17001D93 RID: 7571
			// (get) Token: 0x060065BE RID: 26046 RVA: 0x0015E2C0 File Offset: 0x0015C4C0
			public Value Value
			{
				get
				{
					if (this.value == null)
					{
						ContainerTableValue.BlobProperties blobProperties = this.table.GetBlobProperties(this.uri);
						this.value = (this.table.oldAttributes ? RecordValue.New(AzureBaseHelper.ContentsAttributeKeys, new Value[]
						{
							TextValue.New(blobProperties.ContentType),
							NumberValue.New(blobProperties.Length)
						}) : RecordValue.New(AzureBaseHelper.AttributeKeys, new Value[]
						{
							TextValue.New(blobProperties.ContentType),
							FileHelper.GetFileKind(blobProperties.ContentType),
							NumberValue.New(blobProperties.Length)
						}));
					}
					return this.value;
				}
			}

			// Token: 0x040037D7 RID: 14295
			private readonly ContainerTableValue table;

			// Token: 0x040037D8 RID: 14296
			private readonly TextValue uri;

			// Token: 0x040037D9 RID: 14297
			private Value value;
		}
	}
}
