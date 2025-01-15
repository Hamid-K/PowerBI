using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ExplorationContracts
{
	// Token: 0x02000005 RID: 5
	[DataContract]
	public class Exploration
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		[DataMember(Name = "displayName", EmitDefaultValue = false)]
		public string DisplayName { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002078 File Offset: 0x00000278
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002080 File Offset: 0x00000280
		[DataMember(Name = "theme", EmitDefaultValue = false)]
		public string Theme { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002089 File Offset: 0x00000289
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002091 File Offset: 0x00000291
		[DataMember(Name = "sections", EmitDefaultValue = false)]
		public virtual IList<Section> Sections { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000209A File Offset: 0x0000029A
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020A2 File Offset: 0x000002A2
		[DataMember(Name = "config", EmitDefaultValue = false)]
		public string Config { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020AB File Offset: 0x000002AB
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020B3 File Offset: 0x000002B3
		[DataMember(Name = "compatibilityInfo", EmitDefaultValue = false)]
		public IList<ExplorationCompatibilityInfo> CompatibilityInfo { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020BC File Offset: 0x000002BC
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020C4 File Offset: 0x000002C4
		[DataMember(Name = "filters", EmitDefaultValue = false)]
		public string Filters { get; set; }
	}
}
