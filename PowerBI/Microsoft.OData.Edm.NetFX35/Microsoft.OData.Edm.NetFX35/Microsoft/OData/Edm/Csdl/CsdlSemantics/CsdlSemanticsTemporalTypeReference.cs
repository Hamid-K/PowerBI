using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001DE RID: 478
	internal class CsdlSemanticsTemporalTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmTemporalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000A1C RID: 2588 RVA: 0x0001A90F File Offset: 0x00018B0F
		public CsdlSemanticsTemporalTypeReference(CsdlSemanticsSchema schema, CsdlTemporalTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0001A919 File Offset: 0x00018B19
		public int? Precision
		{
			get
			{
				return ((CsdlTemporalTypeReference)this.Reference).Precision;
			}
		}
	}
}
