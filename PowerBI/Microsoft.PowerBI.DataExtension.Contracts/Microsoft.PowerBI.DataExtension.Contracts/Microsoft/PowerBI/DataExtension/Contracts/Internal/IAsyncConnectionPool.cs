using System;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000014 RID: 20
	public interface IAsyncConnectionPool
	{
		// Token: 0x06000054 RID: 84
		Task<IDbConnection> GetAsync(IDataSourceInfo dataSourceInfo);

		// Token: 0x06000055 RID: 85
		Task<bool> PutAsync(IDbConnection connection, IDataSourceInfo dataSourceInfo);
	}
}
