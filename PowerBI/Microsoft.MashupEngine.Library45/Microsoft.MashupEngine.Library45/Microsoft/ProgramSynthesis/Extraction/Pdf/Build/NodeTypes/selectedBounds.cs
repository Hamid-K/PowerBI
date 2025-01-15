using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C00 RID: 3072
	public struct selectedBounds : IProgramNodeBuilder, IEquatable<selectedBounds>
	{
		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06004F15 RID: 20245 RVA: 0x000F9C26 File Offset: 0x000F7E26
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F16 RID: 20246 RVA: 0x000F9C2E File Offset: 0x000F7E2E
		private selectedBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F17 RID: 20247 RVA: 0x000F9C37 File Offset: 0x000F7E37
		public static selectedBounds CreateUnsafe(ProgramNode node)
		{
			return new selectedBounds(node);
		}

		// Token: 0x06004F18 RID: 20248 RVA: 0x000F9C40 File Offset: 0x000F7E40
		public static selectedBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.selectedBounds)
			{
				return null;
			}
			return new selectedBounds?(selectedBounds.CreateUnsafe(node));
		}

		// Token: 0x06004F19 RID: 20249 RVA: 0x000F9C7A File Offset: 0x000F7E7A
		public static selectedBounds CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new selectedBounds(new Hole(g.Symbol.selectedBounds, holeId));
		}

		// Token: 0x06004F1A RID: 20250 RVA: 0x000F9C92 File Offset: 0x000F7E92
		public bool Is_selectedBounds_fixedBounds(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.selectedBounds_fixedBounds;
		}

		// Token: 0x06004F1B RID: 20251 RVA: 0x000F9CAC File Offset: 0x000F7EAC
		public bool Is_selectedBounds_fixedBounds(GrammarBuilders g, out selectedBounds_fixedBounds value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selectedBounds_fixedBounds)
			{
				value = selectedBounds_fixedBounds.CreateUnsafe(this.Node);
				return true;
			}
			value = default(selectedBounds_fixedBounds);
			return false;
		}

		// Token: 0x06004F1C RID: 20252 RVA: 0x000F9CE4 File Offset: 0x000F7EE4
		public selectedBounds_fixedBounds? As_selectedBounds_fixedBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.selectedBounds_fixedBounds)
			{
				return null;
			}
			return new selectedBounds_fixedBounds?(selectedBounds_fixedBounds.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F1D RID: 20253 RVA: 0x000F9D24 File Offset: 0x000F7F24
		public selectedBounds_fixedBounds Cast_selectedBounds_fixedBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.selectedBounds_fixedBounds)
			{
				return selectedBounds_fixedBounds.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_selectedBounds_fixedBounds is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F1E RID: 20254 RVA: 0x000F9D79 File Offset: 0x000F7F79
		public bool Is_PageBounds(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.PageBounds;
		}

		// Token: 0x06004F1F RID: 20255 RVA: 0x000F9D93 File Offset: 0x000F7F93
		public bool Is_PageBounds(GrammarBuilders g, out PageBounds value)
		{
			if (this.Node.GrammarRule == g.Rule.PageBounds)
			{
				value = PageBounds.CreateUnsafe(this.Node);
				return true;
			}
			value = default(PageBounds);
			return false;
		}

		// Token: 0x06004F20 RID: 20256 RVA: 0x000F9DC8 File Offset: 0x000F7FC8
		public PageBounds? As_PageBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.PageBounds)
			{
				return null;
			}
			return new PageBounds?(PageBounds.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F21 RID: 20257 RVA: 0x000F9E08 File Offset: 0x000F8008
		public PageBounds Cast_PageBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.PageBounds)
			{
				return PageBounds.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_PageBounds is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F22 RID: 20258 RVA: 0x000F9E5D File Offset: 0x000F805D
		public bool Is_NextSeparator(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NextSeparator;
		}

		// Token: 0x06004F23 RID: 20259 RVA: 0x000F9E77 File Offset: 0x000F8077
		public bool Is_NextSeparator(GrammarBuilders g, out NextSeparator value)
		{
			if (this.Node.GrammarRule == g.Rule.NextSeparator)
			{
				value = NextSeparator.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NextSeparator);
			return false;
		}

		// Token: 0x06004F24 RID: 20260 RVA: 0x000F9EAC File Offset: 0x000F80AC
		public NextSeparator? As_NextSeparator(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NextSeparator)
			{
				return null;
			}
			return new NextSeparator?(NextSeparator.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F25 RID: 20261 RVA: 0x000F9EEC File Offset: 0x000F80EC
		public NextSeparator Cast_NextSeparator(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NextSeparator)
			{
				return NextSeparator.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NextSeparator is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F26 RID: 20262 RVA: 0x000F9F41 File Offset: 0x000F8141
		public bool Is_LetBetweenAxis(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LetBetweenAxis;
		}

		// Token: 0x06004F27 RID: 20263 RVA: 0x000F9F5B File Offset: 0x000F815B
		public bool Is_LetBetweenAxis(GrammarBuilders g, out LetBetweenAxis value)
		{
			if (this.Node.GrammarRule == g.Rule.LetBetweenAxis)
			{
				value = LetBetweenAxis.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LetBetweenAxis);
			return false;
		}

		// Token: 0x06004F28 RID: 20264 RVA: 0x000F9F90 File Offset: 0x000F8190
		public LetBetweenAxis? As_LetBetweenAxis(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LetBetweenAxis)
			{
				return null;
			}
			return new LetBetweenAxis?(LetBetweenAxis.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F29 RID: 20265 RVA: 0x000F9FD0 File Offset: 0x000F81D0
		public LetBetweenAxis Cast_LetBetweenAxis(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LetBetweenAxis)
			{
				return LetBetweenAxis.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LetBetweenAxis is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F2A RID: 20266 RVA: 0x000FA028 File Offset: 0x000F8228
		public T Switch<T>(GrammarBuilders g, Func<selectedBounds_fixedBounds, T> func0, Func<PageBounds, T> func1, Func<NextSeparator, T> func2, Func<LetBetweenAxis, T> func3)
		{
			selectedBounds_fixedBounds selectedBounds_fixedBounds;
			if (this.Is_selectedBounds_fixedBounds(g, out selectedBounds_fixedBounds))
			{
				return func0(selectedBounds_fixedBounds);
			}
			PageBounds pageBounds;
			if (this.Is_PageBounds(g, out pageBounds))
			{
				return func1(pageBounds);
			}
			NextSeparator nextSeparator;
			if (this.Is_NextSeparator(g, out nextSeparator))
			{
				return func2(nextSeparator);
			}
			LetBetweenAxis letBetweenAxis;
			if (this.Is_LetBetweenAxis(g, out letBetweenAxis))
			{
				return func3(letBetweenAxis);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selectedBounds");
		}

		// Token: 0x06004F2B RID: 20267 RVA: 0x000FA0A8 File Offset: 0x000F82A8
		public void Switch(GrammarBuilders g, Action<selectedBounds_fixedBounds> func0, Action<PageBounds> func1, Action<NextSeparator> func2, Action<LetBetweenAxis> func3)
		{
			selectedBounds_fixedBounds selectedBounds_fixedBounds;
			if (this.Is_selectedBounds_fixedBounds(g, out selectedBounds_fixedBounds))
			{
				func0(selectedBounds_fixedBounds);
				return;
			}
			PageBounds pageBounds;
			if (this.Is_PageBounds(g, out pageBounds))
			{
				func1(pageBounds);
				return;
			}
			NextSeparator nextSeparator;
			if (this.Is_NextSeparator(g, out nextSeparator))
			{
				func2(nextSeparator);
				return;
			}
			LetBetweenAxis letBetweenAxis;
			if (this.Is_LetBetweenAxis(g, out letBetweenAxis))
			{
				func3(letBetweenAxis);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol selectedBounds");
		}

		// Token: 0x06004F2C RID: 20268 RVA: 0x000FA127 File Offset: 0x000F8327
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F2D RID: 20269 RVA: 0x000FA13C File Offset: 0x000F833C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F2E RID: 20270 RVA: 0x000FA166 File Offset: 0x000F8366
		public bool Equals(selectedBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002328 RID: 9000
		private ProgramNode _node;
	}
}
