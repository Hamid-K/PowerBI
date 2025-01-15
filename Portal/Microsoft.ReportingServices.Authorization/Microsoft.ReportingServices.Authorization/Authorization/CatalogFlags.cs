using System;

namespace Microsoft.ReportingServices.Authorization
{
	// Token: 0x0200001B RID: 27
	internal enum CatalogFlags
	{
		// Token: 0x0400009D RID: 157
		CreateRoles = 1,
		// Token: 0x0400009E RID: 158
		DeleteRoles,
		// Token: 0x0400009F RID: 159
		ReadRoleProperties = 4,
		// Token: 0x040000A0 RID: 160
		UpdateRoleProperties = 8,
		// Token: 0x040000A1 RID: 161
		ReadSystemProperties = 16,
		// Token: 0x040000A2 RID: 162
		UpdateSystemProperties = 32,
		// Token: 0x040000A3 RID: 163
		GenerateEvents = 64,
		// Token: 0x040000A4 RID: 164
		CreateSchedules = 128,
		// Token: 0x040000A5 RID: 165
		DeleteSchedules = 256,
		// Token: 0x040000A6 RID: 166
		ReadSchedules = 512,
		// Token: 0x040000A7 RID: 167
		UpdateSchedules = 1024,
		// Token: 0x040000A8 RID: 168
		ListJobs = 2048,
		// Token: 0x040000A9 RID: 169
		CancelJobs = 4096,
		// Token: 0x040000AA RID: 170
		ExecuteReportDefinition = 8192,
		// Token: 0x040000AB RID: 171
		ReadSystemSecurityPolicy = 131072,
		// Token: 0x040000AC RID: 172
		UpdateSystemSecurityPolicy = 262144
	}
}
