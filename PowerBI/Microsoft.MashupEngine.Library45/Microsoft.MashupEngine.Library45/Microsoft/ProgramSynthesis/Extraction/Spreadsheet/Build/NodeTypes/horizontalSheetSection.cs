using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E58 RID: 3672
	public struct horizontalSheetSection : IProgramNodeBuilder, IEquatable<horizontalSheetSection>
	{
		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x0600631B RID: 25371 RVA: 0x001428EE File Offset: 0x00140AEE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600631C RID: 25372 RVA: 0x001428F6 File Offset: 0x00140AF6
		private horizontalSheetSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600631D RID: 25373 RVA: 0x001428FF File Offset: 0x00140AFF
		public static horizontalSheetSection CreateUnsafe(ProgramNode node)
		{
			return new horizontalSheetSection(node);
		}

		// Token: 0x0600631E RID: 25374 RVA: 0x00142908 File Offset: 0x00140B08
		public static horizontalSheetSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.horizontalSheetSection)
			{
				return null;
			}
			return new horizontalSheetSection?(horizontalSheetSection.CreateUnsafe(node));
		}

		// Token: 0x0600631F RID: 25375 RVA: 0x00142942 File Offset: 0x00140B42
		public static horizontalSheetSection CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new horizontalSheetSection(new Hole(g.Symbol.horizontalSheetSection, holeId));
		}

		// Token: 0x06006320 RID: 25376 RVA: 0x0014295A File Offset: 0x00140B5A
		public bool Is_horizontalSheetSection_verticalSheetSection(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.horizontalSheetSection_verticalSheetSection;
		}

		// Token: 0x06006321 RID: 25377 RVA: 0x00142974 File Offset: 0x00140B74
		public bool Is_horizontalSheetSection_verticalSheetSection(GrammarBuilders g, out horizontalSheetSection_verticalSheetSection value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.horizontalSheetSection_verticalSheetSection)
			{
				value = horizontalSheetSection_verticalSheetSection.CreateUnsafe(this.Node);
				return true;
			}
			value = default(horizontalSheetSection_verticalSheetSection);
			return false;
		}

		// Token: 0x06006322 RID: 25378 RVA: 0x001429AC File Offset: 0x00140BAC
		public horizontalSheetSection_verticalSheetSection? As_horizontalSheetSection_verticalSheetSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.horizontalSheetSection_verticalSheetSection)
			{
				return null;
			}
			return new horizontalSheetSection_verticalSheetSection?(horizontalSheetSection_verticalSheetSection.CreateUnsafe(this.Node));
		}

		// Token: 0x06006323 RID: 25379 RVA: 0x001429EC File Offset: 0x00140BEC
		public horizontalSheetSection_verticalSheetSection Cast_horizontalSheetSection_verticalSheetSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.horizontalSheetSection_verticalSheetSection)
			{
				return horizontalSheetSection_verticalSheetSection.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_horizontalSheetSection_verticalSheetSection is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006324 RID: 25380 RVA: 0x00142A41 File Offset: 0x00140C41
		public bool Is_KthHorizontal(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KthHorizontal;
		}

		// Token: 0x06006325 RID: 25381 RVA: 0x00142A5B File Offset: 0x00140C5B
		public bool Is_KthHorizontal(GrammarBuilders g, out KthHorizontal value)
		{
			if (this.Node.GrammarRule == g.Rule.KthHorizontal)
			{
				value = KthHorizontal.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KthHorizontal);
			return false;
		}

		// Token: 0x06006326 RID: 25382 RVA: 0x00142A90 File Offset: 0x00140C90
		public KthHorizontal? As_KthHorizontal(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KthHorizontal)
			{
				return null;
			}
			return new KthHorizontal?(KthHorizontal.CreateUnsafe(this.Node));
		}

		// Token: 0x06006327 RID: 25383 RVA: 0x00142AD0 File Offset: 0x00140CD0
		public KthHorizontal Cast_KthHorizontal(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KthHorizontal)
			{
				return KthHorizontal.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KthHorizontal is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006328 RID: 25384 RVA: 0x00142B28 File Offset: 0x00140D28
		public T Switch<T>(GrammarBuilders g, Func<horizontalSheetSection_verticalSheetSection, T> func0, Func<KthHorizontal, T> func1)
		{
			horizontalSheetSection_verticalSheetSection horizontalSheetSection_verticalSheetSection;
			if (this.Is_horizontalSheetSection_verticalSheetSection(g, out horizontalSheetSection_verticalSheetSection))
			{
				return func0(horizontalSheetSection_verticalSheetSection);
			}
			KthHorizontal kthHorizontal;
			if (this.Is_KthHorizontal(g, out kthHorizontal))
			{
				return func1(kthHorizontal);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol horizontalSheetSection");
		}

		// Token: 0x06006329 RID: 25385 RVA: 0x00142B80 File Offset: 0x00140D80
		public void Switch(GrammarBuilders g, Action<horizontalSheetSection_verticalSheetSection> func0, Action<KthHorizontal> func1)
		{
			horizontalSheetSection_verticalSheetSection horizontalSheetSection_verticalSheetSection;
			if (this.Is_horizontalSheetSection_verticalSheetSection(g, out horizontalSheetSection_verticalSheetSection))
			{
				func0(horizontalSheetSection_verticalSheetSection);
				return;
			}
			KthHorizontal kthHorizontal;
			if (this.Is_KthHorizontal(g, out kthHorizontal))
			{
				func1(kthHorizontal);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol horizontalSheetSection");
		}

		// Token: 0x0600632A RID: 25386 RVA: 0x00142BD7 File Offset: 0x00140DD7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x00142BEC File Offset: 0x00140DEC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x00142C16 File Offset: 0x00140E16
		public bool Equals(horizontalSheetSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C02 RID: 11266
		private ProgramNode _node;
	}
}
