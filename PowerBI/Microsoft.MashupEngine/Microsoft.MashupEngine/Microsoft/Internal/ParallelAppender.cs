using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Internal
{
	// Token: 0x020001B9 RID: 441
	internal class ParallelAppender<T> : IAppender<T>, IDisposable where T : class
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x00010194 File Offset: 0x0000E394
		public ParallelAppender(IEngineHost engineHost, IResource resource, int writers, Action<int, T> putBufferedItem, Action<T> disposeItem)
		{
			if (writers <= 0)
			{
				throw new ArgumentOutOfRangeException("writers");
			}
			this.syncRoot = new object();
			this.engineHost = engineHost;
			this.resource = resource;
			this.maxWriters = writers;
			this.activeWriters = writers;
			this.maxItems = writers * 2;
			this.putBufferedItem = putBufferedItem;
			this.disposeItem = disposeItem;
			this.completedItems = new Queue<ParallelAppender<T>.Item>(this.maxItems);
			this.unwrittenItems = new Queue<ParallelAppender<T>.Item>(this.maxItems);
			this.completedItemsNotEmpty = new ManualResetEvent(true);
			this.unwrittenItemsNotFull = new ManualResetEvent(true);
			this.threadPool = this.engineHost.QueryService<IThreadPoolService>();
			this.writerThreadStart = CloneCurrentCultures.CreateWrapper(new ThreadStart(this.WriterThread));
			for (int i = 0; i < this.maxItems; i++)
			{
				this.completedItems.Enqueue(new ParallelAppender<T>.Item());
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001027C File Offset: 0x0000E47C
		public void Append(T item)
		{
			ParallelAppender<T>.Item item2 = new ParallelAppender<T>.Item();
			int num = this.nextItemIndex;
			this.nextItemIndex = num + 1;
			item2.Index = num;
			item2.Value = item;
			this.PutNextItem(item2);
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x000102B4 File Offset: 0x0000E4B4
		public void Dispose()
		{
			Exception ex = null;
			bool flag = false;
			while (!flag)
			{
				try
				{
					while (this.activeWriters > 0)
					{
						this.PutNextItem(new ParallelAppender<T>.Item
						{
							Canceled = true
						});
					}
					ParallelAppender<T>.Item item;
					while (this.TryDequeueCompletedItem(out item))
					{
						this.DisposeItem(item);
					}
					flag = true;
				}
				catch (Exception ex2) when (SafeExceptions.IsSafeException(ex2))
				{
					ex = ex ?? ex2;
				}
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00010338 File Offset: 0x0000E538
		private void DisposeItem(ParallelAppender<T>.Item item)
		{
			if (item.Canceled)
			{
				this.activeWriters--;
			}
			if (item.Value != null)
			{
				this.DisposeItemValue(item.Value);
			}
			if (item.Exception != null)
			{
				throw item.Exception;
			}
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00010378 File Offset: 0x0000E578
		protected virtual void PutNextItem(ParallelAppender<T>.Item item)
		{
			try
			{
				this.DisposeItem(this.DequeueCompletedItem());
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				this.EnqueueUnwrittenItem(new ParallelAppender<T>.Item
				{
					Index = item.Index,
					Canceled = item.Canceled,
					Exception = ex
				});
				throw;
			}
			this.EnqueueUnwrittenItem(item);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x000103F0 File Offset: 0x0000E5F0
		private ParallelAppender<T>.Item DequeueCompletedItem()
		{
			ParallelAppender<T>.Item item;
			while (!this.TryDequeueCompletedItem(out item))
			{
				this.completedItemsNotEmpty.WaitOne();
			}
			return item;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00010418 File Offset: 0x0000E618
		private bool TryDequeueCompletedItem(out ParallelAppender<T>.Item item)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.completedItems.Count > 0)
				{
					item = this.completedItems.Dequeue();
					return true;
				}
				this.completedItemsNotEmpty.Reset();
			}
			item = null;
			return false;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x00010484 File Offset: 0x0000E684
		private void EnqueueUnwrittenItem(ParallelAppender<T>.Item item)
		{
			bool flag = false;
			for (;;)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.unwrittenItems.Count < this.maxItems)
					{
						this.unwrittenItems.Enqueue(item);
						if (this.writerActiveWriters + this.writerCanceledWriters < this.maxWriters)
						{
							this.writerActiveWriters++;
							flag = true;
						}
						break;
					}
					this.unwrittenItemsNotFull.Reset();
				}
				this.unwrittenItemsNotFull.WaitOne();
			}
			if (flag)
			{
				this.threadPool.StartThread(this.writerThreadStart);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00010534 File Offset: 0x0000E734
		protected virtual void DisposeItemValue(T value)
		{
			this.disposeItem(value);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00010542 File Offset: 0x0000E742
		protected virtual void PutItemValue(int index, T value)
		{
			this.putBufferedItem(index, value);
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x00010554 File Offset: 0x0000E754
		private void WriterThread()
		{
			bool flag = false;
			while (!flag)
			{
				object obj = this.syncRoot;
				ParallelAppender<T>.Item item;
				lock (obj)
				{
					if (this.unwrittenItems.Count == 0)
					{
						this.writerActiveWriters--;
						break;
					}
					item = this.unwrittenItems.Dequeue();
					this.unwrittenItemsNotFull.Set();
				}
				flag = item.Canceled;
				if (!flag && item.Exception == null)
				{
					try
					{
						this.PutItemValue(item.Index, item.Value);
					}
					catch (Exception ex)
					{
						using (IHostTrace hostTrace = TracingService.CreateTrace(this.engineHost, "ParallelAppender/WriterThread", TraceEventType.Information, this.resource))
						{
							hostTrace.Add(ex, true);
						}
						item.Exception = ex;
					}
				}
				obj = this.syncRoot;
				lock (obj)
				{
					if (flag)
					{
						this.writerCanceledWriters++;
						this.writerActiveWriters--;
					}
					this.completedItems.Enqueue(item);
					this.completedItemsNotEmpty.Set();
				}
			}
		}

		// Token: 0x040004C5 RID: 1221
		private const int itemToWriterRatio = 2;

		// Token: 0x040004C6 RID: 1222
		private readonly object syncRoot;

		// Token: 0x040004C7 RID: 1223
		private readonly IEngineHost engineHost;

		// Token: 0x040004C8 RID: 1224
		private readonly IThreadPoolService threadPool;

		// Token: 0x040004C9 RID: 1225
		private readonly ThreadStart writerThreadStart;

		// Token: 0x040004CA RID: 1226
		private readonly IResource resource;

		// Token: 0x040004CB RID: 1227
		private readonly Action<int, T> putBufferedItem;

		// Token: 0x040004CC RID: 1228
		private readonly Action<T> disposeItem;

		// Token: 0x040004CD RID: 1229
		private readonly Queue<ParallelAppender<T>.Item> completedItems;

		// Token: 0x040004CE RID: 1230
		private readonly Queue<ParallelAppender<T>.Item> unwrittenItems;

		// Token: 0x040004CF RID: 1231
		private readonly ManualResetEvent completedItemsNotEmpty;

		// Token: 0x040004D0 RID: 1232
		private readonly ManualResetEvent unwrittenItemsNotFull;

		// Token: 0x040004D1 RID: 1233
		private readonly int maxWriters;

		// Token: 0x040004D2 RID: 1234
		private readonly int maxItems;

		// Token: 0x040004D3 RID: 1235
		private int activeWriters;

		// Token: 0x040004D4 RID: 1236
		private int writerActiveWriters;

		// Token: 0x040004D5 RID: 1237
		private int writerCanceledWriters;

		// Token: 0x040004D6 RID: 1238
		private int nextItemIndex;

		// Token: 0x020001BA RID: 442
		protected class Item
		{
			// Token: 0x17000277 RID: 631
			// (get) Token: 0x06000870 RID: 2160 RVA: 0x000106B0 File Offset: 0x0000E8B0
			// (set) Token: 0x06000871 RID: 2161 RVA: 0x000106B8 File Offset: 0x0000E8B8
			public bool Canceled
			{
				get
				{
					return this.canceled;
				}
				set
				{
					this.canceled = value;
				}
			}

			// Token: 0x17000278 RID: 632
			// (get) Token: 0x06000872 RID: 2162 RVA: 0x000106C1 File Offset: 0x0000E8C1
			// (set) Token: 0x06000873 RID: 2163 RVA: 0x000106C9 File Offset: 0x0000E8C9
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

			// Token: 0x17000279 RID: 633
			// (get) Token: 0x06000874 RID: 2164 RVA: 0x000106D2 File Offset: 0x0000E8D2
			// (set) Token: 0x06000875 RID: 2165 RVA: 0x000106DA File Offset: 0x0000E8DA
			public T Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
				}
			}

			// Token: 0x1700027A RID: 634
			// (get) Token: 0x06000876 RID: 2166 RVA: 0x000106E3 File Offset: 0x0000E8E3
			// (set) Token: 0x06000877 RID: 2167 RVA: 0x000106EB File Offset: 0x0000E8EB
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

			// Token: 0x040004D7 RID: 1239
			private bool canceled;

			// Token: 0x040004D8 RID: 1240
			private int index;

			// Token: 0x040004D9 RID: 1241
			private T value;

			// Token: 0x040004DA RID: 1242
			private Exception exception;
		}
	}
}
