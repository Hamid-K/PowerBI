using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000078 RID: 120
	public enum EventOpcode
	{
		// Token: 0x0400015F RID: 351
		Info,
		// Token: 0x04000160 RID: 352
		Start,
		// Token: 0x04000161 RID: 353
		Stop,
		// Token: 0x04000162 RID: 354
		DataCollectionStart,
		// Token: 0x04000163 RID: 355
		DataCollectionStop,
		// Token: 0x04000164 RID: 356
		Extension,
		// Token: 0x04000165 RID: 357
		Reply,
		// Token: 0x04000166 RID: 358
		Resume,
		// Token: 0x04000167 RID: 359
		Suspend,
		// Token: 0x04000168 RID: 360
		Send,
		// Token: 0x04000169 RID: 361
		Receive = 240
	}
}
