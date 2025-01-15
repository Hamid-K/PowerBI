using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001E4 RID: 484
	internal class CsdlSpatialTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000D6E RID: 3438 RVA: 0x00025D9A File Offset: 0x00023F9A
		public CsdlSpatialTypeReference(EdmPrimitiveTypeKind kind, int? srid, string typeName, bool isNullable, CsdlLocation location)
			: base(kind, typeName, isNullable, location)
		{
			this.srid = srid;
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x00025DAF File Offset: 0x00023FAF
		public int? Srid
		{
			get
			{
				return this.srid;
			}
		}

		// Token: 0x04000765 RID: 1893
		private readonly int? srid;
	}
}
