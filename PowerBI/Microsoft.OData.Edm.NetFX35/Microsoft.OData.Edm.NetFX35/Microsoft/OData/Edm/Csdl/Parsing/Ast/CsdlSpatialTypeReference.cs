using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x0200003E RID: 62
	internal class CsdlSpatialTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x0000399F File Offset: 0x00001B9F
		public CsdlSpatialTypeReference(EdmPrimitiveTypeKind kind, int? srid, string typeName, bool isNullable, CsdlLocation location)
			: base(kind, typeName, isNullable, location)
		{
			this.srid = srid;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000039B4 File Offset: 0x00001BB4
		public int? Srid
		{
			get
			{
				return this.srid;
			}
		}

		// Token: 0x0400005D RID: 93
		private readonly int? srid;
	}
}
