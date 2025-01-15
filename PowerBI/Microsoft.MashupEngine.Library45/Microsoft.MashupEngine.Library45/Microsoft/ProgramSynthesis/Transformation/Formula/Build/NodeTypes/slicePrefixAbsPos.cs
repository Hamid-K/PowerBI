using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015F3 RID: 5619
	public struct slicePrefixAbsPos : IProgramNodeBuilder, IEquatable<slicePrefixAbsPos>
	{
		// Token: 0x1700203D RID: 8253
		// (get) Token: 0x0600BAA2 RID: 47778 RVA: 0x00282EB2 File Offset: 0x002810B2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BAA3 RID: 47779 RVA: 0x00282EBA File Offset: 0x002810BA
		private slicePrefixAbsPos(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BAA4 RID: 47780 RVA: 0x00282EC3 File Offset: 0x002810C3
		public static slicePrefixAbsPos CreateUnsafe(ProgramNode node)
		{
			return new slicePrefixAbsPos(node);
		}

		// Token: 0x0600BAA5 RID: 47781 RVA: 0x00282ECC File Offset: 0x002810CC
		public static slicePrefixAbsPos? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.slicePrefixAbsPos)
			{
				return null;
			}
			return new slicePrefixAbsPos?(slicePrefixAbsPos.CreateUnsafe(node));
		}

		// Token: 0x0600BAA6 RID: 47782 RVA: 0x00282F06 File Offset: 0x00281106
		public static slicePrefixAbsPos CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new slicePrefixAbsPos(new Hole(g.Symbol.slicePrefixAbsPos, holeId));
		}

		// Token: 0x0600BAA7 RID: 47783 RVA: 0x00282F1E File Offset: 0x0028111E
		public slicePrefixAbsPos(GrammarBuilders g, int value)
		{
			this = new slicePrefixAbsPos(new LiteralNode(g.Symbol.slicePrefixAbsPos, value));
		}

		// Token: 0x1700203E RID: 8254
		// (get) Token: 0x0600BAA8 RID: 47784 RVA: 0x00282F3C File Offset: 0x0028113C
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BAA9 RID: 47785 RVA: 0x00282F53 File Offset: 0x00281153
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BAAA RID: 47786 RVA: 0x00282F68 File Offset: 0x00281168
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BAAB RID: 47787 RVA: 0x00282F92 File Offset: 0x00281192
		public bool Equals(slicePrefixAbsPos other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040046A1 RID: 18081
		private ProgramNode _node;
	}
}
