using System;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018AB RID: 6315
	public abstract class LogicalAstVisitor<TBinding> : LogicalAstVisitor2<TBinding>
	{
		// Token: 0x0600A0C5 RID: 41157 RVA: 0x00214BCC File Offset: 0x00212DCC
		protected sealed override IExpression VisitConstant(IConstantExpression2 constant)
		{
			return this.VisitConstant((IConstantExpression)constant);
		}

		// Token: 0x0600A0C6 RID: 41158 RVA: 0x002110D9 File Offset: 0x0020F2D9
		protected virtual IExpression VisitConstant(IConstantExpression constant)
		{
			return base.VisitConstant((IConstantExpression2)constant);
		}
	}
}
