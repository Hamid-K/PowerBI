using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000173 RID: 371
	public class MsalThrottledUiRequiredException : MsalUiRequiredException
	{
		// Token: 0x0600124F RID: 4687 RVA: 0x0003E8A8 File Offset: 0x0003CAA8
		public MsalThrottledUiRequiredException(MsalUiRequiredException originalException)
			: base(originalException.ErrorCode, originalException.Message, originalException.InnerException, originalException.Classification)
		{
			base.SubError = originalException.SubError;
			base.StatusCode = originalException.StatusCode;
			base.Claims = originalException.Claims;
			base.CorrelationId = originalException.CorrelationId;
			base.ResponseBody = originalException.ResponseBody;
			base.Headers = originalException.Headers;
			this.OriginalServiceException = originalException;
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06001250 RID: 4688 RVA: 0x0003E922 File Offset: 0x0003CB22
		public MsalUiRequiredException OriginalServiceException { get; }
	}
}
