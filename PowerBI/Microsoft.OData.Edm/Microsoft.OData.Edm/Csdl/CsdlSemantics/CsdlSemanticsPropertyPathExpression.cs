using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200018B RID: 395
	internal class CsdlSemanticsPropertyPathExpression : CsdlSemanticsPathExpression
	{
		// Token: 0x06000ACC RID: 2764 RVA: 0x0001B055 File Offset: 0x00019255
		public CsdlSemanticsPropertyPathExpression(CsdlPathExpression expression, IEdmEntityType bindingContext, CsdlSemanticsSchema schema)
			: base(expression, bindingContext, schema)
		{
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000120DC File Offset: 0x000102DC
		public override EdmExpressionKind ExpressionKind
		{
			get
			{
				return EdmExpressionKind.PropertyPath;
			}
		}
	}
}
