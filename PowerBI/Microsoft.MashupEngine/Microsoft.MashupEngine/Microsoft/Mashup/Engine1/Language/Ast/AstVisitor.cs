using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018A0 RID: 6304
	public abstract class AstVisitor : AstVisitor2
	{
		// Token: 0x0600A012 RID: 40978 RVA: 0x002110CB File Offset: 0x0020F2CB
		protected sealed override IExpression VisitConstant(IConstantExpression2 constant)
		{
			return this.VisitConstant((IConstantExpression)constant);
		}

		// Token: 0x0600A013 RID: 40979 RVA: 0x002110D9 File Offset: 0x0020F2D9
		protected virtual IExpression VisitConstant(IConstantExpression constant)
		{
			return base.VisitConstant((IConstantExpression2)constant);
		}
	}
}
