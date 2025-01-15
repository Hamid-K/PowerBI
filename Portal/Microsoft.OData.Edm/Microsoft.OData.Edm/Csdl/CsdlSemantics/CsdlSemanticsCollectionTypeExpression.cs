using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000182 RID: 386
	internal class CsdlSemanticsCollectionTypeExpression : CsdlSemanticsTypeExpression, IEdmCollectionTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x06000A87 RID: 2695 RVA: 0x0001CE43 File Offset: 0x0001B043
		public CsdlSemanticsCollectionTypeExpression(CsdlExpressionTypeReference expressionUsage, CsdlSemanticsTypeDefinition type)
			: base(expressionUsage, type)
		{
		}
	}
}
