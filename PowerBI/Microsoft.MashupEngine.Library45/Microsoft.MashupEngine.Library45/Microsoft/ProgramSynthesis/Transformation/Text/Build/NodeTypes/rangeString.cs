using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C42 RID: 7234
	public struct rangeString : IProgramNodeBuilder, IEquatable<rangeString>
	{
		// Token: 0x170028D8 RID: 10456
		// (get) Token: 0x0600F436 RID: 62518 RVA: 0x00344A76 File Offset: 0x00342C76
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F437 RID: 62519 RVA: 0x00344A7E File Offset: 0x00342C7E
		private rangeString(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F438 RID: 62520 RVA: 0x00344A87 File Offset: 0x00342C87
		public static rangeString CreateUnsafe(ProgramNode node)
		{
			return new rangeString(node);
		}

		// Token: 0x0600F439 RID: 62521 RVA: 0x00344A90 File Offset: 0x00342C90
		public static rangeString? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.rangeString)
			{
				return null;
			}
			return new rangeString?(rangeString.CreateUnsafe(node));
		}

		// Token: 0x0600F43A RID: 62522 RVA: 0x00344ACA File Offset: 0x00342CCA
		public static rangeString CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new rangeString(new Hole(g.Symbol.rangeString, holeId));
		}

		// Token: 0x0600F43B RID: 62523 RVA: 0x00344AE2 File Offset: 0x00342CE2
		public bool Is_rangeString_rangeSubstring(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.rangeString_rangeSubstring;
		}

		// Token: 0x0600F43C RID: 62524 RVA: 0x00344AFC File Offset: 0x00342CFC
		public bool Is_rangeString_rangeSubstring(GrammarBuilders g, out rangeString_rangeSubstring value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.rangeString_rangeSubstring)
			{
				value = rangeString_rangeSubstring.CreateUnsafe(this.Node);
				return true;
			}
			value = default(rangeString_rangeSubstring);
			return false;
		}

		// Token: 0x0600F43D RID: 62525 RVA: 0x00344B34 File Offset: 0x00342D34
		public rangeString_rangeSubstring? As_rangeString_rangeSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.rangeString_rangeSubstring)
			{
				return null;
			}
			return new rangeString_rangeSubstring?(rangeString_rangeSubstring.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F43E RID: 62526 RVA: 0x00344B74 File Offset: 0x00342D74
		public rangeString_rangeSubstring Cast_rangeString_rangeSubstring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.rangeString_rangeSubstring)
			{
				return rangeString_rangeSubstring.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_rangeString_rangeSubstring is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F43F RID: 62527 RVA: 0x00344BC9 File Offset: 0x00342DC9
		public bool Is_RangeConcat(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RangeConcat;
		}

		// Token: 0x0600F440 RID: 62528 RVA: 0x00344BE3 File Offset: 0x00342DE3
		public bool Is_RangeConcat(GrammarBuilders g, out RangeConcat value)
		{
			if (this.Node.GrammarRule == g.Rule.RangeConcat)
			{
				value = RangeConcat.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RangeConcat);
			return false;
		}

		// Token: 0x0600F441 RID: 62529 RVA: 0x00344C18 File Offset: 0x00342E18
		public RangeConcat? As_RangeConcat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RangeConcat)
			{
				return null;
			}
			return new RangeConcat?(RangeConcat.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F442 RID: 62530 RVA: 0x00344C58 File Offset: 0x00342E58
		public RangeConcat Cast_RangeConcat(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RangeConcat)
			{
				return RangeConcat.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RangeConcat is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600F443 RID: 62531 RVA: 0x00344CB0 File Offset: 0x00342EB0
		public T Switch<T>(GrammarBuilders g, Func<rangeString_rangeSubstring, T> func0, Func<RangeConcat, T> func1)
		{
			rangeString_rangeSubstring rangeString_rangeSubstring;
			if (this.Is_rangeString_rangeSubstring(g, out rangeString_rangeSubstring))
			{
				return func0(rangeString_rangeSubstring);
			}
			RangeConcat rangeConcat;
			if (this.Is_RangeConcat(g, out rangeConcat))
			{
				return func1(rangeConcat);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol rangeString");
		}

		// Token: 0x0600F444 RID: 62532 RVA: 0x00344D08 File Offset: 0x00342F08
		public void Switch(GrammarBuilders g, Action<rangeString_rangeSubstring> func0, Action<RangeConcat> func1)
		{
			rangeString_rangeSubstring rangeString_rangeSubstring;
			if (this.Is_rangeString_rangeSubstring(g, out rangeString_rangeSubstring))
			{
				func0(rangeString_rangeSubstring);
				return;
			}
			RangeConcat rangeConcat;
			if (this.Is_RangeConcat(g, out rangeConcat))
			{
				func1(rangeConcat);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol rangeString");
		}

		// Token: 0x0600F445 RID: 62533 RVA: 0x00344D5F File Offset: 0x00342F5F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F446 RID: 62534 RVA: 0x00344D74 File Offset: 0x00342F74
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F447 RID: 62535 RVA: 0x00344D9E File Offset: 0x00342F9E
		public bool Equals(rangeString other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B31 RID: 23345
		private ProgramNode _node;
	}
}
