using System;
using System.Globalization;
using System.Threading;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal;

namespace Microsoft.Identity.Client.ApiConfig.Executors
{
	// Token: 0x020002DF RID: 735
	internal abstract class AbstractExecutor
	{
		// Token: 0x06001B47 RID: 6983 RVA: 0x00057582 File Offset: 0x00055782
		protected AbstractExecutor(IServiceBundle serviceBundle)
		{
			this.ServiceBundle = serviceBundle;
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001B48 RID: 6984 RVA: 0x00057591 File Offset: 0x00055791
		public IServiceBundle ServiceBundle { get; }

		// Token: 0x06001B49 RID: 6985 RVA: 0x0005759C File Offset: 0x0005579C
		protected RequestContext CreateRequestContextAndLogVersionInfo(Guid correlationId, CancellationToken userCancellationToken = default(CancellationToken))
		{
			RequestContext requestContext = new RequestContext(this.ServiceBundle, correlationId, userCancellationToken);
			requestContext.Logger.Info(() => string.Format(CultureInfo.InvariantCulture, "MSAL {0} with assembly version '{1}'. CorrelationId({2})", this.ServiceBundle.PlatformProxy.GetProductName(), MsalIdHelper.GetMsalVersion(), requestContext.CorrelationId));
			return requestContext;
		}
	}
}
