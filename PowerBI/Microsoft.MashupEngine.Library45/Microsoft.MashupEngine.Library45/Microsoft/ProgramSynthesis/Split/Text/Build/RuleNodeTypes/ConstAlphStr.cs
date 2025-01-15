using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200134C RID: 4940
	public struct ConstAlphStr : IProgramNodeBuilder, IEquatable<ConstAlphStr>
	{
		// Token: 0x17001A30 RID: 6704
		// (get) Token: 0x06009853 RID: 38995 RVA: 0x002069BA File Offset: 0x00204BBA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009854 RID: 38996 RVA: 0x002069C2 File Offset: 0x00204BC2
		private ConstAlphStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009855 RID: 38997 RVA: 0x002069CB File Offset: 0x00204BCB
		public static ConstAlphStr CreateUnsafe(ProgramNode node)
		{
			return new ConstAlphStr(node);
		}

		// Token: 0x06009856 RID: 38998 RVA: 0x002069D4 File Offset: 0x00204BD4
		public static ConstAlphStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConstAlphStr)
			{
				return null;
			}
			return new ConstAlphStr?(ConstAlphStr.CreateUnsafe(node));
		}

		// Token: 0x06009857 RID: 38999 RVA: 0x00206A09 File Offset: 0x00204C09
		public ConstAlphStr(GrammarBuilders g, v value0, a value1)
		{
			this._node = g.Rule.ConstAlphStr.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06009858 RID: 39000 RVA: 0x00206A2F File Offset: 0x00204C2F
		public static implicit operator c(ConstAlphStr arg)
		{
			return c.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A31 RID: 6705
		// (get) Token: 0x06009859 RID: 39001 RVA: 0x00206A3D File Offset: 0x00204C3D
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A32 RID: 6706
		// (get) Token: 0x0600985A RID: 39002 RVA: 0x00206A51 File Offset: 0x00204C51
		public a a
		{
			get
			{
				return a.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600985B RID: 39003 RVA: 0x00206A65 File Offset: 0x00204C65
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600985C RID: 39004 RVA: 0x00206A78 File Offset: 0x00204C78
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600985D RID: 39005 RVA: 0x00206AA2 File Offset: 0x00204CA2
		public bool Equals(ConstAlphStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DC3 RID: 15811
		private ProgramNode _node;
	}
}
