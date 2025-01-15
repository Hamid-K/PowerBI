using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000411 RID: 1041
	internal sealed class ReaderQueue<T>
	{
		// Token: 0x06002423 RID: 9251 RVA: 0x0006EEBF File Offset: 0x0006D0BF
		public ReaderQueue()
		{
			this.m_highPriorityQueue = new Queue<T>();
			this.m_normalPriorityQueue = new Queue<T>();
			this.m_lowPriorityQueue = new Queue<T>();
			this.m_sentinel = new ListContext(new object());
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x0006EEF8 File Offset: 0x0006D0F8
		private ListContext Head
		{
			get
			{
				return this.m_sentinel.NextContext;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x0006EF05 File Offset: 0x0006D105
		private ListContext Tail
		{
			get
			{
				return this.m_sentinel.PrevContext;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x0006EF12 File Offset: 0x0006D112
		private bool IsEmpty
		{
			get
			{
				return this.m_sentinel.NextContext == this.m_sentinel;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06002427 RID: 9255 RVA: 0x0006EF27 File Offset: 0x0006D127
		private bool IsClosed
		{
			get
			{
				return !this.m_sentinel.IsValid;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x0006EF38 File Offset: 0x0006D138
		public int Count
		{
			get
			{
				int num;
				lock (this.m_sentinel.ListLock)
				{
					num = this.m_highPriorityQueue.Count + this.m_normalPriorityQueue.Count + this.m_lowPriorityQueue.Count;
				}
				return num;
			}
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x0006EF98 File Offset: 0x0006D198
		public void Abort()
		{
			this.PrivateClose(false);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x0006EFA2 File Offset: 0x0006D1A2
		public void Close()
		{
			this.PrivateClose(true);
		}

		// Token: 0x0600242B RID: 9259 RVA: 0x0006EFAC File Offset: 0x0006D1AC
		private bool PrivateClose(bool isClose)
		{
			if (this.IsClosed)
			{
				return false;
			}
			ListContext listContext;
			lock (this.m_sentinel.ListLock)
			{
				if (this.IsClosed)
				{
					return false;
				}
				listContext = this.Head;
				this.m_sentinel.Invalidate();
				goto IL_0076;
			}
			IL_0043:
			ReaderQueue<T>.ReaderContext readerContext = (ReaderQueue<T>.ReaderContext)listContext;
			listContext = listContext.NextContext;
			if (isClose)
			{
				readerContext.Close();
				readerContext.Delinked(false, null);
			}
			else
			{
				readerContext.Abort();
				readerContext.Delinked(false, new OperationContextAbortedException());
			}
			IL_0076:
			if (listContext == this.m_sentinel)
			{
				ReaderQueue<T>.DisposeQueue(this.m_highPriorityQueue);
				ReaderQueue<T>.DisposeQueue(this.m_normalPriorityQueue);
				ReaderQueue<T>.DisposeQueue(this.m_lowPriorityQueue);
				return true;
			}
			goto IL_0043;
		}

		// Token: 0x0600242C RID: 9260 RVA: 0x0006F06C File Offset: 0x0006D26C
		private static void DisposeQueue(Queue<T> availableItems)
		{
			while (availableItems.Count > 0)
			{
				T t = availableItems.Dequeue();
				IDisposable disposable = t as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x0600242D RID: 9261 RVA: 0x0006F0A0 File Offset: 0x0006D2A0
		public T Dequeue()
		{
			return this.Dequeue(FileTime.MaxValue);
		}

		// Token: 0x0600242E RID: 9262 RVA: 0x0006F0AD File Offset: 0x0006D2AD
		public T Dequeue(TimeSpan timeout)
		{
			return this.Dequeue(FileTime.FromTimeSpan(timeout));
		}

		// Token: 0x0600242F RID: 9263 RVA: 0x0006F0BC File Offset: 0x0006D2BC
		public T Dequeue(FileTime timeoutAt)
		{
			IAsyncResult asyncResult = this.BeginDequeue(null, null, timeoutAt);
			return this.EndDequeue(asyncResult);
		}

		// Token: 0x06002430 RID: 9264 RVA: 0x0006F0DA File Offset: 0x0006D2DA
		public IAsyncResult BeginDequeue(AsyncCallback callback, object state)
		{
			return this.BeginDequeue(callback, state, FileTime.MaxValue);
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x0006F0E9 File Offset: 0x0006D2E9
		public IAsyncResult BeginDequeue(AsyncCallback callback, object state, TimeSpan timeout)
		{
			return this.BeginDequeue(callback, state, FileTime.FromTimeSpan(timeout));
		}

		// Token: 0x06002432 RID: 9266 RVA: 0x0006F0FC File Offset: 0x0006D2FC
		public IAsyncResult BeginDequeue(AsyncCallback callback, object state, FileTime timeoutAt)
		{
			ReaderQueue<T>.ReaderContext readerContext = new ReaderQueue<T>.ReaderContext(callback, state, timeoutAt, ReaderQueue<T>.ContextType.ItemStore);
			return this.BeginDequeue(readerContext);
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x0006F11A File Offset: 0x0006D31A
		public bool TryDequeue(TimeSpan timeout, out T item)
		{
			return this.TryDequeue(FileTime.FromTimeSpan(timeout), out item);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x0006F12C File Offset: 0x0006D32C
		public bool TryDequeue(FileTime timeoutAt, out T item)
		{
			IAsyncResult asyncResult = this.BeginTryDequeue(null, null, timeoutAt);
			return this.EndTryDequeue(asyncResult, out item);
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x0006F14B File Offset: 0x0006D34B
		public IAsyncResult BeginTryDequeue(AsyncCallback callback, object state, TimeSpan timeout)
		{
			return this.BeginTryDequeue(callback, state, FileTime.FromTimeSpan(timeout));
		}

		// Token: 0x06002436 RID: 9270 RVA: 0x0006F15C File Offset: 0x0006D35C
		public IAsyncResult BeginTryDequeue(AsyncCallback callback, object state, FileTime timeoutAt)
		{
			ReaderQueue<T>.ReaderContext readerContext = new ReaderQueue<T>.ReaderContext(callback, state, timeoutAt, ReaderQueue<T>.ContextType.Combined);
			return this.BeginDequeue(readerContext);
		}

		// Token: 0x06002437 RID: 9271 RVA: 0x0006F17A File Offset: 0x0006D37A
		public bool Wait(TimeSpan timeout)
		{
			return this.Wait(FileTime.FromTimeSpan(timeout));
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x0006F188 File Offset: 0x0006D388
		public bool Wait(FileTime timeoutAt)
		{
			IAsyncResult asyncResult = this.BeginWait(null, null, timeoutAt);
			return this.EndWait(asyncResult);
		}

		// Token: 0x06002439 RID: 9273 RVA: 0x0006F1A6 File Offset: 0x0006D3A6
		public IAsyncResult BeginWait(AsyncCallback callback, object state, TimeSpan timeout)
		{
			return this.BeginWait(callback, state, FileTime.FromTimeSpan(timeout));
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x0006F1B8 File Offset: 0x0006D3B8
		public IAsyncResult BeginWait(AsyncCallback callback, object state, FileTime timeoutAt)
		{
			ReaderQueue<T>.ReaderContext readerContext = new ReaderQueue<T>.ReaderContext(callback, state, timeoutAt, ReaderQueue<T>.ContextType.Waiting);
			return this.BeginDequeue(readerContext);
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x0006F1D8 File Offset: 0x0006D3D8
		private IAsyncResult BeginDequeue(ReaderQueue<T>.ReaderContext rc)
		{
			T t = default(T);
			bool flag = false;
			bool flag2 = false;
			Priority priority = Priority.NormalPriority;
			lock (this.m_sentinel.ListLock)
			{
				if (!this.IsClosed)
				{
					Queue<T> queue = null;
					if (this.m_highPriorityQueue.Count > 0)
					{
						queue = this.m_highPriorityQueue;
						priority = Priority.HighPriority;
					}
					else if (this.m_normalPriorityQueue.Count > 0)
					{
						queue = this.m_normalPriorityQueue;
						priority = Priority.NormalPriority;
					}
					else if (this.m_lowPriorityQueue.Count > 0)
					{
						queue = this.m_lowPriorityQueue;
						priority = Priority.LowPriority;
					}
					if (queue != null)
					{
						if (rc.IsItemStore)
						{
							t = queue.Dequeue();
						}
						flag2 = true;
					}
					else
					{
						this.Tail.InsertAfter(rc);
						flag = true;
					}
				}
			}
			if (flag)
			{
				rc.StartListTimer();
			}
			else
			{
				if (flag2)
				{
					rc.SetResult(t, priority);
				}
				else
				{
					rc.Close();
				}
				rc.Delinked(true, null);
			}
			return rc;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x0006F2D8 File Offset: 0x0006D4D8
		public T EndDequeue(IAsyncResult ar)
		{
			ReaderQueue<T>.ReaderContext readerContext = ar as ReaderQueue<T>.ReaderContext;
			if (readerContext == null)
			{
				throw new ArgumentException("Invalid type of IAsyncResult", "ar");
			}
			readerContext.End();
			return readerContext.Result;
		}

		// Token: 0x0600243D RID: 9277 RVA: 0x0006F30C File Offset: 0x0006D50C
		public bool EndTryDequeue(IAsyncResult ar, out T item)
		{
			ReaderQueue<T>.ReaderContext readerContext = ar as ReaderQueue<T>.ReaderContext;
			if (readerContext == null)
			{
				throw new ArgumentException("Invalid type of IAsyncResult", "ar");
			}
			readerContext.End();
			item = readerContext.Result;
			return readerContext.HasResult;
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x0006F34C File Offset: 0x0006D54C
		public bool EndWait(IAsyncResult ar)
		{
			ReaderQueue<T>.ReaderContext readerContext = ar as ReaderQueue<T>.ReaderContext;
			if (readerContext == null)
			{
				throw new ArgumentException("Invalid type of IAsyncResult", "ar");
			}
			readerContext.End();
			return readerContext.HasResult;
		}

		// Token: 0x0600243F RID: 9279 RVA: 0x0006F380 File Offset: 0x0006D580
		public void AbortDequeue(IAsyncResult ar)
		{
			ReaderQueue<T>.ReaderContext readerContext = ar as ReaderQueue<T>.ReaderContext;
			if (readerContext == null)
			{
				throw new ArgumentException("Invalid type of IAsyncResult", "ar");
			}
			bool flag = false;
			if (readerContext.IsLinked)
			{
				lock (this.m_sentinel.ListLock)
				{
					if (readerContext.IsLinked)
					{
						flag = true;
						readerContext.Delink();
					}
				}
			}
			if (flag)
			{
				readerContext.Delinked(false, new OperationContextAbortedException());
			}
		}

		// Token: 0x06002440 RID: 9280 RVA: 0x0006F3FC File Offset: 0x0006D5FC
		public void Enqueue(T item)
		{
			this.Enqueue(item, true);
		}

		// Token: 0x06002441 RID: 9281 RVA: 0x0006F408 File Offset: 0x0006D608
		public void Enqueue(T item, bool useCurrentThread)
		{
			List<ReaderQueue<T>.ReaderContext> list = new List<ReaderQueue<T>.ReaderContext>();
			Priority priority = Priority.GetPriority(Thread.CurrentThread);
			lock (this.m_sentinel.ListLock)
			{
				if (this.IsClosed)
				{
					throw new ObjectDisposedException("ReaderQueue", "Queue is closed");
				}
				while (!this.IsEmpty)
				{
					ReaderQueue<T>.ReaderContext readerContext = (ReaderQueue<T>.ReaderContext)this.Head;
					if (readerContext.IsItemStore)
					{
						readerContext.Delink(item, priority);
						list.Add(readerContext);
						goto IL_00BC;
					}
					readerContext.Delink();
					list.Add(readerContext);
				}
				Queue<T> queue;
				if (priority == Priority.NormalPriority)
				{
					queue = this.m_normalPriorityQueue;
				}
				else if (priority == Priority.HighPriority)
				{
					queue = this.m_highPriorityQueue;
				}
				else
				{
					queue = this.m_lowPriorityQueue;
				}
				queue.Enqueue(item);
			}
			IL_00BC:
			if (list.Count > 0)
			{
				if (useCurrentThread)
				{
					this.ActivateContext(list);
					return;
				}
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.ActivateContext), list);
			}
		}

		// Token: 0x06002442 RID: 9282 RVA: 0x0006F508 File Offset: 0x0006D708
		private void ActivateContext(object state)
		{
			List<ReaderQueue<T>.ReaderContext> list = (List<ReaderQueue<T>.ReaderContext>)state;
			foreach (ReaderQueue<T>.ReaderContext readerContext in list)
			{
				readerContext.Delinked(false, null);
			}
		}

		// Token: 0x04001657 RID: 5719
		private ListContext m_sentinel;

		// Token: 0x04001658 RID: 5720
		private Queue<T> m_highPriorityQueue;

		// Token: 0x04001659 RID: 5721
		private Queue<T> m_normalPriorityQueue;

		// Token: 0x0400165A RID: 5722
		private Queue<T> m_lowPriorityQueue;

		// Token: 0x02000412 RID: 1042
		[Flags]
		private enum ContextType
		{
			// Token: 0x0400165C RID: 5724
			ItemStore = 1,
			// Token: 0x0400165D RID: 5725
			Waiting = 2,
			// Token: 0x0400165E RID: 5726
			Combined = 3
		}

		// Token: 0x02000413 RID: 1043
		private class ReaderContext : ListContext
		{
			// Token: 0x06002443 RID: 9283 RVA: 0x0006F560 File Offset: 0x0006D760
			public ReaderContext(AsyncCallback callback, object state, FileTime timeoutAt, ReaderQueue<T>.ContextType contextType)
				: base(callback, state, timeoutAt)
			{
				this._result = default(T);
				this._hasResult = false;
				this._contextType = contextType;
			}

			// Token: 0x1700073A RID: 1850
			// (get) Token: 0x06002444 RID: 9284 RVA: 0x0006F586 File Offset: 0x0006D786
			public bool HasResult
			{
				get
				{
					return this._hasResult;
				}
			}

			// Token: 0x1700073B RID: 1851
			// (get) Token: 0x06002445 RID: 9285 RVA: 0x0006F58E File Offset: 0x0006D78E
			public T Result
			{
				get
				{
					ReleaseAssert.IsTrue(this._hasResult);
					return this._result;
				}
			}

			// Token: 0x06002446 RID: 9286 RVA: 0x0006F5A1 File Offset: 0x0006D7A1
			internal void SetResult(T item, Priority operationPriority)
			{
				ReleaseAssert.IsTrue(!this._hasResult);
				base.OperationPriority = operationPriority;
				this._result = item;
				this._hasResult = true;
			}

			// Token: 0x06002447 RID: 9287 RVA: 0x0006F5C6 File Offset: 0x0006D7C6
			internal void Delink(T item, Priority operationPriority)
			{
				this.SetResult(item, operationPriority);
				base.Delink();
			}

			// Token: 0x06002448 RID: 9288 RVA: 0x0006F5D8 File Offset: 0x0006D7D8
			internal void Close()
			{
				this.SetResult(default(T), Priority.NormalPriority);
				base.LinkToSelf();
			}

			// Token: 0x06002449 RID: 9289 RVA: 0x0006F5FF File Offset: 0x0006D7FF
			internal void Abort()
			{
				base.LinkToSelf();
			}

			// Token: 0x1700073C RID: 1852
			// (get) Token: 0x0600244A RID: 9290 RVA: 0x0006F607 File Offset: 0x0006D807
			public bool IsWaiting
			{
				get
				{
					return (this._contextType & ReaderQueue<T>.ContextType.Waiting) != (ReaderQueue<T>.ContextType)0;
				}
			}

			// Token: 0x1700073D RID: 1853
			// (get) Token: 0x0600244B RID: 9291 RVA: 0x0006F617 File Offset: 0x0006D817
			public bool IsItemStore
			{
				get
				{
					return (this._contextType & ReaderQueue<T>.ContextType.ItemStore) != (ReaderQueue<T>.ContextType)0;
				}
			}

			// Token: 0x0600244C RID: 9292 RVA: 0x0006F627 File Offset: 0x0006D827
			protected override void OnOperationComplete(bool completedSynchronously)
			{
				if (this.IsWaiting)
				{
					base.InvisibleTimerCompletion();
				}
				base.OnOperationComplete(completedSynchronously);
			}

			// Token: 0x0400165F RID: 5727
			private T _result;

			// Token: 0x04001660 RID: 5728
			private bool _hasResult;

			// Token: 0x04001661 RID: 5729
			private ReaderQueue<T>.ContextType _contextType;
		}
	}
}
