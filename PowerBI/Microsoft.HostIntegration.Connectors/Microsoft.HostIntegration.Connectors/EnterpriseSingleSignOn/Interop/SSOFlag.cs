using System;
using System.Runtime.InteropServices;

namespace Microsoft.EnterpriseSingleSignOn.Interop
{
	// Token: 0x020004B7 RID: 1207
	[ClassInterface(ClassInterfaceType.None)]
	[Guid("2C02A30F-50D8-4509-BA85-C695AD7FA262")]
	public class SSOFlag
	{
		// Token: 0x04001829 RID: 6185
		public const int SSO_FLAG_NONE = 0;

		// Token: 0x0400182A RID: 6186
		public const int SSO_FLAG_REFRESH = 1;

		// Token: 0x0400182B RID: 6187
		public const int SSO_FLAG_ENABLED = 2;

		// Token: 0x0400182C RID: 6188
		public const int SSO_FLAG_SSO_WINDOWS_TO_EXTERNAL = 4;

		// Token: 0x0400182D RID: 6189
		public const int SSO_WINDOWS_TO_EXTERNAL = 4;

		// Token: 0x0400182E RID: 6190
		public const int SSO_FLAG_RUNTIME = 4;

		// Token: 0x0400182F RID: 6191
		public const int SSO_FLAG_SSO_EXTERNAL_TO_WINDOWS = 8;

		// Token: 0x04001830 RID: 6192
		public const int SSO_EXTERNAL_TO_WINDOWS = 8;

		// Token: 0x04001831 RID: 6193
		public const int SSO_FLAG_SSO_VERIFY_EXTERNAL_CREDS = 16;

		// Token: 0x04001832 RID: 6194
		public const int SSO_FLAG_ALLOW_TICKETS = 32;

		// Token: 0x04001833 RID: 6195
		public const int SSO_FLAG_VALIDATE_TICKETS = 64;

		// Token: 0x04001834 RID: 6196
		public const int SSO_FLAG_ADMIN_ENABLED = 128;

		// Token: 0x04001835 RID: 6197
		public const int SSO_FLAG_READ_MODIFY_WRITE = 256;

		// Token: 0x04001836 RID: 6198
		public const int SSO_FLAG_REPLAY = 256;

		// Token: 0x04001837 RID: 6199
		public const int SSO_FLAG_PARTIAL_SYNC_FROM_WINDOWS_TO_DB = 256;

		// Token: 0x04001838 RID: 6200
		public const int SSO_FLAG_PARTIAL_SYNC_FROM_EXTERNAL_TO_DB = 512;

		// Token: 0x04001839 RID: 6201
		public const int SSO_FLAG_FULL_SYNC_FROM_WINDOWS_TO_EXTERNAL = 1024;

		// Token: 0x0400183A RID: 6202
		public const int SSO_FLAG_FULL_SYNC_FROM_EXTERNAL_TO_WINDOWS = 2048;

		// Token: 0x0400183B RID: 6203
		public const int SSO_FLAG_SYNC_VERIFY_EXTERNAL_CREDS = 4096;

		// Token: 0x0400183C RID: 6204
		public const int SSO_FLAG_SYNC_PROVIDE_OLD_EXTERNAL_CREDS = 8192;

		// Token: 0x0400183D RID: 6205
		public const int SSO_FLAG_SYNC_ALLOW_MAPPING_CONFLICTS = 16384;

		// Token: 0x0400183E RID: 6206
		public const int SSO_FLAG_APP_USES_GROUP_MAPPING = 65536;

		// Token: 0x0400183F RID: 6207
		public const int SSO_FLAG_APP_GROUP = 65536;

		// Token: 0x04001840 RID: 6208
		public const int SSO_FLAG_APP_EXTERNAL_NAME_SAME = 131072;

		// Token: 0x04001841 RID: 6209
		public const int SSO_FLAG_APP_ALLOW_LOCAL = 262144;

		// Token: 0x04001842 RID: 6210
		public const int SSO_FLAG_APP_ADMIN_SAME = 524288;

		// Token: 0x04001843 RID: 6211
		public const int SSO_FLAG_APP_CONFIG_STORE = 1048576;

		// Token: 0x04001844 RID: 6212
		public const int SSO_FLAG_APP_TICKET_TIMEOUT = 2097152;

		// Token: 0x04001845 RID: 6213
		public const int SSO_FLAG_APP_ADAPTER = 4194304;

		// Token: 0x04001846 RID: 6214
		public const int SSO_FLAG_APP_FILTER_BY_TYPE = 1;

		// Token: 0x04001847 RID: 6215
		public const int SSO_FLAG_APP_DIRECT_PASSWORD_SYNC = 33554432;

		// Token: 0x04001848 RID: 6216
		public const int SSO_FLAG_APP_WINDOWS_CREDS = 8388608;

		// Token: 0x04001849 RID: 6217
		public const int SSO_FLAG_APP_RESTRICTED_CREDS = 16777216;

		// Token: 0x0400184A RID: 6218
		public const int SSO_FLAG_APP_SHOW_FILTER_ONLY = 67108864;

		// Token: 0x0400184B RID: 6219
		public const int SSO_FLAG_APP_RESTRICT_MAPPING_CREATE = 134217728;

		// Token: 0x0400184C RID: 6220
		public const int SSO_FLAG_MAPPING_REQUIRES_WINDOWS_PASSWORD = 16777216;

		// Token: 0x0400184D RID: 6221
		public const int SSO_FLAG_MAPPING_REQUIRES_EXTERNAL_CREDS = 33554432;

		// Token: 0x0400184E RID: 6222
		public const int SSO_FLAG_MAPPING_ENABLE_AUDIT = 67108864;

		// Token: 0x0400184F RID: 6223
		public const int SSO_FLAG_MAPPING_CONFIG_STORE = 134217728;

		// Token: 0x04001850 RID: 6224
		public const int SSO_FLAG_MAPPING_ADMIN = 268435456;

		// Token: 0x04001851 RID: 6225
		public const int SSO_FLAG_MAPPING_HOSTGROUP = 536870912;

		// Token: 0x04001852 RID: 6226
		public const int SSO_FLAG_MAPPING_GROUP = 1073741824;

		// Token: 0x04001853 RID: 6227
		public const int SSO_FLAG_MAPPING_CHECK = 1048576;

		// Token: 0x04001854 RID: 6228
		public const int SSO_FLAG_FIELD_INFO_MASK = 268435456;

		// Token: 0x04001855 RID: 6229
		public const int SSO_FLAG_FIELD_INFO_SYNC = 536870912;

		// Token: 0x04001856 RID: 6230
		public const int SSO_FLAG_SSO_DISABLE_CRED_CACHE = 1073741824;
	}
}
