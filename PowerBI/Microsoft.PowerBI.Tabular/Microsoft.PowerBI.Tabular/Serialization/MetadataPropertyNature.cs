using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000184 RID: 388
	[Flags]
	internal enum MetadataPropertyNature
	{
		// Token: 0x0400046C RID: 1132
		None = 0,
		// Token: 0x0400046D RID: 1133
		TypeProperty = 1,
		// Token: 0x0400046E RID: 1134
		NameProperty = 2,
		// Token: 0x0400046F RID: 1135
		RegularProperty = 3,
		// Token: 0x04000470 RID: 1136
		ParentProperty = 4,
		// Token: 0x04000471 RID: 1137
		CrossLinkProperty = 5,
		// Token: 0x04000472 RID: 1138
		ChildProperty = 6,
		// Token: 0x04000473 RID: 1139
		LinkTypeProperty = 7,
		// Token: 0x04000474 RID: 1140
		ChildCollection = 8,
		// Token: 0x04000475 RID: 1141
		Annotations = 9,
		// Token: 0x04000476 RID: 1142
		PropertyCategoryMask = 15,
		// Token: 0x04000477 RID: 1143
		ReadOnly = 256,
		// Token: 0x04000478 RID: 1144
		Inferred = 512,
		// Token: 0x04000479 RID: 1145
		Restricted = 1024,
		// Token: 0x0400047A RID: 1146
		Translatable = 2048,
		// Token: 0x0400047B RID: 1147
		Translation = 4096,
		// Token: 0x0400047C RID: 1148
		Timestamp = 8192,
		// Token: 0x0400047D RID: 1149
		MultilineString = 16384,
		// Token: 0x0400047E RID: 1150
		JsonString = 32768,
		// Token: 0x0400047F RID: 1151
		DefaultProperty = 65536
	}
}
