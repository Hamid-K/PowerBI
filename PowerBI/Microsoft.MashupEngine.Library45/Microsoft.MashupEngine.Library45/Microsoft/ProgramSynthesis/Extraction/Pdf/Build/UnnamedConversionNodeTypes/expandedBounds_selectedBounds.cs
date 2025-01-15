using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000BF2 RID: 3058
	public struct expandedBounds_selectedBounds : IProgramNodeBuilder, IEquatable<expandedBounds_selectedBounds>
	{
		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x06004E74 RID: 20084 RVA: 0x000F8BEA File Offset: 0x000F6DEA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x000F8BF2 File Offset: 0x000F6DF2
		private expandedBounds_selectedBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004E76 RID: 20086 RVA: 0x000F8BFB File Offset: 0x000F6DFB
		public static expandedBounds_selectedBounds CreateUnsafe(ProgramNode node)
		{
			return new expandedBounds_selectedBounds(node);
		}

		// Token: 0x06004E77 RID: 20087 RVA: 0x000F8C04 File Offset: 0x000F6E04
		public static expandedBounds_selectedBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.expandedBounds_selectedBounds)
			{
				return null;
			}
			return new expandedBounds_selectedBounds?(expandedBounds_selectedBounds.CreateUnsafe(node));
		}

		// Token: 0x06004E78 RID: 20088 RVA: 0x000F8C39 File Offset: 0x000F6E39
		public expandedBounds_selectedBounds(GrammarBuilders g, selectedBounds value0)
		{
			this._node = g.UnnamedConversion.expandedBounds_selectedBounds.BuildASTNode(value0.Node);
		}

		// Token: 0x06004E79 RID: 20089 RVA: 0x000F8C58 File Offset: 0x000F6E58
		public static implicit operator expandedBounds(expandedBounds_selectedBounds arg)
		{
			return expandedBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06004E7A RID: 20090 RVA: 0x000F8C66 File Offset: 0x000F6E66
		public selectedBounds selectedBounds
		{
			get
			{
				return selectedBounds.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004E7B RID: 20091 RVA: 0x000F8C7A File Offset: 0x000F6E7A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004E7C RID: 20092 RVA: 0x000F8C90 File Offset: 0x000F6E90
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004E7D RID: 20093 RVA: 0x000F8CBA File Offset: 0x000F6EBA
		public bool Equals(expandedBounds_selectedBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400231A RID: 8986
		private ProgramNode _node;
	}
}
