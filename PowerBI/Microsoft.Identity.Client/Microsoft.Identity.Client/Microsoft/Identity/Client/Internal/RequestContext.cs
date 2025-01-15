using System;
using System.Threading;
using Microsoft.Identity.Client.Core;
using Microsoft.Identity.Client.Internal.Logger;
using Microsoft.Identity.Client.TelemetryCore;
using Microsoft.Identity.Client.TelemetryCore.Internal.Events;

namespace Microsoft.Identity.Client.Internal
{
	// Token: 0x0200023A RID: 570
	internal class RequestContext
	{
		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001721 RID: 5921 RVA: 0x0004C60B File Offset: 0x0004A80B
		public Guid CorrelationId { get; }

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001722 RID: 5922 RVA: 0x0004C613 File Offset: 0x0004A813
		public ILoggerAdapter Logger { get; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001723 RID: 5923 RVA: 0x0004C61B File Offset: 0x0004A81B
		public IServiceBundle ServiceBundle { get; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06001724 RID: 5924 RVA: 0x0004C623 File Offset: 0x0004A823
		// (set) Token: 0x06001725 RID: 5925 RVA: 0x0004C62B File Offset: 0x0004A82B
		public ApiEvent ApiEvent { get; set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0004C634 File Offset: 0x0004A834
		public CancellationToken UserCancellationToken { get; }

		// Token: 0x06001727 RID: 5927 RVA: 0x0004C63C File Offset: 0x0004A83C
		public RequestContext(IServiceBundle serviceBundle, Guid correlationId, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (serviceBundle == null)
			{
				throw new ArgumentNullException("serviceBundle");
			}
			this.ServiceBundle = serviceBundle;
			this.Logger = LoggerHelper.CreateLogger(correlationId, this.ServiceBundle.Config);
			this.CorrelationId = correlationId;
			this.UserCancellationToken = cancellationToken;
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x0004C68A File Offset: 0x0004A88A
		public TelemetryHelper CreateTelemetryHelper(ApiEvent eventToStart)
		{
			return new TelemetryHelper(this.ServiceBundle.HttpTelemetryManager, eventToStart);
		}
	}
}
