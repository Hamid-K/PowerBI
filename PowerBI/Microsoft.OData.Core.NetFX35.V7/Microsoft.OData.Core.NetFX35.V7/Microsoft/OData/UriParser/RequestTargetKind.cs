using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000187 RID: 391
	internal enum RequestTargetKind
	{
		// Token: 0x04000874 RID: 2164
		Nothing,
		// Token: 0x04000875 RID: 2165
		ServiceDirectory,
		// Token: 0x04000876 RID: 2166
		Resource,
		// Token: 0x04000877 RID: 2167
		Primitive,
		// Token: 0x04000878 RID: 2168
		PrimitiveValue,
		// Token: 0x04000879 RID: 2169
		Enum,
		// Token: 0x0400087A RID: 2170
		EnumValue,
		// Token: 0x0400087B RID: 2171
		Metadata,
		// Token: 0x0400087C RID: 2172
		VoidOperation,
		// Token: 0x0400087D RID: 2173
		Batch,
		// Token: 0x0400087E RID: 2174
		Dynamic,
		// Token: 0x0400087F RID: 2175
		DynamicValue,
		// Token: 0x04000880 RID: 2176
		MediaResource,
		// Token: 0x04000881 RID: 2177
		Collection
	}
}
