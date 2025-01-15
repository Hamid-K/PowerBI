using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes
{
	// Token: 0x0200156D RID: 5485
	public struct Multiply : IProgramNodeBuilder, IEquatable<Multiply>
	{
		// Token: 0x17001F43 RID: 8003
		// (get) Token: 0x0600B35C RID: 45916 RVA: 0x00273436 File Offset: 0x00271636
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B35D RID: 45917 RVA: 0x0027343E File Offset: 0x0027163E
		private Multiply(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B35E RID: 45918 RVA: 0x00273447 File Offset: 0x00271647
		public static Multiply CreateUnsafe(ProgramNode node)
		{
			return new Multiply(node);
		}

		// Token: 0x0600B35F RID: 45919 RVA: 0x00273450 File Offset: 0x00271650
		public static Multiply? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.Multiply)
			{
				return null;
			}
			return new Multiply?(Multiply.CreateUnsafe(node));
		}

		// Token: 0x0600B360 RID: 45920 RVA: 0x00273485 File Offset: 0x00271685
		public Multiply(GrammarBuilders g, arithmeticLeft value0, multiplyRight value1)
		{
			this._node = g.Rule.Multiply.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600B361 RID: 45921 RVA: 0x002734AB File Offset: 0x002716AB
		public static implicit operator arithmetic(Multiply arg)
		{
			return arithmetic.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001F44 RID: 8004
		// (get) Token: 0x0600B362 RID: 45922 RVA: 0x002734B9 File Offset: 0x002716B9
		public arithmeticLeft arithmeticLeft
		{
			get
			{
				return arithmeticLeft.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001F45 RID: 8005
		// (get) Token: 0x0600B363 RID: 45923 RVA: 0x002734CD File Offset: 0x002716CD
		public multiplyRight multiplyRight
		{
			get
			{
				return multiplyRight.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600B364 RID: 45924 RVA: 0x002734E1 File Offset: 0x002716E1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B365 RID: 45925 RVA: 0x002734F4 File Offset: 0x002716F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B366 RID: 45926 RVA: 0x0027351E File Offset: 0x0027171E
		public bool Equals(Multiply other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400461B RID: 17947
		private ProgramNode _node;
	}
}
