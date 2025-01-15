using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000022 RID: 34
	internal class CsdlPrimitiveTypeReference : CsdlNamedTypeReference
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00002DD6 File Offset: 0x00000FD6
		public CsdlPrimitiveTypeReference(EdmPrimitiveTypeKind kind, string typeName, bool isNullable, CsdlLocation location)
			: base(typeName, isNullable, location)
		{
			this.kind = kind;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002DE9 File Offset: 0x00000FE9
		public EdmPrimitiveTypeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x04000034 RID: 52
		private readonly EdmPrimitiveTypeKind kind;
	}
}
