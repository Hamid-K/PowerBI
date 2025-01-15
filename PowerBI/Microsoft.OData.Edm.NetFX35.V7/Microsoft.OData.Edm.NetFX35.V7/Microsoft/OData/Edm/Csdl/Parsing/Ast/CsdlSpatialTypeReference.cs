using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Ast
{
	// Token: 0x020001D5 RID: 469
	internal class CsdlSpatialTypeReference : CsdlPrimitiveTypeReference
	{
		// Token: 0x06000CB9 RID: 3257 RVA: 0x00023BD4 File Offset: 0x00021DD4
		public CsdlSpatialTypeReference(EdmPrimitiveTypeKind kind, int? srid, string typeName, bool isNullable, CsdlLocation location)
			: base(kind, typeName, isNullable, location)
		{
			this.srid = srid;
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00023BE9 File Offset: 0x00021DE9
		public int? Srid
		{
			get
			{
				return this.srid;
			}
		}

		// Token: 0x040006EC RID: 1772
		private readonly int? srid;
	}
}
