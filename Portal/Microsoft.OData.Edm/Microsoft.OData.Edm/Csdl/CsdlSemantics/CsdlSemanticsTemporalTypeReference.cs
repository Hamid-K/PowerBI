using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AE RID: 430
	internal class CsdlSemanticsTemporalTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmTemporalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000C20 RID: 3104 RVA: 0x0001D690 File Offset: 0x0001B890
		public CsdlSemanticsTemporalTypeReference(CsdlSemanticsSchema schema, CsdlTemporalTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x00021E14 File Offset: 0x00020014
		public int? Precision
		{
			get
			{
				return ((CsdlTemporalTypeReference)this.Reference).Precision;
			}
		}
	}
}
