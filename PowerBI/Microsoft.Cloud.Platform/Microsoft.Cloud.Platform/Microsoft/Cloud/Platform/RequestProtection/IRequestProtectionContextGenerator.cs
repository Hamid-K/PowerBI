using System;

namespace Microsoft.Cloud.Platform.RequestProtection
{
	// Token: 0x02000468 RID: 1128
	public interface IRequestProtectionContextGenerator
	{
		// Token: 0x06002331 RID: 9009
		IRequestProtectionContext GetContext(RequestProtectionOptions options, RequestProtectionGenerationOptions generationOptions);
	}
}
