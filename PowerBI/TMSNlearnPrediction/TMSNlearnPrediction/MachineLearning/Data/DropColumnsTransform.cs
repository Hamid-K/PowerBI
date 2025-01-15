using System;
using System.Collections.Generic;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001F5 RID: 501
	public sealed class DropColumnsTransform : RowToRowTransformBase
	{
		// Token: 0x06000B2D RID: 2861 RVA: 0x0003C1F7 File Offset: 0x0003A3F7
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("DRPCOLST", 65538U, 65538U, 65538U, "DropColumnsTransform", null);
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0003C218 File Offset: 0x0003A418
		public DropColumnsTransform(DropColumnsTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "DropColumns", input)
		{
			Contracts.CheckValue<DropColumnsTransform.Arguments>(this._host, args, "args");
			Contracts.CheckNonEmpty<string>(this._host, args.column, "column");
			this._bindings = new DropColumnsTransform.Bindings(args, false, this._input.Schema);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0003C274 File Offset: 0x0003A474
		public DropColumnsTransform(DropColumnsTransform.KeepArguments args, IHostEnvironment env, IDataView input)
			: base(env, "KeepColumns", input)
		{
			Contracts.CheckValue<DropColumnsTransform.KeepArguments>(this._host, args, "args");
			Contracts.CheckNonEmpty<string>(this._host, args.column, "column");
			this._bindings = new DropColumnsTransform.Bindings(args, true, this._input.Schema);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0003C2D0 File Offset: 0x0003A4D0
		private DropColumnsTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, num == 4);
			this._bindings = new DropColumnsTransform.Bindings(ctx, this._input.Schema);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0003C338 File Offset: 0x0003A538
		public static DropColumnsTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("DropColumns");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(DropColumnsTransform.GetVersionInfo());
			return HostExtensions.Apply<DropColumnsTransform>(h, "Loading Model", (IChannel ch) => new DropColumnsTransform(ctx, h, input));
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0003C3CD File Offset: 0x0003A5CD
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(DropColumnsTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			this._bindings.Save(ctx);
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x0003C409 File Offset: 0x0003A609
		public override ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0003C414 File Offset: 0x0003A614
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return null;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0003C42C File Offset: 0x0003A62C
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor rowCursor = this._input.GetRowCursor(dependencies, rand);
			return new DropColumnsTransform.RowCursor(this._host, this._bindings, rowCursor, active);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0003C474 File Offset: 0x0003A674
		public sealed override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			bool[] active = this._bindings.GetActive(predicate);
			IRowCursor[] rowCursorSet = this._input.GetRowCursorSet(ref consolidator, dependencies, n, rand);
			IRowCursor[] array = new IRowCursor[rowCursorSet.Length];
			for (int i = 0; i < rowCursorSet.Length; i++)
			{
				array[i] = new DropColumnsTransform.RowCursor(this._host, this._bindings, rowCursorSet[i], active);
			}
			return array;
		}

		// Token: 0x04000607 RID: 1543
		internal const string DropColumnsSummary = "Removes a column from the dataset.";

		// Token: 0x04000608 RID: 1544
		internal const string KeepColumnsSummary = "Selects which columns from the dataset to keep.";

		// Token: 0x04000609 RID: 1545
		public const string LoaderSignature = "DropColumnsTransform";

		// Token: 0x0400060A RID: 1546
		private const string DropRegistrationName = "DropColumns";

		// Token: 0x0400060B RID: 1547
		private const string KeepRegistrationName = "KeepColumns";

		// Token: 0x0400060C RID: 1548
		private readonly DropColumnsTransform.Bindings _bindings;

		// Token: 0x020001F6 RID: 502
		public abstract class ArgumentsBase
		{
			// Token: 0x1700014A RID: 330
			// (get) Token: 0x06000B37 RID: 2871
			internal abstract string[] Column { get; }
		}

		// Token: 0x020001F7 RID: 503
		public sealed class Arguments : DropColumnsTransform.ArgumentsBase
		{
			// Token: 0x1700014B RID: 331
			// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0003C4FB File Offset: 0x0003A6FB
			internal override string[] Column
			{
				get
				{
					return this.column;
				}
			}

			// Token: 0x0400060D RID: 1549
			[Argument(4, HelpText = "Column name to drop", ShortName = "col", SortOrder = 1)]
			public string[] column;
		}

		// Token: 0x020001F8 RID: 504
		public sealed class KeepArguments : DropColumnsTransform.ArgumentsBase
		{
			// Token: 0x1700014C RID: 332
			// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0003C50B File Offset: 0x0003A70B
			internal override string[] Column
			{
				get
				{
					return this.column;
				}
			}

			// Token: 0x0400060E RID: 1550
			[Argument(4, HelpText = "Column name to keep", ShortName = "col", SortOrder = 1)]
			public string[] column;
		}

		// Token: 0x020001F9 RID: 505
		private sealed class Bindings : ISchema
		{
			// Token: 0x06000B3D RID: 2877 RVA: 0x0003C51C File Offset: 0x0003A71C
			public Bindings(DropColumnsTransform.ArgumentsBase args, bool keep, ISchema schemaInput)
			{
				this.Keep = keep;
				this.Input = schemaInput;
				this.Names = new HashSet<string>();
				for (int i = 0; i < args.Column.Length; i++)
				{
					string text = args.Column[i];
					Contracts.CheckUserArg(!string.IsNullOrWhiteSpace(text), "source");
					if (!this.Names.Add(text))
					{
						throw Contracts.ExceptUserArg("column", "Column '{0}' specified multiple times", new object[] { text });
					}
				}
				this.BuildMap(out this.ColMap, out this.NameToCol);
			}

			// Token: 0x06000B3E RID: 2878 RVA: 0x0003C5B4 File Offset: 0x0003A7B4
			private void BuildMap(out int[] map, out Dictionary<string, int> nameToCol)
			{
				List<int> list = new List<int>();
				nameToCol = new Dictionary<string, int>();
				for (int i = 0; i < this.Input.ColumnCount; i++)
				{
					string columnName = this.Input.GetColumnName(i);
					if (this.Names.Contains(columnName) != !this.Keep)
					{
						int num;
						if (this.Input.TryGetColumnIndex(columnName, ref num) && num == i)
						{
							nameToCol.Add(columnName, list.Count);
						}
						list.Add(i);
					}
				}
				map = list.ToArray();
			}

			// Token: 0x06000B3F RID: 2879 RVA: 0x0003C63C File Offset: 0x0003A83C
			public Bindings(ModelLoadContext ctx, ISchema schemaInput)
			{
				this.Input = schemaInput;
				this.Keep = Utils.ReadBoolByte(ctx.Reader);
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(num > 0);
				this.Names = new HashSet<string>();
				for (int i = 0; i < num; i++)
				{
					string text = ctx.LoadNonEmptyString();
					if (!this.Names.Add(text))
					{
						throw Contracts.ExceptDecode("Column '{0}' specified multiple times", new object[] { text });
					}
				}
				this.BuildMap(out this.ColMap, out this.NameToCol);
			}

			// Token: 0x06000B40 RID: 2880 RVA: 0x0003C6D4 File Offset: 0x0003A8D4
			public void Save(ModelSaveContext ctx)
			{
				Utils.WriteBoolByte(ctx.Writer, this.Keep);
				ctx.Writer.Write(this.Names.Count);
				foreach (string text in this.Names)
				{
					ctx.SaveNonEmptyString(text);
				}
			}

			// Token: 0x1700014D RID: 333
			// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0003C750 File Offset: 0x0003A950
			public int ColumnCount
			{
				get
				{
					return this.ColMap.Length;
				}
			}

			// Token: 0x06000B42 RID: 2882 RVA: 0x0003C75A File Offset: 0x0003A95A
			public bool TryGetColumnIndex(string name, out int col)
			{
				if (name == null)
				{
					col = 0;
					return false;
				}
				return this.NameToCol.TryGetValue(name, out col);
			}

			// Token: 0x06000B43 RID: 2883 RVA: 0x0003C771 File Offset: 0x0003A971
			public string GetColumnName(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this.Input.GetColumnName(this.ColMap[col]);
			}

			// Token: 0x06000B44 RID: 2884 RVA: 0x0003C7A0 File Offset: 0x0003A9A0
			public ColumnType GetColumnType(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this.Input.GetColumnType(this.ColMap[col]);
			}

			// Token: 0x06000B45 RID: 2885 RVA: 0x0003C7CF File Offset: 0x0003A9CF
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this.Input.GetMetadataTypes(this.ColMap[col]);
			}

			// Token: 0x06000B46 RID: 2886 RVA: 0x0003C7FE File Offset: 0x0003A9FE
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				Contracts.CheckNonEmpty(kind, "kind");
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				return this.Input.GetMetadataTypeOrNull(kind, this.ColMap[col]);
			}

			// Token: 0x06000B47 RID: 2887 RVA: 0x0003C83A File Offset: 0x0003AA3A
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				Contracts.CheckNonEmpty(kind, "kind");
				Contracts.CheckParam(0 <= col && col < this.ColumnCount, "col");
				this.Input.GetMetadata<TValue>(kind, this.ColMap[col], ref value);
			}

			// Token: 0x06000B48 RID: 2888 RVA: 0x0003C877 File Offset: 0x0003AA77
			internal bool[] GetActive(Func<int, bool> predicate)
			{
				return Utils.BuildArray<bool>(this.ColumnCount, predicate);
			}

			// Token: 0x06000B49 RID: 2889 RVA: 0x0003C8A8 File Offset: 0x0003AAA8
			internal Func<int, bool> GetDependencies(Func<int, bool> predicate)
			{
				bool[] active = new bool[this.Input.ColumnCount];
				for (int i = 0; i < this.ColMap.Length; i++)
				{
					if (predicate(i))
					{
						active[this.ColMap[i]] = true;
					}
				}
				return (int col) => 0 <= col && col < active.Length && active[col];
			}

			// Token: 0x0400060F RID: 1551
			public readonly ISchema Input;

			// Token: 0x04000610 RID: 1552
			public readonly bool Keep;

			// Token: 0x04000611 RID: 1553
			public readonly HashSet<string> Names;

			// Token: 0x04000612 RID: 1554
			public readonly int[] ColMap;

			// Token: 0x04000613 RID: 1555
			public readonly Dictionary<string, int> NameToCol;
		}

		// Token: 0x020001FA RID: 506
		private sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x06000B4A RID: 2890 RVA: 0x0003C909 File Offset: 0x0003AB09
			public RowCursor(IChannelProvider provider, DropColumnsTransform.Bindings bindings, IRowCursor input, bool[] active)
				: base(provider, input)
			{
				this._bindings = bindings;
				this._active = active;
			}

			// Token: 0x1700014E RID: 334
			// (get) Token: 0x06000B4B RID: 2891 RVA: 0x0003C922 File Offset: 0x0003AB22
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x06000B4C RID: 2892 RVA: 0x0003C92A File Offset: 0x0003AB2A
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.ColumnCount);
				return this._active == null || this._active[col];
			}

			// Token: 0x06000B4D RID: 2893 RVA: 0x0003C95E File Offset: 0x0003AB5E
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				return base.Input.GetGetter<TValue>(this._bindings.ColMap[col]);
			}

			// Token: 0x04000614 RID: 1556
			private readonly DropColumnsTransform.Bindings _bindings;

			// Token: 0x04000615 RID: 1557
			private readonly bool[] _active;
		}
	}
}
