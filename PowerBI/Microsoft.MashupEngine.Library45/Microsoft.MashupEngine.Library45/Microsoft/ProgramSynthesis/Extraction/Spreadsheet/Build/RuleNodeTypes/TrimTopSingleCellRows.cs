using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E24 RID: 3620
	public struct TrimTopSingleCellRows : IProgramNodeBuilder, IEquatable<TrimTopSingleCellRows>
	{
		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x060060A9 RID: 24745 RVA: 0x0013E01A File Offset: 0x0013C21A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060060AA RID: 24746 RVA: 0x0013E022 File Offset: 0x0013C222
		private TrimTopSingleCellRows(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x0013E02B File Offset: 0x0013C22B
		public static TrimTopSingleCellRows CreateUnsafe(ProgramNode node)
		{
			return new TrimTopSingleCellRows(node);
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x0013E034 File Offset: 0x0013C234
		public static TrimTopSingleCellRows? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TrimTopSingleCellRows)
			{
				return null;
			}
			return new TrimTopSingleCellRows?(TrimTopSingleCellRows.CreateUnsafe(node));
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x0013E069 File Offset: 0x0013C269
		public TrimTopSingleCellRows(GrammarBuilders g, sheetSection value0)
		{
			this._node = g.Rule.TrimTopSingleCellRows.BuildASTNode(value0.Node);
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x0013E088 File Offset: 0x0013C288
		public static implicit operator trimTop(TrimTopSingleCellRows arg)
		{
			return trimTop.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060060AF RID: 24751 RVA: 0x0013E096 File Offset: 0x0013C296
		public sheetSection sheetSection
		{
			get
			{
				return sheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060060B0 RID: 24752 RVA: 0x0013E0AA File Offset: 0x0013C2AA
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x0013E0C0 File Offset: 0x0013C2C0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x0013E0EA File Offset: 0x0013C2EA
		public bool Equals(TrimTopSingleCellRows other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BCE RID: 11214
		private ProgramNode _node;
	}
}
