using System;

namespace Microsoft.Mashup.Engine1.Library.ActiveDirectory
{
	// Token: 0x02000FF3 RID: 4083
	public class ActiveDirectoryRootServiceEntry
	{
		// Token: 0x17001EA3 RID: 7843
		// (get) Token: 0x06006B17 RID: 27415 RVA: 0x00171154 File Offset: 0x0016F354
		// (set) Token: 0x06006B18 RID: 27416 RVA: 0x0017115C File Offset: 0x0016F35C
		public string DefaultNamingContext { get; set; }

		// Token: 0x17001EA4 RID: 7844
		// (get) Token: 0x06006B19 RID: 27417 RVA: 0x00171165 File Offset: 0x0016F365
		// (set) Token: 0x06006B1A RID: 27418 RVA: 0x0017116D File Offset: 0x0016F36D
		public string RootDomainNamingContext { get; set; }

		// Token: 0x17001EA5 RID: 7845
		// (get) Token: 0x06006B1B RID: 27419 RVA: 0x00171176 File Offset: 0x0016F376
		// (set) Token: 0x06006B1C RID: 27420 RVA: 0x0017117E File Offset: 0x0016F37E
		public string SchemaNamingContext { get; set; }

		// Token: 0x17001EA6 RID: 7846
		// (get) Token: 0x06006B1D RID: 27421 RVA: 0x00171187 File Offset: 0x0016F387
		// (set) Token: 0x06006B1E RID: 27422 RVA: 0x0017118F File Offset: 0x0016F38F
		public string ConfigurationNamingContext { get; set; }
	}
}
