using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004AA RID: 1194
	public interface IProviderServiceConfiguration : IServiceConfiguration
	{
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060024A1 RID: 9377
		IEnumerable<EndpointInfo> EndpointsInformation { get; }

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060024A2 RID: 9378
		int MaxConcurrentCalls { get; }

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060024A3 RID: 9379
		int MaxConcurrentSessions { get; }

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060024A4 RID: 9380
		string ServiceCertificateName { get; }

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060024A5 RID: 9381
		NonContractualExceptionBehavior CrashServerOnNonContractualException { get; }

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060024A6 RID: 9382
		bool DisableDefaultErrorHandler { get; }

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x060024A7 RID: 9383
		// (set) Token: 0x060024A8 RID: 9384
		TimeSpan RequestInitializationTimeout { get; set; }

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x060024A9 RID: 9385
		// (set) Token: 0x060024AA RID: 9386
		int MaxPendingAccepts { get; set; }
	}
}
