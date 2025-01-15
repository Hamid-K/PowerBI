using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200003D RID: 61
	internal class CsdlPrimitiveTypeReference : CsdlNamedTypeReference
	{
		// Token: 0x060000F5 RID: 245 RVA: 0x00003984 File Offset: 0x00001B84
		public CsdlPrimitiveTypeReference(EdmPrimitiveTypeKind kind, string typeName, bool isNullable, CsdlLocation location)
			: base(typeName, isNullable, location)
		{
			this.kind = kind;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003997 File Offset: 0x00001B97
		public EdmPrimitiveTypeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x0400005C RID: 92
		private readonly EdmPrimitiveTypeKind kind;
	}
}
