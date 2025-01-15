using System;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x02000114 RID: 276
	internal enum JsonNodeType
	{
		// Token: 0x04000438 RID: 1080
		None,
		// Token: 0x04000439 RID: 1081
		StartObject,
		// Token: 0x0400043A RID: 1082
		EndObject,
		// Token: 0x0400043B RID: 1083
		StartArray,
		// Token: 0x0400043C RID: 1084
		EndArray,
		// Token: 0x0400043D RID: 1085
		Property,
		// Token: 0x0400043E RID: 1086
		PrimitiveValue,
		// Token: 0x0400043F RID: 1087
		EndOfInput
	}
}
