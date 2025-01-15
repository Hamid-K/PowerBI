using System;
using System.Net;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004F5 RID: 1269
	public class RetryOnTransientWebExceptionsPolicy : PredicateRetryPolicy
	{
		// Token: 0x0600268D RID: 9869 RVA: 0x00089A40 File Offset: 0x00087C40
		public RetryOnTransientWebExceptionsPolicy(int numRetries)
			: base(new Predicate<Exception>(RetryOnTransientWebExceptionsPolicy.ShouldRetry), (Exception ex) => false, numRetries)
		{
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x00089A74 File Offset: 0x00087C74
		private static bool ShouldRetry(Exception ex)
		{
			if (ex is CommunicationFrameworkTimeoutException || ex is CommunicationFrameworkCommunicationException)
			{
				return true;
			}
			for (Exception ex2 = ex.InnerException; ex2 != null; ex2 = ex2.InnerException)
			{
				WebException ex3 = ex2 as WebException;
				if (ex3 != null)
				{
					HttpWebResponse httpWebResponse = ex3.Response as HttpWebResponse;
					if (httpWebResponse != null)
					{
						return httpWebResponse.StatusCode == HttpStatusCode.InternalServerError;
					}
				}
			}
			return false;
		}
	}
}
