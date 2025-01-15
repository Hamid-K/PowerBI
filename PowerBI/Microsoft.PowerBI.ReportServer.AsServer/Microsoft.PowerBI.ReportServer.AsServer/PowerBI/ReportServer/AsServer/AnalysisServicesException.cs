using System;
using Microsoft.BIServer.HostingEnvironment;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000019 RID: 25
	public abstract class AnalysisServicesException : Exception
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000433C File Offset: 0x0000253C
		public AnalysisServicesException()
		{
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004344 File Offset: 0x00002544
		public AnalysisServicesException(string message)
			: base(message)
		{
			Logger.Error(this, message, Array.Empty<object>());
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004359 File Offset: 0x00002559
		public AnalysisServicesException(string message, Exception innerException)
			: base(message, innerException)
		{
			Logger.Error(this, message, Array.Empty<object>());
		}
	}
}
