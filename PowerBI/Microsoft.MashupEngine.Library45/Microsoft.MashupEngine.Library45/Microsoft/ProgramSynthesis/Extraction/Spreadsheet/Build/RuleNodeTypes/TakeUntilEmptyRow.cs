using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes
{
	// Token: 0x02000E1D RID: 3613
	public struct TakeUntilEmptyRow : IProgramNodeBuilder, IEquatable<TakeUntilEmptyRow>
	{
		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06006063 RID: 24675 RVA: 0x0013D9DE File Offset: 0x0013BBDE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x0013D9E6 File Offset: 0x0013BBE6
		private TakeUntilEmptyRow(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x0013D9EF File Offset: 0x0013BBEF
		public static TakeUntilEmptyRow CreateUnsafe(ProgramNode node)
		{
			return new TakeUntilEmptyRow(node);
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x0013D9F8 File Offset: 0x0013BBF8
		public static TakeUntilEmptyRow? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.TakeUntilEmptyRow)
			{
				return null;
			}
			return new TakeUntilEmptyRow?(TakeUntilEmptyRow.CreateUnsafe(node));
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x0013DA2D File Offset: 0x0013BC2D
		public TakeUntilEmptyRow(GrammarBuilders g, trimTop value0)
		{
			this._node = g.Rule.TakeUntilEmptyRow.BuildASTNode(value0.Node);
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x0013DA4C File Offset: 0x0013BC4C
		public static implicit operator trimBottom(TakeUntilEmptyRow arg)
		{
			return trimBottom.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06006069 RID: 24681 RVA: 0x0013DA5A File Offset: 0x0013BC5A
		public trimTop trimTop
		{
			get
			{
				return trimTop.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x0013DA6E File Offset: 0x0013BC6E
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x0013DA84 File Offset: 0x0013BC84
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600606C RID: 24684 RVA: 0x0013DAAE File Offset: 0x0013BCAE
		public bool Equals(TakeUntilEmptyRow other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BC7 RID: 11207
		private ProgramNode _node;
	}
}
