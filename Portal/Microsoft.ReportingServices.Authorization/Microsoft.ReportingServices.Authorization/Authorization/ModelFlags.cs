using System;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000021 RID: 33
	internal enum ModelFlags
	{
		// Token: 0x040000EA RID: 234
		Delete = 1,
		// Token: 0x040000EB RID: 235
		ReadProperties,
		// Token: 0x040000EC RID: 236
		UpdateProperties = 4,
		// Token: 0x040000ED RID: 237
		ReadContent = 8,
		// Token: 0x040000EE RID: 238
		UpdateContent = 16,
		// Token: 0x040000EF RID: 239
		ReadDatasource = 32,
		// Token: 0x040000F0 RID: 240
		UpdateDatasource = 64,
		// Token: 0x040000F1 RID: 241
		ReadModelItemAuthorizationPolicies = 128,
		// Token: 0x040000F2 RID: 242
		UpdateModelItemAuthorizationPolicies = 256,
		// Token: 0x040000F3 RID: 243
		ReadAuthorizationPolicy = 131072,
		// Token: 0x040000F4 RID: 244
		UpdateDeleteAuthorizationPolicy = 262144
	}
}
