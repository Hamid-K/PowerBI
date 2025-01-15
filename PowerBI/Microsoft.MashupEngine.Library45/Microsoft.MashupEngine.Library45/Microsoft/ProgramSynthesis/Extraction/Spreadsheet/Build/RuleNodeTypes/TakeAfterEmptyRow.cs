using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E26 RID: 3622
	public struct TakeAfterEmptyRow : IProgramNodeBuilder, IEquatable<TakeAfterEmptyRow>
	{
		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x060060BD RID: 24765 RVA: 0x0013E1E2 File Offset: 0x0013C3E2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060060BE RID: 24766 RVA: 0x0013E1EA File Offset: 0x0013C3EA
		private TakeAfterEmptyRow(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x0013E1F3 File Offset: 0x0013C3F3
		public static TakeAfterEmptyRow CreateUnsafe(ProgramNode node)
		{
			return new TakeAfterEmptyRow(node);
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x0013E1FC File Offset: 0x0013C3FC
		public static TakeAfterEmptyRow? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TakeAfterEmptyRow)
			{
				return null;
			}
			return new TakeAfterEmptyRow?(TakeAfterEmptyRow.CreateUnsafe(node));
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x0013E231 File Offset: 0x0013C431
		public TakeAfterEmptyRow(GrammarBuilders g, sheetSection value0)
		{
			this._node = g.Rule.TakeAfterEmptyRow.BuildASTNode(value0.Node);
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x0013E250 File Offset: 0x0013C450
		public static implicit operator trimTop(TakeAfterEmptyRow arg)
		{
			return trimTop.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x060060C3 RID: 24771 RVA: 0x0013E25E File Offset: 0x0013C45E
		public sheetSection sheetSection
		{
			get
			{
				return sheetSection.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x0013E272 File Offset: 0x0013C472
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x0013E288 File Offset: 0x0013C488
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060060C6 RID: 24774 RVA: 0x0013E2B2 File Offset: 0x0013C4B2
		public bool Equals(TakeAfterEmptyRow other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BD0 RID: 11216
		private ProgramNode _node;
	}
}
