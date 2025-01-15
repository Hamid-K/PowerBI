using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018E RID: 398
	internal class CsdlSemanticsSpatialTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmSpatialTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000ADB RID: 2779 RVA: 0x0001D690 File Offset: 0x0001B890
		public CsdlSemanticsSpatialTypeReference(CsdlSemanticsSchema schema, CsdlSpatialTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x0001D69A File Offset: 0x0001B89A
		public int? SpatialReferenceIdentifier
		{
			get
			{
				return ((CsdlSpatialTypeReference)this.Reference).Srid;
			}
		}
	}
}
