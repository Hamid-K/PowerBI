using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000198 RID: 408
	internal class CsdlSemanticsDecimalTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmDecimalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000B33 RID: 2867 RVA: 0x0001D690 File Offset: 0x0001B890
		public CsdlSemanticsDecimalTypeReference(CsdlSemanticsSchema schema, CsdlDecimalTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0001E568 File Offset: 0x0001C768
		public int? Precision
		{
			get
			{
				return ((CsdlDecimalTypeReference)this.Reference).Precision;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0001E57A File Offset: 0x0001C77A
		public int? Scale
		{
			get
			{
				return ((CsdlDecimalTypeReference)this.Reference).Scale;
			}
		}
	}
}
