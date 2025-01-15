using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Tree.Semantics
{
	// Token: 0x02001ECD RID: 7885
	public class GetNodesByNameVisitor : ProgramNodeVisitor<IEnumerable<ProgramNode>>
	{
		// Token: 0x06010A2E RID: 68142 RVA: 0x003947FB File Offset: 0x003929FB
		public GetNodesByNameVisitor(string targetNodeName)
		{
			this._targetNodeName = targetNodeName;
		}

		// Token: 0x06010A2F RID: 68143 RVA: 0x0039480A File Offset: 0x00392A0A
		private IEnumerable<ProgramNode> Visit(ProgramNode node)
		{
			if (node.Symbol.Name.Equals(this._targetNodeName))
			{
				yield return node;
			}
			else
			{
				foreach (ProgramNode programNode in node.Children)
				{
					foreach (ProgramNode programNode2 in programNode.AcceptVisitor<IEnumerable<ProgramNode>>(this))
					{
						yield return programNode2;
					}
					IEnumerator<ProgramNode> enumerator = null;
				}
				ProgramNode[] array = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x06010A30 RID: 68144 RVA: 0x00394821 File Offset: 0x00392A21
		public override IEnumerable<ProgramNode> VisitNonterminal(NonterminalNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x06010A31 RID: 68145 RVA: 0x00394821 File Offset: 0x00392A21
		public override IEnumerable<ProgramNode> VisitLet(LetNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x06010A32 RID: 68146 RVA: 0x00394821 File Offset: 0x00392A21
		public override IEnumerable<ProgramNode> VisitLambda(LambdaNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x06010A33 RID: 68147 RVA: 0x00394821 File Offset: 0x00392A21
		public override IEnumerable<ProgramNode> VisitLiteral(LiteralNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x06010A34 RID: 68148 RVA: 0x00394821 File Offset: 0x00392A21
		public override IEnumerable<ProgramNode> VisitVariable(VariableNode node)
		{
			return this.Visit(node);
		}

		// Token: 0x06010A35 RID: 68149 RVA: 0x00394821 File Offset: 0x00392A21
		public override IEnumerable<ProgramNode> VisitHole(Hole node)
		{
			return this.Visit(node);
		}

		// Token: 0x04006367 RID: 25447
		private readonly string _targetNodeName;
	}
}
