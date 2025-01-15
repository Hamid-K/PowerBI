using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B1 RID: 177
	[Flags]
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public enum JsonSchemaType
	{
		// Token: 0x04000362 RID: 866
		None = 0,
		// Token: 0x04000363 RID: 867
		String = 1,
		// Token: 0x04000364 RID: 868
		Float = 2,
		// Token: 0x04000365 RID: 869
		Integer = 4,
		// Token: 0x04000366 RID: 870
		Boolean = 8,
		// Token: 0x04000367 RID: 871
		Object = 16,
		// Token: 0x04000368 RID: 872
		Array = 32,
		// Token: 0x04000369 RID: 873
		Null = 64,
		// Token: 0x0400036A RID: 874
		Any = 127
	}
}
