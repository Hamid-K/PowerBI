using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Internal
{
	// Token: 0x020001BB RID: 443
	internal class ParallelEnumerator<T> : IEnumerator<T>, IDisposable, IEnumerator where T : class
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x000106F4 File Offset: 0x0000E8F4
		public ParallelEnumerator(IEngineHost engineHost, IResource resource, int readers, Func<int, T> getBufferedItem, Action<T> disposeItem)
		{
			if (readers <= 0)
			{
				throw new ArgumentOutOfRangeException("readers");
			}
			this.syncRoot = new object();
			this.engineHost = engineHost;
			this.resource = resource;
			this.readerLimit = readers;
			this.maxReaders = 1;
			this.maxItems = this.maxReaders * 2;
			this.getBufferedItem = getBufferedItem;
			this.disposeItem = disposeItem;
			this.unreadItems = new Queue<ParallelEnumerator<T>.Item>(this.maxItems);
			this.unfilledItems = new Queue<ParallelEnumerator<T>.Item>(this.maxItems);
			this.unfilledItemsNotFull = new ManualResetEvent(true);
			this.nextItemComplete = new ManualResetEvent(false);
			this.threadPool = this.engineHost.QueryService<IThreadPoolService>();
			this.fillerThreadStart = CloneCurrentCultures.CreateWrapper(new ThreadStart(this.FillerThread));
			this.FillItem(new ParallelEnumerator<T>.Item());
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x000107C8 File Offset: 0x0000E9C8
		public T Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600087B RID: 2171 RVA: 0x000107D0 File Offset: 0x0000E9D0
		object IEnumerator.Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000107DD File Offset: 0x0000E9DD
		public bool MoveNext()
		{
			return this.TryGetNextItemValue(out this.current, false) && this.current != null;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00010800 File Offset: 0x0000EA00
		public void Dispose()
		{
			this.Cancel();
			T t;
			while (this.TryGetNextItemValue(out t, true))
			{
				if (t != null)
				{
					this.DisposeItemValue(t);
				}
			}
			using (IHostTrace hostTrace = TracingService.CreatePerformanceTrace(this.engineHost, "ParallelEnumerator/Dispose", TraceEventType.Information, this.resource))
			{
				hostTrace.Add("maxActiveReaders", this.maxActiveReaders, false);
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0001087C File Offset: 0x0000EA7C
		private void Cancel()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				this.canceled = true;
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x000108C0 File Offset: 0x0000EAC0
		private bool TryGetNextItemValue(out T value, bool ignoreExceptions = false)
		{
			ParallelEnumerator<T>.Item item;
			if (this.TryGetNextItem(out item))
			{
				try
				{
					value = item.Value;
					if (value == null)
					{
						this.Cancel();
					}
				}
				catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
				{
					if (!ignoreExceptions)
					{
						throw;
					}
					value = default(T);
				}
				object obj2 = this.syncRoot;
				bool flag2;
				lock (obj2)
				{
					flag2 = this.canceled;
				}
				if (!flag2)
				{
					this.FillItem(item);
				}
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x00010974 File Offset: 0x0000EB74
		private void FillItem(ParallelEnumerator<T>.Item item)
		{
			item.Clear();
			int num = this.nextItemIndex;
			this.nextItemIndex = num + 1;
			item.Index = num;
			this.unreadItems.Enqueue(item);
			this.EnqueueUnfilledItem(item);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000109B4 File Offset: 0x0000EBB4
		private void EnqueueUnfilledItem(ParallelEnumerator<T>.Item item)
		{
			bool flag = false;
			for (;;)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.unfilledItems.Count < this.maxItems)
					{
						this.unfilledItems.Enqueue(item);
						if (this.activeReaders < this.maxReaders)
						{
							this.activeReaders++;
							this.maxActiveReaders = Math.Max(this.maxActiveReaders, this.activeReaders);
							flag = true;
						}
						break;
					}
					this.unfilledItemsNotFull.Reset();
				}
				this.unfilledItemsNotFull.WaitOne();
			}
			if (flag)
			{
				this.threadPool.StartThread(this.fillerThreadStart);
			}
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00010A78 File Offset: 0x0000EC78
		protected virtual bool TryGetNextItem(out ParallelEnumerator<T>.Item item)
		{
			if (this.unreadItems.Count == 0)
			{
				item = null;
				return false;
			}
			item = this.unreadItems.Dequeue();
			bool flag2;
			for (;;)
			{
				object obj = this.syncRoot;
				bool flag3;
				lock (obj)
				{
					if (item.IsComplete)
					{
						this.firstResultComplete = true;
						flag2 = true;
						break;
					}
					this.nextItemComplete.Reset();
					flag3 = this.canceled;
					if (flag3 && this.pendingItemCount == 0)
					{
						item = null;
						flag2 = false;
						break;
					}
				}
				if (!flag3 && this.firstResultComplete)
				{
					this.AddFillerThreads();
				}
				this.nextItemComplete.WaitOne();
			}
			return flag2;
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00010B2C File Offset: 0x0000ED2C
		private void AddFillerThreads()
		{
			if (this.maxReaders < this.readerLimit)
			{
				this.maxReaders = Math.Min(this.maxReaders * 2, this.readerLimit);
				this.maxItems = this.maxReaders * 2;
				for (int i = this.unreadItems.Count + 1; i < this.maxItems; i++)
				{
					this.FillItem(new ParallelEnumerator<T>.Item());
				}
			}
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00010B96 File Offset: 0x0000ED96
		protected virtual void DisposeItemValue(T value)
		{
			this.disposeItem(value);
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00010BA4 File Offset: 0x0000EDA4
		protected virtual T GetItemValue(int index)
		{
			return this.getBufferedItem(index);
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		private void FillerThread()
		{
			for (;;)
			{
				object obj = this.syncRoot;
				ParallelEnumerator<T>.Item item;
				lock (obj)
				{
					if (this.canceled || this.unfilledItems.Count == 0)
					{
						this.activeReaders--;
						break;
					}
					item = this.unfilledItems.Dequeue();
					this.pendingItemCount++;
					this.unfilledItemsNotFull.Set();
				}
				try
				{
					item.Value = this.GetItemValue(item.Index);
				}
				catch (Exception ex)
				{
					using (IHostTrace hostTrace = TracingService.CreateTrace(this.engineHost, "ParallelEnumerator/FillerThread", TraceEventType.Information, this.resource))
					{
						hostTrace.Add(ex, true);
					}
					item.Exception = ex;
				}
				obj = this.syncRoot;
				lock (obj)
				{
					item.IsComplete = true;
					this.pendingItemCount--;
					this.nextItemComplete.Set();
					continue;
				}
				break;
			}
		}

		// Token: 0x040004DB RID: 1243
		private const int itemToReaderRatio = 2;

		// Token: 0x040004DC RID: 1244
		private const int threadStartRate = 2;

		// Token: 0x040004DD RID: 1245
		private readonly object syncRoot;

		// Token: 0x040004DE RID: 1246
		private readonly IEngineHost engineHost;

		// Token: 0x040004DF RID: 1247
		private readonly IThreadPoolService threadPool;

		// Token: 0x040004E0 RID: 1248
		private readonly ThreadStart fillerThreadStart;

		// Token: 0x040004E1 RID: 1249
		private readonly IResource resource;

		// Token: 0x040004E2 RID: 1250
		private readonly Func<int, T> getBufferedItem;

		// Token: 0x040004E3 RID: 1251
		private readonly Action<T> disposeItem;

		// Token: 0x040004E4 RID: 1252
		private readonly Queue<ParallelEnumerator<T>.Item> unreadItems;

		// Token: 0x040004E5 RID: 1253
		private readonly Queue<ParallelEnumerator<T>.Item> unfilledItems;

		// Token: 0x040004E6 RID: 1254
		private readonly ManualResetEvent unfilledItemsNotFull;

		// Token: 0x040004E7 RID: 1255
		private readonly ManualResetEvent nextItemComplete;

		// Token: 0x040004E8 RID: 1256
		private readonly int readerLimit;

		// Token: 0x040004E9 RID: 1257
		private int maxItems;

		// Token: 0x040004EA RID: 1258
		private int maxReaders;

		// Token: 0x040004EB RID: 1259
		private int activeReaders;

		// Token: 0x040004EC RID: 1260
		private int maxActiveReaders;

		// Token: 0x040004ED RID: 1261
		private bool canceled;

		// Token: 0x040004EE RID: 1262
		private bool firstResultComplete;

		// Token: 0x040004EF RID: 1263
		private int nextItemIndex;

		// Token: 0x040004F0 RID: 1264
		private int pendingItemCount;

		// Token: 0x040004F1 RID: 1265
		private T current;

		// Token: 0x020001BC RID: 444
		protected class Item
		{
			// Token: 0x1700027D RID: 637
			// (get) Token: 0x06000888 RID: 2184 RVA: 0x00010CF0 File Offset: 0x0000EEF0
			// (set) Token: 0x06000889 RID: 2185 RVA: 0x00010CF8 File Offset: 0x0000EEF8
			public bool IsComplete
			{
				get
				{
					return this.complete;
				}
				set
				{
					this.complete = value;
				}
			}

			// Token: 0x1700027E RID: 638
			// (get) Token: 0x0600088A RID: 2186 RVA: 0x00010D01 File Offset: 0x0000EF01
			// (set) Token: 0x0600088B RID: 2187 RVA: 0x00010D09 File Offset: 0x0000EF09
			public int Index
			{
				get
				{
					return this.index;
				}
				set
				{
					this.index = value;
				}
			}

			// Token: 0x1700027F RID: 639
			// (get) Token: 0x0600088C RID: 2188 RVA: 0x00010D12 File Offset: 0x0000EF12
			// (set) Token: 0x0600088D RID: 2189 RVA: 0x00010D29 File Offset: 0x0000EF29
			public T Value
			{
				get
				{
					if (this.exception != null)
					{
						throw this.exception;
					}
					return this.value;
				}
				set
				{
					this.value = value;
				}
			}

			// Token: 0x17000280 RID: 640
			// (get) Token: 0x0600088E RID: 2190 RVA: 0x00010D32 File Offset: 0x0000EF32
			// (set) Token: 0x0600088F RID: 2191 RVA: 0x00010D3A File Offset: 0x0000EF3A
			public Exception Exception
			{
				get
				{
					return this.exception;
				}
				set
				{
					this.exception = value;
				}
			}

			// Token: 0x06000890 RID: 2192 RVA: 0x00010D43 File Offset: 0x0000EF43
			public void Clear()
			{
				this.complete = false;
				this.value = default(T);
				this.exception = null;
			}

			// Token: 0x040004F2 RID: 1266
			private bool complete;

			// Token: 0x040004F3 RID: 1267
			private int index;

			// Token: 0x040004F4 RID: 1268
			private T value;

			// Token: 0x040004F5 RID: 1269
			private Exception exception;
		}
	}
}
