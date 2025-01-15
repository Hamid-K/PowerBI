using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000108 RID: 264
	internal sealed class MFARequiredException : AuthenticationException
	{
		// Token: 0x06000F1A RID: 3866 RVA: 0x0003342B File Offset: 0x0003162B
		public MFARequiredException()
			: base(-1055719373, AuthenticationSR.Exception_MFARequiredAndSupported)
		{
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0003343D File Offset: 0x0003163D
		public MFARequiredException(Exception innerException)
			: base(-1055719373, AuthenticationSR.Exception_MFARequiredAndSupported, innerException)
		{
		}

		// Token: 0x040008AE RID: 2222
		private const int InteractiveLoginRequiredHResult = -1055719373;
	}
}
