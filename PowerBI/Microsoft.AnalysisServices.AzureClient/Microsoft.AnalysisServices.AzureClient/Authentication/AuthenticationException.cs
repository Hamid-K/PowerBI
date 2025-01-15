using System;

namespace Microsoft.AnalysisServices.AzureClient.Authentication
{
	// Token: 0x02000016 RID: 22
	internal class AuthenticationException : Exception
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002D08 File Offset: 0x00000F08
		public AuthenticationException()
			: this(-1055719372, "Failed to acquire authentication token")
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002D1A File Offset: 0x00000F1A
		public AuthenticationException(Exception innerException)
			: this(-1055719372, "Failed to acquire authentication token", innerException)
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002D2D File Offset: 0x00000F2D
		protected AuthenticationException(int hResult, string message)
			: base(message)
		{
			base.HResult = hResult;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002D3D File Offset: 0x00000F3D
		protected AuthenticationException(int hResult, string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = hResult;
		}

		// Token: 0x04000039 RID: 57
		private const int AuthenticationFailHResult = -1055719372;

		// Token: 0x0400003A RID: 58
		private const string ErrorMessage = "Failed to acquire authentication token";
	}
}
