using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E47 RID: 3655
	public struct TitleCellsAbove : IProgramNodeBuilder, IEquatable<TitleCellsAbove>
	{
		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06006210 RID: 25104 RVA: 0x00140052 File Offset: 0x0013E252
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006211 RID: 25105 RVA: 0x0014005A File Offset: 0x0013E25A
		private TitleCellsAbove(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006212 RID: 25106 RVA: 0x00140063 File Offset: 0x0013E263
		public static TitleCellsAbove CreateUnsafe(ProgramNode node)
		{
			return new TitleCellsAbove(node);
		}

		// Token: 0x06006213 RID: 25107 RVA: 0x0014006C File Offset: 0x0013E26C
		public static TitleCellsAbove? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TitleCellsAbove)
			{
				return null;
			}
			return new TitleCellsAbove?(TitleCellsAbove.CreateUnsafe(node));
		}

		// Token: 0x06006214 RID: 25108 RVA: 0x001400A1 File Offset: 0x0013E2A1
		public TitleCellsAbove(GrammarBuilders g, titleOf value0, titleAboveMode value1)
		{
			this._node = g.Rule.TitleCellsAbove.BuildASTNode(value0.Node, value1.Node);
		}

		// Token: 0x06006215 RID: 25109 RVA: 0x001400C7 File Offset: 0x0013E2C7
		public static implicit operator above(TitleCellsAbove arg)
		{
			return above.CreateUnsafe(arg.Node);
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06006216 RID: 25110 RVA: 0x001400D5 File Offset: 0x0013E2D5
		public titleOf titleOf
		{
			get
			{
				return titleOf.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06006217 RID: 25111 RVA: 0x001400E9 File Offset: 0x0013E2E9
		public titleAboveMode titleAboveMode
		{
			get
			{
				return titleAboveMode.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x06006218 RID: 25112 RVA: 0x001400FD File Offset: 0x0013E2FD
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006219 RID: 25113 RVA: 0x00140110 File Offset: 0x0013E310
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600621A RID: 25114 RVA: 0x0014013A File Offset: 0x0013E33A
		public bool Equals(TitleCellsAbove other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BF1 RID: 11249
		private ProgramNode _node;
	}
}
