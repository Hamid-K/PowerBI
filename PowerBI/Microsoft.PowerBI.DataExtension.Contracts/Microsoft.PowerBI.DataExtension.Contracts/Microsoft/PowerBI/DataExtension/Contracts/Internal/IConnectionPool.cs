using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000016 RID: 22
	public interface IConnectionPool
	{
		// Token: 0x06000057 RID: 87
		IDbConnection Get(IDataSourceInfo dataSourceInfo);

		// Token: 0x06000058 RID: 88
		bool Put(IDbConnection connection, IDataSourceInfo dataSourceInfo);
	}
}
