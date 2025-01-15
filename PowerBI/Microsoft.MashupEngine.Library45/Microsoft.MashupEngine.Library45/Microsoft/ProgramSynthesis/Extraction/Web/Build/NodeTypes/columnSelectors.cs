using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001083 RID: 4227
	public struct columnSelectors : IProgramNodeBuilder, IEquatable<columnSelectors>
	{
		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06007EEB RID: 32491 RVA: 0x001AACB6 File Offset: 0x001A8EB6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007EEC RID: 32492 RVA: 0x001AACBE File Offset: 0x001A8EBE
		private columnSelectors(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007EED RID: 32493 RVA: 0x001AACC7 File Offset: 0x001A8EC7
		public static columnSelectors CreateUnsafe(ProgramNode node)
		{
			return new columnSelectors(node);
		}

		// Token: 0x06007EEE RID: 32494 RVA: 0x001AACD0 File Offset: 0x001A8ED0
		public static columnSelectors? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.columnSelectors)
			{
				return null;
			}
			return new columnSelectors?(columnSelectors.CreateUnsafe(node));
		}

		// Token: 0x06007EEF RID: 32495 RVA: 0x001AAD0A File Offset: 0x001A8F0A
		public static columnSelectors CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new columnSelectors(new Hole(g.Symbol.columnSelectors, holeId));
		}

		// Token: 0x06007EF0 RID: 32496 RVA: 0x001AAD22 File Offset: 0x001A8F22
		public bool Is_SingleColumn(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.SingleColumn;
		}

		// Token: 0x06007EF1 RID: 32497 RVA: 0x001AAD3C File Offset: 0x001A8F3C
		public bool Is_SingleColumn(GrammarBuilders g, out SingleColumn value)
		{
			if (this.Node.GrammarRule == g.Rule.SingleColumn)
			{
				value = SingleColumn.CreateUnsafe(this.Node);
				return true;
			}
			value = default(SingleColumn);
			return false;
		}

		// Token: 0x06007EF2 RID: 32498 RVA: 0x001AAD74 File Offset: 0x001A8F74
		public SingleColumn? As_SingleColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.SingleColumn)
			{
				return null;
			}
			return new SingleColumn?(SingleColumn.CreateUnsafe(this.Node));
		}

		// Token: 0x06007EF3 RID: 32499 RVA: 0x001AADB4 File Offset: 0x001A8FB4
		public SingleColumn Cast_SingleColumn(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.SingleColumn)
			{
				return SingleColumn.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_SingleColumn is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007EF4 RID: 32500 RVA: 0x001AAE09 File Offset: 0x001A9009
		public bool Is_ColumnSequence(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ColumnSequence;
		}

		// Token: 0x06007EF5 RID: 32501 RVA: 0x001AAE23 File Offset: 0x001A9023
		public bool Is_ColumnSequence(GrammarBuilders g, out ColumnSequence value)
		{
			if (this.Node.GrammarRule == g.Rule.ColumnSequence)
			{
				value = ColumnSequence.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ColumnSequence);
			return false;
		}

		// Token: 0x06007EF6 RID: 32502 RVA: 0x001AAE58 File Offset: 0x001A9058
		public ColumnSequence? As_ColumnSequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ColumnSequence)
			{
				return null;
			}
			return new ColumnSequence?(ColumnSequence.CreateUnsafe(this.Node));
		}

		// Token: 0x06007EF7 RID: 32503 RVA: 0x001AAE98 File Offset: 0x001A9098
		public ColumnSequence Cast_ColumnSequence(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ColumnSequence)
			{
				return ColumnSequence.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ColumnSequence is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007EF8 RID: 32504 RVA: 0x001AAEF0 File Offset: 0x001A90F0
		public T Switch<T>(GrammarBuilders g, Func<SingleColumn, T> func0, Func<ColumnSequence, T> func1)
		{
			SingleColumn singleColumn;
			if (this.Is_SingleColumn(g, out singleColumn))
			{
				return func0(singleColumn);
			}
			ColumnSequence columnSequence;
			if (this.Is_ColumnSequence(g, out columnSequence))
			{
				return func1(columnSequence);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol columnSelectors");
		}

		// Token: 0x06007EF9 RID: 32505 RVA: 0x001AAF48 File Offset: 0x001A9148
		public void Switch(GrammarBuilders g, Action<SingleColumn> func0, Action<ColumnSequence> func1)
		{
			SingleColumn singleColumn;
			if (this.Is_SingleColumn(g, out singleColumn))
			{
				func0(singleColumn);
				return;
			}
			ColumnSequence columnSequence;
			if (this.Is_ColumnSequence(g, out columnSequence))
			{
				func1(columnSequence);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol columnSelectors");
		}

		// Token: 0x06007EFA RID: 32506 RVA: 0x001AAF9F File Offset: 0x001A919F
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007EFB RID: 32507 RVA: 0x001AAFB4 File Offset: 0x001A91B4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007EFC RID: 32508 RVA: 0x001AAFDE File Offset: 0x001A91DE
		public bool Equals(columnSelectors other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x0400339C RID: 13212
		private ProgramNode _node;
	}
}
