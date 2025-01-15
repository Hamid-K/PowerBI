using System;
using Microsoft.AnalysisServices.Tabular;

namespace Microsoft.PowerBI.ReportServer.AsServer.ProviderManager
{
	// Token: 0x02000028 RID: 40
	internal class ProviderDataSourceCredentials
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004D50 File Offset: 0x00002F50
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004D58 File Offset: 0x00002F58
		public string Account { get; set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004D61 File Offset: 0x00002F61
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004D69 File Offset: 0x00002F69
		public string Password { get; set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004D72 File Offset: 0x00002F72
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004D7A File Offset: 0x00002F7A
		public string ConnectionString { get; set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004D83 File Offset: 0x00002F83
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004D8B File Offset: 0x00002F8B
		public string Provider { get; set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004D94 File Offset: 0x00002F94
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004D9C File Offset: 0x00002F9C
		public string AuthenticatedUser { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004DA5 File Offset: 0x00002FA5
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00004DAD File Offset: 0x00002FAD
		public ImpersonationMode? ImpersonationMode { get; set; }
	}
}
