using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library.ConnectionPool
{
	// Token: 0x02000352 RID: 850
	internal interface IConnectionPoolManager
	{
		// Token: 0x06001C14 RID: 7188
		IDbConnectionPool CreateConnectionPool(global::System.Action onCloseCallBack);

		// Token: 0x06001C15 RID: 7189
		void CloseConnections();
	}
}
