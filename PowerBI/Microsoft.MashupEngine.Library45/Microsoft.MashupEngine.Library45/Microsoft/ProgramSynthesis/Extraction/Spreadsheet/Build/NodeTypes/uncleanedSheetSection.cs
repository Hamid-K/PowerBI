using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E5A RID: 3674
	public struct uncleanedSheetSection : IProgramNodeBuilder, IEquatable<uncleanedSheetSection>
	{
		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x0600633F RID: 25407 RVA: 0x00142F66 File Offset: 0x00141166
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x00142F6E File Offset: 0x0014116E
		private uncleanedSheetSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006341 RID: 25409 RVA: 0x00142F77 File Offset: 0x00141177
		public static uncleanedSheetSection CreateUnsafe(ProgramNode node)
		{
			return new uncleanedSheetSection(node);
		}

		// Token: 0x06006342 RID: 25410 RVA: 0x00142F80 File Offset: 0x00141180
		public static uncleanedSheetSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.uncleanedSheetSection)
			{
				return null;
			}
			return new uncleanedSheetSection?(uncleanedSheetSection.CreateUnsafe(node));
		}

		// Token: 0x06006343 RID: 25411 RVA: 0x00142FBA File Offset: 0x001411BA
		public static uncleanedSheetSection CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new uncleanedSheetSection(new Hole(g.Symbol.uncleanedSheetSection, holeId));
		}

		// Token: 0x06006344 RID: 25412 RVA: 0x00142FD2 File Offset: 0x001411D2
		public bool Is_uncleanedSheetSection_wholeSheet(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.uncleanedSheetSection_wholeSheet;
		}

		// Token: 0x06006345 RID: 25413 RVA: 0x00142FEC File Offset: 0x001411EC
		public bool Is_uncleanedSheetSection_wholeSheet(GrammarBuilders g, out uncleanedSheetSection_wholeSheet value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.uncleanedSheetSection_wholeSheet)
			{
				value = uncleanedSheetSection_wholeSheet.CreateUnsafe(this.Node);
				return true;
			}
			value = default(uncleanedSheetSection_wholeSheet);
			return false;
		}

		// Token: 0x06006346 RID: 25414 RVA: 0x00143024 File Offset: 0x00141224
		public uncleanedSheetSection_wholeSheet? As_uncleanedSheetSection_wholeSheet(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.uncleanedSheetSection_wholeSheet)
			{
				return null;
			}
			return new uncleanedSheetSection_wholeSheet?(uncleanedSheetSection_wholeSheet.CreateUnsafe(this.Node));
		}

		// Token: 0x06006347 RID: 25415 RVA: 0x00143064 File Offset: 0x00141264
		public uncleanedSheetSection_wholeSheet Cast_uncleanedSheetSection_wholeSheet(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.uncleanedSheetSection_wholeSheet)
			{
				return uncleanedSheetSection_wholeSheet.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_uncleanedSheetSection_wholeSheet is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006348 RID: 25416 RVA: 0x001430B9 File Offset: 0x001412B9
		public bool Is_KthSplit(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KthSplit;
		}

		// Token: 0x06006349 RID: 25417 RVA: 0x001430D3 File Offset: 0x001412D3
		public bool Is_KthSplit(GrammarBuilders g, out KthSplit value)
		{
			if (this.Node.GrammarRule == g.Rule.KthSplit)
			{
				value = KthSplit.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KthSplit);
			return false;
		}

		// Token: 0x0600634A RID: 25418 RVA: 0x00143108 File Offset: 0x00141308
		public KthSplit? As_KthSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KthSplit)
			{
				return null;
			}
			return new KthSplit?(KthSplit.CreateUnsafe(this.Node));
		}

		// Token: 0x0600634B RID: 25419 RVA: 0x00143148 File Offset: 0x00141348
		public KthSplit Cast_KthSplit(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KthSplit)
			{
				return KthSplit.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KthSplit is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600634C RID: 25420 RVA: 0x0014319D File Offset: 0x0014139D
		public bool Is_Area(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Area;
		}

		// Token: 0x0600634D RID: 25421 RVA: 0x001431B7 File Offset: 0x001413B7
		public bool Is_Area(GrammarBuilders g, out Area value)
		{
			if (this.Node.GrammarRule == g.Rule.Area)
			{
				value = Area.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Area);
			return false;
		}

		// Token: 0x0600634E RID: 25422 RVA: 0x001431EC File Offset: 0x001413EC
		public Area? As_Area(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Area)
			{
				return null;
			}
			return new Area?(Area.CreateUnsafe(this.Node));
		}

		// Token: 0x0600634F RID: 25423 RVA: 0x0014322C File Offset: 0x0014142C
		public Area Cast_Area(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Area)
			{
				return Area.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Area is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006350 RID: 25424 RVA: 0x00143284 File Offset: 0x00141484
		public T Switch<T>(GrammarBuilders g, Func<uncleanedSheetSection_wholeSheet, T> func0, Func<KthSplit, T> func1, Func<Area, T> func2)
		{
			uncleanedSheetSection_wholeSheet uncleanedSheetSection_wholeSheet;
			if (this.Is_uncleanedSheetSection_wholeSheet(g, out uncleanedSheetSection_wholeSheet))
			{
				return func0(uncleanedSheetSection_wholeSheet);
			}
			KthSplit kthSplit;
			if (this.Is_KthSplit(g, out kthSplit))
			{
				return func1(kthSplit);
			}
			Area area;
			if (this.Is_Area(g, out area))
			{
				return func2(area);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol uncleanedSheetSection");
		}

		// Token: 0x06006351 RID: 25425 RVA: 0x001432F0 File Offset: 0x001414F0
		public void Switch(GrammarBuilders g, Action<uncleanedSheetSection_wholeSheet> func0, Action<KthSplit> func1, Action<Area> func2)
		{
			uncleanedSheetSection_wholeSheet uncleanedSheetSection_wholeSheet;
			if (this.Is_uncleanedSheetSection_wholeSheet(g, out uncleanedSheetSection_wholeSheet))
			{
				func0(uncleanedSheetSection_wholeSheet);
				return;
			}
			KthSplit kthSplit;
			if (this.Is_KthSplit(g, out kthSplit))
			{
				func1(kthSplit);
				return;
			}
			Area area;
			if (this.Is_Area(g, out area))
			{
				func2(area);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol uncleanedSheetSection");
		}

		// Token: 0x06006352 RID: 25426 RVA: 0x0014335B File Offset: 0x0014155B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006353 RID: 25427 RVA: 0x00143370 File Offset: 0x00141570
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006354 RID: 25428 RVA: 0x0014339A File Offset: 0x0014159A
		public bool Equals(uncleanedSheetSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C04 RID: 11268
		private ProgramNode _node;
	}
}
