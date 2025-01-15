using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200019B RID: 411
	internal class CsdlSemanticsStringTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmStringTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000B34 RID: 2868 RVA: 0x0001B588 File Offset: 0x00019788
		public CsdlSemanticsStringTypeReference(CsdlSemanticsSchema schema, CsdlStringTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000B35 RID: 2869 RVA: 0x0001F44E File Offset: 0x0001D64E
		public bool IsUnbounded
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).IsUnbounded;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001F460 File Offset: 0x0001D660
		public int? MaxLength
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).MaxLength;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0001F472 File Offset: 0x0001D672
		public bool? IsUnicode
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).IsUnicode;
			}
		}
	}
}
