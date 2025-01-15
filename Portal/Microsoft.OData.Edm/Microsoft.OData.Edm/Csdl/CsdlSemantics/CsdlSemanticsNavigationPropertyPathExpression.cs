using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000168 RID: 360
	internal class CsdlSemanticsNavigationPropertyPathExpression : CsdlSemanticsPathExpression
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x0001B055 File Offset: 0x00019255
		public CsdlSemanticsNavigationPropertyPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(expression, bindingContext, schema)
		{
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00012081 File Offset: 0x00010281
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.NavigationPropertyPath;
			}
		}
	}
}
