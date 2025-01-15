using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000188 RID: 392
	internal class CsdlSemanticsDecimalTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmDecimalTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000A69 RID: 2665 RVA: 0x0001B588 File Offset: 0x00019788
		public CsdlSemanticsDecimalTypeReference(CsdlSemanticsSchema schema, CsdlDecimalTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x0001C210 File Offset: 0x0001A410
		public int? Precision
		{
			get
			{
				return ((CsdlDecimalTypeReference)this.Reference).Precision;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x0001C222 File Offset: 0x0001A422
		public int? Scale
		{
			get
			{
				return ((CsdlDecimalTypeReference)this.Reference).Scale;
			}
		}
	}
}
