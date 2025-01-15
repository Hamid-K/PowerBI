using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.ExplorationContracts
{
	// Token: 0x02000008 RID: 8
	[DataContract]
	public class Section
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000020FF File Offset: 0x000002FF
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002107 File Offset: 0x00000307
		[DataMember(Name = "visualContainers", EmitDefaultValue = false)]
		public virtual IList<VisualContainer> VisualContainers { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002110 File Offset: 0x00000310
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002118 File Offset: 0x00000318
		[DataMember(Name = "name", EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002121 File Offset: 0x00000321
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002129 File Offset: 0x00000329
		[DataMember(Name = "filters", EmitDefaultValue = false)]
		public string Filters { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002132 File Offset: 0x00000332
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000213A File Offset: 0x0000033A
		[DataMember(Name = "width", EmitDefaultValue = false)]
		public decimal Width { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002143 File Offset: 0x00000343
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000214B File Offset: 0x0000034B
		[DataMember(Name = "height", EmitDefaultValue = false)]
		public decimal Height { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002154 File Offset: 0x00000354
		// (set) Token: 0x06000020 RID: 32 RVA: 0x0000215C File Offset: 0x0000035C
		[DataMember(Name = "config", EmitDefaultValue = false)]
		public string Config { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002165 File Offset: 0x00000365
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000216D File Offset: 0x0000036D
		[DataMember(Name = "displayName", EmitDefaultValue = false)]
		public string DisplayName { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002176 File Offset: 0x00000376
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000217E File Offset: 0x0000037E
		[DataMember(Name = "ordinal", EmitDefaultValue = false)]
		public int Ordinal { get; set; }
	}
}
