using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019D RID: 413
	internal class CsdlSemanticsTemporalTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmTemporalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x0001B588 File Offset: 0x00019788
		public CsdlSemanticsTemporalTypeReference(CsdlSemanticsSchema schema, CsdlTemporalTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x0001F688 File Offset: 0x0001D888
		public int? Precision
		{
			get
			{
				return ((CsdlTemporalTypeReference)this.Reference).Precision;
			}
		}
	}
}
