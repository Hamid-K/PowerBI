using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001FF RID: 511
	internal class CsdlPrimitiveTypeReference : CsdlNamedTypeReference
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x0002642F File Offset: 0x0002462F
		public CsdlPrimitiveTypeReference(EdmPrimitiveTypeKind kind, string typeName, bool isNullable, CsdlLocation location)
			: base(typeName, isNullable, location)
		{
			this.kind = kind;
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x00026442 File Offset: 0x00024642
		public EdmPrimitiveTypeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x0400079D RID: 1949
		private readonly EdmPrimitiveTypeKind kind;
	}
}
