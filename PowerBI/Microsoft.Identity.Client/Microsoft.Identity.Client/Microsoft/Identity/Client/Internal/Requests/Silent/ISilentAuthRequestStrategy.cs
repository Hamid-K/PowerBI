using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Internal.Requests.Silent
{
	// Token: 0x0200024E RID: 590
	internal interface ISilentAuthRequestStrategy
	{
		// Token: 0x060017D7 RID: 6103
		Task<AuthenticationResult> ExecuteAsync(CancellationToken cancellationToken);
	}
}
