using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common
{
	// Token: 0x02000007 RID: 7
	[NullableContext(1)]
	public interface IOAuthTokenService
	{
		// Token: 0x06000007 RID: 7
		Task<string> GetVNetOAuthTokenAsync();
	}
}
