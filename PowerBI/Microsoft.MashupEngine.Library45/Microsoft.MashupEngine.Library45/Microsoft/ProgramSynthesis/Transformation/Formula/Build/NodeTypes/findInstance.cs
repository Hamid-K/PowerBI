using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015F1 RID: 5617
	public struct findInstance : IProgramNodeBuilder, IEquatable<findInstance>
	{
		// Token: 0x17002039 RID: 8249
		// (get) Token: 0x0600BA8E RID: 47758 RVA: 0x00282CCA File Offset: 0x00280ECA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA8F RID: 47759 RVA: 0x00282CD2 File Offset: 0x00280ED2
		private findInstance(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA90 RID: 47760 RVA: 0x00282CDB File Offset: 0x00280EDB
		public static findInstance CreateUnsafe(ProgramNode node)
		{
			return new findInstance(node);
		}

		// Token: 0x0600BA91 RID: 47761 RVA: 0x00282CE4 File Offset: 0x00280EE4
		public static findInstance? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.findInstance)
			{
				return null;
			}
			return new findInstance?(findInstance.CreateUnsafe(node));
		}

		// Token: 0x0600BA92 RID: 47762 RVA: 0x00282D1E File Offset: 0x00280F1E
		public static findInstance CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new findInstance(new Hole(g.Symbol.findInstance, holeId));
		}

		// Token: 0x0600BA93 RID: 47763 RVA: 0x00282D36 File Offset: 0x00280F36
		public findInstance(GrammarBuilders g, int value)
		{
			this = new findInstance(new LiteralNode(g.Symbol.findInstance, value));
		}

		// Token: 0x1700203A RID: 8250
		// (get) Token: 0x0600BA94 RID: 47764 RVA: 0x00282D54 File Offset: 0x00280F54
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA95 RID: 47765 RVA: 0x00282D6B File Offset: 0x00280F6B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA96 RID: 47766 RVA: 0x00282D80 File Offset: 0x00280F80
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA97 RID: 47767 RVA: 0x00282DAA File Offset: 0x00280FAA
		public bool Equals(findInstance other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400469F RID: 18079
		private ProgramNode _node;
	}
}
