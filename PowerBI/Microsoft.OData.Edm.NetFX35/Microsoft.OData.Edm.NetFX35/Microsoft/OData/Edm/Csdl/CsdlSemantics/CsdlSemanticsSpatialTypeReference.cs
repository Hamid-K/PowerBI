using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000BD RID: 189
	internal class CsdlSemanticsSpatialTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmSpatialTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000330 RID: 816 RVA: 0x00007490 File Offset: 0x00005690
		public CsdlSemanticsSpatialTypeReference(CsdlSemanticsSchema schema, CsdlSpatialTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000749A File Offset: 0x0000569A
		public int? SpatialReferenceIdentifier
		{
			get
			{
				return ((CsdlSpatialTypeReference)this.Reference).Srid;
			}
		}
	}
}
