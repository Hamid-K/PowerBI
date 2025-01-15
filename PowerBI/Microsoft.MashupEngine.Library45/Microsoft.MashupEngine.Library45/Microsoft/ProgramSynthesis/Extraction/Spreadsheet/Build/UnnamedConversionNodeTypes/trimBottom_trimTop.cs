using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02000E0A RID: 3594
	public struct trimBottom_trimTop : IProgramNodeBuilder, IEquatable<trimBottom_trimTop>
	{
		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06005FA4 RID: 24484 RVA: 0x0013C8DA File Offset: 0x0013AADA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06005FA5 RID: 24485 RVA: 0x0013C8E2 File Offset: 0x0013AAE2
		private trimBottom_trimTop(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06005FA6 RID: 24486 RVA: 0x0013C8EB File Offset: 0x0013AAEB
		public static trimBottom_trimTop CreateUnsafe(ProgramNode node)
		{
			return new trimBottom_trimTop(node);
		}

		// Token: 0x06005FA7 RID: 24487 RVA: 0x0013C8F4 File Offset: 0x0013AAF4
		public static trimBottom_trimTop? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.trimBottom_trimTop)
			{
				return null;
			}
			return new trimBottom_trimTop?(trimBottom_trimTop.CreateUnsafe(node));
		}

		// Token: 0x06005FA8 RID: 24488 RVA: 0x0013C929 File Offset: 0x0013AB29
		public trimBottom_trimTop(GrammarBuilders g, trimTop value0)
		{
			this._node = g.UnnamedConversion.trimBottom_trimTop.BuildASTNode(value0.Node);
		}

		// Token: 0x06005FA9 RID: 24489 RVA: 0x0013C948 File Offset: 0x0013AB48
		public static implicit operator trimBottom(trimBottom_trimTop arg)
		{
			return trimBottom.CreateUnsafe(arg.Node);
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06005FAA RID: 24490 RVA: 0x0013C956 File Offset: 0x0013AB56
		public trimTop trimTop
		{
			get
			{
				return trimTop.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06005FAB RID: 24491 RVA: 0x0013C96A File Offset: 0x0013AB6A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06005FAC RID: 24492 RVA: 0x0013C980 File Offset: 0x0013AB80
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06005FAD RID: 24493 RVA: 0x0013C9AA File Offset: 0x0013ABAA
		public bool Equals(trimBottom_trimTop other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BB4 RID: 11188
		private ProgramNode _node;
	}
}
