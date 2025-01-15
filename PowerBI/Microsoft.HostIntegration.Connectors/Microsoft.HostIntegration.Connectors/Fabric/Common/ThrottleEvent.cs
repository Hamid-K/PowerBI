using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000420 RID: 1056
	internal sealed class ThrottleEvent
	{
		// Token: 0x060024CB RID: 9419 RVA: 0x00070ABC File Offset: 0x0006ECBC
		public ThrottleEvent(int maximumThroughput)
		{
			this.m_maximumThroughput = maximumThroughput;
			this.m_availableThroughput = this.m_maximumThroughput;
			this.m_sentinel = new ListContext(new object());
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x00070AE7 File Offset: 0x0006ECE7
		private ListContext Head
		{
			get
			{
				return this.m_sentinel.NextContext;
			}
		}

		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x060024CD RID: 9421 RVA: 0x00070AF4 File Offset: 0x0006ECF4
		private ListContext Tail
		{
			get
			{
				return this.m_sentinel.PrevContext;
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x060024CE RID: 9422 RVA: 0x00070B01 File Offset: 0x0006ED01
		private bool IsEmpty
		{
			get
			{
				return this.m_sentinel.NextContext == this.m_sentinel;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x060024CF RID: 9423 RVA: 0x00070B16 File Offset: 0x0006ED16
		private bool IsClosed
		{
			get
			{
				return !this.m_sentinel.IsValid;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x060024D0 RID: 9424 RVA: 0x00070B26 File Offset: 0x0006ED26
		// (set) Token: 0x060024D1 RID: 9425 RVA: 0x00070B30 File Offset: 0x0006ED30
		public int MaximumThroughput
		{
			get
			{
				return this.m_maximumThroughput;
			}
			set
			{
				int num;
				lock (this.m_sentinel.ListLock)
				{
					num = value - this.m_maximumThroughput;
					this.m_maximumThroughput = value;
					if (num < 0)
					{
						this.m_availableThroughput += num;
						return;
					}
				}
				for (int i = 0; i < num; i++)
				{
					this.Release();
				}
			}
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x00070BA0 File Offset: 0x0006EDA0
		public void Close()
		{
			if (this.IsClosed)
			{
				return;
			}
			ListContext listContext;
			lock (this.m_sentinel.ListLock)
			{
				if (this.IsClosed)
				{
					return;
				}
				listContext = this.Head;
				this.m_sentinel.Invalidate();
				goto IL_0060;
			}
			IL_0040:
			ListContext nextContext = listContext.NextContext;
			listContext.LinkToSelf();
			listContext.Delinked(false, new ObjectDisposedException("ThrottleEvent"));
			listContext = nextContext;
			IL_0060:
			if (listContext != this.m_sentinel)
			{
				goto IL_0040;
			}
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x00070C28 File Offset: 0x0006EE28
		public void Acquire()
		{
			this.Acquire(FileTime.MaxValue);
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x00070C35 File Offset: 0x0006EE35
		public void Acquire(TimeSpan timeout)
		{
			this.Acquire(FileTime.FromTimeSpan(timeout));
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x00070C44 File Offset: 0x0006EE44
		public void Acquire(FileTime timeoutAt)
		{
			IAsyncResult asyncResult = this.BeginAcquire(null, null, timeoutAt);
			ThrottleEvent.EndAcquire(asyncResult);
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x00070C61 File Offset: 0x0006EE61
		public IAsyncResult BeginAcquire(AsyncCallback callback, object state)
		{
			return this.BeginAcquire(callback, state, FileTime.MaxValue);
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x00070C70 File Offset: 0x0006EE70
		public IAsyncResult BeginAcquire(AsyncCallback callback, object state, TimeSpan timeout)
		{
			return this.BeginAcquire(callback, state, FileTime.FromTimeSpan(timeout));
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x00070C80 File Offset: 0x0006EE80
		public IAsyncResult BeginAcquire(AsyncCallback callback, object state, FileTime timeoutAt)
		{
			ListContext listContext = new ListContext(callback, state, timeoutAt);
			bool flag = false;
			bool flag2 = false;
			lock (this.m_sentinel.ListLock)
			{
				if (this.m_availableThroughput > 0)
				{
					this.m_availableThroughput--;
					flag = true;
				}
				else if (!this.IsClosed)
				{
					this.Tail.InsertAfter(listContext);
					flag2 = true;
				}
			}
			if (flag2)
			{
				listContext.StartListTimer();
			}
			else
			{
				Exception ex;
				if (flag)
				{
					ex = null;
				}
				else
				{
					ex = new ObjectDisposedException("ThrottleEvent");
				}
				listContext.Delinked(true, ex);
			}
			return listContext;
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x00070D20 File Offset: 0x0006EF20
		public static void EndAcquire(IAsyncResult ar)
		{
			ListContext listContext = ar as ListContext;
			if (listContext == null)
			{
				throw new ArgumentException("Invalid type of IAsyncResult", "ar");
			}
			listContext.End();
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x00070D50 File Offset: 0x0006EF50
		public void AbortAcquire(IAsyncResult ar)
		{
			ListContext listContext = ar as ListContext;
			if (listContext == null)
			{
				throw new ArgumentException("Invalid type of IAsyncResult", "ar");
			}
			bool flag = false;
			if (listContext.IsLinked)
			{
				lock (this.m_sentinel.ListLock)
				{
					if (listContext.IsLinked)
					{
						flag = true;
						listContext.Delink();
					}
				}
			}
			if (flag)
			{
				listContext.Delinked(false, new OperationContextAbortedException());
			}
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x00070DCC File Offset: 0x0006EFCC
		public void Release()
		{
			if (this.IsClosed)
			{
				throw new ObjectDisposedException("ThrottleEvent");
			}
			ListContext head;
			lock (this.m_sentinel.ListLock)
			{
				if (this.m_availableThroughput < 0)
				{
					this.m_availableThroughput++;
					return;
				}
				if (this.IsEmpty)
				{
					this.m_availableThroughput++;
					return;
				}
				head = this.Head;
				head.Delink();
			}
			head.Delinked(false, null);
		}

		// Token: 0x0400167E RID: 5758
		private ListContext m_sentinel;

		// Token: 0x0400167F RID: 5759
		private int m_maximumThroughput;

		// Token: 0x04001680 RID: 5760
		private int m_availableThroughput;
	}
}
