using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000278 RID: 632
	public sealed class ConcatTransform : RowToRowTransformBase
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x0004D060 File Offset: 0x0004B260
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("CONCAT F", 65538U, 65538U, 65537U, "ConcatTransform", "ConcatFunction");
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0004D088 File Offset: 0x0004B288
		public ConcatTransform(ConcatTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "Concat", input)
		{
			Contracts.CheckValue<ConcatTransform.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, Utils.Size<ConcatTransform.Column>(args.column) > 0, "columns");
			for (int i = 0; i < args.column.Length; i++)
			{
				Contracts.CheckUserArg(this._host, Utils.Size<string>(args.column[i].source) > 0, "columns");
			}
			this._bindings = new ConcatTransform.Bindings(args.column, null, this._input.Schema);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0004D184 File Offset: 0x0004B384
		public ConcatTransform(ConcatTransform.TaggedArguments args, IHostEnvironment env, IDataView input)
			: base(env, "Concat", input)
		{
			Contracts.CheckValue<ConcatTransform.TaggedArguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, Utils.Size<ConcatTransform.TaggedColumn>(args.column) > 0, "columns");
			for (int i = 0; i < args.column.Length; i++)
			{
				Contracts.CheckUserArg(this._host, Utils.Size<KeyValuePair<string, string>>(args.column[i].source) > 0, "columns");
			}
			ConcatTransform.Column[] array = args.column.Select(delegate(ConcatTransform.TaggedColumn c)
			{
				ConcatTransform.Column column = new ConcatTransform.Column();
				column.name = c.name;
				column.source = c.source.Select((KeyValuePair<string, string> kvp) => kvp.Value).ToArray<string>();
				return column;
			}).ToArray<ConcatTransform.Column>();
			this._bindings = new ConcatTransform.Bindings(array, args.column, this._input.Schema);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0004D250 File Offset: 0x0004B450
		private ConcatTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			this._bindings = new ConcatTransform.Bindings(ctx, this._input.Schema);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0004D2B8 File Offset: 0x0004B4B8
		public static ConcatTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("Concat");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(ConcatTransform.GetVersionInfo());
			return HostExtensions.Apply<ConcatTransform>(h, "Loading Model", (IChannel ch) => new ConcatTransform(ctx, h, input));
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0004D34D File Offset: 0x0004B54D
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(ConcatTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			this._bindings.Save(ctx);
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000DED RID: 3565 RVA: 0x0004D389 File Offset: 0x0004B589
		public override ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x0004D394 File Offset: 0x0004B594
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			if (this._bindings.AnyNewColumnsActive(predicate))
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0004D3C0 File Offset: 0x0004B5C0
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor rowCursor = this._input.GetRowCursor(dependencies, rand);
			return new ConcatTransform.RowCursor(this._host, this._bindings, rowCursor, active);
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0004D408 File Offset: 0x0004B608
		public sealed override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor[] array = this._input.GetRowCursorSet(ref consolidator, dependencies, n, rand);
			if (array.Length == 1 && n > 1 && this._bindings.AnyNewColumnsActive(predicate))
			{
				array = DataViewUtils.CreateSplitCursors(out consolidator, this._host, array[0], n);
			}
			IRowCursor[] array2 = new IRowCursor[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new ConcatTransform.RowCursor(this._host, this._bindings, array[i], active);
			}
			return array2;
		}

		// Token: 0x040007DD RID: 2013
		internal const string Summary = "Concatenates two columns of the same item type.";

		// Token: 0x040007DE RID: 2014
		internal const string LoadName = "Concat";

		// Token: 0x040007DF RID: 2015
		internal const string LoaderSignature = "ConcatTransform";

		// Token: 0x040007E0 RID: 2016
		internal const string LoaderSignatureOld = "ConcatFunction";

		// Token: 0x040007E1 RID: 2017
		private const int VersionAddedAliases = 65538;

		// Token: 0x040007E2 RID: 2018
		private const string RegistrationName = "Concat";

		// Token: 0x040007E3 RID: 2019
		private readonly ConcatTransform.Bindings _bindings;

		// Token: 0x02000279 RID: 633
		public sealed class Column : ManyToOneColumn
		{
			// Token: 0x06000DF3 RID: 3571 RVA: 0x0004D4B0 File Offset: 0x0004B6B0
			public static ConcatTransform.Column Parse(string str)
			{
				ConcatTransform.Column column = new ConcatTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000DF4 RID: 3572 RVA: 0x0004D4CF File Offset: 0x0004B6CF
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x0200027A RID: 634
		public sealed class TaggedColumn
		{
			// Token: 0x06000DF6 RID: 3574 RVA: 0x0004D4EC File Offset: 0x0004B6EC
			public static ConcatTransform.TaggedColumn Parse(string str)
			{
				ConcatTransform.Column column = ConcatTransform.Column.Parse(str);
				if (column == null)
				{
					return null;
				}
				ConcatTransform.TaggedColumn taggedColumn = new ConcatTransform.TaggedColumn();
				taggedColumn.name = column.name;
				taggedColumn.source = column.source.Select((string s) => new KeyValuePair<string, string>(null, s)).ToArray<KeyValuePair<string, string>>();
				return taggedColumn;
			}

			// Token: 0x06000DF7 RID: 3575 RVA: 0x0004D568 File Offset: 0x0004B768
			public bool TryUnparse(StringBuilder sb)
			{
				if (this.source != null)
				{
					if (!this.source.Any((KeyValuePair<string, string> kvp) => !string.IsNullOrEmpty(kvp.Key)))
					{
						ConcatTransform.Column column = new ConcatTransform.Column();
						column.name = this.name;
						column.source = this.source.Select((KeyValuePair<string, string> kvp) => kvp.Value).ToArray<string>();
						return column.TryUnparse(sb);
					}
				}
				return false;
			}

			// Token: 0x040007E6 RID: 2022
			[Argument(0, HelpText = "Name of the new column", ShortName = "name")]
			public string name;

			// Token: 0x040007E7 RID: 2023
			[Argument(4, HelpText = "Name of the source column", ShortName = "src")]
			public KeyValuePair<string, string>[] source;
		}

		// Token: 0x0200027B RID: 635
		public sealed class Arguments
		{
			// Token: 0x040007EB RID: 2027
			[Argument(4, HelpText = "New column definition(s) (optional form: name:srcs)", ShortName = "col", SortOrder = 1)]
			public ConcatTransform.Column[] column;
		}

		// Token: 0x0200027C RID: 636
		public sealed class TaggedArguments
		{
			// Token: 0x040007EC RID: 2028
			[Argument(4, HelpText = "New column definition(s) (optional form: name:srcs)", ShortName = "col", SortOrder = 1)]
			public ConcatTransform.TaggedColumn[] column;
		}

		// Token: 0x0200027D RID: 637
		private sealed class Bindings : ManyToOneColumnBindingsBase
		{
			// Token: 0x06000DFE RID: 3582 RVA: 0x0004D610 File Offset: 0x0004B810
			public Bindings(ConcatTransform.Column[] columns, ConcatTransform.TaggedColumn[] taggedColumns, ISchema schemaInput)
				: base(columns, schemaInput, new Func<ColumnType[], string>(ConcatTransform.Bindings.TestTypes))
			{
				this._aliases = new string[columns.Length][];
				for (int i = 0; i < columns.Length; i++)
				{
					this._aliases[i] = new string[columns[i].source.Length];
					if (taggedColumns != null)
					{
						ConcatTransform.TaggedColumn taggedColumn = taggedColumns[i];
						for (int j = 0; j < taggedColumn.source.Length; j++)
						{
							KeyValuePair<string, string> keyValuePair = taggedColumn.source[j];
							if (!string.IsNullOrEmpty(keyValuePair.Key))
							{
								this._aliases[i][j] = keyValuePair.Key;
							}
						}
					}
				}
				this.CacheTypes(out this._types, out this._typesSlotNames, out this.EchoSrc, out this._isNormalized);
				this._getSlotNames = new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetSlotNames);
			}

			// Token: 0x06000DFF RID: 3583 RVA: 0x0004D6E0 File Offset: 0x0004B8E0
			public Bindings(ModelLoadContext ctx, ISchema schemaInput)
				: base(ctx, schemaInput, new Func<ColumnType[], string>(ConcatTransform.Bindings.TestTypes))
			{
				this._aliases = new string[this.Infos.Length][];
				for (int i = 0; i < this.Infos.Length; i++)
				{
					int num = this.Infos[i].SrcIndices.Length;
					this._aliases[i] = new string[num];
					if (ctx.Header.ModelVerReadable >= 65538U)
					{
						for (;;)
						{
							int num2 = ctx.Reader.ReadInt32();
							if (num2 == -1)
							{
								break;
							}
							Contracts.CheckDecode(0 <= num2 && num2 < num);
							Contracts.CheckDecode(this._aliases[i][num2] == null);
							this._aliases[i][num2] = ctx.LoadNonEmptyString();
						}
					}
				}
				this.CacheTypes(out this._types, out this._typesSlotNames, out this.EchoSrc, out this._isNormalized);
				this._getSlotNames = new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetSlotNames);
			}

			// Token: 0x06000E00 RID: 3584 RVA: 0x0004D7D0 File Offset: 0x0004B9D0
			public override void Save(ModelSaveContext ctx)
			{
				base.Save(ctx);
				for (int i = 0; i < this.Infos.Length; i++)
				{
					for (int j = 0; j < this._aliases[i].Length; j++)
					{
						if (!string.IsNullOrEmpty(this._aliases[i][j]))
						{
							ctx.Writer.Write(j);
							ctx.SaveNonEmptyString(this._aliases[i][j]);
						}
					}
					ctx.Writer.Write(-1);
				}
			}

			// Token: 0x06000E01 RID: 3585 RVA: 0x0004D860 File Offset: 0x0004BA60
			private static string TestTypes(ColumnType[] types)
			{
				ColumnType type = types[0].ItemType;
				if (!type.IsPrimitive)
				{
					return "Expected primitive type";
				}
				if (!types.All((ColumnType t) => type.Equals(t.ItemType)))
				{
					return "All source columns must have the same type";
				}
				return null;
			}

			// Token: 0x06000E02 RID: 3586 RVA: 0x0004D8B0 File Offset: 0x0004BAB0
			private void CacheTypes(out ColumnType[] types, out ColumnType[] typesSlotNames, out bool[] echoSrc, out bool[] isNormalized)
			{
				echoSrc = new bool[this.Infos.Length];
				isNormalized = new bool[this.Infos.Length];
				types = new ColumnType[this.Infos.Length];
				typesSlotNames = new ColumnType[this.Infos.Length];
				for (int i = 0; i < this.Infos.Length; i++)
				{
					ManyToOneColumnBindingsBase.ColInfo colInfo = this.Infos[i];
					if (colInfo.SrcTypes.Length == 1 && colInfo.SrcTypes[0].IsVector)
					{
						echoSrc[i] = true;
						DvBool @false = DvBool.False;
						isNormalized[i] = colInfo.SrcTypes[0].ItemType.IsNumber && MetadataUtils.TryGetMetadata<DvBool>(this.Input, BoolType.Instance, "IsNormalized", colInfo.SrcIndices[0], ref @false) && @false.IsTrue;
						types[i] = colInfo.SrcTypes[0];
					}
					else
					{
						isNormalized[i] = colInfo.SrcTypes[0].ItemType.IsNumber;
						if (isNormalized[i])
						{
							foreach (int num in colInfo.SrcIndices)
							{
								DvBool false2 = DvBool.False;
								if (!MetadataUtils.TryGetMetadata<DvBool>(this.Input, BoolType.Instance, "IsNormalized", num, ref false2) || !false2.IsTrue)
								{
									isNormalized[i] = false;
									break;
								}
							}
						}
						types[i] = new VectorType(colInfo.SrcTypes[0].ItemType.AsPrimitive, colInfo.SrcSize);
						if (colInfo.SrcSize != 0)
						{
							bool flag = false;
							for (int k = 0; k < colInfo.SrcTypes.Length; k++)
							{
								ColumnType columnType = colInfo.SrcTypes[k];
								if (!columnType.IsVector)
								{
									flag = true;
									break;
								}
								ColumnType metadataTypeOrNull = this.Input.GetMetadataTypeOrNull("SlotNames", colInfo.SrcIndices[k]);
								if (metadataTypeOrNull != null && metadataTypeOrNull.VectorSize == columnType.VectorSize && metadataTypeOrNull.ItemType.IsText)
								{
									flag = true;
									break;
								}
							}
							if (flag)
							{
								typesSlotNames[i] = MetadataUtils.GetNamesType(colInfo.SrcSize);
							}
						}
					}
				}
			}

			// Token: 0x06000E03 RID: 3587 RVA: 0x0004DABB File Offset: 0x0004BCBB
			protected override ColumnType GetColumnTypeCore(int iinfo)
			{
				return this._types[iinfo];
			}

			// Token: 0x06000E04 RID: 3588 RVA: 0x0004DAC8 File Offset: 0x0004BCC8
			protected override IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypesCore(int iinfo)
			{
				if (this.EchoSrc[iinfo])
				{
					return this.Input.GetMetadataTypes(this.Infos[iinfo].SrcIndices[0]);
				}
				IEnumerable<KeyValuePair<string, ColumnType>> enumerable = base.GetMetadataTypesCore(iinfo);
				ColumnType columnType = this._typesSlotNames[iinfo];
				if (columnType != null)
				{
					enumerable = MetadataUtils.Prepend<KeyValuePair<string, ColumnType>>(enumerable, new KeyValuePair<string, ColumnType>[] { MetadataUtils.GetPair(columnType, "SlotNames") });
				}
				if (this._isNormalized[iinfo])
				{
					enumerable = MetadataUtils.Prepend<KeyValuePair<string, ColumnType>>(enumerable, new KeyValuePair<string, ColumnType>[] { MetadataUtils.GetPair(BoolType.Instance, "IsNormalized") });
				}
				return enumerable;
			}

			// Token: 0x06000E05 RID: 3589 RVA: 0x0004DB6C File Offset: 0x0004BD6C
			protected override ColumnType GetMetadataTypeCore(string kind, int iinfo)
			{
				if (this.EchoSrc[iinfo])
				{
					return this.Input.GetMetadataTypeOrNull(kind, this.Infos[iinfo].SrcIndices[0]);
				}
				if (kind != null)
				{
					if (kind == "SlotNames")
					{
						return this._typesSlotNames[iinfo];
					}
					if (kind == "IsNormalized")
					{
						if (this._isNormalized[iinfo])
						{
							return BoolType.Instance;
						}
						return null;
					}
				}
				return base.GetMetadataTypeCore(kind, iinfo);
			}

			// Token: 0x06000E06 RID: 3590 RVA: 0x0004DBE4 File Offset: 0x0004BDE4
			protected override void GetMetadataCore<TValue>(string kind, int iinfo, ref TValue value)
			{
				if (this.EchoSrc[iinfo])
				{
					this.Input.GetMetadata<TValue>(kind, this.Infos[iinfo].SrcIndices[0], ref value);
					return;
				}
				if (kind != null)
				{
					if (!(kind == "SlotNames"))
					{
						if (kind == "IsNormalized")
						{
							if (!this._isNormalized[iinfo])
							{
								throw MetadataUtils.ExceptGetMetadata();
							}
							MetadataUtils.Marshal<DvBool, TValue>(new MetadataUtils.MetadataGetter<DvBool>(this.IsNormalized), iinfo, ref value);
							return;
						}
					}
					else
					{
						if (this._typesSlotNames[iinfo] == null)
						{
							throw MetadataUtils.ExceptGetMetadata();
						}
						MetadataUtils.Marshal<VBuffer<DvText>, TValue>(this._getSlotNames, iinfo, ref value);
						return;
					}
				}
				base.GetMetadataCore<TValue>(kind, iinfo, ref value);
			}

			// Token: 0x06000E07 RID: 3591 RVA: 0x0004DC84 File Offset: 0x0004BE84
			private void IsNormalized(int iinfo, ref DvBool dst)
			{
				dst = DvBool.True;
			}

			// Token: 0x06000E08 RID: 3592 RVA: 0x0004DC94 File Offset: 0x0004BE94
			private void GetSlotNames(int iinfo, ref VBuffer<DvText> dst)
			{
				ColumnType columnType = this._typesSlotNames[iinfo];
				TextBufferBuilder textBufferBuilder = new TextBufferBuilder();
				textBufferBuilder.Reset(columnType.VectorSize, false);
				StringBuilder stringBuilder = new StringBuilder();
				VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
				ManyToOneColumnBindingsBase.ColInfo colInfo = this.Infos[iinfo];
				string[] array = this._aliases[iinfo];
				int num = 0;
				for (int i = 0; i < colInfo.SrcTypes.Length; i++)
				{
					int num2 = colInfo.SrcIndices[i];
					ColumnType columnType2 = colInfo.SrcTypes[i];
					string text = array[i] ?? this.Input.GetColumnName(num2);
					if (!columnType2.IsVector)
					{
						textBufferBuilder.AddFeature(num++, new DvText(text));
					}
					else
					{
						ColumnType metadataTypeOrNull = this.Input.GetMetadataTypeOrNull("SlotNames", num2);
						if (metadataTypeOrNull != null && metadataTypeOrNull.VectorSize == columnType2.VectorSize && metadataTypeOrNull.ItemType.IsText)
						{
							this.Input.GetMetadata<VBuffer<DvText>>("SlotNames", num2, ref vbuffer);
							stringBuilder.Clear();
							stringBuilder.Append(text).Append(".");
							int length = stringBuilder.Length;
							foreach (KeyValuePair<int, DvText> keyValuePair in vbuffer.Items(false))
							{
								if (keyValuePair.Value.HasChars)
								{
									stringBuilder.Length = length;
									keyValuePair.Value.AddToStringBuilder(stringBuilder);
									textBufferBuilder.AddFeature(num + keyValuePair.Key, new DvText(stringBuilder.ToString()));
								}
							}
						}
						num += colInfo.SrcTypes[i].VectorSize;
					}
				}
				textBufferBuilder.GetResult(ref dst);
			}

			// Token: 0x040007ED RID: 2029
			public readonly bool[] EchoSrc;

			// Token: 0x040007EE RID: 2030
			private readonly ColumnType[] _types;

			// Token: 0x040007EF RID: 2031
			private readonly ColumnType[] _typesSlotNames;

			// Token: 0x040007F0 RID: 2032
			private readonly bool[] _isNormalized;

			// Token: 0x040007F1 RID: 2033
			private readonly string[][] _aliases;

			// Token: 0x040007F2 RID: 2034
			private readonly MetadataUtils.MetadataGetter<VBuffer<DvText>> _getSlotNames;
		}

		// Token: 0x0200027E RID: 638
		private sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x06000E09 RID: 3593 RVA: 0x0004DE6C File Offset: 0x0004C06C
			public RowCursor(IChannelProvider provider, ConcatTransform.Bindings bindings, IRowCursor input, bool[] active)
				: base(provider, input)
			{
				this._bindings = bindings;
				this._active = active;
				if (ConcatTransform.RowCursor._methSrcGetter == null)
				{
					Func<int, int, ValueGetter<int>> func = new Func<int, int, ValueGetter<int>>(this.GetSrcGetter<int>);
					Interlocked.CompareExchange<MethodInfo>(ref ConcatTransform.RowCursor._methSrcGetter, func.GetMethodInfo().GetGenericMethodDefinition(), null);
				}
				if (ConcatTransform.RowCursor._methGetter == null)
				{
					Func<int, ValueGetter<VBuffer<int>>> func2 = new Func<int, ValueGetter<VBuffer<int>>>(this.MakeGetter<int>);
					Interlocked.CompareExchange<MethodInfo>(ref ConcatTransform.RowCursor._methGetter, func2.GetMethodInfo().GetGenericMethodDefinition(), null);
				}
				this._getters = new Delegate[this._bindings.Infos.Length];
				for (int i = 0; i < this._bindings.Infos.Length; i++)
				{
					if (this.IsIndexActive(i))
					{
						this._getters[i] = this.MakeGetter(i);
					}
				}
			}

			// Token: 0x17000194 RID: 404
			// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0004DF3A File Offset: 0x0004C13A
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x06000E0B RID: 3595 RVA: 0x0004DF42 File Offset: 0x0004C142
			private bool IsIndexActive(int iinfo)
			{
				return this._active == null || this._active[this._bindings.MapIinfoToCol(iinfo)];
			}

			// Token: 0x06000E0C RID: 3596 RVA: 0x0004DF61 File Offset: 0x0004C161
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.ColumnCount);
				return this._active == null || this._active[col];
			}

			// Token: 0x06000E0D RID: 3597 RVA: 0x0004DF95 File Offset: 0x0004C195
			private ValueGetter<T> GetSrcGetter<T>(int iinfo, int isrc)
			{
				return base.Input.GetGetter<T>(this._bindings.Infos[iinfo].SrcIndices[isrc]);
			}

			// Token: 0x06000E0E RID: 3598 RVA: 0x0004DFB8 File Offset: 0x0004C1B8
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				bool flag;
				int num = this._bindings.MapColumnIndex(out flag, col);
				if (flag)
				{
					return base.Input.GetGetter<TValue>(num);
				}
				ValueGetter<TValue> valueGetter = this._getters[num] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x06000E0F RID: 3599 RVA: 0x0004E030 File Offset: 0x0004C230
			private Delegate MakeGetter(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo colInfo = this._bindings.Infos[iinfo];
				MethodInfo methodInfo;
				if (this._bindings.EchoSrc[iinfo])
				{
					methodInfo = ConcatTransform.RowCursor._methSrcGetter.MakeGenericMethod(new Type[] { colInfo.SrcTypes[0].RawType });
					return (Delegate)methodInfo.Invoke(this, new object[] { iinfo, 0 });
				}
				methodInfo = ConcatTransform.RowCursor._methGetter.MakeGenericMethod(new Type[] { colInfo.SrcTypes[0].ItemType.RawType });
				return (Delegate)methodInfo.Invoke(this, new object[] { iinfo });
			}

			// Token: 0x06000E10 RID: 3600 RVA: 0x0004E45C File Offset: 0x0004C65C
			private ValueGetter<VBuffer<T>> MakeGetter<T>(int iinfo)
			{
				ManyToOneColumnBindingsBase.ColInfo info = this._bindings.Infos[iinfo];
				ValueGetter<T>[] srcGetterOnes = new ValueGetter<T>[info.SrcIndices.Length];
				ValueGetter<VBuffer<T>>[] srcGetterVecs = new ValueGetter<VBuffer<T>>[info.SrcIndices.Length];
				for (int i = 0; i < info.SrcIndices.Length; i++)
				{
					if (info.SrcTypes[i].IsVector)
					{
						srcGetterVecs[i] = this.GetSrcGetter<VBuffer<T>>(iinfo, i);
					}
					else
					{
						srcGetterOnes[i] = this.GetSrcGetter<T>(iinfo, i);
					}
				}
				T tmp = default(T);
				VBuffer<T>[] tmpBufs = new VBuffer<T>[info.SrcIndices.Length];
				return delegate(ref VBuffer<T> dst)
				{
					int num = 0;
					int num2 = 0;
					for (int j = 0; j < info.SrcIndices.Length; j++)
					{
						ColumnType columnType = info.SrcTypes[j];
						checked
						{
							if (columnType.IsVector)
							{
								srcGetterVecs[j].Invoke(ref tmpBufs[j]);
								if (columnType.VectorSize != 0 && columnType.VectorSize != tmpBufs[j].Length)
								{
									throw Contracts.Except(this._ch, "Column '{0}': expected {1} slots, but got {2}", new object[]
									{
										this.Input.Schema.GetColumnName(info.SrcIndices[j]),
										columnType.VectorSize,
										tmpBufs[j].Length
									});
								}
								num += tmpBufs[j].Length;
								num2 += tmpBufs[j].Count;
							}
							else
							{
								num++;
								num2++;
							}
						}
					}
					T[] array = dst.Values;
					int[] array2 = dst.Indices;
					if (num2 <= num / 2)
					{
						if (Utils.Size<T>(array) < num2)
						{
							array = new T[num2];
						}
						if (Utils.Size<int>(array2) < num2)
						{
							array2 = new int[num2];
						}
						int num3 = 0;
						int num4 = 0;
						for (int k = 0; k < info.SrcIndices.Length; k++)
						{
							if (info.SrcTypes[k].IsVector)
							{
								VBuffer<T> vbuffer = tmpBufs[k];
								if (vbuffer.IsDense)
								{
									for (int l = 0; l < vbuffer.Length; l++)
									{
										array[num4] = vbuffer.Values[l];
										array2[num4++] = num3 + l;
									}
								}
								else
								{
									for (int m = 0; m < vbuffer.Count; m++)
									{
										array[num4] = vbuffer.Values[m];
										array2[num4++] = num3 + vbuffer.Indices[m];
									}
								}
								num3 += vbuffer.Length;
							}
							else
							{
								srcGetterOnes[k].Invoke(ref tmp);
								array[num4] = tmp;
								array2[num4++] = num3;
								num3++;
							}
						}
						dst = new VBuffer<T>(num, num4, array, array2);
						return;
					}
					if (Utils.Size<T>(array) < num)
					{
						array = new T[num];
					}
					int num5 = 0;
					for (int n = 0; n < info.SrcIndices.Length; n++)
					{
						if (info.SrcTypes[n].IsVector)
						{
							tmpBufs[n].CopyTo(array, num5);
							num5 += tmpBufs[n].Length;
						}
						else
						{
							srcGetterOnes[n].Invoke(ref tmp);
							array[num5++] = tmp;
						}
					}
					dst = new VBuffer<T>(num, array, array2);
				};
			}

			// Token: 0x040007F3 RID: 2035
			private readonly ConcatTransform.Bindings _bindings;

			// Token: 0x040007F4 RID: 2036
			private readonly bool[] _active;

			// Token: 0x040007F5 RID: 2037
			private readonly Delegate[] _getters;

			// Token: 0x040007F6 RID: 2038
			private static MethodInfo _methSrcGetter;

			// Token: 0x040007F7 RID: 2039
			private static MethodInfo _methGetter;
		}
	}
}
