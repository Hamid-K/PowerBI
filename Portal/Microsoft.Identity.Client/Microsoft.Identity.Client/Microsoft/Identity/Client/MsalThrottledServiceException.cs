using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000172 RID: 370
	public class MsalThrottledServiceException : MsalServiceException
	{
		// Token: 0x0600124D RID: 4685 RVA: 0x0003E82C File Offset: 0x0003CA2C
		public MsalThrottledServiceException(MsalServiceException originalException)
			: base(originalException.ErrorCode, originalException.Message, originalException.InnerException)
		{
			base.SubError = originalException.SubError;
			base.StatusCode = originalException.StatusCode;
			base.Claims = originalException.Claims;
			base.CorrelationId = originalException.CorrelationId;
			base.ResponseBody = originalException.ResponseBody;
			base.Headers = originalException.Headers;
			this.OriginalServiceException = originalException;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600124E RID: 4686 RVA: 0x0003E8A0 File Offset: 0x0003CAA0
		public MsalServiceException OriginalServiceException { get; }
	}
}
