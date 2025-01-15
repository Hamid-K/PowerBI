using System;
using Microsoft.Cloud.Platform.Configuration;

namespace Microsoft.AnalysisServices.Azure.Common
{
	// Token: 0x02000094 RID: 148
	[Serializable]
	public sealed class SampleModelConfiguration : ConfigurationClass
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00010D14 File Offset: 0x0000EF14
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x00010D1C File Offset: 0x0000EF1C
		[ConfigurationProperty]
		public string ABFFileName { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00010D25 File Offset: 0x0000EF25
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00010D2D File Offset: 0x0000EF2D
		[ConfigurationProperty]
		public string DatabaseName { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00010D36 File Offset: 0x0000EF36
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x00010D3E File Offset: 0x0000EF3E
		[ConfigurationProperty]
		public int ModelMaxMemoryMB { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00010D47 File Offset: 0x0000EF47
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x00010D4F File Offset: 0x0000EF4F
		[ConfigurationProperty]
		public int ModelMaxCPU { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x00010D58 File Offset: 0x0000EF58
		// (set) Token: 0x0600052F RID: 1327 RVA: 0x00010D60 File Offset: 0x0000EF60
		[ConfigurationProperty]
		public int ModelProcessLimitMB { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00010D69 File Offset: 0x0000EF69
		// (set) Token: 0x06000531 RID: 1329 RVA: 0x00010D71 File Offset: 0x0000EF71
		[ConfigurationProperty]
		public string TestQuery { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000532 RID: 1330 RVA: 0x00010D7A File Offset: 0x0000EF7A
		// (set) Token: 0x06000533 RID: 1331 RVA: 0x00010D82 File Offset: 0x0000EF82
		[ConfigurationProperty]
		public string TestQueryResult { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00010D8B File Offset: 0x0000EF8B
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x00010D93 File Offset: 0x0000EF93
		[ConfigurationProperty]
		public string TestUtterance { get; set; }
	}
}
