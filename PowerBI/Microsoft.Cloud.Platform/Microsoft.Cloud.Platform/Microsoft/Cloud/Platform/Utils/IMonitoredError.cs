using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200024D RID: 589
	public interface IMonitoredError : IContainsPrivateInformation
	{
		// Token: 0x06000F2A RID: 3882
		bool IsFatal();

		// Token: 0x06000F2B RID: 3883
		bool IsBenign();

		// Token: 0x06000F2C RID: 3884
		bool IsPermanent();

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000F2D RID: 3885
		string ErrorShortName { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000F2E RID: 3886
		ErrorCorrelationId ErrorCorrelationId { get; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000F2F RID: 3887
		// (set) Token: 0x06000F30 RID: 3888
		MonitoringScopeId MonitoringScope { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000F31 RID: 3889
		// (set) Token: 0x06000F32 RID: 3890
		long ErrorEventId { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000F33 RID: 3891
		// (set) Token: 0x06000F34 RID: 3892
		string ErrorEventName { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000F35 RID: 3893
		// (set) Token: 0x06000F36 RID: 3894
		long ErrorEventsKitId { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000F37 RID: 3895
		// (set) Token: 0x06000F38 RID: 3896
		string ErrorEventsKitName { get; set; }
	}
}
