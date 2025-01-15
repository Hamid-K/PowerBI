using System;

namespace Microsoft.Identity.Client.Http
{
	// Token: 0x0200028A RID: 650
	internal class HttpManagerManagedIdentity : HttpManagerWithRetry
	{
		// Token: 0x060018FC RID: 6396 RVA: 0x0005279F File Offset: 0x0005099F
		public HttpManagerManagedIdentity(IMsalHttpClientFactory httpClientFactory)
			: base(httpClientFactory)
		{
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x000527A8 File Offset: 0x000509A8
		protected override bool IsRetryableStatusCode(int statusCode)
		{
			if (statusCode <= 408)
			{
				if (statusCode != 404 && statusCode != 408)
				{
					return false;
				}
			}
			else if (statusCode != 429 && statusCode != 500 && statusCode - 503 > 1)
			{
				return false;
			}
			return true;
		}
	}
}
