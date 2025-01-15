using System;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x02000251 RID: 593
	internal enum JsonNodeType
	{
		// Token: 0x040006ED RID: 1773
		None,
		// Token: 0x040006EE RID: 1774
		StartObject,
		// Token: 0x040006EF RID: 1775
		EndObject,
		// Token: 0x040006F0 RID: 1776
		StartArray,
		// Token: 0x040006F1 RID: 1777
		EndArray,
		// Token: 0x040006F2 RID: 1778
		Property,
		// Token: 0x040006F3 RID: 1779
		PrimitiveValue,
		// Token: 0x040006F4 RID: 1780
		EndOfInput
	}
}
