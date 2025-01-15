using System;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000921 RID: 2337
	public static class MqConstants
	{
		// Token: 0x04002308 RID: 8968
		public const int DefaultPort = 1414;

		// Token: 0x04002309 RID: 8969
		public const int MqrcNoMessageAvailable = 2033;

		// Token: 0x0400230A RID: 8970
		public const int MqrcNotAuthorized = 2035;

		// Token: 0x0400230B RID: 8971
		public const long PageSize = 500L;

		// Token: 0x0400230C RID: 8972
		public const int MaximumLengthMessageFormat = 8;

		// Token: 0x0400230D RID: 8973
		public const int RequiredLengthMessageId = 24;

		// Token: 0x0400230E RID: 8974
		public const int RequiredLengthMessageCorrelator = 24;

		// Token: 0x0400230F RID: 8975
		public const int MaximumLengthMessageReplyToQueue = 48;

		// Token: 0x04002310 RID: 8976
		public const int MaximumLengthMessageReplyToQueueManager = 48;

		// Token: 0x04002311 RID: 8977
		public const int RequiredLengthMessageGroupId = 24;

		// Token: 0x04002312 RID: 8978
		public const int MaximumLengthMessageTokenGet = 16;

		// Token: 0x04002313 RID: 8979
		public const int MinimumCcsId = 37;

		// Token: 0x04002314 RID: 8980
		public const int MaximumCcsId = 65520;
	}
}
