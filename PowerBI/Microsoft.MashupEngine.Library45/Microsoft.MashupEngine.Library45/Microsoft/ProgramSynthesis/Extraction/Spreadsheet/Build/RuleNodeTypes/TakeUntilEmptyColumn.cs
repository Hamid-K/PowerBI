using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E27 RID: 3623
	public struct TakeUntilEmptyColumn : IProgramNodeBuilder, IEquatable<TakeUntilEmptyColumn>
	{
		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x060060C7 RID: 24775 RVA: 0x0013E2C6 File Offset: 0x0013C4C6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060060C8 RID: 24776 RVA: 0x0013E2CE File Offset: 0x0013C4CE
		private TakeUntilEmptyColumn(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060060C9 RID: 24777 RVA: 0x0013E2D7 File Offset: 0x0013C4D7
		public static TakeUntilEmptyColumn CreateUnsafe(ProgramNode node)
		{
			return new TakeUntilEmptyColumn(node);
		}

		// Token: 0x060060CA RID: 24778 RVA: 0x0013E2E0 File Offset: 0x0013C4E0
		public static TakeUntilEmptyColumn? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TakeUntilEmptyColumn)
			{
				return null;
			}
			return new TakeUntilEmptyColumn?(TakeUntilEmptyColumn.CreateUnsafe(node));
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x0013E315 File Offset: 0x0013C515
		public TakeUntilEmptyColumn(GrammarBuilders g, horizontalSheetSection value0)
		{
			this._node = g.Rule.TakeUntilEmptyColumn.BuildASTNode(value0.Node);
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x0013E334 File Offset: 0x0013C534
		public static implicit operator sheetSection(TakeUntilEmptyColumn arg)
		{
			return sheetSection.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x060060CD RID: 24781 RVA: 0x0013E342 File Offset: 0x0013C542
		public horizontalSheetSection horizontalSheetSection
		{
			get
			{
				return horizontalSheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x0013E356 File Offset: 0x0013C556
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x0013E36C File Offset: 0x0013C56C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x0013E396 File Offset: 0x0013C596
		public bool Equals(TakeUntilEmptyColumn other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BD1 RID: 11217
		private ProgramNode _node;
	}
}
