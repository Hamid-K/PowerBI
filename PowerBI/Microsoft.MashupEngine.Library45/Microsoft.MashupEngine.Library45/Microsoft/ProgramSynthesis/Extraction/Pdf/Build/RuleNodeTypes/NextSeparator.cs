using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes
{
	// Token: 0x02000BF8 RID: 3064
	public struct NextSeparator : IProgramNodeBuilder, IEquatable<NextSeparator>
	{
		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x06004EB2 RID: 20146 RVA: 0x000F9176 File Offset: 0x000F7376
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x000F917E File Offset: 0x000F737E
		private NextSeparator(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x000F9187 File Offset: 0x000F7387
		public static NextSeparator CreateUnsafe(ProgramNode node)
		{
			return new NextSeparator(node);
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x000F9190 File Offset: 0x000F7390
		public static NextSeparator? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.NextSeparator)
			{
				return null;
			}
			return new NextSeparator?(NextSeparator.CreateUnsafe(node));
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x000F91C5 File Offset: 0x000F73C5
		public NextSeparator(GrammarBuilders g, selectedBounds value0, dir value1, k value2)
		{
			this._node = g.Rule.NextSeparator.BuildASTNode(value0.Node, value1.Node, value2.Node);
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x000F91F2 File Offset: 0x000F73F2
		public static implicit operator selectedBounds(NextSeparator arg)
		{
			return selectedBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x06004EB8 RID: 20152 RVA: 0x000F9200 File Offset: 0x000F7400
		public selectedBounds selectedBounds
		{
			get
			{
				return selectedBounds.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06004EB9 RID: 20153 RVA: 0x000F9214 File Offset: 0x000F7414
		public dir dir
		{
			get
			{
				return dir.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x06004EBA RID: 20154 RVA: 0x000F9228 File Offset: 0x000F7428
		public k k
		{
			get
			{
				return k.CreateUnsafe(this.Node.Children[2]);
			}
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x000F923C File Offset: 0x000F743C
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004EBC RID: 20156 RVA: 0x000F9250 File Offset: 0x000F7450
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x000F927A File Offset: 0x000F747A
		public bool Equals(NextSeparator other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002320 RID: 8992
		private ProgramNode _node;
	}
}
