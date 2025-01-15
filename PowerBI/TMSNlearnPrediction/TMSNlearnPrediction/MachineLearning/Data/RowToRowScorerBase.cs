using System;
using System.Reflection;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000078 RID: 120
	public abstract class RowToRowScorerBase : RowToRowTransformBase, IDataScorerTransform, ITransformTemplate, IDataTransform, IDataView, ISchematized, ICanSaveModel
	{
		// Token: 0x06000213 RID: 531 RVA: 0x0000C895 File Offset: 0x0000AA95
		protected RowToRowScorerBase(IHostEnvironment env, IDataView input, string registrationName, ISchemaBindableMapper bindable)
			: base(env, registrationName, input)
		{
			this._bindable = bindable;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		protected RowToRowScorerBase(ModelLoadContext ctx, IHost host, IDataView input)
			: base(host, input)
		{
			ctx.LoadModel<ISchemaBindableMapper, SignatureLoadModel>(out this._bindable, "SchemaBindableMapper", new object[] { host });
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C8DA File Offset: 0x0000AADA
		public sealed override void Save(ModelSaveContext ctx)
		{
			ctx.CheckAtModel();
			ctx.SaveModel<ISchemaBindableMapper>(this._bindable, "SchemaBindableMapper");
			this.SaveCore(ctx);
		}

		// Token: 0x06000216 RID: 534
		protected abstract void SaveCore(ModelSaveContext ctx);

		// Token: 0x06000217 RID: 535
		public abstract IDataTransform ApplyToData(IHostEnvironment env, IDataView newSource);

		// Token: 0x06000218 RID: 536
		protected abstract RowToRowScorerBase.BindingsBase GetBindings();

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000C8FA File Offset: 0x0000AAFA
		public sealed override ISchema Schema
		{
			get
			{
				return this.GetBindings();
			}
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C938 File Offset: 0x0000AB38
		private static bool[] GetActive(RowToRowScorerBase.BindingsBase bindings, Func<int, bool> predicate, out Func<int, bool> predicateInput, out Func<int, bool> predicateMapper)
		{
			bool[] active = bindings.GetActive(predicate);
			bool[] activeInput = bindings.GetActiveInput(predicate);
			predicateMapper = bindings.GetActiveMapperColumns(active);
			Func<int, bool> predicateInputForMapper = bindings.RowMapper.GetDependencies(predicateMapper);
			predicateInput = (int col) => 0 <= col && col < activeInput.Length && (activeInput[col] || predicateInputForMapper(col));
			return active;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C98C File Offset: 0x0000AB8C
		protected sealed override bool? ShouldUseParallelCursors(Func<int, bool> predicate)
		{
			if (this.WantParallelCursors(predicate))
			{
				return new bool?(true);
			}
			return null;
		}

		// Token: 0x0600021C RID: 540
		protected abstract bool WantParallelCursors(Func<int, bool> predicate);

		// Token: 0x0600021D RID: 541 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
		protected override IRowCursor GetRowCursorCore(Func<int, bool> predicate, IRandom rand = null)
		{
			RowToRowScorerBase.BindingsBase bindings = this.GetBindings();
			Func<int, bool> func;
			Func<int, bool> func2;
			bool[] active = RowToRowScorerBase.GetActive(bindings, predicate, out func, out func2);
			IRowCursor rowCursor = this._input.GetRowCursor(func, rand);
			return new RowToRowScorerBase.RowCursor(this._host, this, rowCursor, active, func2);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		public override IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			RowToRowScorerBase.BindingsBase bindings = this.GetBindings();
			Func<int, bool> func;
			Func<int, bool> func2;
			bool[] active = RowToRowScorerBase.GetActive(bindings, predicate, out func, out func2);
			IRowCursor[] array = this._input.GetRowCursorSet(ref consolidator, func, n, rand);
			if (array.Length == 1 && n > 1 && this.WantParallelCursors(predicate))
			{
				array = DataViewUtils.CreateSplitCursors(out consolidator, this._host, array[0], n);
			}
			IRowCursor[] array2 = new IRowCursor[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = new RowToRowScorerBase.RowCursor(this._host, this, array[i], active, func2);
			}
			return array2;
		}

		// Token: 0x0600021F RID: 543
		protected abstract Delegate[] GetGetters(IRow output, Func<int, bool> predicate);

		// Token: 0x06000220 RID: 544 RVA: 0x0000CA98 File Offset: 0x0000AC98
		protected static Delegate[] GetGettersFromRow(IRow row, Func<int, bool> predicate)
		{
			Delegate[] array = new Delegate[row.Schema.ColumnCount];
			for (int i = 0; i < array.Length; i++)
			{
				if (predicate(i))
				{
					array[i] = RowToRowScorerBase.GetGetterFromRow(row, i);
				}
			}
			return array;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		protected static Delegate GetGetterFromRow(IRow row, int col)
		{
			ColumnType columnType = row.Schema.GetColumnType(col);
			Func<IRow, int, ValueGetter<int>> func = new Func<IRow, int, ValueGetter<int>>(RowToRowScorerBase.GetGetterFromRow<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType.RawType });
			return (Delegate)methodInfo.Invoke(null, new object[] { row, col });
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000CB46 File Offset: 0x0000AD46
		protected static ValueGetter<T> GetGetterFromRow<T>(IRow output, int col)
		{
			return output.GetGetter<T>(col);
		}

		// Token: 0x040000C0 RID: 192
		protected readonly ISchemaBindableMapper _bindable;

		// Token: 0x0200007A RID: 122
		public abstract class BindingsBase : ScorerBindingsBase
		{
			// Token: 0x0600022E RID: 558 RVA: 0x0000D05F File Offset: 0x0000B25F
			protected BindingsBase(ISchema schema, ISchemaBoundRowMapper mapper, string suffix, bool user, params string[] namesDerived)
				: base(schema, mapper, suffix, user, namesDerived)
			{
				this.RowMapper = mapper;
			}

			// Token: 0x040000C6 RID: 198
			public readonly ISchemaBoundRowMapper RowMapper;
		}

		// Token: 0x0200007B RID: 123
		protected sealed class RowCursor : SynchronizedCursorBase<IRowCursor>, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x0600022F RID: 559 RVA: 0x0000D098 File Offset: 0x0000B298
			public RowCursor(IChannelProvider provider, RowToRowScorerBase parent, IRowCursor input, bool[] active, Func<int, bool> predicateMapper)
				: base(provider, input)
			{
				RowToRowScorerBase.RowCursor <>4__this = this;
				this._bindings = parent.GetBindings();
				this._active = active;
				IRow outputRow = this._bindings.RowMapper.GetOutputRow(input, predicateMapper);
				this._getters = parent.GetGetters(outputRow, (int iinfo) => active[<>4__this._bindings.MapIinfoToCol(iinfo)]);
			}

			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000230 RID: 560 RVA: 0x0000D10E File Offset: 0x0000B30E
			public ISchema Schema
			{
				get
				{
					return this._bindings;
				}
			}

			// Token: 0x06000231 RID: 561 RVA: 0x0000D116 File Offset: 0x0000B316
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._bindings.ColumnCount);
				return this._active[col];
			}

			// Token: 0x06000232 RID: 562 RVA: 0x0000D140 File Offset: 0x0000B340
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col));
				bool flag;
				int num = this._bindings.MapColumnIndex(out flag, col);
				if (flag)
				{
					return base.Input.GetGetter<TValue>(num);
				}
				Delegate @delegate = this._getters[num];
				ValueGetter<TValue> valueGetter = @delegate as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x040000C7 RID: 199
			private readonly RowToRowScorerBase.BindingsBase _bindings;

			// Token: 0x040000C8 RID: 200
			private readonly bool[] _active;

			// Token: 0x040000C9 RID: 201
			private readonly Delegate[] _getters;
		}
	}
}
