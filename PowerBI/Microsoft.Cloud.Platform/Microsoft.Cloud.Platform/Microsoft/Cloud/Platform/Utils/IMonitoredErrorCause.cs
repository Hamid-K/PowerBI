using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200024E RID: 590
	public interface IMonitoredErrorCause
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000F39 RID: 3897
		string Message { get; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000F3A RID: 3898
		Exception CauseException { get; }
	}
}
