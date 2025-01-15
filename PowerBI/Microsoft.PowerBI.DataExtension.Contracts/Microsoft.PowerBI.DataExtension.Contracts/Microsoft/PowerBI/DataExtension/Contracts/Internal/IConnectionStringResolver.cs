using System;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataExtension.Contracts.Internal
{
	// Token: 0x02000017 RID: 23
	public interface IConnectionStringResolver
	{
		// Token: 0x06000059 RID: 89
		Task<bool> TryResolveConnectionStringAsync(uint errorCode, string dataSourceName, out string connectionString);
	}
}
