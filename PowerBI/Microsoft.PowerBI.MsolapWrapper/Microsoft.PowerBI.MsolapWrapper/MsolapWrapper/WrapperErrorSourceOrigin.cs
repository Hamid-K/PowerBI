using System;

namespace MsolapWrapper
{
	// Token: 0x02000028 RID: 40
	[Flags]
	public enum WrapperErrorSourceOrigin
	{
		// Token: 0x04000130 RID: 304
		None = 0,
		// Token: 0x04000131 RID: 305
		MsolapWrapper = 2,
		// Token: 0x04000132 RID: 306
		AS = 4,
		// Token: 0x04000133 RID: 307
		OnPremise = 8
	}
}
