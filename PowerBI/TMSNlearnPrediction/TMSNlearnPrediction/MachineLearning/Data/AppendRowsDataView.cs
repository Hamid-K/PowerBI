using System;
using System.Linq;
using System.Reflection;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000082 RID: 130
	public sealed class AppendRowsDataView : IDataView, ISchematized
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000DC9F File Offset: 0x0000BE9F
		public bool CanShuffle
		{
			get
			{
				return this._canShuffle;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000DCA7 File Offset: 0x0000BEA7
		public ISchema Schema
		{
			get
			{
				return this._schema;
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
		public static IDataView Create(IHostEnvironment env, ISchema schema, params IDataView[] sources)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<IDataView[]>(env, sources, "sources");
			Contracts.CheckNonEmpty<IDataView>(env, sources, "sources", "There must be at least one source.");
			Contracts.CheckParam(env, sources.All((IDataView s) => s != null), "sources");
			if (sources.Length == 1)
			{
				return sources[0];
			}
			return new AppendRowsDataView(env, schema, sources);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000DD30 File Offset: 0x0000BF30
		private AppendRowsDataView(IHostEnvironment env, ISchema schema, IDataView[] sources)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("AppendRowsDataView");
			this._sources = sources;
			this._schema = schema ?? this._sources[0].Schema;
			this.CheckSchemaConsistency();
			this._canShuffle = true;
			this._counts = new int[this._sources.Length];
			for (int i = 0; i < this._sources.Length; i++)
			{
				IDataView dataView = this._sources[i];
				if (!dataView.CanShuffle)
				{
					this._canShuffle = false;
					this._counts = null;
					return;
				}
				long? rowCount = dataView.GetRowCount(true);
				if (rowCount == null || rowCount < 0L || rowCount > 2147483647L)
				{
					this._canShuffle = false;
					this._counts = null;
					return;
				}
				this._counts[i] = (int)rowCount.Value;
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000DE5C File Offset: 0x0000C05C
		private void CheckSchemaConsistency()
		{
			int num = ((this._schema == this._sources[0].Schema) ? 1 : 0);
			int colCount = this._schema.ColumnCount;
			Contracts.Check(this._host, this._sources.All((IDataView source) => source.Schema.ColumnCount == colCount), "Inconsistent schema: all source dataviews must have identical column names, sizes, and item types.");
			for (int i = 0; i < colCount; i++)
			{
				string columnName = this._schema.GetColumnName(i);
				ColumnType columnType = this._schema.GetColumnType(i);
				for (int j = num; j < this._sources.Length; j++)
				{
					ISchema schema = this._sources[j].Schema;
					Contracts.Check(this._host, schema.GetColumnName(i) == columnName, "Inconsistent schema: all source dataviews must have identical column names, sizes, and item types.");
					Contracts.Check(this._host, schema.GetColumnType(i).SameSizeAndItemType(columnType), "Inconsistent schema: all source dataviews must have identical column names, sizes, and item types.");
				}
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000DF5C File Offset: 0x0000C15C
		public long? GetRowCount(bool lazy = true)
		{
			long num = 0L;
			IDataView[] sources = this._sources;
			int i = 0;
			while (i < sources.Length)
			{
				IDataView dataView = sources[i];
				long? rowCount = dataView.GetRowCount(lazy);
				long? num2;
				if (rowCount == null)
				{
					num2 = null;
				}
				else
				{
					Contracts.Check(this._host, rowCount.Value >= 0L, "One of the sources returned a negative row count");
					if (num + rowCount.Value >= num)
					{
						num += rowCount.Value;
						i++;
						continue;
					}
					num2 = null;
				}
				return num2;
			}
			return new long?(num);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000DFF4 File Offset: 0x0000C1F4
		public IRowCursor GetRowCursor(Func<int, bool> needCol, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, needCol, "needCol");
			if (rand == null || !this._canShuffle)
			{
				return new AppendRowsDataView.Cursor(this, needCol);
			}
			return new AppendRowsDataView.RandCursor(this, needCol, rand, this._counts);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000E028 File Offset: 0x0000C228
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			consolidator = null;
			return new IRowCursor[] { this.GetRowCursor(predicate, rand) };
		}

		// Token: 0x040000D5 RID: 213
		public const string RegistrationName = "AppendRowsDataView";

		// Token: 0x040000D6 RID: 214
		private readonly IDataView[] _sources;

		// Token: 0x040000D7 RID: 215
		private readonly int[] _counts;

		// Token: 0x040000D8 RID: 216
		private readonly ISchema _schema;

		// Token: 0x040000D9 RID: 217
		private readonly IHost _host;

		// Token: 0x040000DA RID: 218
		private readonly bool _canShuffle;

		// Token: 0x02000083 RID: 131
		private abstract class CursorBase : RootCursorBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted
		{
			// Token: 0x0600025F RID: 607 RVA: 0x0000E04C File Offset: 0x0000C24C
			public CursorBase(AppendRowsDataView parent)
				: base(parent._host)
			{
				this._sources = parent._sources;
				this._schema = parent._schema;
				this._getters = new Delegate[this._schema.ColumnCount];
			}

			// Token: 0x06000260 RID: 608 RVA: 0x0000E088 File Offset: 0x0000C288
			protected Delegate CreateGetter(int col)
			{
				ColumnType columnType = this._schema.GetColumnType(col);
				Func<int, Delegate> func = new Func<int, Delegate>(this.CreateTypedGetter<int>);
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType.RawType });
				return (Delegate)methodInfo.Invoke(this, new object[] { col });
			}

			// Token: 0x06000261 RID: 609
			protected abstract ValueGetter<TValue> CreateTypedGetter<TValue>(int col);

			// Token: 0x06000262 RID: 610 RVA: 0x0000E0F2 File Offset: 0x0000C2F2
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				Contracts.Check(this._ch, this.IsColumnActive(col), "The column must be active against the defined predicate.");
				return this._getters[col] as ValueGetter<TValue>;
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x06000263 RID: 611 RVA: 0x0000E118 File Offset: 0x0000C318
			public override long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x06000264 RID: 612 RVA: 0x0000E11C File Offset: 0x0000C31C
			public ISchema Schema
			{
				get
				{
					return this._schema;
				}
			}

			// Token: 0x06000265 RID: 613 RVA: 0x0000E124 File Offset: 0x0000C324
			public bool IsColumnActive(int col)
			{
				Contracts.Check(this._ch, 0 <= col && col < this._schema.ColumnCount, "Column index is out of range");
				return this._getters[col] != null;
			}

			// Token: 0x040000DC RID: 220
			protected readonly IDataView[] _sources;

			// Token: 0x040000DD RID: 221
			protected readonly ISchema _schema;

			// Token: 0x040000DE RID: 222
			protected readonly Delegate[] _getters;
		}

		// Token: 0x02000084 RID: 132
		private sealed class Cursor : AppendRowsDataView.CursorBase
		{
			// Token: 0x06000266 RID: 614 RVA: 0x0000E15C File Offset: 0x0000C35C
			public Cursor(AppendRowsDataView parent, Func<int, bool> needCol)
				: base(parent)
			{
				this._currentSourceIndex = 0;
				this._currentCursor = this._sources[this._currentSourceIndex].GetRowCursor(needCol, null);
				this._currentIdGetter = this._currentCursor.GetIdGetter();
				for (int i = 0; i < this._getters.Length; i++)
				{
					if (needCol(i))
					{
						this._getters[i] = base.CreateGetter(i);
					}
				}
			}

			// Token: 0x06000267 RID: 615 RVA: 0x0000E1F5 File Offset: 0x0000C3F5
			public override ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					this._currentIdGetter.Invoke(ref val);
					val = val.Combine(new UInt128((ulong)((long)this._currentSourceIndex), 0UL));
				};
			}

			// Token: 0x06000268 RID: 616 RVA: 0x0000E288 File Offset: 0x0000C488
			protected override ValueGetter<TValue> CreateTypedGetter<TValue>(int col)
			{
				ValueGetter<TValue> getSrc = null;
				int capturedSourceIndex = -1;
				return delegate(ref TValue val)
				{
					Contracts.Check(this._ch, this.State == 1, "A getter can only be used when the cursor state is Good.");
					if (this._currentSourceIndex != capturedSourceIndex)
					{
						getSrc = this._currentCursor.GetGetter<TValue>(col);
						capturedSourceIndex = this._currentSourceIndex;
					}
					getSrc.Invoke(ref val);
				};
			}

			// Token: 0x06000269 RID: 617 RVA: 0x0000E2CC File Offset: 0x0000C4CC
			protected override bool MoveNextCore()
			{
				while (!this._currentCursor.MoveNext())
				{
					this._currentCursor.Dispose();
					this._currentCursor = null;
					if (++this._currentSourceIndex >= this._sources.Length)
					{
						return false;
					}
					this._currentCursor = this._sources[this._currentSourceIndex].GetRowCursor((int c) => base.IsColumnActive(c), null);
					this._currentIdGetter = this._currentCursor.GetIdGetter();
				}
				return true;
			}

			// Token: 0x0600026A RID: 618 RVA: 0x0000E355 File Offset: 0x0000C555
			public override void Dispose()
			{
				if (base.State != 2)
				{
					this._ch.Done();
					this._ch.Dispose();
					if (this._currentCursor != null)
					{
						this._currentCursor.Dispose();
					}
					base.Dispose();
				}
			}

			// Token: 0x040000DF RID: 223
			private IRowCursor _currentCursor;

			// Token: 0x040000E0 RID: 224
			private ValueGetter<UInt128> _currentIdGetter;

			// Token: 0x040000E1 RID: 225
			private int _currentSourceIndex;
		}

		// Token: 0x02000085 RID: 133
		private sealed class RandCursor : AppendRowsDataView.CursorBase
		{
			// Token: 0x0600026D RID: 621 RVA: 0x0000E390 File Offset: 0x0000C590
			public RandCursor(AppendRowsDataView parent, Func<int, bool> needCol, IRandom rand, int[] counts)
				: base(parent)
			{
				this._rand = rand;
				this._cursorSet = new IRowCursor[counts.Length];
				for (int i = 0; i < counts.Length; i++)
				{
					this._cursorSet[i] = parent._sources[i].GetRowCursor(needCol, RandomUtils.Create(this._rand));
				}
				this._sampler = new AppendRowsDataView.MultinomialWithoutReplacementSampler(this._ch, counts, rand);
				this._currentSourceIndex = -1;
				for (int j = 0; j < this._getters.Length; j++)
				{
					if (needCol(j))
					{
						this._getters[j] = base.CreateGetter(j);
					}
				}
			}

			// Token: 0x0600026E RID: 622 RVA: 0x0000E49C File Offset: 0x0000C69C
			public override ValueGetter<UInt128> GetIdGetter()
			{
				ValueGetter<UInt128>[] idGetters = new ValueGetter<UInt128>[this._cursorSet.Length];
				for (int i = 0; i < this._cursorSet.Length; i++)
				{
					idGetters[i] = this._cursorSet[i].GetIdGetter();
				}
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._ch, this.IsGood, "Cannot call ID getter in current state");
					idGetters[this._currentSourceIndex].Invoke(ref val);
					val = val.Combine(new UInt128((ulong)((long)this._currentSourceIndex), 0UL));
				};
			}

			// Token: 0x0600026F RID: 623 RVA: 0x0000E598 File Offset: 0x0000C798
			protected override ValueGetter<TValue> CreateTypedGetter<TValue>(int col)
			{
				ValueGetter<TValue>[] getSrc = new ValueGetter<TValue>[this._cursorSet.Length];
				return delegate(ref TValue val)
				{
					Contracts.Check(this._ch, this.State == 1, "A getter can only be used when the cursor state is Good.");
					if (getSrc[this._currentSourceIndex] == null)
					{
						getSrc[this._currentSourceIndex] = this._cursorSet[this._currentSourceIndex].GetGetter<TValue>(col);
					}
					getSrc[this._currentSourceIndex].Invoke(ref val);
				};
			}

			// Token: 0x06000270 RID: 624 RVA: 0x0000E5D8 File Offset: 0x0000C7D8
			protected override bool MoveNextCore()
			{
				int num;
				if ((num = this._sampler.Next()) < 0)
				{
					return false;
				}
				this._currentSourceIndex = num;
				this._cursorSet[this._currentSourceIndex].MoveNext();
				return true;
			}

			// Token: 0x06000271 RID: 625 RVA: 0x0000E614 File Offset: 0x0000C814
			public override void Dispose()
			{
				if (base.State != 2)
				{
					this._ch.Done();
					this._ch.Dispose();
					foreach (IRowCursor rowCursor in this._cursorSet)
					{
						rowCursor.Dispose();
					}
					base.Dispose();
				}
			}

			// Token: 0x040000E2 RID: 226
			private readonly IRowCursor[] _cursorSet;

			// Token: 0x040000E3 RID: 227
			private readonly AppendRowsDataView.MultinomialWithoutReplacementSampler _sampler;

			// Token: 0x040000E4 RID: 228
			private readonly IRandom _rand;

			// Token: 0x040000E5 RID: 229
			private int _currentSourceIndex;
		}

		// Token: 0x02000086 RID: 134
		private sealed class MultinomialWithoutReplacementSampler
		{
			// Token: 0x06000272 RID: 626 RVA: 0x0000E668 File Offset: 0x0000C868
			public MultinomialWithoutReplacementSampler(IExceptionContext context, int[] counts, IRandom rand)
			{
				this._ectx = context;
				this._rowsLeft = (int[])counts.Clone();
				this._rand = rand;
				foreach (int num in this._rowsLeft)
				{
					this._totalLeft += num;
				}
				this._batch = new int[1000];
			}

			// Token: 0x06000273 RID: 627 RVA: 0x0000E6D4 File Offset: 0x0000C8D4
			private void GenerateNextBatch()
			{
				this._batchEnd = 0;
				int num = 0;
				while (num < this._rowsLeft.Length && this._batchEnd < 1000)
				{
					int num2;
					if (this._totalLeft <= 1000)
					{
						num2 = this._batchEnd + this._rowsLeft[num];
					}
					else
					{
						num2 = this._batchEnd + (int)Math.Ceiling((double)this._rowsLeft[num] * 1000.0 / (double)this._totalLeft);
						if (num2 > 1000)
						{
							num2 = 1000;
						}
					}
					for (int i = this._batchEnd; i < num2; i++)
					{
						this._batch[i] = num;
					}
					this._rowsLeft[num] -= num2 - this._batchEnd;
					this._batchEnd = num2;
					num++;
				}
				this._totalLeft -= this._batchEnd;
				Utils.Shuffle<int>(this._rand, this._batch, 0, this._batchEnd);
			}

			// Token: 0x06000274 RID: 628 RVA: 0x0000E7D0 File Offset: 0x0000C9D0
			public int Next()
			{
				if (this._batchPos < this._batchEnd)
				{
					return this._batch[this._batchPos++];
				}
				if (this._totalLeft > 0)
				{
					this.GenerateNextBatch();
					this._batchPos = 0;
					return this._batch[this._batchPos++];
				}
				return -1;
			}

			// Token: 0x040000E6 RID: 230
			private const int BatchSize = 1000;

			// Token: 0x040000E7 RID: 231
			private readonly int[] _rowsLeft;

			// Token: 0x040000E8 RID: 232
			private readonly IRandom _rand;

			// Token: 0x040000E9 RID: 233
			private readonly int[] _batch;

			// Token: 0x040000EA RID: 234
			private readonly IExceptionContext _ectx;

			// Token: 0x040000EB RID: 235
			private int _batchEnd;

			// Token: 0x040000EC RID: 236
			private int _batchPos;

			// Token: 0x040000ED RID: 237
			private int _totalLeft;
		}
	}
}
