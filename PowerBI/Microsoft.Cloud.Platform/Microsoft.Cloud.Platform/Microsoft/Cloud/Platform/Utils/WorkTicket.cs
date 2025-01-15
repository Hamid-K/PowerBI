using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002F4 RID: 756
	public class WorkTicket : IDisposable
	{
		// Token: 0x060013F6 RID: 5110 RVA: 0x000452E8 File Offset: 0x000434E8
		public bool IsValid()
		{
			return this.m_manager != null && this.m_id != 0L && !this.m_disposed;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x00045305 File Offset: 0x00043505
		public virtual void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x00045310 File Offset: 0x00043510
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.IsValid())
				{
					this.m_manager.OnWorkTicketDisposed(this);
					if (this.m_creationCallStack != null)
					{
						this.m_creationCallStack.Dispose();
						this.m_creationCallStack = null;
					}
					this.m_disposed = true;
					return;
				}
				if (this.m_disposed)
				{
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Work ticket '{0}.{1}' disposed more than once", new object[]
					{
						this.m_manager.Name,
						this.m_id
					});
				}
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060013F9 RID: 5113 RVA: 0x00045390 File Offset: 0x00043590
		public long Id
		{
			get
			{
				if (!this.IsValid())
				{
					throw new ObjectDisposedException("WorkTicket " + this.m_id);
				}
				return this.m_id;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060013FA RID: 5114 RVA: 0x000453BB File Offset: 0x000435BB
		public IIdentifiable OwningEntity
		{
			get
			{
				if (!this.IsValid())
				{
					throw new ObjectDisposedException("WorkTicket " + this.m_id);
				}
				return this.m_owningEntity;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060013FB RID: 5115 RVA: 0x000453E6 File Offset: 0x000435E6
		// (set) Token: 0x060013FC RID: 5116 RVA: 0x00045411 File Offset: 0x00043611
		public object Context
		{
			get
			{
				if (!this.IsValid())
				{
					throw new ObjectDisposedException("WorkTicket " + this.m_id);
				}
				return this.m_context;
			}
			set
			{
				if (!this.IsValid())
				{
					throw new ObjectDisposedException("WorkTicket " + this.m_id);
				}
				this.m_context = value;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060013FD RID: 5117 RVA: 0x00045440 File Offset: 0x00043640
		public virtual string DisplayName
		{
			get
			{
				if (!this.IsValid())
				{
					throw new ObjectDisposedException("WorkTicket " + this.m_id);
				}
				return this.m_manager.Name + ":" + this.m_id;
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x00045490 File Offset: 0x00043690
		public void Dump(TraceDump dumper)
		{
			dumper.Add("WorkTicketManager=" + this.m_manager.Name);
			dumper.Add("Id=" + this.m_id);
			if (this.m_owningEntity != null)
			{
				dumper.Add("OwningEntity=" + this.m_owningEntity.Name);
			}
			dumper.Add("Tracked=" + this.m_tracked.ToString());
			dumper.Add("DetectLeaks=" + this.m_detectLeaks.ToString());
			if (this.m_creationCallStack != null)
			{
				this.m_creationCallStack.Dump(dumper);
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060013FF RID: 5119 RVA: 0x00045540 File Offset: 0x00043740
		internal bool Tracked
		{
			get
			{
				if (!this.IsValid())
				{
					throw new ObjectDisposedException("WorkTicket " + this.m_id);
				}
				return this.m_tracked;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001400 RID: 5120 RVA: 0x0004556B File Offset: 0x0004376B
		internal CallStackRef CreationCallStack
		{
			get
			{
				return this.m_creationCallStack;
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x00045574 File Offset: 0x00043774
		internal void Init(WorkTicketManager workTicketManager, long workTicketId, IIdentifiable owner, bool isTracked, bool detectLeaks, bool captureStack)
		{
			this.m_manager = workTicketManager;
			this.m_disposed = false;
			this.m_id = workTicketId;
			this.m_owningEntity = owner;
			this.m_tracked = isTracked;
			this.m_detectLeaks = detectLeaks;
			if (captureStack)
			{
				this.m_creationCallStack = CallStackRef.Capture(1, true);
			}
			this.OnWorkTicketInitialized();
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00009B3B File Offset: 0x00007D3B
		protected virtual void OnWorkTicketInitialized()
		{
		}

		// Token: 0x0400078D RID: 1933
		private WorkTicketManager m_manager;

		// Token: 0x0400078E RID: 1934
		private long m_id;

		// Token: 0x0400078F RID: 1935
		private IIdentifiable m_owningEntity;

		// Token: 0x04000790 RID: 1936
		private bool m_disposed;

		// Token: 0x04000791 RID: 1937
		private object m_context;

		// Token: 0x04000792 RID: 1938
		private bool m_tracked;

		// Token: 0x04000793 RID: 1939
		private bool m_detectLeaks;

		// Token: 0x04000794 RID: 1940
		private CallStackRef m_creationCallStack;
	}
}
