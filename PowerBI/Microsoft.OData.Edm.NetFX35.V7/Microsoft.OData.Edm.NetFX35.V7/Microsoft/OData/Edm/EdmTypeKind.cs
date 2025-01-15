using System;

namespace Microsoft.OData.Edm
{
	// Token: 0x020000C2 RID: 194
	public enum EdmTypeKind
	{
		// Token: 0x0400017C RID: 380
		None,
		// Token: 0x0400017D RID: 381
		Primitive,
		// Token: 0x0400017E RID: 382
		Entity,
		// Token: 0x0400017F RID: 383
		Complex,
		// Token: 0x04000180 RID: 384
		Collection,
		// Token: 0x04000181 RID: 385
		EntityReference,
		// Token: 0x04000182 RID: 386
		Enum,
		// Token: 0x04000183 RID: 387
		TypeDefinition,
		// Token: 0x04000184 RID: 388
		Untyped
	}
}
