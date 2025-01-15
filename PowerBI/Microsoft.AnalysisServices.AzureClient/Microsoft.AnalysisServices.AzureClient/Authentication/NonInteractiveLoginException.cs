using System;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x02000022 RID: 34
	internal sealed class NonInteractiveLoginException : AuthenticationException
	{
		// Token: 0x060000EB RID: 235 RVA: 0x0000480E File Offset: 0x00002A0E
		public NonInteractiveLoginException()
			: base(-1055719373, "Interactive login is required")
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004820 File Offset: 0x00002A20
		public NonInteractiveLoginException(Exception innerException)
			: base(-1055719373, "Interactive login is required", innerException)
		{
		}

		// Token: 0x0400008C RID: 140
		private const int InteractiveLoginRequiredHResult = -1055719373;

		// Token: 0x0400008D RID: 141
		private const string ErrorMessage = "Interactive login is required";
	}
}
