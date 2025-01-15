using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002F4 RID: 756
	public enum ExpirationType
	{
		// Token: 0x04000F1B RID: 3867
		None,
		// Token: 0x04000F1C RID: 3868
		AbsoluteExpiration,
		// Token: 0x04000F1D RID: 3869
		SlidingExpiration,
		// Token: 0x04000F1E RID: 3870
		NotProvided,
		// Token: 0x04000F1F RID: 3871
		Default = 3
	}
}
