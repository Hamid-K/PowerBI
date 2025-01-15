using System;

namespace Microsoft.HostIntegration.MqClient.ProgrammableCommandFormat.Parameters
{
	// Token: 0x02000B87 RID: 2951
	public class QueueSharingGroupDisposition
	{
		// Token: 0x04004DEB RID: 19947
		public const int All = -1;

		// Token: 0x04004DEC RID: 19948
		public const int QueueManager = 0;

		// Token: 0x04004DED RID: 19949
		public const int Copy = 1;

		// Token: 0x04004DEE RID: 19950
		public const int Shared = 2;

		// Token: 0x04004DEF RID: 19951
		public const int Group = 3;

		// Token: 0x04004DF0 RID: 19952
		public const int Private = 4;

		// Token: 0x04004DF1 RID: 19953
		public const int Live = 6;
	}
}
