using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015F2 RID: 5618
	public struct findOffset : IProgramNodeBuilder, IEquatable<findOffset>
	{
		// Token: 0x1700203B RID: 8251
		// (get) Token: 0x0600BA98 RID: 47768 RVA: 0x00282DBE File Offset: 0x00280FBE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA99 RID: 47769 RVA: 0x00282DC6 File Offset: 0x00280FC6
		private findOffset(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA9A RID: 47770 RVA: 0x00282DCF File Offset: 0x00280FCF
		public static findOffset CreateUnsafe(ProgramNode node)
		{
			return new findOffset(node);
		}

		// Token: 0x0600BA9B RID: 47771 RVA: 0x00282DD8 File Offset: 0x00280FD8
		public static findOffset? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.findOffset)
			{
				return null;
			}
			return new findOffset?(findOffset.CreateUnsafe(node));
		}

		// Token: 0x0600BA9C RID: 47772 RVA: 0x00282E12 File Offset: 0x00281012
		public static findOffset CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new findOffset(new Hole(g.Symbol.findOffset, holeId));
		}

		// Token: 0x0600BA9D RID: 47773 RVA: 0x00282E2A File Offset: 0x0028102A
		public findOffset(GrammarBuilders g, int value)
		{
			this = new findOffset(new LiteralNode(g.Symbol.findOffset, value));
		}

		// Token: 0x1700203C RID: 8252
		// (get) Token: 0x0600BA9E RID: 47774 RVA: 0x00282E48 File Offset: 0x00281048
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA9F RID: 47775 RVA: 0x00282E5F File Offset: 0x0028105F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BAA0 RID: 47776 RVA: 0x00282E74 File Offset: 0x00281074
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BAA1 RID: 47777 RVA: 0x00282E9E File Offset: 0x0028109E
		public bool Equals(findOffset other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040046A0 RID: 18080
		private ProgramNode _node;
	}
}
