using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.AST.Visitors
{
	// Token: 0x020008EC RID: 2284
	public class RuleCountVisitor : ProgramNodeVisitor<IReadOnlyDictionary<GrammarRule, int>>
	{
		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x000919CB File Offset: 0x0008FBCB
		public static RuleCountVisitor Instance { get; } = new RuleCountVisitor();

		// Token: 0x0600315A RID: 12634 RVA: 0x000919D2 File Offset: 0x0008FBD2
		private RuleCountVisitor()
		{
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000919DC File Offset: 0x0008FBDC
		public override IReadOnlyDictionary<GrammarRule, int> VisitNonterminal(NonterminalNode node)
		{
			Dictionary<GrammarRule, int> dictionary = new Dictionary<GrammarRule, int>();
			GrammarRule rule = node.Rule;
			dictionary[rule] = 1;
			return dictionary.MultisetUnion(node.Children.Select((ProgramNode child) => child.AcceptVisitor<IReadOnlyDictionary<GrammarRule, int>>(this)).Aggregate(RuleCountVisitor.Empty, (IReadOnlyDictionary<GrammarRule, int> a, IReadOnlyDictionary<GrammarRule, int> b) => a.MultisetUnion(b, null)), null);
		}

		// Token: 0x0600315C RID: 12636 RVA: 0x00091A44 File Offset: 0x0008FC44
		public override IReadOnlyDictionary<GrammarRule, int> VisitLet(LetNode node)
		{
			Dictionary<GrammarRule, int> dictionary = new Dictionary<GrammarRule, int>();
			GrammarRule rule = node.Rule;
			dictionary[rule] = 1;
			return dictionary.MultisetUnion(node.Children.Select((ProgramNode child) => child.AcceptVisitor<IReadOnlyDictionary<GrammarRule, int>>(this)).Aggregate(RuleCountVisitor.Empty, (IReadOnlyDictionary<GrammarRule, int> a, IReadOnlyDictionary<GrammarRule, int> b) => a.MultisetUnion(b, null)), null);
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x00091AAC File Offset: 0x0008FCAC
		public override IReadOnlyDictionary<GrammarRule, int> VisitLambda(LambdaNode node)
		{
			Dictionary<GrammarRule, int> dictionary = new Dictionary<GrammarRule, int>();
			GrammarRule rule = node.Rule;
			dictionary[rule] = 1;
			return dictionary.MultisetUnion(node.Children.Select((ProgramNode child) => child.AcceptVisitor<IReadOnlyDictionary<GrammarRule, int>>(this)).Aggregate(RuleCountVisitor.Empty, (IReadOnlyDictionary<GrammarRule, int> a, IReadOnlyDictionary<GrammarRule, int> b) => a.MultisetUnion(b, null)), null);
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x00091B13 File Offset: 0x0008FD13
		public override IReadOnlyDictionary<GrammarRule, int> VisitLiteral(LiteralNode node)
		{
			return RuleCountVisitor.Empty;
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x00091B13 File Offset: 0x0008FD13
		public override IReadOnlyDictionary<GrammarRule, int> VisitVariable(VariableNode node)
		{
			return RuleCountVisitor.Empty;
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x00091B13 File Offset: 0x0008FD13
		public override IReadOnlyDictionary<GrammarRule, int> VisitHole(Hole node)
		{
			return RuleCountVisitor.Empty;
		}

		// Token: 0x04001896 RID: 6294
		private static readonly IReadOnlyDictionary<GrammarRule, int> Empty = new Dictionary<GrammarRule, int>();
	}
}
