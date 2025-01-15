using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000A1 RID: 161
	internal class CsdlSemanticsCollectionTypeExpression : CsdlSemanticsTypeExpression, IEdmCollectionTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x00006AA0 File Offset: 0x00004CA0
		public CsdlSemanticsCollectionTypeExpression(CsdlExpressionTypeReference expressionUsage, CsdlSemanticsTypeDefinition type)
			: base(expressionUsage, type)
		{
		}
	}
}
