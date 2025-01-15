using System;
using System.Data;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200010F RID: 271
	internal class RSServiceStorageAccess : IStorageAccess, IDisposable
	{
		// Token: 0x06000AA9 RID: 2729 RVA: 0x00028707 File Offset: 0x00026907
		public RSServiceStorageAccess(RSService service)
		{
			this.m_service = service;
			this.m_service.WillDisconnectStorage();
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00028721 File Offset: 0x00026921
		public RSServiceStorageAccess(RSService service, IsolationLevel isolationLevel)
			: this(service)
		{
			this.m_service.SetDatabaseConnectionSettings(ConnectionManager.DefaultTransactionType, isolationLevel);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0002873C File Offset: 0x0002693C
		~RSServiceStorageAccess()
		{
			this.Dispose(false);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0002876C File Offset: 0x0002696C
		public void Abort()
		{
			this.m_service.AbortTransaction();
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00028779 File Offset: 0x00026979
		public void Commit()
		{
			this.m_service.DisconnectStorage();
			this.m_hasCommitted = true;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x0002878D File Offset: 0x0002698D
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00028796 File Offset: 0x00026996
		private void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				GC.SuppressFinalize(this);
			}
			if (!this.m_hasCommitted)
			{
				this.m_service.AbortTransaction();
			}
		}

		// Token: 0x0400049D RID: 1181
		private bool m_hasCommitted;

		// Token: 0x0400049E RID: 1182
		private readonly RSService m_service;
	}
}
