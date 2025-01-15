using System;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000124 RID: 292
	public sealed class ProduceIdTransform : RowToRowTransformBase
	{
		// Token: 0x060005F9 RID: 1529 RVA: 0x000208D5 File Offset: 0x0001EAD5
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("PR ID XF", 65537U, 65537U, 65537U, "ProduceIdTransform", null);
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x000208F6 File Offset: 0x0001EAF6
		public override ISchema Schema
		{
			get
			{
				return this._bindings;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x000208FE File Offset: 0x0001EAFE
		public override bool CanShuffle
		{
			get
			{
				return this._input.CanShuffle;
			}
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x0002090C File Offset: 0x0001EB0C
		public ProduceIdTransform(ProduceIdTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "ProduceIdTransform", input)
		{
			Contracts.CheckValue<ProduceIdTransform.Arguments>(this._host, args, "args");
			Contracts.CheckNonWhiteSpace(this._host, args.column, "column");
			this._bindings = new ProduceIdTransform.Bindings(input.Schema, true, args.column);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x00020966 File Offset: 0x0001EB66
		private ProduceIdTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			this._bindings = ProduceIdTransform.Bindings.Create(ctx, this._input.Schema);
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x000209A8 File Offset: 0x0001EBA8
		public static ProduceIdTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("ProduceIdTransform");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(ProduceIdTransform.GetVersionInfo());
			return HostExtensions.Apply<ProduceIdTransform>(h, "Loading Model", (IChannel ch) => new ProduceIdTransform(ctx, h, input));
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00020A3D File Offset: 0x0001EC3D
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(ProduceIdTransform.GetVersionInfo());
			this._bindings.Save(ctx);
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00020A70 File Offset: 0x0001EC70
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			IRowCursor rowCursor = this._input.GetRowCursor(dependencies, rand);
			bool flag = predicate(this._bindings.MapIinfoToCol(0));
			return new ProduceIdTransform.RowCursor(this._host, this._bindings, rowCursor, flag);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00020AC0 File Offset: 0x0001ECC0
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool> dependencies = this._bindings.GetDependencies(predicate);
			IRowCursor[] rowCursorSet = this._input.GetRowCursorSet(ref consolidator, dependencies, n, rand);
			bool flag = predicate(this._bindings.MapIinfoToCol(0));
			for (int i = 0; i < rowCursorSet.Length; i++)
			{
				rowCursorSet[i] = new ProduceIdTransform.RowCursor(this._host, this._bindings, rowCursorSet[i], flag);
			}
			return rowCursorSet;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00020B38 File Offset: 0x0001ED38
		protected override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			return null;
		}

		// Token: 0x040002FD RID: 765
		internal const string Summary = "Produces a new column with the row ID.";

		// Token: 0x040002FE RID: 766
		public const string LoaderSignature = "ProduceIdTransform";

		// Token: 0x040002FF RID: 767
		private readonly ProduceIdTransform.Bindings _bindings;

		// Token: 0x02000125 RID: 293
		public sealed class Arguments
		{
			// Token: 0x04000300 RID: 768
			[Argument(0, HelpText = "Name of the column to produce", ShortName = "col", SortOrder = 1)]
			public string column = "Id";
		}

		// Token: 0x02000126 RID: 294
		private sealed class Bindings : ColumnBindingsBase
		{
			// Token: 0x06000604 RID: 1540 RVA: 0x00020B64 File Offset: 0x0001ED64
			public Bindings(ISchema input, bool user, string name)
				: base(input, user, new string[] { name })
			{
			}

			// Token: 0x06000605 RID: 1541 RVA: 0x00020B85 File Offset: 0x0001ED85
			protected override ColumnType GetColumnTypeCore(int iinfo)
			{
				return NumberType.UG;
			}

			// Token: 0x06000606 RID: 1542 RVA: 0x00020B8C File Offset: 0x0001ED8C
			public static ProduceIdTransform.Bindings Create(ModelLoadContext ctx, ISchema input)
			{
				string text = ctx.LoadNonEmptyString();
				return new ProduceIdTransform.Bindings(input, true, text);
			}

			// Token: 0x06000607 RID: 1543 RVA: 0x00020BA8 File Offset: 0x0001EDA8
			public void Save(ModelSaveContext ctx)
			{
				ctx.SaveNonEmptyString(base.GetColumnNameCore(0));
			}

			// Token: 0x06000608 RID: 1544 RVA: 0x00020BDC File Offset: 0x0001EDDC
			public Func<int, bool> GetDependencies(Func<int, bool> predicate)
			{
				bool[] active = base.GetActiveInput(predicate);
				return (int col) => 0 <= col && col < active.Length && active[col];
			}
		}

		// Token: 0x02000127 RID: 295
		private sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x17000079 RID: 121
			// (get) Token: 0x06000609 RID: 1545 RVA: 0x00020C08 File Offset: 0x0001EE08
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x0600060A RID: 1546 RVA: 0x00020C10 File Offset: 0x0001EE10
			public RowCursor(IChannelProvider provider, ProduceIdTransform.Bindings bindings, IRowCursor input, bool active)
				: base(provider, input)
			{
				Contracts.CheckValue<ProduceIdTransform.Bindings>(this._ch, bindings, "bindings");
				this._bindings = bindings;
				this._active = active;
			}

			// Token: 0x0600060B RID: 1547 RVA: 0x00020C3C File Offset: 0x0001EE3C
			public bool IsColumnActive(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col < this._bindings.ColumnCount, "col");
				bool flag;
				int num = this._bindings.MapColumnIndex(out flag, col);
				if (flag)
				{
					return base.Input.IsColumnActive(num);
				}
				return this._active;
			}

			// Token: 0x0600060C RID: 1548 RVA: 0x00020C94 File Offset: 0x0001EE94
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col < this._bindings.ColumnCount, "col");
				Contracts.CheckParam(this._ch, this.IsColumnActive(col), "col");
				bool flag;
				int num = this._bindings.MapColumnIndex(out flag, col);
				if (flag)
				{
					return base.Input.GetGetter<TValue>(num);
				}
				Delegate idGetter = base.Input.GetIdGetter();
				ValueGetter<TValue> valueGetter = idGetter as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x04000301 RID: 769
			private readonly ProduceIdTransform.Bindings _bindings;

			// Token: 0x04000302 RID: 770
			private readonly bool _active;
		}
	}
}
