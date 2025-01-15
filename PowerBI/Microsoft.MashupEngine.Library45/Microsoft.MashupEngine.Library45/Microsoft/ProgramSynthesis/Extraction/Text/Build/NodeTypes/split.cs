using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes
{
	// Token: 0x02000F3C RID: 3900
	public struct split : IProgramNodeBuilder, IEquatable<split>
	{
		// Token: 0x1700135D RID: 4957
		// (get) Token: 0x06006C57 RID: 27735 RVA: 0x00162BB2 File Offset: 0x00160DB2
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06006C58 RID: 27736 RVA: 0x00162BBA File Offset: 0x00160DBA
		private split(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06006C59 RID: 27737 RVA: 0x00162BC3 File Offset: 0x00160DC3
		public static split CreateUnsafe(ProgramNode node)
		{
			return new split(node);
		}

		// Token: 0x06006C5A RID: 27738 RVA: 0x00162BCC File Offset: 0x00160DCC
		public static split? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.split)
			{
				return null;
			}
			return new split?(split.CreateUnsafe(node));
		}

		// Token: 0x06006C5B RID: 27739 RVA: 0x00162C06 File Offset: 0x00160E06
		public static split CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new split(new Hole(g.Symbol.split, holeId));
		}

		// Token: 0x06006C5C RID: 27740 RVA: 0x00162C1E File Offset: 0x00160E1E
		public bool Is_SplitPosition(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SplitPosition;
		}

		// Token: 0x06006C5D RID: 27741 RVA: 0x00162C38 File Offset: 0x00160E38
		public bool Is_SplitPosition(GrammarBuilders g, out SplitPosition value)
		{
			if (this.Node.GrammarRule == g.Rule.SplitPosition)
			{
				value = SplitPosition.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SplitPosition);
			return false;
		}

		// Token: 0x06006C5E RID: 27742 RVA: 0x00162C70 File Offset: 0x00160E70
		public SplitPosition? As_SplitPosition(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SplitPosition)
			{
				return null;
			}
			return new SplitPosition?(SplitPosition.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C5F RID: 27743 RVA: 0x00162CB0 File Offset: 0x00160EB0
		public SplitPosition Cast_SplitPosition(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SplitPosition)
			{
				return SplitPosition.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SplitPosition is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C60 RID: 27744 RVA: 0x00162D05 File Offset: 0x00160F05
		public bool Is_SplitDelimiter(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SplitDelimiter;
		}

		// Token: 0x06006C61 RID: 27745 RVA: 0x00162D1F File Offset: 0x00160F1F
		public bool Is_SplitDelimiter(GrammarBuilders g, out SplitDelimiter value)
		{
			if (this.Node.GrammarRule == g.Rule.SplitDelimiter)
			{
				value = SplitDelimiter.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SplitDelimiter);
			return false;
		}

		// Token: 0x06006C62 RID: 27746 RVA: 0x00162D54 File Offset: 0x00160F54
		public SplitDelimiter? As_SplitDelimiter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SplitDelimiter)
			{
				return null;
			}
			return new SplitDelimiter?(SplitDelimiter.CreateUnsafe(this.Node));
		}

		// Token: 0x06006C63 RID: 27747 RVA: 0x00162D94 File Offset: 0x00160F94
		public SplitDelimiter Cast_SplitDelimiter(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SplitDelimiter)
			{
				return SplitDelimiter.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SplitDelimiter is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06006C64 RID: 27748 RVA: 0x00162DEC File Offset: 0x00160FEC
		public T Switch<T>(GrammarBuilders g, Func<SplitPosition, T> func0, Func<SplitDelimiter, T> func1)
		{
			SplitPosition splitPosition;
			if (this.Is_SplitPosition(g, out splitPosition))
			{
				return func0(splitPosition);
			}
			SplitDelimiter splitDelimiter;
			if (this.Is_SplitDelimiter(g, out splitDelimiter))
			{
				return func1(splitDelimiter);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol split");
		}

		// Token: 0x06006C65 RID: 27749 RVA: 0x00162E44 File Offset: 0x00161044
		public void Switch(GrammarBuilders g, Action<SplitPosition> func0, Action<SplitDelimiter> func1)
		{
			SplitPosition splitPosition;
			if (this.Is_SplitPosition(g, out splitPosition))
			{
				func0(splitPosition);
				return;
			}
			SplitDelimiter splitDelimiter;
			if (this.Is_SplitDelimiter(g, out splitDelimiter))
			{
				func1(splitDelimiter);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol split");
		}

		// Token: 0x06006C66 RID: 27750 RVA: 0x00162E9B File Offset: 0x0016109B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06006C67 RID: 27751 RVA: 0x00162EB0 File Offset: 0x001610B0
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06006C68 RID: 27752 RVA: 0x00162EDA File Offset: 0x001610DA
		public bool Equals(split other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002F27 RID: 12071
		private ProgramNode _node;
	}
}
