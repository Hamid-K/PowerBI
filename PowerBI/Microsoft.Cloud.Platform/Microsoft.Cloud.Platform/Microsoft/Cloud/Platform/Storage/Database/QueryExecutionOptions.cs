using System;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x0200003E RID: 62
	[Flags]
	public enum QueryExecutionOptions
	{
		// Token: 0x040000A5 RID: 165
		None = 0,
		// Token: 0x040000A6 RID: 166
		ReturnValue = 1,
		// Token: 0x040000A7 RID: 167
		UnThrottled = 2,
		// Token: 0x040000A8 RID: 168
		Modify = 4
	}
}
