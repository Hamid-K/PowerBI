using System;

namespace Microsoft.Identity.Json.Schema
{
	// Token: 0x020000B3 RID: 179
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal enum UndefinedSchemaIdHandling
	{
		// Token: 0x04000354 RID: 852
		None,
		// Token: 0x04000355 RID: 853
		UseTypeName,
		// Token: 0x04000356 RID: 854
		UseAssemblyQualifiedName
	}
}
