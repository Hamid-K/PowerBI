using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200032D RID: 813
	public interface IImageInstance
	{
		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x06001E68 RID: 7784
		byte[] ImageData { get; }

		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x06001E69 RID: 7785
		string StreamName { get; }

		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x06001E6A RID: 7786
		string MIMEType { get; }
	}
}
