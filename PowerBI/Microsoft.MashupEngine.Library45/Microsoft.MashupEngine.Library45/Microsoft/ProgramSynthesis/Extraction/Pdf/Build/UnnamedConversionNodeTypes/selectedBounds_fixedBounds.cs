using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000BF1 RID: 3057
	public struct selectedBounds_fixedBounds : IProgramNodeBuilder, IEquatable<selectedBounds_fixedBounds>
	{
		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x000F8B06 File Offset: 0x000F6D06
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x000F8B0E File Offset: 0x000F6D0E
		private selectedBounds_fixedBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x000F8B17 File Offset: 0x000F6D17
		public static selectedBounds_fixedBounds CreateUnsafe(ProgramNode node)
		{
			return new selectedBounds_fixedBounds(node);
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x000F8B20 File Offset: 0x000F6D20
		public static selectedBounds_fixedBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.selectedBounds_fixedBounds)
			{
				return null;
			}
			return new selectedBounds_fixedBounds?(selectedBounds_fixedBounds.CreateUnsafe(node));
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x000F8B55 File Offset: 0x000F6D55
		public selectedBounds_fixedBounds(GrammarBuilders g, fixedBounds value0)
		{
			this._node = g.UnnamedConversion.selectedBounds_fixedBounds.BuildASTNode(value0.Node);
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x000F8B74 File Offset: 0x000F6D74
		public static implicit operator selectedBounds(selectedBounds_fixedBounds arg)
		{
			return selectedBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06004E70 RID: 20080 RVA: 0x000F8B82 File Offset: 0x000F6D82
		public fixedBounds fixedBounds
		{
			get
			{
				return fixedBounds.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004E71 RID: 20081 RVA: 0x000F8B96 File Offset: 0x000F6D96
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x000F8BAC File Offset: 0x000F6DAC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x000F8BD6 File Offset: 0x000F6DD6
		public bool Equals(selectedBounds_fixedBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002319 RID: 8985
		private ProgramNode _node;
	}
}
