using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017F RID: 383
	internal class CsdlSemanticsSpatialTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmSpatialTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000A1F RID: 2591 RVA: 0x0001B588 File Offset: 0x00019788
		public CsdlSemanticsSpatialTypeReference(CsdlSemanticsSchema schema, CsdlSpatialTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0001B592 File Offset: 0x00019792
		public int? SpatialReferenceIdentifier
		{
			get
			{
				return ((CsdlSpatialTypeReference)this.Reference).Srid;
			}
		}
	}
}
