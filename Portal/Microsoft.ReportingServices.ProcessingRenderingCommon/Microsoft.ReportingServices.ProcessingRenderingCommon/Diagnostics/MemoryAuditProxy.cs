using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200009B RID: 155
	public abstract class MemoryAuditProxy : IDisposable
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x0000EBCE File Offset: 0x0000CDCE
		protected MemoryAuditProxy()
		{
			MemoryAuditCollection.Register(this);
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		~MemoryAuditProxy()
		{
			this.Dispose(false);
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000EC0C File Offset: 0x0000CE0C
		public virtual void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004CA RID: 1226
		public abstract long CurrentMemoryUsageKBytes { get; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004CB RID: 1227
		public abstract long CurrentFreeableMemoryKBytes { get; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004CC RID: 1228
		public abstract long PendingFreeKBytes { get; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004CD RID: 1229
		public abstract double FreeOverhead { get; }

		// Token: 0x060004CE RID: 1230
		public abstract void SetNewMemoryTarget(long memoryTargetKBytes);

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004CF RID: 1231
		public abstract bool CanPerformPartialRelease { get; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000EC15 File Offset: 0x0000CE15
		// (set) Token: 0x060004D1 RID: 1233 RVA: 0x0000EC1D File Offset: 0x0000CE1D
		internal object CollectionToken
		{
			get
			{
				return this.m_collectionToken;
			}
			set
			{
				this.m_collectionToken = value;
			}
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000EC28 File Offset: 0x0000CE28
		private void Dispose(bool isDisposing)
		{
			try
			{
				if (!this.m_disposed)
				{
					MemoryAuditCollection.UnRegister(this);
					if (isDisposing)
					{
						GC.SuppressFinalize(this);
					}
				}
			}
			finally
			{
				this.m_disposed = true;
			}
		}

		// Token: 0x040002C9 RID: 713
		private bool m_disposed;

		// Token: 0x040002CA RID: 714
		private object m_collectionToken;
	}
}
