using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Identity.Client.Platforms.Shared.Desktop.OsBrowser
{
	// Token: 0x02000184 RID: 388
	internal interface IUriInterceptor
	{
		// Token: 0x060012B1 RID: 4785
		Task<Uri> ListenToSingleRequestAndRespondAsync(int port, string path, Func<Uri, MessageAndHttpCode> responseProducer, CancellationToken cancellationToken);
	}
}
