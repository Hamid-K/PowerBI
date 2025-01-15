using System;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003EA RID: 1002
	internal class ListContext : OperationContext
	{
		// Token: 0x06002337 RID: 9015 RVA: 0x0006C407 File Offset: 0x0006A607
		public ListContext(object listLock)
			: base(null, null, FileTime.MaxValue)
		{
			this.m_listLock = listLock;
			this.LinkToSelf();
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0006C423 File Offset: 0x0006A623
		public ListContext(AsyncCallback callback, object state, FileTime fileTime)
			: base(callback, state, fileTime)
		{
			this.m_listLock = null;
			this.LinkToSelf();
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06002339 RID: 9017 RVA: 0x0006C43B File Offset: 0x0006A63B
		public ListContext NextContext
		{
			get
			{
				return this.m_nextContext;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x0600233A RID: 9018 RVA: 0x0006C443 File Offset: 0x0006A643
		public ListContext PrevContext
		{
			get
			{
				return this.m_prevContext;
			}
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0006C44C File Offset: 0x0006A64C
		public void Invalidate()
		{
			this.m_nextContext = (this.m_prevContext = null);
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0006C46C File Offset: 0x0006A66C
		public void LinkToSelf()
		{
			this.m_prevContext = this;
			this.m_nextContext = this;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x0006C48C File Offset: 0x0006A68C
		public void Delink()
		{
			ReleaseAssert.IsTrue(this.IsLinked);
			ReleaseAssert.IsTrue(this.m_listLock != null);
			this.m_nextContext.m_prevContext = this.m_prevContext;
			this.m_prevContext.m_nextContext = this.m_nextContext;
			this.LinkToSelf();
		}

		// Token: 0x0600233E RID: 9022 RVA: 0x0006C4E0 File Offset: 0x0006A6E0
		public void InsertAfter(ListContext context)
		{
			ReleaseAssert.IsTrue(context != null && !context.IsLinked);
			ReleaseAssert.IsTrue(this.m_listLock != null);
			context.m_nextContext = this.m_nextContext;
			context.m_prevContext = this;
			context.m_listLock = this.m_listLock;
			this.m_nextContext.m_prevContext = context;
			this.m_nextContext = context;
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x0006C544 File Offset: 0x0006A744
		public void InsertBefore(ListContext context)
		{
			ReleaseAssert.IsTrue(context != null && !context.IsLinked);
			ReleaseAssert.IsTrue(this.m_listLock != null);
			context.m_nextContext = this;
			context.m_prevContext = this.m_prevContext;
			context.m_listLock = this.m_listLock;
			this.m_prevContext.m_nextContext = context;
			this.m_prevContext = context;
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06002340 RID: 9024 RVA: 0x0006C5A8 File Offset: 0x0006A7A8
		public bool IsValid
		{
			get
			{
				return this.m_nextContext != null || this.m_prevContext != null;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06002341 RID: 9025 RVA: 0x0006C5C0 File Offset: 0x0006A7C0
		public bool IsLinked
		{
			get
			{
				return this.m_nextContext != this && this.m_prevContext != this;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06002342 RID: 9026 RVA: 0x0006C5D9 File Offset: 0x0006A7D9
		public object ListLock
		{
			get
			{
				return this.m_listLock;
			}
		}

		// Token: 0x06002343 RID: 9027 RVA: 0x0006C5E1 File Offset: 0x0006A7E1
		public void StartListTimer()
		{
			base.StartTimer();
		}

		// Token: 0x06002344 RID: 9028 RVA: 0x0006BEE2 File Offset: 0x0006A0E2
		public void Delinked(bool completedSynchronously, Exception exception)
		{
			base.OperationCompleted(completedSynchronously, exception);
		}

		// Token: 0x06002345 RID: 9029 RVA: 0x0006C5EC File Offset: 0x0006A7EC
		protected override void OnOperationComplete(bool completedSynchronously)
		{
			ReleaseAssert.IsTrue(this.IsValid);
			if (this.IsLinked)
			{
				lock (this.m_listLock)
				{
					if (this.IsLinked)
					{
						this.Delink();
					}
					else
					{
						this.InvisibleTimerCompletion();
					}
				}
			}
			this.m_listLock = null;
			base.OnOperationComplete(completedSynchronously);
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x0006C658 File Offset: 0x0006A858
		protected void InvisibleTimerCompletion()
		{
			if (base.Fault is TimeoutException)
			{
				base.Fault = null;
			}
		}

		// Token: 0x04001604 RID: 5636
		private object m_listLock;

		// Token: 0x04001605 RID: 5637
		private ListContext m_nextContext;

		// Token: 0x04001606 RID: 5638
		private ListContext m_prevContext;
	}
}
