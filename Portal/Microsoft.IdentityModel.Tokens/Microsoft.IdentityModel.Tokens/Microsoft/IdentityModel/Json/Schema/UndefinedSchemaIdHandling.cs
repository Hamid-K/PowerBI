using System;

namespace Microsoft.IdentityModel.Json.Schema
{
	// Token: 0x020000B4 RID: 180
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal enum UndefinedSchemaIdHandling
	{
		// Token: 0x0400036F RID: 879
		None,
		// Token: 0x04000370 RID: 880
		UseTypeName,
		// Token: 0x04000371 RID: 881
		UseAssemblyQualifiedName
	}
}
