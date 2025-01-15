using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003CF RID: 975
	public sealed class UngroupTransform : TransformBase
	{
		// Token: 0x060014CF RID: 5327 RVA: 0x00078895 File Offset: 0x00076A95
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("UNGRP XF", 65537U, 65537U, 65537U, "UngroupTransform", null);
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x000788B8 File Offset: 0x00076AB8
		public UngroupTransform(UngroupTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "UngroupTransform", input)
		{
			Contracts.CheckValue<UngroupTransform.Arguments>(this._host, args, "args");
			Contracts.CheckUserArg(this._host, Utils.Size<string>(args.column) > 0, "column", "There must be at least one pivot column");
			Contracts.CheckUserArg(this._host, args.column.Distinct<string>().Count<string>() == args.column.Length, "column", "Duplicate pivot columns are not allowed");
			this._schemaImpl = new UngroupTransform.SchemaImpl(this._host, this._input.Schema, args.mode, args.column);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00078980 File Offset: 0x00076B80
		public static UngroupTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(UngroupTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(env, input, "input");
			IHost h = env.Register("UngroupTransform");
			return HostExtensions.Apply<UngroupTransform>(h, "Loading Model", (IChannel ch) => new UngroupTransform(ctx, h, input));
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x00078A0B File Offset: 0x00076C0B
		private UngroupTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			this._schemaImpl = UngroupTransform.SchemaImpl.Create(ctx, host, input.Schema);
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00078A28 File Offset: 0x00076C28
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(UngroupTransform.GetVersionInfo());
			this._schemaImpl.Save(ctx);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00078A58 File Offset: 0x00076C58
		public override long? GetRowCount(bool lazy = true)
		{
			int commonPivotColumnSize = this._schemaImpl.GetCommonPivotColumnSize();
			if (commonPivotColumnSize > 0)
			{
				long? rowCount = this._input.GetRowCount(true);
				if (rowCount != null && rowCount.Value <= 9223372036854775807L / (long)commonPivotColumnSize)
				{
					return new long?(rowCount.Value * (long)commonPivotColumnSize);
				}
			}
			return null;
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060014D5 RID: 5333 RVA: 0x00078AB9 File Offset: 0x00076CB9
		public override ISchema Schema
		{
			get
			{
				return this._schemaImpl;
			}
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x00078AC4 File Offset: 0x00076CC4
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return null;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060014D7 RID: 5335 RVA: 0x00078ADA File Offset: 0x00076CDA
		public override bool CanShuffle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00078AF0 File Offset: 0x00076CF0
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			bool[] activeInput = this._schemaImpl.GetActiveInput(predicate);
			IRowCursor rowCursor = this._input.GetRowCursor((int col) => activeInput[col], null);
			return new UngroupTransform.Cursor(this._host, rowCursor, this._schemaImpl, predicate);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00078B80 File Offset: 0x00076D80
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			bool[] activeInput = this._schemaImpl.GetActiveInput(predicate);
			IRowCursor[] inputCursors = this._input.GetRowCursorSet(ref consolidator, (int col) => activeInput[col], n, null);
			return Utils.BuildArray<IRowCursor>(inputCursors.Length, (int x) => new UngroupTransform.Cursor(this._host, inputCursors[x], this._schemaImpl, predicate));
		}

		// Token: 0x04000C7B RID: 3195
		public const string Summary = "Un-groups vector columns into sequences of rows, inverse of Group transform";

		// Token: 0x04000C7C RID: 3196
		public const string LoaderSignature = "UngroupTransform";

		// Token: 0x04000C7D RID: 3197
		private readonly UngroupTransform.SchemaImpl _schemaImpl;

		// Token: 0x020003D0 RID: 976
		public enum UngroupMode
		{
			// Token: 0x04000C7F RID: 3199
			Inner,
			// Token: 0x04000C80 RID: 3200
			Outer,
			// Token: 0x04000C81 RID: 3201
			First
		}

		// Token: 0x020003D1 RID: 977
		public sealed class Arguments
		{
			// Token: 0x04000C82 RID: 3202
			[Argument(4, HelpText = "Columns to unroll, or 'pivot'", ShortName = "col")]
			public string[] column;

			// Token: 0x04000C83 RID: 3203
			[Argument(0, HelpText = "Specifies how to unroll multiple pivot columns of different size.")]
			public UngroupTransform.UngroupMode mode;
		}

		// Token: 0x020003D2 RID: 978
		private sealed class SchemaImpl : ISchema
		{
			// Token: 0x060014DB RID: 5339 RVA: 0x00078BFC File Offset: 0x00076DFC
			private static bool ShouldPreserveMetadata(string kind)
			{
				switch (kind)
				{
				case "IsNormalized":
				case "KeyValues":
				case "ScoreColumnSetId":
				case "ScoreColumnKind":
				case "ScoreValueKind":
				case "HasMissingValues":
				case "IsUserVisible":
					return true;
				}
				return false;
			}

			// Token: 0x060014DC RID: 5340 RVA: 0x00078CB4 File Offset: 0x00076EB4
			public SchemaImpl(IExceptionContext ectx, ISchema inputSchema, UngroupTransform.UngroupMode mode, string[] pivotColumns)
			{
				this._ectx = ectx;
				this._inputSchema = inputSchema;
				this.Mode = mode;
				UngroupTransform.SchemaImpl.CheckAndBind(this._ectx, inputSchema, pivotColumns, out this._infos);
				this._pivotColMap = new Dictionary<string, int>();
				this._pivotIndex = Utils.CreateArray<int>(this._inputSchema.ColumnCount, -1);
				for (int i = 0; i < this._infos.Length; i++)
				{
					UngroupTransform.SchemaImpl.PivotColumnInfo pivotColumnInfo = this._infos[i];
					this._pivotColMap[pivotColumnInfo.Name] = pivotColumnInfo.Index;
					this._pivotIndex[pivotColumnInfo.Index] = i;
				}
			}

			// Token: 0x060014DD RID: 5341 RVA: 0x00078D60 File Offset: 0x00076F60
			private static void CheckAndBind(IExceptionContext ectx, ISchema inputSchema, string[] pivotColumns, out UngroupTransform.SchemaImpl.PivotColumnInfo[] infos)
			{
				infos = new UngroupTransform.SchemaImpl.PivotColumnInfo[pivotColumns.Length];
				for (int i = 0; i < pivotColumns.Length; i++)
				{
					string text = pivotColumns[i];
					Contracts.CheckUserArg(ectx, !string.IsNullOrEmpty(text), "column", "Column name cannot be empty");
					int num;
					if (!inputSchema.TryGetColumnIndex(text, ref num))
					{
						throw Contracts.ExceptUserArg(ectx, "column", "Pivot column '{0}' is not found", new object[] { text });
					}
					ColumnType columnType = inputSchema.GetColumnType(num);
					if (!columnType.IsVector || !columnType.ItemType.IsPrimitive)
					{
						throw Contracts.ExceptUserArg(ectx, "column", "Pivot column '{0}' has type '{1}', but must be a vector of primitive types", new object[] { text, columnType });
					}
					infos[i] = new UngroupTransform.SchemaImpl.PivotColumnInfo(text, num, columnType.VectorSize, columnType.ItemType.AsPrimitive);
				}
			}

			// Token: 0x060014DE RID: 5342 RVA: 0x00078E3C File Offset: 0x0007703C
			public static UngroupTransform.SchemaImpl Create(ModelLoadContext ctx, IExceptionContext ectx, ISchema inputSchema)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ectx, Enum.IsDefined(typeof(UngroupTransform.UngroupMode), num));
				UngroupTransform.UngroupMode ungroupMode = (UngroupTransform.UngroupMode)num;
				int num2 = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ectx, num2 > 0);
				string[] array = new string[num2];
				for (int i = 0; i < num2; i++)
				{
					array[i] = ctx.LoadNonEmptyString();
				}
				return new UngroupTransform.SchemaImpl(ectx, inputSchema, ungroupMode, array);
			}

			// Token: 0x060014DF RID: 5343 RVA: 0x00078EB4 File Offset: 0x000770B4
			public void Save(ModelSaveContext ctx)
			{
				ctx.Writer.Write((int)this.Mode);
				ctx.Writer.Write(this._infos.Length);
				foreach (UngroupTransform.SchemaImpl.PivotColumnInfo pivotColumnInfo in this._infos)
				{
					ctx.SaveNonEmptyString(pivotColumnInfo.Name);
				}
			}

			// Token: 0x060014E0 RID: 5344 RVA: 0x00078F14 File Offset: 0x00077114
			public bool[] GetActiveInput(Func<int, bool> predicate)
			{
				bool[] array = Utils.BuildArray<bool>(this.ColumnCount, predicate);
				for (int i = 0; i < this._infos.Length; i++)
				{
					bool flag = this._infos[i].Size == 0 && (i == 0 || this.Mode != UngroupTransform.UngroupMode.First);
					array[this._infos[i].Index] = flag;
				}
				return array;
			}

			// Token: 0x170001EC RID: 492
			// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00078F99 File Offset: 0x00077199
			public int PivotColumnCount
			{
				get
				{
					return this._infos.Length;
				}
			}

			// Token: 0x060014E2 RID: 5346 RVA: 0x00078FA3 File Offset: 0x000771A3
			public UngroupTransform.SchemaImpl.PivotColumnInfo GetPivotColumnInfo(int iinfo)
			{
				return this._infos[iinfo];
			}

			// Token: 0x060014E3 RID: 5347 RVA: 0x00078FB6 File Offset: 0x000771B6
			public UngroupTransform.SchemaImpl.PivotColumnInfo GetPivotColumnInfoByCol(int col)
			{
				return this._infos[this._pivotIndex[col]];
			}

			// Token: 0x060014E4 RID: 5348 RVA: 0x00078FD0 File Offset: 0x000771D0
			public bool IsPivot(int col)
			{
				return this._pivotIndex[col] >= 0;
			}

			// Token: 0x060014E5 RID: 5349 RVA: 0x00078FE0 File Offset: 0x000771E0
			public int GetCommonPivotColumnSize()
			{
				if (this.Mode == UngroupTransform.UngroupMode.First)
				{
					return this._infos[0].Size;
				}
				int num = 0;
				foreach (UngroupTransform.SchemaImpl.PivotColumnInfo pivotColumnInfo in this._infos)
				{
					if (pivotColumnInfo.Size == 0)
					{
						return 0;
					}
					if (num == 0)
					{
						num = pivotColumnInfo.Size;
					}
					else if (num > pivotColumnInfo.Size && this.Mode == UngroupTransform.UngroupMode.Inner)
					{
						num = pivotColumnInfo.Size;
					}
					else if (num < pivotColumnInfo.Size && this.Mode == UngroupTransform.UngroupMode.Outer)
					{
						num = pivotColumnInfo.Size;
					}
				}
				return num;
			}

			// Token: 0x170001ED RID: 493
			// (get) Token: 0x060014E6 RID: 5350 RVA: 0x0007908A File Offset: 0x0007728A
			public int ColumnCount
			{
				get
				{
					return this._inputSchema.ColumnCount;
				}
			}

			// Token: 0x060014E7 RID: 5351 RVA: 0x00079097 File Offset: 0x00077297
			public bool TryGetColumnIndex(string name, out int col)
			{
				return this._inputSchema.TryGetColumnIndex(name, ref col);
			}

			// Token: 0x060014E8 RID: 5352 RVA: 0x000790A6 File Offset: 0x000772A6
			public string GetColumnName(int col)
			{
				return this._inputSchema.GetColumnName(col);
			}

			// Token: 0x060014E9 RID: 5353 RVA: 0x000790B4 File Offset: 0x000772B4
			public ColumnType GetColumnType(int col)
			{
				Contracts.Check(this._ectx, 0 <= col && col < this.ColumnCount);
				if (!this.IsPivot(col))
				{
					return this._inputSchema.GetColumnType(col);
				}
				return this._infos[this._pivotIndex[col]].ItemType;
			}

			// Token: 0x060014EA RID: 5354 RVA: 0x00079120 File Offset: 0x00077320
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				Contracts.Check(this._ectx, 0 <= col && col < this.ColumnCount);
				if (!this.IsPivot(col))
				{
					return this._inputSchema.GetMetadataTypes(col);
				}
				return from pair in this._inputSchema.GetMetadataTypes(col)
					where UngroupTransform.SchemaImpl.ShouldPreserveMetadata(pair.Key)
					select pair;
			}

			// Token: 0x060014EB RID: 5355 RVA: 0x0007918C File Offset: 0x0007738C
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				Contracts.Check(this._ectx, 0 <= col && col < this.ColumnCount);
				if (this.IsPivot(col) && !UngroupTransform.SchemaImpl.ShouldPreserveMetadata(kind))
				{
					return null;
				}
				return this._inputSchema.GetMetadataTypeOrNull(kind, col);
			}

			// Token: 0x060014EC RID: 5356 RVA: 0x000791CC File Offset: 0x000773CC
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				Contracts.Check(this._ectx, 0 <= col && col < this.ColumnCount);
				if (this.IsPivot(col) && !UngroupTransform.SchemaImpl.ShouldPreserveMetadata(kind))
				{
					throw MetadataUtils.ExceptGetMetadata(this._ectx);
				}
				this._inputSchema.GetMetadata<TValue>(kind, col, ref value);
			}

			// Token: 0x04000C84 RID: 3204
			private readonly ISchema _inputSchema;

			// Token: 0x04000C85 RID: 3205
			private readonly IExceptionContext _ectx;

			// Token: 0x04000C86 RID: 3206
			public readonly UngroupTransform.UngroupMode Mode;

			// Token: 0x04000C87 RID: 3207
			private readonly UngroupTransform.SchemaImpl.PivotColumnInfo[] _infos;

			// Token: 0x04000C88 RID: 3208
			private readonly Dictionary<string, int> _pivotColMap;

			// Token: 0x04000C89 RID: 3209
			private readonly int[] _pivotIndex;

			// Token: 0x020003D3 RID: 979
			public struct PivotColumnInfo
			{
				// Token: 0x060014EE RID: 5358 RVA: 0x0007921F File Offset: 0x0007741F
				public PivotColumnInfo(string name, int index, int size, PrimitiveType itemType)
				{
					this.Name = name;
					this.Index = index;
					this.Size = size;
					this.ItemType = itemType;
				}

				// Token: 0x04000C8B RID: 3211
				public readonly string Name;

				// Token: 0x04000C8C RID: 3212
				public readonly int Index;

				// Token: 0x04000C8D RID: 3213
				public readonly int Size;

				// Token: 0x04000C8E RID: 3214
				public readonly PrimitiveType ItemType;
			}
		}

		// Token: 0x020003D4 RID: 980
		private sealed class Cursor : LinkedRootCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x060014EF RID: 5359 RVA: 0x00079240 File Offset: 0x00077440
			public Cursor(IChannelProvider provider, IRowCursor input, UngroupTransform.SchemaImpl schema, Func<int, bool> predicate)
				: base(provider, input)
			{
				this._schema = schema;
				this._active = Utils.BuildArray<bool>(this._schema.ColumnCount, predicate);
				this._cachedGetters = new Delegate[this._schema.ColumnCount];
				this._colSizes = new int[this._schema.ColumnCount];
				int num = ((this._schema.Mode == UngroupTransform.UngroupMode.First) ? 1 : this._schema.PivotColumnCount);
				this._fixedSize = 0;
				List<Func<int>> list = new List<Func<int>>();
				for (int i = 0; i < num; i++)
				{
					UngroupTransform.SchemaImpl.PivotColumnInfo pivotColumnInfo = this._schema.GetPivotColumnInfo(i);
					if (pivotColumnInfo.Size > 0)
					{
						if (this._fixedSize == 0)
						{
							this._fixedSize = pivotColumnInfo.Size;
						}
						else if (this._schema.Mode == UngroupTransform.UngroupMode.Inner && this._fixedSize > pivotColumnInfo.Size)
						{
							this._fixedSize = pivotColumnInfo.Size;
						}
						else if (this._schema.Mode == UngroupTransform.UngroupMode.Outer && this._fixedSize < pivotColumnInfo.Size)
						{
							this._fixedSize = pivotColumnInfo.Size;
						}
					}
					else
					{
						Type rawType = pivotColumnInfo.ItemType.RawType;
						Func<int, Func<int>> func = new Func<int, Func<int>>(this.MakeSizeGetter<int>);
						MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { rawType });
						Func<int> func2 = (Func<int>)methodInfo.Invoke(this, new object[] { pivotColumnInfo.Index });
						list.Add(func2);
					}
				}
				this._sizeGetters = list.ToArray();
			}

			// Token: 0x170001EE RID: 494
			// (get) Token: 0x060014F0 RID: 5360 RVA: 0x000793E6 File Offset: 0x000775E6
			public override long Batch
			{
				get
				{
					return base.Input.Batch;
				}
			}

			// Token: 0x060014F1 RID: 5361 RVA: 0x00079428 File Offset: 0x00077628
			public override ValueGetter<UInt128> GetIdGetter()
			{
				ValueGetter<UInt128> idGetter = base.Input.GetIdGetter();
				return delegate(ref UInt128 val)
				{
					idGetter.Invoke(ref val);
					val = val.Combine(new UInt128((ulong)((long)this._pivotColPosition), 0UL));
				};
			}

			// Token: 0x060014F2 RID: 5362 RVA: 0x00079460 File Offset: 0x00077660
			protected override bool MoveNextCore()
			{
				this._pivotColPosition++;
				while (this._pivotColPosition >= this._pivotColSize)
				{
					if (!base.Input.MoveNext())
					{
						return false;
					}
					this._pivotColPosition = 0;
					this._pivotColSize = this.CalcPivotColSize();
				}
				return true;
			}

			// Token: 0x060014F3 RID: 5363 RVA: 0x000794B0 File Offset: 0x000776B0
			private int CalcPivotColSize()
			{
				int num = this._fixedSize;
				foreach (Func<int> func in this._sizeGetters)
				{
					int num2 = func();
					if (this._schema.Mode == UngroupTransform.UngroupMode.Inner && num2 == 0)
					{
						return 0;
					}
					if (num == 0)
					{
						num = num2;
					}
					else if (this._schema.Mode == UngroupTransform.UngroupMode.Inner && num > num2)
					{
						num = num2;
					}
					else if (this._schema.Mode == UngroupTransform.UngroupMode.Outer && num < num2)
					{
						num = num2;
					}
				}
				return num;
			}

			// Token: 0x060014F4 RID: 5364 RVA: 0x00079560 File Offset: 0x00077760
			private Func<int> MakeSizeGetter<T>(int col)
			{
				ValueGetter<T> srcGetter = this.GetGetter<T>(col);
				T cur = default(T);
				return delegate
				{
					srcGetter.Invoke(ref cur);
					return this._colSizes[col];
				};
			}

			// Token: 0x170001EF RID: 495
			// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000795AB File Offset: 0x000777AB
			public ISchema Schema
			{
				get
				{
					return this._schema;
				}
			}

			// Token: 0x060014F6 RID: 5366 RVA: 0x000795B3 File Offset: 0x000777B3
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._schema.ColumnCount);
				return this._active[col];
			}

			// Token: 0x060014F7 RID: 5367 RVA: 0x000795E0 File Offset: 0x000777E0
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col < this._schema.ColumnCount, "col");
				if (!this._schema.IsPivot(col))
				{
					return base.Input.GetGetter<TValue>(col);
				}
				if (this._cachedGetters[col] == null)
				{
					this._cachedGetters[col] = this.MakeGetter<TValue>(col, this._schema.GetPivotColumnInfoByCol(col).ItemType);
				}
				ValueGetter<TValue> valueGetter = this._cachedGetters[col] as ValueGetter<TValue>;
				Contracts.Check(this._ch, valueGetter != null, "Unexpected getter type requested");
				return valueGetter;
			}

			// Token: 0x060014F8 RID: 5368 RVA: 0x0007980C File Offset: 0x00077A0C
			private ValueGetter<T> MakeGetter<T>(int col, PrimitiveType itemType)
			{
				ValueGetter<VBuffer<T>> srcGetter = base.Input.GetGetter<VBuffer<T>>(col);
				long cachedPosition = -1L;
				int cachedIndex = 0;
				VBuffer<T> row = default(VBuffer<T>);
				T naValue = Conversions.Instance.GetNAOrDefault<T>(itemType);
				return delegate(ref T value)
				{
					Contracts.Check(this._ch, this.Input.State == 1, "Cursor is not active");
					if (cachedPosition < this.Input.Position)
					{
						srcGetter.Invoke(ref row);
						this._colSizes[col] = row.Length;
						cachedPosition = this.Input.Position;
						cachedIndex = 0;
					}
					if (this._pivotColPosition >= row.Length)
					{
						value = naValue;
						return;
					}
					if (row.IsDense)
					{
						value = row.Values[this._pivotColPosition];
						return;
					}
					while (cachedIndex < row.Count && this._pivotColPosition > row.Indices[cachedIndex])
					{
						cachedIndex++;
					}
					if (cachedIndex < row.Count && this._pivotColPosition == row.Indices[cachedIndex])
					{
						value = row.Values[cachedIndex];
						return;
					}
					value = default(T);
				};
			}

			// Token: 0x04000C8F RID: 3215
			private readonly UngroupTransform.SchemaImpl _schema;

			// Token: 0x04000C90 RID: 3216
			private int _pivotColSize;

			// Token: 0x04000C91 RID: 3217
			private int _pivotColPosition;

			// Token: 0x04000C92 RID: 3218
			private readonly bool[] _active;

			// Token: 0x04000C93 RID: 3219
			private readonly Delegate[] _cachedGetters;

			// Token: 0x04000C94 RID: 3220
			private readonly int _fixedSize;

			// Token: 0x04000C95 RID: 3221
			private readonly Func<int>[] _sizeGetters;

			// Token: 0x04000C96 RID: 3222
			private int[] _colSizes;
		}
	}
}
