using System;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004BB RID: 1211
	public class SSONotificationFlag
	{
		// Token: 0x04001860 RID: 6240
		public const int SSO_NOTIFICATION_FLAG_NONE = 0;

		// Token: 0x04001861 RID: 6241
		public const int SSO_NOTIFICATION_FLAG_ADMIN_CHANGE = 1;

		// Token: 0x04001862 RID: 6242
		public const int SSO_NOTIFICATION_FLAG_TEST = 8;

		// Token: 0x04001863 RID: 6243
		public const int SSO_NOTIFICATION_FLAG_AUDIT = 16;

		// Token: 0x04001864 RID: 6244
		public const int SSO_NOTIFICATION_FLAG_WINDOWS = 32;

		// Token: 0x04001865 RID: 6245
		public const int SSO_NOTIFICATION_FLAG_WAIT = 64;

		// Token: 0x04001866 RID: 6246
		public const int SSO_NOTIFICATION_FLAG_SEND_ONLY = 128;
	}
}
