using System;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x0200002E RID: 46
	public interface IDeliveryReportServerInformation
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000063 RID: 99
		Extension[] RenderingExtension { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000064 RID: 100
		Setting[] ServerSettings { get; }
	}
}
