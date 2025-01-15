using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.Mashup.Pdf;
using Microsoft.ProgramSynthesis.Extraction.Pdf;
using Microsoft.ProgramSynthesis.Extraction.Pdf.DocumentFeatures;

namespace Microsoft.Mashup.Engine1.Library.Pdf
{
	// Token: 0x02002035 RID: 8245
	public sealed class PdfModule : Module45
	{
		// Token: 0x17002DC4 RID: 11716
		// (get) Token: 0x060112E9 RID: 70377 RVA: 0x003B27EA File Offset: 0x003B09EA
		public override string Name
		{
			get
			{
				return "Pdf";
			}
		}

		// Token: 0x17002DC5 RID: 11717
		// (get) Token: 0x060112EA RID: 70378 RVA: 0x003B27F1 File Offset: 0x003B09F1
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
							return "Pdf.Tables";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x17002DC6 RID: 11718
		// (get) Token: 0x060112EB RID: 70379 RVA: 0x003B282C File Offset: 0x003B0A2C
		public override ResourceManager DocumentationResources
		{
			get
			{
				return Resources.ResourceManager;
			}
		}

		// Token: 0x060112EC RID: 70380 RVA: 0x003B2834 File Offset: 0x003B0A34
		protected override RecordValue GetModuleExports(RecordValue environment, IEngineHost host)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new PdfModule.TablesFunctionValue(host);
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x0400682F RID: 26671
		public const string PdfTablesName = "Pdf.Tables";

		// Token: 0x04006830 RID: 26672
		private Keys exportKeys;

		// Token: 0x02002036 RID: 8246
		private enum Exports
		{
			// Token: 0x04006832 RID: 26674
			Pdf_Tables,
			// Token: 0x04006833 RID: 26675
			Count
		}

		// Token: 0x02002037 RID: 8247
		private class TablesFunctionValue : NativeFunctionValue2<TableValue, BinaryValue, Value>
		{
			// Token: 0x060112EE RID: 70382 RVA: 0x003B2865 File Offset: 0x003B0A65
			public TablesFunctionValue(IEngineHost host)
				: base(PdfModule.TablesFunctionValue.resultType, 1, "pdf", TypeValue.Binary, "options", PdfModule.TablesFunctionValue.optionType)
			{
				this.host = host;
			}

			// Token: 0x17002DC7 RID: 11719
			// (get) Token: 0x060112EF RID: 70383 RVA: 0x003B288E File Offset: 0x003B0A8E
			public override RecordValue MetaValue
			{
				get
				{
					if (this.metaValue == null)
					{
						this.metaValue = base.MetaValue.Concatenate(PdfModule.TablesFunctionValue.PublishMetaValue).AsRecord;
					}
					return this.metaValue;
				}
			}

			// Token: 0x17002DC8 RID: 11720
			// (get) Token: 0x060112F0 RID: 70384 RVA: 0x003B28BC File Offset: 0x003B0ABC
			private static RecordValue PublishMetaValue
			{
				get
				{
					return RecordValue.New(Keys.New("Publish"), new Value[] { RecordValue.New(Keys.New("ButtonText", "SourceImage", "SourceTypeImage"), new Value[]
					{
						ListValue.New(new Value[]
						{
							TextValue.New(Resources.Pdf_Tables_ButtonTitle),
							TextValue.New(Resources.Pdf_Tables_ButtonHelp)
						}),
						PdfModule.TablesFunctionValue.SourceImageIcons,
						PdfModule.TablesFunctionValue.SourceTypeImageIcons
					}) });
				}
			}

