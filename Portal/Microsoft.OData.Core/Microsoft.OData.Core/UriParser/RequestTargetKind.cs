using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001D5 RID: 469
	internal enum RequestTargetKind
	{
		// Token: 0x040009CC RID: 2508
		Nothing,
		// Token: 0x040009CD RID: 2509
		ServiceDirectory,
		// Token: 0x040009CE RID: 2510
		Resource,
		// Token: 0x040009CF RID: 2511
		Primitive,
		// Token: 0x040009D0 RID: 2512
		PrimitiveValue,
		// Token: 0x040009D1 RID: 2513
		Enum,
		// Token: 0x040009D2 RID: 2514
		EnumValue,
		// Token: 0x040009D3 RID: 2515
		Metadata,
		// Token: 0x040009D4 RID: 2516
		VoidOperation,
		// Token: 0x040009D5 RID: 2517
		Batch,
		// Token: 0x040009D6 RID: 2518
		Dynamic,
		// Token: 0x040009D7 RID: 2519
		DynamicValue,
		// Token: 0x040009D8 RID: 2520
		MediaResource,
		// Token: 0x040009D9 RID: 2521
		Collection
	}
}
