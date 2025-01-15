using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E59 RID: 3673
	public struct verticalSheetSection : IProgramNodeBuilder, IEquatable<verticalSheetSection>
	{
		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x0600632D RID: 25389 RVA: 0x00142C2A File Offset: 0x00140E2A
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600632E RID: 25390 RVA: 0x00142C32 File Offset: 0x00140E32
		private verticalSheetSection(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x00142C3B File Offset: 0x00140E3B
		public static verticalSheetSection CreateUnsafe(ProgramNode node)
		{
			return new verticalSheetSection(node);
		}

		// Token: 0x06006330 RID: 25392 RVA: 0x00142C44 File Offset: 0x00140E44
		public static verticalSheetSection? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.verticalSheetSection)
			{
				return null;
			}
			return new verticalSheetSection?(verticalSheetSection.CreateUnsafe(node));
		}

		// Token: 0x06006331 RID: 25393 RVA: 0x00142C7E File Offset: 0x00140E7E
		public static verticalSheetSection CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new verticalSheetSection(new Hole(g.Symbol.verticalSheetSection, holeId));
		}

		// Token: 0x06006332 RID: 25394 RVA: 0x00142C96 File Offset: 0x00140E96
		public bool Is_verticalSheetSection_uncleanedSheetSection(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.verticalSheetSection_uncleanedSheetSection;
		}

		// Token: 0x06006333 RID: 25395 RVA: 0x00142CB0 File Offset: 0x00140EB0
		public bool Is_verticalSheetSection_uncleanedSheetSection(GrammarBuilders g, out verticalSheetSection_uncleanedSheetSection value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.verticalSheetSection_uncleanedSheetSection)
			{
				value = verticalSheetSection_uncleanedSheetSection.CreateUnsafe(this.Node);
				return true;
			}
			value = default(verticalSheetSection_uncleanedSheetSection);
			return false;
		}

		// Token: 0x06006334 RID: 25396 RVA: 0x00142CE8 File Offset: 0x00140EE8
		public verticalSheetSection_uncleanedSheetSection? As_verticalSheetSection_uncleanedSheetSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.verticalSheetSection_uncleanedSheetSection)
			{
				return null;
			}
			return new verticalSheetSection_uncleanedSheetSection?(verticalSheetSection_uncleanedSheetSection.CreateUnsafe(this.Node));
		}

		// Token: 0x06006335 RID: 25397 RVA: 0x00142D28 File Offset: 0x00140F28
		public verticalSheetSection_uncleanedSheetSection Cast_verticalSheetSection_uncleanedSheetSection(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.verticalSheetSection_uncleanedSheetSection)
			{
				return verticalSheetSection_uncleanedSheetSection.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_verticalSheetSection_uncleanedSheetSection is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006336 RID: 25398 RVA: 0x00142D7D File Offset: 0x00140F7D
		public bool Is_KthVertical(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.KthVertical;
		}

		// Token: 0x06006337 RID: 25399 RVA: 0x00142D97 File Offset: 0x00140F97
		public bool Is_KthVertical(GrammarBuilders g, out KthVertical value)
		{
			if (this.Node.GrammarRule == g.Rule.KthVertical)
			{
				value = KthVertical.CreateUnsafe(this.Node);
				return true;
			}
			value = default(KthVertical);
			return false;
		}

		// Token: 0x06006338 RID: 25400 RVA: 0x00142DCC File Offset: 0x00140FCC
		public KthVertical? As_KthVertical(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.KthVertical)
			{
				return null;
			}
			return new KthVertical?(KthVertical.CreateUnsafe(this.Node));
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x00142E0C File Offset: 0x0014100C
		public KthVertical Cast_KthVertical(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.KthVertical)
			{
				return KthVertical.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_KthVertical is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600633A RID: 25402 RVA: 0x00142E64 File Offset: 0x00141064
		public T Switch<T>(GrammarBuilders g, Func<verticalSheetSection_uncleanedSheetSection, T> func0, Func<KthVertical, T> func1)
		{
			verticalSheetSection_uncleanedSheetSection verticalSheetSection_uncleanedSheetSection;
			if (this.Is_verticalSheetSection_uncleanedSheetSection(g, out verticalSheetSection_uncleanedSheetSection))
			{
				return func0(verticalSheetSection_uncleanedSheetSection);
			}
			KthVertical kthVertical;
			if (this.Is_KthVertical(g, out kthVertical))
			{
				return func1(kthVertical);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol verticalSheetSection");
		}

		// Token: 0x0600633B RID: 25403 RVA: 0x00142EBC File Offset: 0x001410BC
		public void Switch(GrammarBuilders g, Action<verticalSheetSection_uncleanedSheetSection> func0, Action<KthVertical> func1)
		{
			verticalSheetSection_uncleanedSheetSection verticalSheetSection_uncleanedSheetSection;
			if (this.Is_verticalSheetSection_uncleanedSheetSection(g, out verticalSheetSection_uncleanedSheetSection))
			{
				func0(verticalSheetSection_uncleanedSheetSection);
				return;
			}
			KthVertical kthVertical;
			if (this.Is_KthVertical(g, out kthVertical))
			{
				func1(kthVertical);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol verticalSheetSection");
		}

		// Token: 0x0600633C RID: 25404 RVA: 0x00142F13 File Offset: 0x00141113
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600633D RID: 25405 RVA: 0x00142F28 File Offset: 0x00141128
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x00142F52 File Offset: 0x00141152
		public bool Equals(verticalSheetSection other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C03 RID: 11267
		private ProgramNode _node;
	}
}
