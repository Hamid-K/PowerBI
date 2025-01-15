using System;

namespace Microsoft.OData.Json
{
	// Token: 0x020001E4 RID: 484
	public enum JsonNodeType
	{
		// Token: 0x040009A9 RID: 2473
		None,
		// Token: 0x040009AA RID: 2474
		StartObject,
		// Token: 0x040009AB RID: 2475
		EndObject,
		// Token: 0x040009AC RID: 2476
		StartArray,
		// Token: 0x040009AD RID: 2477
		EndArray,
		// Token: 0x040009AE RID: 2478
		Property,
		// Token: 0x040009AF RID: 2479
		PrimitiveValue,
		// Token: 0x040009B0 RID: 2480
		EndOfInput
	}
}