			// Token: 0x17002DC9 RID: 11721
			// (get) Token: 0x060112F1 RID: 70385 RVA: 0x003B2944 File Offset: 0x003B0B44
			private static RecordValue SourceImageIcons
			{
				get
				{
					if (PdfModule.TablesFunctionValue.sourceImageIcons == null)
					{
						PdfModule.TablesFunctionValue.sourceImageIcons = RecordValue.New(Keys.New("Icon32"), new Value[] { ListValue.New(new Value[]
						{
							new PdfModule.ResourceBinaryValue("PdfFileLarge"),
							new PdfModule.ResourceBinaryValue("PdfFileLarge_40"),
							new PdfModule.ResourceBinaryValue("PdfFileLarge_48"),
							new PdfModule.ResourceBinaryValue("PdfFileLarge_64"),
							new PdfModule.ResourceBinaryValue("PdfFileLarge_80"),
							new PdfModule.ResourceBinaryValue("PdfFileLarge_96")
						}) });
					}
					return PdfModule.TablesFunctionValue.sourceImageIcons;
				}
			}

			// Token: 0x17002DCA RID: 11722
			// (get) Token: 0x060112F2 RID: 70386 RVA: 0x003B29D4 File Offset: 0x003B0BD4
			private static RecordValue SourceTypeImageIcons
			{
				get
				{
					if (PdfModule.TablesFunctionValue.sourceTypeImageIcons == null)
					{
						PdfModule.TablesFunctionValue.sourceTypeImageIcons = RecordValue.New(Keys.New("Icon16", "Icon32"), new Value[]
						{
							ListValue.New(new Value[]
							{
								new PdfModule.ResourceBinaryValue("PdfFile"),
								new PdfModule.ResourceBinaryValue("PdfFile_20"),
								new PdfModule.ResourceBinaryValue("PdfFile_24"),
								new PdfModule.ResourceBinaryValue("PdfFile_32"),
								new PdfModule.ResourceBinaryValue("PdfFile_40"),
								new PdfModule.ResourceBinaryValue("PdfFile_48")
							}),
							ListValue.New(new Value[]
							{
								new PdfModule.ResourceBinaryValue("PdfFileLarge"),
								new PdfModule.ResourceBinaryValue("PdfFileLarge_40"),
								new PdfModule.ResourceBinaryValue("PdfFileLarge_48"),
								new PdfModule.ResourceBinaryValue("PdfFileLarge_64"),
								new PdfModule.ResourceBinaryValue("PdfFileLarge_80"),
								new PdfModule.ResourceBinaryValue("PdfFileLarge_96")
							})
						});
					}
					return PdfModule.TablesFunctionValue.sourceTypeImageIcons;
				}
			}

			// Token: 0x060112F3 RID: 70387 RVA: 0x003B2AC8 File Offset: 0x003B0CC8
			public override TableValue TypedInvoke(BinaryValue pdf, Value options)
			{
				PdfTablesOptions pdfTablesOptions = new PdfTablesOptions(PdfModule.TablesFunctionValue.optionRecord.CreateOptions("Pdf.Tables", options));
				string text;
				string sourceKeyOrDigest = DataSource.GetSourceKeyOrDigest(pdf, out text);
				string text2;
				TableValue navTable;
				using (Stream stream = this.GetSeekableBinaryValue(pdf, text, sourceKeyOrDigest).Open(out text2))
				{
					IPersistentCache persistentCache = this.host.GetPersistentCache();
					string text3 = PersistentCacheKey.PdfTables.Qualify(text, sourceKeyOrDigest, options.CreateCacheKey(), text2);
					Stream stream2;
					if (!persistentCache.TryGetValue(text3, out stream2))
					{
						MemoryStream memoryStream = new MemoryStream();
						PdfModule.TablesFunctionValue.PdfTableInfo.SerializeArray(this.GetTableInfos(stream, pdfTablesOptions), new BinaryWriter(memoryStream, new UTF8Encoding(false, false)));
						memoryStream.Position = 0L;
						stream2 = persistentCache.Add(text3, memoryStream);
					}
					PdfModule.TablesFunctionValue.PdfTableInfo[] array = PdfModule.TablesFunctionValue.PdfTableInfo.DeserializeArray(new BinaryReader(stream2));
					stream2.Close();
					navTable = this.GetNavTable(array);
				}
				return navTable;
			}

