using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000109 RID: 265
	internal sealed class NonInteractiveLoginException : AuthenticationException
	{
		// Token: 0x06000F29 RID: 3881 RVA: 0x00033780 File Offset: 0x00031980
		public NonInteractiveLoginException()
			: base(-1055719373, AuthenticationSR.Exception_InteractiveLoginRequired)
		{
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x00033792 File Offset: 0x00031992
		public NonInteractiveLoginException(Exception innerException)
			: base(-1055719373, AuthenticationSR.Exception_InteractiveLoginRequired, innerException)
		{
		}

		// Token: 0x040008BC RID: 2236
		private const int InteractiveLoginRequiredHResult = -1055719373;
	}
}
