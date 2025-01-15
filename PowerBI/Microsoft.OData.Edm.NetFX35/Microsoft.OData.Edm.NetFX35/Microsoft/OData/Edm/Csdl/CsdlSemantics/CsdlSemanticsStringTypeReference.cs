using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001DD RID: 477
	internal class CsdlSemanticsStringTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmStringTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000A18 RID: 2584 RVA: 0x0001A8CF File Offset: 0x00018ACF
		public CsdlSemanticsStringTypeReference(CsdlSemanticsSchema schema, CsdlStringTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0001A8D9 File Offset: 0x00018AD9
		public bool IsUnbounded
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).IsUnbounded;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x0001A8EB File Offset: 0x00018AEB
		public int? MaxLength
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).MaxLength;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0001A8FD File Offset: 0x00018AFD
		public bool? IsUnicode
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).IsUnicode;
			}
		}
	}
}
