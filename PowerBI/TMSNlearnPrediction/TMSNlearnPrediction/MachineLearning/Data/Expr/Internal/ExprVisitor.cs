using System;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x02000190 RID: 400
	internal abstract class ExprVisitor : NodeVisitor
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x0002D520 File Offset: 0x0002B720
		public override void Visit(NameNode node)
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0002D522 File Offset: 0x0002B722
		public override void Visit(ParamNode node)
		{
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0002D524 File Offset: 0x0002B724
		public override void PostVisit(LambdaNode node)
		{
		}
	}
}
