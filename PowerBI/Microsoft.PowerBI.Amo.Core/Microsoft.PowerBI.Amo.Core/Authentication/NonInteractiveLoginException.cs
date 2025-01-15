using System;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000FE RID: 254
	internal sealed class NonInteractiveLoginException : AuthenticationException
	{
		// Token: 0x06000FB8 RID: 4024 RVA: 0x00036094 File Offset: 0x00034294
		public NonInteractiveLoginException()
			: base(-1055719373, AuthenticationSR.Exception_InteractiveLoginRequired)
		{
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000360A6 File Offset: 0x000342A6
		public NonInteractiveLoginException(Exception innerException)
			: base(-1055719373, AuthenticationSR.Exception_InteractiveLoginRequired, innerException)
		{
		}

		// Token: 0x04000875 RID: 2165
		private const int InteractiveLoginRequiredHResult = -1055719373;
	}
}
