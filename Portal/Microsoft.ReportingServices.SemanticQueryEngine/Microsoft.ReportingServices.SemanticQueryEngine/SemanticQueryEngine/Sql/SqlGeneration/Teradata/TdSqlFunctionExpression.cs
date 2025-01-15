using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Teradata
{
	// Token: 0x02000040 RID: 64
	internal class TdSqlFunctionExpression : SqlFunctionExpression
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x0000C864 File Offset: 0x0000AA64
		internal TdSqlFunctionExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments, TdSqlBatch sqlBatch)
			: base(qpInfo, functionContext, arguments, sqlBatch)
		{
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000C871 File Offset: 0x0000AA71
		protected override ISqlSnippet CreateBasicSqlSnippetForDivide(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType)
		{
			if (arg1DataType == DataType.Integer && arg2DataType == DataType.Integer)
			{
				arg1 = SqlFunctionExpression.CastAsDecimal(arg1);
			}
			return base.CreateBasicSqlSnippetForDivide(arg1, arg1DataType, arg2, arg2DataType);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C88F File Offset: 0x0000AA8F
		protected override ISqlSnippet CreateBasicSqlSnippetForMod(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				arg1,
				TdSqlFunctionExpression.ModSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C8BC File Offset: 0x0000AABC
		protected override ISqlSnippet CreateBasicSqlSnippetForPower(ISqlSnippet pBase, DataType pBaseDataType, ISqlSnippet pExp, DataType pExpDataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CaseWhenSnippet,
				pExp,
				SqlExpression.MultiplySnippet,
				TdSqlFunctionExpression.LOGOpenParenSnippet,
				pBase,
				SqlExpression.CloseParenSnippet,
				SqlExpression.LessThanSnippet,
				TdSqlFunctionExpression.ExponentLowerLimit,
				SqlExpression.ThenSnippet,
				SqlExpression.ZeroSnippet,
				SqlExpression.ElseSnippet,
				pBase,
				TdSqlFunctionExpression.PowerSnippet,
				pExp,
				SqlExpression.EndSnippet
			});
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C944 File Offset: 0x0000AB44
		protected override ISqlSnippet CreateBasicSqlSnippetForTruncate(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			SqlCompositeSnippet sqlCompositeSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				arg1,
				SqlExpression.DivideSnippet,
				TdSqlFunctionExpression.TenExponentNegativeOpenParenSnippet,
				arg2,
				SqlExpression.CloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				sqlCompositeSnippet,
				SqlExpression.MinusSnippet,
				SqlExpression.OpenParenSnippet,
				sqlCompositeSnippet,
				TdSqlFunctionExpression.Mod1CloseParenSnippet,
				SqlExpression.CloseParenSnippet,
				SqlExpression.MultiplySnippet,
				TdSqlFunctionExpression.TenExponentNegativeOpenParenSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000C9E8 File Offset: 0x0000ABE8
		protected override ISqlSnippet CreateBasicSqlSnippetForString(ISqlSnippet argument, DataType argDataType)
		{
			ISqlSnippet sqlSnippet = argument;
			if (argDataType != DataType.Integer)
			{
				if (argDataType == DataType.Float)
				{
					sqlSnippet = SqlFunctionExpression.CastAsFloat(argument);
				}
			}
			else
			{
				sqlSnippet = this.CreateBasicSqlSnippetForInteger(argument, argDataType);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				sqlSnippet,
				SqlExpression.AsVarChar255CloseParenSnippet
			});
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000CA30 File Offset: 0x0000AC30
		protected override ISqlSnippet CreateBasicSqlSnippetForRound(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				arg1,
				SqlExpression.DivideSnippet,
				TdSqlFunctionExpression.TenExponentNegativeOpenParenSnippet,
				arg2,
				SqlExpression.CloseParenSnippet,
				TdSqlFunctionExpression.AsDecimal38and0Snippet,
				SqlExpression.CloseParenSnippet,
				SqlExpression.MultiplySnippet,
				TdSqlFunctionExpression.TenExponentNegativeOpenParenSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000CAA0 File Offset: 0x0000ACA0
		protected override ISqlSnippet CreateBasicSqlSnippetForInteger(ISqlSnippet argument, DataType argDataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				TdSqlFunctionExpression.AsBigIntCloseParenSnippet
			});
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000CAC4 File Offset: 0x0000ACC4
		protected override ISqlSnippet CreateBasicSqlSnippetForReplace(ISqlSnippet searchIn, ISqlSnippet searchFor, ISqlSnippet replace)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CaseWhenSnippet,
				searchIn,
				SqlExpression.IsNullSnippet,
				SqlExpression.ThenSnippet,
				SqlExpression.NullSnippet,
				SqlExpression.ElseSnippet,
				new SqlStringSnippet(this.SqlBatch.ReplaceFunctionName),
				SqlExpression.OpenParenSnippet,
				searchIn,
				SqlExpression.CommaSnippet,
				searchFor,
				SqlExpression.CommaSnippet,
				replace,
				SqlExpression.CloseParenSnippet,
				SqlExpression.EndSnippet
			});
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000CB58 File Offset: 0x0000AD58
		protected override ISqlSnippet CreateBasicSqlSnippetForLength(ISqlSnippet argument)
		{
			if (argument is SqlNullExpression)
			{
				return new SqlCompositeSnippet(new ISqlSnippet[]
				{
					TdSqlFunctionExpression.LengthOpenParenSnippet,
					SqlExpression.CastOpenParenSnippet,
					argument,
					TdSqlFunctionExpression.AsVarChar2CloseParenSnippet,
					SqlExpression.CloseParenSnippet
				});
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.LengthOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		protected override ISqlSnippet CreateBasicSqlSnippetForFind(ISqlSnippet searchIn, ISqlSnippet searchFor)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.PositionOpenParenSnippet,
				searchFor,
				TdSqlFunctionExpression.InSnippet,
				searchIn,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000CBE9 File Offset: 0x0000ADE9
		protected override ISqlSnippet CreateBasicSqlSnippetForSubstring(ISqlSnippet source, ISqlSnippet start, ISqlSnippet length)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.SubstrOpenParenSnippet,
				source,
				TdSqlFunctionExpression.SubstrFromSnippet,
				start,
				TdSqlFunctionExpression.SubstrForSnippet,
				length,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000CC22 File Offset: 0x0000AE22
		protected override ISqlSnippet CreateBasicSqlSnippetForLeft(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.SubstrOpenParenSnippet,
				arg1,
				TdSqlFunctionExpression.SubstrFromSnippet,
				SqlExpression.OneSnippet,
				TdSqlFunctionExpression.SubstrForSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000CC60 File Offset: 0x0000AE60
		protected override ISqlSnippet CreateBasicSqlSnippetForRight(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			ISqlSnippet sqlSnippet = this.CreateBasicSqlSnippetForLength(arg1);
			ISqlSnippet sqlSnippet2 = new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				sqlSnippet,
				SqlExpression.MinusSnippet,
				arg2,
				SqlExpression.PlusSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.CloseParenSnippet
			});
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.SubstrOpenParenSnippet,
				arg1,
				TdSqlFunctionExpression.SubstrFromSnippet,
				SqlExpression.CaseWhenSnippet,
				sqlSnippet2,
				SqlExpression.GreaterThanSnippet,
				SqlExpression.ZeroSnippet,
				SqlExpression.ThenSnippet,
				sqlSnippet2,
				SqlExpression.ElseSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.EndSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000CD1D File Offset: 0x0000AF1D
		protected override ISqlSnippet CreateBasicSqlSnippetForConcat(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				arg1,
				TdSqlFunctionExpression.ConcatSnippet,
				arg2,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000CD4A File Offset: 0x0000AF4A
		protected override ISqlSnippet CreateBasicSqlSnippetForLTrim(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.TrimLeadingOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000CD6B File Offset: 0x0000AF6B
		protected override ISqlSnippet CreateBasicSqlSnippetForRTrim(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.TrimTrailingOpenParenSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000CD8C File Offset: 0x0000AF8C
		protected override ISqlSnippet CreateBasicSqlSnippetForDate(ISqlSnippet argument)
		{
			return TdSqlFunctionExpression.CastAsTimestamp(TdSqlFunctionExpression.CastAsDate(argument));
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000CD9C File Offset: 0x0000AF9C
		protected override ISqlSnippet CreateBasicSqlSnippetForDate(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType, ISqlSnippet arg3, DataType arg3DataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				arg1,
				TdSqlFunctionExpression.AsNumeric4Format9999CloseParenSnippet,
				TdSqlFunctionExpression.AsVarChar4CloseParenSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				TdSqlFunctionExpression.StrDashSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				arg2,
				TdSqlFunctionExpression.AsNumeric2Format99CloseParenSnippet,
				TdSqlFunctionExpression.AsVarChar2CloseParenSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				TdSqlFunctionExpression.StrDashSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				arg3,
				TdSqlFunctionExpression.AsNumeric2Format99CloseParenSnippet,
				TdSqlFunctionExpression.AsVarChar2CloseParenSnippet,
				TdSqlFunctionExpression.AsTimestampFormatYYYYMMDDCloseParenSnippet
			});
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000CE70 File Offset: 0x0000B070
		protected override ISqlSnippet CreateBasicSqlSnippetForDateTime(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType, ISqlSnippet arg3, DataType arg3DataType, ISqlSnippet arg4, DataType arg4DataType, ISqlSnippet arg5, DataType arg5DataType, ISqlSnippet arg6, DataType arg6DataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				arg1,
				TdSqlFunctionExpression.AsNumeric4Format9999CloseParenSnippet,
				TdSqlFunctionExpression.AsVarChar4CloseParenSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				TdSqlFunctionExpression.StrDashSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				arg2,
				TdSqlFunctionExpression.AsNumeric2Format99CloseParenSnippet,
				TdSqlFunctionExpression.AsVarChar2CloseParenSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				TdSqlFunctionExpression.StrDashSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				arg3,
				TdSqlFunctionExpression.AsNumeric2Format99CloseParenSnippet,
				TdSqlFunctionExpression.AsVarChar2CloseParenSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				TdSqlFunctionExpression.StrSpaceSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				arg4,
				TdSqlFunctionExpression.AsNumeric2Format99CloseParenSnippet,
				TdSqlFunctionExpression.AsVarChar2CloseParenSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				TdSqlFunctionExpression.StrDashSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				arg5,
				TdSqlFunctionExpression.AsNumeric2Format99CloseParenSnippet,
				TdSqlFunctionExpression.AsVarChar2CloseParenSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				TdSqlFunctionExpression.StrDashSnippet,
				TdSqlFunctionExpression.ConcatSnippet,
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				arg6,
				TdSqlFunctionExpression.AsNumeric4comma2Format99dot99CloseParenSnippet,
				TdSqlFunctionExpression.AsVarChar5CloseParenSnippet,
				TdSqlFunctionExpression.AsTimestampFormatYYYYMMDDHHMISSCloseParenSnippet
			});
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000D013 File Offset: 0x0000B213
		protected override ISqlSnippet CreateBasicSqlSnippetForYear(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.YearFromSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000D03C File Offset: 0x0000B23C
		protected override ISqlSnippet CreateBasicSqlSnippetForQuarter(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.CastAsInteger(new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.OpenParenSnippet,
					this.CreateBasicSqlSnippetForMonth(argument),
					SqlExpression.SubtractSnippet,
					SqlExpression.OneSnippet,
					SqlExpression.CloseParenSnippet,
					TdSqlFunctionExpression.DivideBy3Snippet
				})),
				SqlExpression.AddSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		protected override ISqlSnippet CreateBasicSqlSnippetForMonth(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.MonthFromSnippet,
				TdSqlFunctionExpression.CastAsDate(argument),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
		protected override ISqlSnippet CreateBasicSqlSnippetForDay(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.DayFromSnippet,
				TdSqlFunctionExpression.CastAsDate(argument),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000D115 File Offset: 0x0000B315
		protected override ISqlSnippet CreateBasicSqlSnippetForHour(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.HourFromSnippet,
				TdSqlFunctionExpression.CastAsTimestamp(argument),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000D143 File Offset: 0x0000B343
		protected override ISqlSnippet CreateBasicSqlSnippetForMinute(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.MinuteFromSnippet,
				TdSqlFunctionExpression.CastAsTimestamp(argument),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000D171 File Offset: 0x0000B371
		protected override ISqlSnippet CreateBasicSqlSnippetForSecond(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.SecondFromSnippet,
				TdSqlFunctionExpression.CastAsTimestamp(argument),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000D19F File Offset: 0x0000B39F
		protected override ISqlSnippet CreateBasicSqlSnippetForDayOfYear(ISqlSnippet argument)
		{
			return TdSqlFunctionExpression.CastAsInteger(new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.CastOpenParenSnippet,
				argument,
				TdSqlFunctionExpression.AsDateFormatDayOfYearCloseParenSnippet,
				TdSqlFunctionExpression.AsVarChar3CloseParenSnippet
			}));
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		protected override ISqlSnippet CreateBasicSqlSnippetForWeek(ISqlSnippet argument)
		{
			ISqlSnippet sqlSnippet = this.CreateBasicSqlSnippetForDayOfYear(argument);
			ISqlSnippet sqlSnippet2 = this.CreateSqlSnippetForDayOfWeek(this.CreateSqlSnippetForFirstDayOfYear(argument), this.GetFirstDayOfWeekSnippet());
			return TdSqlFunctionExpression.CastAsInteger(new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				sqlSnippet,
				SqlExpression.PlusSnippet,
				sqlSnippet2,
				TdSqlFunctionExpression.Subtract2CloseParenSnippet,
				SqlExpression.DivideBy7CloseParenSnippet,
				SqlExpression.AddSnippet,
				SqlExpression.OneSnippet
			}));
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000D252 File Offset: 0x0000B452
		protected override ISqlSnippet CreateBasicSqlSnippetForDayOfWeek(ISqlSnippet argument)
		{
			return this.CreateSqlSnippetForDayOfWeek(argument, TdSqlFunctionExpression.StrMondaySnippet);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000D260 File Offset: 0x0000B460
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
			return TdSqlFunctionExpression.CastAsInteger(sqlSnippet);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D318 File Offset: 0x0000B518
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

		// Token: 0x060002C7 RID: 711 RVA: 0x0000D3B5 File Offset: 0x0000B5B5
		protected override ISqlSnippet CreateBasicSqlSnippetForTime(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				TdSqlFunctionExpression.AsTime6CloseParenSnippet
			});
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D3D8 File Offset: 0x0000B5D8
		private ISqlSnippet GetFirstDayOfWeekSnippet()
		{
			switch (this.SqlBatch.FirstDayOfWeek)
			{
			case DayOfWeek.Sunday:
				return TdSqlFunctionExpression.StrSundaySnippet;
			case DayOfWeek.Monday:
				return TdSqlFunctionExpression.StrMondaySnippet;
			case DayOfWeek.Tuesday:
				return TdSqlFunctionExpression.StrTuesdaySnippet;
			case DayOfWeek.Wednesday:
				return TdSqlFunctionExpression.StrWednesdaySnippet;
			case DayOfWeek.Thursday:
				return TdSqlFunctionExpression.StrThursdaySnippet;
			case DayOfWeek.Friday:
				return TdSqlFunctionExpression.StrFridaySnippet;
			case DayOfWeek.Saturday:
				return TdSqlFunctionExpression.StrSaturdaySnippet;
			default:
				throw SQEAssert.AssertFalseAndThrow(new ArgumentOutOfRangeException("this.SqlBatch.FirstDayOfWeek", "Unknown value: " + this.SqlBatch.FirstDayOfWeek.ToString()));
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D474 File Offset: 0x0000B674
		protected internal override void CreateBasicSqlSnippetForSingleValueIn(SqlExpression item, SqlLiteralExpression.LiteralSet literalSet, SqlCompositeSnippet inClause, bool isReal)
		{
			inClause.Append(new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				item
			}));
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < literalSet.Tuples.Count; i++)
			{
				ISqlSnippet sqlSnippet = literalSet.Tuples[i];
				if (SqlFunctionExpression.FindSqlNullExpression<ISqlSnippet>(ref sqlSnippet, ref sqlSnippet))
				{
					flag = true;
				}
				else
				{
					if (flag2)
					{
						inClause.Append(new SqlCompositeSnippet(new ISqlSnippet[]
						{
							SqlExpression.OrSnippet,
							item
						}));
					}
					inClause.Append(SqlExpression.EqualsSnippet);
					if (isReal)
					{
						sqlSnippet = SqlFunctionExpression.CastAsReal(sqlSnippet);
					}
					inClause.Append(sqlSnippet);
					flag2 = true;
				}
			}
			inClause.Append(SqlExpression.CloseParenSnippet);
			if (flag)
			{
				inClause.Prepend(SqlExpression.OpenParenSnippet);
				inClause.Append(SqlExpression.OrSnippet);
				inClause.Append(item);
				inClause.Append(SqlExpression.IsNullSnippet);
				inClause.Append(SqlExpression.CloseParenSnippet);
				return;
			}
			if (item.IsNullable && base.NeedToHandleUnknown)
			{
				inClause.Append(SqlExpression.AndSnippet);
				inClause.Append(SqlExpression.NotOpenParenSnippet);
				inClause.Append(item);
				inClause.Append(SqlExpression.IsNullSnippet);
				inClause.Append(SqlExpression.CloseParenSnippet);
			}
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D598 File Offset: 0x0000B798
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffYear(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.CastAsDate(endDate),
				SqlExpression.MinusSnippet,
				TdSqlFunctionExpression.CastAsDate(startDate),
				TdSqlFunctionExpression.YearPrec4Snippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D5D8 File Offset: 0x0000B7D8
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffQuarter(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.CastAsInteger(this.CreateBasicSqlSnippetForDateDiffYear(startDate, endDate)),
				TdSqlFunctionExpression.MultiplyBy4Snippet,
				SqlExpression.AddSnippet,
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.CastAsInteger(new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.OpenParenSnippet,
					this.CreateBasicSqlSnippetForMonth(endDate),
					SqlExpression.SubtractSnippet,
					SqlExpression.OneSnippet,
					SqlExpression.CloseParenSnippet,
					TdSqlFunctionExpression.DivideBy3Snippet
				})),
				SqlExpression.MinusSnippet,
				TdSqlFunctionExpression.CastAsInteger(new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.OpenParenSnippet,
					this.CreateBasicSqlSnippetForMonth(startDate),
					SqlExpression.SubtractSnippet,
					SqlExpression.OneSnippet,
					SqlExpression.CloseParenSnippet,
					TdSqlFunctionExpression.DivideBy3Snippet
				})),
				SqlExpression.CloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffMonth(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.CastAsDate(endDate),
				SqlExpression.MinusSnippet,
				TdSqlFunctionExpression.CastAsDate(startDate),
				TdSqlFunctionExpression.MonthPrec4Snippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D703 File Offset: 0x0000B903
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffDay(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.CastAsDate(endDate),
				SqlExpression.MinusSnippet,
				TdSqlFunctionExpression.CastAsDate(startDate),
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000D73C File Offset: 0x0000B93C
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffHour(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.CastAsInteger(this.CreateBasicSqlSnippetForDateDiffDay(startDate, endDate)),
				TdSqlFunctionExpression.MultiplyBy24CloseParenSnippet,
				SqlExpression.AddSnippet,
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.HourFromSnippet,
				TdSqlFunctionExpression.CastAsTimestamp(endDate),
				SqlExpression.CloseParenSnippet,
				SqlExpression.MinusSnippet,
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.HourFromSnippet,
				TdSqlFunctionExpression.CastAsTimestamp(endDate),
				SqlExpression.CloseParenSnippet,
				SqlExpression.CloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D7F0 File Offset: 0x0000B9F0
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffMinute(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				this.CreateBasicSqlSnippetForDateDiffHour(startDate, endDate),
				TdSqlFunctionExpression.MultiplyBy60CloseParenSnippet,
				SqlExpression.AddSnippet,
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.MinuteFromSnippet,
				TdSqlFunctionExpression.CastAsTimestamp(endDate),
				SqlExpression.CloseParenSnippet,
				SqlExpression.MinusSnippet,
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.MinuteFromSnippet,
				TdSqlFunctionExpression.CastAsTimestamp(endDate),
				SqlExpression.CloseParenSnippet,
				SqlExpression.CloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D8A0 File Offset: 0x0000BAA0
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffSecond(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				this.CreateBasicSqlSnippetForDateDiffMinute(startDate, endDate),
				TdSqlFunctionExpression.MultiplyBy60CloseParenSnippet,
				SqlExpression.AddSnippet,
				SqlExpression.CastOpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.SecondFromSnippet,
				TdSqlFunctionExpression.CastAsTimestamp(endDate),
				SqlExpression.CloseParenSnippet,
				SqlExpression.MinusSnippet,
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.SecondFromSnippet,
				TdSqlFunctionExpression.CastAsTimestamp(endDate),
				SqlExpression.CloseParenSnippet,
				SqlExpression.CloseParenSnippet,
				TdSqlFunctionExpression.AsBigIntCloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000D960 File Offset: 0x0000BB60
		private ISqlSnippet CreateBasicSqlSnippetForDateDiffWeek(ISqlSnippet startDate, ISqlSnippet endDate)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CaseWhenSnippet,
				endDate,
				SqlExpression.GreaterThanSnippet,
				startDate,
				SqlExpression.ThenSnippet,
				TdSqlFunctionExpression.CastAsInteger(new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.OpenParenSnippet,
					SqlExpression.OpenParenSnippet,
					TdSqlFunctionExpression.CastAsInteger(new SqlCompositeSnippet(new ISqlSnippet[]
					{
						SqlExpression.OpenParenSnippet,
						SqlExpression.OpenParenSnippet,
						TdSqlFunctionExpression.CastAsDate(endDate),
						SqlExpression.MinusSnippet,
						TdSqlFunctionExpression.CastAsDate(startDate),
						SqlExpression.CloseParenSnippet,
						SqlExpression.CloseParenSnippet
					})),
					SqlExpression.PlusSnippet,
					SqlExpression.SevenSnippet,
					SqlExpression.CloseParenSnippet,
					SqlExpression.MinusSnippet,
					this.CreateSqlSnippetForDayOfWeek(endDate, this.GetFirstDayOfWeekSnippet()),
					SqlExpression.CloseParenSnippet,
					TdSqlFunctionExpression.DivideBy7Snippet
				})),
				SqlExpression.ElseSnippet,
				TdSqlFunctionExpression.CastAsInteger(new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.OpenParenSnippet,
					SqlExpression.OpenParenSnippet,
					TdSqlFunctionExpression.CastAsInteger(new SqlCompositeSnippet(new ISqlSnippet[]
					{
						SqlExpression.OpenParenSnippet,
						SqlExpression.OpenParenSnippet,
						TdSqlFunctionExpression.CastAsDate(startDate),
						SqlExpression.MinusSnippet,
						TdSqlFunctionExpression.CastAsDate(endDate),
						SqlExpression.CloseParenSnippet,
						SqlExpression.CloseParenSnippet
					})),
					SqlExpression.PlusSnippet,
					SqlExpression.SevenSnippet,
					SqlExpression.CloseParenSnippet,
					SqlExpression.MinusSnippet,
					this.CreateSqlSnippetForDayOfWeek(startDate, this.GetFirstDayOfWeekSnippet()),
					SqlExpression.CloseParenSnippet,
					TdSqlFunctionExpression.DivideBy7Snippet
				})),
				TdSqlFunctionExpression.MultiplyByNeg1Snippet,
				SqlExpression.EndSnippet
			});
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000DB18 File Offset: 0x0000BD18
		private ISqlSnippet CreateSqlSnippetForDayOfWeek(ISqlSnippet theDate, ISqlSnippet firstDayOfWeek)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.CastAsDate(theDate),
				SqlExpression.MinusSnippet,
				TdSqlFunctionExpression.CastAsDate(firstDayOfWeek),
				SqlExpression.CloseParenSnippet,
				TdSqlFunctionExpression.ModSnippet,
				SqlExpression.SevenSnippet,
				SqlExpression.CloseParenSnippet,
				SqlExpression.AddSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		private ISqlSnippet CreateSqlSnippetForFirstDayOfYear(ISqlSnippet theDate)
		{
			return TdSqlFunctionExpression.CastAsDate(new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				TdSqlFunctionExpression.ExtractOpenParenSnippet,
				TdSqlFunctionExpression.YearFromSnippet,
				theDate,
				SqlExpression.CloseParenSnippet,
				TdSqlFunctionExpression.Subtract1900CloseParenSnippet,
				TdSqlFunctionExpression.Multiply10kPlus0101Snippet
			}));
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0000DBF1 File Offset: 0x0000BDF1
		private static ISqlSnippet CastAsDate(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				TdSqlFunctionExpression.AsDateCloseParenSnippet
			});
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0000DC12 File Offset: 0x0000BE12
		private static ISqlSnippet CastAsInteger(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				TdSqlFunctionExpression.AsIntCloseParenSnippet
			});
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x0000DC34 File Offset: 0x0000BE34
		private ISqlSnippet CreateBasicSqlSnippetForDateAddYear(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[] { this.CreateBasicSqlSnippetForDateAddMonth(date, new SqlCompositeSnippet(new ISqlSnippet[]
			{
				delta,
				TdSqlFunctionExpression.MultiplyBy12Snippet
			})) });
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000DC70 File Offset: 0x0000BE70
		private ISqlSnippet CreateBasicSqlSnippetForDateAddQuarter(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[] { this.CreateBasicSqlSnippetForDateAddMonth(date, new SqlCompositeSnippet(new ISqlSnippet[]
			{
				delta,
				TdSqlFunctionExpression.MultiplyBy3Snippet
			})) });
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000DCA9 File Offset: 0x0000BEA9
		private ISqlSnippet CreateBasicSqlSnippetForDateAddMonth(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				TdSqlFunctionExpression.AddMonthsOpenParenSnippet,
				date,
				SqlExpression.CommaSnippet,
				SqlExpression.CastOpenParenSnippet,
				delta,
				TdSqlFunctionExpression.AsIntCloseParenSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000DCE6 File Offset: 0x0000BEE6
		private ISqlSnippet CreateBasicSqlSnippetForDateAddDay(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				date,
				SqlExpression.PlusSnippet,
				SqlExpression.CastOpenParenSnippet,
				delta,
				TdSqlFunctionExpression.AsIntervalSnippet,
				TdSqlFunctionExpression.DayPrec4Snippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000DD23 File Offset: 0x0000BF23
		private ISqlSnippet CreateBasicSqlSnippetForDateAddHour(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				date,
				SqlExpression.PlusSnippet,
				SqlExpression.CastOpenParenSnippet,
				delta,
				TdSqlFunctionExpression.AsIntervalSnippet,
				TdSqlFunctionExpression.HourPrec4Snippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000DD60 File Offset: 0x0000BF60
		private ISqlSnippet CreateBasicSqlSnippetForDateAddMinute(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				date,
				SqlExpression.PlusSnippet,
				SqlExpression.CastOpenParenSnippet,
				delta,
				TdSqlFunctionExpression.AsIntervalSnippet,
				TdSqlFunctionExpression.MinutePrec4Snippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000DD9D File Offset: 0x0000BF9D
		private ISqlSnippet CreateBasicSqlSnippetForDateAddSecond(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				date,
				SqlExpression.PlusSnippet,
				SqlExpression.CastOpenParenSnippet,
				delta,
				TdSqlFunctionExpression.AsIntervalSnippet,
				TdSqlFunctionExpression.SecondPrec4Snippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		private ISqlSnippet CreateBasicSqlSnippetForDateAddWeek(ISqlSnippet date, ISqlSnippet delta)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[] { this.CreateBasicSqlSnippetForDateAddDay(date, new SqlCompositeSnippet(new ISqlSnippet[]
			{
				delta,
				TdSqlFunctionExpression.MultiplyBy7Snippet
			})) });
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000DE15 File Offset: 0x0000C015
		internal static ISqlSnippet CastAsTimestamp(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				TdSqlFunctionExpression.AsTimestampCloseParenSnippet
			});
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000DE36 File Offset: 0x0000C036
		private new TdSqlBatch SqlBatch
		{
			[DebuggerStepThrough]
			get
			{
				return (TdSqlBatch)base.SqlBatch;
			}
		}

		// Token: 0x040000FA RID: 250
		private static readonly ISqlSnippet LOGOpenParenSnippet = new SqlStringSnippet(" LOG(");

		// Token: 0x040000FB RID: 251
		private new static readonly ISqlSnippet ModSnippet = new SqlStringSnippet(" MOD ");

		// Token: 0x040000FC RID: 252
		private static readonly ISqlSnippet PowerSnippet = new SqlStringSnippet(" ** ");

		// Token: 0x040000FD RID: 253
		private static readonly ISqlSnippet LengthOpenParenSnippet = new SqlStringSnippet("CHARACTER_LENGTH(");

		// Token: 0x040000FE RID: 254
		private static readonly ISqlSnippet PositionOpenParenSnippet = new SqlStringSnippet("POSITION(");

		// Token: 0x040000FF RID: 255
		private static readonly ISqlSnippet SubstrOpenParenSnippet = new SqlStringSnippet("SUBSTRING(");

		// Token: 0x04000100 RID: 256
		private static readonly ISqlSnippet SubstrFromSnippet = new SqlStringSnippet(" FROM ");

		// Token: 0x04000101 RID: 257
		private static readonly ISqlSnippet SubstrForSnippet = new SqlStringSnippet(" FOR ");

		// Token: 0x04000102 RID: 258
		private static readonly ISqlSnippet ExtractOpenParenSnippet = new SqlStringSnippet("EXTRACT(");

		// Token: 0x04000103 RID: 259
		private static readonly ISqlSnippet AddMonthsOpenParenSnippet = new SqlStringSnippet("ADD_MONTHS(");

		// Token: 0x04000104 RID: 260
		private static readonly ISqlSnippet AsDecimal38and0Snippet = new SqlStringSnippet("AS DECIMAL(38,0)");

		// Token: 0x04000105 RID: 261
		private static readonly ISqlSnippet TrimLeadingOpenParenSnippet = new SqlStringSnippet("TRIM(LEADING FROM ");

		// Token: 0x04000106 RID: 262
		private static readonly ISqlSnippet TrimTrailingOpenParenSnippet = new SqlStringSnippet("TRIM(TRAILING FROM ");

		// Token: 0x04000107 RID: 263
		private static readonly ISqlSnippet TenExponentNegativeOpenParenSnippet = new SqlStringSnippet("10 ** -(");

		// Token: 0x04000108 RID: 264
		internal static readonly ISqlSnippet AsTimestampCloseParenSnippet = new SqlStringSnippet(" AS TIMESTAMP)");

		// Token: 0x04000109 RID: 265
		internal static readonly ISqlSnippet AsTimestamp0CloseParenSnippet = new SqlStringSnippet(" AS TIMESTAMP(0))");

		// Token: 0x0400010A RID: 266
		internal static readonly ISqlSnippet AsTime6CloseParenSnippet = new SqlStringSnippet(" AS TIME(6))");

		// Token: 0x0400010B RID: 267
		private static readonly ISqlSnippet AsDateCloseParenSnippet = new SqlStringSnippet(" AS DATE)");

		// Token: 0x0400010C RID: 268
		internal static readonly ISqlSnippet AsIntCloseParenSnippet = new SqlStringSnippet(" AS INTEGER)");

		// Token: 0x0400010D RID: 269
		private static readonly ISqlSnippet AsBigIntCloseParenSnippet = new SqlStringSnippet(" AS BIGINT)");

		// Token: 0x0400010E RID: 270
		private static readonly ISqlSnippet AsIntervalSnippet = new SqlStringSnippet(" AS INTERVAL");

		// Token: 0x0400010F RID: 271
		private static readonly ISqlSnippet AsDateFormatDayOfYearCloseParenSnippet = new SqlStringSnippet(" AS DATE FORMAT 'DDD')");

		// Token: 0x04000110 RID: 272
		internal static readonly ISqlSnippet AsVarChar2CloseParenSnippet = new SqlStringSnippet(" AS VARCHAR(2))");

		// Token: 0x04000111 RID: 273
		private static readonly ISqlSnippet AsVarChar3CloseParenSnippet = new SqlStringSnippet(" AS VARCHAR(3))");

		// Token: 0x04000112 RID: 274
		private static readonly ISqlSnippet AsVarChar4CloseParenSnippet = new SqlStringSnippet(" AS VARCHAR(4))");

		// Token: 0x04000113 RID: 275
		private static readonly ISqlSnippet AsVarChar5CloseParenSnippet = new SqlStringSnippet(" AS VARCHAR(5))");

		// Token: 0x04000114 RID: 276
		private static readonly ISqlSnippet AsTimestampFormatYYYYMMDDCloseParenSnippet = new SqlStringSnippet(" AS TIMESTAMP FORMAT 'YYYY-MM-DD')");

		// Token: 0x04000115 RID: 277
		private static readonly ISqlSnippet AsTimestampFormatYYYYMMDDHHMISSCloseParenSnippet = new SqlStringSnippet(" AS TIMESTAMP(2) FORMAT 'YYYY-MM-DD-HH-MI-SS.S(2)')");

		// Token: 0x04000116 RID: 278
		private static readonly ISqlSnippet AsNumeric4Format9999CloseParenSnippet = new SqlStringSnippet(" AS NUMERIC(4) FORMAT '9999')");

		// Token: 0x04000117 RID: 279
		private static readonly ISqlSnippet AsNumeric2Format99CloseParenSnippet = new SqlStringSnippet(" AS NUMERIC(2) FORMAT '99')");

		// Token: 0x04000118 RID: 280
		private static readonly ISqlSnippet AsNumeric4comma2Format99dot99CloseParenSnippet = new SqlStringSnippet(" AS NUMERIC(4,2) FORMAT '99.99')");

		// Token: 0x04000119 RID: 281
		private static readonly ISqlSnippet YearPrec4Snippet = new SqlStringSnippet(" YEAR(4)");

		// Token: 0x0400011A RID: 282
		private static readonly ISqlSnippet MonthPrec4Snippet = new SqlStringSnippet(" MONTH(4)");

		// Token: 0x0400011B RID: 283
		private static readonly ISqlSnippet DayPrec4Snippet = new SqlStringSnippet(" DAY(4)");

		// Token: 0x0400011C RID: 284
		private static readonly ISqlSnippet HourPrec4Snippet = new SqlStringSnippet(" HOUR(4)");

		// Token: 0x0400011D RID: 285
		private static readonly ISqlSnippet MinutePrec4Snippet = new SqlStringSnippet(" MINUTE(4)");

		// Token: 0x0400011E RID: 286
		private static readonly ISqlSnippet SecondPrec4Snippet = new SqlStringSnippet(" SECOND(4,6)");

		// Token: 0x0400011F RID: 287
		private static readonly ISqlSnippet InSnippet = new SqlStringSnippet(" IN ");

		// Token: 0x04000120 RID: 288
		private static readonly ISqlSnippet StrMondaySnippet = new SqlStringSnippet("/*MONDAY*/ '1900-01-01'");

		// Token: 0x04000121 RID: 289
		private static readonly ISqlSnippet StrTuesdaySnippet = new SqlStringSnippet("/*TUESDAY*/ '1900-01-02'");

		// Token: 0x04000122 RID: 290
		private static readonly ISqlSnippet StrWednesdaySnippet = new SqlStringSnippet("/*WEDNESDAY*/ '1900-01-03'");

		// Token: 0x04000123 RID: 291
		private static readonly ISqlSnippet StrThursdaySnippet = new SqlStringSnippet("/*THURSDAY*/ '1900-01-04'");

		// Token: 0x04000124 RID: 292
		private static readonly ISqlSnippet StrFridaySnippet = new SqlStringSnippet("/*FRIDAY*/ '1900-01-05'");

		// Token: 0x04000125 RID: 293
		private static readonly ISqlSnippet StrSaturdaySnippet = new SqlStringSnippet("/*SATURDAY*/ '1900-01-06'");

		// Token: 0x04000126 RID: 294
		private static readonly ISqlSnippet StrSundaySnippet = new SqlStringSnippet("/*SUNDAY*/ '1900-01-07'");

		// Token: 0x04000127 RID: 295
		private static readonly ISqlSnippet StrDashSnippet = new SqlStringSnippet("'-'");

		// Token: 0x04000128 RID: 296
		private static readonly ISqlSnippet StrSpaceSnippet = new SqlStringSnippet("' '");

		// Token: 0x04000129 RID: 297
		private static readonly ISqlSnippet ConcatSnippet = new SqlStringSnippet(" || ");

		// Token: 0x0400012A RID: 298
		private static readonly ISqlSnippet YearFromSnippet = new SqlStringSnippet("YEAR FROM ");

		// Token: 0x0400012B RID: 299
		private static readonly ISqlSnippet MonthFromSnippet = new SqlStringSnippet("MONTH FROM ");

		// Token: 0x0400012C RID: 300
		private static readonly ISqlSnippet DayFromSnippet = new SqlStringSnippet("DAY FROM ");

		// Token: 0x0400012D RID: 301
		private static readonly ISqlSnippet HourFromSnippet = new SqlStringSnippet("HOUR FROM ");

		// Token: 0x0400012E RID: 302
		private static readonly ISqlSnippet MinuteFromSnippet = new SqlStringSnippet("MINUTE FROM ");

		// Token: 0x0400012F RID: 303
		private static readonly ISqlSnippet SecondFromSnippet = new SqlStringSnippet("SECOND FROM ");

		// Token: 0x04000130 RID: 304
		private static readonly ISqlSnippet DivideBy3Snippet = new SqlStringSnippet(" / 3");

		// Token: 0x04000131 RID: 305
		private static readonly ISqlSnippet DivideBy7Snippet = new SqlStringSnippet(" / 7");

		// Token: 0x04000132 RID: 306
		private static readonly ISqlSnippet MultiplyBy3Snippet = new SqlStringSnippet(" * 3");

		// Token: 0x04000133 RID: 307
		private static readonly ISqlSnippet MultiplyBy4Snippet = new SqlStringSnippet(" * 4");

		// Token: 0x04000134 RID: 308
		private static readonly ISqlSnippet MultiplyBy7Snippet = new SqlStringSnippet(" * 7");

		// Token: 0x04000135 RID: 309
		private static readonly ISqlSnippet MultiplyByNeg1Snippet = new SqlStringSnippet(" * (-1)");

		// Token: 0x04000136 RID: 310
		private static readonly ISqlSnippet MultiplyBy12Snippet = new SqlStringSnippet(" * 12");

		// Token: 0x04000137 RID: 311
		private static readonly ISqlSnippet MultiplyBy24CloseParenSnippet = new SqlStringSnippet(" * 24)");

		// Token: 0x04000138 RID: 312
		private static readonly ISqlSnippet MultiplyBy60CloseParenSnippet = new SqlStringSnippet(" * 60)");

		// Token: 0x04000139 RID: 313
		private static readonly ISqlSnippet Subtract1900CloseParenSnippet = new SqlStringSnippet("- 1900)");

		// Token: 0x0400013A RID: 314
		private static readonly ISqlSnippet Multiply10kPlus0101Snippet = new SqlStringSnippet(" * 10000 + 0101");

		// Token: 0x0400013B RID: 315
		private static readonly ISqlSnippet Subtract2CloseParenSnippet = new SqlStringSnippet(" - 2)");

		// Token: 0x0400013C RID: 316
		private static readonly ISqlSnippet Mod1CloseParenSnippet = new SqlStringSnippet(" MOD 1)");

		// Token: 0x0400013D RID: 317
		private static readonly ISqlSnippet ExponentLowerLimit = new SqlStringSnippet(" -4931 ");
	}
}
