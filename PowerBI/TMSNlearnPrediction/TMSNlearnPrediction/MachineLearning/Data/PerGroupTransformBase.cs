using System;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000121 RID: 289
	public abstract class PerGroupTransformBase<TLabel, TScore, TState> : IDataTransform, IDataView, ISchematized, ICanSaveModel where TState : class
	{
		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060005DE RID: 1502 RVA: 0x000201E4 File Offset: 0x0001E3E4
		public ISchema Schema
		{
			get
			{
				return this.GetBindings();
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x000201EC File Offset: 0x0001E3EC
		public IDataView Source
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060005E0 RID: 1504 RVA: 0x000201F4 File Offset: 0x0001E3F4
		public bool CanShuffle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000201F8 File Offset: 0x0001E3F8
		protected PerGroupTransformBase(IHostEnvironment env, IDataView input, string labelCol, string scoreCol, string groupCol, string registrationName)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register(registrationName);
			Contracts.CheckValue<IDataView>(this._host, input, "input");
			Contracts.CheckNonWhiteSpace(this._host, labelCol, "labelCol");
			Contracts.CheckNonWhiteSpace(this._host, scoreCol, "scoreCol");
			Contracts.CheckNonWhiteSpace(this._host, groupCol, "groupCol");
			this._input = input;
			this._labelCol = labelCol;
			this._scoreCol = scoreCol;
			this._groupCol = groupCol;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0002028C File Offset: 0x0001E48C
		protected PerGroupTransformBase(ModelLoadContext ctx, IHostEnvironment env, IDataView input, string registrationName)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register(registrationName);
			Contracts.CheckValue<IDataView>(this._host, input, "input");
			this._input = input;
			this._labelCol = ctx.LoadNonEmptyString();
			this._scoreCol = ctx.LoadNonEmptyString();
			this._groupCol = ctx.LoadNonEmptyString();
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000202F4 File Offset: 0x0001E4F4
		public virtual void Save(ModelSaveContext ctx)
		{
			ctx.SaveNonEmptyString(this._labelCol);
			ctx.SaveNonEmptyString(this._scoreCol);
			ctx.SaveNonEmptyString(this._groupCol);
		}

		// Token: 0x060005E4 RID: 1508
		protected abstract PerGroupTransformBase<TLabel, TScore, TState>.BindingsBase GetBindings();

		// Token: 0x060005E5 RID: 1509 RVA: 0x0002031A File Offset: 0x0001E51A
		public long? GetRowCount(bool lazy = true)
		{
			return this._input.GetRowCount(lazy);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00020328 File Offset: 0x0001E528
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			consolidator = null;
			return new IRowCursor[] { this.GetRowCursor(predicate, rand) };
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00020370 File Offset: 0x0001E570
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			PerGroupTransformBase<TLabel, TScore, TState>.BindingsBase bindings = this.GetBindings();
			if (!bindings.AnyNewColumnsActive(predicate))
			{
				bool[] activeInput = bindings.GetActiveInput(predicate);
				IRowCursor rowCursor = this._input.GetRowCursor((int c) => activeInput[c], null);
				return new BindingsWrappedRowCursor(this._host, rowCursor, bindings);
			}
			return this.GetRowCursorCore(predicate);
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x000203E0 File Offset: 0x0001E5E0
		private IRowCursor GetRowCursorCore(Func<int, bool> predicate)
		{
			PerGroupTransformBase<TLabel, TScore, TState>.BindingsBase bindings = this.GetBindings();
			bool[] active = bindings.GetActive(predicate);
			Func<int, bool> dependencies = bindings.GetDependencies(predicate);
			return new PerGroupTransformBase<TLabel, TScore, TState>.RowCursor(this, this._input.GetRowCursor(dependencies, null), this._input.GetRowCursor(dependencies, null), active);
		}

		// Token: 0x060005E9 RID: 1513
		protected abstract Delegate[] CreateGetters(TState state, Func<int, bool> predicate);

		// Token: 0x060005EA RID: 1514
		protected abstract ValueGetter<TLabel> GetLabelGetter(IRow row);

		// Token: 0x060005EB RID: 1515
		protected abstract ValueGetter<TScore> GetScoreGetter(IRow row);

		// Token: 0x060005EC RID: 1516
		protected abstract TState InitializeState(IRow input);

		// Token: 0x060005ED RID: 1517
		protected abstract void ProcessExample(TState state, TLabel label, TScore score);

		// Token: 0x060005EE RID: 1518
		protected abstract void UpdateState(TState state);

		// Token: 0x040002EB RID: 747
		protected readonly IHost _host;

		// Token: 0x040002EC RID: 748
		protected readonly IDataView _input;

		// Token: 0x040002ED RID: 749
		protected readonly string _labelCol;

		// Token: 0x040002EE RID: 750
		protected readonly string _scoreCol;

		// Token: 0x040002EF RID: 751
		protected readonly string _groupCol;

		// Token: 0x02000122 RID: 290
		protected abstract class BindingsBase : ColumnBindingsBase
		{
			// Token: 0x060005EF RID: 1519 RVA: 0x00020428 File Offset: 0x0001E628
			protected BindingsBase(IExceptionContext ectx, ISchema input, string labelCol, string scoreCol, string groupCol, bool user, params string[] names)
				: base(input, user, names)
			{
				if (!input.TryGetColumnIndex(labelCol, ref this.LabelIndex))
				{
					throw user ? Contracts.ExceptParam(ectx, "labelCol", "Label column '{0}' does not exist", new object[] { labelCol }) : Contracts.ExceptDecode(ectx, "Label column '{0}' does not exist", new object[] { labelCol });
				}
				if (!input.TryGetColumnIndex(scoreCol, ref this.ScoreIndex))
				{
					throw user ? Contracts.ExceptParam(ectx, "scoreCol", "Score column '{0}' does not exist", new object[] { scoreCol }) : Contracts.ExceptDecode(ectx, "Score column '{0}' does not exist", new object[] { scoreCol });
				}
				if (!input.TryGetColumnIndex(groupCol, ref this.GroupIndex))
				{
					throw user ? Contracts.ExceptParam(ectx, "groupCol", "Group column '{0}' does not exist", new object[] { groupCol }) : Contracts.ExceptDecode("Group column '{0}' does not exist", new object[] { groupCol });
				}
			}

			// Token: 0x060005F0 RID: 1520 RVA: 0x0002054C File Offset: 0x0001E74C
			public Func<int, bool> GetDependencies(Func<int, bool> predicate)
			{
				bool[] active = new bool[this.Input.ColumnCount];
				for (int i = 0; i < base.ColumnCount; i++)
				{
					if (predicate(i))
					{
						bool flag;
						int num = base.MapColumnIndex(out flag, i);
						if (flag)
						{
							active[num] = true;
						}
						else
						{
							active[this.LabelIndex] = true;
							active[this.ScoreIndex] = true;
							active[this.GroupIndex] = true;
						}
					}
				}
				return (int col) => 0 <= col && col < active.Length && active[col];
			}

			// Token: 0x040002F0 RID: 752
			public readonly int LabelIndex;

			// Token: 0x040002F1 RID: 753
			public readonly int ScoreIndex;

			// Token: 0x040002F2 RID: 754
			public readonly int GroupIndex;
		}

		// Token: 0x02000123 RID: 291
		private sealed class RowCursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000205DD File Offset: 0x0001E7DD
			public ISchema Schema
			{
				get
				{
					return this._parent.GetBindings();
				}
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000205EA File Offset: 0x0001E7EA
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x060005F3 RID: 1523 RVA: 0x00020618 File Offset: 0x0001E818
			public RowCursor(PerGroupTransformBase<TLabel, TScore, TState> parent, IRowCursor input, IRowCursor groupCursor, bool[] active)
			{
				PerGroupTransformBase<TLabel, TScore, TState>.RowCursor.<>c__DisplayClass7 CS$<>8__locals1 = new PerGroupTransformBase<TLabel, TScore, TState>.RowCursor.<>c__DisplayClass7();
				CS$<>8__locals1.active = active;
				base..ctor(parent._host);
				this._parent = parent;
				this._input = input;
				this._groupCursor = groupCursor;
				this._active = CS$<>8__locals1.active;
				this._state = this._parent.InitializeState(this._input);
				PerGroupTransformBase<TLabel, TScore, TState>.BindingsBase bindings = this._parent.GetBindings();
				this._getters = this._parent.CreateGetters(this._state, (int iinfo) => CS$<>8__locals1.active[bindings.MapIinfoToCol(iinfo)]);
				this._newGroupInGroupCursorDel = RowCursorUtils.GetIsNewGroupDelegate(this._groupCursor, bindings.GroupIndex);
				this._newGroupInInputCursorDel = RowCursorUtils.GetIsNewGroupDelegate(this._input, bindings.GroupIndex);
				this._labelGetter = this._parent.GetLabelGetter(this._groupCursor);
				this._scoreGetter = this._parent.GetScoreGetter(this._groupCursor);
			}

			// Token: 0x060005F4 RID: 1524 RVA: 0x0002071E File Offset: 0x0001E91E
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._parent.GetBindings().ColumnCount);
				return this._active[col];
			}

			// Token: 0x060005F5 RID: 1525 RVA: 0x00020750 File Offset: 0x0001E950
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.CheckParam(this.IsColumnActive(col), "col", "requested column is not active");
				bool flag;
				col = this._parent.GetBindings().MapColumnIndex(out flag, col);
				if (flag)
				{
					return this._input.GetGetter<TValue>(col);
				}
				Delegate @delegate = this._getters[col];
				ValueGetter<TValue> valueGetter = @delegate as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue in GetGetter: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x060005F6 RID: 1526 RVA: 0x000207FD File Offset: 0x0001E9FD
			public override ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._ch, base.IsGood, "Cannot call ID getter in current state");
					val = new UInt128((ulong)base.Position, 0UL);
				};
			}

			// Token: 0x060005F7 RID: 1527 RVA: 0x0002080C File Offset: 0x0001EA0C
			protected override bool MoveNextCore()
			{
				if (!this._input.MoveNext())
				{
					return false;
				}
				if (!this._newGroupInInputCursorDel())
				{
					return true;
				}
				if (this._groupCursor.State == null && this._groupCursor.MoveNext())
				{
					this._newGroupInGroupCursorDel();
				}
				while (this._groupCursor.State != 2 && !this._newGroupInGroupCursorDel())
				{
					TLabel tlabel = default(TLabel);
					TScore tscore = default(TScore);
					this._labelGetter.Invoke(ref tlabel);
					this._scoreGetter.Invoke(ref tscore);
					this._parent.ProcessExample(this._state, tlabel, tscore);
					this._groupCursor.MoveNext();
				}
				this._parent.UpdateState(this._state);
				return true;
			}

			// Token: 0x040002F3 RID: 755
			private readonly PerGroupTransformBase<TLabel, TScore, TState> _parent;

			// Token: 0x040002F4 RID: 756
			private readonly IRowCursor _groupCursor;

			// Token: 0x040002F5 RID: 757
			private readonly IRowCursor _input;

			// Token: 0x040002F6 RID: 758
			private readonly bool[] _active;

			// Token: 0x040002F7 RID: 759
			private readonly Delegate[] _getters;

			// Token: 0x040002F8 RID: 760
			private readonly TState _state;

			// Token: 0x040002F9 RID: 761
			private readonly Func<bool> _newGroupInGroupCursorDel;

			// Token: 0x040002FA RID: 762
			private readonly Func<bool> _newGroupInInputCursorDel;

			// Token: 0x040002FB RID: 763
			private readonly ValueGetter<TLabel> _labelGetter;

			// Token: 0x040002FC RID: 764
			private readonly ValueGetter<TScore> _scoreGetter;
		}
	}
}
