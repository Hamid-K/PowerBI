using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000014 RID: 20
	[DataContract]
	internal sealed class CustomPVProperties
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000379C File Offset: 0x0000199C
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000037A4 File Offset: 0x000019A4
		public List<PVPropertyValue> SortValue { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000037AD File Offset: 0x000019AD
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000037B5 File Offset: 0x000019B5
		public PVFilter FilterValue { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x000037BE File Offset: 0x000019BE
		// (set) Token: 0x0600005F RID: 95 RVA: 0x000037C6 File Offset: 0x000019C6
		public string StringValue { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000037CF File Offset: 0x000019CF
		// (set) Token: 0x06000061 RID: 97 RVA: 0x000037D7 File Offset: 0x000019D7
		public bool BooleanValue { get; set; }
	}
}
