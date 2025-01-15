using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000BF0 RID: 3056
	public struct tableBounds_expandedBounds : IProgramNodeBuilder, IEquatable<tableBounds_expandedBounds>
	{
		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06004E60 RID: 20064 RVA: 0x000F8A21 File Offset: 0x000F6C21
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004E61 RID: 20065 RVA: 0x000F8A29 File Offset: 0x000F6C29
		private tableBounds_expandedBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004E62 RID: 20066 RVA: 0x000F8A32 File Offset: 0x000F6C32
		public static tableBounds_expandedBounds CreateUnsafe(ProgramNode node)
		{
			return new tableBounds_expandedBounds(node);
		}

		// Token: 0x06004E63 RID: 20067 RVA: 0x000F8A3C File Offset: 0x000F6C3C
		public static tableBounds_expandedBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.tableBounds_expandedBounds)
			{
				return null;
			}
			return new tableBounds_expandedBounds?(tableBounds_expandedBounds.CreateUnsafe(node));
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x000F8A71 File Offset: 0x000F6C71
		public tableBounds_expandedBounds(GrammarBuilders g, expandedBounds value0)
		{
			this._node = g.UnnamedConversion.tableBounds_expandedBounds.BuildASTNode(value0.Node);
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x000F8A90 File Offset: 0x000F6C90
		public static implicit operator tableBounds(tableBounds_expandedBounds arg)
		{
			return tableBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06004E66 RID: 20070 RVA: 0x000F8A9E File Offset: 0x000F6C9E
		public expandedBounds expandedBounds
		{
			get
			{
				return expandedBounds.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004E67 RID: 20071 RVA: 0x000F8AB2 File Offset: 0x000F6CB2
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004E68 RID: 20072 RVA: 0x000F8AC8 File Offset: 0x000F6CC8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004E69 RID: 20073 RVA: 0x000F8AF2 File Offset: 0x000F6CF2
		public bool Equals(tableBounds_expandedBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002318 RID: 8984
		private ProgramNode _node;
	}
}
