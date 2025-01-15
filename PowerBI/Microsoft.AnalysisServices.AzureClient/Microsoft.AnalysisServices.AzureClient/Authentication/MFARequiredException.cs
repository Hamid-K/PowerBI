using System;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x02000021 RID: 33
	internal sealed class MFARequiredException : AuthenticationException
	{
		// Token: 0x060000E9 RID: 233 RVA: 0x000047E9 File Offset: 0x000029E9
		public MFARequiredException()
			: base(-1055719373, "Interactive login is required")
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000047FB File Offset: 0x000029FB
		public MFARequiredException(Exception innerException)
			: base(-1055719373, "Interactive login is required", innerException)
		{
		}

		// Token: 0x0400008A RID: 138
		private const int InteractiveLoginRequiredHResult = -1055719373;

		// Token: 0x0400008B RID: 139
		private const string ErrorMessage = "Interactive login is required";
	}
}
