using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E63 RID: 3683
	public struct mSection : IProgramNodeBuilder, IEquatable<mSection>
	{
		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x060063F1 RID: 25585 RVA: 0x00144DD6 File Offset: 0x00142FD6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060063F2 RID: 25586 RVA: 0x00144DDE File Offset: 0x00142FDE
		private mSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060063F3 RID: 25587 RVA: 0x00144DE7 File Offset: 0x00142FE7
		public static mSection CreateUnsafe(ProgramNode node)
		{
			return new mSection(node);
		}

		// Token: 0x060063F4 RID: 25588 RVA: 0x00144DF0 File Offset: 0x00142FF0
		public static mSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.mSection)
			{
				return null;
			}
			return new mSection?(mSection.CreateUnsafe(node));
		}

		// Token: 0x060063F5 RID: 25589 RVA: 0x00144E2A File Offset: 0x0014302A
		public static mSection CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new mSection(new Hole(g.Symbol.mSection, holeId));
		}

		// Token: 0x060063F6 RID: 25590 RVA: 0x00144E42 File Offset: 0x00143042
		public bool Is_MSplitOnEmptyRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MSplitOnEmptyRows;
		}

		// Token: 0x060063F7 RID: 25591 RVA: 0x00144E5C File Offset: 0x0014305C
		public bool Is_MSplitOnEmptyRows(GrammarBuilders g, out MSplitOnEmptyRows value)
		{
			if (this.Node.GrammarRule == g.Rule.MSplitOnEmptyRows)
			{
				value = MSplitOnEmptyRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MSplitOnEmptyRows);
			return false;
		}

		// Token: 0x060063F8 RID: 25592 RVA: 0x00144E94 File Offset: 0x00143094
		public MSplitOnEmptyRows? As_MSplitOnEmptyRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MSplitOnEmptyRows)
			{
				return null;
			}
			return new MSplitOnEmptyRows?(MSplitOnEmptyRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060063F9 RID: 25593 RVA: 0x00144ED4 File Offset: 0x001430D4
		public MSplitOnEmptyRows Cast_MSplitOnEmptyRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MSplitOnEmptyRows)
			{
				return MSplitOnEmptyRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MSplitOnEmptyRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063FA RID: 25594 RVA: 0x00144F29 File Offset: 0x00143129
		public bool Is_MSplitOnEmptyColumns(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MSplitOnEmptyColumns;
		}

		// Token: 0x060063FB RID: 25595 RVA: 0x00144F43 File Offset: 0x00143143
		public bool Is_MSplitOnEmptyColumns(GrammarBuilders g, out MSplitOnEmptyColumns value)
		{
			if (this.Node.GrammarRule == g.Rule.MSplitOnEmptyColumns)
			{
				value = MSplitOnEmptyColumns.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MSplitOnEmptyColumns);
			return false;
		}

		// Token: 0x060063FC RID: 25596 RVA: 0x00144F78 File Offset: 0x00143178
		public MSplitOnEmptyColumns? As_MSplitOnEmptyColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MSplitOnEmptyColumns)
			{
				return null;
			}
			return new MSplitOnEmptyColumns?(MSplitOnEmptyColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x060063FD RID: 25597 RVA: 0x00144FB8 File Offset: 0x001431B8
		public MSplitOnEmptyColumns Cast_MSplitOnEmptyColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MSplitOnEmptyColumns)
			{
				return MSplitOnEmptyColumns.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MSplitOnEmptyColumns is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063FE RID: 25598 RVA: 0x00145010 File Offset: 0x00143210
		public T Switch<T>(GrammarBuilders g, Func<MSplitOnEmptyRows, T> func0, Func<MSplitOnEmptyColumns, T> func1)
		{
			MSplitOnEmptyRows msplitOnEmptyRows;
			if (this.Is_MSplitOnEmptyRows(g, out msplitOnEmptyRows))
			{
				return func0(msplitOnEmptyRows);
			}
			MSplitOnEmptyColumns msplitOnEmptyColumns;
			if (this.Is_MSplitOnEmptyColumns(g, out msplitOnEmptyColumns))
			{
				return func1(msplitOnEmptyColumns);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol mSection");
		}

		// Token: 0x060063FF RID: 25599 RVA: 0x00145068 File Offset: 0x00143268
		public void Switch(GrammarBuilders g, Action<MSplitOnEmptyRows> func0, Action<MSplitOnEmptyColumns> func1)
		{
			MSplitOnEmptyRows msplitOnEmptyRows;
			if (this.Is_MSplitOnEmptyRows(g, out msplitOnEmptyRows))
			{
				func0(msplitOnEmptyRows);
				return;
			}
			MSplitOnEmptyColumns msplitOnEmptyColumns;
			if (this.Is_MSplitOnEmptyColumns(g, out msplitOnEmptyColumns))
			{
				func1(msplitOnEmptyColumns);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol mSection");
		}

		// Token: 0x06006400 RID: 25600 RVA: 0x001450BF File Offset: 0x001432BF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006401 RID: 25601 RVA: 0x001450D4 File Offset: 0x001432D4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006402 RID: 25602 RVA: 0x001450FE File Offset: 0x001432FE
		public bool Equals(mSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C0D RID: 11277
		private ProgramNode _node;
	}
}
