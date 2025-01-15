using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001FC RID: 508
	internal class CsdlUntypedTypeReference : CsdlNamedTypeReference
	{
		// Token: 0x06000D4A RID: 3402 RVA: 0x00024514 File Offset: 0x00022714
		public CsdlUntypedTypeReference(string typeName, CsdlLocation location)
			: base(typeName, true, location)
		{
		}
	}
}
