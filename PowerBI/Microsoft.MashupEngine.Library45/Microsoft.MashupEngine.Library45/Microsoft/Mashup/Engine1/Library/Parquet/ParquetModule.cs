using System;
using System.IO;
using System.Resources;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.Parquet;

namespace Microsoft.Mashup.Engine1.Library.Parquet
{
	// Token: 0x02001F2D RID: 7981
	public sealed class ParquetModule : Module45
	{
		// Token: 0x17002C70 RID: 11376
		// (get) Token: 0x06010CA4 RID: 68772 RVA: 0x0039D485 File Offset: 0x0039B685
		public override ResourceManager DocumentationResources
		{
			get
			{
				return Resources.ResourceManager;
			}
		}

		// Token: 0x06010CA5 RID: 68773 RVA: 0x0039D48C File Offset: 0x0039B68C
		protected override RecordValue GetModuleExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return new ParquetModule.Parquet.DocumentFunctionValue(hostEnvironment);
				}
				if (index != 1)
				{
					throw new InvalidOperationException();
				}
				return new ParquetModule.Parquet.MetadataFunctionValue(hostEnvironment);
			});
		}

		// Token: 0x17002C71 RID: 11377
		// (get) Token: 0x06010CA6 RID: 68774 RVA: 0x0039D4BD File Offset: 0x0039B6BD
		public override string Name
		{
			get
			{
				return "Parquet";
			}
		}

		// Token: 0x17002C72 RID: 11378
		// (get) Token: 0x06010CA7 RID: 68775 RVA: 0x0039D4C4 File Offset: 0x0039B6C4
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(2, delegate(int index)
					{
						if (index == 0)
						{
							return "Parquet.Document";
						}
						if (index != 1)
						{
							throw new InvalidOperationException();
						}
						return "Parquet.Metadata";
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x0400649F RID: 25759
		private Keys exportKeys;

		// Token: 0x02001F2E RID: 7982
		public static class Parquet
		{
			// Token: 0x02001F2F RID: 7983
			public class DocumentFunctionValue : NativeFunctionValue2<TableValue, BinaryValue, Value>
			{
				// Token: 0x06010CA9 RID: 68777 RVA: 0x0039D4FF File Offset: 0x0039B6FF
				public DocumentFunctionValue(IEngineHost host)
					: base(TypeValue.Any, 1, "binary", TypeValue.Binary, "options", ParquetModule.Parquet.DocumentFunctionValue.optionsType)
				{
					this.host = host;
				}

				// Token: 0x17002C73 RID: 11379
				// (get) Token: 0x06010CAA RID: 68778 RVA: 0x0039D528 File Offset: 0x0039B728
				public override IFunctionIdentity FunctionIdentity
				{
					get
					{
						return new FunctionTypeIdentity(typeof(ParquetModule.Parquet.DocumentFunctionValue));
					}
				}

				// Token: 0x06010CAB RID: 68779 RVA: 0x0039D53C File Offset: 0x0039B73C
				public override TableValue TypedInvoke(BinaryValue binary, Value options)
				{
					OptionsRecord optionsRecord = ParquetApi.OptionRecord.CreateOptions("Parquet", options);
					if (ParquetOptions.GetColumnNameEncodingOption(optionsRecord, false))
					{
						throw ValueException.NewExpressionError<Message0>(Resources.LegacyColumnNameEncoding, null, null);
					}
					return new ParquetModule.Parquet.DocumentFunctionValue.ParquetDocumentTableValue(ParquetQuery.New(this.host, binary, optionsRecord));
				}

				// Token: 0x040064A0 RID: 25760
				private static readonly TypeValue optionsType = ParquetApi.OptionRecord.CreateRecordType().Nullable;

				// Token: 0x040064A1 RID: 25761
				private readonly IEngineHost host;

				// Token: 0x02001F30 RID: 7984
				private sealed class ParquetDocumentTableValue : WrappingTableValue
				{
					// Token: 0x06010CAD RID: 68781 RVA: 0x00397E8A File Offset: 0x0039608A
					public ParquetDocumentTableValue(ParquetQuery query)
						: base(new QueryTableValue(query))
					{
					}

					// Token: 0x06010CAE RID: 68782 RVA: 0x00397E98 File Offset: 0x00396098
					private ParquetDocumentTableValue(TableValue table)
						: base(table)
					{
					}

					// Token: 0x06010CAF RID: 68783 RVA: 0x0039D598 File Offset: 0x0039B798
					protected override TableValue New(TableValue table)
					{
						if (this.Query.Equals(table.Query))
						{
							return new ParquetModule.Parquet.DocumentFunctionValue.ParquetDocumentTableValue(table);
						}
						return table;
					}

					// Token: 0x06010CB0 RID: 68784 RVA: 0x0039D5B5 File Offset: 0x0039B7B5
					public override ActionValue Replace(Value value)
					{
						return ((ParquetQuery)this.Query).Replace(value);
					}
				}
			}

			// Token: 0x02001F31 RID: 7985
			public class MetadataFunctionValue : NativeFunctionValue1<Value, BinaryValue>
			{
				// Token: 0x06010CB1 RID: 68785 RVA: 0x0039D5C8 File Offset: 0x0039B7C8
				public MetadataFunctionValue(IEngineHost host)
					: base(TypeValue.Any, "binary", TypeValue.Binary)
				{
					this.host = host;
				}

				// Token: 0x06010CB2 RID: 68786 RVA: 0x0039D5E8 File Offset: 0x0039B7E8
				public override Value TypedInvoke(BinaryValue binary)
				{
					Value value;
					using (Stream stream = binary.Open(true))
					{
						if (!stream.CanSeek)
						{
							throw ValueException.NewParameterError<Message0>(Resources.SeekableStreamRequired, binary);
						}
						stream.Seek(stream.Length - 8L, SeekOrigin.Begin);
						BinaryReader binaryReader = new BinaryReader(stream);
						int i = binaryReader.ReadInt32();
						if (binaryReader.ReadInt32() != 827474256 || i > 131072)
						{
							throw ValueException.NewDataSourceError<Message1>(Strings.BinaryFormat_EndOfInput(i), binary, null);
						}
						stream.Seek(stream.Length - (long)i - 8L, SeekOrigin.Begin);
						byte[] array = new byte[i];
						while (i > 0)
						{
							i -= stream.Read(array, array.Length - i, i);
						}
						value = ParquetSummary.MakeSummary(array);
					}
					return value;
				}

				// Token: 0x040064A2 RID: 25762
				private const int ParquetMagic = 827474256;

				// Token: 0x040064A3 RID: 25763
				private const int MaxFooterLength = 131072;

				// Token: 0x040064A4 RID: 25764
				private readonly IEngineHost host;
			}
		}

		// Token: 0x02001F32 RID: 7986
		private enum Exports
		{
			// Token: 0x040064A6 RID: 25766
			ParquetDocument,
			// Token: 0x040064A7 RID: 25767
			ParquetMetadata,
			// Token: 0x040064A8 RID: 25768
			Count
		}
	}
}
