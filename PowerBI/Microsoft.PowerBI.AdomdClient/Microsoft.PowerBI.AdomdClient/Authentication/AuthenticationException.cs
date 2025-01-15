using System;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x020000FC RID: 252
	internal class AuthenticationException : AdomdException
	{
		// Token: 0x06000EA4 RID: 3748 RVA: 0x000319B2 File Offset: 0x0002FBB2
		public AuthenticationException()
			: this(-1055719372, AuthenticationSR.Exception_AcquireTokenFailure)
		{
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x000319C4 File Offset: 0x0002FBC4
		public AuthenticationException(Exception innerException)
			: this(-1055719372, AuthenticationSR.Exception_AcquireTokenFailure, innerException)
		{
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000319D7 File Offset: 0x0002FBD7
		protected AuthenticationException(int hResult, string message)
			: base(message)
		{
			base.HResult = hResult;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000319E7 File Offset: 0x0002FBE7
		protected AuthenticationException(int hResult, string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = hResult;
		}

		// Token: 0x04000859 RID: 2137
		private const int AuthenticationFailHResult = -1055719372;
	}
}
