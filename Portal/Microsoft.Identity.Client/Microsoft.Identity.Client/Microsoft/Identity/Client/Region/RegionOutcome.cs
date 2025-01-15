using System;

namespace Microsoft.Identity.Client.Region
{
	// Token: 0x02000269 RID: 617
	public enum RegionOutcome
	{
		// Token: 0x04000B03 RID: 2819
		None,
		// Token: 0x04000B04 RID: 2820
		UserProvidedValid,
		// Token: 0x04000B05 RID: 2821
		UserProvidedAutodetectionFailed,
		// Token: 0x04000B06 RID: 2822
		UserProvidedInvalid,
		// Token: 0x04000B07 RID: 2823
		AutodetectSuccess,
		// Token: 0x04000B08 RID: 2824
		FallbackToGlobal
	}
}
