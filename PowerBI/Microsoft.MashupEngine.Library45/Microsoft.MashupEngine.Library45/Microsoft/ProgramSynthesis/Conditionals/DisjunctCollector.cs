using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Conditionals.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Conditionals.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Conditionals
{
	// Token: 0x02000A09 RID: 2569
	internal sealed class DisjunctCollector : ProgramNodeVisitor<IEnumerable<conjunct>>
	{
		// Token: 0x06003DF5 RID: 15861 RVA: 0x000C0F66 File Offset: 0x000BF166
		public static ArgumentException Error(ProgramNode node)
		{
			return new ArgumentException("Unexpected rule " + node.GrammarRule.Id);
		}

		// Token: 0x06003DF6 RID: 15862 RVA: 0x000C107D File Offset: 0x000BF27D
		public override IEnumerable<conjunct> VisitHole(Hole node)
		{
			throw DisjunctCollector.Error(node);
		}

		// Token: 0x06003DF7 RID: 15863 RVA: 0x000C107D File Offset: 0x000BF27D
		public override IEnumerable<conjunct> VisitLambda(LambdaNode node)
		{
			throw DisjunctCollector.Error(node);
		}

		// Token: 0x06003DF8 RID: 15864 RVA: 0x000C107D File Offset: 0x000BF27D
		public override IEnumerable<conjunct> VisitLet(LetNode node)
		{
			throw DisjunctCollector.Error(node);
		}

		// Token: 0x06003DF9 RID: 15865 RVA: 0x000C107D File Offset: 0x000BF27D
		public override IEnumerable<conjunct> VisitLiteral(LiteralNode node)
		{
			throw DisjunctCollector.Error(node);
		}

		// Token: 0x06003DFA RID: 15866 RVA: 0x000C1088 File Offset: 0x000BF288
		public override IEnumerable<conjunct> VisitNonterminal(NonterminalNode node)
		{
			output output;
			if (Language.Build.Node.Is.output(node, out output))
			{
				return output.Cast_Start().disjunct.Node.AcceptVisitor<IEnumerable<conjunct>>(this);
			}
			disjunct disjunct;
			if (Language.Build.Node.Is.disjunct(node, out disjunct))
			{
				return disjunct.Switch<IEnumerable<conjunct>>(Language.Build, (ConvertDisjunctConjunct conv) => conv.conjunct.Yield<conjunct>(), (Disjunction disjunction) => disjunction.conjunct.Yield<conjunct>().Concat(disjunction.disjunct.Node.AcceptVisitor<IEnumerable<conjunct>>(this)));
			}
			throw DisjunctCollector.Error(node);
		}

		// Token: 0x06003DFB RID: 15867 RVA: 0x000C107D File Offset: 0x000BF27D
		public override IEnumerable<conjunct> VisitVariable(VariableNode node)
		{
			throw DisjunctCollector.Error(node);
		}
	}
}
