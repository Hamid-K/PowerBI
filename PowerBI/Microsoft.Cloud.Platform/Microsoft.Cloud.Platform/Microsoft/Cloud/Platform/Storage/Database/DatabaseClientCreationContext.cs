using System;
using Microsoft.Cloud.Platform.EventsKit;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000040 RID: 64
	public sealed class DatabaseClientCreationContext
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00005B7D File Offset: 0x00003D7D
		public DatabaseClientCreationContext(string identity, IDatabaseSpecificationProxy proxy, WorkTicketManager workTicketManager, IEventsKitFactory eventsKitFactory, IActivityFactory activityFactory, IMonitoredActivityCompletionModelFactory modelFactory)
		{
			this.Identity = identity;
			this.Proxy = proxy;
			this.TicketManager = workTicketManager;
			this.EventsKitFactory = eventsKitFactory;
			this.ActivityFactory = activityFactory;
			this.ModelFactory = modelFactory;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00005BB2 File Offset: 0x00003DB2
		// (set) Token: 0x0600017C RID: 380 RVA: 0x00005BBA File Offset: 0x00003DBA
		public string Identity { get; private set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00005BC3 File Offset: 0x00003DC3
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00005BCB File Offset: 0x00003DCB
		public IDatabaseSpecificationProxy Proxy { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00005BD4 File Offset: 0x00003DD4
		// (set) Token: 0x06000180 RID: 384 RVA: 0x00005BDC File Offset: 0x00003DDC
		public WorkTicketManager TicketManager { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00005BE5 File Offset: 0x00003DE5
		// (set) Token: 0x06000182 RID: 386 RVA: 0x00005BED File Offset: 0x00003DED
		public IEventsKitFactory EventsKitFactory { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00005BF6 File Offset: 0x00003DF6
		// (set) Token: 0x06000184 RID: 388 RVA: 0x00005BFE File Offset: 0x00003DFE
		public IActivityFactory ActivityFactory { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00005C07 File Offset: 0x00003E07
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00005C0F File Offset: 0x00003E0F
		public IMonitoredActivityCompletionModelFactory ModelFactory { get; private set; }
	}
}
