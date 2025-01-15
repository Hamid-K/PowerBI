using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015F5 RID: 5621
	public struct absPos : IProgramNodeBuilder, IEquatable<absPos>
	{
		// Token: 0x17002041 RID: 8257
		// (get) Token: 0x0600BAB6 RID: 47798 RVA: 0x0028309A File Offset: 0x0028129A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600BAB7 RID: 47799 RVA: 0x002830A2 File Offset: 0x002812A2
		private absPos(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600BAB8 RID: 47800 RVA: 0x002830AB File Offset: 0x002812AB
		public static absPos CreateUnsafe(ProgramNode node)
		{
			return new absPos(node);
		}

		// Token: 0x0600BAB9 RID: 47801 RVA: 0x002830B4 File Offset: 0x002812B4
		public static absPos? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.absPos)
			{
				return null;
			}
			return new absPos?(absPos.CreateUnsafe(node));
		}

		// Token: 0x0600BABA RID: 47802 RVA: 0x002830EE File Offset: 0x002812EE
		public static absPos CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new absPos(new Hole(g.Symbol.absPos, holeId));
		}

		// Token: 0x0600BABB RID: 47803 RVA: 0x00283106 File Offset: 0x00281306
		public absPos(GrammarBuilders g, int value)
		{
			this = new absPos(new LiteralNode(g.Symbol.absPos, value));
		}

		// Token: 0x17002042 RID: 8258
		// (get) Token: 0x0600BABC RID: 47804 RVA: 0x00283124 File Offset: 0x00281324
		public int Value
		{
			get
			{
				return (int)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600BABD RID: 47805 RVA: 0x0028313B File Offset: 0x0028133B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600BABE RID: 47806 RVA: 0x00283150 File Offset: 0x00281350
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600BABF RID: 47807 RVA: 0x0028317A File Offset: 0x0028137A
		public bool Equals(absPos other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040046A3 RID: 18083
		private ProgramNode _node;
	}
}
