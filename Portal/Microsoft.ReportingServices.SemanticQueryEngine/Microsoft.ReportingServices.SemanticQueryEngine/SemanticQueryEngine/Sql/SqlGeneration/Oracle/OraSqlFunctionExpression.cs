using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Oracle
{
	// Token: 0x02000045 RID: 69
	internal class OraSqlFunctionExpression : SqlFunctionExpression
	{
		// Token: 0x06000308 RID: 776 RVA: 0x0000E836 File Offset: 0x0000CA36
		internal OraSqlFunctionExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments, OraSqlBatch sqlBatch)
			: base(qpInfo, functionContext, arguments, OraSqlFunctionExpression.IsFunctionNullable(qpInfo, arguments), sqlBatch)
		{
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000E84A File Offset: 0x0000CA4A
		protected override ISqlSnippet CreateBasicSqlSnippetForMod(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ModOpenParenSnippet,
				arg1,
				SqlExpression.CommaSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000E877 File Offset: 0x0000CA77
		protected override ISqlSnippet CreateBasicSqlSnippetForTruncate(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.TruncOpenParenSnippet,
				arg1,
				SqlExpression.CommaSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000E8A4 File Offset: 0x0000CAA4
		protected override ISqlSnippet CreateBasicSqlSnippetForInteger(ISqlSnippet argument, DataType argDataType)
		{
			if (argDataType == DataType.Decimal || argDataType == DataType.Float)
			{
				argument = base.CreateSqlSnippetForTruncate(argument, SqlExpression.ZeroSnippet);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				OraSqlFunctionExpression.AsIntCloseParenSnippet
			});
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000E8DB File Offset: 0x0000CADB
		protected override ISqlSnippet CreateBasicSqlSnippetForString(ISqlSnippet argument, DataType argDataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ToCharOpenParenSnippet,
				argument,
				OraSqlFunctionExpression.CommaTMCommaNlsNumCharsDotCommaCloseParenSnippet
			});
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		protected override ISqlSnippet CreateBasicSqlSnippetForLength(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.LengthOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000E91D File Offset: 0x0000CB1D
		protected override ISqlSnippet CreateBasicSqlSnippetForFind(ISqlSnippet searchIn, ISqlSnippet searchFor)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.InstrOpenParenSnippet,
				searchIn,
				SqlExpression.CommaSnippet,
				searchFor,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000E94A File Offset: 0x0000CB4A
		protected override ISqlSnippet CreateBasicSqlSnippetForSubstring(ISqlSnippet source, ISqlSnippet start, ISqlSnippet length)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.SubstrOpenParenSnippet,
				source,
				SqlExpression.CommaSnippet,
				start,
				SqlExpression.CommaSnippet,
				length,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000E983 File Offset: 0x0000CB83
		protected override ISqlSnippet CreateBasicSqlSnippetForLeft(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.SubstrOpenParenSnippet,
				arg1,
				SqlExpression.CommaSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.CommaSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		protected override ISqlSnippet CreateBasicSqlSnippetForRight(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			ISqlSnippet sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				OraSqlFunctionExpression.LengthOpenParenSnippet,
				arg1,
				SqlExpression.CloseParenSnippet,
				SqlExpression.MinusSnippet,
				arg2,
				SqlExpression.PlusSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.CloseParenSnippet
			});
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.SubstrOpenParenSnippet,
				arg1,
				SqlExpression.CommaSnippet,
				SqlExpression.CaseWhenSnippet,
				sqlSnippet,
				SqlExpression.GreaterThanSnippet,
				SqlExpression.ZeroSnippet,
				SqlExpression.ThenSnippet,
				sqlSnippet,
				SqlExpression.ElseSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.EndSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000EA86 File Offset: 0x0000CC86
		protected override ISqlSnippet CreateBasicSqlSnippetForConcat(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				arg1,
				OraSqlFunctionExpression.ConcatSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000EAB3 File Offset: 0x0000CCB3
		protected override ISqlSnippet ConvertNullToEmptyString(Expression expression, SqlExpression stringExpression)
		{
			return stringExpression;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000EAB6 File Offset: 0x0000CCB6
		protected override ISqlSnippet CreateBasicSqlSnippetForDate(ISqlSnippet argument)
		{
			return this.CreateSqlSnippetForTruncCastDate(argument, OraSqlFunctionExpression.StrDDDSnippet);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000EAC4 File Offset: 0x0000CCC4
		protected override ISqlSnippet CreateBasicSqlSnippetForDate(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType, ISqlSnippet arg3, DataType arg3DataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ToTimeStampOpenParenSnippet,
				base.CreateSqlSnippetForString(arg1, arg1DataType),
				OraSqlFunctionExpression.ConcatSnippet,
				OraSqlFunctionExpression.StrDashSnippet,
				OraSqlFunctionExpression.ConcatSnippet,
				base.CreateSqlSnippetForString(arg2, arg2DataType),
				OraSqlFunctionExpression.ConcatSnippet,
				OraSqlFunctionExpression.StrDashSnippet,
				OraSqlFunctionExpression.ConcatSnippet,
				base.CreateSqlSnippetForString(arg3, arg3DataType),
				SqlExpression.CommaSnippet,
				OraSqlFunctionExpression.StrYYYYMMDDSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000EB58 File Offset: 0x0000CD58
		protected override ISqlSnippet CreateBasicSqlSnippetForDateTime(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType, ISqlSnippet arg3, DataType arg3DataType, ISqlSnippet arg4, DataType arg4DataType, ISqlSnippet arg5, DataType arg5DataType, ISqlSnippet arg6, DataType arg6DataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ToTimeStampOpenParenSnippet,
				base.CreateSqlSnippetForString(arg1, arg1DataType),
				OraSqlFunctionExpression.ConcatSnippet,
				OraSqlFunctionExpression.StrDashSnippet,
				OraSqlFunctionExpression.ConcatSnippet,
				base.CreateSqlSnippetForString(arg2, arg2DataType),
				OraSqlFunctionExpression.ConcatSnippet,
				OraSqlFunctionExpression.StrDashSnippet,
				OraSqlFunctionExpression.ConcatSnippet,
				base.CreateSqlSnippetForString(arg3, arg3DataType),
				OraSqlFunctionExpression.ConcatSnippet,
				OraSqlFunctionExpression.StrDashSnippet,
				OraSqlFunctionExpression.ConcatSnippet,
				base.CreateSqlSnippetForString(arg4, arg4DataType),
				OraSqlFunctionExpression.ConcatSnippet,
				OraSqlFunctionExpression.StrDashSnippet,
				OraSqlFunctionExpression.ConcatSnippet,
				base.CreateSqlSnippetForString(arg5, arg5DataType),
				OraSqlFunctionExpression.ConcatSnippet,
				OraSqlFunctionExpression.StrDashSnippet,
				OraSqlFunctionExpression.ConcatSnippet,
				base.CreateSqlSnippetForString(arg6, arg6DataType),
				SqlExpression.CommaSnippet,
				OraSqlFunctionExpression.StrYYYYMMDDHHMISSFFSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00005C3C File Offset: 0x00003E3C
		protected override ISqlSnippet CreateBasicSqlSnippetForTime(ISqlSnippet argument)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000EC64 File Offset: 0x0000CE64
		protected override ISqlSnippet CreateBasicSqlSnippetForYear(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ExtractOpenParenSnippet,
				OraSqlFunctionExpression.YearFromSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000EC8D File Offset: 0x0000CE8D
		protected override ISqlSnippet CreateBasicSqlSnippetForQuarter(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.CeilOpenParenSnippet,
				this.CreateBasicSqlSnippetForMonth(argument),
				OraSqlFunctionExpression.DivideBy3CloseParenSnippet
			});
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		protected override ISqlSnippet CreateBasicSqlSnippetForMonth(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ExtractOpenParenSnippet,
				OraSqlFunctionExpression.MonthFromSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000ECDD File Offset: 0x0000CEDD
		protected override ISqlSnippet CreateBasicSqlSnippetForDay(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ExtractOpenParenSnippet,
				OraSqlFunctionExpression.DayFromSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000ED06 File Offset: 0x0000CF06
		protected override ISqlSnippet CreateBasicSqlSnippetForHour(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ExtractOpenParenSnippet,
				OraSqlFunctionExpression.HourFromSnippet,
				SqlExpression.CastOpenParenSnippet,
				argument,
				OraSqlFunctionExpression.AsTimestampCloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000ED3F File Offset: 0x0000CF3F
		protected override ISqlSnippet CreateBasicSqlSnippetForMinute(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ExtractOpenParenSnippet,
				OraSqlFunctionExpression.MinuteFromSnippet,
				SqlExpression.CastOpenParenSnippet,
				argument,
				OraSqlFunctionExpression.AsTimestampCloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000ED78 File Offset: 0x0000CF78
		protected override ISqlSnippet CreateBasicSqlSnippetForSecond(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.ExtractOpenParenSnippet,
				OraSqlFunctionExpression.SecondFromSnippet,
				SqlExpression.CastOpenParenSnippet,
				argument,
				OraSqlFunctionExpression.AsTimestampCloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		protected override ISqlSnippet CreateBasicSqlSnippetForDayOfYear(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				this.CreateSqlSnippetForTruncCastDate(argument, OraSqlFunctionExpression.StrDDDSnippet),
				SqlExpression.MinusSnippet,
				this.CreateSqlSnippetForTruncCastDate(argument, OraSqlFunctionExpression.StrYearSnippet),
				SqlExpression.PlusSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000EE14 File Offset: 0x0000D014
		protected override ISqlSnippet CreateBasicSqlSnippetForWeek(ISqlSnippet argument)
		{
			ISqlSnippet sqlSnippet = this.CreateBasicSqlSnippetForDayOfYear(argument);
			ISqlSnippet sqlSnippet2 = this.CreateSqlSnippetForDayOfWeek(this.CreateSqlSnippetForTruncCastDate(argument, OraSqlFunctionExpression.StrYearSnippet), this.GetFirstDayOfWeekSnippet());
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.CeilOpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				sqlSnippet,
				SqlExpression.PlusSnippet,
				sqlSnippet2,
				SqlExpression.MinusSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.CloseParenSnippet,
				SqlExpression.DivideBy7CloseParenSnippet
			});
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000EE8E File Offset: 0x0000D08E
		protected override ISqlSnippet CreateBasicSqlSnippetForDayOfWeek(ISqlSnippet argument)
		{
			return this.CreateSqlSnippetForDayOfWeek(argument, OraSqlFunctionExpression.StrMondaySnippet);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000EE9C File Offset: 0x0000D09C
		protected override ISqlSnippet CreateBasicSqlSnippetForDateDiff(SqlFunctionExpression.DatePart datePart, ISqlSnippet startDate, ISqlSnippet endDate)
		{
			ISqlSnippet sqlSnippet;
			switch (datePart)
			{
			case SqlFunctionExpression.DatePart.Year:
				sqlSnippet = this.CreateBasicSqlSnippetForDateDiffYear(startDate, endDate);
				break;
			case SqlFunctionExpression.DatePart.Quarter:
				sqlSnippet = this.CreateBasicSqlSnippetForDateDiffQuarter(startDate, endDate);
				break;
			case SqlFunctionExpression.DatePart.Month:
				sqlSnippet = this.CreateBasicSqlSnippetForDateDiffMonth(startDate, endDate);
				break;
			case SqlFunctionExpression.DatePart.Day:
				sqlSnippet = this.CreateBasicSqlSnippetForDateDiffDay(startDate, endDate);
				break;
			case SqlFunctionExpression.DatePart.Hour:
				sqlSnippet = this.CreateBasicSqlSnippetForDateDiffHour(startDate, endDate);
				break;
			case SqlFunctionExpression.DatePart.Minute:
				sqlSnippet = this.CreateBasicSqlSnippetForDateDiffMinute(startDate, endDate);
				break;
			case SqlFunctionExpression.DatePart.Second:
				sqlSnippet = this.CreateBasicSqlSnippetForDateDiffSecond(startDate, endDate);
				break;
			case SqlFunctionExpression.DatePart.Week:
				sqlSnippet = this.CreateBasicSqlSnippetForDateDiffWeek(startDate, endDate);
				break;
			default:
				throw SQEAssert.AssertFalseAndThrow("Unknown date part: {0}.", new object[] { datePart.ToString() });
			}
			return SqlFunctionExpression.CastAsDecimal(sqlSnippet);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000EF50 File Offset: 0x0000D150
		protected override ISqlSnippet CreateBasicSqlSnippetForDateAdd(SqlFunctionExpression.DatePart datePart, ISqlSnippet delta, ISqlSnippet date)
		{
			switch (datePart)
			{
			case SqlFunctionExpression.DatePart.Year:
				return this.CreateBasicSqlSnippetForDateAddYear(date, delta);
			case SqlFunctionExpression.DatePart.Quarter:
				return this.CreateBasicSqlSnippetForDateAddQuarter(date, delta);
			case SqlFunctionExpression.DatePart.Month:
				return this.CreateBasicSqlSnippetForDateAddMonth(date, delta);
			case SqlFunctionExpression.DatePart.Day:
				return this.CreateBasicSqlSnippetForDateAddDay(date, delta);
			case SqlFunctionExpression.DatePart.Hour:
				return this.CreateBasicSqlSnippetForDateAddHour(date, delta);
			case SqlFunctionExpression.DatePart.Minute:
				return this.CreateBasicSqlSnippetForDateAddMinute(date, delta);
			case SqlFunctionExpression.DatePart.Second:
				return this.CreateBasicSqlSnippetForDateAddSecond(date, delta);
			case SqlFunctionExpression.DatePart.Week:
				return this.CreateBasicSqlSnippetForDateAddWeek(date, delta);
			default:
				throw SQEAssert.AssertFalseAndThrow("Unknown date part: {0}.", new object[] { datePart.ToString() });
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000EFF0 File Offset: 0x0000D1F0
		private ISqlSnippet CreateSqlSnippetForTruncCastDate(ISqlSnippet argument, ISqlSnippet truncTo)
		{
			ISqlSnippet sqlSnippet;
			if (this.SqlBatch.EnableTSTruncation)
			{
				sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CastOpenParenSnippet,
					SqlExpression.CastOpenParenSnippet,
					argument,
					OraSqlFunctionExpression.AsTimestampCloseParenSnippet,
					SqlExpression.MinusSnippet,
					OraSqlFunctionExpression.NumToDSPoint5SecondSnippet,
					OraSqlFunctionExpression.AsDateCloseParenSnippet
				});
			}
			else
			{
				sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CastOpenParenSnippet,
					argument,
					OraSqlFunctionExpression.AsDateCloseParenSnippet
				});
			}
			if (truncTo == OraSqlFunctionExpression.StrSecondSnippet)
			{
				return sqlSnippet;
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.TruncOpenParenSnippet,
				sqlSnippet,
				SqlExpression.CommaSnippet,
				truncTo,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000F0A4 File Offset: 0x0000D2A4
		private ISqlSnippet CreateSqlSnippetForDayOfWeek(ISqlSnippet argument, ISqlSnippet firstDayOfWeek)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				OraSqlFunctionExpression.ModOpenParenSnippet,
				OraSqlFunctionExpression.ToCharOpenParenSnippet,
				this.CreateSqlSnippetForTruncCastDate(argument, OraSqlFunctionExpression.StrDDDSnippet),
				SqlExpression.CommaSnippet,
				OraSqlFunctionExpression.StrDSnippet,
				SqlExpression.CloseParenSnippet,
				SqlExpression.MinusSnippet,
				OraSqlFunctionExpression.ToCharOpenParenSnippet,
				firstDayOfWeek,
				SqlExpression.CommaSnippet,
				OraSqlFunctionExpression.StrDSnippet,
				SqlExpression.CloseParenSnippet,
				SqlExpression.PlusSnippet,
				SqlExpression.SevenSnippet,
				SqlExpression.CommaSnippet,
				SqlExpression.SevenSnippet,
				SqlExpression.CloseParenSnippet,
				SqlExpression.PlusSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000F174 File Offset: 0x0000D374
		private ISqlSnippet GetFirstDayOfWeekSnippet()
		{
			switch (this.SqlBatch.FirstDayOfWeek)
			{
			case DayOfWeek.Sunday:
				return OraSqlFunctionExpression.StrSundaySnippet;
			case DayOfWeek.Monday:
				return OraSqlFunctionExpression.StrMondaySnippet;
			case DayOfWeek.Tuesday:
				return OraSqlFunctionExpression.StrTuesdaySnippet;
			case DayOfWeek.Wednesday:
				return OraSqlFunctionExpression.StrWednesdaySnippet;
			case DayOfWeek.Thursday:
				return OraSqlFunctionExpression.StrThursdaySnippet;
			case DayOfWeek.Friday:
				return OraSqlFunctionExpression.StrFridaySnippet;
			case DayOfWeek.Saturday:
				return OraSqlFunctionExpression.StrSaturdaySnippet;
			default:
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("this.SqlBatch.FirstDayOfWeek", "Unknown value: " + this.SqlBatch.FirstDayOfWeek.ToString()));
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000F20D File Offset: 0x0000D40D
		private ISqlSnippet CreateSqlSnippetForNumToDSInterval(ISqlSnippet argument, ISqlSnippet intervalUnit)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.NumToDSIntervalOpenParenSnippet,
				argument,
				SqlExpression.CommaSnippet,
				intervalUnit,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000F23C File Offset: 0x0000D43C
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffYear(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				OraSqlFunctionExpression.MonthsBetweenOpenParenSnippet,
				this.CreateSqlSnippetForTruncCastDate(endDate, OraSqlFunctionExpression.StrYearSnippet),
				SqlExpression.CommaSnippet,
				this.CreateSqlSnippetForTruncCastDate(startDate, OraSqlFunctionExpression.StrYearSnippet),
				SqlExpression.CloseParenSnippet,
				OraSqlFunctionExpression.DivideBy12CloseParenSnippet
			});
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000F29C File Offset: 0x0000D49C
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffQuarter(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				this.CreateBasicSqlSnippetForYear(endDate),
				OraSqlFunctionExpression.MultiplyBy4Snippet,
				SqlExpression.PlusSnippet,
				this.CreateBasicSqlSnippetForQuarter(endDate),
				SqlExpression.CloseParenSnippet,
				SqlExpression.MinusSnippet,
				SqlExpression.OpenParenSnippet,
				this.CreateBasicSqlSnippetForYear(startDate),
				OraSqlFunctionExpression.MultiplyBy4Snippet,
				SqlExpression.PlusSnippet,
				this.CreateBasicSqlSnippetForQuarter(startDate),
				SqlExpression.CloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000F33C File Offset: 0x0000D53C
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffMonth(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				OraSqlFunctionExpression.MonthsBetweenOpenParenSnippet,
				this.CreateSqlSnippetForTruncCastDate(endDate, OraSqlFunctionExpression.StrMonthSnippet),
				SqlExpression.CommaSnippet,
				this.CreateSqlSnippetForTruncCastDate(startDate, OraSqlFunctionExpression.StrMonthSnippet),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F38A File Offset: 0x0000D58A
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffDay(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				this.CreateSqlSnippetForTruncCastDate(endDate, OraSqlFunctionExpression.StrDDDSnippet),
				SqlExpression.MinusSnippet,
				this.CreateSqlSnippetForTruncCastDate(startDate, OraSqlFunctionExpression.StrDDDSnippet)
			});
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffHour(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				this.CreateSqlSnippetForTruncCastDate(endDate, OraSqlFunctionExpression.StrHH24Snippet),
				SqlExpression.MinusSnippet,
				this.CreateSqlSnippetForTruncCastDate(startDate, OraSqlFunctionExpression.StrHH24Snippet),
				SqlExpression.CloseParenSnippet,
				OraSqlFunctionExpression.MultiplyBy24CloseParenSnippet
			});
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000F420 File Offset: 0x0000D620
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffMinute(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				this.CreateSqlSnippetForTruncCastDate(endDate, OraSqlFunctionExpression.StrMISnippet),
				SqlExpression.MinusSnippet,
				this.CreateSqlSnippetForTruncCastDate(startDate, OraSqlFunctionExpression.StrMISnippet),
				SqlExpression.CloseParenSnippet,
				OraSqlFunctionExpression.MultiplyBy1440CloseParenSnippet
			});
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000F480 File Offset: 0x0000D680
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffSecond(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			ISqlSnippet sqlSnippet = this.CreateSqlSnippetForTruncCastDate(startDate, OraSqlFunctionExpression.StrSecondSnippet);
			ISqlSnippet sqlSnippet2 = this.CreateSqlSnippetForTruncCastDate(endDate, OraSqlFunctionExpression.StrSecondSnippet);
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				sqlSnippet2,
				SqlExpression.MinusSnippet,
				sqlSnippet,
				SqlExpression.CloseParenSnippet,
				OraSqlFunctionExpression.MultiplyBy86400CloseParenSnippet
			});
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000F4E4 File Offset: 0x0000D6E4
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffWeek(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			ISqlSnippet sqlSnippet = this.CreateSqlSnippetForTruncCastDate(startDate, OraSqlFunctionExpression.StrDDDSnippet);
			ISqlSnippet sqlSnippet2 = this.CreateSqlSnippetForTruncCastDate(endDate, OraSqlFunctionExpression.StrDDDSnippet);
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CaseWhenSnippet,
				endDate,
				SqlExpression.GreaterThanSnippet,
				startDate,
				SqlExpression.ThenSnippet,
				SqlExpression.FloorOpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				sqlSnippet2,
				SqlExpression.MinusSnippet,
				sqlSnippet,
				SqlExpression.PlusSnippet,
				SqlExpression.SevenSnippet,
				SqlExpression.MinusSnippet,
				this.CreateSqlSnippetForDayOfWeek(endDate, this.GetFirstDayOfWeekSnippet()),
				SqlExpression.CloseParenSnippet,
				SqlExpression.DivideBy7CloseParenSnippet,
				SqlExpression.ElseSnippet,
				OraSqlFunctionExpression.CeilOpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				sqlSnippet2,
				SqlExpression.MinusSnippet,
				sqlSnippet,
				SqlExpression.PlusSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.MinusSnippet,
				this.CreateSqlSnippetForDayOfWeek(endDate, this.GetFirstDayOfWeekSnippet()),
				SqlExpression.CloseParenSnippet,
				SqlExpression.DivideBy7CloseParenSnippet,
				SqlExpression.EndSnippet
			});
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000F60C File Offset: 0x0000D80C
		private ISqlSnippet CreateBasicSqlSnippetForDateAddYear(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[] { this.CreateBasicSqlSnippetForDateAddMonth(date, new SqlCompositeSnippet(new ISqlSnippet[]
			{
				delta,
				OraSqlFunctionExpression.MultiplyBy12Snippet
			})) });
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000F648 File Offset: 0x0000D848
		private ISqlSnippet CreateBasicSqlSnippetForDateAddQuarter(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[] { this.CreateBasicSqlSnippetForDateAddMonth(date, new SqlCompositeSnippet(new ISqlSnippet[]
			{
				delta,
				OraSqlFunctionExpression.MultiplyBy3Snippet
			})) });
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000F684 File Offset: 0x0000D884
		private ISqlSnippet CreateBasicSqlSnippetForDateAddMonth(ISqlSnippet date, ISqlSnippet delta)
		{
			ISqlSnippet sqlSnippet = this.CreateSqlSnippetForTruncCastDate(date, OraSqlFunctionExpression.StrSecondSnippet);
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				date,
				SqlExpression.PlusSnippet,
				SqlExpression.OpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				OraSqlFunctionExpression.AddMonthsOpenParenSnippet,
				sqlSnippet,
				SqlExpression.CommaSnippet,
				delta,
				SqlExpression.CloseParenSnippet,
				OraSqlFunctionExpression.AsTimestampCloseParenSnippet,
				SqlExpression.MinusSnippet,
				SqlExpression.CastOpenParenSnippet,
				sqlSnippet,
				OraSqlFunctionExpression.AsTimestampCloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000F718 File Offset: 0x0000D918
		private ISqlSnippet CreateBasicSqlSnippetForDateAddDay(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				date,
				SqlExpression.PlusSnippet,
				this.CreateSqlSnippetForNumToDSInterval(delta, OraSqlFunctionExpression.StrDaySnippet),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000F750 File Offset: 0x0000D950
		private ISqlSnippet CreateBasicSqlSnippetForDateAddHour(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				date,
				SqlExpression.PlusSnippet,
				this.CreateSqlSnippetForNumToDSInterval(delta, OraSqlFunctionExpression.StrHourSnippet),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000F788 File Offset: 0x0000D988
		private ISqlSnippet CreateBasicSqlSnippetForDateAddMinute(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				date,
				SqlExpression.PlusSnippet,
				this.CreateSqlSnippetForNumToDSInterval(delta, OraSqlFunctionExpression.StrMinuteSnippet),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000F7C0 File Offset: 0x0000D9C0
		private ISqlSnippet CreateBasicSqlSnippetForDateAddSecond(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				date,
				SqlExpression.PlusSnippet,
				this.CreateSqlSnippetForNumToDSInterval(delta, OraSqlFunctionExpression.StrSecondSnippet),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		private ISqlSnippet CreateBasicSqlSnippetForDateAddWeek(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[] { this.CreateBasicSqlSnippetForDateAddDay(date, new SqlCompositeSnippet(new ISqlSnippet[]
			{
				delta,
				OraSqlFunctionExpression.MultiplyBy7Snippet
			})) });
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000F834 File Offset: 0x0000DA34
		private static bool IsFunctionNullable(IQPExpressionInfo qpInfo, SqlExpression[] arguments)
		{
			FunctionNode nodeAsFunction = qpInfo.Expression.NodeAsFunction;
			if (nodeAsFunction == null)
			{
				throw SQEAssert.AssertFalseAndThrow("qpInfo must be a scalar function.", Array.Empty<object>());
			}
			if (nodeAsFunction.FunctionName != FunctionName.Concat)
			{
				return qpInfo.Nullable;
			}
			if (qpInfo.Nullable)
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			for (int i = 0; i < arguments.Length; i++)
			{
				if (!arguments[i].IsNullable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000F898 File Offset: 0x0000DA98
		private new OraSqlBatch SqlBatch
		{
			[DebuggerStepThrough]
			get
			{
				return (OraSqlBatch)base.SqlBatch;
			}
		}

		// Token: 0x04000149 RID: 329
		private static readonly ISqlSnippet ModOpenParenSnippet = new SqlStringSnippet("MOD(");

		// Token: 0x0400014A RID: 330
		private static readonly ISqlSnippet LengthOpenParenSnippet = new SqlStringSnippet("LENGTH(");

		// Token: 0x0400014B RID: 331
		private static readonly ISqlSnippet InstrOpenParenSnippet = new SqlStringSnippet("INSTR(");

		// Token: 0x0400014C RID: 332
		private static readonly ISqlSnippet TruncOpenParenSnippet = new SqlStringSnippet("TRUNC(");

		// Token: 0x0400014D RID: 333
		private static readonly ISqlSnippet SubstrOpenParenSnippet = new SqlStringSnippet("SUBSTR(");

		// Token: 0x0400014E RID: 334
		internal static readonly ISqlSnippet ToTimeStampOpenParenSnippet = new SqlStringSnippet("TO_TIMESTAMP(");

		// Token: 0x0400014F RID: 335
		private static readonly ISqlSnippet ToCharOpenParenSnippet = new SqlStringSnippet("TO_CHAR(");

		// Token: 0x04000150 RID: 336
		private static readonly ISqlSnippet ExtractOpenParenSnippet = new SqlStringSnippet("EXTRACT(");

		// Token: 0x04000151 RID: 337
		private static readonly ISqlSnippet CeilOpenParenSnippet = new SqlStringSnippet("CEIL(");

		// Token: 0x04000152 RID: 338
		private static readonly ISqlSnippet MonthsBetweenOpenParenSnippet = new SqlStringSnippet("MONTHS_BETWEEN(");

		// Token: 0x04000153 RID: 339
		private static readonly ISqlSnippet AddMonthsOpenParenSnippet = new SqlStringSnippet("ADD_MONTHS(");

		// Token: 0x04000154 RID: 340
		private static readonly ISqlSnippet NumToDSIntervalOpenParenSnippet = new SqlStringSnippet("NUMTODSINTERVAL(");

		// Token: 0x04000155 RID: 341
		private static readonly ISqlSnippet AsTimestampCloseParenSnippet = new SqlStringSnippet(" AS TIMESTAMP)");

		// Token: 0x04000156 RID: 342
		private static readonly ISqlSnippet AsDateCloseParenSnippet = new SqlStringSnippet(" AS DATE)");

		// Token: 0x04000157 RID: 343
		private static readonly ISqlSnippet AsIntCloseParenSnippet = new SqlStringSnippet(" AS INTEGER)");

		// Token: 0x04000158 RID: 344
		private static readonly ISqlSnippet StrYearSnippet = new SqlStringSnippet("'YEAR'");

		// Token: 0x04000159 RID: 345
		private static readonly ISqlSnippet StrMonthSnippet = new SqlStringSnippet("'MONTH'");

		// Token: 0x0400015A RID: 346
		private static readonly ISqlSnippet StrDaySnippet = new SqlStringSnippet("'DAY'");

		// Token: 0x0400015B RID: 347
		private static readonly ISqlSnippet StrHourSnippet = new SqlStringSnippet("'HOUR'");

		// Token: 0x0400015C RID: 348
		private static readonly ISqlSnippet StrMinuteSnippet = new SqlStringSnippet("'MINUTE'");

		// Token: 0x0400015D RID: 349
		private static readonly ISqlSnippet StrSecondSnippet = new SqlStringSnippet("'SECOND'");

		// Token: 0x0400015E RID: 350
		private static readonly ISqlSnippet StrDDDSnippet = new SqlStringSnippet("'DDD'");

		// Token: 0x0400015F RID: 351
		private static readonly ISqlSnippet StrDSnippet = new SqlStringSnippet("'D'");

		// Token: 0x04000160 RID: 352
		private static readonly ISqlSnippet StrHH24Snippet = new SqlStringSnippet("'HH24'");

		// Token: 0x04000161 RID: 353
		private static readonly ISqlSnippet StrMISnippet = new SqlStringSnippet("'MI'");

		// Token: 0x04000162 RID: 354
		private static readonly ISqlSnippet StrMondaySnippet = new SqlStringSnippet("/*MONDAY*/TIMESTAMP '2007-01-01 00:00:00'");

		// Token: 0x04000163 RID: 355
		private static readonly ISqlSnippet StrTuesdaySnippet = new SqlStringSnippet("/*TUESDAY*/TIMESTAMP '2007-01-02 00:00:00'");

		// Token: 0x04000164 RID: 356
		private static readonly ISqlSnippet StrWednesdaySnippet = new SqlStringSnippet("/*WEDNESDAY*/TIMESTAMP '2007-01-03 00:00:00'");

		// Token: 0x04000165 RID: 357
		private static readonly ISqlSnippet StrThursdaySnippet = new SqlStringSnippet("/*THURSDAY*/TIMESTAMP '2007-01-04 00:00:00'");

		// Token: 0x04000166 RID: 358
		private static readonly ISqlSnippet StrFridaySnippet = new SqlStringSnippet("/*FRIDAY*/TIMESTAMP '2007-01-05 00:00:00'");

		// Token: 0x04000167 RID: 359
		private static readonly ISqlSnippet StrSaturdaySnippet = new SqlStringSnippet("/*SATURDAY*/TIMESTAMP '2007-01-06 00:00:00'");

		// Token: 0x04000168 RID: 360
		private static readonly ISqlSnippet StrSundaySnippet = new SqlStringSnippet("/*SUNDAY*/TIMESTAMP '2007-01-07 00:00:00'");

		// Token: 0x04000169 RID: 361
		private static readonly ISqlSnippet StrYYYYMMDDSnippet = new SqlStringSnippet("'YYYY-MM-DD'");

		// Token: 0x0400016A RID: 362
		internal static readonly ISqlSnippet StrYYYYMMDDHHMISSFFSnippet = new SqlStringSnippet("'YYYY-MM-DD-HH24-MI-SS.FF'");

		// Token: 0x0400016B RID: 363
		private static readonly ISqlSnippet StrDashSnippet = new SqlStringSnippet("'-'");

		// Token: 0x0400016C RID: 364
		private static readonly ISqlSnippet ConcatSnippet = new SqlStringSnippet(" || ");

		// Token: 0x0400016D RID: 365
		private static readonly ISqlSnippet YearFromSnippet = new SqlStringSnippet("YEAR FROM ");

		// Token: 0x0400016E RID: 366
		private static readonly ISqlSnippet MonthFromSnippet = new SqlStringSnippet("MONTH FROM ");

		// Token: 0x0400016F RID: 367
		private static readonly ISqlSnippet DayFromSnippet = new SqlStringSnippet("DAY FROM ");

		// Token: 0x04000170 RID: 368
		private static readonly ISqlSnippet HourFromSnippet = new SqlStringSnippet("HOUR FROM ");

		// Token: 0x04000171 RID: 369
		private static readonly ISqlSnippet MinuteFromSnippet = new SqlStringSnippet("MINUTE FROM ");

		// Token: 0x04000172 RID: 370
		private static readonly ISqlSnippet SecondFromSnippet = new SqlStringSnippet("SECOND FROM ");

		// Token: 0x04000173 RID: 371
		private static readonly ISqlSnippet CommaTMCommaNlsNumCharsDotCommaCloseParenSnippet = new SqlStringSnippet(", 'TM', 'NLS_NUMERIC_CHARACTERS = ''.,''')");

		// Token: 0x04000174 RID: 372
		private static readonly ISqlSnippet NumToDSPoint5SecondSnippet = new SqlStringSnippet("NUMTODSINTERVAL(0.5, 'SECOND')");

		// Token: 0x04000175 RID: 373
		private static readonly ISqlSnippet DivideBy3CloseParenSnippet = new SqlStringSnippet(" / 3)");

		// Token: 0x04000176 RID: 374
		private static readonly ISqlSnippet DivideBy12CloseParenSnippet = new SqlStringSnippet(" / 12)");

		// Token: 0x04000177 RID: 375
		private static readonly ISqlSnippet MultiplyBy3Snippet = new SqlStringSnippet(" * 3");

		// Token: 0x04000178 RID: 376
		private static readonly ISqlSnippet MultiplyBy4Snippet = new SqlStringSnippet(" * 4");

		// Token: 0x04000179 RID: 377
		private static readonly ISqlSnippet MultiplyBy7Snippet = new SqlStringSnippet(" * 7");

		// Token: 0x0400017A RID: 378
		private static readonly ISqlSnippet MultiplyBy12Snippet = new SqlStringSnippet(" * 12");

		// Token: 0x0400017B RID: 379
		private static readonly ISqlSnippet MultiplyBy24CloseParenSnippet = new SqlStringSnippet(" * 24)");

		// Token: 0x0400017C RID: 380
		private static readonly ISqlSnippet MultiplyBy1440CloseParenSnippet = new SqlStringSnippet(" * 1440)");

		// Token: 0x0400017D RID: 381
		private static readonly ISqlSnippet MultiplyBy86400CloseParenSnippet = new SqlStringSnippet(" * 86400)");
	}
}
