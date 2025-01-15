using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000339 RID: 825
	public interface IImage
	{
		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06001ED2 RID: 7890
		Image.SourceType Source { get; }

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06001ED3 RID: 7891
		ReportStringProperty Value { get; }

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06001ED4 RID: 7892
		ReportStringProperty MIMEType { get; }
	}
}
