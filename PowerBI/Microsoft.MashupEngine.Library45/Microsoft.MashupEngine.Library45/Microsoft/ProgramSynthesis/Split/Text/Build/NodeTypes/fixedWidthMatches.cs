using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Split.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Split.Text.Build.NodeTypes
{
	// Token: 0x0200136C RID: 4972
	public struct fixedWidthMatches : IProgramNodeBuilder, IEquatable<fixedWidthMatches>
	{
		// Token: 0x17001A75 RID: 6773
		// (get) Token: 0x06009A0B RID: 39435 RVA: 0x0020A3BA File Offset: 0x002085BA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06009A0C RID: 39436 RVA: 0x0020A3C2 File Offset: 0x002085C2
		private fixedWidthMatches(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06009A0D RID: 39437 RVA: 0x0020A3CB File Offset: 0x002085CB
		public static fixedWidthMatches CreateUnsafe(ProgramNode node)
		{
			return new fixedWidthMatches(node);
		}

		// Token: 0x06009A0E RID: 39438 RVA: 0x0020A3D4 File Offset: 0x002085D4
		public static fixedWidthMatches? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.fixedWidthMatches)
			{
				return null;
			}
			return new fixedWidthMatches?(fixedWidthMatches.CreateUnsafe(node));
		}

		// Token: 0x06009A0F RID: 39439 RVA: 0x0020A40E File Offset: 0x0020860E
		public static fixedWidthMatches CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new fixedWidthMatches(new Hole(g.Symbol.fixedWidthMatches, holeId));
		}

		// Token: 0x06009A10 RID: 39440 RVA: 0x0020A426 File Offset: 0x00208626
		public bool Is_FixedWidth(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FixedWidth;
		}

		// Token: 0x06009A11 RID: 39441 RVA: 0x0020A440 File Offset: 0x00208640
		public bool Is_FixedWidth(GrammarBuilders g, out FixedWidth value)
		{
			if (this.Node.GrammarRule == g.Rule.FixedWidth)
			{
				value = FixedWidth.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FixedWidth);
			return false;
		}

		// Token: 0x06009A12 RID: 39442 RVA: 0x0020A478 File Offset: 0x00208678
		public FixedWidth? As_FixedWidth(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FixedWidth)
			{
				return null;
			}
			return new FixedWidth?(FixedWidth.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A13 RID: 39443 RVA: 0x0020A4B8 File Offset: 0x002086B8
		public FixedWidth Cast_FixedWidth(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FixedWidth)
			{
				return FixedWidth.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FixedWidth is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009A14 RID: 39444 RVA: 0x0020A50D File Offset: 0x0020870D
		public bool Is_FixedWidthDelimiters(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.FixedWidthDelimiters;
		}

		// Token: 0x06009A15 RID: 39445 RVA: 0x0020A527 File Offset: 0x00208727
		public bool Is_FixedWidthDelimiters(GrammarBuilders g, out FixedWidthDelimiters value)
		{
			if (this.Node.GrammarRule == g.Rule.FixedWidthDelimiters)
			{
				value = FixedWidthDelimiters.CreateUnsafe(this.Node);
				return true;
			}
			value = default(FixedWidthDelimiters);
			return false;
		}

		// Token: 0x06009A16 RID: 39446 RVA: 0x0020A55C File Offset: 0x0020875C
		public FixedWidthDelimiters? As_FixedWidthDelimiters(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.FixedWidthDelimiters)
			{
				return null;
			}
			return new FixedWidthDelimiters?(FixedWidthDelimiters.CreateUnsafe(this.Node));
		}

		// Token: 0x06009A17 RID: 39447 RVA: 0x0020A59C File Offset: 0x0020879C
		public FixedWidthDelimiters Cast_FixedWidthDelimiters(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.FixedWidthDelimiters)
			{
				return FixedWidthDelimiters.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_FixedWidthDelimiters is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06009A18 RID: 39448 RVA: 0x0020A5F4 File Offset: 0x002087F4
		public T Switch<T>(GrammarBuilders g, Func<FixedWidth, T> func0, Func<FixedWidthDelimiters, T> func1)
		{
			FixedWidth fixedWidth;
			if (this.Is_FixedWidth(g, out fixedWidth))
			{
				return func0(fixedWidth);
			}
			FixedWidthDelimiters fixedWidthDelimiters;
			if (this.Is_FixedWidthDelimiters(g, out fixedWidthDelimiters))
			{
				return func1(fixedWidthDelimiters);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol fixedWidthMatches");
		}

		// Token: 0x06009A19 RID: 39449 RVA: 0x0020A64C File Offset: 0x0020884C
		public void Switch(GrammarBuilders g, Action<FixedWidth> func0, Action<FixedWidthDelimiters> func1)
		{
			FixedWidth fixedWidth;
			if (this.Is_FixedWidth(g, out fixedWidth))
			{
				func0(fixedWidth);
				return;
			}
			FixedWidthDelimiters fixedWidthDelimiters;
			if (this.Is_FixedWidthDelimiters(g, out fixedWidthDelimiters))
			{
				func1(fixedWidthDelimiters);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol fixedWidthMatches");
		}

		// Token: 0x06009A1A RID: 39450 RVA: 0x0020A6A3 File Offset: 0x002088A3
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06009A1B RID: 39451 RVA: 0x0020A6B8 File Offset: 0x002088B8
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06009A1C RID: 39452 RVA: 0x0020A6E2 File Offset: 0x002088E2
		public bool Equals(fixedWidthMatches other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003DE3 RID: 15843
		private ProgramNode _node;
	}
}
