using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000079 RID: 121
	[Serializable]
	public class ExternalImageLoadingException : Exception
	{
		// Token: 0x06000767 RID: 1895 RVA: 0x0001BBD7 File Offset: 0x00019DD7
		public ExternalImageLoadingException(Exception innerException)
			: base("Error loading external image", innerException)
		{
		}
	}
}
