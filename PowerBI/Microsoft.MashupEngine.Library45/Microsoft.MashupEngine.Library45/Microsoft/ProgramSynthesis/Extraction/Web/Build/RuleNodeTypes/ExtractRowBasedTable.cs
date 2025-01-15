using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001039 RID: 4153
	public struct ExtractRowBasedTable : IProgramNodeBuilder, IEquatable<ExtractRowBasedTable>
	{
		// Token: 0x170015E3 RID: 5603
		// (get) Token: 0x06007AE4 RID: 31460 RVA: 0x001A27CE File Offset: 0x001A09CE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007AE5 RID: 31461 RVA: 0x001A27D6 File Offset: 0x001A09D6
		private ExtractRowBasedTable(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007AE6 RID: 31462 RVA: 0x001A27DF File Offset: 0x001A09DF
		public static ExtractRowBasedTable CreateUnsafe(ProgramNode node)
		{
			return new ExtractRowBasedTable(node);
		}

		// Token: 0x06007AE7 RID: 31463 RVA: 0x001A27E8 File Offset: 0x001A09E8
		public static ExtractRowBasedTable? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ExtractRowBasedTable)
			{
				return null;
			}
			return new ExtractRowBasedTable?(ExtractRowBasedTable.CreateUnsafe(node));
		}

		// Token: 0x06007AE8 RID: 31464 RVA: 0x001A281D File Offset: 0x001A0A1D
		public ExtractRowBasedTable(GrammarBuilders g, columnSelectors value0, resultSequence value1)
		{
			this._node = g.Rule.ExtractRowBasedTable.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06007AE9 RID: 31465 RVA: 0x001A2843 File Offset: 0x001A0A43
		public static implicit operator resultTable(ExtractRowBasedTable arg)
		{
			return resultTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06007AEA RID: 31466 RVA: 0x001A2851 File Offset: 0x001A0A51
		public columnSelectors columnSelectors
		{
			get
			{
				return columnSelectors.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06007AEB RID: 31467 RVA: 0x001A2865 File Offset: 0x001A0A65
		public resultSequence resultSequence
		{
			get
			{
				return resultSequence.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06007AEC RID: 31468 RVA: 0x001A2879 File Offset: 0x001A0A79
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007AED RID: 31469 RVA: 0x001A288C File Offset: 0x001A0A8C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007AEE RID: 31470 RVA: 0x001A28B6 File Offset: 0x001A0AB6
		public bool Equals(ExtractRowBasedTable other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003352 RID: 13138
		private ProgramNode _node;
	}
}
