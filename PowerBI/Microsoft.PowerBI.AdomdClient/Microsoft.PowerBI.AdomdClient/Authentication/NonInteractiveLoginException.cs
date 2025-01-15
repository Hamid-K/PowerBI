using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000109 RID: 265
	internal sealed class NonInteractiveLoginException : AuthenticationException
	{
		// Token: 0x06000F1C RID: 3868 RVA: 0x00033450 File Offset: 0x00031650
		public NonInteractiveLoginException()
			: base(-1055719373, AuthenticationSR.Exception_InteractiveLoginRequired)
		{
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00033462 File Offset: 0x00031662
		public NonInteractiveLoginException(Exception innerException)
			: base(-1055719373, AuthenticationSR.Exception_InteractiveLoginRequired, innerException)
		{
		}

		// Token: 0x040008AF RID: 2223
		private const int InteractiveLoginRequiredHResult = -1055719373;
	}
}
