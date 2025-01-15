using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E44 RID: 3652
	public struct FirstSplit : IProgramNodeBuilder, IEquatable<FirstSplit>
	{
		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x060061F0 RID: 25072 RVA: 0x0013FD72 File Offset: 0x0013DF72
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x0013FD7A File Offset: 0x0013DF7A
		private FirstSplit(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060061F2 RID: 25074 RVA: 0x0013FD83 File Offset: 0x0013DF83
		public static FirstSplit CreateUnsafe(ProgramNode node)
		{
			return new FirstSplit(node);
		}

		// Token: 0x060061F3 RID: 25075 RVA: 0x0013FD8C File Offset: 0x0013DF8C
		public static FirstSplit? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.FirstSplit)
			{
				return null;
			}
			return new FirstSplit?(FirstSplit.CreateUnsafe(node));
		}

		// Token: 0x060061F4 RID: 25076 RVA: 0x0013FDC1 File Offset: 0x0013DFC1
		public FirstSplit(GrammarBuilders g, splitForTitle value0)
		{
			this._node = g.Rule.FirstSplit.BuildASTNode(value0.Node);
		}

		// Token: 0x060061F5 RID: 25077 RVA: 0x0013FDE0 File Offset: 0x0013DFE0
		public static implicit operator headerSection(FirstSplit arg)
		{
			return headerSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x060061F6 RID: 25078 RVA: 0x0013FDEE File Offset: 0x0013DFEE
		public splitForTitle splitForTitle
		{
			get
			{
				return splitForTitle.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060061F7 RID: 25079 RVA: 0x0013FE02 File Offset: 0x0013E002
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060061F8 RID: 25080 RVA: 0x0013FE18 File Offset: 0x0013E018
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060061F9 RID: 25081 RVA: 0x0013FE42 File Offset: 0x0013E042
		public bool Equals(FirstSplit other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BEE RID: 11246
		private ProgramNode _node;
	}
}
