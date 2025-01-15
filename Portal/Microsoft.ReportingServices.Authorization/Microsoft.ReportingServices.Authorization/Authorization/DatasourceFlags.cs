using System;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x02000020 RID: 32
	internal enum DatasourceFlags
	{
		// Token: 0x040000E2 RID: 226
		Delete = 1,
		// Token: 0x040000E3 RID: 227
		ReadProperties,
		// Token: 0x040000E4 RID: 228
		UpdateProperties = 4,
		// Token: 0x040000E5 RID: 229
		ReadContent = 8,
		// Token: 0x040000E6 RID: 230
		UpdateContent = 16,
		// Token: 0x040000E7 RID: 231
		ReadAuthorizationPolicy = 131072,
		// Token: 0x040000E8 RID: 232
		UpdateDeleteAuthorizationPolicy = 262144
	}
}
