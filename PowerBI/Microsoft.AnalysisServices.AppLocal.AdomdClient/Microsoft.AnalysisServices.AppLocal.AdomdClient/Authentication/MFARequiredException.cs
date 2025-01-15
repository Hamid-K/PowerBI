using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x02000108 RID: 264
	internal sealed class MFARequiredException : AuthenticationException
	{
		// Token: 0x06000F27 RID: 3879 RVA: 0x0003375B File Offset: 0x0003195B
		public MFARequiredException()
			: base(-1055719373, AuthenticationSR.Exception_MFARequiredAndSupported)
		{
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0003376D File Offset: 0x0003196D
		public MFARequiredException(Exception innerException)
			: base(-1055719373, AuthenticationSR.Exception_MFARequiredAndSupported, innerException)
		{
		}

		// Token: 0x040008BB RID: 2235
		private const int InteractiveLoginRequiredHResult = -1055719373;
	}
}
