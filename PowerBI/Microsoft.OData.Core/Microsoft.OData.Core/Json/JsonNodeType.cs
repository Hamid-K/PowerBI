using System;

namespace Microsoft.OData.Json
{
	// Token: 0x02000216 RID: 534
	public enum JsonNodeType
	{
		// Token: 0x04000A89 RID: 2697
		None,
		// Token: 0x04000A8A RID: 2698
		StartObject,
		// Token: 0x04000A8B RID: 2699
		EndObject,
		// Token: 0x04000A8C RID: 2700
		StartArray,
		// Token: 0x04000A8D RID: 2701
		EndArray,
		// Token: 0x04000A8E RID: 2702
		Property,
		// Token: 0x04000A8F RID: 2703
		PrimitiveValue,
		// Token: 0x04000A90 RID: 2704
		EndOfInput
	}
}
