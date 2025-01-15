using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes
{
	// Token: 0x02000BFA RID: 3066
	public struct NextSeparator_beforeRelative : IProgramNodeBuilder, IEquatable<NextSeparator_beforeRelative>
	{
		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06004EC9 RID: 20169 RVA: 0x000F938A File Offset: 0x000F758A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004ECA RID: 20170 RVA: 0x000F9392 File Offset: 0x000F7592
		private NextSeparator_beforeRelative(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004ECB RID: 20171 RVA: 0x000F939B File Offset: 0x000F759B
		public static NextSeparator_beforeRelative CreateUnsafe(ProgramNode node)
		{
			return new NextSeparator_beforeRelative(node);
		}

		// Token: 0x06004ECC RID: 20172 RVA: 0x000F93A4 File Offset: 0x000F75A4
		public static NextSeparator_beforeRelative? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NextSeparator_beforeRelative)
			{
				return null;
			}
			return new NextSeparator_beforeRelative?(NextSeparator_beforeRelative.CreateUnsafe(node));
		}

		// Token: 0x06004ECD RID: 20173 RVA: 0x000F93D9 File Offset: 0x000F75D9
		public NextSeparator_beforeRelative(GrammarBuilders g, before value0, dir value1, k value2)
		{
			this._node = g.Rule.NextSeparator_beforeRelative.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06004ECE RID: 20174 RVA: 0x000F9406 File Offset: 0x000F7606
		public static implicit operator beforeRelativeBounds(NextSeparator_beforeRelative arg)
		{
			return beforeRelativeBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06004ECF RID: 20175 RVA: 0x000F9414 File Offset: 0x000F7614
		public before before
		{
			get
			{
				return before.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x06004ED0 RID: 20176 RVA: 0x000F9428 File Offset: 0x000F7628
		public dir dir
		{
			get
			{
				return dir.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06004ED1 RID: 20177 RVA: 0x000F943C File Offset: 0x000F763C
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06004ED2 RID: 20178 RVA: 0x000F9450 File Offset: 0x000F7650
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004ED3 RID: 20179 RVA: 0x000F9464 File Offset: 0x000F7664
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004ED4 RID: 20180 RVA: 0x000F948E File Offset: 0x000F768E
		public bool Equals(NextSeparator_beforeRelative other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002322 RID: 8994
		private ProgramNode _node;
	}
}
