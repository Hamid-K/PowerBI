using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015EC RID: 5612
	public struct matchDesc : IProgramNodeBuilder, IEquatable<matchDesc>
	{
		// Token: 0x1700202F RID: 8239
		// (get) Token: 0x0600BA5C RID: 47708 RVA: 0x00282812 File Offset: 0x00280A12
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA5D RID: 47709 RVA: 0x0028281A File Offset: 0x00280A1A
		private matchDesc(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA5E RID: 47710 RVA: 0x00282823 File Offset: 0x00280A23
		public static matchDesc CreateUnsafe(ProgramNode node)
		{
			return new matchDesc(node);
		}

		// Token: 0x0600BA5F RID: 47711 RVA: 0x0028282C File Offset: 0x00280A2C
		public static matchDesc? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.matchDesc)
			{
				return null;
			}
			return new matchDesc?(matchDesc.CreateUnsafe(node));
		}

		// Token: 0x0600BA60 RID: 47712 RVA: 0x00282866 File Offset: 0x00280A66
		public static matchDesc CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new matchDesc(new Hole(g.Symbol.matchDesc, holeId));
		}

		// Token: 0x0600BA61 RID: 47713 RVA: 0x0028287E File Offset: 0x00280A7E
		public matchDesc(GrammarBuilders g, MatchDescriptor value)
		{
			this = new matchDesc(new LiteralNode(g.Symbol.matchDesc, value));
		}

		// Token: 0x17002030 RID: 8240
		// (get) Token: 0x0600BA62 RID: 47714 RVA: 0x00282897 File Offset: 0x00280A97
		public MatchDescriptor Value
		{
			get
			{
				return (MatchDescriptor)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA63 RID: 47715 RVA: 0x002828AE File Offset: 0x00280AAE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA64 RID: 47716 RVA: 0x002828C4 File Offset: 0x00280AC4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA65 RID: 47717 RVA: 0x002828EE File Offset: 0x00280AEE
		public bool Equals(matchDesc other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400469A RID: 18074
		private ProgramNode _node;
	}
}
