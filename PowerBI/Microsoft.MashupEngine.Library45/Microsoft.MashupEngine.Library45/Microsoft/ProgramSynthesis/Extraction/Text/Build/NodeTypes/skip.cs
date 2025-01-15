using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F3E RID: 3902
	public struct skip : IProgramNodeBuilder, IEquatable<skip>
	{
		// Token: 0x1700135F RID: 4959
		// (get) Token: 0x06006C83 RID: 27779 RVA: 0x00163442 File Offset: 0x00161642
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006C84 RID: 27780 RVA: 0x0016344A File Offset: 0x0016164A
		private skip(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006C85 RID: 27781 RVA: 0x00163453 File Offset: 0x00161653
		public static skip CreateUnsafe(ProgramNode node)
		{
			return new skip(node);
		}

		// Token: 0x06006C86 RID: 27782 RVA: 0x0016345C File Offset: 0x0016165C
		public static skip? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.skip)
			{
				return null;
			}
			return new skip?(skip.CreateUnsafe(node));
		}

		// Token: 0x06006C87 RID: 27783 RVA: 0x00163496 File Offset: 0x00161696
		public static skip CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new skip(new Hole(g.Symbol.skip, holeId));
		}

		// Token: 0x06006C88 RID: 27784 RVA: 0x001634AE File Offset: 0x001616AE
		public bool Is_skip_lines(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.skip_lines;
		}

		// Token: 0x06006C89 RID: 27785 RVA: 0x001634C8 File Offset: 0x001616C8
		public bool Is_skip_lines(GrammarBuilders g, out skip_lines value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.skip_lines)
			{
				value = skip_lines.CreateUnsafe(this.Node);
				return true;
			}
			value = default(skip_lines);
			return false;
		}

		// Token: 0x06006C8A RID: 27786 RVA: 0x00163500 File Offset: 0x00161700
		public skip_lines? As_skip_lines(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.skip_lines)
			{
				return null;
			}
			return new skip_lines?(skip_lines.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C8B RID: 27787 RVA: 0x00163540 File Offset: 0x00161740
		public skip_lines Cast_skip_lines(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.skip_lines)
			{
				return skip_lines.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_skip_lines is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C8C RID: 27788 RVA: 0x00163595 File Offset: 0x00161795
		public bool Is_Skip(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Skip;
		}

		// Token: 0x06006C8D RID: 27789 RVA: 0x001635AF File Offset: 0x001617AF
		public bool Is_Skip(GrammarBuilders g, out Skip value)
		{
			if (this.Node.GrammarRule == g.Rule.Skip)
			{
				value = Skip.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Skip);
			return false;
		}

		// Token: 0x06006C8E RID: 27790 RVA: 0x001635E4 File Offset: 0x001617E4
		public Skip? As_Skip(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Skip)
			{
				return null;
			}
			return new Skip?(Skip.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C8F RID: 27791 RVA: 0x00163624 File Offset: 0x00161824
		public Skip Cast_Skip(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Skip)
			{
				return Skip.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Skip is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C90 RID: 27792 RVA: 0x0016367C File Offset: 0x0016187C
		public T Switch<T>(GrammarBuilders g, Func<skip_lines, T> func0, Func<Skip, T> func1)
		{
			skip_lines skip_lines;
			if (this.Is_skip_lines(g, out skip_lines))
			{
				return func0(skip_lines);
			}
			Skip skip;
			if (this.Is_Skip(g, out skip))
			{
				return func1(skip);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol skip");
		}

		// Token: 0x06006C91 RID: 27793 RVA: 0x001636D4 File Offset: 0x001618D4
		public void Switch(GrammarBuilders g, Action<skip_lines> func0, Action<Skip> func1)
		{
			skip_lines skip_lines;
			if (this.Is_skip_lines(g, out skip_lines))
			{
				func0(skip_lines);
				return;
			}
			Skip skip;
			if (this.Is_Skip(g, out skip))
			{
				func1(skip);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol skip");
		}

		// Token: 0x06006C92 RID: 27794 RVA: 0x0016372B File Offset: 0x0016192B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006C93 RID: 27795 RVA: 0x00163740 File Offset: 0x00161940
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006C94 RID: 27796 RVA: 0x0016376A File Offset: 0x0016196A
		public bool Equals(skip other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F29 RID: 12073
		private ProgramNode _node;
	}
}
