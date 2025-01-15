using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E6B RID: 3691
	public struct splitForTitle : IProgramNodeBuilder, IEquatable<splitForTitle>
	{
		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x0600647B RID: 25723 RVA: 0x001463F6 File Offset: 0x001445F6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600647C RID: 25724 RVA: 0x001463FE File Offset: 0x001445FE
		private splitForTitle(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600647D RID: 25725 RVA: 0x00146407 File Offset: 0x00144607
		public static splitForTitle CreateUnsafe(ProgramNode node)
		{
			return new splitForTitle(node);
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x00146410 File Offset: 0x00144610
		public static splitForTitle? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.splitForTitle)
			{
				return null;
			}
			return new splitForTitle?(splitForTitle.CreateUnsafe(node));
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x0014644A File Offset: 0x0014464A
		public static splitForTitle CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new splitForTitle(new Hole(g.Symbol.splitForTitle, holeId));
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x00146462 File Offset: 0x00144662
		public bool Is_TitleSplitOnEmptyRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TitleSplitOnEmptyRows;
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x0014647C File Offset: 0x0014467C
		public bool Is_TitleSplitOnEmptyRows(GrammarBuilders g, out TitleSplitOnEmptyRows value)
		{
			if (this.Node.GrammarRule == g.Rule.TitleSplitOnEmptyRows)
			{
				value = TitleSplitOnEmptyRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TitleSplitOnEmptyRows);
			return false;
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x001464B4 File Offset: 0x001446B4
		public TitleSplitOnEmptyRows? As_TitleSplitOnEmptyRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TitleSplitOnEmptyRows)
			{
				return null;
			}
			return new TitleSplitOnEmptyRows?(TitleSplitOnEmptyRows.CreateUnsafe(this.Node));
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x001464F4 File Offset: 0x001446F4
		public TitleSplitOnEmptyRows Cast_TitleSplitOnEmptyRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TitleSplitOnEmptyRows)
			{
				return TitleSplitOnEmptyRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TitleSplitOnEmptyRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006484 RID: 25732 RVA: 0x00146549 File Offset: 0x00144749
		public bool Is_TitleSplitOnMatchingRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TitleSplitOnMatchingRows;
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x00146563 File Offset: 0x00144763
		public bool Is_TitleSplitOnMatchingRows(GrammarBuilders g, out TitleSplitOnMatchingRows value)
		{
			if (this.Node.GrammarRule == g.Rule.TitleSplitOnMatchingRows)
			{
				value = TitleSplitOnMatchingRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TitleSplitOnMatchingRows);
			return false;
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x00146598 File Offset: 0x00144798
		public TitleSplitOnMatchingRows? As_TitleSplitOnMatchingRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TitleSplitOnMatchingRows)
			{
				return null;
			}
			return new TitleSplitOnMatchingRows?(TitleSplitOnMatchingRows.CreateUnsafe(this.Node));
		}

		// Token: 0x06006487 RID: 25735 RVA: 0x001465D8 File Offset: 0x001447D8
		public TitleSplitOnMatchingRows Cast_TitleSplitOnMatchingRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TitleSplitOnMatchingRows)
			{
				return TitleSplitOnMatchingRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TitleSplitOnMatchingRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x00146630 File Offset: 0x00144830
		public T Switch<T>(GrammarBuilders g, Func<TitleSplitOnEmptyRows, T> func0, Func<TitleSplitOnMatchingRows, T> func1)
		{
			TitleSplitOnEmptyRows titleSplitOnEmptyRows;
			if (this.Is_TitleSplitOnEmptyRows(g, out titleSplitOnEmptyRows))
			{
				return func0(titleSplitOnEmptyRows);
			}
			TitleSplitOnMatchingRows titleSplitOnMatchingRows;
			if (this.Is_TitleSplitOnMatchingRows(g, out titleSplitOnMatchingRows))
			{
				return func1(titleSplitOnMatchingRows);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitForTitle");
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x00146688 File Offset: 0x00144888
		public void Switch(GrammarBuilders g, Action<TitleSplitOnEmptyRows> func0, Action<TitleSplitOnMatchingRows> func1)
		{
			TitleSplitOnEmptyRows titleSplitOnEmptyRows;
			if (this.Is_TitleSplitOnEmptyRows(g, out titleSplitOnEmptyRows))
			{
				func0(titleSplitOnEmptyRows);
				return;
			}
			TitleSplitOnMatchingRows titleSplitOnMatchingRows;
			if (this.Is_TitleSplitOnMatchingRows(g, out titleSplitOnMatchingRows))
			{
				func1(titleSplitOnMatchingRows);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol splitForTitle");
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x001466DF File Offset: 0x001448DF
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600648B RID: 25739 RVA: 0x001466F4 File Offset: 0x001448F4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600648C RID: 25740 RVA: 0x0014671E File Offset: 0x0014491E
		public bool Equals(splitForTitle other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C15 RID: 11285
		private ProgramNode _node;
	}
}
