using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000B3 RID: 179
	public class BlockServiceTicket : IDisposable
	{
		// Token: 0x06000532 RID: 1330 RVA: 0x00013248 File Offset: 0x00011448
		public object GetService()
		{
			if (this.m_disposed)
			{
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "Trying to get service of a non-valid BlockServiceTicket {0}.", new object[] { this.m_workTicket.Id });
				throw new ObjectDisposedException("BlockServiceTicket " + this.m_workTicket.Id);
			}
			return this.m_publishedService.m_service;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000132B1 File Offset: 0x000114B1
		internal BlockServiceTicket([NotNull] WorkTicket workTicket, [NotNull] PublishedBlockService publishedService, [NotNull] BlockServiceManager manager, object context)
		{
			this.m_workTicket = workTicket;
			this.m_publishedService = publishedService;
			this.m_manager = manager;
			this.m_context = context;
			workTicket.Context = this;
			this.m_disposed = false;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x000132E4 File Offset: 0x000114E4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x000132F0 File Offset: 0x000114F0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (!this.m_disposed)
				{
					this.m_manager.OnBlockServiceTicketDisposed(this);
					this.m_workTicket.Dispose();
					this.m_workTicket = null;
					this.m_disposed = true;
					return;
				}
				TraceSourceBase<ModularizationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Warning, "BlockServiceTicket of id {0} disposed multiple times", new object[] { this.m_workTicket.Id });
			}
		}

		// Token: 0x040001C9 RID: 457
		private WorkTicket m_workTicket;

		// Token: 0x040001CA RID: 458
		private PublishedBlockService m_publishedService;

		// Token: 0x040001CB RID: 459
		private BlockServiceManager m_manager;

		// Token: 0x040001CC RID: 460
		private object m_context;

		// Token: 0x040001CD RID: 461
		private bool m_disposed;
	}
}
