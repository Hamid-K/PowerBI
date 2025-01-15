using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Mashup.Cdm;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Mashup.Engine1.Library.Lines;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Cdm
{
	// Token: 0x02000006 RID: 6
	public sealed class CdmModule : Module45
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002380 File Offset: 0x00000580
		protected override RecordValue GetModuleExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new CdmModule.CdmContentsFunctionValue(host);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023B1 File Offset: 0x000005B1
		public override string Name
		{
			get
			{
				return "CdmView";
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023B8 File Offset: 0x000005B8
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "Cdm.Contents";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x04000005 RID: 5
		private const string CdmContents = "Cdm.Contents";

		// Token: 0x04000006 RID: 6
		private Keys exportKeys;

		// Token: 0x02000007 RID: 7
		public sealed class CdmEntitiesTableValue : TableValue
		{
			// Token: 0x0600001F RID: 31 RVA: 0x000023FC File Offset: 0x000005FC
			public CdmEntitiesTableValue(IEngineHost host, ICdmProvider cdmProvider, ICdmMashup cdmMashup, TableValue table, TextValue folioFilePathTextValue)
			{
				this.host = host;
				this.cdmProvider = cdmProvider;
				this.cdmMashup = cdmMashup;
				this.table = table;
				this.folioFilePath = folioFilePathTextValue.String;
				if (!Utils.TryParsePath(this.folioFilePath, ref this.baseFolderPath, ref this.folioFileName))
				{
					throw ValueException.NewDataSourceError<Message1>(Resources.Cdm_InvalidFileName(this.folioFilePath), folioFilePathTextValue, null);
				}
			}

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000020 RID: 32 RVA: 0x00002466 File Offset: 0x00000666
			public override TypeValue Type
			{
				get
				{
					return NavigationTableServices.DefaultTypeValue;
				}
			}

			// Token: 0x06000021 RID: 33 RVA: 0x0000246D File Offset: 0x0000066D
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.GetEntitiesNavTable().GetEnumerator();
			}

			// Token: 0x06000022 RID: 34 RVA: 0x0000247C File Offset: 0x0000067C
			private TableValue GetEntitiesNavTable()
			{
				CdmHelper.GetBinaryFileContent(this.table, this.folioFilePath);
				CdmManifest manifest = this.cdmProvider.GetManifest(this.baseFolderPath, this.folioFileName, this.cdmMashup);
				return ListValue.New(CdmModule.CdmEntitiesTableValue.GenerateNavTableRecords(this.host, this.table, manifest)).ToTable(this.Type.AsTableType);
			}

			// Token: 0x06000023 RID: 35 RVA: 0x000024E0 File Offset: 0x000006E0
			private static IEnumerable<IValueReference> GenerateNavTableRecords(IEngineHost host, TableValue table, CdmManifest manifest)
			{
				FunctionValue csvFunction = new LinesModule.Csv.DocumentFunctionValue(host);
				ICulture culture = Culture.GetCulture(host, TextValue.New(manifest.Culture ?? string.Empty));
				foreach (CdmEntity cdmEntity in manifest.Data)
				{
					List<Value> list = new List<Value>();
					List<ListValue> list2 = new List<ListValue>();
					foreach (CdmEntity.Column column in cdmEntity.Columns)
					{
						TextValue textValue = TextValue.New(column.Name);
						list.Add(textValue);
						TypeValue any;
						if (!CdmModule.CdmEntitiesTableValue.dataTypeMapping.TryGetValue(column.DataType, out any))
						{
							any = TypeValue.Any;
						}
						list2.Add(ListValue.New(new Value[] { textValue, any }));
					}
					ListValue listValue = ListValue.New(list.ToArray());
					Value[] array = list2.ToArray();
					ListValue listValue2 = ListValue.New(array);
					TableValue tableValue = LinesModule.Table.FromList.Invoke(ListValue.Empty, Value.Null, listValue).AsTable;
					if (cdmEntity.DataPartitions.Count != 0)
					{
						List<TableValue> list3 = new List<TableValue>();
						foreach (CdmEntity.DataPartition dataPartition in cdmEntity.DataPartitions)
						{
							BinaryValue binaryFileContent = CdmHelper.GetBinaryFileContent(table, dataPartition.Location);
							PartitionFormat format = dataPartition.Format;
							TableValue tableValue2;
							if (format != null)
							{
								if (format != 1)
								{
									throw new InvalidOperationException();
								}
								RecordValue recordValue = RecordValue.New(Keys.New("LegacyColumnNameEncoding"), new Value[] { LogicalValue.False });
								tableValue2 = CdmModule.CdmEntitiesTableValue.GetParquetDocument(host).Invoke(binaryFileContent, recordValue).AsTable;
							}
							else
							{
								tableValue2 = csvFunction.Invoke(binaryFileContent, CdmModule.CdmEntitiesTableValue.BuildPartitionOption(dataPartition, listValue)).AsTable;
								if (dataPartition.HasColumnHeaders)
								{
									tableValue2 = tableValue2.Skip(new RowCount(1L));
								}
							}
							list3.Add(tableValue2);
						}
						array = list3.ToArray();
						ListValue listValue3 = ListValue.New(array);
						tableValue = CdmModule.CdmEntitiesTableValue.CombineTables(listValue, listValue3, host);
					}
					TableValue tableValue3 = tableValue.TransformColumnTypes(host, listValue2, culture);
					RecordValue recordValue2 = RecordValue.New(NavigationTableServices.MetadataValues, new Value[]
					{
						TextValue.New(cdmEntity.Name),
						tableValue3
					});
					yield return recordValue2;
				}
				List<CdmEntity>.Enumerator enumerator = default(List<CdmEntity>.Enumerator);
				yield break;
				yield break;
			}

			// Token: 0x06000024 RID: 36 RVA: 0x000024FE File Offset: 0x000006FE
			private static FunctionValue GetParquetDocument(IEngineHost engineHost)
			{
				return Modules.GetLibrary(engineHost, null)["Parquet.Document"].AsFunction;
			}

			// Token: 0x06000025 RID: 37 RVA: 0x00002518 File Offset: 0x00000718
			private static TableValue CombineTables(ListValue columns, ListValue rows, IEngineHost host)
			{
				FunctionValue asFunction = Modules.GetLibrary(host, null)["Table.ExpandTableColumn"].AsFunction;
				TableValue tableValue = ListValue.New(CdmModule.CdmEntitiesTableValue.TableFromList(rows, "Partition")).ToTable();
				return asFunction.Invoke(tableValue, TextValue.New("Partition"), columns).AsTable;
			}

			// Token: 0x06000026 RID: 38 RVA: 0x00002567 File Offset: 0x00000767
			private static IEnumerable<IValueReference> TableFromList(ListValue rows, string columnName)
			{
				foreach (IValueReference valueReference in rows)
				{
					Value value = (Value)valueReference;
					RecordValue recordValue = RecordValue.New(Keys.New(columnName), new Value[] { value });
					yield return recordValue;
				}
				IEnumerator<IValueReference> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00002580 File Offset: 0x00000780
			private static RecordValue BuildPartitionOption(CdmEntity.DataPartition partition, ListValue columnsNames)
			{
				RecordBuilder recordBuilder = new RecordBuilder(5);
				recordBuilder.Add("Columns", columnsNames, TypeValue.Any);
				if (partition.CsvType != null)
				{
					NumberValue numberValue;
					if (!CdmModule.CdmEntitiesTableValue.csvTypeMapping.TryGetValue(partition.CsvType, out numberValue))
					{
						throw ValueException.NewDataSourceError<Message3>(Resources.Cdm_InvalidEntityPartitionParameter(partition.CsvType, "CsvStyle", partition.CsvType), TextValue.Empty, null);
					}
					recordBuilder.Add("CsvStyle", numberValue, TypeValue.Any);
				}
				if (partition.Delimiter != null)
				{
					recordBuilder.Add("Delimiter", TextValue.New(partition.Delimiter), TypeValue.Any);
				}
				if (partition.Encoding != null)
				{
					int num;
					if (!int.TryParse(partition.Encoding, out num) && !CdmModule.CdmEntitiesTableValue.codepageIdEncodingMapping.TryGetValue(partition.Encoding, out num))
					{
						throw ValueException.NewDataSourceError<Message3>(Resources.Cdm_InvalidEntityPartitionParameter(partition.Location, "Encoding", partition.Encoding), TextValue.Empty, null);
					}
					recordBuilder.Add("Encoding", NumberValue.New(num), TypeValue.Any);
				}
				if (partition.QuoteStyle != null)
				{
					Value value;
					if (!CdmModule.CdmEntitiesTableValue.quoteStyleMapping.TryGetValue(partition.QuoteStyle, out value))
					{
						throw ValueException.NewDataSourceError<Message3>(Resources.Cdm_InvalidEntityPartitionParameter(partition.Location, "QuoteStyle", partition.QuoteStyle), TextValue.Empty, null);
					}
					recordBuilder.Add("QuoteStyle", value, TypeValue.Any);
				}
				return recordBuilder.ToRecord();
			}

			// Token: 0x04000007 RID: 7
			private static readonly Dictionary<string, TypeValue> dataTypeMapping = new Dictionary<string, TypeValue>(StringComparer.OrdinalIgnoreCase)
			{
				{
					"int16",
					TypeValue.Int16
				},
				{
					"int32",
					TypeValue.Int32
				},
				{
					"int64",
					TypeValue.Int64
				},
				{
					"float",
					TypeValue.Single
				},
				{
					"double",
					TypeValue.Double
				},
				{
					"guid",
					TypeValue.Guid
				},
				{
					"string",
					TypeValue.Text
				},
				{
					"char",
					TypeValue.Text
				},
				{
					"byte",
					TypeValue.Byte
				},
				{
					"binary",
					TypeValue.Binary
				},
				{
					"time",
					TypeValue.Time
				},
				{
					"date",
					TypeValue.Date
				},
				{
					"datetime",
					TypeValue.DateTime
				},
				{
					"datetimeoffset",
					TypeValue.DateTimeZone
				},
				{
					"boolean",
					TypeValue.Logical
				},
				{
					"decimal",
					TypeValue.Decimal
				},
				{
					"json",
					TypeValue.Text
				}
			};

			// Token: 0x04000008 RID: 8
			private static readonly Dictionary<string, int> codepageIdEncodingMapping = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
			{
				{ "UTF-8", 65001 },
				{ "ISO-8859-1", 28591 },
				{ "WINDOWS-1252", 1252 }
			};

			// Token: 0x04000009 RID: 9
			private static readonly Dictionary<string, NumberValue> csvTypeMapping = new Dictionary<string, NumberValue>(StringComparer.OrdinalIgnoreCase)
			{
				{
					"CsvStyle.QuoteAfterDelimiter",
					NumberValue.New(0)
				},
				{
					"CsvStyle.QuoteAlways",
					NumberValue.New(1)
				}
			};

			// Token: 0x0400000A RID: 10
			private static readonly Dictionary<string, Value> quoteStyleMapping = new Dictionary<string, Value>(StringComparer.OrdinalIgnoreCase)
			{
				{
					"QuoteStyle.None",
					LinesModule.QuoteStyle.None.Value
				},
				{
					"QuoteStyle.Csv",
					LinesModule.QuoteStyle.Csv.Value
				}
			};

			// Token: 0x0400000B RID: 11
			private readonly ICdmProvider cdmProvider;

			// Token: 0x0400000C RID: 12
			private readonly ICdmMashup cdmMashup;

			// Token: 0x0400000D RID: 13
			private readonly IEngineHost host;

			// Token: 0x0400000E RID: 14
			private readonly TableValue table;

			// Token: 0x0400000F RID: 15
			private readonly Uri baseFolderPath;

			// Token: 0x04000010 RID: 16
			private readonly string folioFilePath;

			// Token: 0x04000011 RID: 17
			private readonly string folioFileName;
		}

		// Token: 0x0200000A RID: 10
		public sealed class CdmContentsFunctionValue : NativeFunctionValue1<TableValue, TableValue>
		{
			// Token: 0x0600003B RID: 59 RVA: 0x00002E6F File Offset: 0x0000106F
			public CdmContentsFunctionValue(IEngineHost host)
				: base(FileHelper.FolderResultTypeValue(FileHelper.FolderOptions.EnumerateFoldersAndFiles), 1, "table", TypeValue.Table)
			{
				this.host = host;
			}

			// Token: 0x0600003C RID: 60 RVA: 0x00002E8F File Offset: 0x0000108F
			public override TableValue TypedInvoke(TableValue table)
			{
				return CdmModule.CdmFolderTableValue.CreateNavigationTable(this.host, table);
			}

			// Token: 0x04000026 RID: 38
			private readonly IEngineHost host;
		}

		// Token: 0x0200000B RID: 11
		private sealed class CdmFolderTableValue : TableValue
		{
			// Token: 0x0600003D RID: 61 RVA: 0x00002E9D File Offset: 0x0000109D
			internal CdmFolderTableValue(IEngineHost host, ICdmProvider cdmProvider, ICdmMashup cdmMashup, TableValue table)
			{
				this.host = host;
				this.cdmProvider = cdmProvider;
				this.cdmMashup = cdmMashup;
				this.table = table;
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600003E RID: 62 RVA: 0x00002EC2 File Offset: 0x000010C2
			public override TypeValue Type
			{
				get
				{
					return CdmModule.CdmFolderTableValue.type;
				}
			}

			// Token: 0x0600003F RID: 63 RVA: 0x00002EC9 File Offset: 0x000010C9
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				return this.GetEntitiesNavTable().GetEnumerator();
			}

			// Token: 0x06000040 RID: 64 RVA: 0x00002ED8 File Offset: 0x000010D8
			public static TableValue CreateNavigationTable(IEngineHost host, TableValue table)
			{
				CdmModule.CdmFolderTableValue.ValidateArgument(table);
				ICdmProvider cdmProvider = new MashupCdmProvider(host);
				ICdmMashup cdmMashup = new CdmMashup(host, table, false);
				return ListValue.New(CdmModule.CdmFolderTableValue.GenerateNavTableRcords(host, cdmProvider, cdmMashup, table)).ToTable(CdmModule.CdmFolderTableValue.type.AsTableType);
			}

			// Token: 0x06000041 RID: 65 RVA: 0x00002F18 File Offset: 0x00001118
			private TableValue GetEntitiesNavTable()
			{
				return ListValue.New(CdmModule.CdmFolderTableValue.GenerateNavTableRcords(this.host, this.cdmProvider, this.cdmMashup, this.table)).ToTable(this.Type.AsTableType);
			}

			// Token: 0x06000042 RID: 66 RVA: 0x00002F4C File Offset: 0x0000114C
			private static void ValidateArgument(TableValue table)
			{
				Keys columns = table.Columns;
				foreach (string text in FileHelper.TableEntryKeys)
				{
					if (!columns.Contains(text))
					{
						throw ValueException.NewDataSourceError<Message1>(Resources.Cdm_InvalidTableParameter(text), TextValue.Empty, null);
					}
				}
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00002FBC File Offset: 0x000011BC
			private static IEnumerable<IValueReference> GenerateNavTableRcords(IEngineHost host, ICdmProvider cdmProvider, ICdmMashup cdmMashup, TableValue table)
			{
				ICdmMashup cdmMashupToGetManifestName = new CdmMashup(host, table, false);
				foreach (IValueReference valueReference in table)
				{
					Value value = valueReference.Value.AsRecord["Content"];
					Value value2 = valueReference.Value.AsRecord["Folder Path"];
					Value value3 = valueReference.Value.AsRecord["Name"];
					string folder = value2.AsString;
					string name = value3.AsString;
					if (value.IsTable)
					{
						TableValue tableValue = new CdmModule.CdmFolderTableValue(host, cdmProvider, cdmMashup, value.AsTable);
						RecordValue recordValue = RecordValue.New(CdmModule.CdmFolderTableValue.keys, new Value[]
						{
							TextValue.New(name),
							TextValue.New(name),
							tableValue,
							TextValue.New("Folder")
						});
						yield return recordValue;
					}
					else if (value.IsBinary)
					{
						foreach (string text in CdmModule.CdmFolderTableValue.patterns)
						{
							if (Regex.IsMatch(name, text, RegexOptions.IgnoreCase))
							{
								string text2 = folder + name;
								TableValue tableValue2 = new CdmModule.CdmEntitiesTableValue(host, cdmProvider, cdmMashup, table, TextValue.New(text2));
								Uri uri;
								string text3;
								Utils.TryParsePath(text2, ref uri, ref text3);
								string text4 = cdmProvider.GetManifestName(uri, text3, cdmMashupToGetManifestName);
								if (string.IsNullOrEmpty(text4))
								{
									text4 = name.Split(new char[] { '.' }).FirstOrDefault<string>();
								}
								RecordValue recordValue2 = RecordValue.New(CdmModule.CdmFolderTableValue.keys, new Value[]
								{
									TextValue.New(name),
									TextValue.New(text4),
									tableValue2,
									TextValue.New("Database")
								});
								yield return recordValue2;
							}
						}
						List<string>.Enumerator enumerator2 = default(List<string>.Enumerator);
					}
					folder = null;
					name = null;
				}
				IEnumerator<IValueReference> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x04000027 RID: 39
			private static List<string> patterns = new List<string> { "^.*\\.manifest\\.cdm\\.json$", "^model\\.json$" };

			// Token: 0x04000028 RID: 40
			private static Keys keys = Keys.New("Id", "Name", "Data", "Kind");

			// Token: 0x04000029 RID: 41
			private static TypeValue type = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(CdmModule.CdmFolderTableValue.keys, new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(NavigationTableServices.ConvertToLink(TypeValue.Table, "Folder", false), false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false)
			})), new TableKey[]
			{
				new TableKey(new int[1], true)
			}));

			// Token: 0x0400002A RID: 42
			private readonly ICdmProvider cdmProvider;

			// Token: 0x0400002B RID: 43
			private readonly ICdmMashup cdmMashup;

			// Token: 0x0400002C RID: 44
			private readonly IEngineHost host;

			// Token: 0x0400002D RID: 45
			private readonly TableValue table;
		}

		// Token: 0x0200000D RID: 13
		private enum Exports
		{
			// Token: 0x0400003F RID: 63
			CdmContents,
			// Token: 0x04000040 RID: 64
			Count
		}
	}
}
