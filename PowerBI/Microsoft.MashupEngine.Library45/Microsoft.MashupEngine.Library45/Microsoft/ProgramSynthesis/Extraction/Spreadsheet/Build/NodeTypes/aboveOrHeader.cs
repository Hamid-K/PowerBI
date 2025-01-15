using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E69 RID: 3689
	public struct aboveOrHeader : IProgramNodeBuilder, IEquatable<aboveOrHeader>
	{
		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x0600645D RID: 25693 RVA: 0x00145FCA File Offset: 0x001441CA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600645E RID: 25694 RVA: 0x00145FD2 File Offset: 0x001441D2
		private aboveOrHeader(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600645F RID: 25695 RVA: 0x00145FDB File Offset: 0x001441DB
		public static aboveOrHeader CreateUnsafe(ProgramNode node)
		{
			return new aboveOrHeader(node);
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x00145FE4 File Offset: 0x001441E4
		public static aboveOrHeader? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.aboveOrHeader)
			{
				return null;
			}
			return new aboveOrHeader?(aboveOrHeader.CreateUnsafe(node));
		}

		// Token: 0x06006461 RID: 25697 RVA: 0x0014601E File Offset: 0x0014421E
		public static aboveOrHeader CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new aboveOrHeader(new Hole(g.Symbol.aboveOrHeader, holeId));
		}

		// Token: 0x06006462 RID: 25698 RVA: 0x00146036 File Offset: 0x00144236
		public bool Is_aboveOrHeader_above(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.aboveOrHeader_above;
		}

		// Token: 0x06006463 RID: 25699 RVA: 0x00146050 File Offset: 0x00144250
		public bool Is_aboveOrHeader_above(GrammarBuilders g, out aboveOrHeader_above value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.aboveOrHeader_above)
			{
				value = aboveOrHeader_above.CreateUnsafe(this.Node);
				return true;
			}
			value = default(aboveOrHeader_above);
			return false;
		}

		// Token: 0x06006464 RID: 25700 RVA: 0x00146088 File Offset: 0x00144288
		public aboveOrHeader_above? As_aboveOrHeader_above(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.aboveOrHeader_above)
			{
				return null;
			}
			return new aboveOrHeader_above?(aboveOrHeader_above.CreateUnsafe(this.Node));
		}

		// Token: 0x06006465 RID: 25701 RVA: 0x001460C8 File Offset: 0x001442C8
		public aboveOrHeader_above Cast_aboveOrHeader_above(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.aboveOrHeader_above)
			{
				return aboveOrHeader_above.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_aboveOrHeader_above is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006466 RID: 25702 RVA: 0x0014611D File Offset: 0x0014431D
		public bool Is_aboveOrHeader_headerSection(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.aboveOrHeader_headerSection;
		}

		// Token: 0x06006467 RID: 25703 RVA: 0x00146137 File Offset: 0x00144337
		public bool Is_aboveOrHeader_headerSection(GrammarBuilders g, out aboveOrHeader_headerSection value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.aboveOrHeader_headerSection)
			{
				value = aboveOrHeader_headerSection.CreateUnsafe(this.Node);
				return true;
			}
			value = default(aboveOrHeader_headerSection);
			return false;
		}

		// Token: 0x06006468 RID: 25704 RVA: 0x0014616C File Offset: 0x0014436C
		public aboveOrHeader_headerSection? As_aboveOrHeader_headerSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.aboveOrHeader_headerSection)
			{
				return null;
			}
			return new aboveOrHeader_headerSection?(aboveOrHeader_headerSection.CreateUnsafe(this.Node));
		}

		// Token: 0x06006469 RID: 25705 RVA: 0x001461AC File Offset: 0x001443AC
		public aboveOrHeader_headerSection Cast_aboveOrHeader_headerSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.aboveOrHeader_headerSection)
			{
				return aboveOrHeader_headerSection.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_aboveOrHeader_headerSection is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600646A RID: 25706 RVA: 0x00146204 File Offset: 0x00144404
		public T Switch<T>(GrammarBuilders g, Func<aboveOrHeader_above, T> func0, Func<aboveOrHeader_headerSection, T> func1)
		{
			aboveOrHeader_above aboveOrHeader_above;
			if (this.Is_aboveOrHeader_above(g, out aboveOrHeader_above))
			{
				return func0(aboveOrHeader_above);
			}
			aboveOrHeader_headerSection aboveOrHeader_headerSection;
			if (this.Is_aboveOrHeader_headerSection(g, out aboveOrHeader_headerSection))
			{
				return func1(aboveOrHeader_headerSection);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol aboveOrHeader");
		}

		// Token: 0x0600646B RID: 25707 RVA: 0x0014625C File Offset: 0x0014445C
		public void Switch(GrammarBuilders g, Action<aboveOrHeader_above> func0, Action<aboveOrHeader_headerSection> func1)
		{
			aboveOrHeader_above aboveOrHeader_above;
			if (this.Is_aboveOrHeader_above(g, out aboveOrHeader_above))
			{
				func0(aboveOrHeader_above);
				return;
			}
			aboveOrHeader_headerSection aboveOrHeader_headerSection;
			if (this.Is_aboveOrHeader_headerSection(g, out aboveOrHeader_headerSection))
			{
				func1(aboveOrHeader_headerSection);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol aboveOrHeader");
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x001462B3 File Offset: 0x001444B3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600646D RID: 25709 RVA: 0x001462C8 File Offset: 0x001444C8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x001462F2 File Offset: 0x001444F2
		public bool Equals(aboveOrHeader other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C13 RID: 11283
		private ProgramNode _node;
	}
}
