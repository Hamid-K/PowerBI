using System;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x02000005 RID: 5
	public class DefaultServiceErrorExtraction : IServiceErrorExtractor
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		public bool CanExtractFromException(Exception e)
		{
			return true;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000206A File Offset: 0x0000026A
		public bool TryExtractServiceError(Exception ex, ServiceErrorExtractor extractor, out ServiceError error)
		{
			error = new ServiceError();
			return false;
		}
	}
}
