using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000B3 RID: 179
	internal class CsdlSemanticsPropertyPathExpression : CsdlSemanticsPathExpression
	{
		// Token: 0x06000309 RID: 777 RVA: 0x0000711B File Offset: 0x0000531B
		public CsdlSemanticsPropertyPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(expression, bindingContext, schema)
		{
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00007126 File Offset: 0x00005326
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyPath;
			}
		}
	}
}
