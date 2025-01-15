using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200134E RID: 4942
	public struct ConstantDelimiter : IProgramNodeBuilder, IEquatable<ConstantDelimiter>
	{
		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x0600986A RID: 39018 RVA: 0x00206BCE File Offset: 0x00204DCE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600986B RID: 39019 RVA: 0x00206BD6 File Offset: 0x00204DD6
		private ConstantDelimiter(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600986C RID: 39020 RVA: 0x00206BDF File Offset: 0x00204DDF
		public static ConstantDelimiter CreateUnsafe(ProgramNode node)
		{
			return new ConstantDelimiter(node);
		}

		// Token: 0x0600986D RID: 39021 RVA: 0x00206BE8 File Offset: 0x00204DE8
		public static ConstantDelimiter? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConstantDelimiter)
			{
				return null;
			}
			return new ConstantDelimiter?(ConstantDelimiter.CreateUnsafe(node));
		}

		// Token: 0x0600986E RID: 39022 RVA: 0x00206C1D File Offset: 0x00204E1D
		public ConstantDelimiter(GrammarBuilders g, v value0, s value1)
		{
			this._node = g.Rule.ConstantDelimiter.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600986F RID: 39023 RVA: 0x00206C43 File Offset: 0x00204E43
		public static implicit operator constantDelimiterMatches(ConstantDelimiter arg)
		{
			return constantDelimiterMatches.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A38 RID: 6712
		// (get) Token: 0x06009870 RID: 39024 RVA: 0x00206C51 File Offset: 0x00204E51
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A39 RID: 6713
		// (get) Token: 0x06009871 RID: 39025 RVA: 0x00206C65 File Offset: 0x00204E65
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06009872 RID: 39026 RVA: 0x00206C79 File Offset: 0x00204E79
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009873 RID: 39027 RVA: 0x00206C8C File Offset: 0x00204E8C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009874 RID: 39028 RVA: 0x00206CB6 File Offset: 0x00204EB6
		public bool Equals(ConstantDelimiter other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DC5 RID: 15813
		private ProgramNode _node;
	}
}
