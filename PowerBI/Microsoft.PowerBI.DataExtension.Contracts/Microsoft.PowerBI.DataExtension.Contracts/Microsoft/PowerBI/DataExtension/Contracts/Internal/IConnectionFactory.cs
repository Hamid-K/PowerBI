using System;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000015 RID: 21
	public interface IConnectionFactory
	{
		// Token: 0x06000056 RID: 86
		IDbConnection CreateConnection(string dataExtension, string connectionString);
	}
}
