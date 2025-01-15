using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000116 RID: 278
	public class SerializeOptions
	{
		// Token: 0x060011F8 RID: 4600 RVA: 0x0007E7D0 File Offset: 0x0007C9D0
		internal SerializeOptions Clone()
		{
			return new SerializeOptions
			{
				IgnoreInferredProperties = this.IgnoreInferredProperties,
				IgnoreInferredObjects = this.IgnoreInferredObjects,
				IgnoreChildren = this.IgnoreChildren,
				IgnoreChildrenExceptAnnotations = this.IgnoreChildrenExceptAnnotations,
				IgnoreTimestamps = this.IgnoreTimestamps,
				PartitionsMergedWithTable = this.PartitionsMergedWithTable,
				SplitMultilineStrings = this.SplitMultilineStrings,
				IncludeTranslatablePropertiesOnly = this.IncludeTranslatablePropertiesOnly,
				IncludeRestrictedInformation = this.IncludeRestrictedInformation
			};
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0007E84E File Offset: 0x0007CA4E
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x0007E856 File Offset: 0x0007CA56
		public bool IgnoreInferredProperties { get; set; }

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060011FB RID: 4603 RVA: 0x0007E85F File Offset: 0x0007CA5F
		// (set) Token: 0x060011FC RID: 4604 RVA: 0x0007E867 File Offset: 0x0007CA67
		public bool IgnoreInferredObjects { get; set; }

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x0007E870 File Offset: 0x0007CA70
		// (set) Token: 0x060011FE RID: 4606 RVA: 0x0007E878 File Offset: 0x0007CA78
		public bool IgnoreChildren { get; set; }

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060011FF RID: 4607 RVA: 0x0007E881 File Offset: 0x0007CA81
		// (set) Token: 0x06001200 RID: 4608 RVA: 0x0007E889 File Offset: 0x0007CA89
		public bool IgnoreChildrenExceptAnnotations { get; set; }

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x0007E892 File Offset: 0x0007CA92
		// (set) Token: 0x06001202 RID: 4610 RVA: 0x0007E89A File Offset: 0x0007CA9A
		public bool IgnoreTimestamps { get; set; }

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x0007E8A3 File Offset: 0x0007CAA3
		// (set) Token: 0x06001204 RID: 4612 RVA: 0x0007E8AB File Offset: 0x0007CAAB
		public bool PartitionsMergedWithTable { get; set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x0007E8B4 File Offset: 0x0007CAB4
		// (set) Token: 0x06001206 RID: 4614 RVA: 0x0007E8BC File Offset: 0x0007CABC
		public bool SplitMultilineStrings { get; set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x0007E8C5 File Offset: 0x0007CAC5
		// (set) Token: 0x06001208 RID: 4616 RVA: 0x0007E8CD File Offset: 0x0007CACD
		public bool IncludeTranslatablePropertiesOnly { get; set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x0007E8D6 File Offset: 0x0007CAD6
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x0007E8DE File Offset: 0x0007CADE
		public bool IncludeRestrictedInformation { get; set; }
	}
}
