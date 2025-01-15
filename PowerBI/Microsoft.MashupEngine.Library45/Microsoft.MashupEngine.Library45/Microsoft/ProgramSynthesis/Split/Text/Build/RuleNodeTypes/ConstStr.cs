using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200134A RID: 4938
	public struct ConstStr : IProgramNodeBuilder, IEquatable<ConstStr>
	{
		// Token: 0x17001A2A RID: 6698
		// (get) Token: 0x0600983D RID: 38973 RVA: 0x002067C2 File Offset: 0x002049C2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600983E RID: 38974 RVA: 0x002067CA File Offset: 0x002049CA
		private ConstStr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600983F RID: 38975 RVA: 0x002067D3 File Offset: 0x002049D3
		public static ConstStr CreateUnsafe(ProgramNode node)
		{
			return new ConstStr(node);
		}

		// Token: 0x06009840 RID: 38976 RVA: 0x002067DC File Offset: 0x002049DC
		public static ConstStr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConstStr)
			{
				return null;
			}
			return new ConstStr?(ConstStr.CreateUnsafe(node));
		}

		// Token: 0x06009841 RID: 38977 RVA: 0x00206811 File Offset: 0x00204A11
		public ConstStr(GrammarBuilders g, v value0, s value1)
		{
			this._node = g.Rule.ConstStr.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06009842 RID: 38978 RVA: 0x00206837 File Offset: 0x00204A37
		public static implicit operator c(ConstStr arg)
		{
			return c.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A2B RID: 6699
		// (get) Token: 0x06009843 RID: 38979 RVA: 0x00206845 File Offset: 0x00204A45
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A2C RID: 6700
		// (get) Token: 0x06009844 RID: 38980 RVA: 0x00206859 File Offset: 0x00204A59
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06009845 RID: 38981 RVA: 0x0020686D File Offset: 0x00204A6D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009846 RID: 38982 RVA: 0x00206880 File Offset: 0x00204A80
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009847 RID: 38983 RVA: 0x002068AA File Offset: 0x00204AAA
		public bool Equals(ConstStr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DC1 RID: 15809
		private ProgramNode _node;
	}
}
