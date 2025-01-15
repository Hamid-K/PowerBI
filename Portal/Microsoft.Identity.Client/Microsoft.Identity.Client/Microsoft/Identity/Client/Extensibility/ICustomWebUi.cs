using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Extensibility
{
	// Token: 0x0200029A RID: 666
	public interface ICustomWebUi
	{
		// Token: 0x0600193E RID: 6462
		Task<Uri> AcquireAuthorizationCodeAsync(Uri authorizationUri, Uri redirectUri, CancellationToken cancellationToken);
	}
}
