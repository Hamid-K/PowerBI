using System;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x02000008 RID: 8
	public interface IServiceErrorExtractor
	{
		// Token: 0x0600000D RID: 13
		bool CanExtractFromException(Exception e);

		// Token: 0x0600000E RID: 14
		bool TryExtractServiceError(Exception ex, ServiceErrorExtractor extractor, out ServiceError error);
	}
}
