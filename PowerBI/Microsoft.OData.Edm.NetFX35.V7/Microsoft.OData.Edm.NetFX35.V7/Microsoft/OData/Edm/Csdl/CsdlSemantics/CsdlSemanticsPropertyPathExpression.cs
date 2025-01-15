using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200017C RID: 380
	internal class CsdlSemanticsPropertyPathExpression : CsdlSemanticsPathExpression
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x000194AD File Offset: 0x000176AD
		public CsdlSemanticsPropertyPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(expression, bindingContext, schema)
		{
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00013BF8 File Offset: 0x00011DF8
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyPath;
			}
		}
	}
}
