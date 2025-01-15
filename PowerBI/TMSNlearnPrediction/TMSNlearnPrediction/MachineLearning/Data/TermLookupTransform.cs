using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Data.IO;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003C8 RID: 968
	public sealed class TermLookupTransform : OneToOneTransformBase
	{
		// Token: 0x060014A4 RID: 5284 RVA: 0x000779F9 File Offset: 0x00075BF9
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("TXTLOOKT", 65538U, 65538U, 65538U, "TermLookupTransform", null);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00077A1C File Offset: 0x00075C1C
		public TermLookupTransform(TermLookupTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "TextLookup", Contracts.CheckRef<TermLookupTransform.Arguments>(env, args, "args").column, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsText))
		{
			Contracts.CheckUserArg(this._host, !string.IsNullOrWhiteSpace(args.dataFile), "dataFile", "must specify dataFile");
			Contracts.CheckUserArg(this._host, string.IsNullOrEmpty(args.termColumn) == string.IsNullOrEmpty(args.valueColumn), "termColumn", "Either both term and value column should be specified, or neither.");
			using (IChannel channel = this._host.Start("Training"))
			{
				this._bytes = TermLookupTransform.GetBytes(this._host, this.Infos, args);
				this._ldr = TermLookupTransform.GetLoader(this._host, this._bytes);
				this._valueMap = TermLookupTransform.Train(channel, this._ldr);
				this.SetMetadata();
				channel.Done();
			}
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x00077B20 File Offset: 0x00075D20
		public TermLookupTransform(IHostEnvironment env, IDataView input, IDataView lookup, string sourceTerm, string sourceValue, string targetTerm, string targetValue)
			: base(env, "TextLookup", new TermLookupTransform.Column[]
			{
				new TermLookupTransform.Column
				{
					name = sourceValue,
					source = sourceTerm
				}
			}, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsText))
		{
			Contracts.CheckValue<IDataView>(this._host, input, "input");
			Contracts.CheckValue<IDataView>(this._host, lookup, "lookup");
			Contracts.CheckNonEmpty(this._host, targetTerm, "targetTerm", "Term column must be specified when passing in a data view as lookup table.");
			Contracts.CheckNonEmpty(this._host, targetValue, "targetValue", "Value column must be specified when passing in a data view as lookup table.");
			using (IChannel channel = this._host.Start("Training"))
			{
				this._bytes = TermLookupTransform.GetBytesFromDataView(this._host, lookup, targetTerm, targetValue);
				this._ldr = TermLookupTransform.GetLoader(this._host, this._bytes);
				this._valueMap = TermLookupTransform.Train(channel, this._ldr);
				this.SetMetadata();
				channel.Done();
			}
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00077C38 File Offset: 0x00075E38
		private static SubComponent<IDataLoader, SignatureDataLoader> GetLoaderSubComponent(string filename, bool keyValues, IHost host)
		{
			if (!keyValues)
			{
				return new SubComponent<IDataLoader, SignatureDataLoader>("Text", new string[] { "col=Term:TX:0", "col=Value:Num:1" });
			}
			ulong num = ulong.MaxValue;
			ulong num2 = 0UL;
			try
			{
				TextLoader.Arguments arguments = new TextLoader.Arguments();
				CmdParser.ParseArguments("col=Term:TX:0 col=Value:TX:1", arguments);
				TextLoader textLoader = new TextLoader(arguments, host, new MultiFileSource(filename));
				using (IRowCursor rowCursor = textLoader.GetRowCursor((int c) => true, null))
				{
					ValueGetter<DvText> getter = rowCursor.GetGetter<DvText>(0);
					ValueGetter<DvText> getter2 = rowCursor.GetGetter<DvText>(1);
					DvText dvText = default(DvText);
					using (IChannel channel = host.Start("Creating Text Lookup Loader"))
					{
						long num3 = 0L;
						while (rowCursor.MoveNext())
						{
							getter2.Invoke(ref dvText);
							ulong num4;
							if (Conversions.Instance.TryParseKey(ref dvText, 1UL, 18446744073709551615UL, out num4))
							{
								if (num4 < num && num4 != 0UL)
								{
									num = num4;
								}
								if (num4 > num2)
								{
									num2 = num4;
								}
							}
							else if (Conversions.Instance.TryParse(ref dvText, out num4))
							{
								num = 0UL;
							}
							else
							{
								DvText dvText2 = default(DvText);
								getter.Invoke(ref dvText2);
								if (num3 < 5L)
								{
									channel.Warning("Term '{0}' in mapping file is mapped to non key value '{1}'", new object[] { dvText2, dvText });
								}
								num3 += 1L;
							}
						}
						if (num3 > 0L)
						{
							channel.Warning("Found {0} non key values in the file '{1}'", new object[] { num3, filename });
						}
						if (num > num2)
						{
							num = 0UL;
							num2 = (ulong)(-2);
							channel.Warning("did not find any valid key values in the file '{0}'", new object[] { filename });
						}
						else
						{
							channel.Info("Found key values in the range {0} to {1} in the file '{2}'", new object[] { num, num2, filename });
						}
						channel.Done();
					}
				}
			}
			catch (Exception ex)
			{
				throw Contracts.Except(host, ex, "Failed to parse the lookup file '{0}' in TermLookupTransform", new object[] { filename });
			}
			string text;
			if (num2 - num < 2147483647UL)
			{
				text = string.Format("col=Value:U4[{0}-{1}]:1", num, num2);
			}
			else if (num2 - num < (ulong)(-1))
			{
				text = string.Format("col=Value:U4[{0}-*]:1", num);
			}
			else
			{
				text = string.Format("col=Value:U8[{0}-*]:1", num);
			}
			return new SubComponent<IDataLoader, SignatureDataLoader>("Text", new string[] { "col=Term:TXT:0", text });
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00077F18 File Offset: 0x00076118
		private static byte[] GetBytes(IHost host, OneToOneTransformBase.ColInfo[] infos, TermLookupTransform.Arguments args)
		{
			string dataFile = args.dataFile;
			SubComponent<IDataLoader, SignatureDataLoader> subComponent = args.loader;
			string text;
			string text2;
			if (!string.IsNullOrEmpty(args.termColumn))
			{
				text = args.termColumn;
				text2 = args.valueColumn;
			}
			else
			{
				string extension = Path.GetExtension(dataFile);
				if (SubComponentExtensions.IsGood(subComponent) || string.Equals(extension, ".idv", StringComparison.OrdinalIgnoreCase))
				{
					throw Contracts.ExceptUserArg(host, "termColumn", "Term and value columns needed.");
				}
				subComponent = TermLookupTransform.GetLoaderSubComponent(args.dataFile, args.keyValues, host);
				text = "Term";
				text2 = "Value";
			}
			return TermLookupTransform.GetBytesOne(host, dataFile, subComponent, text, text2);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00077FAC File Offset: 0x000761AC
		private static byte[] GetBytesFromDataView(IHost host, IDataView lookup, string termColumn, string valueColumn)
		{
			ISchema schema = lookup.Schema;
			int num;
			if (!schema.TryGetColumnIndex(termColumn, ref num))
			{
				throw Contracts.ExceptUserArg(host, "termColumn", "column not found: '{0}'", new object[] { termColumn });
			}
			int num2;
			if (!schema.TryGetColumnIndex(valueColumn, ref num2))
			{
				throw Contracts.ExceptUserArg(host, "valueColumn", "column not found: '{0}'", new object[] { valueColumn });
			}
			ColumnType columnType = schema.GetColumnType(num);
			Contracts.CheckUserArg(host, columnType.IsText, "termColumn", "term column must contain text");
			schema.GetColumnType(num2);
			ChooseColumnsTransform chooseColumnsTransform = new ChooseColumnsTransform(new ChooseColumnsTransform.Arguments
			{
				column = new ChooseColumnsTransform.Column[]
				{
					new ChooseColumnsTransform.Column
					{
						name = "Term",
						source = termColumn
					},
					new ChooseColumnsTransform.Column
					{
						name = "Value",
						source = valueColumn
					}
				}
			}, host, lookup);
			BinarySaver binarySaver = new BinarySaver(new BinarySaver.Arguments(), host);
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				binarySaver.SaveData(memoryStream, chooseColumnsTransform, new int[] { 0, 1 });
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x000780F8 File Offset: 0x000762F8
		private static byte[] GetBytesOne(IHost host, string dataFile, SubComponent<IDataLoader, SignatureDataLoader> sub, string termColumn, string valueColumn)
		{
			if (!SubComponentExtensions.IsGood(sub))
			{
				string extension = Path.GetExtension(dataFile);
				bool flag = string.Equals(extension, ".idv", StringComparison.OrdinalIgnoreCase);
				bool flag2 = string.Equals(extension, ".tdv", StringComparison.OrdinalIgnoreCase);
				if (!flag && !flag2)
				{
					throw Contracts.ExceptUserArg(host, "loader", "must specify the loader");
				}
				sub = new SubComponent<IDataLoader, SignatureDataLoader>(flag ? "BinaryLoader" : "TransposeLoader");
			}
			IDataLoader dataLoader = ComponentCatalog.CreateInstance<IDataLoader, SignatureDataLoader>(sub, new object[]
			{
				host,
				new MultiFileSource(dataFile)
			});
			return TermLookupTransform.GetBytesFromDataView(host, dataLoader, termColumn, valueColumn);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00078188 File Offset: 0x00076388
		private static BinaryLoader GetLoader(IHostEnvironment env, byte[] bytes)
		{
			MemoryStream memoryStream = new MemoryStream(bytes, false);
			return new BinaryLoader(new BinaryLoader.Arguments(), env, memoryStream, true);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x000781B0 File Offset: 0x000763B0
		private static TermLookupTransform.ValueMap Train(IExceptionContext ectx, BinaryLoader ldr)
		{
			ISchema schema = ldr.Schema;
			ColumnType columnType = schema.GetColumnType(1);
			TermLookupTransform.ValueMap valueMap = TermLookupTransform.ValueMap.Create(columnType);
			using (IRowCursor rowCursor = ldr.GetRowCursor((int c) => true, null))
			{
				valueMap.Train(ectx, rowCursor, 0, 1);
			}
			return valueMap;
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0007824C File Offset: 0x0007644C
		private TermLookupTransform(IChannel ch, ModelLoadContext ctx, IHost host, IDataView input)
		{
			TermLookupTransform.<>c__DisplayClass8 CS$<>8__locals1 = new TermLookupTransform.<>c__DisplayClass8();
			CS$<>8__locals1.ch = ch;
			base..ctor(ctx, host, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsText));
			byte[] rgb = null;
			Action<BinaryReader> action = delegate(BinaryReader r)
			{
				rgb = TermLookupTransform.ReadAllBytes(CS$<>8__locals1.ch, r);
			};
			if (!ctx.TryLoadBinaryStream("DefaultMap.idv", action))
			{
				throw Contracts.ExceptDecode(CS$<>8__locals1.ch, "Missing map idv stream");
			}
			this._bytes = rgb;
			this._ldr = TermLookupTransform.GetLoader(this._host, this._bytes);
			TermLookupTransform.ValidateLoader(CS$<>8__locals1.ch, this._ldr);
			this._valueMap = TermLookupTransform.Train(CS$<>8__locals1.ch, this._ldr);
			this.SetMetadata();
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x00078310 File Offset: 0x00076510
		private static byte[] ReadAllBytes(IExceptionContext ectx, BinaryReader rdr)
		{
			long length = rdr.BaseStream.Length;
			Contracts.CheckDecode(ectx, length <= 2147483647L);
			byte[] array = new byte[(int)length];
			int num = rdr.Read(array, 0, array.Length);
			Contracts.CheckDecode(ectx, num == array.Length);
			return array;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x00078380 File Offset: 0x00076580
		public static TermLookupTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("TextLookup");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(TermLookupTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(h, input, "input");
			return HostExtensions.Apply<TermLookupTransform>(h, "Loading Model", (IChannel ch) => new TermLookupTransform(ch, ctx, h, input));
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x00078424 File Offset: 0x00076624
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(TermLookupTransform.GetVersionInfo());
			base.SaveBase(ctx);
			ctx.SaveBinaryStream("DefaultMap.idv", delegate(BinaryWriter w)
			{
				w.Write(this._bytes);
			});
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00078471 File Offset: 0x00076671
		[Conditional("DEBUG")]
		private static void DebugValidateLoader(BinaryLoader ldr)
		{
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00078473 File Offset: 0x00076673
		private static void ValidateLoader(IExceptionContext ectx, BinaryLoader ldr)
		{
			if (ldr == null)
			{
				return;
			}
			Contracts.CheckDecode(ectx, ldr.Schema.ColumnCount == 2);
			Contracts.CheckDecode(ectx, ldr.Schema.GetColumnType(0).IsText);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x000784A4 File Offset: 0x000766A4
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._valueMap.Type;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000784B4 File Offset: 0x000766B4
		private void SetMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			for (int i = 0; i < this.Infos.Length; i++)
			{
				using (metadata.BuildMetadata(i, this._ldr.Schema, 1))
				{
				}
			}
			metadata.Seal();
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00078514 File Offset: 0x00076714
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			ValueGetter<DvText> srcGetter = base.GetSrcGetter<DvText>(input, iinfo);
			return this._valueMap.GetGetter(srcGetter);
		}

		// Token: 0x04000C68 RID: 3176
		public const string LoaderSignature = "TermLookupTransform";

		// Token: 0x04000C69 RID: 3177
		internal const string Summary = "Maps text values columns to new columns using a map dataset.";

		// Token: 0x04000C6A RID: 3178
		private const string DefaultMapName = "DefaultMap.idv";

		// Token: 0x04000C6B RID: 3179
		private const string RegistrationName = "TextLookup";

		// Token: 0x04000C6C RID: 3180
		private readonly byte[] _bytes;

		// Token: 0x04000C6D RID: 3181
		private readonly BinaryLoader _ldr;

		// Token: 0x04000C6E RID: 3182
		private readonly TermLookupTransform.ValueMap _valueMap;

		// Token: 0x020003C9 RID: 969
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x060014B9 RID: 5305 RVA: 0x0007853C File Offset: 0x0007673C
			public static TermLookupTransform.Column Parse(string str)
			{
				TermLookupTransform.Column column = new TermLookupTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x060014BA RID: 5306 RVA: 0x0007855B File Offset: 0x0007675B
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x020003CA RID: 970
		public sealed class Arguments
		{
			// Token: 0x04000C71 RID: 3185
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public TermLookupTransform.Column[] column;

			// Token: 0x04000C72 RID: 3186
			[Argument(0, IsInputFileName = true, HelpText = "The data file containing the terms", ShortName = "data", SortOrder = 2)]
			public string dataFile;

			// Token: 0x04000C73 RID: 3187
			[Argument(4, HelpText = "The data loader", NullName = "<Auto>")]
			public SubComponent<IDataLoader, SignatureDataLoader> loader;

			// Token: 0x04000C74 RID: 3188
			[Argument(0, HelpText = "The name of the text column containing the terms", ShortName = "term")]
			public string termColumn;

			// Token: 0x04000C75 RID: 3189
			[Argument(0, HelpText = "The name of the column containing the values", ShortName = "value")]
			public string valueColumn;

			// Token: 0x04000C76 RID: 3190
			[Argument(0, HelpText = "If term and value columns are unspecified, specifies whether the values are key values or numeric.", ShortName = "key")]
			public bool keyValues = true;
		}

		// Token: 0x020003CB RID: 971
		private abstract class ValueMap
		{
			// Token: 0x060014BD RID: 5309 RVA: 0x0007857B File Offset: 0x0007677B
			protected ValueMap(ColumnType type)
			{
				this.Type = type;
			}

			// Token: 0x060014BE RID: 5310 RVA: 0x0007858C File Offset: 0x0007678C
			public static TermLookupTransform.ValueMap Create(ColumnType type)
			{
				if (!type.IsVector)
				{
					Func<PrimitiveType, TermLookupTransform.OneValueMap<int>> func = new Func<PrimitiveType, TermLookupTransform.OneValueMap<int>>(TermLookupTransform.ValueMap.CreatePrimitive<int>);
					MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { type.RawType });
					return (TermLookupTransform.ValueMap)methodInfo.Invoke(null, new object[] { type });
				}
				Func<VectorType, TermLookupTransform.VecValueMap<int>> func2 = new Func<VectorType, TermLookupTransform.VecValueMap<int>>(TermLookupTransform.ValueMap.CreateVector<int>);
				MethodInfo methodInfo2 = func2.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { type.ItemType.RawType });
				return (TermLookupTransform.ValueMap)methodInfo2.Invoke(null, new object[] { type });
			}

			// Token: 0x060014BF RID: 5311 RVA: 0x00078641 File Offset: 0x00076841
			public static TermLookupTransform.OneValueMap<TVal> CreatePrimitive<TVal>(PrimitiveType type)
			{
				return new TermLookupTransform.OneValueMap<TVal>(type);
			}

			// Token: 0x060014C0 RID: 5312 RVA: 0x00078649 File Offset: 0x00076849
			public static TermLookupTransform.VecValueMap<TVal> CreateVector<TVal>(VectorType type)
			{
				return new TermLookupTransform.VecValueMap<TVal>(type);
			}

			// Token: 0x060014C1 RID: 5313
			public abstract void Train(IExceptionContext ectx, IRowCursor cursor, int colTerm, int colValue);

			// Token: 0x060014C2 RID: 5314
			public abstract Delegate GetGetter(ValueGetter<DvText> getSrc);

			// Token: 0x04000C77 RID: 3191
			public readonly ColumnType Type;
		}

		// Token: 0x020003CC RID: 972
		private abstract class ValueMap<TRes> : TermLookupTransform.ValueMap
		{
			// Token: 0x060014C3 RID: 5315 RVA: 0x00078651 File Offset: 0x00076851
			protected ValueMap(ColumnType type)
				: base(type)
			{
			}

			// Token: 0x060014C4 RID: 5316 RVA: 0x0007865C File Offset: 0x0007685C
			public override void Train(IExceptionContext ectx, IRowCursor cursor, int colTerm, int colValue)
			{
				ValueGetter<DvText> getter = cursor.GetGetter<DvText>(colTerm);
				ValueGetter<TRes> getter2 = cursor.GetGetter<TRes>(colValue);
				NormStr.Pool pool = new NormStr.Pool();
				List<TRes> list = new List<TRes>();
				DvText dvText = default(DvText);
				while (cursor.MoveNext())
				{
					getter.Invoke(ref dvText);
					dvText = dvText.Trim();
					if (dvText.IsNA)
					{
						throw Contracts.Except(ectx, "Missing term in lookup data around row: {0}", new object[] { list.Count });
					}
					NormStr normStr = dvText.AddToPool(pool);
					if (normStr.Id != list.Count)
					{
						throw Contracts.Except(ectx, "Duplicate term in lookup data: '{0}'", new object[] { normStr });
					}
					TRes tres = default(TRes);
					getter2.Invoke(ref tres);
					list.Add(tres);
				}
				this._terms = pool;
				this._values = list.ToArray();
			}

			// Token: 0x060014C5 RID: 5317 RVA: 0x0007873D File Offset: 0x0007693D
			public override Delegate GetGetter(ValueGetter<DvText> getTerm)
			{
				return this.GetGetterCore(getTerm);
			}

			// Token: 0x060014C6 RID: 5318 RVA: 0x000787C8 File Offset: 0x000769C8
			private ValueGetter<TRes> GetGetterCore(ValueGetter<DvText> getTerm)
			{
				DvText src = default(DvText);
				return delegate(ref TRes dst)
				{
					getTerm.Invoke(ref src);
					src = src.Trim();
					NormStr normStr = src.FindInPool(this._terms);
					if (normStr == null)
					{
						this.GetMissing(ref dst);
						return;
					}
					this.CopyValue(ref this._values[normStr.Id], ref dst);
				};
			}

			// Token: 0x060014C7 RID: 5319
			protected abstract void GetMissing(ref TRes dst);

			// Token: 0x060014C8 RID: 5320
			protected abstract void CopyValue(ref TRes src, ref TRes dst);

			// Token: 0x04000C78 RID: 3192
			private NormStr.Pool _terms;

			// Token: 0x04000C79 RID: 3193
			private TRes[] _values;
		}

		// Token: 0x020003CD RID: 973
		private sealed class OneValueMap<TRes> : TermLookupTransform.ValueMap<TRes>
		{
			// Token: 0x060014C9 RID: 5321 RVA: 0x00078804 File Offset: 0x00076A04
			public OneValueMap(PrimitiveType type)
				: base(type)
			{
				ValueMapper<DvText, TRes> valueMapper;
				bool flag;
				if (Conversions.Instance.TryGetStandardConversion<DvText, TRes>(TextType.Instance, type, out valueMapper, out flag))
				{
					DvText na = DvText.NA;
					valueMapper.Invoke(ref na, ref this._badValue);
				}
			}

			// Token: 0x060014CA RID: 5322 RVA: 0x00078842 File Offset: 0x00076A42
			protected override void GetMissing(ref TRes dst)
			{
				dst = this._badValue;
			}

			// Token: 0x060014CB RID: 5323 RVA: 0x00078850 File Offset: 0x00076A50
			protected override void CopyValue(ref TRes src, ref TRes dst)
			{
				dst = src;
			}

			// Token: 0x04000C7A RID: 3194
			private readonly TRes _badValue;
		}

		// Token: 0x020003CE RID: 974
		private sealed class VecValueMap<TItem> : TermLookupTransform.ValueMap<VBuffer<TItem>>
		{
			// Token: 0x060014CC RID: 5324 RVA: 0x0007885E File Offset: 0x00076A5E
			public VecValueMap(VectorType type)
				: base(type)
			{
			}

			// Token: 0x060014CD RID: 5325 RVA: 0x00078867 File Offset: 0x00076A67
			protected override void GetMissing(ref VBuffer<TItem> dst)
			{
				dst = new VBuffer<TItem>(this.Type.VectorSize, 0, dst.Values, dst.Indices);
			}

			// Token: 0x060014CE RID: 5326 RVA: 0x0007888C File Offset: 0x00076A8C
			protected override void CopyValue(ref VBuffer<TItem> src, ref VBuffer<TItem> dst)
			{
				src.CopyTo(ref dst);
			}
		}
	}
}
