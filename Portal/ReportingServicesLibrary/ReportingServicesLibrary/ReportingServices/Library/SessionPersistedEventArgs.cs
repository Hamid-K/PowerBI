using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000281 RID: 641
	internal sealed class SessionPersistedEventArgs : EventArgs
	{
		// Token: 0x06001739 RID: 5945 RVA: 0x0005DB24 File Offset: 0x0005BD24
		public SessionPersistedEventArgs(ConnectionManager connectionManager)
		{
			RSTrace.CatalogTrace.Assert(connectionManager != null);
			this.m_connectionManager = connectionManager;
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x0005DB41 File Offset: 0x0005BD41
		public ConnectionManager ConnectionManager
		{
			get
			{
				return this.m_connectionManager;
			}
		}

		// Token: 0x04000871 RID: 2161
		private readonly ConnectionManager m_connectionManager;
	}
}
