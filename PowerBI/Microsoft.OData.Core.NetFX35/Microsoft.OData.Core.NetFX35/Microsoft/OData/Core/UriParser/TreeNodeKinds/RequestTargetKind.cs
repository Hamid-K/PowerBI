using System;

namespace Microsoft.OData.Core.UriParser.TreeNodeKinds
{
	// Token: 0x0200028A RID: 650
	internal enum RequestTargetKind
	{
		// Token: 0x040009C4 RID: 2500
		Nothing,
		// Token: 0x040009C5 RID: 2501
		ServiceDirectory,
		// Token: 0x040009C6 RID: 2502
		Resource,
		// Token: 0x040009C7 RID: 2503
		ComplexObject,
		// Token: 0x040009C8 RID: 2504
		Primitive,
		// Token: 0x040009C9 RID: 2505
		PrimitiveValue,
		// Token: 0x040009CA RID: 2506
		Enum,
		// Token: 0x040009CB RID: 2507
		EnumValue,
		// Token: 0x040009CC RID: 2508
		Metadata,
		// Token: 0x040009CD RID: 2509
		VoidOperation,
		// Token: 0x040009CE RID: 2510
		Batch,
		// Token: 0x040009CF RID: 2511
		OpenProperty,
		// Token: 0x040009D0 RID: 2512
		OpenPropertyValue,
		// Token: 0x040009D1 RID: 2513
		MediaResource,
		// Token: 0x040009D2 RID: 2514
		Collection
	}
}
