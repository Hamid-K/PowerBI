using System;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000FD RID: 253
	internal sealed class MFARequiredException : AuthenticationException
	{
		// Token: 0x06000FB6 RID: 4022 RVA: 0x0003606F File Offset: 0x0003426F
		public MFARequiredException()
			: base(-1055719373, AuthenticationSR.Exception_MFARequiredAndSupported)
		{
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x00036081 File Offset: 0x00034281
		public MFARequiredException(Exception innerException)
			: base(-1055719373, AuthenticationSR.Exception_MFARequiredAndSupported, innerException)
		{
		}

		// Token: 0x04000874 RID: 2164
		private const int InteractiveLoginRequiredHResult = -1055719373;
	}
}
