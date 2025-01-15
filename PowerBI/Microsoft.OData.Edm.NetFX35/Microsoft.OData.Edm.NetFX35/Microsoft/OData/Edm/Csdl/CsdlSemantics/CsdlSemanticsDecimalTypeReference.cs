using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A6 RID: 422
	internal class CsdlSemanticsDecimalTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmDecimalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x000163B5 File Offset: 0x000145B5
		public CsdlSemanticsDecimalTypeReference(CsdlSemanticsSchema schema, CsdlDecimalTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x0600088E RID: 2190 RVA: 0x000163BF File Offset: 0x000145BF
		public int? Precision
		{
			get
			{
				return ((CsdlDecimalTypeReference)this.Reference).Precision;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x0600088F RID: 2191 RVA: 0x000163D1 File Offset: 0x000145D1
		public int? Scale
		{
			get
			{
				return ((CsdlDecimalTypeReference)this.Reference).Scale;
			}
		}
	}
}
