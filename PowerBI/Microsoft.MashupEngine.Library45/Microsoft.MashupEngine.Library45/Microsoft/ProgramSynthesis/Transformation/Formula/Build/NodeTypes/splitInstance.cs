using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015EF RID: 5615
	public struct splitInstance : IProgramNodeBuilder, IEquatable<splitInstance>
	{
		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x0600BA7A RID: 47738 RVA: 0x00282AE6 File Offset: 0x00280CE6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BA7B RID: 47739 RVA: 0x00282AEE File Offset: 0x00280CEE
		private splitInstance(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BA7C RID: 47740 RVA: 0x00282AF7 File Offset: 0x00280CF7
		public static splitInstance CreateUnsafe(ProgramNode node)
		{
			return new splitInstance(node);
		}

		// Token: 0x0600BA7D RID: 47741 RVA: 0x00282B00 File Offset: 0x00280D00
		public static splitInstance? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitInstance)
			{
				return null;
			}
			return new splitInstance?(splitInstance.CreateUnsafe(node));
		}

		// Token: 0x0600BA7E RID: 47742 RVA: 0x00282B3A File Offset: 0x00280D3A
		public static splitInstance CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitInstance(new Hole(g.Symbol.splitInstance, holeId));
		}

		// Token: 0x0600BA7F RID: 47743 RVA: 0x00282B52 File Offset: 0x00280D52
		public splitInstance(GrammarBuilders g, int value)
		{
			this = new splitInstance(new LiteralNode(g.Symbol.splitInstance, value));
		}

		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x0600BA80 RID: 47744 RVA: 0x00282B70 File Offset: 0x00280D70
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BA81 RID: 47745 RVA: 0x00282B87 File Offset: 0x00280D87
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BA82 RID: 47746 RVA: 0x00282B9C File Offset: 0x00280D9C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BA83 RID: 47747 RVA: 0x00282BC6 File Offset: 0x00280DC6
		public bool Equals(splitInstance other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400469D RID: 18077
		private ProgramNode _node;
	}
}
