using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000A8 RID: 168
	public enum EdmTypeKind
	{
		// Token: 0x0400012E RID: 302
		None,
		// Token: 0x0400012F RID: 303
		Primitive,
		// Token: 0x04000130 RID: 304
		Entity,
		// Token: 0x04000131 RID: 305
		Complex,
		// Token: 0x04000132 RID: 306
		Collection,
		// Token: 0x04000133 RID: 307
		EntityReference,
		// Token: 0x04000134 RID: 308
		Enum,
		// Token: 0x04000135 RID: 309
		TypeDefinition,
		// Token: 0x04000136 RID: 310
		Untyped,
		// Token: 0x04000137 RID: 311
		Path
	}
}