			// Token: 0x060112F4 RID: 70388 RVA: 0x003B2BBC File Offset: 0x003B0DBC
			private PdfModule.TablesFunctionValue.PdfTableInfo[] GetTableInfos(Stream pdfStream, PdfTablesOptions pdfTablesOptions)
			{
				PdfModule.TablesFunctionValue.PdfTableInfo[] array;
				try
				{
					PdfAnalyzerVersion? pdfAnalyzerVersion = new PdfAnalyzerVersion?(pdfTablesOptions.Version.GetValueOrDefault(PdfAnalyzerVersion.V1));
					InferredTablesMode? inferredTablesMode = new InferredTablesMode?(InferredTablesMode.AllCandidates);
					PdfAnalyzerOptions pdfAnalyzerOptions = new PdfAnalyzerOptions(pdfAnalyzerVersion, pdfTablesOptions.StartPageIndex, pdfTablesOptions.EndPageIndex, inferredTablesMode, pdfTablesOptions.EnforceBorderLines.GetValueOrDefault(), pdfTablesOptions.MultiPageTables.GetValueOrDefault(true), true, true, null, null);
					using (PdfAnalyzer pdfAnalyzer = PdfAnalyzer.LoadPdf(pdfStream, null, pdfAnalyzerOptions))
					{
						array = (from t in pdfAnalyzer.AnalyzeAllPages()
							select PdfModule.TablesFunctionValue.PdfTableInfo.From(t)).ToArray<PdfModule.TablesFunctionValue.PdfTableInfo>();
					}
				}
				catch (PdfLoadException ex)
				{
					throw ValueException.NewDataSourceError(ex.Message, Value.Null, ex);
				}
				return array;
			}

			// Token: 0x060112F5 RID: 70389 RVA: 0x003B2CA0 File Offset: 0x003B0EA0
			private TableValue GetNavTable(PdfModule.TablesFunctionValue.PdfTableInfo[] tableInfos)
			{
				List<Value> list = new List<Value>();
				foreach (PdfModule.TablesFunctionValue.PdfTableInfo pdfTableInfo in tableInfos)
				{
					list.Add(ListValue.New(new Value[]
					{
						TextValue.New(pdfTableInfo.Id),
						TextValue.New(pdfTableInfo.Name),
						TextValue.New(pdfTableInfo.Kind),
						this.GetDataTable(pdfTableInfo.Data)
					}));
				}
				return TableModule.Table.Literal.Invoke(PdfModule.TablesFunctionValue.resultType, ListValue.New(list)).AsTable;
			}

			// Token: 0x060112F6 RID: 70390 RVA: 0x003B2D2C File Offset: 0x003B0F2C
			private TableValue GetDataTable(string[,] data)
			{
				int length = data.GetLength(0);
				List<string> list = new List<string>();
				List<RecordValue> list2 = new List<RecordValue>();
				for (int i = 0; i < length; i++)
				{
					list.Add("Column" + (i + 1).ToString());
					list2.Add(RecordTypeAlgebra.NewField(NullableTypeValue.Text, false));
				}
				Keys keys = Keys.New(list.ToArray());
				Value[] array = list2.ToArray();
				TypeValue typeValue = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(keys, array)));
				int length2 = data.GetLength(1);
				List<ListValue> list3 = new List<ListValue>(length2);
				for (int j = 0; j < length2; j++)
				{
					List<Value> list4 = new List<Value>();
					for (int k = 0; k < length; k++)
					{
						string text = data[k, j];
						list4.Add(TextValue.NewOrNull(text));
					}
					list3.Add(ListValue.New(list4));
				}
				return ValueServices.AddFirstRowMayContainHeadersMeta(ValueServices.AddShouldInferTableTypeMeta(TableModule.Table.Literal.Invoke(typeValue, ListValue.New(list3)).AsTable));
			}

