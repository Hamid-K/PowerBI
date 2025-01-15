using System;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004BC RID: 1212
	public class SSONotificationType
	{
		// Token: 0x04001867 RID: 6247
		public const int SSO_NOTIFICATION_TYPE_NONE = 0;

		// Token: 0x04001868 RID: 6248
		public const int SSO_NOTIFICATION_TYPE_SHUTDOWN = 1;

		// Token: 0x04001869 RID: 6249
		public const int SSO_NOTIFICATION_TYPE_SHUTDOWN_COMPLETE = 2;

		// Token: 0x0400186A RID: 6250
		public const int SSO_NOTIFICATION_TYPE_PASSWORD_CHANGE = 3;

		// Token: 0x0400186B RID: 6251
		public const int SSO_NOTIFICATION_TYPE_PASSWORD_CHANGE_COMPLETE = 4;

		// Token: 0x0400186C RID: 6252
		public const int SSO_NOTIFICATION_TYPE_PASSWORD_EXPIRED = 5;

		// Token: 0x0400186D RID: 6253
		public const int SSO_NOTIFICATION_TYPE_STATUS_REQUEST = 6;

		// Token: 0x0400186E RID: 6254
		public const int SSO_NOTIFICATION_TYPE_STATUS_ONLINE = 7;

		// Token: 0x0400186F RID: 6255
		public const int SSO_NOTIFICATION_TYPE_STATUS_OFFLINE = 8;

		// Token: 0x04001870 RID: 6256
		public const int SSO_NOTIFICATION_TYPE_PROPERTIES_CHANGED = 9;

		// Token: 0x04001871 RID: 6257
		public const int SSO_NOTIFICATION_TYPE_ADAPTERS_IN_GROUP = 4096;

		// Token: 0x04001872 RID: 6258
		public const int SSO_NOTIFICATION_TYPE_ADD_ADAPTER = 4097;

		// Token: 0x04001873 RID: 6259
		public const int SSO_NOTIFICATION_TYPE_DELETE_ADAPTER = 4098;
	}
}
