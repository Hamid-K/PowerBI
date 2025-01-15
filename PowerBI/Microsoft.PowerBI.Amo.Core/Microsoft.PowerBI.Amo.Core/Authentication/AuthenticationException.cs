using System;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x020000F1 RID: 241
	internal class AuthenticationException : AmoException
	{
		// Token: 0x06000F42 RID: 3906 RVA: 0x0003468A File Offset: 0x0003288A
		public AuthenticationException()
			: this(-1055719372, AuthenticationSR.Exception_AcquireTokenFailure)
		{
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x0003469C File Offset: 0x0003289C
		public AuthenticationException(Exception innerException)
			: this(-1055719372, AuthenticationSR.Exception_AcquireTokenFailure, innerException)
		{
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x000346AF File Offset: 0x000328AF
		protected AuthenticationException(int hResult, string message)
			: base(message)
		{
			base.HResult = hResult;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x000346BF File Offset: 0x000328BF
		protected AuthenticationException(int hResult, string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = hResult;
		}

		// Token: 0x04000822 RID: 2082
		private const int AuthenticationFailHResult = -1055719372;
	}
}
