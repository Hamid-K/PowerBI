using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015E4 RID: 5604
	public struct numberRoundDesc : IProgramNodeBuilder, IEquatable<numberRoundDesc>
	{
		// Token: 0x1700201F RID: 8223
		// (get) Token: 0x0600BA0C RID: 47628 RVA: 0x00282086 File Offset: 0x00280286
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA0D RID: 47629 RVA: 0x0028208E File Offset: 0x0028028E
		private numberRoundDesc(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA0E RID: 47630 RVA: 0x00282097 File Offset: 0x00280297
		public static numberRoundDesc CreateUnsafe(ProgramNode node)
		{
			return new numberRoundDesc(node);
		}

		// Token: 0x0600BA0F RID: 47631 RVA: 0x002820A0 File Offset: 0x002802A0
		public static numberRoundDesc? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.numberRoundDesc)
			{
				return null;
			}
			return new numberRoundDesc?(numberRoundDesc.CreateUnsafe(node));
		}

		// Token: 0x0600BA10 RID: 47632 RVA: 0x002820DA File Offset: 0x002802DA
		public static numberRoundDesc CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new numberRoundDesc(new Hole(g.Symbol.numberRoundDesc, holeId));
		}

		// Token: 0x0600BA11 RID: 47633 RVA: 0x002820F2 File Offset: 0x002802F2
		public numberRoundDesc(GrammarBuilders g, RoundNumberDescriptor value)
		{
			this = new numberRoundDesc(new LiteralNode(g.Symbol.numberRoundDesc, value));
		}

		// Token: 0x17002020 RID: 8224
		// (get) Token: 0x0600BA12 RID: 47634 RVA: 0x0028210B File Offset: 0x0028030B
		public RoundNumberDescriptor Value
		{
			get
			{
				return (RoundNumberDescriptor)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA13 RID: 47635 RVA: 0x00282122 File Offset: 0x00280322
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA14 RID: 47636 RVA: 0x00282138 File Offset: 0x00280338
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA15 RID: 47637 RVA: 0x00282162 File Offset: 0x00280362
		public bool Equals(numberRoundDesc other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004692 RID: 18066
		private ProgramNode _node;
	}
}
