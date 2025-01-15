using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Pdf.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.Build.NodeTypes
{
	// Token: 0x02000C02 RID: 3074
	public struct beforeRelativeBounds : IProgramNodeBuilder, IEquatable<beforeRelativeBounds>
	{
		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06004F41 RID: 20289 RVA: 0x000FA4B6 File Offset: 0x000F86B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004F42 RID: 20290 RVA: 0x000FA4BE File Offset: 0x000F86BE
		private beforeRelativeBounds(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004F43 RID: 20291 RVA: 0x000FA4C7 File Offset: 0x000F86C7
		public static beforeRelativeBounds CreateUnsafe(ProgramNode node)
		{
			return new beforeRelativeBounds(node);
		}

		// Token: 0x06004F44 RID: 20292 RVA: 0x000FA4D0 File Offset: 0x000F86D0
		public static beforeRelativeBounds? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.beforeRelativeBounds)
			{
				return null;
			}
			return new beforeRelativeBounds?(beforeRelativeBounds.CreateUnsafe(node));
		}

		// Token: 0x06004F45 RID: 20293 RVA: 0x000FA50A File Offset: 0x000F870A
		public static beforeRelativeBounds CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new beforeRelativeBounds(new Hole(g.Symbol.beforeRelativeBounds, holeId));
		}

		// Token: 0x06004F46 RID: 20294 RVA: 0x000FA522 File Offset: 0x000F8722
		public bool Is_beforeRelativeBounds_selectedBounds(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.beforeRelativeBounds_selectedBounds;
		}

		// Token: 0x06004F47 RID: 20295 RVA: 0x000FA53C File Offset: 0x000F873C
		public bool Is_beforeRelativeBounds_selectedBounds(GrammarBuilders g, out beforeRelativeBounds_selectedBounds value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.beforeRelativeBounds_selectedBounds)
			{
				value = beforeRelativeBounds_selectedBounds.CreateUnsafe(this.Node);
				return true;
			}
			value = default(beforeRelativeBounds_selectedBounds);
			return false;
		}

		// Token: 0x06004F48 RID: 20296 RVA: 0x000FA574 File Offset: 0x000F8774
		public beforeRelativeBounds_selectedBounds? As_beforeRelativeBounds_selectedBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.beforeRelativeBounds_selectedBounds)
			{
				return null;
			}
			return new beforeRelativeBounds_selectedBounds?(beforeRelativeBounds_selectedBounds.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F49 RID: 20297 RVA: 0x000FA5B4 File Offset: 0x000F87B4
		public beforeRelativeBounds_selectedBounds Cast_beforeRelativeBounds_selectedBounds(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.beforeRelativeBounds_selectedBounds)
			{
				return beforeRelativeBounds_selectedBounds.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_beforeRelativeBounds_selectedBounds is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F4A RID: 20298 RVA: 0x000FA609 File Offset: 0x000F8809
		public bool Is_NextSeparator_beforeRelative(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NextSeparator_beforeRelative;
		}

		// Token: 0x06004F4B RID: 20299 RVA: 0x000FA623 File Offset: 0x000F8823
		public bool Is_NextSeparator_beforeRelative(GrammarBuilders g, out NextSeparator_beforeRelative value)
		{
			if (this.Node.GrammarRule == g.Rule.NextSeparator_beforeRelative)
			{
				value = NextSeparator_beforeRelative.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NextSeparator_beforeRelative);
			return false;
		}

		// Token: 0x06004F4C RID: 20300 RVA: 0x000FA658 File Offset: 0x000F8858
		public NextSeparator_beforeRelative? As_NextSeparator_beforeRelative(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NextSeparator_beforeRelative)
			{
				return null;
			}
			return new NextSeparator_beforeRelative?(NextSeparator_beforeRelative.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F4D RID: 20301 RVA: 0x000FA698 File Offset: 0x000F8898
		public NextSeparator_beforeRelative Cast_NextSeparator_beforeRelative(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NextSeparator_beforeRelative)
			{
				return NextSeparator_beforeRelative.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NextSeparator_beforeRelative is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F4E RID: 20302 RVA: 0x000FA6ED File Offset: 0x000F88ED
		public bool Is_NextSameWidthSeparator(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NextSameWidthSeparator;
		}

		// Token: 0x06004F4F RID: 20303 RVA: 0x000FA707 File Offset: 0x000F8907
		public bool Is_NextSameWidthSeparator(GrammarBuilders g, out NextSameWidthSeparator value)
		{
			if (this.Node.GrammarRule == g.Rule.NextSameWidthSeparator)
			{
				value = NextSameWidthSeparator.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NextSameWidthSeparator);
			return false;
		}

		// Token: 0x06004F50 RID: 20304 RVA: 0x000FA73C File Offset: 0x000F893C
		public NextSameWidthSeparator? As_NextSameWidthSeparator(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NextSameWidthSeparator)
			{
				return null;
			}
			return new NextSameWidthSeparator?(NextSameWidthSeparator.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F51 RID: 20305 RVA: 0x000FA77C File Offset: 0x000F897C
		public NextSameWidthSeparator Cast_NextSameWidthSeparator(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NextSameWidthSeparator)
			{
				return NextSameWidthSeparator.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NextSameWidthSeparator is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F52 RID: 20306 RVA: 0x000FA7D1 File Offset: 0x000F89D1
		public bool Is_NextFontSizeDecrease(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NextFontSizeDecrease;
		}

		// Token: 0x06004F53 RID: 20307 RVA: 0x000FA7EB File Offset: 0x000F89EB
		public bool Is_NextFontSizeDecrease(GrammarBuilders g, out NextFontSizeDecrease value)
		{
			if (this.Node.GrammarRule == g.Rule.NextFontSizeDecrease)
			{
				value = NextFontSizeDecrease.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NextFontSizeDecrease);
			return false;
		}

		// Token: 0x06004F54 RID: 20308 RVA: 0x000FA820 File Offset: 0x000F8A20
		public NextFontSizeDecrease? As_NextFontSizeDecrease(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NextFontSizeDecrease)
			{
				return null;
			}
			return new NextFontSizeDecrease?(NextFontSizeDecrease.CreateUnsafe(this.Node));
		}

		// Token: 0x06004F55 RID: 20309 RVA: 0x000FA860 File Offset: 0x000F8A60
		public NextFontSizeDecrease Cast_NextFontSizeDecrease(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NextFontSizeDecrease)
			{
				return NextFontSizeDecrease.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NextFontSizeDecrease is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06004F56 RID: 20310 RVA: 0x000FA8B8 File Offset: 0x000F8AB8
		public T Switch<T>(GrammarBuilders g, Func<beforeRelativeBounds_selectedBounds, T> func0, Func<NextSeparator_beforeRelative, T> func1, Func<NextSameWidthSeparator, T> func2, Func<NextFontSizeDecrease, T> func3)
		{
			beforeRelativeBounds_selectedBounds beforeRelativeBounds_selectedBounds;
			if (this.Is_beforeRelativeBounds_selectedBounds(g, out beforeRelativeBounds_selectedBounds))
			{
				return func0(beforeRelativeBounds_selectedBounds);
			}
			NextSeparator_beforeRelative nextSeparator_beforeRelative;
			if (this.Is_NextSeparator_beforeRelative(g, out nextSeparator_beforeRelative))
			{
				return func1(nextSeparator_beforeRelative);
			}
			NextSameWidthSeparator nextSameWidthSeparator;
			if (this.Is_NextSameWidthSeparator(g, out nextSameWidthSeparator))
			{
				return func2(nextSameWidthSeparator);
			}
			NextFontSizeDecrease nextFontSizeDecrease;
			if (this.Is_NextFontSizeDecrease(g, out nextFontSizeDecrease))
			{
				return func3(nextFontSizeDecrease);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol beforeRelativeBounds");
		}

		// Token: 0x06004F57 RID: 20311 RVA: 0x000FA938 File Offset: 0x000F8B38
		public void Switch(GrammarBuilders g, Action<beforeRelativeBounds_selectedBounds> func0, Action<NextSeparator_beforeRelative> func1, Action<NextSameWidthSeparator> func2, Action<NextFontSizeDecrease> func3)
		{
			beforeRelativeBounds_selectedBounds beforeRelativeBounds_selectedBounds;
			if (this.Is_beforeRelativeBounds_selectedBounds(g, out beforeRelativeBounds_selectedBounds))
			{
				func0(beforeRelativeBounds_selectedBounds);
				return;
			}
			NextSeparator_beforeRelative nextSeparator_beforeRelative;
			if (this.Is_NextSeparator_beforeRelative(g, out nextSeparator_beforeRelative))
			{
				func1(nextSeparator_beforeRelative);
				return;
			}
			NextSameWidthSeparator nextSameWidthSeparator;
			if (this.Is_NextSameWidthSeparator(g, out nextSameWidthSeparator))
			{
				func2(nextSameWidthSeparator);
				return;
			}
			NextFontSizeDecrease nextFontSizeDecrease;
			if (this.Is_NextFontSizeDecrease(g, out nextFontSizeDecrease))
			{
				func3(nextFontSizeDecrease);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol beforeRelativeBounds");
		}

		// Token: 0x06004F58 RID: 20312 RVA: 0x000FA9B7 File Offset: 0x000F8BB7
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004F59 RID: 20313 RVA: 0x000FA9CC File Offset: 0x000F8BCC
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004F5A RID: 20314 RVA: 0x000FA9F6 File Offset: 0x000F8BF6
		public bool Equals(beforeRelativeBounds other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400232A RID: 9002
		private ProgramNode _node;
	}
}
