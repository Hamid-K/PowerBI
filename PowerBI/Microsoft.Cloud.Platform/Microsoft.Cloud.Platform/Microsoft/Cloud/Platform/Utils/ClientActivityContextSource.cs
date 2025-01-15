using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000210 RID: 528
	[Flags]
	public enum ClientActivityContextSource
	{
		// Token: 0x04000575 RID: 1397
		None = 0,
		// Token: 0x04000576 RID: 1398
		CreateNew = 1,
		// Token: 0x04000577 RID: 1399
		Header = 3,
		// Token: 0x04000578 RID: 1400
		QueryString = 5,
		// Token: 0x04000579 RID: 1401
		Cookie = 9
	}
}
