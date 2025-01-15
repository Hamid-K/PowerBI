using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000D5 RID: 213
	internal class CsdlSemanticsEntityReferenceTypeExpression : CsdlSemanticsTypeExpression, IEdmEntityReferenceTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060003CD RID: 973 RVA: 0x000098A5 File Offset: 0x00007AA5
		public CsdlSemanticsEntityReferenceTypeExpression(CsdlExpressionTypeReference expressionUsage, CsdlSemanticsTypeDefinition type)
			: base(expressionUsage, type)
		{
		}
	}
}
