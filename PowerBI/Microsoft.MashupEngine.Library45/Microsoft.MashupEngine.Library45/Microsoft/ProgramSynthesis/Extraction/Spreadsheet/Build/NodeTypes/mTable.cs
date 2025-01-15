using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E62 RID: 3682
	public struct mTable : IProgramNodeBuilder, IEquatable<mTable>
	{
		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x060063BF RID: 25535 RVA: 0x0014422E File Offset: 0x0014242E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x060063C0 RID: 25536 RVA: 0x00144236 File Offset: 0x00142436
		private mTable(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x060063C1 RID: 25537 RVA: 0x0014423F File Offset: 0x0014243F
		public static mTable CreateUnsafe(ProgramNode node)
		{
			return new mTable(node);
		}

		// Token: 0x060063C2 RID: 25538 RVA: 0x00144248 File Offset: 0x00142448
		public static mTable? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.mTable)
			{
				return null;
			}
			return new mTable?(mTable.CreateUnsafe(node));
		}

		// Token: 0x060063C3 RID: 25539 RVA: 0x00144282 File Offset: 0x00142482
		public static mTable CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new mTable(new Hole(g.Symbol.mTable, holeId));
		}

		// Token: 0x060063C4 RID: 25540 RVA: 0x0014429A File Offset: 0x0014249A
		public bool Is_MWholeSheet(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MWholeSheet;
		}

		// Token: 0x060063C5 RID: 25541 RVA: 0x001442B4 File Offset: 0x001424B4
		public bool Is_MWholeSheet(GrammarBuilders g, out MWholeSheet value)
		{
			if (this.Node.GrammarRule == g.Rule.MWholeSheet)
			{
				value = MWholeSheet.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MWholeSheet);
			return false;
		}

		// Token: 0x060063C6 RID: 25542 RVA: 0x001442EC File Offset: 0x001424EC
		public MWholeSheet? As_MWholeSheet(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MWholeSheet)
			{
				return null;
			}
			return new MWholeSheet?(MWholeSheet.CreateUnsafe(this.Node));
		}

		// Token: 0x060063C7 RID: 25543 RVA: 0x0014432C File Offset: 0x0014252C
		public MWholeSheet Cast_MWholeSheet(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MWholeSheet)
			{
				return MWholeSheet.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MWholeSheet is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063C8 RID: 25544 RVA: 0x00144381 File Offset: 0x00142581
		public bool Is_KthMSection(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KthMSection;
		}

		// Token: 0x060063C9 RID: 25545 RVA: 0x0014439B File Offset: 0x0014259B
		public bool Is_KthMSection(GrammarBuilders g, out KthMSection value)
		{
			if (this.Node.GrammarRule == g.Rule.KthMSection)
			{
				value = KthMSection.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KthMSection);
			return false;
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x001443D0 File Offset: 0x001425D0
		public KthMSection? As_KthMSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KthMSection)
			{
				return null;
			}
			return new KthMSection?(KthMSection.CreateUnsafe(this.Node));
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x00144410 File Offset: 0x00142610
		public KthMSection Cast_KthMSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KthMSection)
			{
				return KthMSection.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KthMSection is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x00144465 File Offset: 0x00142665
		public bool Is_KthAndNextMSection(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KthAndNextMSection;
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x0014447F File Offset: 0x0014267F
		public bool Is_KthAndNextMSection(GrammarBuilders g, out KthAndNextMSection value)
		{
			if (this.Node.GrammarRule == g.Rule.KthAndNextMSection)
			{
				value = KthAndNextMSection.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KthAndNextMSection);
			return false;
		}

		// Token: 0x060063CE RID: 25550 RVA: 0x001444B4 File Offset: 0x001426B4
		public KthAndNextMSection? As_KthAndNextMSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KthAndNextMSection)
			{
				return null;
			}
			return new KthAndNextMSection?(KthAndNextMSection.CreateUnsafe(this.Node));
		}

		// Token: 0x060063CF RID: 25551 RVA: 0x001444F4 File Offset: 0x001426F4
		public KthAndNextMSection Cast_KthAndNextMSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KthAndNextMSection)
			{
				return KthAndNextMSection.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KthAndNextMSection is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063D0 RID: 25552 RVA: 0x00144549 File Offset: 0x00142749
		public bool Is_MTrimTopSingleCellRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MTrimTopSingleCellRows;
		}

		// Token: 0x060063D1 RID: 25553 RVA: 0x00144563 File Offset: 0x00142763
		public bool Is_MTrimTopSingleCellRows(GrammarBuilders g, out MTrimTopSingleCellRows value)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimTopSingleCellRows)
			{
				value = MTrimTopSingleCellRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MTrimTopSingleCellRows);
			return false;
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x00144598 File Offset: 0x00142798
		public MTrimTopSingleCellRows? As_MTrimTopSingleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MTrimTopSingleCellRows)
			{
				return null;
			}
			return new MTrimTopSingleCellRows?(MTrimTopSingleCellRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x001445D8 File Offset: 0x001427D8
		public MTrimTopSingleCellRows Cast_MTrimTopSingleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimTopSingleCellRows)
			{
				return MTrimTopSingleCellRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MTrimTopSingleCellRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x0014462D File Offset: 0x0014282D
		public bool Is_MTrimTopSingleLeftCellRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MTrimTopSingleLeftCellRows;
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x00144647 File Offset: 0x00142847
		public bool Is_MTrimTopSingleLeftCellRows(GrammarBuilders g, out MTrimTopSingleLeftCellRows value)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimTopSingleLeftCellRows)
			{
				value = MTrimTopSingleLeftCellRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MTrimTopSingleLeftCellRows);
			return false;
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x0014467C File Offset: 0x0014287C
		public MTrimTopSingleLeftCellRows? As_MTrimTopSingleLeftCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MTrimTopSingleLeftCellRows)
			{
				return null;
			}
			return new MTrimTopSingleLeftCellRows?(MTrimTopSingleLeftCellRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x001446BC File Offset: 0x001428BC
		public MTrimTopSingleLeftCellRows Cast_MTrimTopSingleLeftCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimTopSingleLeftCellRows)
			{
				return MTrimTopSingleLeftCellRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MTrimTopSingleLeftCellRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063D8 RID: 25560 RVA: 0x00144711 File Offset: 0x00142911
		public bool Is_MTrimBottomSingleCellRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MTrimBottomSingleCellRows;
		}

		// Token: 0x060063D9 RID: 25561 RVA: 0x0014472B File Offset: 0x0014292B
		public bool Is_MTrimBottomSingleCellRows(GrammarBuilders g, out MTrimBottomSingleCellRows value)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimBottomSingleCellRows)
			{
				value = MTrimBottomSingleCellRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MTrimBottomSingleCellRows);
			return false;
		}

		// Token: 0x060063DA RID: 25562 RVA: 0x00144760 File Offset: 0x00142960
		public MTrimBottomSingleCellRows? As_MTrimBottomSingleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MTrimBottomSingleCellRows)
			{
				return null;
			}
			return new MTrimBottomSingleCellRows?(MTrimBottomSingleCellRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060063DB RID: 25563 RVA: 0x001447A0 File Offset: 0x001429A0
		public MTrimBottomSingleCellRows Cast_MTrimBottomSingleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimBottomSingleCellRows)
			{
				return MTrimBottomSingleCellRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MTrimBottomSingleCellRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063DC RID: 25564 RVA: 0x001447F5 File Offset: 0x001429F5
		public bool Is_MTrimLeftSingleCellColumns(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MTrimLeftSingleCellColumns;
		}

		// Token: 0x060063DD RID: 25565 RVA: 0x0014480F File Offset: 0x00142A0F
		public bool Is_MTrimLeftSingleCellColumns(GrammarBuilders g, out MTrimLeftSingleCellColumns value)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimLeftSingleCellColumns)
			{
				value = MTrimLeftSingleCellColumns.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MTrimLeftSingleCellColumns);
			return false;
		}

		// Token: 0x060063DE RID: 25566 RVA: 0x00144844 File Offset: 0x00142A44
		public MTrimLeftSingleCellColumns? As_MTrimLeftSingleCellColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MTrimLeftSingleCellColumns)
			{
				return null;
			}
			return new MTrimLeftSingleCellColumns?(MTrimLeftSingleCellColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x060063DF RID: 25567 RVA: 0x00144884 File Offset: 0x00142A84
		public MTrimLeftSingleCellColumns Cast_MTrimLeftSingleCellColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimLeftSingleCellColumns)
			{
				return MTrimLeftSingleCellColumns.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MTrimLeftSingleCellColumns is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063E0 RID: 25568 RVA: 0x001448D9 File Offset: 0x00142AD9
		public bool Is_MTrimRightSingleCellColumns(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MTrimRightSingleCellColumns;
		}

		// Token: 0x060063E1 RID: 25569 RVA: 0x001448F3 File Offset: 0x00142AF3
		public bool Is_MTrimRightSingleCellColumns(GrammarBuilders g, out MTrimRightSingleCellColumns value)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimRightSingleCellColumns)
			{
				value = MTrimRightSingleCellColumns.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MTrimRightSingleCellColumns);
			return false;
		}

		// Token: 0x060063E2 RID: 25570 RVA: 0x00144928 File Offset: 0x00142B28
		public MTrimRightSingleCellColumns? As_MTrimRightSingleCellColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MTrimRightSingleCellColumns)
			{
				return null;
			}
			return new MTrimRightSingleCellColumns?(MTrimRightSingleCellColumns.CreateUnsafe(this.Node));
		}

		// Token: 0x060063E3 RID: 25571 RVA: 0x00144968 File Offset: 0x00142B68
		public MTrimRightSingleCellColumns Cast_MTrimRightSingleCellColumns(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimRightSingleCellColumns)
			{
				return MTrimRightSingleCellColumns.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MTrimRightSingleCellColumns is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063E4 RID: 25572 RVA: 0x001449BD File Offset: 0x00142BBD
		public bool Is_MTrimTopDoubleCellRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MTrimTopDoubleCellRows;
		}

		// Token: 0x060063E5 RID: 25573 RVA: 0x001449D7 File Offset: 0x00142BD7
		public bool Is_MTrimTopDoubleCellRows(GrammarBuilders g, out MTrimTopDoubleCellRows value)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimTopDoubleCellRows)
			{
				value = MTrimTopDoubleCellRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MTrimTopDoubleCellRows);
			return false;
		}

		// Token: 0x060063E6 RID: 25574 RVA: 0x00144A0C File Offset: 0x00142C0C
		public MTrimTopDoubleCellRows? As_MTrimTopDoubleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MTrimTopDoubleCellRows)
			{
				return null;
			}
			return new MTrimTopDoubleCellRows?(MTrimTopDoubleCellRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060063E7 RID: 25575 RVA: 0x00144A4C File Offset: 0x00142C4C
		public MTrimTopDoubleCellRows Cast_MTrimTopDoubleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimTopDoubleCellRows)
			{
				return MTrimTopDoubleCellRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MTrimTopDoubleCellRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063E8 RID: 25576 RVA: 0x00144AA1 File Offset: 0x00142CA1
		public bool Is_MTrimBottomDoubleCellRows(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.MTrimBottomDoubleCellRows;
		}

		// Token: 0x060063E9 RID: 25577 RVA: 0x00144ABB File Offset: 0x00142CBB
		public bool Is_MTrimBottomDoubleCellRows(GrammarBuilders g, out MTrimBottomDoubleCellRows value)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimBottomDoubleCellRows)
			{
				value = MTrimBottomDoubleCellRows.CreateUnsafe(this.Node);
				return true;
			}
			value = default(MTrimBottomDoubleCellRows);
			return false;
		}

		// Token: 0x060063EA RID: 25578 RVA: 0x00144AF0 File Offset: 0x00142CF0
		public MTrimBottomDoubleCellRows? As_MTrimBottomDoubleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.MTrimBottomDoubleCellRows)
			{
				return null;
			}
			return new MTrimBottomDoubleCellRows?(MTrimBottomDoubleCellRows.CreateUnsafe(this.Node));
		}

		// Token: 0x060063EB RID: 25579 RVA: 0x00144B30 File Offset: 0x00142D30
		public MTrimBottomDoubleCellRows Cast_MTrimBottomDoubleCellRows(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.MTrimBottomDoubleCellRows)
			{
				return MTrimBottomDoubleCellRows.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_MTrimBottomDoubleCellRows is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x060063EC RID: 25580 RVA: 0x00144B88 File Offset: 0x00142D88
		public T Switch<T>(GrammarBuilders g, Func<MWholeSheet, T> func0, Func<KthMSection, T> func1, Func<KthAndNextMSection, T> func2, Func<MTrimTopSingleCellRows, T> func3, Func<MTrimTopSingleLeftCellRows, T> func4, Func<MTrimBottomSingleCellRows, T> func5, Func<MTrimLeftSingleCellColumns, T> func6, Func<MTrimRightSingleCellColumns, T> func7, Func<MTrimTopDoubleCellRows, T> func8, Func<MTrimBottomDoubleCellRows, T> func9)
		{
			MWholeSheet mwholeSheet;
			if (this.Is_MWholeSheet(g, out mwholeSheet))
			{
				return func0(mwholeSheet);
			}
			KthMSection kthMSection;
			if (this.Is_KthMSection(g, out kthMSection))
			{
				return func1(kthMSection);
			}
			KthAndNextMSection kthAndNextMSection;
			if (this.Is_KthAndNextMSection(g, out kthAndNextMSection))
			{
				return func2(kthAndNextMSection);
			}
			MTrimTopSingleCellRows mtrimTopSingleCellRows;
			if (this.Is_MTrimTopSingleCellRows(g, out mtrimTopSingleCellRows))
			{
				return func3(mtrimTopSingleCellRows);
			}
			MTrimTopSingleLeftCellRows mtrimTopSingleLeftCellRows;
			if (this.Is_MTrimTopSingleLeftCellRows(g, out mtrimTopSingleLeftCellRows))
			{
				return func4(mtrimTopSingleLeftCellRows);
			}
			MTrimBottomSingleCellRows mtrimBottomSingleCellRows;
			if (this.Is_MTrimBottomSingleCellRows(g, out mtrimBottomSingleCellRows))
			{
				return func5(mtrimBottomSingleCellRows);
			}
			MTrimLeftSingleCellColumns mtrimLeftSingleCellColumns;
			if (this.Is_MTrimLeftSingleCellColumns(g, out mtrimLeftSingleCellColumns))
			{
				return func6(mtrimLeftSingleCellColumns);
			}
			MTrimRightSingleCellColumns mtrimRightSingleCellColumns;
			if (this.Is_MTrimRightSingleCellColumns(g, out mtrimRightSingleCellColumns))
			{
				return func7(mtrimRightSingleCellColumns);
			}
			MTrimTopDoubleCellRows mtrimTopDoubleCellRows;
			if (this.Is_MTrimTopDoubleCellRows(g, out mtrimTopDoubleCellRows))
			{
				return func8(mtrimTopDoubleCellRows);
			}
			MTrimBottomDoubleCellRows mtrimBottomDoubleCellRows;
			if (this.Is_MTrimBottomDoubleCellRows(g, out mtrimBottomDoubleCellRows))
			{
				return func9(mtrimBottomDoubleCellRows);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol mTable");
		}

		// Token: 0x060063ED RID: 25581 RVA: 0x00144C88 File Offset: 0x00142E88
		public void Switch(GrammarBuilders g, Action<MWholeSheet> func0, Action<KthMSection> func1, Action<KthAndNextMSection> func2, Action<MTrimTopSingleCellRows> func3, Action<MTrimTopSingleLeftCellRows> func4, Action<MTrimBottomSingleCellRows> func5, Action<MTrimLeftSingleCellColumns> func6, Action<MTrimRightSingleCellColumns> func7, Action<MTrimTopDoubleCellRows> func8, Action<MTrimBottomDoubleCellRows> func9)
		{
			MWholeSheet mwholeSheet;
			if (this.Is_MWholeSheet(g, out mwholeSheet))
			{
				func0(mwholeSheet);
				return;
			}
			KthMSection kthMSection;
			if (this.Is_KthMSection(g, out kthMSection))
			{
				func1(kthMSection);
				return;
			}
			KthAndNextMSection kthAndNextMSection;
			if (this.Is_KthAndNextMSection(g, out kthAndNextMSection))
			{
				func2(kthAndNextMSection);
				return;
			}
			MTrimTopSingleCellRows mtrimTopSingleCellRows;
			if (this.Is_MTrimTopSingleCellRows(g, out mtrimTopSingleCellRows))
			{
				func3(mtrimTopSingleCellRows);
				return;
			}
			MTrimTopSingleLeftCellRows mtrimTopSingleLeftCellRows;
			if (this.Is_MTrimTopSingleLeftCellRows(g, out mtrimTopSingleLeftCellRows))
			{
				func4(mtrimTopSingleLeftCellRows);
				return;
			}
			MTrimBottomSingleCellRows mtrimBottomSingleCellRows;
			if (this.Is_MTrimBottomSingleCellRows(g, out mtrimBottomSingleCellRows))
			{
				func5(mtrimBottomSingleCellRows);
				return;
			}
			MTrimLeftSingleCellColumns mtrimLeftSingleCellColumns;
			if (this.Is_MTrimLeftSingleCellColumns(g, out mtrimLeftSingleCellColumns))
			{
				func6(mtrimLeftSingleCellColumns);
				return;
			}
			MTrimRightSingleCellColumns mtrimRightSingleCellColumns;
			if (this.Is_MTrimRightSingleCellColumns(g, out mtrimRightSingleCellColumns))
			{
				func7(mtrimRightSingleCellColumns);
				return;
			}
			MTrimTopDoubleCellRows mtrimTopDoubleCellRows;
			if (this.Is_MTrimTopDoubleCellRows(g, out mtrimTopDoubleCellRows))
			{
				func8(mtrimTopDoubleCellRows);
				return;
			}
			MTrimBottomDoubleCellRows mtrimBottomDoubleCellRows;
			if (this.Is_MTrimBottomDoubleCellRows(g, out mtrimBottomDoubleCellRows))
			{
				func9(mtrimBottomDoubleCellRows);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol mTable");
		}

		// Token: 0x060063EE RID: 25582 RVA: 0x00144D85 File Offset: 0x00142F85
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x060063EF RID: 25583 RVA: 0x00144D98 File Offset: 0x00142F98
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x060063F0 RID: 25584 RVA: 0x00144DC2 File Offset: 0x00142FC2
		public bool Equals(mTable other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C0C RID: 11276
		private ProgramNode _node;
	}
}
