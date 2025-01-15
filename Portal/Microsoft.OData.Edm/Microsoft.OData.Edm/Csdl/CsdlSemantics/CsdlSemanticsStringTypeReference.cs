using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001AC RID: 428
	internal class CsdlSemanticsStringTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmStringTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000C0B RID: 3083 RVA: 0x0001D690 File Offset: 0x0001B890
		public CsdlSemanticsStringTypeReference(CsdlSemanticsSchema schema, CsdlStringTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00021BDA File Offset: 0x0001FDDA
		public bool IsUnbounded
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).IsUnbounded;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000C0D RID: 3085 RVA: 0x00021BEC File Offset: 0x0001FDEC
		public int? MaxLength
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).MaxLength;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00021BFE File Offset: 0x0001FDFE
		public bool? IsUnicode
		{
			get
			{
				return ((CsdlStringTypeReference)this.Reference).IsUnicode;
			}
		}
	}
}