			// Token: 0x060112F7 RID: 70391 RVA: 0x003B2E34 File Offset: 0x003B1034
			private SeekableBinaryValue GetSeekableBinaryValue(BinaryValue pdf, string pdfKeyType, string pdfKey)
			{
				IPersistentCache persistentCache = this.host.GetPersistentCache();
				string text = null;
				string text2 = PersistentCacheKey.PdfTables.Qualify(pdfKeyType, pdfKey, text, "SeekableBinaryValue");
				return new SeekableBinaryValue(persistentCache, text2, pdf);
			}

			// Token: 0x04006834 RID: 26676
			private static readonly OptionRecordDefinition optionRecord = new OptionRecordDefinition(Resources.ResourceManager, new OptionItem[]
			{
				new OptionItem("Implementation", NullableTypeValue.Text, Value.Null, OptionItemOption.None, null, null),
				new OptionItem("StartPage", NullableTypeValue.Int32, Value.Null, OptionItemOption.None, null, null),
				new OptionItem("EndPage", NullableTypeValue.Int32, Value.Null, OptionItemOption.None, null, null),
				new OptionItem("MultiPageTables", NullableTypeValue.Logical, Value.Null, OptionItemOption.None, null, null),
				new OptionItem("EnforceBorderLines", NullableTypeValue.Logical, Value.Null, OptionItemOption.None, null, null)
			});

			// Token: 0x04006835 RID: 26677
			private static readonly TypeValue optionType = PdfModule.TablesFunctionValue.optionRecord.CreateRecordType().Nullable;

