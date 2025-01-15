using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000196 RID: 406
	internal class CsdlSemanticsBinaryTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmBinaryTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000B26 RID: 2854 RVA: 0x0001D690 File Offset: 0x0001B890
		public CsdlSemanticsBinaryTypeReference(CsdlSemanticsSchema schema, CsdlBinaryTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0001E40E File Offset: 0x0001C60E
		public bool IsUnbounded
		{
			get
			{
				return ((CsdlBinaryTypeReference)this.Reference).IsUnbounded;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0001E420 File Offset: 0x0001C620
		public int? MaxLength
		{
			get
			{
				return ((CsdlBinaryTypeReference)this.Reference).MaxLength;
			}
		}
	}
}
