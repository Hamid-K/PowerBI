using System;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x02000095 RID: 149
	internal class CsdlSemanticsSpatialTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmSpatialTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600026D RID: 621 RVA: 0x00006201 File Offset: 0x00004401
		public CsdlSemanticsSpatialTypeReference(CsdlSemanticsSchema schema, CsdlSpatialTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000620B File Offset: 0x0000440B
		public int? SpatialReferenceIdentifier
		{
			get
			{
				return ((CsdlSpatialTypeReference)this.Reference).Srid;
			}
		}
	}
}