			// Token: 0x04006836 RID: 26678
			private static readonly TypeValue resultType = NavigationTableServices.AddNavigationTableMetadataWithKind(TableTypeValue.New(RecordTypeValue.New(RecordValue.New(Keys.New("Id", "Name", "Kind", "Data"), new Value[]
			{
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Text, false),
				RecordTypeAlgebra.NewField(TypeValue.Table, false)
			})), new TableKey[]
			{
				new TableKey(new int[1], true)
			}));

			// Token: 0x04006837 RID: 26679
			private static RecordValue sourceImageIcons;

			// Token: 0x04006838 RID: 26680
			private static RecordValue sourceTypeImageIcons;

			// Token: 0x04006839 RID: 26681
			private readonly IEngineHost host;

			// Token: 0x0400683A RID: 26682
			private RecordValue metaValue;

			// Token: 0x02002038 RID: 8248
			private class PdfTableInfo
			{
				// Token: 0x17002DCB RID: 11723
				// (get) Token: 0x060112F9 RID: 70393 RVA: 0x003B2FA9 File Offset: 0x003B11A9
				// (set) Token: 0x060112FA RID: 70394 RVA: 0x003B2FB1 File Offset: 0x003B11B1
				public string Id { get; private set; }

				// Token: 0x17002DCC RID: 11724
				// (get) Token: 0x060112FB RID: 70395 RVA: 0x003B2FBA File Offset: 0x003B11BA
				// (set) Token: 0x060112FC RID: 70396 RVA: 0x003B2FC2 File Offset: 0x003B11C2
				public string Name { get; private set; }

				// Token: 0x17002DCD RID: 11725
				// (get) Token: 0x060112FD RID: 70397 RVA: 0x003B2FCB File Offset: 0x003B11CB
				// (set) Token: 0x060112FE RID: 70398 RVA: 0x003B2FD3 File Offset: 0x003B11D3
				public string Kind { get; private set; }

				// Token: 0x17002DCE RID: 11726
				// (get) Token: 0x060112FF RID: 70399 RVA: 0x003B2FDC File Offset: 0x003B11DC
				// (set) Token: 0x06011300 RID: 70400 RVA: 0x003B2FE4 File Offset: 0x003B11E4
				public string[,] Data { get; private set; }

				// Token: 0x06011301 RID: 70401 RVA: 0x003B2FF0 File Offset: 0x003B11F0
				public static PdfModule.TablesFunctionValue.PdfTableInfo[] DeserializeArray(BinaryReader reader)
				{
					int num = reader.ReadInt32();
					PdfModule.TablesFunctionValue.PdfTableInfo[] array = new PdfModule.TablesFunctionValue.PdfTableInfo[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = PdfModule.TablesFunctionValue.PdfTableInfo.Deserialize(reader);
					}
					return array;
				}

				// Token: 0x06011302 RID: 70402 RVA: 0x003B3024 File Offset: 0x003B1224
				public static PdfModule.TablesFunctionValue.PdfTableInfo From(IPdfTable table)
				{
					return new PdfModule.TablesFunctionValue.PdfTableInfo
					{
						Id = table.TableIdentity.Identifier,
						Name = ((table.Kind == TableKind.Page) ? table.TableIdentity.Identifier : string.Concat(new string[]
						{
							table.TableIdentity.Identifier,
							" (Page ",
							(table.StartingPageIndex + 1).ToString(),
							(table.StartingPageIndex != table.EndingPageIndex) ? ("-" + (table.EndingPageIndex + 1).ToString()) : "",
							")"
						})),
						Kind = ((table.Kind == TableKind.Page) ? "Page" : "Table"),
						Data = table.GetTextTable()
					};
				}

				// Token: 0x06011303 RID: 70403 RVA: 0x003B30FC File Offset: 0x003B12FC
				public static void SerializeArray(PdfModule.TablesFunctionValue.PdfTableInfo[] tableInfos, BinaryWriter writer)
				{
					writer.Write(tableInfos.Length);
					for (int i = 0; i < tableInfos.Length; i++)
					{
						tableInfos[i].Serialize(writer);
					}
				}

				// Token: 0x06011304 RID: 70404 RVA: 0x003B312C File Offset: 0x003B132C
				private static PdfModule.TablesFunctionValue.PdfTableInfo Deserialize(BinaryReader reader)
				{
					PdfModule.TablesFunctionValue.PdfTableInfo pdfTableInfo = new PdfModule.TablesFunctionValue.PdfTableInfo();
					pdfTableInfo.Id = reader.ReadString();
					pdfTableInfo.Name = reader.ReadString();
					pdfTableInfo.Kind = reader.ReadString();
					int num = reader.ReadInt32();
					int num2 = reader.ReadInt32();
					pdfTableInfo.Data = new string[num, num2];
					for (int i = 0; i < num; i++)
					{
						for (int j = 0; j < num2; j++)
						{
							pdfTableInfo.Data[i, j] = reader.ReadNullableString();
						}
					}
					return pdfTableInfo;
				}

				// Token: 0x06011305 RID: 70405 RVA: 0x003B31B0 File Offset: 0x003B13B0
				private void Serialize(BinaryWriter writer)
				{
					writer.Write(this.Id);
					writer.Write(this.Name);
					writer.Write(this.Kind);
					int length = this.Data.GetLength(0);
					writer.Write(length);
					int length2 = this.Data.GetLength(1);
					writer.Write(length2);
					for (int i = 0; i < length; i++)
					{
						for (int j = 0; j < length2; j++)
						{
							writer.WriteNullableString(this.Data[i, j]);
						}
					}
				}
			}
		}

		// Token: 0x0200203A RID: 8250
		private class ResourceBinaryValue : StreamedBinaryValue
		{
			// Token: 0x0601130A RID: 70410 RVA: 0x003B3248 File Offset: 0x003B1448
			public ResourceBinaryValue(string name)
			{
				this.name = name;
			}

			// Token: 0x0601130B RID: 70411 RVA: 0x003B3258 File Offset: 0x003B1458
			public override Stream Open()
			{
				string text = "Microsoft.Mashup.Pdf.Images." + this.name + ".png";
				return typeof(PdfModule.ResourceBinaryValue).Assembly.GetManifestResourceStream(text);
			}

			// Token: 0x04006841 RID: 26689
			private readonly string name;
		}
	}
}
