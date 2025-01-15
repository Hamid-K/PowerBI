using System;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x0200001D RID: 29
	internal enum ReportFlagsPrimary
	{
		// Token: 0x040000B9 RID: 185
		Delete = 1,
		// Token: 0x040000BA RID: 186
		ExecuteAndView,
		// Token: 0x040000BB RID: 187
		ReadProperties = 4,
		// Token: 0x040000BC RID: 188
		UpdateProperties = 8,
		// Token: 0x040000BD RID: 189
		UpdateParameters = 16,
		// Token: 0x040000BE RID: 190
		ReadDatasource = 32,
		// Token: 0x040000BF RID: 191
		UpdateDatasource = 64,
		// Token: 0x040000C0 RID: 192
		ReadReportDefinition = 128,
		// Token: 0x040000C1 RID: 193
		UpdateReportDefinition = 256,
		// Token: 0x040000C2 RID: 194
		CreateSubscription = 512,
		// Token: 0x040000C3 RID: 195
		DeleteSubscription = 1024,
		// Token: 0x040000C4 RID: 196
		ReadSubscription = 2048,
		// Token: 0x040000C5 RID: 197
		DeleteHistory = 4096,
		// Token: 0x040000C6 RID: 198
		UpdateSubscription = 8192,
		// Token: 0x040000C7 RID: 199
		CreateAnySubscription = 16384,
		// Token: 0x040000C8 RID: 200
		DeleteAnySubscription = 32768,
		// Token: 0x040000C9 RID: 201
		ReadAnySubscription = 65536,
		// Token: 0x040000CA RID: 202
		ReadAuthorizationPolicy = 131072,
		// Token: 0x040000CB RID: 203
		UpdateDeleteAuthorizationPolicy = 262144,
		// Token: 0x040000CC RID: 204
		UpdateAnySubscription = 524288,
		// Token: 0x040000CD RID: 205
		ReadPolicy = 1048576,
		// Token: 0x040000CE RID: 206
		UpdatePolicy = 2097152
	}
}
