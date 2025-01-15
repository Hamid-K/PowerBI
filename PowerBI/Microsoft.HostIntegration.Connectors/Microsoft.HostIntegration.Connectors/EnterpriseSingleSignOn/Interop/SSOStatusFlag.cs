using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004A0 RID: 1184
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("B001766D-5E76-49cb-B918-B94D2A57B698")]
	public class SSOStatusFlag
	{
		// Token: 0x04001819 RID: 6169
		public const int SSO_STATUS_FLAG_NONE = 0;

		// Token: 0x0400181A RID: 6170
		public const int SSO_STATUS_FLAG_ONLINE = 1;

		// Token: 0x0400181B RID: 6171
		public const int SSO_STATUS_FLAG_SECRET_SERVER_RUNNING = 2;

		// Token: 0x0400181C RID: 6172
		public const int SSO_STATUS_FLAG_PASSWORD_SYNC_LOADED = 4;

		// Token: 0x0400181D RID: 6173
		public const int SSO_STATUS_FLAG_PASSWORD_SYNC_FOR_WINDOWS_RUNNING = 8;

		// Token: 0x0400181E RID: 6174
		public const int SSO_STATUS_FLAG_PASSWORD_SYNC_FOR_PCNS_RUNNING = 8;

		// Token: 0x0400181F RID: 6175
		public const int SSO_STATUS_FLAG_PASSWORD_SYNC_FOR_ADAPTERS_RUNNING = 16;

		// Token: 0x04001820 RID: 6176
		public const int SSO_STATUS_FLAG_CLUSTER = 32;

		// Token: 0x04001821 RID: 6177
		public const int SSO_STATUS_FLAG_64BIT = 64;

		// Token: 0x04001822 RID: 6178
		public const int SSO_STATUS_FLAG_SSL = 128;

		// Token: 0x04001823 RID: 6179
		public const int SSO_STATUS_FLAG_CASE_SENSITIVE = 256;

		// Token: 0x04001824 RID: 6180
		public const int SSO_STATUS_FLAG_PASSWORD_SYNC_FOR_MIIS_RUNNING = 512;

		// Token: 0x04001825 RID: 6181
		public const int SSO_STATUS_FLAG_PASSWORD_SYNC_ALLOW_PCNS = 1024;

		// Token: 0x04001826 RID: 6182
		public const int SSO_STATUS_FLAG_PASSWORD_SYNC_ALLOW_MIIS = 2048;

		// Token: 0x04001827 RID: 6183
		public const int SSO_STATUS_FLAG_ALLOW_REMOTE_LOOKUP = 4096;

		// Token: 0x04001828 RID: 6184
		public const int SSO_STATUS_FLAG_ADMINISTRATOR = 8192;
	}
}
