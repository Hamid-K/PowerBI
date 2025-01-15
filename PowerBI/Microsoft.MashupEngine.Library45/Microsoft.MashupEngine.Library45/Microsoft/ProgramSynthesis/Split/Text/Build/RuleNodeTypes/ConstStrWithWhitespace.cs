using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes
{
	// Token: 0x0200134B RID: 4939
	public struct ConstStrWithWhitespace : IProgramNodeBuilder, IEquatable<ConstStrWithWhitespace>
	{
		// Token: 0x17001A2D RID: 6701
		// (get) Token: 0x06009848 RID: 38984 RVA: 0x002068BE File Offset: 0x00204ABE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009849 RID: 38985 RVA: 0x002068C6 File Offset: 0x00204AC6
		private ConstStrWithWhitespace(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600984A RID: 38986 RVA: 0x002068CF File Offset: 0x00204ACF
		public static ConstStrWithWhitespace CreateUnsafe(ProgramNode node)
		{
			return new ConstStrWithWhitespace(node);
		}

		// Token: 0x0600984B RID: 38987 RVA: 0x002068D8 File Offset: 0x00204AD8
		public static ConstStrWithWhitespace? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ConstStrWithWhitespace)
			{
				return null;
			}
			return new ConstStrWithWhitespace?(ConstStrWithWhitespace.CreateUnsafe(node));
		}

		// Token: 0x0600984C RID: 38988 RVA: 0x0020690D File Offset: 0x00204B0D
		public ConstStrWithWhitespace(GrammarBuilders g, v value0, s value1)
		{
			this._node = g.Rule.ConstStrWithWhitespace.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x0600984D RID: 38989 RVA: 0x00206933 File Offset: 0x00204B33
		public static implicit operator c(ConstStrWithWhitespace arg)
		{
			return c.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001A2E RID: 6702
		// (get) Token: 0x0600984E RID: 38990 RVA: 0x00206941 File Offset: 0x00204B41
		public v v
		{
			get
			{
				return v.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17001A2F RID: 6703
		// (get) Token: 0x0600984F RID: 38991 RVA: 0x00206955 File Offset: 0x00204B55
		public s s
		{
			get
			{
				return s.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06009850 RID: 38992 RVA: 0x00206969 File Offset: 0x00204B69
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009851 RID: 38993 RVA: 0x0020697C File Offset: 0x00204B7C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009852 RID: 38994 RVA: 0x002069A6 File Offset: 0x00204BA6
		public bool Equals(ConstStrWithWhitespace other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DC2 RID: 15810
		private ProgramNode _node;
	}
}
