using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes
{
	// Token: 0x02000BFC RID: 3068
	public struct NextFontSizeDecrease : IProgramNodeBuilder, IEquatable<NextFontSizeDecrease>
	{
		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06004EE2 RID: 20194 RVA: 0x000F95F6 File Offset: 0x000F77F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004EE3 RID: 20195 RVA: 0x000F95FE File Offset: 0x000F77FE
		private NextFontSizeDecrease(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004EE4 RID: 20196 RVA: 0x000F9607 File Offset: 0x000F7807
		public static NextFontSizeDecrease CreateUnsafe(ProgramNode node)
		{
			return new NextFontSizeDecrease(node);
		}

		// Token: 0x06004EE5 RID: 20197 RVA: 0x000F9610 File Offset: 0x000F7810
		public static NextFontSizeDecrease? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NextFontSizeDecrease)
			{
				return null;
			}
			return new NextFontSizeDecrease?(NextFontSizeDecrease.CreateUnsafe(node));
		}

		// Token: 0x06004EE6 RID: 20198 RVA: 0x000F9645 File Offset: 0x000F7845
		public NextFontSizeDecrease(GrammarBuilders g, before value0, dir value1)
		{
			this._node = g.Rule.NextFontSizeDecrease.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06004EE7 RID: 20199 RVA: 0x000F966B File Offset: 0x000F786B
		public static implicit operator beforeRelativeBounds(NextFontSizeDecrease arg)
		{
			return beforeRelativeBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x000F9679 File Offset: 0x000F7879
		public before before
		{
			get
			{
				return before.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06004EE9 RID: 20201 RVA: 0x000F968D File Offset: 0x000F788D
		public dir dir
		{
			get
			{
				return dir.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06004EEA RID: 20202 RVA: 0x000F96A1 File Offset: 0x000F78A1
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004EEB RID: 20203 RVA: 0x000F96B4 File Offset: 0x000F78B4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004EEC RID: 20204 RVA: 0x000F96DE File Offset: 0x000F78DE
		public bool Equals(NextFontSizeDecrease other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002324 RID: 8996
		private ProgramNode _node;
	}
}
