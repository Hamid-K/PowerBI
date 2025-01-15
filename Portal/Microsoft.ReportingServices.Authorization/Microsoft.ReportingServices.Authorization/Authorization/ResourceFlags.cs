using System;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x0200001F RID: 31
	internal enum ResourceFlags
	{
		// Token: 0x040000D8 RID: 216
		Delete = 1,
		// Token: 0x040000D9 RID: 217
		ReadProperties,
		// Token: 0x040000DA RID: 218
		UpdateProperties = 4,
		// Token: 0x040000DB RID: 219
		ReadContent = 8,
		// Token: 0x040000DC RID: 220
		UpdateContent = 16,
		// Token: 0x040000DD RID: 221
		ReadAuthorizationPolicy = 131072,
		// Token: 0x040000DE RID: 222
		UpdateDeleteAuthorizationPolicy = 262144,
		// Token: 0x040000DF RID: 223
		Comment = 32,
		// Token: 0x040000E0 RID: 224
		ManageComments = 64
	}
}
