using System;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008CD RID: 2253
	public class LambdaNode : NonterminalNode
	{
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x0008EA6E File Offset: 0x0008CC6E
		public ProgramNode BodyNode
		{
			get
			{
				return this.Children[0];
			}
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x0008EA78 File Offset: 0x0008CC78
		public LambdaNode(LambdaRule rule, ProgramNode bodyNode)
			: base(rule, new ProgramNode[] { bodyNode })
		{
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x0008EA8B File Offset: 0x0008CC8B
		protected override object Evaluate(State state)
		{
			LambdaNode.<>c__DisplayClass3_0 CS$<>8__locals1 = new LambdaNode.<>c__DisplayClass3_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.state = state;
			CS$<>8__locals1.variable = ((LambdaRule)base.Rule).Variable;
			return new FunctionalSymbol1(new Func<object, object>(CS$<>8__locals1.<Evaluate>g__Lambda|0));
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x0008EAC6 File Offset: 0x0008CCC6
		public override ProgramNode Clone()
		{
			return new LambdaNode(base.Rule as LambdaRule, this.BodyNode.Clone());
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x0008EAE3 File Offset: 0x0008CCE3
		public override T AcceptVisitor<T>(ProgramNodeVisitor<T> visitor)
		{
			return visitor.VisitLambda(this);
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x0008EAEC File Offset: 0x0008CCEC
		public override TResult AcceptVisitor<TResult, TArgs>(ProgramNodeVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitLambda(this, args);
		}
	}
}
