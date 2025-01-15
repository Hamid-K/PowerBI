using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011B4 RID: 4532
	internal sealed class AggregateFunctionCall : SqlExpression
	{
		// Token: 0x170020BD RID: 8381
		// (get) Token: 0x060077D1 RID: 30673 RVA: 0x001A0366 File Offset: 0x0019E566
		// (set) Token: 0x060077D2 RID: 30674 RVA: 0x001A036E File Offset: 0x0019E56E
		private ConstantSqlString AggregateType { get; set; }

		// Token: 0x170020BE RID: 8382
		// (get) Token: 0x060077D3 RID: 30675 RVA: 0x001A0377 File Offset: 0x0019E577
		// (set) Token: 0x060077D4 RID: 30676 RVA: 0x001A037F File Offset: 0x0019E57F
		private SqlExpression Expression { get; set; }

		// Token: 0x170020BF RID: 8383
		// (get) Token: 0x060077D5 RID: 30677 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060077D6 RID: 30678 RVA: 0x001A0388 File Offset: 0x0019E588
		private AggregateFunctionCall()
		{
		}

		// Token: 0x060077D7 RID: 30679 RVA: 0x001A0390 File Offset: 0x0019E590
		public static AggregateFunctionCall Average(SqlExpression expression)
		{
			return new AggregateFunctionCall
			{
				AggregateType = SqlLanguageStrings.AvgSqlString,
				Expression = expression
			};
		}

		// Token: 0x060077D8 RID: 30680 RVA: 0x001A03A9 File Offset: 0x0019E5A9
		public static AggregateFunctionCall Count(SqlExpression expression)
		{
			return new AggregateFunctionCall
			{
				AggregateType = SqlLanguageStrings.CountSqlString,
				Expression = expression
			};
		}

		// Token: 0x060077D9 RID: 30681 RVA: 0x001A03C2 File Offset: 0x0019E5C2
		public static AggregateFunctionCall CountBig(SqlExpression expression)
		{
			return new AggregateFunctionCall
			{
				AggregateType = SqlLanguageStrings.CountBigSqlString,
				Expression = expression
			};
		}

		// Token: 0x060077DA RID: 30682 RVA: 0x001A03DB File Offset: 0x0019E5DB
		public static AggregateFunctionCall Max(SqlExpression expression)
		{
			return new AggregateFunctionCall
			{
				AggregateType = SqlLanguageStrings.MaxSqlString,
				Expression = expression
			};
		}

		// Token: 0x060077DB RID: 30683 RVA: 0x001A03F4 File Offset: 0x0019E5F4
		public static AggregateFunctionCall Min(SqlExpression expression)
		{
			return new AggregateFunctionCall
			{
				AggregateType = SqlLanguageStrings.MinSqlString,
				Expression = expression
			};
		}

		// Token: 0x060077DC RID: 30684 RVA: 0x001A040D File Offset: 0x0019E60D
		public static AggregateFunctionCall StandardDeviation(SqlExpression expression)
		{
			return AggregateFunctionCall.StandardDeviation(expression, SqlLanguageStrings.StDevSqlString);
		}

		// Token: 0x060077DD RID: 30685 RVA: 0x001A041A File Offset: 0x0019E61A
		public static AggregateFunctionCall StandardDeviation(SqlExpression expression, ConstantSqlString function)
		{
			return new AggregateFunctionCall
			{
				AggregateType = function,
				Expression = expression
			};
		}

		// Token: 0x060077DE RID: 30686 RVA: 0x001A042F File Offset: 0x0019E62F
		public static AggregateFunctionCall Sum(SqlExpression expression)
		{
			return new AggregateFunctionCall
			{
				AggregateType = SqlLanguageStrings.SumSqlString,
				Expression = expression
			};
		}

		// Token: 0x060077DF RID: 30687 RVA: 0x001A0448 File Offset: 0x0019E648
		public static AggregateFunctionCall Distinct(SqlExpression expression)
		{
			return new AggregateFunctionCall
			{
				AggregateType = SqlLanguageStrings.DistinctSqlString,
				Expression = expression
			};
		}

		// Token: 0x060077E0 RID: 30688 RVA: 0x001A0461 File Offset: 0x0019E661
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.Write(this.AggregateType);
			writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
			this.Expression.WriteCreateScript(writer);
			writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
		}
	}
}
