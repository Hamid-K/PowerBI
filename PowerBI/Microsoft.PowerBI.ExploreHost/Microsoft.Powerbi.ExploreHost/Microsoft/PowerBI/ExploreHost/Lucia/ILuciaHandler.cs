using System;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.ExploreHost.Lucia
{
	// Token: 0x02000066 RID: 102
	public interface ILuciaHandler
	{
		// Token: 0x060002CD RID: 717
		Task<string> InterpretAsync(string interpretRequest, string databaseName);
	}
}
