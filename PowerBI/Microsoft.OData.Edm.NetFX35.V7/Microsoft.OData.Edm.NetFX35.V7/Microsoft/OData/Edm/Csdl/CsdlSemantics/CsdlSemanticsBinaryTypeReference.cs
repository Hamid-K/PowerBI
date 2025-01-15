using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000186 RID: 390
	internal class CsdlSemanticsBinaryTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmBinaryTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000A5D RID: 2653 RVA: 0x0001B588 File Offset: 0x00019788
		public CsdlSemanticsBinaryTypeReference(CsdlSemanticsSchema schema, CsdlBinaryTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0001C0F2 File Offset: 0x0001A2F2
		public bool IsUnbounded
		{
			get
			{
				return ((CsdlBinaryTypeReference)this.Reference).IsUnbounded;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000A5F RID: 2655 RVA: 0x0001C104 File Offset: 0x0001A304
		public int? MaxLength
		{
			get
			{
				return ((CsdlBinaryTypeReference)this.Reference).MaxLength;
			}
		}
	}
}
