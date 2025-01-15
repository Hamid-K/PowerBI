using System;
using System.Threading.Tasks;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000095 RID: 149
	[CLSCompliant(true)]
	public interface IConnectionStringBuilder
	{
		// Token: 0x06000537 RID: 1335
		Task<string> Build(DatabaseAuthenticationInfo databaseAuthenticationInfo);
	}
}
