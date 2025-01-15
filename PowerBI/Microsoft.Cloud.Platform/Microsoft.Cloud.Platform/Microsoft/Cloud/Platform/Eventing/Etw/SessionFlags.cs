using System;

namespace Microsoft.Cloud.Platform.Eventing.Etw
{
	// Token: 0x020003E5 RID: 997
	[Flags]
	public enum SessionFlags
	{
		// Token: 0x04000ACC RID: 2764
		None = 0,
		// Token: 0x04000ACD RID: 2765
		LocalSequenceNumbers = 32768,
		// Token: 0x04000ACE RID: 2766
		GlobalSequenceNumbers = 32768
	}
}
