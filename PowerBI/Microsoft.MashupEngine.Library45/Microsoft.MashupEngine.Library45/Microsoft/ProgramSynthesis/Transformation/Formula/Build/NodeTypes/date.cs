using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes
{
	// Token: 0x020015B7 RID: 5559
	public struct date : IProgramNodeBuilder, IEquatable<date>
	{
		// Token: 0x17001FDD RID: 8157
		// (get) Token: 0x0600B7BA RID: 47034 RVA: 0x0027D742 File Offset: 0x0027B942
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B7BB RID: 47035 RVA: 0x0027D74A File Offset: 0x0027B94A
		private date(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B7BC RID: 47036 RVA: 0x0027D753 File Offset: 0x0027B953
		public static date CreateUnsafe(ProgramNode node)
		{
			return new date(node);
		}

		// Token: 0x0600B7BD RID: 47037 RVA: 0x0027D75C File Offset: 0x0027B95C
		public static date? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.date)
			{
				return null;
			}
			return new date?(date.CreateUnsafe(node));
		}

		// Token: 0x0600B7BE RID: 47038 RVA: 0x0027D796 File Offset: 0x0027B996
		public static date CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new date(new Hole(g.Symbol.date, holeId));
		}

		// Token: 0x0600B7BF RID: 47039 RVA: 0x0027D7AE File Offset: 0x0027B9AE
		public bool Is_date_idate(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.date_idate;
		}

		// Token: 0x0600B7C0 RID: 47040 RVA: 0x0027D7C8 File Offset: 0x0027B9C8
		public bool Is_date_idate(GrammarBuilders g, out date_idate value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.date_idate)
			{
				value = date_idate.CreateUnsafe(this.Node);
				return true;
			}
			value = default(date_idate);
			return false;
		}

		// Token: 0x0600B7C1 RID: 47041 RVA: 0x0027D800 File Offset: 0x0027BA00
		public date_idate? As_date_idate(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.date_idate)
			{
				return null;
			}
			return new date_idate?(date_idate.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7C2 RID: 47042 RVA: 0x0027D840 File Offset: 0x0027BA40
		public date_idate Cast_date_idate(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.date_idate)
			{
				return date_idate.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_date_idate is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B7C3 RID: 47043 RVA: 0x0027D895 File Offset: 0x0027BA95
		public bool Is_RoundDateTime(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.RoundDateTime;
		}

		// Token: 0x0600B7C4 RID: 47044 RVA: 0x0027D8AF File Offset: 0x0027BAAF
		public bool Is_RoundDateTime(GrammarBuilders g, out RoundDateTime value)
		{
			if (this.Node.GrammarRule == g.Rule.RoundDateTime)
			{
				value = RoundDateTime.CreateUnsafe(this.Node);
				return true;
			}
			value = default(RoundDateTime);
			return false;
		}

		// Token: 0x0600B7C5 RID: 47045 RVA: 0x0027D8E4 File Offset: 0x0027BAE4
		public RoundDateTime? As_RoundDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.RoundDateTime)
			{
				return null;
			}
			return new RoundDateTime?(RoundDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600B7C6 RID: 47046 RVA: 0x0027D924 File Offset: 0x0027BB24
		public RoundDateTime Cast_RoundDateTime(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.RoundDateTime)
			{
				return RoundDateTime.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_RoundDateTime is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x0600B7C7 RID: 47047 RVA: 0x0027D97C File Offset: 0x0027BB7C
		public T Switch<T>(GrammarBuilders g, Func<date_idate, T> func0, Func<RoundDateTime, T> func1)
		{
			date_idate date_idate;
			if (this.Is_date_idate(g, out date_idate))
			{
				return func0(date_idate);
			}
			RoundDateTime roundDateTime;
			if (this.Is_RoundDateTime(g, out roundDateTime))
			{
				return func1(roundDateTime);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol date");
		}

		// Token: 0x0600B7C8 RID: 47048 RVA: 0x0027D9D4 File Offset: 0x0027BBD4
		public void Switch(GrammarBuilders g, Action<date_idate> func0, Action<RoundDateTime> func1)
		{
			date_idate date_idate;
			if (this.Is_date_idate(g, out date_idate))
			{
				func0(date_idate);
				return;
			}
			RoundDateTime roundDateTime;
			if (this.Is_RoundDateTime(g, out roundDateTime))
			{
				func1(roundDateTime);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol date");
		}

		// Token: 0x0600B7C9 RID: 47049 RVA: 0x0027DA2B File Offset: 0x0027BC2B
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B7CA RID: 47050 RVA: 0x0027DA40 File Offset: 0x0027BC40
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B7CB RID: 47051 RVA: 0x0027DA6A File Offset: 0x0027BC6A
		public bool Equals(date other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04004665 RID: 18021
		private ProgramNode _node;
	}
}
