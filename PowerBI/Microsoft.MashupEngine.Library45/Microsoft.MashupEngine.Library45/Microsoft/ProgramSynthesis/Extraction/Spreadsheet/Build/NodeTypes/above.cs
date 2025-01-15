using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes
{
	// Token: 0x02000E6C RID: 3692
	public struct above : IProgramNodeBuilder, IEquatable<above>
	{
		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x0600648D RID: 25741 RVA: 0x00146732 File Offset: 0x00144932
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x0014673A File Offset: 0x0014493A
		private above(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600648F RID: 25743 RVA: 0x00146743 File Offset: 0x00144943
		public static above CreateUnsafe(ProgramNode node)
		{
			return new above(node);
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x0014674C File Offset: 0x0014494C
		public static above? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.above)
			{
				return null;
			}
			return new above?(above.CreateUnsafe(node));
		}

		// Token: 0x06006491 RID: 25745 RVA: 0x00146786 File Offset: 0x00144986
		public static above CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new above(new Hole(g.Symbol.above, holeId));
		}

		// Token: 0x06006492 RID: 25746 RVA: 0x0014679E File Offset: 0x0014499E
		public bool Is_TitleCellsAbove(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TitleCellsAbove;
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x001467B8 File Offset: 0x001449B8
		public bool Is_TitleCellsAbove(GrammarBuilders g, out TitleCellsAbove value)
		{
			if (this.Node.GrammarRule == g.Rule.TitleCellsAbove)
			{
				value = TitleCellsAbove.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TitleCellsAbove);
			return false;
		}

		// Token: 0x06006494 RID: 25748 RVA: 0x001467F0 File Offset: 0x001449F0
		public TitleCellsAbove? As_TitleCellsAbove(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TitleCellsAbove)
			{
				return null;
			}
			return new TitleCellsAbove?(TitleCellsAbove.CreateUnsafe(this.Node));
		}

		// Token: 0x06006495 RID: 25749 RVA: 0x00146830 File Offset: 0x00144A30
		public TitleCellsAbove Cast_TitleCellsAbove(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TitleCellsAbove)
			{
				return TitleCellsAbove.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TitleCellsAbove is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x00146885 File Offset: 0x00144A85
		public bool Is_TitleCellsAboveMatching(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TitleCellsAboveMatching;
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x0014689F File Offset: 0x00144A9F
		public bool Is_TitleCellsAboveMatching(GrammarBuilders g, out TitleCellsAboveMatching value)
		{
			if (this.Node.GrammarRule == g.Rule.TitleCellsAboveMatching)
			{
				value = TitleCellsAboveMatching.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TitleCellsAboveMatching);
			return false;
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x001468D4 File Offset: 0x00144AD4
		public TitleCellsAboveMatching? As_TitleCellsAboveMatching(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TitleCellsAboveMatching)
			{
				return null;
			}
			return new TitleCellsAboveMatching?(TitleCellsAboveMatching.CreateUnsafe(this.Node));
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x00146914 File Offset: 0x00144B14
		public TitleCellsAboveMatching Cast_TitleCellsAboveMatching(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TitleCellsAboveMatching)
			{
				return TitleCellsAboveMatching.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TitleCellsAboveMatching is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x0014696C File Offset: 0x00144B6C
		public T Switch<T>(GrammarBuilders g, Func<TitleCellsAbove, T> func0, Func<TitleCellsAboveMatching, T> func1)
		{
			TitleCellsAbove titleCellsAbove;
			if (this.Is_TitleCellsAbove(g, out titleCellsAbove))
			{
				return func0(titleCellsAbove);
			}
			TitleCellsAboveMatching titleCellsAboveMatching;
			if (this.Is_TitleCellsAboveMatching(g, out titleCellsAboveMatching))
			{
				return func1(titleCellsAboveMatching);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol above");
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x001469C4 File Offset: 0x00144BC4
		public void Switch(GrammarBuilders g, Action<TitleCellsAbove> func0, Action<TitleCellsAboveMatching> func1)
		{
			TitleCellsAbove titleCellsAbove;
			if (this.Is_TitleCellsAbove(g, out titleCellsAbove))
			{
				func0(titleCellsAbove);
				return;
			}
			TitleCellsAboveMatching titleCellsAboveMatching;
			if (this.Is_TitleCellsAboveMatching(g, out titleCellsAboveMatching))
			{
				func1(titleCellsAboveMatching);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol above");
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x00146A1B File Offset: 0x00144C1B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x00146A30 File Offset: 0x00144C30
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x00146A5A File Offset: 0x00144C5A
		public bool Equals(above other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002C16 RID: 11286
		private ProgramNode _node;
	}
}
