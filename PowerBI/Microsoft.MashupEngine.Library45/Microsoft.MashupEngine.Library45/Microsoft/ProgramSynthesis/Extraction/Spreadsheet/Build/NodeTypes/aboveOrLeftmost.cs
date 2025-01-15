using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E67 RID: 3687
	public struct aboveOrLeftmost : IProgramNodeBuilder, IEquatable<aboveOrLeftmost>
	{
		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06006435 RID: 25653 RVA: 0x00145846 File Offset: 0x00143A46
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006436 RID: 25654 RVA: 0x0014584E File Offset: 0x00143A4E
		private aboveOrLeftmost(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006437 RID: 25655 RVA: 0x00145857 File Offset: 0x00143A57
		public static aboveOrLeftmost CreateUnsafe(ProgramNode node)
		{
			return new aboveOrLeftmost(node);
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x00145860 File Offset: 0x00143A60
		public static aboveOrLeftmost? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.aboveOrLeftmost)
			{
				return null;
			}
			return new aboveOrLeftmost?(aboveOrLeftmost.CreateUnsafe(node));
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x0014589A File Offset: 0x00143A9A
		public static aboveOrLeftmost CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new aboveOrLeftmost(new Hole(g.Symbol.aboveOrLeftmost, holeId));
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x001458B2 File Offset: 0x00143AB2
		public bool Is_aboveOrLeftmost_above(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.aboveOrLeftmost_above;
		}

		// Token: 0x0600643B RID: 25659 RVA: 0x001458CC File Offset: 0x00143ACC
		public bool Is_aboveOrLeftmost_above(GrammarBuilders g, out aboveOrLeftmost_above value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.aboveOrLeftmost_above)
			{
				value = aboveOrLeftmost_above.CreateUnsafe(this.Node);
				return true;
			}
			value = default(aboveOrLeftmost_above);
			return false;
		}

		// Token: 0x0600643C RID: 25660 RVA: 0x00145904 File Offset: 0x00143B04
		public aboveOrLeftmost_above? As_aboveOrLeftmost_above(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.aboveOrLeftmost_above)
			{
				return null;
			}
			return new aboveOrLeftmost_above?(aboveOrLeftmost_above.CreateUnsafe(this.Node));
		}

		// Token: 0x0600643D RID: 25661 RVA: 0x00145944 File Offset: 0x00143B44
		public aboveOrLeftmost_above Cast_aboveOrLeftmost_above(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.aboveOrLeftmost_above)
			{
				return aboveOrLeftmost_above.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_aboveOrLeftmost_above is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600643E RID: 25662 RVA: 0x00145999 File Offset: 0x00143B99
		public bool Is_LeftmostColumn(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LeftmostColumn;
		}

		// Token: 0x0600643F RID: 25663 RVA: 0x001459B3 File Offset: 0x00143BB3
		public bool Is_LeftmostColumn(GrammarBuilders g, out LeftmostColumn value)
		{
			if (this.Node.GrammarRule == g.Rule.LeftmostColumn)
			{
				value = LeftmostColumn.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LeftmostColumn);
			return false;
		}

		// Token: 0x06006440 RID: 25664 RVA: 0x001459E8 File Offset: 0x00143BE8
		public LeftmostColumn? As_LeftmostColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LeftmostColumn)
			{
				return null;
			}
			return new LeftmostColumn?(LeftmostColumn.CreateUnsafe(this.Node));
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x00145A28 File Offset: 0x00143C28
		public LeftmostColumn Cast_LeftmostColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LeftmostColumn)
			{
				return LeftmostColumn.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LeftmostColumn is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x00145A7D File Offset: 0x00143C7D
		public bool Is_LeftOf(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.LeftOf;
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x00145A97 File Offset: 0x00143C97
		public bool Is_LeftOf(GrammarBuilders g, out LeftOf value)
		{
			if (this.Node.GrammarRule == g.Rule.LeftOf)
			{
				value = LeftOf.CreateUnsafe(this.Node);
				return true;
			}
			value = default(LeftOf);
			return false;
		}

		// Token: 0x06006444 RID: 25668 RVA: 0x00145ACC File Offset: 0x00143CCC
		public LeftOf? As_LeftOf(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.LeftOf)
			{
				return null;
			}
			return new LeftOf?(LeftOf.CreateUnsafe(this.Node));
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x00145B0C File Offset: 0x00143D0C
		public LeftOf Cast_LeftOf(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.LeftOf)
			{
				return LeftOf.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_LeftOf is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x00145B64 File Offset: 0x00143D64
		public T Switch<T>(GrammarBuilders g, Func<aboveOrLeftmost_above, T> func0, Func<LeftmostColumn, T> func1, Func<LeftOf, T> func2)
		{
			aboveOrLeftmost_above aboveOrLeftmost_above;
			if (this.Is_aboveOrLeftmost_above(g, out aboveOrLeftmost_above))
			{
				return func0(aboveOrLeftmost_above);
			}
			LeftmostColumn leftmostColumn;
			if (this.Is_LeftmostColumn(g, out leftmostColumn))
			{
				return func1(leftmostColumn);
			}
			LeftOf leftOf;
			if (this.Is_LeftOf(g, out leftOf))
			{
				return func2(leftOf);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol aboveOrLeftmost");
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x00145BD0 File Offset: 0x00143DD0
		public void Switch(GrammarBuilders g, Action<aboveOrLeftmost_above> func0, Action<LeftmostColumn> func1, Action<LeftOf> func2)
		{
			aboveOrLeftmost_above aboveOrLeftmost_above;
			if (this.Is_aboveOrLeftmost_above(g, out aboveOrLeftmost_above))
			{
				func0(aboveOrLeftmost_above);
				return;
			}
			LeftmostColumn leftmostColumn;
			if (this.Is_LeftmostColumn(g, out leftmostColumn))
			{
				func1(leftmostColumn);
				return;
			}
			LeftOf leftOf;
			if (this.Is_LeftOf(g, out leftOf))
			{
				func2(leftOf);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol aboveOrLeftmost");
		}

		// Token: 0x06006448 RID: 25672 RVA: 0x00145C3B File Offset: 0x00143E3B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006449 RID: 25673 RVA: 0x00145C50 File Offset: 0x00143E50
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600644A RID: 25674 RVA: 0x00145C7A File Offset: 0x00143E7A
		public bool Equals(aboveOrLeftmost other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C11 RID: 11281
		private ProgramNode _node;
	}
}
