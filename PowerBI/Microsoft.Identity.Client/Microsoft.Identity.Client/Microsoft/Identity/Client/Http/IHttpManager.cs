using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client.Http
{
	// Token: 0x0200028D RID: 653
	internal interface IHttpManager
	{
		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001911 RID: 6417
		long LastRequestDurationInMs { get; }

		// Token: 0x06001912 RID: 6418
		Task<HttpResponse> SendPostAsync(Uri endpoint, IDictionary<string, string> headers, IDictionary<string, string> bodyParameters, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001913 RID: 6419
		Task<HttpResponse> SendPostAsync(Uri endpoint, IDictionary<string, string> headers, HttpContent body, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001914 RID: 6420
		Task<HttpResponse> SendGetAsync(Uri endpoint, IDictionary<string, string> headers, ILoggerAdapter logger, bool retry = true, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001915 RID: 6421
		Task<HttpResponse> SendPostForceResponseAsync(Uri uri, IDictionary<string, string> headers, StringContent body, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001916 RID: 6422
		Task<HttpResponse> SendPostForceResponseAsync(Uri uri, IDictionary<string, string> headers, IDictionary<string, string> bodyParameters, ILoggerAdapter logger, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x06001917 RID: 6423
		Task<HttpResponse> SendGetForceResponseAsync(Uri endpoint, IDictionary<string, string> headers, ILoggerAdapter logger, bool retry = true, CancellationToken cancellationToken = default(CancellationToken));
	}
}
