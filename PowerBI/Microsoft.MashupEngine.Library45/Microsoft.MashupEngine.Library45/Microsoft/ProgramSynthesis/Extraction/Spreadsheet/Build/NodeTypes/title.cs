using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E66 RID: 3686
	public struct title : IProgramNodeBuilder, IEquatable<title>
	{
		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x0600641B RID: 25627 RVA: 0x001452F2 File Offset: 0x001434F2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600641C RID: 25628 RVA: 0x001452FA File Offset: 0x001434FA
		private title(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600641D RID: 25629 RVA: 0x00145303 File Offset: 0x00143503
		public static title CreateUnsafe(ProgramNode node)
		{
			return new title(node);
		}

		// Token: 0x0600641E RID: 25630 RVA: 0x0014530C File Offset: 0x0014350C
		public static title? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.title)
			{
				return null;
			}
			return new title?(title.CreateUnsafe(node));
		}

		// Token: 0x0600641F RID: 25631 RVA: 0x00145346 File Offset: 0x00143546
		public static title CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new title(new Hole(g.Symbol.title, holeId));
		}

		// Token: 0x06006420 RID: 25632 RVA: 0x0014535E File Offset: 0x0014355E
		public bool Is_title_above(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.title_above;
		}

		// Token: 0x06006421 RID: 25633 RVA: 0x00145378 File Offset: 0x00143578
		public bool Is_title_above(GrammarBuilders g, out title_above value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.title_above)
			{
				value = title_above.CreateUnsafe(this.Node);
				return true;
			}
			value = default(title_above);
			return false;
		}

		// Token: 0x06006422 RID: 25634 RVA: 0x001453B0 File Offset: 0x001435B0
		public title_above? As_title_above(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.title_above)
			{
				return null;
			}
			return new title_above?(title_above.CreateUnsafe(this.Node));
		}

		// Token: 0x06006423 RID: 25635 RVA: 0x001453F0 File Offset: 0x001435F0
		public title_above Cast_title_above(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.title_above)
			{
				return title_above.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_title_above is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006424 RID: 25636 RVA: 0x00145445 File Offset: 0x00143645
		public bool Is_TopLeftCell(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TopLeftCell;
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x0014545F File Offset: 0x0014365F
		public bool Is_TopLeftCell(GrammarBuilders g, out TopLeftCell value)
		{
			if (this.Node.GrammarRule == g.Rule.TopLeftCell)
			{
				value = TopLeftCell.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TopLeftCell);
			return false;
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x00145494 File Offset: 0x00143694
		public TopLeftCell? As_TopLeftCell(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TopLeftCell)
			{
				return null;
			}
			return new TopLeftCell?(TopLeftCell.CreateUnsafe(this.Node));
		}

		// Token: 0x06006427 RID: 25639 RVA: 0x001454D4 File Offset: 0x001436D4
		public TopLeftCell Cast_TopLeftCell(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TopLeftCell)
			{
				return TopLeftCell.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TopLeftCell is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006428 RID: 25640 RVA: 0x00145529 File Offset: 0x00143729
		public bool Is_TopSameFontCells(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TopSameFontCells;
		}

		// Token: 0x06006429 RID: 25641 RVA: 0x00145543 File Offset: 0x00143743
		public bool Is_TopSameFontCells(GrammarBuilders g, out TopSameFontCells value)
		{
			if (this.Node.GrammarRule == g.Rule.TopSameFontCells)
			{
				value = TopSameFontCells.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TopSameFontCells);
			return false;
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x00145578 File Offset: 0x00143778
		public TopSameFontCells? As_TopSameFontCells(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TopSameFontCells)
			{
				return null;
			}
			return new TopSameFontCells?(TopSameFontCells.CreateUnsafe(this.Node));
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x001455B8 File Offset: 0x001437B8
		public TopSameFontCells Cast_TopSameFontCells(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TopSameFontCells)
			{
				return TopSameFontCells.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TopSameFontCells is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x0014560D File Offset: 0x0014380D
		public bool Is_BottomLeftSameFontCells(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.BottomLeftSameFontCells;
		}

		// Token: 0x0600642D RID: 25645 RVA: 0x00145627 File Offset: 0x00143827
		public bool Is_BottomLeftSameFontCells(GrammarBuilders g, out BottomLeftSameFontCells value)
		{
			if (this.Node.GrammarRule == g.Rule.BottomLeftSameFontCells)
			{
				value = BottomLeftSameFontCells.CreateUnsafe(this.Node);
				return true;
			}
			value = default(BottomLeftSameFontCells);
			return false;
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x0014565C File Offset: 0x0014385C
		public BottomLeftSameFontCells? As_BottomLeftSameFontCells(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.BottomLeftSameFontCells)
			{
				return null;
			}
			return new BottomLeftSameFontCells?(BottomLeftSameFontCells.CreateUnsafe(this.Node));
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x0014569C File Offset: 0x0014389C
		public BottomLeftSameFontCells Cast_BottomLeftSameFontCells(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.BottomLeftSameFontCells)
			{
				return BottomLeftSameFontCells.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_BottomLeftSameFontCells is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006430 RID: 25648 RVA: 0x001456F4 File Offset: 0x001438F4
		public T Switch<T>(GrammarBuilders g, Func<title_above, T> func0, Func<TopLeftCell, T> func1, Func<TopSameFontCells, T> func2, Func<BottomLeftSameFontCells, T> func3)
		{
			title_above title_above;
			if (this.Is_title_above(g, out title_above))
			{
				return func0(title_above);
			}
			TopLeftCell topLeftCell;
			if (this.Is_TopLeftCell(g, out topLeftCell))
			{
				return func1(topLeftCell);
			}
			TopSameFontCells topSameFontCells;
			if (this.Is_TopSameFontCells(g, out topSameFontCells))
			{
				return func2(topSameFontCells);
			}
			BottomLeftSameFontCells bottomLeftSameFontCells;
			if (this.Is_BottomLeftSameFontCells(g, out bottomLeftSameFontCells))
			{
				return func3(bottomLeftSameFontCells);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol title");
		}

		// Token: 0x06006431 RID: 25649 RVA: 0x00145774 File Offset: 0x00143974
		public void Switch(GrammarBuilders g, Action<title_above> func0, Action<TopLeftCell> func1, Action<TopSameFontCells> func2, Action<BottomLeftSameFontCells> func3)
		{
			title_above title_above;
			if (this.Is_title_above(g, out title_above))
			{
				func0(title_above);
				return;
			}
			TopLeftCell topLeftCell;
			if (this.Is_TopLeftCell(g, out topLeftCell))
			{
				func1(topLeftCell);
				return;
			}
			TopSameFontCells topSameFontCells;
			if (this.Is_TopSameFontCells(g, out topSameFontCells))
			{
				func2(topSameFontCells);
				return;
			}
			BottomLeftSameFontCells bottomLeftSameFontCells;
			if (this.Is_BottomLeftSameFontCells(g, out bottomLeftSameFontCells))
			{
				func3(bottomLeftSameFontCells);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol title");
		}

		// Token: 0x06006432 RID: 25650 RVA: 0x001457F3 File Offset: 0x001439F3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x00145808 File Offset: 0x00143A08
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006434 RID: 25652 RVA: 0x00145832 File Offset: 0x00143A32
		public bool Equals(title other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C10 RID: 11280
		private ProgramNode _node;
	}
}
