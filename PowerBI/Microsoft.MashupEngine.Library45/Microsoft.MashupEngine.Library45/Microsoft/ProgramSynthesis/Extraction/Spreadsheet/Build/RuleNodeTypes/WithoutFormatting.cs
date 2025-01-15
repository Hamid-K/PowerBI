using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E3E RID: 3646
	public struct WithoutFormatting : IProgramNodeBuilder, IEquatable<WithoutFormatting>
	{
		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x060061B4 RID: 25012 RVA: 0x0013F81A File Offset: 0x0013DA1A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060061B5 RID: 25013 RVA: 0x0013F822 File Offset: 0x0013DA22
		private WithoutFormatting(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060061B6 RID: 25014 RVA: 0x0013F82B File Offset: 0x0013DA2B
		public static WithoutFormatting CreateUnsafe(ProgramNode node)
		{
			return new WithoutFormatting(node);
		}

		// Token: 0x060061B7 RID: 25015 RVA: 0x0013F834 File Offset: 0x0013DA34
		public static WithoutFormatting? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.WithoutFormatting)
			{
				return null;
			}
			return new WithoutFormatting?(WithoutFormatting.CreateUnsafe(node));
		}

		// Token: 0x060061B8 RID: 25016 RVA: 0x0013F869 File Offset: 0x0013DA69
		public WithoutFormatting(GrammarBuilders g, sheetPair value0)
		{
			this._node = g.Rule.WithoutFormatting.BuildASTNode(value0.Node);
		}

		// Token: 0x060061B9 RID: 25017 RVA: 0x0013F888 File Offset: 0x0013DA88
		public static implicit operator withoutFormatting(WithoutFormatting arg)
		{
			return withoutFormatting.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x060061BA RID: 25018 RVA: 0x0013F896 File Offset: 0x0013DA96
		public sheetPair sheetPair
		{
			get
			{
				return sheetPair.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060061BB RID: 25019 RVA: 0x0013F8AA File Offset: 0x0013DAAA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060061BC RID: 25020 RVA: 0x0013F8C0 File Offset: 0x0013DAC0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060061BD RID: 25021 RVA: 0x0013F8EA File Offset: 0x0013DAEA
		public bool Equals(WithoutFormatting other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BE8 RID: 11240
		private ProgramNode _node;
	}
}
