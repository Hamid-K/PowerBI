using System;
using System.Data.SqlClient;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x0200005A RID: 90
	public interface IConnectionManager : IDisposable
	{
		// Token: 0x06000380 RID: 896
		SqlConnection GetConnection(string connectionName);
	}
}
