using System;
using System.Data;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002D5 RID: 725
	internal sealed class ChunkConnectionManager
	{
		// Token: 0x060019F5 RID: 6645 RVA: 0x00068B78 File Offset: 0x00066D78
		public ChunkConnectionManager(ConnectionManager connectionManager)
		{
			this.m_connMgr = connectionManager;
			this.m_isConnectionOwner = this.m_connMgr == null;
		}

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x00068B96 File Offset: 0x00066D96
		public SegmentChunkDbInterface DbInterface
		{
			get
			{
				if (this.m_dbInterface == null)
				{
					this.m_dbInterface = new SegmentChunkDbInterface();
					this.m_dbInterface.ConnectionManager = this.ConnectionManager;
				}
				return this.m_dbInterface;
			}
		}

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x00068BC2 File Offset: 0x00066DC2
		public bool IsConnectionOwner
		{
			get
			{
				return this.m_isConnectionOwner;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x00068BCA File Offset: 0x00066DCA
		public ConnectionManager ConnectionManager
		{
			get
			{
				if (this.m_connMgr == null)
				{
					this.m_connMgr = new ConnectionManager(ConnectionTransactionType.Explicit, this.m_isolationLevel);
				}
				return this.m_connMgr;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00068BEC File Offset: 0x00066DEC
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x00068BF4 File Offset: 0x00066DF4
		public IsolationLevel IsolationLevel
		{
			get
			{
				return this.m_isolationLevel;
			}
			set
			{
				RSTrace.CatalogTrace.Assert(this.m_connMgr == null, "m_connMgr -- attempt to change isolation level after manager created");
				if (this.m_connMgr == null)
				{
					this.m_isolationLevel = value;
				}
			}
		}

		// Token: 0x0400096D RID: 2413
		private ConnectionManager m_connMgr;

		// Token: 0x0400096E RID: 2414
		private SegmentChunkDbInterface m_dbInterface;

		// Token: 0x0400096F RID: 2415
		private IsolationLevel m_isolationLevel;

		// Token: 0x04000970 RID: 2416
		private readonly bool m_isConnectionOwner;
	}
}
