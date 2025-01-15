using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x020000FC RID: 252
	internal class AuthenticationException : AdomdException
	{
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00031CE2 File Offset: 0x0002FEE2
		public AuthenticationException()
			: this(-1055719372, AuthenticationSR.Exception_AcquireTokenFailure)
		{
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00031CF4 File Offset: 0x0002FEF4
		public AuthenticationException(Exception innerException)
			: this(-1055719372, AuthenticationSR.Exception_AcquireTokenFailure, innerException)
		{
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00031D07 File Offset: 0x0002FF07
		protected AuthenticationException(int hResult, string message)
			: base(message)
		{
			base.HResult = hResult;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00031D17 File Offset: 0x0002FF17
		protected AuthenticationException(int hResult, string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = hResult;
		}

		// Token: 0x04000866 RID: 2150
		private const int AuthenticationFailHResult = -1055719372;
	}
}
