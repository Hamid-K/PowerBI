using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000508 RID: 1288
	internal class AlternativesGenerator : ProgramNodeVisitor<IEnumerable<ProgramNode>>
	{
		// Token: 0x06001CB6 RID: 7350 RVA: 0x00055AAB File Offset: 0x00053CAB
		public AlternativesGenerator(Func<ProgramNode, IEnumerable<ProgramNode>> alternativesFunc)
		{
			this._alternativesFunc = alternativesFunc;
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x00055ABA File Offset: 0x00053CBA
		public override IEnumerable<ProgramNode> VisitLiteral(LiteralNode node)
		{
			return this._alternativesFunc(node);
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00002188 File Offset: 0x00000388
		public override IEnumerable<ProgramNode> VisitVariable(VariableNode node)
		{
			return null;
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x00002188 File Offset: 0x00000388
		public override IEnumerable<ProgramNode> VisitHole(Hole node)
		{
			return null;
		}

		// Token: 0x06001CBA RID: 7354 RVA: 0x00055AC8 File Offset: 0x00053CC8
		public override IEnumerable<ProgramNode> VisitLambda(LambdaNode node)
		{
			return this.VisitNonterminal(node);
		}

		// Token: 0x06001CBB RID: 7355 RVA: 0x00055AC8 File Offset: 0x00053CC8
		public override IEnumerable<ProgramNode> VisitLet(LetNode node)
		{
			return this.VisitNonterminal(node);
		}

		// Token: 0x06001CBC RID: 7356 RVA: 0x00055AD4 File Offset: 0x00053CD4
		public override IEnumerable<ProgramNode> VisitNonterminal(NonterminalNode node)
		{
			return (from r in node.Children.Select((ProgramNode child) => child.AcceptVisitor<IEnumerable<ProgramNode>>(this)).Enumerate<IEnumerable<ProgramNode>>()
				where r.Item2 != null
				select r).SelectMany((Record<int, IEnumerable<ProgramNode>> childAlts) => childAlts.Item2.Select((ProgramNode alt) => node.Rule.BuildASTNode(node, node.Children.MutateAt(childAlts.Item1, (ProgramNode x) => alt).ToArray<ProgramNode>()))).Concat(this._alternativesFunc(node) ?? Enumerable.Empty<ProgramNode>());
		}

		// Token: 0x04000DFD RID: 3581
		private Func<ProgramNode, IEnumerable<ProgramNode>> _alternativesFunc;
	}
}
