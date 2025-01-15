using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes
{
	// Token: 0x02001ABD RID: 6845
	public struct dropCondition : IProgramNodeBuilder, IEquatable<dropCondition>
	{
		// Token: 0x170025DF RID: 9695
		// (get) Token: 0x0600E269 RID: 57961 RVA: 0x00301A1E File Offset: 0x002FFC1E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600E26A RID: 57962 RVA: 0x00301A26 File Offset: 0x002FFC26
		private dropCondition(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600E26B RID: 57963 RVA: 0x00301A2F File Offset: 0x002FFC2F
		public static dropCondition CreateUnsafe(ProgramNode node)
		{
			return new dropCondition(node);
		}

		// Token: 0x0600E26C RID: 57964 RVA: 0x00301A38 File Offset: 0x002FFC38
		public static dropCondition? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.dropCondition)
			{
				return null;
			}
			return new dropCondition?(dropCondition.CreateUnsafe(node));
		}

		// Token: 0x0600E26D RID: 57965 RVA: 0x00301A72 File Offset: 0x002FFC72
		public static dropCondition CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new dropCondition(new Hole(g.Symbol.dropCondition, holeId));
		}

		// Token: 0x0600E26E RID: 57966 RVA: 0x00301A8A File Offset: 0x002FFC8A
		public dropCondition(GrammarBuilders g, DropCondition value)
		{
			this = new dropCondition(new LiteralNode(g.Symbol.dropCondition, value));
		}

		// Token: 0x170025E0 RID: 9696
		// (get) Token: 0x0600E26F RID: 57967 RVA: 0x00301AA3 File Offset: 0x002FFCA3
		public DropCondition Value
		{
			get
			{
				return (DropCondition)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600E270 RID: 57968 RVA: 0x00301ABA File Offset: 0x002FFCBA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600E271 RID: 57969 RVA: 0x00301AD0 File Offset: 0x002FFCD0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600E272 RID: 57970 RVA: 0x00301AFA File Offset: 0x002FFCFA
		public bool Equals(dropCondition other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400557C RID: 21884
		private ProgramNode _node;
	}
}
