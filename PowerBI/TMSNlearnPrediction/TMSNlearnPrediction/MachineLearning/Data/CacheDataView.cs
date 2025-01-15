using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001C1 RID: 449
	public sealed class CacheDataView : IDataView, IRowSeekable, ISchematized
	{
		// Token: 0x06000A08 RID: 2568 RVA: 0x00035BB0 File Offset: 0x00033DB0
		public CacheDataView(IHostEnvironment env, IDataView input, int[] prefetch)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			this._host = env.Register("Cache");
			Contracts.CheckValue<IDataView>(this._host, input, "input");
			this._subsetInput = CacheDataView.SelectCachableColumns(input, this._host, ref prefetch, out this._inputToSubsetColIndex);
			this._rowCount = this._subsetInput.GetRowCount(true) ?? (-1L);
			if (this._rowCount > 2146435071L)
			{
				throw Contracts.Except(this._host, "The input data view has too many ({0}) rows. CacheDataView can only cache up to {1} rows", new object[] { this._rowCount, 2146435071 });
			}
			this._cacheLock = new object();
			this._cacheFillerThreads = new ConcurrentBag<Thread>();
			this._caches = new CacheDataView.ColumnCache[this._subsetInput.Schema.ColumnCount];
			if (Utils.Size<int>(prefetch) > 0)
			{
				this.KickoffFiller(prefetch);
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00035CB4 File Offset: 0x00033EB4
		private static IDataView SelectCachableColumns(IDataView data, IHostEnvironment env, ref int[] prefetch, out int[] inputToSubset)
		{
			List<int> list = null;
			ISchema schema = data.Schema;
			if (prefetch == null)
			{
				prefetch = new int[0];
			}
			else if (prefetch.Length > 0)
			{
				int[] array = new int[prefetch.Length];
				Array.Copy(prefetch, array, prefetch.Length);
				Array.Sort<int>(array);
				prefetch = array;
				if (prefetch.Length > 0 && (prefetch[0] < 0 || prefetch[prefetch.Length - 1] >= schema.ColumnCount))
				{
					throw Contracts.Except(env, "Prefetch array had column indices out of range");
				}
			}
			int num = 0;
			inputToSubset = null;
			for (int i = 0; i < schema.ColumnCount; i++)
			{
				ColumnType columnType = schema.GetColumnType(i);
				if (!columnType.IsCachable())
				{
					if (inputToSubset == null)
					{
						inputToSubset = new int[schema.ColumnCount];
						for (int j = 0; j < i; j++)
						{
							inputToSubset[j] = j;
						}
					}
					inputToSubset[i] = -1;
					Utils.Add<int>(ref list, i);
					if (num < prefetch.Length && prefetch[num] == i)
					{
						throw Contracts.Except(env, "Asked to prefetch column '{0}' into cache, but it is of unhandled type '{1}'", new object[]
						{
							schema.GetColumnName(i),
							columnType
						});
					}
				}
				else
				{
					while (num < prefetch.Length && prefetch[num] == i)
					{
						prefetch[num++] -= Utils.Size<int>(list);
					}
					if (inputToSubset != null)
					{
						inputToSubset[i] = i - Utils.Size<int>(list);
					}
				}
			}
			if (Utils.Size<int>(list) == 0)
			{
				return data;
			}
			return new ChooseColumnsByIndexTransform(new ChooseColumnsByIndexTransform.Arguments
			{
				drop = true,
				index = list.ToArray()
			}, env, data);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x00035E3C File Offset: 0x0003403C
		public int MapInputToCacheColumnIndex(int inputIndex)
		{
			int num = ((this._inputToSubsetColIndex == null) ? this._subsetInput.Schema.ColumnCount : this._inputToSubsetColIndex.Length);
			Contracts.CheckParam(this._host, 0 <= inputIndex && inputIndex < num, "inputIndex", "Input column index not in range");
			return (this._inputToSubsetColIndex == null) ? inputIndex : this._inputToSubsetColIndex[inputIndex];
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x00035EA1 File Offset: 0x000340A1
		public bool CanShuffle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x00035EA4 File Offset: 0x000340A4
		public ISchema Schema
		{
			get
			{
				return this._subsetInput.Schema;
			}
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x00035EB4 File Offset: 0x000340B4
		public long? GetRowCount(bool lazy = true)
		{
			if (this._rowCount < 0L)
			{
				if (lazy)
				{
					return null;
				}
				if (this._cacheDefaultWaiter == null)
				{
					this.KickoffFiller(new int[0]);
				}
				this._cacheDefaultWaiter.Wait(long.MaxValue, default(CancellationToken));
			}
			return new long?(this._rowCount);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00035F18 File Offset: 0x00034118
		public IRowCursor GetRowCursor(Func<int, bool> predicate, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			CacheDataView.WaiterWaiter.Wrapper wrapper = CacheDataView.WaiterWaiter.Create(this, predicate);
			if (wrapper.IsTrivial)
			{
				return this.GetRowCursorWaiterCore<CacheDataView.TrivialWaiter.Wrapper>(CacheDataView.TrivialWaiter.Create(this), predicate, rand);
			}
			return this.GetRowCursorWaiterCore<CacheDataView.WaiterWaiter.Wrapper>(wrapper, predicate, rand);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x00035F60 File Offset: 0x00034160
		private int[] GetPermutationOrNull(IRandom rand)
		{
			if (rand == null)
			{
				return null;
			}
			if (this._rowCount < 0L)
			{
				this._cacheDefaultWaiter.Wait(long.MaxValue, default(CancellationToken));
			}
			long rowCount = this._rowCount;
			if (rowCount > 2146435071L)
			{
				return null;
			}
			return Utils.GetRandomPermutation(rand, (int)this._rowCount);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x00035FBC File Offset: 0x000341BC
		private IRowCursor GetRowCursorWaiterCore<TWaiter>(TWaiter waiter, Func<int, bool> predicate, IRandom rand) where TWaiter : struct, CacheDataView.IWaiter
		{
			int[] permutationOrNull = this.GetPermutationOrNull(rand);
			if (permutationOrNull == null)
			{
				return this.CreateCursor<CacheDataView.SequenceIndex<TWaiter>.Wrapper>(predicate, CacheDataView.SequenceIndex<TWaiter>.Create(waiter));
			}
			return this.CreateCursor<CacheDataView.RandomIndex<TWaiter>.Wrapper>(predicate, CacheDataView.RandomIndex<TWaiter>.Create(waiter, permutationOrNull));
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00035FF0 File Offset: 0x000341F0
		public IRowCursor[] GetRowCursorSet(out IRowCursorConsolidator consolidator, Func<int, bool> predicate, int n, IRandom rand = null)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			n = DataViewUtils.GetThreadCount(this._host, n, false);
			if (n <= 1)
			{
				consolidator = null;
				return new IRowCursor[] { this.GetRowCursor(predicate, rand) };
			}
			consolidator = new CacheDataView.Consolidator();
			CacheDataView.WaiterWaiter.Wrapper wrapper = CacheDataView.WaiterWaiter.Create(this, predicate);
			if (wrapper.IsTrivial)
			{
				return this.GetRowCursorSetWaiterCore<CacheDataView.TrivialWaiter.Wrapper>(CacheDataView.TrivialWaiter.Create(this), predicate, n, rand);
			}
			return this.GetRowCursorSetWaiterCore<CacheDataView.WaiterWaiter.Wrapper>(wrapper, predicate, n, rand);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x00036070 File Offset: 0x00034270
		private IRowCursor[] GetRowCursorSetWaiterCore<TWaiter>(TWaiter waiter, Func<int, bool> predicate, int n, IRandom rand) where TWaiter : struct, CacheDataView.IWaiter
		{
			CacheDataView.JobScheduler jobScheduler = new CacheDataView.JobScheduler(n);
			IRowCursor[] array = new IRowCursor[n];
			int[] permutationOrNull = this.GetPermutationOrNull(rand);
			for (int i = 0; i < n; i++)
			{
				if (permutationOrNull == null)
				{
					array[i] = this.CreateCursor<CacheDataView.BlockSequenceIndex<TWaiter>.Wrapper>(predicate, CacheDataView.BlockSequenceIndex<TWaiter>.Create(waiter, jobScheduler));
				}
				else
				{
					array[i] = this.CreateCursor<CacheDataView.BlockRandomIndex<TWaiter>.Wrapper>(predicate, CacheDataView.BlockRandomIndex<TWaiter>.Create(waiter, jobScheduler, permutationOrNull));
				}
			}
			return array;
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x000360C9 File Offset: 0x000342C9
		private IRowCursor CreateCursor<TIndex>(Func<int, bool> predicate, TIndex index) where TIndex : struct, CacheDataView.IIndex
		{
			return new CacheDataView.RowCursor<TIndex>(this, predicate, index);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000360D4 File Offset: 0x000342D4
		public IRowSeeker GetSeeker(Func<int, bool> predicate)
		{
			Contracts.CheckValue<Func<int, bool>>(this._host, predicate, "predicate");
			this.GetRowCount(false);
			CacheDataView.WaiterWaiter.Wrapper wrapper = CacheDataView.WaiterWaiter.Create(this, predicate);
			if (wrapper.IsTrivial)
			{
				return this.GetSeeker<CacheDataView.TrivialWaiter.Wrapper>(predicate, CacheDataView.TrivialWaiter.Create(this));
			}
			return this.GetSeeker<CacheDataView.WaiterWaiter.Wrapper>(predicate, wrapper);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x00036121 File Offset: 0x00034321
		private IRowSeeker GetSeeker<TWaiter>(Func<int, bool> predicate, TWaiter waiter) where TWaiter : struct, CacheDataView.IWaiter
		{
			return new CacheDataView.RowSeeker<TWaiter>(this, predicate, waiter);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x00036158 File Offset: 0x00034358
		private void KickoffFiller(int[] columns)
		{
			HashSet<int> hashSet = null;
			IRowCursor cursor;
			CacheDataView.ColumnCache[] caches;
			OrderedWaiter waiter;
			lock (this._cacheLock)
			{
				foreach (int num in columns)
				{
					if (this._caches[num] == null)
					{
						Utils.Add<int>(ref hashSet, num);
					}
				}
				if (Utils.Size<int>(hashSet) == 0 && this._cacheDefaultWaiter != null)
				{
					return;
				}
				if (hashSet == null)
				{
					cursor = this._subsetInput.GetRowCursor((int c) => false, null);
				}
				else
				{
					cursor = this._subsetInput.GetRowCursor(new Func<int, bool>(hashSet.Contains), null);
				}
				waiter = new OrderedWaiter(false);
				this._cacheDefaultWaiter = waiter;
				caches = new CacheDataView.ColumnCache[Utils.Size<int>(hashSet)];
				if (caches.Length > 0)
				{
					int num2 = 0;
					foreach (int num3 in hashSet)
					{
						caches[num2++] = (this._caches[num3] = CacheDataView.ColumnCache.Create(this, cursor, num3, waiter));
					}
				}
			}
			Thread thread = Utils.CreateBackgroundThread(delegate
			{
				this.Filler(cursor, caches, waiter);
			});
			this._cacheFillerThreads.Add(thread);
			thread.Start();
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0003631C File Offset: 0x0003451C
		private void Filler(IRowCursor cursor, CacheDataView.ColumnCache[] caches, OrderedWaiter waiter)
		{
			try
			{
				try
				{
					using (IChannel channel = this._host.Start("Cache Filler"))
					{
						channel.Trace("Begin cache of {0} columns", new object[] { caches.Length });
						long num = 0L;
						if (caches.Length > 0)
						{
							while (cursor.MoveNext())
							{
								num += 1L;
								if (num > 2146435071L)
								{
									throw Contracts.Except(this._host, "The input data view has too many ({0}) rows. CacheDataView can only cache up to {1} rows", new object[] { num, 2146435071 });
								}
								for (int i = 0; i < caches.Length; i++)
								{
									caches[i].CacheCurrent();
								}
								waiter.Increment();
							}
						}
						else
						{
							while (cursor.MoveNext())
							{
								num += 1L;
								if (this._rowCount > 2146435071L)
								{
									throw Contracts.Except(this._host, "The input data view has too many ({0}) rows. CacheDataView can only cache up to {1} rows", new object[] { this._rowCount, 2146435071 });
								}
								waiter.Increment();
							}
						}
						long num2 = Interlocked.CompareExchange(ref this._rowCount, num, -1L);
						for (int j = 0; j < caches.Length; j++)
						{
							caches[j].Freeze();
						}
						if (num2 == -1L)
						{
							channel.Trace("Number of rows determined as {0}", new object[] { num });
						}
						waiter.IncrementAll();
						channel.Trace("End cache of {0} columns", new object[] { caches.Length });
						channel.Done();
					}
				}
				finally
				{
					if (cursor != null)
					{
						cursor.Dispose();
					}
				}
			}
			catch (Exception ex)
			{
				waiter.SignalException(ex);
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00036520 File Offset: 0x00034720
		internal void Wait()
		{
			if (this._cacheFillerThreads != null)
			{
				foreach (Thread thread in this._cacheFillerThreads)
				{
					if (thread.IsAlive)
					{
						thread.Join();
					}
				}
			}
		}

		// Token: 0x04000526 RID: 1318
		private const int _batchShift = 6;

		// Token: 0x04000527 RID: 1319
		private const int _batchSize = 64;

		// Token: 0x04000528 RID: 1320
		private const int _batchMask = 63;

		// Token: 0x04000529 RID: 1321
		private readonly IHost _host;

		// Token: 0x0400052A RID: 1322
		private readonly IDataView _subsetInput;

		// Token: 0x0400052B RID: 1323
		private long _rowCount;

		// Token: 0x0400052C RID: 1324
		private readonly int[] _inputToSubsetColIndex;

		// Token: 0x0400052D RID: 1325
		private readonly object _cacheLock;

		// Token: 0x0400052E RID: 1326
		private readonly ConcurrentBag<Thread> _cacheFillerThreads;

		// Token: 0x0400052F RID: 1327
		private readonly CacheDataView.ColumnCache[] _caches;

		// Token: 0x04000530 RID: 1328
		private volatile OrderedWaiter _cacheDefaultWaiter;

		// Token: 0x020001C2 RID: 450
		private sealed class Consolidator : IRowCursorConsolidator
		{
			// Token: 0x06000A1A RID: 2586 RVA: 0x0003657C File Offset: 0x0003477C
			public IRowCursor CreateCursor(IChannelProvider provider, IRowCursor[] inputs)
			{
				return DataViewUtils.ConsolidateGeneric(provider, inputs, 64);
			}
		}

		// Token: 0x020001C3 RID: 451
		private abstract class RowCursorSeekerBase : IDisposable
		{
			// Token: 0x17000120 RID: 288
			// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0003658F File Offset: 0x0003478F
			public ISchema Schema
			{
				get
				{
					return this._parent.Schema;
				}
			}

			// Token: 0x17000121 RID: 289
			// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0003659C File Offset: 0x0003479C
			public long Position
			{
				get
				{
					return this._position;
				}
			}

			// Token: 0x06000A1E RID: 2590 RVA: 0x000365A4 File Offset: 0x000347A4
			protected RowCursorSeekerBase(CacheDataView parent, Func<int, bool> predicate)
			{
				this._parent = parent;
				this._ch = parent._host.Start("Cursor");
				this._position = -1L;
				int columnCount = this.Schema.ColumnCount;
				int[] array;
				Utils.BuildSubsetMaps(columnCount, predicate, ref array, ref this._colToActivesIndex);
				this._getters = new Delegate[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					int num = array[i];
					this._getters[i] = this.CreateGetterDelegate(num);
				}
			}

			// Token: 0x06000A1F RID: 2591 RVA: 0x00036626 File Offset: 0x00034826
			public bool IsColumnActive(int col)
			{
				Contracts.CheckParam(this._ch, 0 <= col && col < this._colToActivesIndex.Length, "col");
				return this._colToActivesIndex[col] >= 0;
			}

			// Token: 0x06000A20 RID: 2592 RVA: 0x00036658 File Offset: 0x00034858
			public void Dispose()
			{
				if (!this._disposed)
				{
					this.DisposeCore();
					this._position = -1L;
					this._ch.Done();
					this._ch.Dispose();
					this._disposed = true;
				}
			}

			// Token: 0x06000A21 RID: 2593 RVA: 0x00036690 File Offset: 0x00034890
			public ValueGetter<TValue> GetGetter<TValue>(int col)
			{
				if (!this.IsColumnActive(col))
				{
					throw Contracts.Except(this._ch, "Column #{0} is requested but not active in the cursor", new object[] { col });
				}
				ValueGetter<TValue> valueGetter = this._getters[this._colToActivesIndex[col]] as ValueGetter<TValue>;
				if (valueGetter == null)
				{
					throw Contracts.Except(this._ch, "Invalid TValue: '{0}'", new object[] { typeof(TValue) });
				}
				return valueGetter;
			}

			// Token: 0x06000A22 RID: 2594 RVA: 0x00036707 File Offset: 0x00034907
			private Delegate CreateGetterDelegate(int col)
			{
				return Utils.MarshalInvoke<int, Delegate>(new Func<int, Delegate>(this.CreateGetterDelegate<int>), this.Schema.GetColumnType(col).RawType, col);
			}

			// Token: 0x06000A23 RID: 2595 RVA: 0x0003672C File Offset: 0x0003492C
			private Delegate CreateGetterDelegate<TValue>(int col)
			{
				CacheDataView.ColumnCache<TValue> columnCache = (CacheDataView.ColumnCache<TValue>)this._parent._caches[col];
				return this.CreateGetterDelegateCore<TValue>(columnCache);
			}

			// Token: 0x06000A24 RID: 2596
			protected abstract ValueGetter<TValue> CreateGetterDelegateCore<TValue>(CacheDataView.ColumnCache<TValue> cache);

			// Token: 0x06000A25 RID: 2597
			protected abstract void DisposeCore();

			// Token: 0x04000532 RID: 1330
			protected readonly CacheDataView _parent;

			// Token: 0x04000533 RID: 1331
			protected readonly IChannel _ch;

			// Token: 0x04000534 RID: 1332
			private readonly int[] _colToActivesIndex;

			// Token: 0x04000535 RID: 1333
			private readonly Delegate[] _getters;

			// Token: 0x04000536 RID: 1334
			protected long _position;

			// Token: 0x04000537 RID: 1335
			private bool _disposed;
		}

		// Token: 0x020001C4 RID: 452
		private sealed class RowCursor<TIndex> : CacheDataView.RowCursorSeekerBase, IRowCursor, ICursor, IDisposable, IRow, ISchematized, ICounted where TIndex : struct, CacheDataView.IIndex
		{
			// Token: 0x17000122 RID: 290
			// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00036753 File Offset: 0x00034953
			public CursorState State
			{
				get
				{
					return this._state;
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000A27 RID: 2599 RVA: 0x0003675C File Offset: 0x0003495C
			public long Batch
			{
				get
				{
					TIndex index = this._index;
					return index.Batch;
				}
			}

			// Token: 0x06000A28 RID: 2600 RVA: 0x0003677D File Offset: 0x0003497D
			public RowCursor(CacheDataView parent, Func<int, bool> predicate, TIndex index)
				: base(parent, predicate)
			{
				this._state = 0;
				this._index = index;
			}

			// Token: 0x06000A29 RID: 2601 RVA: 0x00036798 File Offset: 0x00034998
			public ValueGetter<UInt128> GetIdGetter()
			{
				TIndex index = this._index;
				return index.GetIdGetter();
			}

			// Token: 0x06000A2A RID: 2602 RVA: 0x000367B9 File Offset: 0x000349B9
			public ICursor GetRootCursor()
			{
				return this;
			}

			// Token: 0x06000A2B RID: 2603 RVA: 0x000367BC File Offset: 0x000349BC
			public bool MoveNext()
			{
				if (this._state == 2)
				{
					return false;
				}
				TIndex index = this._index;
				if (index.MoveNext())
				{
					this._position += 1L;
					this._state = 1;
					return true;
				}
				base.Dispose();
				return false;
			}

			// Token: 0x06000A2C RID: 2604 RVA: 0x0003680C File Offset: 0x00034A0C
			public bool MoveMany(long count)
			{
				Contracts.CheckParam(this._ch, count > 0L, "count");
				if (this._state == 2)
				{
					return false;
				}
				TIndex index = this._index;
				if (index.MoveMany(count))
				{
					this._position += count;
					this._state = 1;
					return true;
				}
				base.Dispose();
				return false;
			}

			// Token: 0x06000A2D RID: 2605 RVA: 0x0003686E File Offset: 0x00034A6E
			protected override void DisposeCore()
			{
				this._state = 2;
			}

			// Token: 0x06000A2E RID: 2606 RVA: 0x000368D8 File Offset: 0x00034AD8
			protected override ValueGetter<TValue> CreateGetterDelegateCore<TValue>(CacheDataView.ColumnCache<TValue> cache)
			{
				return delegate(ref TValue value)
				{
					Contracts.Check(this._ch, this._state == 1, "Cannot use getter with cursor in this state");
					CacheDataView.ColumnCache<TValue> cache2 = cache;
					TIndex index = this._index;
					cache2.Fetch((int)index.GetIndex(), ref value);
				};
			}

			// Token: 0x04000538 RID: 1336
			private CursorState _state;

			// Token: 0x04000539 RID: 1337
			private readonly TIndex _index;
		}

		// Token: 0x020001C5 RID: 453
		private sealed class RowSeeker<TWaiter> : CacheDataView.RowCursorSeekerBase, IRowSeeker, IRow, ISchematized, ICounted, IDisposable where TWaiter : struct, CacheDataView.IWaiter
		{
			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000A2F RID: 2607 RVA: 0x00036905 File Offset: 0x00034B05
			public long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x06000A30 RID: 2608 RVA: 0x0003693B File Offset: 0x00034B3B
			public ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._ch, base.Position >= 0L, "Cannot call ID getter in current state");
					val = new UInt128((ulong)base.Position, 0UL);
				};
			}

			// Token: 0x06000A31 RID: 2609 RVA: 0x00036949 File Offset: 0x00034B49
			public RowSeeker(CacheDataView parent, Func<int, bool> predicate, TWaiter waiter)
				: base(parent, predicate)
			{
				this._waiter = waiter;
			}

			// Token: 0x06000A32 RID: 2610 RVA: 0x0003695C File Offset: 0x00034B5C
			public bool MoveTo(long rowIndex)
			{
				if (rowIndex >= 0L)
				{
					TWaiter waiter = this._waiter;
					if (waiter.Wait(rowIndex))
					{
						this._position = rowIndex;
						return true;
					}
				}
				this._position = -1L;
				return false;
			}

			// Token: 0x06000A33 RID: 2611 RVA: 0x00036997 File Offset: 0x00034B97
			protected override void DisposeCore()
			{
			}

			// Token: 0x06000A34 RID: 2612 RVA: 0x000369BC File Offset: 0x00034BBC
			protected override ValueGetter<TValue> CreateGetterDelegateCore<TValue>(CacheDataView.ColumnCache<TValue> cache)
			{
				return delegate(ref TValue value)
				{
					cache.Fetch((int)this.Position, ref value);
				};
			}

			// Token: 0x0400053A RID: 1338
			private readonly TWaiter _waiter;
		}

		// Token: 0x020001C6 RID: 454
		private interface IWaiter
		{
			// Token: 0x06000A36 RID: 2614
			bool Wait(long pos);
		}

		// Token: 0x020001C7 RID: 455
		private sealed class TrivialWaiter : CacheDataView.IWaiter
		{
			// Token: 0x06000A37 RID: 2615 RVA: 0x000369E9 File Offset: 0x00034BE9
			private TrivialWaiter(CacheDataView parent)
			{
				this._lim = parent._rowCount;
			}

			// Token: 0x06000A38 RID: 2616 RVA: 0x000369FD File Offset: 0x00034BFD
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool Wait(long pos)
			{
				return pos < this._lim;
			}

			// Token: 0x06000A39 RID: 2617 RVA: 0x00036A08 File Offset: 0x00034C08
			public static CacheDataView.TrivialWaiter.Wrapper Create(CacheDataView parent)
			{
				return new CacheDataView.TrivialWaiter.Wrapper(new CacheDataView.TrivialWaiter(parent));
			}

			// Token: 0x0400053B RID: 1339
			private readonly long _lim;

			// Token: 0x020001C8 RID: 456
			public struct Wrapper : CacheDataView.IWaiter
			{
				// Token: 0x06000A3A RID: 2618 RVA: 0x00036A15 File Offset: 0x00034C15
				public Wrapper(CacheDataView.TrivialWaiter waiter)
				{
					this._waiter = waiter;
				}

				// Token: 0x06000A3B RID: 2619 RVA: 0x00036A1E File Offset: 0x00034C1E
				public bool Wait(long pos)
				{
					return this._waiter.Wait(pos);
				}

				// Token: 0x0400053C RID: 1340
				private readonly CacheDataView.TrivialWaiter _waiter;
			}
		}

		// Token: 0x020001C9 RID: 457
		private sealed class WaiterWaiter : CacheDataView.IWaiter
		{
			// Token: 0x17000125 RID: 293
			// (get) Token: 0x06000A3C RID: 2620 RVA: 0x00036A2C File Offset: 0x00034C2C
			public bool IsTrivial
			{
				get
				{
					return this._waiters.Length == 0;
				}
			}

			// Token: 0x06000A3D RID: 2621 RVA: 0x00036A3C File Offset: 0x00034C3C
			private WaiterWaiter(CacheDataView parent, Func<int, bool> pred)
			{
				this._parent = parent;
				int[] array = Enumerable.Range(0, this._parent.Schema.ColumnCount).Where(pred).ToArray<int>();
				this._parent.KickoffFiller(array);
				HashSet<OrderedWaiter> hashSet = new HashSet<OrderedWaiter>();
				foreach (int num in array)
				{
					OrderedWaiter waiter = this._parent._caches[num].Waiter;
					if (waiter != null)
					{
						hashSet.Add(waiter);
					}
				}
				if (this._parent._rowCount < 0L && hashSet.Count == 0)
				{
					hashSet.Add(this._parent._cacheDefaultWaiter);
				}
				this._waiters = new OrderedWaiter[hashSet.Count];
				hashSet.CopyTo(this._waiters);
			}

			// Token: 0x06000A3E RID: 2622 RVA: 0x00036B0C File Offset: 0x00034D0C
			public bool Wait(long pos)
			{
				foreach (OrderedWaiter orderedWaiter in this._waiters)
				{
					orderedWaiter.Wait(pos, default(CancellationToken));
				}
				return pos < this._parent._rowCount || this._parent._rowCount == -1L;
			}

			// Token: 0x06000A3F RID: 2623 RVA: 0x00036B61 File Offset: 0x00034D61
			public static CacheDataView.WaiterWaiter.Wrapper Create(CacheDataView parent, Func<int, bool> pred)
			{
				return new CacheDataView.WaiterWaiter.Wrapper(new CacheDataView.WaiterWaiter(parent, pred));
			}

			// Token: 0x0400053D RID: 1341
			private readonly OrderedWaiter[] _waiters;

			// Token: 0x0400053E RID: 1342
			private readonly CacheDataView _parent;

			// Token: 0x020001CA RID: 458
			public struct Wrapper : CacheDataView.IWaiter
			{
				// Token: 0x17000126 RID: 294
				// (get) Token: 0x06000A40 RID: 2624 RVA: 0x00036B6F File Offset: 0x00034D6F
				public bool IsTrivial
				{
					get
					{
						return this._waiter.IsTrivial;
					}
				}

				// Token: 0x06000A41 RID: 2625 RVA: 0x00036B7C File Offset: 0x00034D7C
				public Wrapper(CacheDataView.WaiterWaiter waiter)
				{
					this._waiter = waiter;
				}

				// Token: 0x06000A42 RID: 2626 RVA: 0x00036B85 File Offset: 0x00034D85
				public bool Wait(long pos)
				{
					return this._waiter.Wait(pos);
				}

				// Token: 0x0400053F RID: 1343
				private readonly CacheDataView.WaiterWaiter _waiter;
			}
		}

		// Token: 0x020001CB RID: 459
		private interface IIndex
		{
			// Token: 0x17000127 RID: 295
			// (get) Token: 0x06000A43 RID: 2627
			long Batch { get; }

			// Token: 0x06000A44 RID: 2628
			long GetIndex();

			// Token: 0x06000A45 RID: 2629
			ValueGetter<UInt128> GetIdGetter();

			// Token: 0x06000A46 RID: 2630
			bool MoveNext();

			// Token: 0x06000A47 RID: 2631
			bool MoveMany(long count);
		}

		// Token: 0x020001CC RID: 460
		private sealed class SequenceIndex<TWaiter> : CacheDataView.IIndex where TWaiter : struct, CacheDataView.IWaiter
		{
			// Token: 0x17000128 RID: 296
			// (get) Token: 0x06000A48 RID: 2632 RVA: 0x00036B93 File Offset: 0x00034D93
			public long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x06000A49 RID: 2633 RVA: 0x00036B97 File Offset: 0x00034D97
			private SequenceIndex(TWaiter waiter)
			{
				this._curr = -1L;
				this._waiter = waiter;
			}

			// Token: 0x06000A4A RID: 2634 RVA: 0x00036BAE File Offset: 0x00034DAE
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public long GetIndex()
			{
				return this._curr;
			}

			// Token: 0x06000A4B RID: 2635 RVA: 0x00036BE2 File Offset: 0x00034DE2
			public ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._curr >= 0L, "Cannot call ID getter in current state");
					val = new UInt128((ulong)this._curr, 0UL);
				};
			}

			// Token: 0x06000A4C RID: 2636 RVA: 0x00036BF0 File Offset: 0x00034DF0
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool MoveNext()
			{
				TWaiter waiter = this._waiter;
				if (waiter.Wait(this._curr += 1L))
				{
					return true;
				}
				this._curr = -2L;
				return false;
			}

			// Token: 0x06000A4D RID: 2637 RVA: 0x00036C34 File Offset: 0x00034E34
			public bool MoveMany(long count)
			{
				TWaiter waiter = this._waiter;
				if (waiter.Wait(this._curr += count))
				{
					return true;
				}
				this._curr = -2L;
				return false;
			}

			// Token: 0x06000A4E RID: 2638 RVA: 0x00036C74 File Offset: 0x00034E74
			public static CacheDataView.SequenceIndex<TWaiter>.Wrapper Create(TWaiter waiter)
			{
				return new CacheDataView.SequenceIndex<TWaiter>.Wrapper(new CacheDataView.SequenceIndex<TWaiter>(waiter));
			}

			// Token: 0x04000540 RID: 1344
			private long _curr;

			// Token: 0x04000541 RID: 1345
			private readonly TWaiter _waiter;

			// Token: 0x020001CD RID: 461
			public struct Wrapper : CacheDataView.IIndex
			{
				// Token: 0x06000A50 RID: 2640 RVA: 0x00036C81 File Offset: 0x00034E81
				public Wrapper(CacheDataView.SequenceIndex<TWaiter> index)
				{
					this._index = index;
				}

				// Token: 0x17000129 RID: 297
				// (get) Token: 0x06000A51 RID: 2641 RVA: 0x00036C8A File Offset: 0x00034E8A
				public long Batch
				{
					get
					{
						return this._index.Batch;
					}
				}

				// Token: 0x06000A52 RID: 2642 RVA: 0x00036C97 File Offset: 0x00034E97
				public long GetIndex()
				{
					return this._index.GetIndex();
				}

				// Token: 0x06000A53 RID: 2643 RVA: 0x00036CA4 File Offset: 0x00034EA4
				public ValueGetter<UInt128> GetIdGetter()
				{
					return this._index.GetIdGetter();
				}

				// Token: 0x06000A54 RID: 2644 RVA: 0x00036CB1 File Offset: 0x00034EB1
				public bool MoveNext()
				{
					return this._index.MoveNext();
				}

				// Token: 0x06000A55 RID: 2645 RVA: 0x00036CBE File Offset: 0x00034EBE
				public bool MoveMany(long count)
				{
					return this._index.MoveMany(count);
				}

				// Token: 0x04000542 RID: 1346
				private readonly CacheDataView.SequenceIndex<TWaiter> _index;
			}
		}

		// Token: 0x020001CE RID: 462
		private sealed class RandomIndex<TWaiter> : CacheDataView.IIndex where TWaiter : struct, CacheDataView.IWaiter
		{
			// Token: 0x1700012A RID: 298
			// (get) Token: 0x06000A56 RID: 2646 RVA: 0x00036CCC File Offset: 0x00034ECC
			public long Batch
			{
				get
				{
					return 0L;
				}
			}

			// Token: 0x06000A57 RID: 2647 RVA: 0x00036CD0 File Offset: 0x00034ED0
			private RandomIndex(TWaiter waiter, int[] perm)
			{
				this._curr = -1;
				this._waiter = waiter;
				this._perm = perm;
			}

			// Token: 0x06000A58 RID: 2648 RVA: 0x00036CED File Offset: 0x00034EED
			public long GetIndex()
			{
				return (long)this._perm[this._curr];
			}

			// Token: 0x06000A59 RID: 2649 RVA: 0x00036D30 File Offset: 0x00034F30
			public ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._curr >= 0, "Cannot call ID getter in current state");
					val = new UInt128((ulong)((long)this._perm[this._curr]), 0UL);
				};
			}

			// Token: 0x06000A5A RID: 2650 RVA: 0x00036D40 File Offset: 0x00034F40
			public bool MoveNext()
			{
				if (++this._curr < this._perm.Length)
				{
					TWaiter waiter = this._waiter;
					waiter.Wait((long)this._perm[this._curr]);
					return true;
				}
				this._curr = -2;
				return false;
			}

			// Token: 0x06000A5B RID: 2651 RVA: 0x00036D98 File Offset: 0x00034F98
			public bool MoveMany(long count)
			{
				if (count < (long)(this._perm.Length - this._curr))
				{
					this._curr += (int)count;
					TWaiter waiter = this._waiter;
					waiter.Wait((long)this._perm[this._curr]);
					return true;
				}
				this._curr = -2;
				return false;
			}

			// Token: 0x06000A5C RID: 2652 RVA: 0x00036DF5 File Offset: 0x00034FF5
			public static CacheDataView.RandomIndex<TWaiter>.Wrapper Create(TWaiter waiter, int[] perm)
			{
				return new CacheDataView.RandomIndex<TWaiter>.Wrapper(new CacheDataView.RandomIndex<TWaiter>(waiter, perm));
			}

			// Token: 0x04000543 RID: 1347
			private int _curr;

			// Token: 0x04000544 RID: 1348
			private readonly int[] _perm;

			// Token: 0x04000545 RID: 1349
			private readonly TWaiter _waiter;

			// Token: 0x020001CF RID: 463
			public struct Wrapper : CacheDataView.IIndex
			{
				// Token: 0x06000A5E RID: 2654 RVA: 0x00036E03 File Offset: 0x00035003
				public Wrapper(CacheDataView.RandomIndex<TWaiter> index)
				{
					this._index = index;
				}

				// Token: 0x1700012B RID: 299
				// (get) Token: 0x06000A5F RID: 2655 RVA: 0x00036E0C File Offset: 0x0003500C
				public long Batch
				{
					get
					{
						return this._index.Batch;
					}
				}

				// Token: 0x06000A60 RID: 2656 RVA: 0x00036E19 File Offset: 0x00035019
				public long GetIndex()
				{
					return this._index.GetIndex();
				}

				// Token: 0x06000A61 RID: 2657 RVA: 0x00036E26 File Offset: 0x00035026
				public ValueGetter<UInt128> GetIdGetter()
				{
					return this._index.GetIdGetter();
				}

				// Token: 0x06000A62 RID: 2658 RVA: 0x00036E33 File Offset: 0x00035033
				public bool MoveNext()
				{
					return this._index.MoveNext();
				}

				// Token: 0x06000A63 RID: 2659 RVA: 0x00036E40 File Offset: 0x00035040
				public bool MoveMany(long count)
				{
					return this._index.MoveMany(count);
				}

				// Token: 0x04000546 RID: 1350
				private readonly CacheDataView.RandomIndex<TWaiter> _index;
			}
		}

		// Token: 0x020001D0 RID: 464
		private sealed class JobScheduler
		{
			// Token: 0x06000A64 RID: 2660 RVA: 0x00036E4E File Offset: 0x0003504E
			public JobScheduler(int workersCount)
			{
				this._workersCount = workersCount;
				this._c = -1L;
			}

			// Token: 0x06000A65 RID: 2661 RVA: 0x00036E65 File Offset: 0x00035065
			public long GetAvailableJob(long completedJob)
			{
				if (completedJob == -1L)
				{
					return Interlocked.Increment(ref this._c);
				}
				return completedJob + (long)this._workersCount;
			}

			// Token: 0x04000547 RID: 1351
			private readonly int _workersCount;

			// Token: 0x04000548 RID: 1352
			private long _c;
		}

		// Token: 0x020001D1 RID: 465
		private sealed class BlockSequenceIndex<TWaiter> : CacheDataView.IIndex where TWaiter : struct, CacheDataView.IWaiter
		{
			// Token: 0x1700012C RID: 300
			// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00036E81 File Offset: 0x00035081
			public long Batch
			{
				get
				{
					return this._batch;
				}
			}

			// Token: 0x06000A67 RID: 2663 RVA: 0x00036E89 File Offset: 0x00035089
			private BlockSequenceIndex(TWaiter waiter, CacheDataView.JobScheduler scheduler)
			{
				this._curr = -1L;
				this._batch = -1L;
				this._reserved = true;
				this._waiter = waiter;
				this._scheduler = scheduler;
			}

			// Token: 0x06000A68 RID: 2664 RVA: 0x00036EB6 File Offset: 0x000350B6
			public long GetIndex()
			{
				return this._curr;
			}

			// Token: 0x06000A69 RID: 2665 RVA: 0x00036EEA File Offset: 0x000350EA
			public ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._curr >= 0L, "Cannot call ID getter in current state");
					val = new UInt128((ulong)this._curr, 0UL);
				};
			}

			// Token: 0x06000A6A RID: 2666 RVA: 0x00036EF8 File Offset: 0x000350F8
			public bool MoveNext()
			{
				if ((this._curr & 63L) == 63L)
				{
					this._batch = this._scheduler.GetAvailableJob(this._batch);
					this._curr = this._batch << 6;
					TWaiter waiter = this._waiter;
					if (waiter.Wait(this._curr))
					{
						TWaiter waiter2 = this._waiter;
						this._reserved = waiter2.Wait(this._curr + 63L);
						return true;
					}
				}
				else
				{
					if (this._reserved)
					{
						this._curr += 1L;
						return true;
					}
					TWaiter waiter3 = this._waiter;
					if (waiter3.Wait(this._curr += 1L))
					{
						return true;
					}
				}
				this._curr = -2L;
				return false;
			}

			// Token: 0x06000A6B RID: 2667 RVA: 0x00036FC8 File Offset: 0x000351C8
			public bool MoveMany(long count)
			{
				while ((count -= 1L) >= 0L && this.MoveNext())
				{
				}
				return this._curr >= 0L;
			}

			// Token: 0x06000A6C RID: 2668 RVA: 0x00036FEA File Offset: 0x000351EA
			public static CacheDataView.BlockSequenceIndex<TWaiter>.Wrapper Create(TWaiter waiter, CacheDataView.JobScheduler scheduler)
			{
				return new CacheDataView.BlockSequenceIndex<TWaiter>.Wrapper(new CacheDataView.BlockSequenceIndex<TWaiter>(waiter, scheduler));
			}

			// Token: 0x04000549 RID: 1353
			private long _curr;

			// Token: 0x0400054A RID: 1354
			private long _batch;

			// Token: 0x0400054B RID: 1355
			private bool _reserved;

			// Token: 0x0400054C RID: 1356
			private readonly CacheDataView.JobScheduler _scheduler;

			// Token: 0x0400054D RID: 1357
			private readonly TWaiter _waiter;

			// Token: 0x020001D2 RID: 466
			public struct Wrapper : CacheDataView.IIndex
			{
				// Token: 0x06000A6E RID: 2670 RVA: 0x00036FF8 File Offset: 0x000351F8
				public Wrapper(CacheDataView.BlockSequenceIndex<TWaiter> index)
				{
					this._index = index;
				}

				// Token: 0x1700012D RID: 301
				// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00037001 File Offset: 0x00035201
				public long Batch
				{
					get
					{
						return this._index.Batch;
					}
				}

				// Token: 0x06000A70 RID: 2672 RVA: 0x0003700E File Offset: 0x0003520E
				public long GetIndex()
				{
					return this._index.GetIndex();
				}

				// Token: 0x06000A71 RID: 2673 RVA: 0x0003701B File Offset: 0x0003521B
				public ValueGetter<UInt128> GetIdGetter()
				{
					return this._index.GetIdGetter();
				}

				// Token: 0x06000A72 RID: 2674 RVA: 0x00037028 File Offset: 0x00035228
				public bool MoveNext()
				{
					return this._index.MoveNext();
				}

				// Token: 0x06000A73 RID: 2675 RVA: 0x00037035 File Offset: 0x00035235
				public bool MoveMany(long count)
				{
					return this._index.MoveMany(count);
				}

				// Token: 0x0400054E RID: 1358
				private readonly CacheDataView.BlockSequenceIndex<TWaiter> _index;
			}
		}

		// Token: 0x020001D3 RID: 467
		private sealed class BlockRandomIndex<TWaiter> : CacheDataView.IIndex where TWaiter : struct, CacheDataView.IWaiter
		{
			// Token: 0x1700012E RID: 302
			// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00037043 File Offset: 0x00035243
			public long Batch
			{
				get
				{
					return this._batch;
				}
			}

			// Token: 0x06000A75 RID: 2677 RVA: 0x0003704C File Offset: 0x0003524C
			private BlockRandomIndex(TWaiter waiter, CacheDataView.JobScheduler scheduler, int[] perm)
			{
				this._curr = (this._currMax = -1);
				this._batch = -1L;
				this._perm = perm;
				this._waiter = waiter;
				this._scheduler = scheduler;
			}

			// Token: 0x06000A76 RID: 2678 RVA: 0x0003708C File Offset: 0x0003528C
			public long GetIndex()
			{
				return (long)this._perm[this._curr];
			}

			// Token: 0x06000A77 RID: 2679 RVA: 0x000370CF File Offset: 0x000352CF
			public ValueGetter<UInt128> GetIdGetter()
			{
				return delegate(ref UInt128 val)
				{
					Contracts.Check(this._curr >= 0, "Cannot call ID getter in current state");
					val = new UInt128((ulong)((long)this._perm[this._curr]), 0UL);
				};
			}

			// Token: 0x06000A78 RID: 2680 RVA: 0x000370E0 File Offset: 0x000352E0
			public bool MoveNext()
			{
				if (this._curr == this._currMax)
				{
					this._batch = this._scheduler.GetAvailableJob(this._batch);
					this._curr = (int)((int)this._batch << 6);
					if (this._curr >= this._perm.Length || this._curr < 0)
					{
						this._curr = -2;
						return false;
					}
					this._currMax = Math.Min(this._perm.Length - 1, this._curr + 63);
				}
				else
				{
					this._curr++;
				}
				TWaiter waiter = this._waiter;
				waiter.Wait(this.GetIndex());
				return true;
			}

			// Token: 0x06000A79 RID: 2681 RVA: 0x0003718E File Offset: 0x0003538E
			public bool MoveMany(long count)
			{
				while ((count -= 1L) >= 0L && this.MoveNext())
				{
				}
				return this._curr >= 0;
			}

			// Token: 0x06000A7A RID: 2682 RVA: 0x000371AF File Offset: 0x000353AF
			public static CacheDataView.BlockRandomIndex<TWaiter>.Wrapper Create(TWaiter waiter, CacheDataView.JobScheduler scheduler, int[] perm)
			{
				return new CacheDataView.BlockRandomIndex<TWaiter>.Wrapper(new CacheDataView.BlockRandomIndex<TWaiter>(waiter, scheduler, perm));
			}

			// Token: 0x0400054F RID: 1359
			private int _curr;

			// Token: 0x04000550 RID: 1360
			private int _currMax;

			// Token: 0x04000551 RID: 1361
			private readonly int[] _perm;

			// Token: 0x04000552 RID: 1362
			private readonly CacheDataView.JobScheduler _scheduler;

			// Token: 0x04000553 RID: 1363
			private readonly TWaiter _waiter;

			// Token: 0x04000554 RID: 1364
			private long _batch;

			// Token: 0x020001D4 RID: 468
			public struct Wrapper : CacheDataView.IIndex
			{
				// Token: 0x06000A7C RID: 2684 RVA: 0x000371BE File Offset: 0x000353BE
				public Wrapper(CacheDataView.BlockRandomIndex<TWaiter> index)
				{
					this._index = index;
				}

				// Token: 0x1700012F RID: 303
				// (get) Token: 0x06000A7D RID: 2685 RVA: 0x000371C7 File Offset: 0x000353C7
				public long Batch
				{
					get
					{
						return this._index.Batch;
					}
				}

				// Token: 0x06000A7E RID: 2686 RVA: 0x000371D4 File Offset: 0x000353D4
				public long GetIndex()
				{
					return this._index.GetIndex();
				}

				// Token: 0x06000A7F RID: 2687 RVA: 0x000371E1 File Offset: 0x000353E1
				public ValueGetter<UInt128> GetIdGetter()
				{
					return this._index.GetIdGetter();
				}

				// Token: 0x06000A80 RID: 2688 RVA: 0x000371EE File Offset: 0x000353EE
				public bool MoveNext()
				{
					return this._index.MoveNext();
				}

				// Token: 0x06000A81 RID: 2689 RVA: 0x000371FB File Offset: 0x000353FB
				public bool MoveMany(long count)
				{
					return this._index.MoveMany(count);
				}

				// Token: 0x04000555 RID: 1365
				private readonly CacheDataView.BlockRandomIndex<TWaiter> _index;
			}
		}

		// Token: 0x020001D5 RID: 469
		private abstract class ColumnCache
		{
			// Token: 0x17000130 RID: 304
			// (get) Token: 0x06000A82 RID: 2690 RVA: 0x00037209 File Offset: 0x00035409
			public OrderedWaiter Waiter
			{
				get
				{
					return this._waiter;
				}
			}

			// Token: 0x06000A83 RID: 2691 RVA: 0x00037211 File Offset: 0x00035411
			protected ColumnCache(IExceptionContext ctx, OrderedWaiter waiter)
			{
				this._ctx = ctx;
				this._waiter = waiter;
			}

			// Token: 0x06000A84 RID: 2692 RVA: 0x00037228 File Offset: 0x00035428
			public static CacheDataView.ColumnCache Create(CacheDataView parent, IRowCursor input, int srcCol, OrderedWaiter waiter)
			{
				IHost host = parent._host;
				ColumnType columnType = input.Schema.GetColumnType(srcCol);
				Type type;
				if (columnType.IsVector)
				{
					type = typeof(CacheDataView.ColumnCache.ImplVec<>).MakeGenericType(new Type[] { columnType.ItemType.RawType });
				}
				else
				{
					type = typeof(CacheDataView.ColumnCache.ImplOne<>).MakeGenericType(new Type[] { columnType.RawType });
				}
				if (CacheDataView.ColumnCache._pipeConstructorTypes == null)
				{
					Interlocked.CompareExchange<Type[]>(ref CacheDataView.ColumnCache._pipeConstructorTypes, new Type[]
					{
						typeof(CacheDataView),
						typeof(IRowCursor),
						typeof(int),
						typeof(OrderedWaiter)
					}, null);
				}
				ConstructorInfo constructor = type.GetConstructor(CacheDataView.ColumnCache._pipeConstructorTypes);
				return (CacheDataView.ColumnCache)constructor.Invoke(new object[] { parent, input, srcCol, waiter });
			}

			// Token: 0x06000A85 RID: 2693
			public abstract void CacheCurrent();

			// Token: 0x06000A86 RID: 2694 RVA: 0x00037330 File Offset: 0x00035530
			public virtual void Freeze()
			{
				this._waiter = null;
			}

			// Token: 0x04000556 RID: 1366
			protected IExceptionContext _ctx;

			// Token: 0x04000557 RID: 1367
			private static volatile Type[] _pipeConstructorTypes;

			// Token: 0x04000558 RID: 1368
			private OrderedWaiter _waiter;

			// Token: 0x020001D7 RID: 471
			private sealed class ImplVec<T> : CacheDataView.ColumnCache<VBuffer<T>>
			{
				// Token: 0x06000A89 RID: 2697 RVA: 0x0003734C File Offset: 0x0003554C
				public ImplVec(CacheDataView parent, IRowCursor input, int srcCol, OrderedWaiter waiter)
					: base(parent, input, srcCol, waiter)
				{
					ColumnType columnType = input.Schema.GetColumnType(srcCol);
					this._uniformLength = columnType.VectorSize;
					this._indices = new CacheDataView.ColumnCache.ImplVec<T>.BigArray<int>();
					this._values = new CacheDataView.ColumnCache.ImplVec<T>.BigArray<T>();
					this._getter = input.GetGetter<VBuffer<T>>(srcCol);
				}

				// Token: 0x06000A8A RID: 2698 RVA: 0x000373A0 File Offset: 0x000355A0
				public override void CacheCurrent()
				{
					this._getter.Invoke(ref this._temp);
					if (this._uniformLength != 0 && this._uniformLength != this._temp.Length)
					{
						throw Contracts.Except(this._ctx, "Caching expected vector of size {0}, but {1} encountered.", new object[]
						{
							this._uniformLength,
							this._temp.Length
						});
					}
					if (!this._temp.IsDense)
					{
						this._indices.AddRange(this._temp.Indices, this._temp.Count);
					}
					this._values.AddRange(this._temp.Values, this._temp.Count);
					int num = this._rowCount + 1;
					Utils.EnsureSize<long>(ref this._indexBoundaries, num + 1, true);
					Utils.EnsureSize<long>(ref this._valueBoundaries, num + 1, true);
					this._indexBoundaries[num] = this._indices.Length;
					this._valueBoundaries[num] = this._values.Length;
					if (this._uniformLength == 0)
					{
						Utils.EnsureSize<int>(ref this._lengths, num, true);
						this._lengths[num - 1] = this._temp.Length;
					}
					this._rowCount = num;
				}

				// Token: 0x06000A8B RID: 2699 RVA: 0x000374E4 File Offset: 0x000356E4
				public override void Fetch(int idx, ref VBuffer<T> value)
				{
					int num = (int)(this._indexBoundaries[idx + 1] - this._indexBoundaries[idx]);
					int num2 = (int)(this._valueBoundaries[idx + 1] - this._valueBoundaries[idx]);
					int num3 = ((this._uniformLength == 0) ? this._lengths[idx] : this._uniformLength);
					T[] values = value.Values;
					Utils.EnsureSize<T>(ref values, num2, true);
					this._values.CopyTo(this._valueBoundaries[idx], values, num2);
					int[] indices = value.Indices;
					if (num2 < num3)
					{
						Utils.EnsureSize<int>(ref indices, num, true);
						this._indices.CopyTo(this._indexBoundaries[idx], indices, num);
						value = new VBuffer<T>(num3, num, values, indices);
						return;
					}
					value = new VBuffer<T>(num3, values, indices);
				}

				// Token: 0x06000A8C RID: 2700 RVA: 0x000375A8 File Offset: 0x000357A8
				public override void Freeze()
				{
					Array.Resize<long>(ref this._indexBoundaries, this._rowCount + 1);
					Array.Resize<long>(ref this._valueBoundaries, this._rowCount + 1);
					Array.Resize<int>(ref this._lengths, this._rowCount);
					this._values.TrimCapacity();
					this._indices.TrimCapacity();
					this._temp = default(VBuffer<T>);
					base.Freeze();
					this._getter = null;
				}

				// Token: 0x04000559 RID: 1369
				private int _rowCount;

				// Token: 0x0400055A RID: 1370
				private long[] _indexBoundaries;

				// Token: 0x0400055B RID: 1371
				private long[] _valueBoundaries;

				// Token: 0x0400055C RID: 1372
				private int[] _lengths;

				// Token: 0x0400055D RID: 1373
				private readonly int _uniformLength;

				// Token: 0x0400055E RID: 1374
				private readonly CacheDataView.ColumnCache.ImplVec<T>.BigArray<int> _indices;

				// Token: 0x0400055F RID: 1375
				private readonly CacheDataView.ColumnCache.ImplVec<T>.BigArray<T> _values;

				// Token: 0x04000560 RID: 1376
				private ValueGetter<VBuffer<T>> _getter;

				// Token: 0x04000561 RID: 1377
				private VBuffer<T> _temp;

				// Token: 0x020001D8 RID: 472
				private sealed class BigArray<TValue>
				{
					// Token: 0x17000131 RID: 305
					// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0003761B File Offset: 0x0003581B
					public long Length
					{
						get
						{
							return this._len;
						}
					}

					// Token: 0x06000A8F RID: 2703 RVA: 0x0003762B File Offset: 0x0003582B
					private void LongMinToMajorMinorMin(long min, out int major, out int minor)
					{
						major = (int)(min >> 25);
						minor = (int)(min & 33554431L);
					}

					// Token: 0x06000A90 RID: 2704 RVA: 0x0003763F File Offset: 0x0003583F
					private void LongLimToMajorMaxMinorLim(long lim, out int major, out int minor)
					{
						major = (int)((lim -= 1L) >> 25);
						minor = (int)((lim & 33554431L) + 1L);
					}

					// Token: 0x06000A91 RID: 2705 RVA: 0x0003765C File Offset: 0x0003585C
					public void TrimCapacity()
					{
						if (this._len == 0L)
						{
							return;
						}
						int num;
						int num2;
						this.LongLimToMajorMaxMinorLim(this._len, out num, out num2);
						if (Utils.Size<TValue>(this._values[num]) != num2)
						{
							Array.Resize<TValue>(ref this._values[num], num2);
						}
						Array.Resize<TValue[]>(ref this._values, num + 1);
					}

					// Token: 0x06000A92 RID: 2706 RVA: 0x000376B4 File Offset: 0x000358B4
					public void AddRange(TValue[] src, int length)
					{
						if (length == 0)
						{
							return;
						}
						int num;
						int num2;
						this.LongMinToMajorMinorMin(this._len, out num, out num2);
						int num3;
						int num4;
						this.LongLimToMajorMaxMinorLim(this._len + (long)length, out num3, out num4);
						Utils.EnsureSize<TValue[]>(ref this._values, num3 + 1, 33554432, true);
						switch (num3 - num)
						{
						case 0:
							Utils.EnsureSize<TValue>(ref this._values[num3], (num3 >= 4) ? 33554432 : num4, 33554432, true);
							Array.Copy(src, 0, this._values[num3], num2, length);
							break;
						case 1:
							Utils.EnsureSize<TValue>(ref this._values[num], 33554432, 33554432, true);
							Array.Copy(src, 0, this._values[num], num2, 33554432 - num2);
							Utils.EnsureSize<TValue>(ref this._values[num3], (num3 >= 4) ? 33554432 : num4, 33554432, true);
							Array.Copy(src, 33554432 - num2, this._values[num3], 0, num4);
							break;
						default:
						{
							Utils.EnsureSize<TValue>(ref this._values[num], 33554432, 33554432, true);
							int num5 = 33554432 - num2;
							Array.Copy(src, 0, this._values[num], num2, num5);
							for (int i = num + 1; i < num3; i++)
							{
								this._values[i] = new TValue[33554432];
								Array.Copy(src, num5, this._values[i], 0, 33554432);
								num5 += 33554432;
							}
							Utils.EnsureSize<TValue>(ref this._values[num3], (num3 >= 4) ? 33554432 : num4, 33554432, true);
							Array.Copy(src, num5, this._values[num3], 0, num4);
							break;
						}
						}
						this._len += (long)length;
					}

					// Token: 0x06000A93 RID: 2707 RVA: 0x00037888 File Offset: 0x00035A88
					public void CopyTo(long idx, TValue[] dst, int length)
					{
						if (length == 0)
						{
							return;
						}
						int num;
						int num2;
						this.LongMinToMajorMinorMin(idx, out num, out num2);
						int num3;
						int num4;
						this.LongLimToMajorMaxMinorLim(idx + (long)length, out num3, out num4);
						switch (num3 - num)
						{
						case 0:
							Array.Copy(this._values[num3], num2, dst, 0, length);
							return;
						case 1:
							Array.Copy(this._values[num], num2, dst, 0, 33554432 - num2);
							Array.Copy(this._values[num3], 0, dst, 33554432 - num2, num4);
							return;
						default:
						{
							int num5 = 33554432 - num2;
							Array.Copy(this._values[num], num2, dst, 0, num5);
							for (int i = num + 1; i < num3; i++)
							{
								Array.Copy(this._values[i], 0, dst, num5, 33554432);
								num5 += 33554432;
							}
							Array.Copy(this._values[num3], 0, dst, num5, num4);
							return;
						}
						}
					}

					// Token: 0x04000562 RID: 1378
					private const int _bits = 25;

					// Token: 0x04000563 RID: 1379
					private const int _subLength = 33554432;

					// Token: 0x04000564 RID: 1380
					private const int _mask = 33554431;

					// Token: 0x04000565 RID: 1381
					private const int _fullAllocationBeyond = 4;

					// Token: 0x04000566 RID: 1382
					private long _len;

					// Token: 0x04000567 RID: 1383
					private TValue[][] _values;
				}
			}

			// Token: 0x020001D9 RID: 473
			private sealed class ImplOne<T> : CacheDataView.ColumnCache<T>
			{
				// Token: 0x06000A94 RID: 2708 RVA: 0x00037969 File Offset: 0x00035B69
				public ImplOne(CacheDataView parent, IRowCursor input, int srcCol, OrderedWaiter waiter)
					: base(parent, input, srcCol, waiter)
				{
					this._getter = input.GetGetter<T>(srcCol);
					if (parent._rowCount >= 0L)
					{
						this._values = new T[(int)parent._rowCount];
					}
				}

				// Token: 0x06000A95 RID: 2709 RVA: 0x000379A0 File Offset: 0x00035BA0
				public override void CacheCurrent()
				{
					Utils.EnsureSize<T>(ref this._values, this._rowCount + 1, true);
					this._getter.Invoke(ref this._values[this._rowCount]);
					this._rowCount++;
				}

				// Token: 0x06000A96 RID: 2710 RVA: 0x000379EC File Offset: 0x00035BEC
				public override void Fetch(int idx, ref T value)
				{
					value = this._values[idx];
				}

				// Token: 0x06000A97 RID: 2711 RVA: 0x00037A00 File Offset: 0x00035C00
				public override void Freeze()
				{
					Array.Resize<T>(ref this._values, this._rowCount);
					base.Freeze();
					this._getter = null;
				}

				// Token: 0x04000568 RID: 1384
				private int _rowCount;

				// Token: 0x04000569 RID: 1385
				private T[] _values;

				// Token: 0x0400056A RID: 1386
				private ValueGetter<T> _getter;
			}
		}

		// Token: 0x020001D6 RID: 470
		private abstract class ColumnCache<T> : CacheDataView.ColumnCache
		{
			// Token: 0x06000A87 RID: 2695 RVA: 0x00037339 File Offset: 0x00035539
			public ColumnCache(CacheDataView parent, IRowCursor input, int srcCol, OrderedWaiter waiter)
				: base(parent._host, waiter)
			{
			}

			// Token: 0x06000A88 RID: 2696
			public abstract void Fetch(int idx, ref T value);
		}
	}
}
