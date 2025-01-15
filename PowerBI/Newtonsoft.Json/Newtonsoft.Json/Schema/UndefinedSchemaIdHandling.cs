using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020000B3 RID: 179
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public enum UndefinedSchemaIdHandling
	{
		// Token: 0x0400036E RID: 878
		None,
		// Token: 0x0400036F RID: 879
		UseTypeName,
		// Token: 0x04000370 RID: 880
		UseAssemblyQualifiedName
	}
}
