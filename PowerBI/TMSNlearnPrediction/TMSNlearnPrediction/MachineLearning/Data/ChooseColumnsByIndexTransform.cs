using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000265 RID: 613
	public sealed class ChooseColumnsByIndexTransform : RowToRowTransformBase
	{
		// Token: 0x06000D91 RID: 3473 RVA: 0x0004BB63 File Offset: 0x00049D63
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("CHSCOLIF", 65537U, 65537U, 65537U, "ChooseColumnsIdxTrans", "ChooseColumnsIdxFunc");
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x0004BB88 File Offset: 0x00049D88
		public ChooseColumnsByIndexTransform(ChooseColumnsByIndexTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "ChooseColumnsByIndex", input)
		{
			Contracts.CheckValue<ChooseColumnsByIndexTransform.Arguments>(this._host, args, "args");
			this._bindings = new ChooseColumnsByIndexTransform.Bindings(args, this._input.Schema);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0004BBBF File Offset: 0x00049DBF
		private ChooseColumnsByIndexTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			this._bindings = new ChooseColumnsByIndexTransform.Bindings(ctx, this._input.Schema);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0004BC04 File Offset: 0x00049E04
		public static ChooseColumnsByIndexTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("ChooseColumnsByIndex");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(ChooseColumnsByIndexTransform.GetVersionInfo());
			return HostExtensions.Apply<ChooseColumnsByIndexTransform>(h, "Loading Model", (IChannel ch) => new ChooseColumnsByIndexTransform(ctx, h, input));
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0004BC99 File Offset: 0x00049E99
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(ChooseColumnsByIndexTransform.GetVersionInfo());
			this._bindings.Save(ctx);
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0004BCC9 File Offset: 0x00049EC9
		public override ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x0004BCD4 File Offset: 0x00049ED4
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return null;
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x0004BCEC File Offset: 0x00049EEC
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor rowCursor = this._input.GetRowCursor(dependencies, rand);
			return new ChooseColumnsByIndexTransform.RowCursor(this._host, this._bindings, rowCursor, active);
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x0004BD34 File Offset: 0x00049F34
		public sealed override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor[] rowCursorSet = this._input.GetRowCursorSet(ref consolidator, dependencies, n, rand);
			IRowCursor[] array = new IRowCursor[rowCursorSet.Length];
			for (int i = 0; i < rowCursorSet.Length; i++)
			{
				array[i] = new ChooseColumnsByIndexTransform.RowCursor(this._host, this._bindings, rowCursorSet[i], active);
			}
			return array;
		}

		// Token: 0x040007B1 RID: 1969
		public const string LoaderSignature = "ChooseColumnsIdxTrans";

		// Token: 0x040007B2 RID: 1970
		internal const string LoaderSignatureOld = "ChooseColumnsIdxFunc";

		// Token: 0x040007B3 RID: 1971
		private const string RegistrationName = "ChooseColumnsByIndex";

		// Token: 0x040007B4 RID: 1972
		private readonly ChooseColumnsByIndexTransform.Bindings _bindings;

		// Token: 0x02000266 RID: 614
		public sealed class Arguments
		{
			// Token: 0x040007B5 RID: 1973
			[Argument(4, HelpText = "Column index to select", ShortName = "ind")]
			public int[] index;

			// Token: 0x040007B6 RID: 1974
			[Argument(4, HelpText = "If true, selected columns are dropped instead of kept, with the order of kept columns being the same as the original", ShortName = "d")]
			public bool drop;
		}

		// Token: 0x02000267 RID: 615
		private sealed class Bindings : ISchema
		{
			// Token: 0x06000D9B RID: 3483 RVA: 0x0004BDBC File Offset: 0x00049FBC
			public Bindings(ChooseColumnsByIndexTransform.Arguments args, ISchema schemaInput)
			{
				this._input = schemaInput;
				int[] array = ((args.index == null) ? new int[0] : args.index.ToArray<int>());
				this.BuildNameDict(array, args.drop, out this.Sources, out this._dropped, out this._nameToIndex, true);
			}

			// Token: 0x06000D9C RID: 3484 RVA: 0x0004BE14 File Offset: 0x0004A014
			private void BuildNameDict(int[] indexCopy, bool drop, out int[] sources, out int[] dropped, out Dictionary<string, int> nameToCol, bool user)
			{
				int i = 0;
				while (i < indexCopy.Length)
				{
					int num = indexCopy[i];
					if (num < 0 || this._input.ColumnCount <= num)
					{
						if (user)
						{
							throw Contracts.ExceptUserArg("columnIndices", "Column index {0} invalid for input with {1} columns", new object[]
							{
								num,
								this._input.ColumnCount
							});
						}
						throw Contracts.ExceptDecode("Column index {0} invalid for input with {1} columns", new object[]
						{
							num,
							this._input.ColumnCount
						});
					}
					else
					{
						i++;
					}
				}
				if (drop)
				{
					sources = Enumerable.Range(0, this._input.ColumnCount).Except(indexCopy).ToArray<int>();
					dropped = indexCopy;
				}
				else
				{
					sources = indexCopy;
					dropped = null;
				}
				if (user)
				{
					Contracts.CheckUserArg(sources.Length > 0, "columnIndices", "Choose columns by index has no output columns");
				}
				else
				{
					Contracts.CheckDecode(sources.Length > 0, "Choose columns by index has no output columns");
				}
				nameToCol = new Dictionary<string, int>();
				for (int j = 0; j < sources.Length; j++)
				{
					nameToCol[this._input.GetColumnName(sources[j])] = j;
				}
			}

			// Token: 0x06000D9D RID: 3485 RVA: 0x0004BF48 File Offset: 0x0004A148
			public Bindings(ModelLoadContext ctx, ISchema schemaInput)
			{
				this._input = schemaInput;
				bool flag = Utils.ReadBoolByte(ctx.Reader);
				this.BuildNameDict(Utils.ReadIntArray(ctx.Reader) ?? new int[0], flag, out this.Sources, out this._dropped, out this._nameToIndex, false);
			}

			// Token: 0x06000D9E RID: 3486 RVA: 0x0004BF9D File Offset: 0x0004A19D
			public void Save(ModelSaveContext ctx)
			{
				Utils.WriteBoolByte(ctx.Writer, this._dropped != null);
				Utils.WriteIntArray(ctx.Writer, this._dropped ?? this.Sources);
			}

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x06000D9F RID: 3487 RVA: 0x0004BFD1 File Offset: 0x0004A1D1
			public int ColumnCount
			{
				get
				{
					return this.Sources.Length;
				}
			}

			// Token: 0x06000DA0 RID: 3488 RVA: 0x0004BFDB File Offset: 0x0004A1DB
			public bool TryGetColumnIndex(string name, out int col)
			{
				if (name == null)
				{
					col = 0;
					return false;
				}
				return this._nameToIndex.TryGetValue(name, out col);
			}

			// Token: 0x06000DA1 RID: 3489 RVA: 0x0004BFF2 File Offset: 0x0004A1F2
			public string GetColumnName(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this._input.GetColumnName(this.Sources[col]);
			}

			// Token: 0x06000DA2 RID: 3490 RVA: 0x0004C021 File Offset: 0x0004A221
			public ColumnType GetColumnType(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this._input.GetColumnType(this.Sources[col]);
			}

			// Token: 0x06000DA3 RID: 3491 RVA: 0x0004C050 File Offset: 0x0004A250
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this._input.GetMetadataTypes(this.Sources[col]);
			}

			// Token: 0x06000DA4 RID: 3492 RVA: 0x0004C07F File Offset: 0x0004A27F
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				Contracts.CheckNonEmpty(kind, "kind");
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this._input.GetMetadataTypeOrNull(kind, this.Sources[col]);
			}

			// Token: 0x06000DA5 RID: 3493 RVA: 0x0004C0BB File Offset: 0x0004A2BB
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				Contracts.CheckNonEmpty(kind, "kind");
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				this._input.GetMetadata<TValue>(kind, this.Sources[col], ref value);
			}

			// Token: 0x06000DA6 RID: 3494 RVA: 0x0004C0F8 File Offset: 0x0004A2F8
			internal bool[] GetActive(Func<int, bool> predicate)
			{
				return Utils.BuildArray<bool>(this.ColumnCount, predicate);
			}

			// Token: 0x06000DA7 RID: 3495 RVA: 0x0004C12C File Offset: 0x0004A32C
			internal Func<int, bool> GetDependencies(Func<int, bool> predicate)
			{
				bool[] active = new bool[this._input.ColumnCount];
				for (int i = 0; i < this.Sources.Length; i++)
				{
					if (predicate(i))
					{
						active[this.Sources[i]] = true;
					}
				}
				return (int col) => 0 <= col && col < active.Length && active[col];
			}

			// Token: 0x040007B7 RID: 1975
			public readonly int[] Sources;

			// Token: 0x040007B8 RID: 1976
			private readonly ISchema _input;

			// Token: 0x040007B9 RID: 1977
			private readonly Dictionary<string, int> _nameToIndex;

			// Token: 0x040007BA RID: 1978
			private readonly int[] _dropped;
		}

		// Token: 0x02000268 RID: 616
		private sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x06000DA8 RID: 3496 RVA: 0x0004C18D File Offset: 0x0004A38D
			public RowCursor(IChannelProvider provider, ChooseColumnsByIndexTransform.Bindings bindings, IRowCursor input, bool[] active)
				: base(provider, input)
			{
				this._bindings = bindings;
				this._active = active;
			}

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x06000DA9 RID: 3497 RVA: 0x0004C1A6 File Offset: 0x0004A3A6
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x06000DAA RID: 3498 RVA: 0x0004C1AE File Offset: 0x0004A3AE
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.ColumnCount);
				return this._active == null || this._active[col];
			}

			// Token: 0x06000DAB RID: 3499 RVA: 0x0004C1E4 File Offset: 0x0004A3E4
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				int num = this._bindings.Sources[col];
				return base.Input.GetGetter<TValue>(num);
			}

			// Token: 0x040007BB RID: 1979
			private readonly ChooseColumnsByIndexTransform.Bindings _bindings;

			// Token: 0x040007BC RID: 1980
			private readonly bool[] _active;
		}
	}
}
