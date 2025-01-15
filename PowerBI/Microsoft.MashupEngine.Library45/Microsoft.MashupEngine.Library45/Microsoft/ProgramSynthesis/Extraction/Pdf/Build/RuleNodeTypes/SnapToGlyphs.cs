using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes
{
	// Token: 0x02000BF5 RID: 3061
	public struct SnapToGlyphs : IProgramNodeBuilder, IEquatable<SnapToGlyphs>
	{
		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06004E92 RID: 20114 RVA: 0x000F8E96 File Offset: 0x000F7096
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x000F8E9E File Offset: 0x000F709E
		private SnapToGlyphs(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x000F8EA7 File Offset: 0x000F70A7
		public static SnapToGlyphs CreateUnsafe(ProgramNode node)
		{
			return new SnapToGlyphs(node);
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x000F8EB0 File Offset: 0x000F70B0
		public static SnapToGlyphs? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.SnapToGlyphs)
			{
				return null;
			}
			return new SnapToGlyphs?(SnapToGlyphs.CreateUnsafe(node));
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x000F8EE5 File Offset: 0x000F70E5
		public SnapToGlyphs(GrammarBuilders g, expandedBounds value0)
		{
			this._node = g.Rule.SnapToGlyphs.BuildASTNode(value0.Node);
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x000F8F04 File Offset: 0x000F7104
		public static implicit operator tableBounds(SnapToGlyphs arg)
		{
			return tableBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06004E98 RID: 20120 RVA: 0x000F8F12 File Offset: 0x000F7112
		public expandedBounds expandedBounds
		{
			get
			{
				return expandedBounds.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x000F8F26 File Offset: 0x000F7126
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x000F8F3C File Offset: 0x000F713C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x000F8F66 File Offset: 0x000F7166
		public bool Equals(SnapToGlyphs other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400231D RID: 8989
		private ProgramNode _node;
	}
}
