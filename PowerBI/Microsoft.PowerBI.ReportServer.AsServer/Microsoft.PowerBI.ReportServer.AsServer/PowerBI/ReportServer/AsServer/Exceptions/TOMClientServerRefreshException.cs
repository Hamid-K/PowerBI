using System;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.PowerBI.ReportServer.AsServer.Exceptions
{
	// Token: 0x02000030 RID: 48
	internal class TOMClientServerRefreshException : AnalysisServicesException
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00003BE1 File Offset: 0x00001DE1
		public TOMClientServerRefreshException(string message)
			: base(message)
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000559C File Offset: 0x0000379C
		public TOMClientServerRefreshException(string message, Exception innerException)
			: base(message, innerException)
		{
			Logger.Error(this, message, Array.Empty<object>());
		}
	}
}
