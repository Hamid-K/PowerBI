using System;
using Microsoft.InfoNav.Experimental.Insights.ServiceContracts.Internal;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;

namespace Microsoft.PowerBI.ExploreHost.Insights
{
	// Token: 0x0200007B RID: 123
	internal sealed class DbConnectionWrapper : IDbConnectionWrapper
	{
		// Token: 0x06000357 RID: 855 RVA: 0x0000AB8D File Offset: 0x00008D8D
		internal DbConnectionWrapper(IDbConnection dbConnection)
		{
			this.m_dbConnection = dbConnection;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000358 RID: 856 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public IDbConnection Connection
		{
			get
			{
				return this.m_dbConnection;
			}
		}

		// Token: 0x0400017F RID: 383
		private readonly IDbConnection m_dbConnection;
	}
}
