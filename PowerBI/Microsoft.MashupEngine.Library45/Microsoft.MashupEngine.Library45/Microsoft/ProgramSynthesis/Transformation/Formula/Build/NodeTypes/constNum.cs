using System;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015DC RID: 5596
	public struct constNum : IProgramNodeBuilder, IEquatable<constNum>
	{
		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x0600B9BC RID: 47548 RVA: 0x002818FE File Offset: 0x0027FAFE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B9BD RID: 47549 RVA: 0x00281906 File Offset: 0x0027FB06
		private constNum(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B9BE RID: 47550 RVA: 0x0028190F File Offset: 0x0027FB0F
		public static constNum CreateUnsafe(ProgramNode node)
		{
			return new constNum(node);
		}

		// Token: 0x0600B9BF RID: 47551 RVA: 0x00281918 File Offset: 0x0027FB18
		public static constNum? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.constNum)
			{
				return null;
			}
			return new constNum?(constNum.CreateUnsafe(node));
		}

		// Token: 0x0600B9C0 RID: 47552 RVA: 0x00281952 File Offset: 0x0027FB52
		public static constNum CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new constNum(new Hole(g.Symbol.constNum, holeId));
		}

		// Token: 0x0600B9C1 RID: 47553 RVA: 0x0028196A File Offset: 0x0027FB6A
		public constNum(GrammarBuilders g, decimal value)
		{
			this = new constNum(new LiteralNode(g.Symbol.constNum, value));
		}

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x0600B9C2 RID: 47554 RVA: 0x00281988 File Offset: 0x0027FB88
		public decimal Value
		{
			get
			{
				return (decimal)((LiteralNode)this.Node).Value;
			}
		}

		// Token: 0x0600B9C3 RID: 47555 RVA: 0x0028199F File Offset: 0x0027FB9F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B9C4 RID: 47556 RVA: 0x002819B4 File Offset: 0x0027FBB4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B9C5 RID: 47557 RVA: 0x002819DE File Offset: 0x0027FBDE
		public bool Equals(constNum other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400468A RID: 18058
		private ProgramNode _node;
	}
}
