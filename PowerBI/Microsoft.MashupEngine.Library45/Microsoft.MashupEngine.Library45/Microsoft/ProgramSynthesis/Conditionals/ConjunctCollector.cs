using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Conditionals.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Conditionals
{
	// Token: 0x02000A07 RID: 2567
	internal sealed class ConjunctCollector : ProgramNodeVisitor<IEnumerable<pred>>
	{
		// Token: 0x06003DE9 RID: 15849 RVA: 0x000C0F66 File Offset: 0x000BF166
		public static ArgumentException Error(ProgramNode node)
		{
			return new ArgumentException("Unexpected rule " + node.GrammarRule.Id);
		}

		// Token: 0x06003DEA RID: 15850 RVA: 0x000C0F82 File Offset: 0x000BF182
		public override IEnumerable<pred> VisitHole(Hole node)
		{
			throw ConjunctCollector.Error(node);
		}

		// Token: 0x06003DEB RID: 15851 RVA: 0x000C0F82 File Offset: 0x000BF182
		public override IEnumerable<pred> VisitLambda(LambdaNode node)
		{
			throw ConjunctCollector.Error(node);
		}

		// Token: 0x06003DEC RID: 15852 RVA: 0x000C0F82 File Offset: 0x000BF182
		public override IEnumerable<pred> VisitLet(LetNode node)
		{
			throw ConjunctCollector.Error(node);
		}

		// Token: 0x06003DED RID: 15853 RVA: 0x000C0F82 File Offset: 0x000BF182
		public override IEnumerable<pred> VisitLiteral(LiteralNode node)
		{
			throw ConjunctCollector.Error(node);
		}

		// Token: 0x06003DEE RID: 15854 RVA: 0x000C0F8C File Offset: 0x000BF18C
		public override IEnumerable<pred> VisitNonterminal(NonterminalNode node)
		{
			conjunct conjunct;
			if (Language.Build.Node.Is.conjunct(node, out conjunct))
			{
				return conjunct.Cast_Conjunct().baseConjunct.Node.AcceptVisitor<IEnumerable<pred>>(this);
			}
			baseConjunct baseConjunct;
			if (Language.Build.Node.Is.baseConjunct(node, out baseConjunct))
			{
				return baseConjunct.Switch<IEnumerable<pred>>(Language.Build, (baseConjunct_pred conv) => conv.pred.Yield<pred>(), (Conjunction conjunction) => conjunction.pred.Yield<pred>().Concat(conjunction.baseConjunct.Node.AcceptVisitor<IEnumerable<pred>>(this)));
			}
			throw ConjunctCollector.Error(node);
		}

		// Token: 0x06003DEF RID: 15855 RVA: 0x000C0F82 File Offset: 0x000BF182
		public override IEnumerable<pred> VisitVariable(VariableNode node)
		{
			throw ConjunctCollector.Error(node);
		}
	}
}
