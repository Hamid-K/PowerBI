using System;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200009A RID: 154
	internal sealed class RSParameterTranslator : IParametersTranslator
	{
		// Token: 0x0600062B RID: 1579 RVA: 0x00019E2D File Offset: 0x0001802D
		public RSParameterTranslator()
			: this(null)
		{
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00019E36 File Offset: 0x00018036
		internal RSParameterTranslator(IRSStorage storage)
		{
			if (storage != null && storage.ConnectionManager != null)
			{
				this.m_connMgr = storage.ConnectionManager;
				this.m_ownConnection = false;
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00019E64 File Offset: 0x00018064
		public void GetParamsInstance(string paramsInstanceId, out ExternalItemPath itemPath, out NameValueCollection parameters)
		{
			ServerParameterStore serverParameterStore = this.InitStoreConnection(false);
			try
			{
				serverParameterStore.Get(paramsInstanceId, out itemPath, out parameters);
			}
			finally
			{
				this.CloseStoreConnection();
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00019E9C File Offset: 0x0001809C
		internal NameValueCollection StoreReportParameters(ExternalItemPath itemPath, NameValueCollection paramsInstance, out bool replaced)
		{
			ServerParameterStore serverParameterStore = this.InitStoreConnection(true);
			NameValueCollection nameValueCollection2;
			try
			{
				NameValueCollection nameValueCollection = serverParameterStore.Store(itemPath, paramsInstance, null, out replaced);
				if (this.m_ownConnection)
				{
					this.m_connMgr.CommitTransaction();
				}
				nameValueCollection2 = nameValueCollection;
			}
			catch (Exception ex)
			{
				if (this.m_ownConnection)
				{
					if (RSTrace.CatalogTrace.TraceError)
					{
						RSTrace.CatalogTrace.Trace(TraceLevel.Error, "Error when trying to StoreReportParameters. " + ex.ToString());
					}
					this.m_connMgr.AbortTransaction();
				}
				throw;
			}
			finally
			{
				this.CloseStoreConnection();
			}
			return nameValueCollection2;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00019F34 File Offset: 0x00018134
		private ServerParameterStore InitStoreConnection(bool explictCommit)
		{
			if (this.m_ownConnection)
			{
				RSTrace.CatalogTrace.Assert(this.m_connMgr == null);
				if (explictCommit)
				{
					this.m_connMgr = new ConnectionManager();
				}
				else
				{
					this.m_connMgr = new ConnectionManager(ConnectionTransactionType.AutoCommit, IsolationLevel.ReadCommitted);
				}
				this.m_connMgr.WillDisconnectStorage();
			}
			return new ServerParameterStore(this.m_connMgr);
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00019F93 File Offset: 0x00018193
		private void CloseStoreConnection()
		{
			if (this.m_ownConnection)
			{
				ConnectionManager connMgr = this.m_connMgr;
				this.m_connMgr = null;
				connMgr.DisconnectStorage();
			}
		}

		// Token: 0x0400034C RID: 844
		private ConnectionManager m_connMgr;

		// Token: 0x0400034D RID: 845
		private readonly bool m_ownConnection = true;
	}
}
