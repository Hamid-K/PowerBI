using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E55 RID: 3669
	public struct trimBottom : IProgramNodeBuilder, IEquatable<trimBottom>
	{
		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x060062BD RID: 25277 RVA: 0x001414BA File Offset: 0x0013F6BA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060062BE RID: 25278 RVA: 0x001414C2 File Offset: 0x0013F6C2
		private trimBottom(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060062BF RID: 25279 RVA: 0x001414CB File Offset: 0x0013F6CB
		public static trimBottom CreateUnsafe(ProgramNode node)
		{
			return new trimBottom(node);
		}

		// Token: 0x060062C0 RID: 25280 RVA: 0x001414D4 File Offset: 0x0013F6D4
		public static trimBottom? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.trimBottom)
			{
				return null;
			}
			return new trimBottom?(trimBottom.CreateUnsafe(node));
		}

		// Token: 0x060062C1 RID: 25281 RVA: 0x0014150E File Offset: 0x0013F70E
		public static trimBottom CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new trimBottom(new Hole(g.Symbol.trimBottom, holeId));
		}

		// Token: 0x060062C2 RID: 25282 RVA: 0x00141526 File Offset: 0x0013F726
		public bool Is_trimBottom_trimTop(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.trimBottom_trimTop;
		}

		// Token: 0x060062C3 RID: 25283 RVA: 0x00141540 File Offset: 0x0013F740
		public bool Is_trimBottom_trimTop(GrammarBuilders g, out trimBottom_trimTop value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.trimBottom_trimTop)
			{
				value = trimBottom_trimTop.CreateUnsafe(this.Node);
				return true;
			}
			value = default(trimBottom_trimTop);
			return false;
		}

		// Token: 0x060062C4 RID: 25284 RVA: 0x00141578 File Offset: 0x0013F778
		public trimBottom_trimTop? As_trimBottom_trimTop(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.trimBottom_trimTop)
			{
				return null;
			}
			return new trimBottom_trimTop?(trimBottom_trimTop.CreateUnsafe(this.Node));
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x001415B8 File Offset: 0x0013F7B8
		public trimBottom_trimTop Cast_trimBottom_trimTop(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.trimBottom_trimTop)
			{
				return trimBottom_trimTop.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_trimBottom_trimTop is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x0014160D File Offset: 0x0013F80D
		public bool Is_TrimBottomSingleCellRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimBottomSingleCellRows;
		}

		// Token: 0x060062C7 RID: 25287 RVA: 0x00141627 File Offset: 0x0013F827
		public bool Is_TrimBottomSingleCellRows(GrammarBuilders g, out TrimBottomSingleCellRows value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimBottomSingleCellRows)
			{
				value = TrimBottomSingleCellRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimBottomSingleCellRows);
			return false;
		}

		// Token: 0x060062C8 RID: 25288 RVA: 0x0014165C File Offset: 0x0013F85C
		public TrimBottomSingleCellRows? As_TrimBottomSingleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimBottomSingleCellRows)
			{
				return null;
			}
			return new TrimBottomSingleCellRows?(TrimBottomSingleCellRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060062C9 RID: 25289 RVA: 0x0014169C File Offset: 0x0013F89C
		public TrimBottomSingleCellRows Cast_TrimBottomSingleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimBottomSingleCellRows)
			{
				return TrimBottomSingleCellRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimBottomSingleCellRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062CA RID: 25290 RVA: 0x001416F1 File Offset: 0x0013F8F1
		public bool Is_TakeUntilEmptyRow(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TakeUntilEmptyRow;
		}

		// Token: 0x060062CB RID: 25291 RVA: 0x0014170B File Offset: 0x0013F90B
		public bool Is_TakeUntilEmptyRow(GrammarBuilders g, out TakeUntilEmptyRow value)
		{
			if (this.Node.GrammarRule == g.Rule.TakeUntilEmptyRow)
			{
				value = TakeUntilEmptyRow.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TakeUntilEmptyRow);
			return false;
		}

		// Token: 0x060062CC RID: 25292 RVA: 0x00141740 File Offset: 0x0013F940
		public TakeUntilEmptyRow? As_TakeUntilEmptyRow(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TakeUntilEmptyRow)
			{
				return null;
			}
			return new TakeUntilEmptyRow?(TakeUntilEmptyRow.CreateUnsafe(this.Node));
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x00141780 File Offset: 0x0013F980
		public TakeUntilEmptyRow Cast_TakeUntilEmptyRow(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TakeUntilEmptyRow)
			{
				return TakeUntilEmptyRow.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TakeUntilEmptyRow is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x001417D5 File Offset: 0x0013F9D5
		public bool Is_TrimAboveBottomBorder(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimAboveBottomBorder;
		}

		// Token: 0x060062CF RID: 25295 RVA: 0x001417EF File Offset: 0x0013F9EF
		public bool Is_TrimAboveBottomBorder(GrammarBuilders g, out TrimAboveBottomBorder value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimAboveBottomBorder)
			{
				value = TrimAboveBottomBorder.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimAboveBottomBorder);
			return false;
		}

		// Token: 0x060062D0 RID: 25296 RVA: 0x00141824 File Offset: 0x0013FA24
		public TrimAboveBottomBorder? As_TrimAboveBottomBorder(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimAboveBottomBorder)
			{
				return null;
			}
			return new TrimAboveBottomBorder?(TrimAboveBottomBorder.CreateUnsafe(this.Node));
		}

		// Token: 0x060062D1 RID: 25297 RVA: 0x00141864 File Offset: 0x0013FA64
		public TrimAboveBottomBorder Cast_TrimAboveBottomBorder(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimAboveBottomBorder)
			{
				return TrimAboveBottomBorder.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimAboveBottomBorder is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060062D2 RID: 25298 RVA: 0x001418BC File Offset: 0x0013FABC
		public T Switch<T>(GrammarBuilders g, Func<trimBottom_trimTop, T> func0, Func<TrimBottomSingleCellRows, T> func1, Func<TakeUntilEmptyRow, T> func2, Func<TrimAboveBottomBorder, T> func3)
		{
			trimBottom_trimTop trimBottom_trimTop;
			if (this.Is_trimBottom_trimTop(g, out trimBottom_trimTop))
			{
				return func0(trimBottom_trimTop);
			}
			TrimBottomSingleCellRows trimBottomSingleCellRows;
			if (this.Is_TrimBottomSingleCellRows(g, out trimBottomSingleCellRows))
			{
				return func1(trimBottomSingleCellRows);
			}
			TakeUntilEmptyRow takeUntilEmptyRow;
			if (this.Is_TakeUntilEmptyRow(g, out takeUntilEmptyRow))
			{
				return func2(takeUntilEmptyRow);
			}
			TrimAboveBottomBorder trimAboveBottomBorder;
			if (this.Is_TrimAboveBottomBorder(g, out trimAboveBottomBorder))
			{
				return func3(trimAboveBottomBorder);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol trimBottom");
		}

		// Token: 0x060062D3 RID: 25299 RVA: 0x0014193C File Offset: 0x0013FB3C
		public void Switch(GrammarBuilders g, Action<trimBottom_trimTop> func0, Action<TrimBottomSingleCellRows> func1, Action<TakeUntilEmptyRow> func2, Action<TrimAboveBottomBorder> func3)
		{
			trimBottom_trimTop trimBottom_trimTop;
			if (this.Is_trimBottom_trimTop(g, out trimBottom_trimTop))
			{
				func0(trimBottom_trimTop);
				return;
			}
			TrimBottomSingleCellRows trimBottomSingleCellRows;
			if (this.Is_TrimBottomSingleCellRows(g, out trimBottomSingleCellRows))
			{
				func1(trimBottomSingleCellRows);
				return;
			}
			TakeUntilEmptyRow takeUntilEmptyRow;
			if (this.Is_TakeUntilEmptyRow(g, out takeUntilEmptyRow))
			{
				func2(takeUntilEmptyRow);
				return;
			}
			TrimAboveBottomBorder trimAboveBottomBorder;
			if (this.Is_TrimAboveBottomBorder(g, out trimAboveBottomBorder))
			{
				func3(trimAboveBottomBorder);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol trimBottom");
		}

		// Token: 0x060062D4 RID: 25300 RVA: 0x001419BB File Offset: 0x0013FBBB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060062D5 RID: 25301 RVA: 0x001419D0 File Offset: 0x0013FBD0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060062D6 RID: 25302 RVA: 0x001419FA File Offset: 0x0013FBFA
		public bool Equals(trimBottom other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002BFF RID: 11263
		private ProgramNode _node;
	}
}
