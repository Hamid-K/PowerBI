using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200023B RID: 571
	public interface ITryingRetriableFlow : ISequencer
	{
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000ECA RID: 3786
		bool Succeeded { get; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000ECB RID: 3787
		// (set) Token: 0x06000ECC RID: 3788
		bool ShouldRetryOnKnownExceptions { get; set; }
	}
}
