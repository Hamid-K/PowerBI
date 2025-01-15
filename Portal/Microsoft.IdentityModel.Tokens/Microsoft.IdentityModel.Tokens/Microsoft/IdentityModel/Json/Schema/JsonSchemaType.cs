using System;

namespace Microsoft.IdentityModel.Json.Schema
{
	// Token: 0x020000B2 RID: 178
	[Flags]
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal enum JsonSchemaType
	{
		// Token: 0x04000363 RID: 867
		None = 0,
		// Token: 0x04000364 RID: 868
		String = 1,
		// Token: 0x04000365 RID: 869
		Float = 2,
		// Token: 0x04000366 RID: 870
		Integer = 4,
		// Token: 0x04000367 RID: 871
		Boolean = 8,
		// Token: 0x04000368 RID: 872
		Object = 16,
		// Token: 0x04000369 RID: 873
		Array = 32,
		// Token: 0x0400036A RID: 874
		Null = 64,
		// Token: 0x0400036B RID: 875
		Any = 127
	}
}
