using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E5B RID: 3675
	public struct wholeSheet : IProgramNodeBuilder, IEquatable<wholeSheet>
	{
		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06006355 RID: 25429 RVA: 0x001433AE File Offset: 0x001415AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006356 RID: 25430 RVA: 0x001433B6 File Offset: 0x001415B6
		private wholeSheet(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006357 RID: 25431 RVA: 0x001433BF File Offset: 0x001415BF
		public static wholeSheet CreateUnsafe(ProgramNode node)
		{
			return new wholeSheet(node);
		}

		// Token: 0x06006358 RID: 25432 RVA: 0x001433C8 File Offset: 0x001415C8
		public static wholeSheet? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.wholeSheet)
			{
				return null;
			}
			return new wholeSheet?(wholeSheet.CreateUnsafe(node));
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x00143402 File Offset: 0x00141602
		public static wholeSheet CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new wholeSheet(new Hole(g.Symbol.wholeSheet, holeId));
		}

		// Token: 0x0600635A RID: 25434 RVA: 0x0014341A File Offset: 0x0014161A
		public bool Is_wholeSheet_wholeSheetFull(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.wholeSheet_wholeSheetFull;
		}

		// Token: 0x0600635B RID: 25435 RVA: 0x00143434 File Offset: 0x00141634
		public bool Is_wholeSheet_wholeSheetFull(GrammarBuilders g, out wholeSheet_wholeSheetFull value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.wholeSheet_wholeSheetFull)
			{
				value = wholeSheet_wholeSheetFull.CreateUnsafe(this.Node);
				return true;
			}
			value = default(wholeSheet_wholeSheetFull);
			return false;
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x0014346C File Offset: 0x0014166C
		public wholeSheet_wholeSheetFull? As_wholeSheet_wholeSheetFull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.wholeSheet_wholeSheetFull)
			{
				return null;
			}
			return new wholeSheet_wholeSheetFull?(wholeSheet_wholeSheetFull.CreateUnsafe(this.Node));
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x001434AC File Offset: 0x001416AC
		public wholeSheet_wholeSheetFull Cast_wholeSheet_wholeSheetFull(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.wholeSheet_wholeSheetFull)
			{
				return wholeSheet_wholeSheetFull.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_wholeSheet_wholeSheetFull is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x00143501 File Offset: 0x00141701
		public bool Is_TrimHiddenWholeSheet(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TrimHiddenWholeSheet;
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x0014351B File Offset: 0x0014171B
		public bool Is_TrimHiddenWholeSheet(GrammarBuilders g, out TrimHiddenWholeSheet value)
		{
			if (this.Node.GrammarRule == g.Rule.TrimHiddenWholeSheet)
			{
				value = TrimHiddenWholeSheet.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TrimHiddenWholeSheet);
			return false;
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x00143550 File Offset: 0x00141750
		public TrimHiddenWholeSheet? As_TrimHiddenWholeSheet(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TrimHiddenWholeSheet)
			{
				return null;
			}
			return new TrimHiddenWholeSheet?(TrimHiddenWholeSheet.CreateUnsafe(this.Node));
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x00143590 File Offset: 0x00141790
		public TrimHiddenWholeSheet Cast_TrimHiddenWholeSheet(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TrimHiddenWholeSheet)
			{
				return TrimHiddenWholeSheet.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TrimHiddenWholeSheet is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x001435E8 File Offset: 0x001417E8
		public T Switch<T>(GrammarBuilders g, Func<wholeSheet_wholeSheetFull, T> func0, Func<TrimHiddenWholeSheet, T> func1)
		{
			wholeSheet_wholeSheetFull wholeSheet_wholeSheetFull;
			if (this.Is_wholeSheet_wholeSheetFull(g, out wholeSheet_wholeSheetFull))
			{
				return func0(wholeSheet_wholeSheetFull);
			}
			TrimHiddenWholeSheet trimHiddenWholeSheet;
			if (this.Is_TrimHiddenWholeSheet(g, out trimHiddenWholeSheet))
			{
				return func1(trimHiddenWholeSheet);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol wholeSheet");
		}

		// Token: 0x06006363 RID: 25443 RVA: 0x00143640 File Offset: 0x00141840
		public void Switch(GrammarBuilders g, Action<wholeSheet_wholeSheetFull> func0, Action<TrimHiddenWholeSheet> func1)
		{
			wholeSheet_wholeSheetFull wholeSheet_wholeSheetFull;
			if (this.Is_wholeSheet_wholeSheetFull(g, out wholeSheet_wholeSheetFull))
			{
				func0(wholeSheet_wholeSheetFull);
				return;
			}
			TrimHiddenWholeSheet trimHiddenWholeSheet;
			if (this.Is_TrimHiddenWholeSheet(g, out trimHiddenWholeSheet))
			{
				func1(trimHiddenWholeSheet);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol wholeSheet");
		}

		// Token: 0x06006364 RID: 25444 RVA: 0x00143697 File Offset: 0x00141897
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006365 RID: 25445 RVA: 0x001436AC File Offset: 0x001418AC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006366 RID: 25446 RVA: 0x001436D6 File Offset: 0x001418D6
		public bool Equals(wholeSheet other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C05 RID: 11269
		private ProgramNode _node;
	}
}
