using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001F2 RID: 498
	internal class CsdlPrimitiveTypeReference : CsdlNamedTypeReference
	{
		// Token: 0x06000D2A RID: 3370 RVA: 0x000242F4 File Offset: 0x000224F4
		public CsdlPrimitiveTypeReference(EdmPrimitiveTypeKind kind, string typeName, bool isNullable, CsdlLocation location)
			: base(typeName, isNullable, location)
		{
			this.kind = kind;
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00024307 File Offset: 0x00022507
		public EdmPrimitiveTypeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x04000727 RID: 1831
		private readonly EdmPrimitiveTypeKind kind;
	}
}
