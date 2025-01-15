using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001C9 RID: 457
	internal class CsdlUntypedTypeReference : CsdlNamedTypeReference
	{
		// Token: 0x06000D1D RID: 3357 RVA: 0x000259A8 File Offset: 0x00023BA8
		public CsdlUntypedTypeReference(string typeName, CsdlLocation location)
			: base(typeName, true, location)
		{
		}
	}
}
