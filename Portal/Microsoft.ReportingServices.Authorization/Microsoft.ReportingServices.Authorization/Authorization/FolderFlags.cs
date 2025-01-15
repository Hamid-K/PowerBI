using System;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x0200001C RID: 28
	internal enum FolderFlags
	{
		// Token: 0x040000AE RID: 174
		CreateFolder = 1,
		// Token: 0x040000AF RID: 175
		Delete,
		// Token: 0x040000B0 RID: 176
		ReadProperties = 4,
		// Token: 0x040000B1 RID: 177
		UpdateProperties = 8,
		// Token: 0x040000B2 RID: 178
		CreateReport = 16,
		// Token: 0x040000B3 RID: 179
		CreateResource = 32,
		// Token: 0x040000B4 RID: 180
		CreateDatasource = 64,
		// Token: 0x040000B5 RID: 181
		CreateModel = 128,
		// Token: 0x040000B6 RID: 182
		ReadAuthorizationPolicy = 131072,
		// Token: 0x040000B7 RID: 183
		UpdateDeleteAuthorizationPolicy = 262144
	}
}
