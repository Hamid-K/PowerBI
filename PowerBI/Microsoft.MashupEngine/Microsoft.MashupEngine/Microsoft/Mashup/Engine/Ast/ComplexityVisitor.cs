using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B86 RID: 7046
	public sealed class ComplexityVisitor : AstVisitor2
	{
		// Token: 0x0600B07A RID: 45178 RVA: 0x00242B17 File Offset: 0x00240D17
		public static int AnalyzeComplexity(IExpression node)
		{
			ComplexityVisitor complexityVisitor = new ComplexityVisitor();
			complexityVisitor.VisitExpression(node);
			return complexityVisitor.complexity;
		}

		// Token: 0x0600B07B RID: 45179 RVA: 0x00242B2B File Offset: 0x00240D2B
		protected override IExpression VisitExpression(IExpression node)
		{
			this.complexity++;
			return base.VisitExpression(node);
		}

		// Token: 0x04005AC6 RID: 23238
		private int complexity;
	}
}
