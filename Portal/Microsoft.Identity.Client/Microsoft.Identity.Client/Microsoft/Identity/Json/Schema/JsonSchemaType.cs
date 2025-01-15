using System;

namespace Microsoft.Identity.Json.Schema
{
	// Token: 0x020000B1 RID: 177
	[Flags]
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal enum JsonSchemaType
	{
		// Token: 0x04000348 RID: 840
		None = 0,
		// Token: 0x04000349 RID: 841
		String = 1,
		// Token: 0x0400034A RID: 842
		Float = 2,
		// Token: 0x0400034B RID: 843
		Integer = 4,
		// Token: 0x0400034C RID: 844
		Boolean = 8,
		// Token: 0x0400034D RID: 845
		Object = 16,
		// Token: 0x0400034E RID: 846
		Array = 32,
		// Token: 0x0400034F RID: 847
		Null = 64,
		// Token: 0x04000350 RID: 848
		Any = 127
	}
}
