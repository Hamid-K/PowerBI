using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200007A RID: 122
	[Serializable]
	public class ExternalImageLoadingDisabledException : Exception
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x0001BBE5 File Offset: 0x00019DE5
		public ExternalImageLoadingDisabledException()
			: base("Loading external image has been disabled")
		{
		}
	}
}
