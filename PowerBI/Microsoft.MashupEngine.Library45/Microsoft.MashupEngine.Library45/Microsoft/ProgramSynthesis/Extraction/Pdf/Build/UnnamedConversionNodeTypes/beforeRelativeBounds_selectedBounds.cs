using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000BF3 RID: 3059
	public struct beforeRelativeBounds_selectedBounds : IProgramNodeBuilder, IEquatable<beforeRelativeBounds_selectedBounds>
	{
		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06004E7E RID: 20094 RVA: 0x000F8CCE File Offset: 0x000F6ECE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004E7F RID: 20095 RVA: 0x000F8CD6 File Offset: 0x000F6ED6
		private beforeRelativeBounds_selectedBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004E80 RID: 20096 RVA: 0x000F8CDF File Offset: 0x000F6EDF
		public static beforeRelativeBounds_selectedBounds CreateUnsafe(ProgramNode node)
		{
			return new beforeRelativeBounds_selectedBounds(node);
		}

		// Token: 0x06004E81 RID: 20097 RVA: 0x000F8CE8 File Offset: 0x000F6EE8
		public static beforeRelativeBounds_selectedBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.beforeRelativeBounds_selectedBounds)
			{
				return null;
			}
			return new beforeRelativeBounds_selectedBounds?(beforeRelativeBounds_selectedBounds.CreateUnsafe(node));
		}

		// Token: 0x06004E82 RID: 20098 RVA: 0x000F8D1D File Offset: 0x000F6F1D
		public beforeRelativeBounds_selectedBounds(GrammarBuilders g, selectedBounds value0)
		{
			this._node = g.UnnamedConversion.beforeRelativeBounds_selectedBounds.BuildASTNode(value0.Node);
		}

		// Token: 0x06004E83 RID: 20099 RVA: 0x000F8D3C File Offset: 0x000F6F3C
		public static implicit operator beforeRelativeBounds(beforeRelativeBounds_selectedBounds arg)
		{
			return beforeRelativeBounds.CreateUnsafe(arg.Node);
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06004E84 RID: 20100 RVA: 0x000F8D4A File Offset: 0x000F6F4A
		public selectedBounds selectedBounds
		{
			get
			{
				return selectedBounds.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06004E85 RID: 20101 RVA: 0x000F8D5E File Offset: 0x000F6F5E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004E86 RID: 20102 RVA: 0x000F8D74 File Offset: 0x000F6F74
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004E87 RID: 20103 RVA: 0x000F8D9E File Offset: 0x000F6F9E
		public bool Equals(beforeRelativeBounds_selectedBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400231B RID: 8987
		private ProgramNode _node;
	}
}
