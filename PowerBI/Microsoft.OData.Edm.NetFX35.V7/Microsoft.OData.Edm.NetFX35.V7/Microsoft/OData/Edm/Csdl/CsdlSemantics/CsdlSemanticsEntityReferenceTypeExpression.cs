using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000184 RID: 388
	internal class CsdlSemanticsEntityReferenceTypeExpression : CsdlSemanticsTypeExpression, IEdmEntityReferenceTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x0001ACFB File Offset: 0x00018EFB
		public CsdlSemanticsEntityReferenceTypeExpression(CsdlExpressionTypeReference expressionUsage, CsdlSemanticsTypeDefinition type)
			: base(expressionUsage, type)
		{
		}
	}
}
