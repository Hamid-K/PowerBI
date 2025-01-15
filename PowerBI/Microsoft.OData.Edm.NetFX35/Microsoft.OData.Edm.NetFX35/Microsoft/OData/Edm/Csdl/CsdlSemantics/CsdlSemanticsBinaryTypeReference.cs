using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A3 RID: 419
	internal class CsdlSemanticsBinaryTypeReference : CsdlSemanticsPrimitiveTypeReference, IEdmBinaryTypeReference, IEdmPrimitiveTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600086B RID: 2155 RVA: 0x00016043 File Offset: 0x00014243
		public CsdlSemanticsBinaryTypeReference(CsdlSemanticsSchema schema, CsdlBinaryTypeReference reference)
			: base(schema, reference)
		{
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x0001604D File Offset: 0x0001424D
		public bool IsUnbounded
		{
			get
			{
				return ((CsdlBinaryTypeReference)this.Reference).IsUnbounded;
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0001605F File Offset: 0x0001425F
		public int? MaxLength
		{
			get
			{
				return ((CsdlBinaryTypeReference)this.Reference).MaxLength;
			}
		}
	}
}
