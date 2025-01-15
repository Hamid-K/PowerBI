using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes
{
	// Token: 0x02001038 RID: 4152
	public struct ExtractTable : IProgramNodeBuilder, IEquatable<ExtractTable>
	{
		// Token: 0x170015E1 RID: 5601
		// (get) Token: 0x06007ADA RID: 31450 RVA: 0x001A26EA File Offset: 0x001A08EA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007ADB RID: 31451 RVA: 0x001A26F2 File Offset: 0x001A08F2
		private ExtractTable(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007ADC RID: 31452 RVA: 0x001A26FB File Offset: 0x001A08FB
		public static ExtractTable CreateUnsafe(ProgramNode node)
		{
			return new ExtractTable(node);
		}

		// Token: 0x06007ADD RID: 31453 RVA: 0x001A2704 File Offset: 0x001A0904
		public static ExtractTable? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.ExtractTable)
			{
				return null;
			}
			return new ExtractTable?(ExtractTable.CreateUnsafe(node));
		}

		// Token: 0x06007ADE RID: 31454 RVA: 0x001A2739 File Offset: 0x001A0939
		public ExtractTable(GrammarBuilders g, columnSelectors value0)
		{
			this._node = g.Rule.ExtractTable.BuildASTNode(value0.Node);
		}

		// Token: 0x06007ADF RID: 31455 RVA: 0x001A2758 File Offset: 0x001A0958
		public static implicit operator resultTable(ExtractTable arg)
		{
			return resultTable.CreateUnsafe(arg.Node);
		}

		// Token: 0x170015E2 RID: 5602
		// (get) Token: 0x06007AE0 RID: 31456 RVA: 0x001A2766 File Offset: 0x001A0966
		public columnSelectors columnSelectors
		{
			get
			{
				return columnSelectors.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x06007AE1 RID: 31457 RVA: 0x001A277A File Offset: 0x001A097A
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007AE2 RID: 31458 RVA: 0x001A2790 File Offset: 0x001A0990
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007AE3 RID: 31459 RVA: 0x001A27BA File Offset: 0x001A09BA
		public bool Equals(ExtractTable other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003351 RID: 13137
		private ProgramNode _node;
	}
}
