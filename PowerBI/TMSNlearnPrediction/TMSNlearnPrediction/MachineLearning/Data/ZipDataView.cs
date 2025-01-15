using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000407 RID: 1031
	public sealed class ZipDataView : IDataView, ISchematized
	{
		// Token: 0x06001582 RID: 5506 RVA: 0x0007DBC0 File Offset: 0x0007BDC0
		public static IDataView Create(IHostEnvironment env, IEnumerable<IDataView> sources)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("ZipDataView");
			Contracts.CheckValue<IEnumerable<IDataView>>(host, sources, "sources");
			IDataView[] array = sources.ToArray<IDataView>();
			Contracts.CheckParam(host, array.Length > 0, "sources", "Sources can not be empty");
			if (array.Length == 1)
			{
				return array[0];
			}
			return new ZipDataView(host, array);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0007DC28 File Offset: 0x0007BE28
		private ZipDataView(IHost host, IDataView[] sources)
		{
			this._host = host;
			this._sources = sources;
			this._schema = new ZipDataView.ZipSchema(this._sources.Select((IDataView x) => x.Schema).ToArray<ISchema>());
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x0007DC81 File Offset: 0x0007BE81
		public bool CanShuffle
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x0007DC84 File Offset: 0x0007BE84
		public ISchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x0007DC8C File Offset: 0x0007BE8C
		public long? GetRowCount(bool lazy = true)
		{
			long num = -1L;
			foreach (IDataView dataView in this._sources)
			{
				long? rowCount = dataView.GetRowCount(lazy);
				if (rowCount == null)
				{
					return null;
				}
				Contracts.Check(this._host, rowCount.Value >= 0L, "One of the sources returned a negative row count");
				if (num < 0L || num > rowCount.Value)
				{
					num = rowCount.Value;
				}
			}
			return new long?(num);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x0007DD48 File Offset: 0x0007BF48
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			Func<int, bool>[] srcPredicates = this._schema.GetInputPredicates(predicate);
			IRowCursor[] array = this._sources.Select(delegate(IDataView dv, int i)
			{
				if (srcPredicates[i] != null)
				{
					return dv.GetRowCursor(srcPredicates[i], null);
				}
				return this.GetMinimumCursor(dv);
			}).ToArray<IRowCursor>();
			return new ZipDataView.Cursor(this, array, predicate);
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x0007DDAD File Offset: 0x0007BFAD
		private IRowCursor GetMinimumCursor(IDataView dv)
		{
			return dv.GetRowCursor((int x) => false, null);
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x0007DDD4 File Offset: 0x0007BFD4
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			consolidator = null;
			return new IRowCursor[] { this.GetRowCursor(predicate, rand) };
		}

		// Token: 0x04000D36 RID: 3382
		public const string RegistrationName = "ZipDataView";

		// Token: 0x04000D37 RID: 3383
		private readonly IHost _host;

		// Token: 0x04000D38 RID: 3384
		private readonly IDataView[] _sources;

		// Token: 0x04000D39 RID: 3385
		private readonly ZipDataView.ZipSchema _schema;

		// Token: 0x02000408 RID: 1032
		private sealed class ZipSchema : ISchema
		{
			// Token: 0x0600158C RID: 5516 RVA: 0x0007DDF8 File Offset: 0x0007BFF8
			public ZipSchema(ISchema[] sources)
			{
				this._sources = sources;
				this._cumulativeColCounts = new int[this._sources.Length + 1];
				this._cumulativeColCounts[0] = 0;
				for (int i = 0; i < sources.Length; i++)
				{
					ISchema schema = sources[i];
					this._cumulativeColCounts[i + 1] = this._cumulativeColCounts[i] + schema.ColumnCount;
				}
			}

			// Token: 0x0600158D RID: 5517 RVA: 0x0007DE88 File Offset: 0x0007C088
			public Func<int, bool>[] GetInputPredicates(Func<int, bool> predicate)
			{
				Func<int, bool>[] array = new Func<int, bool>[this._sources.Length];
				for (int i = 0; i < this._sources.Length; i++)
				{
					int lastColCount = this._cumulativeColCounts[i];
					array[i] = (int srcCol) => predicate(srcCol + lastColCount);
				}
				return array;
			}

			// Token: 0x0600158E RID: 5518 RVA: 0x0007DEEE File Offset: 0x0007C0EE
			public void CheckColumnInRange(int col)
			{
				Contracts.CheckParam(0 <= col && col < this._cumulativeColCounts[this._cumulativeColCounts.Length - 1], "col", "Column index out of range");
			}

			// Token: 0x0600158F RID: 5519 RVA: 0x0007DF1A File Offset: 0x0007C11A
			public void GetColumnSource(int col, out int srcIndex, out int srcCol)
			{
				this.CheckColumnInRange(col);
				if (!Utils.TryFindIndexSorted(this._cumulativeColCounts, 0, this._cumulativeColCounts.Length, col, ref srcIndex))
				{
					srcIndex--;
				}
				srcCol = col - this._cumulativeColCounts[srcIndex];
			}

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x06001590 RID: 5520 RVA: 0x0007DF4E File Offset: 0x0007C14E
			public int ColumnCount
			{
				get
				{
					return this._cumulativeColCounts[this._cumulativeColCounts.Length - 1];
				}
			}

			// Token: 0x06001591 RID: 5521 RVA: 0x0007DF64 File Offset: 0x0007C164
			public bool TryGetColumnIndex(string name, out int col)
			{
				int num = this._sources.Length;
				while (--num >= 0)
				{
					if (this._sources[num].TryGetColumnIndex(name, ref col))
					{
						col += this._cumulativeColCounts[num];
						return true;
					}
				}
				col = -1;
				return false;
			}

			// Token: 0x06001592 RID: 5522 RVA: 0x0007DFA8 File Offset: 0x0007C1A8
			public string GetColumnName(int col)
			{
				int num;
				int num2;
				this.GetColumnSource(col, out num, out num2);
				return this._sources[num].GetColumnName(num2);
			}

			// Token: 0x06001593 RID: 5523 RVA: 0x0007DFD0 File Offset: 0x0007C1D0
			public ColumnType GetColumnType(int col)
			{
				int num;
				int num2;
				this.GetColumnSource(col, out num, out num2);
				return this._sources[num].GetColumnType(num2);
			}

			// Token: 0x06001594 RID: 5524 RVA: 0x0007DFF8 File Offset: 0x0007C1F8
			public IEnumerable<KeyValuePair<string, ColumnType>> GetMetadataTypes(int col)
			{
				int num;
				int num2;
				this.GetColumnSource(col, out num, out num2);
				return this._sources[num].GetMetadataTypes(num2);
			}

			// Token: 0x06001595 RID: 5525 RVA: 0x0007E020 File Offset: 0x0007C220
			public ColumnType GetMetadataTypeOrNull(string kind, int col)
			{
				int num;
				int num2;
				this.GetColumnSource(col, out num, out num2);
				return this._sources[num].GetMetadataTypeOrNull(kind, num2);
			}

			// Token: 0x06001596 RID: 5526 RVA: 0x0007E048 File Offset: 0x0007C248
			public void GetMetadata<TValue>(string kind, int col, ref TValue value)
			{
				int num;
				int num2;
				this.GetColumnSource(col, out num, out num2);
				this._sources[num].GetMetadata<TValue>(kind, num2, ref value);
			}

			// Token: 0x04000D3C RID: 3388
			private readonly ISchema[] _sources;

			// Token: 0x04000D3D RID: 3389
			private readonly int[] _cumulativeColCounts;
		}

		// Token: 0x02000409 RID: 1033
		private sealed class Cursor : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06001597 RID: 5527 RVA: 0x0007E070 File Offset: 0x0007C270
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x06001598 RID: 5528 RVA: 0x0007E074 File Offset: 0x0007C274
			public Cursor(ZipDataView parent, IRowCursor[] srcCursors, Func<int, bool> predicate)
				: base(parent._host)
			{
				this._cursors = srcCursors;
				this._schema = parent._schema;
				this._isColumnActive = Utils.BuildArray<bool>(this._schema.ColumnCount, predicate);
			}

			// Token: 0x06001599 RID: 5529 RVA: 0x0007E0AC File Offset: 0x0007C2AC
			public override void Dispose()
			{
				for (int i = this._cursors.Length - 1; i >= 0; i--)
				{
					this._cursors[i].Dispose();
				}
				base.Dispose();
			}

			// Token: 0x0600159A RID: 5530 RVA: 0x0007E10C File Offset: 0x0007C30C
			public override ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._ch, base.IsGood, "Cannot call ID getter in current state");
					val = new UInt128((ulong)base.Position, 0UL);
				};
			}

			// Token: 0x0600159B RID: 5531 RVA: 0x0007E11C File Offset: 0x0007C31C
			protected override bool MoveNextCore()
			{
				foreach (IRowCursor rowCursor in this._cursors)
				{
					if (!rowCursor.MoveNext())
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600159C RID: 5532 RVA: 0x0007E154 File Offset: 0x0007C354
			protected override bool MoveManyCore(long count)
			{
				foreach (IRowCursor rowCursor in this._cursors)
				{
					if (!rowCursor.MoveMany(count))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x0600159D RID: 5533 RVA: 0x0007E18A File Offset: 0x0007C38A
			public ISchema Schema
			{
				get
				{
					return this._schema;
				}
			}

			// Token: 0x0600159E RID: 5534 RVA: 0x0007E192 File Offset: 0x0007C392
			public bool IsColumnActive(int col)
			{
				this._schema.CheckColumnInRange(col);
				return this._isColumnActive[col];
			}

			// Token: 0x0600159F RID: 5535 RVA: 0x0007E1A8 File Offset: 0x0007C3A8
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				int num;
				int num2;
				this._schema.GetColumnSource(col, out num, out num2);
				return this._cursors[num].GetGetter<TValue>(num2);
			}

			// Token: 0x04000D3E RID: 3390
			private readonly IRowCursor[] _cursors;

			// Token: 0x04000D3F RID: 3391
			private readonly ZipDataView.ZipSchema _schema;

			// Token: 0x04000D40 RID: 3392
			private readonly bool[] _isColumnActive;
		}
	}
}
