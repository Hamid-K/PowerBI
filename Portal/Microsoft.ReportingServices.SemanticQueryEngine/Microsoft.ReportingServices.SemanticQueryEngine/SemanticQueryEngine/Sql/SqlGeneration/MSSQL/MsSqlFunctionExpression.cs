using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.MSSQL
{
	// Token: 0x0200004D RID: 77
	internal class MsSqlFunctionExpression : SqlFunctionExpression
	{
		// Token: 0x0600036F RID: 879 RVA: 0x000102C0 File Offset: 0x0000E4C0
		internal MsSqlFunctionExpression(IQPExpressionInfo qpInfo, FunctionContext functionContext, SqlExpression[] arguments, SqlBatch sqlBatch)
			: base(qpInfo, functionContext, arguments, sqlBatch)
		{
			using (List<Expression>.Enumerator enumerator = base.FunctionNode.Arguments.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (MsSqlFunctionExpression.HasDateTimeOffset(enumerator.Current))
					{
						this.m_argsHaveDateTimeOffset = true;
						break;
					}
				}
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001032C File Offset: 0x0000E52C
		internal static ISqlSnippet CastGuidAsString(Expression expression, ISqlSnippet sqlSnippet)
		{
			if (!MsSqlFunctionExpression.IsGuidTraverser.IsGuid(expression))
			{
				return sqlSnippet;
			}
			if (sqlSnippet is SqlExpression && ((SqlExpression)sqlSnippet).Values.Count != 1)
			{
				throw SQEAssert.AssertFalseAndThrow("sqlSnippet must have exactly one value.", Array.Empty<object>());
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				sqlSnippet,
				MsSqlFunctionExpression.AsVarChar36CloseParenSnippet
			});
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000C871 File Offset: 0x0000AA71
		protected override ISqlSnippet CreateBasicSqlSnippetForDivide(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType)
		{
			if (arg1DataType == DataType.Integer && arg2DataType == DataType.Integer)
			{
				arg1 = SqlFunctionExpression.CastAsDecimal(arg1);
			}
			return base.CreateBasicSqlSnippetForDivide(arg1, arg1DataType, arg2, arg2DataType);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001038D File Offset: 0x0000E58D
		protected override ISqlSnippet CreateBasicSqlSnippetForPower(ISqlSnippet pBase, DataType pBaseDataType, ISqlSnippet pExp, DataType pExpDataType)
		{
			if (pExpDataType == DataType.Float)
			{
				if (pBaseDataType != DataType.Float)
				{
					pBase = SqlFunctionExpression.CastAsFloat(pBase);
				}
			}
			else if (pExpDataType == DataType.Decimal && pBaseDataType == DataType.Integer)
			{
				pBase = SqlFunctionExpression.CastAsDecimal(pBase);
			}
			return base.CreateBasicSqlSnippetForPower(pBase, pBaseDataType, pExp, pExpDataType);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x000103BE File Offset: 0x0000E5BE
		protected override ISqlSnippet CreateBasicSqlSnippetForTruncate(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.RoundOpenParenSnippet,
				arg1,
				SqlExpression.CommaSnippet,
				arg2,
				SqlExpression.CommaSnippet,
				SqlExpression.OneSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000103FB File Offset: 0x0000E5FB
		protected override ISqlSnippet CreateBasicSqlSnippetForInteger(ISqlSnippet argument, DataType argDataType)
		{
			if (argDataType == DataType.Decimal)
			{
				argument = base.CreateSqlSnippetForTruncate(argument, SqlExpression.ZeroSnippet);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				MsSqlFunctionExpression.AsBigIntCloseParenSnippet
			});
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001042E File Offset: 0x0000E62E
		protected override ISqlSnippet CreateBasicSqlSnippetForString(ISqlSnippet argument, DataType argDataType)
		{
			if (argDataType == DataType.Float)
			{
				return new SqlCompositeSnippet(new ISqlSnippet[]
				{
					MsSqlFunctionExpression.CastFloatAsStringOpenSnippet,
					argument,
					MsSqlFunctionExpression.CastFloatAsStringCloseSnippet
				});
			}
			return base.CreateBasicSqlSnippetForString(argument, argDataType);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001045C File Offset: 0x0000E65C
		protected override ISqlSnippet CreateBasicSqlSnippetForLength(ISqlSnippet argument)
		{
			return base.CreateBasicSqlSnippetForLength(MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], argument));
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001047C File Offset: 0x0000E67C
		protected override ISqlSnippet CreateBasicSqlSnippetForFind(ISqlSnippet searchIn, ISqlSnippet searchFor)
		{
			if (searchIn is SqlNullExpression)
			{
				searchIn = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CastOpenParenSnippet,
					searchIn,
					SqlExpression.AsVarChar255CloseParenSnippet
				});
			}
			else
			{
				searchIn = MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], searchIn);
			}
			if (searchFor is SqlNullExpression)
			{
				searchFor = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CastOpenParenSnippet,
					searchFor,
					SqlExpression.AsVarChar255CloseParenSnippet
				});
			}
			else
			{
				searchFor = MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[1], searchFor);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.CharIndexOpenParenSnippet,
				searchFor,
				SqlExpression.CommaSnippet,
				searchIn,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0001053C File Offset: 0x0000E73C
		protected override ISqlSnippet CreateBasicSqlSnippetForSubstring(ISqlSnippet source, ISqlSnippet start, ISqlSnippet length)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.SubStringOpenParenSnippet,
				MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], source),
				SqlExpression.CommaSnippet,
				start,
				SqlExpression.CommaSnippet,
				length,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00010596 File Offset: 0x0000E796
		protected override ISqlSnippet CreateBasicSqlSnippetForLeft(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return base.CreateBasicSqlSnippetForLeft(MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], arg1), arg2);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x000105B6 File Offset: 0x0000E7B6
		protected override ISqlSnippet CreateBasicSqlSnippetForRight(ISqlSnippet arg1, ISqlSnippet arg2)
		{
			return base.CreateBasicSqlSnippetForRight(MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], arg1), arg2);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000105D8 File Offset: 0x0000E7D8
		protected override ISqlSnippet ConvertNullToEmptyString(Expression expression, SqlExpression stringExpression)
		{
			ISqlSnippet sqlSnippet = MsSqlFunctionExpression.CastGuidAsString(expression, stringExpression);
			if (sqlSnippet is SqlExpression)
			{
				stringExpression = (SqlExpression)sqlSnippet;
			}
			else
			{
				stringExpression = new SqlSnippetExpression(sqlSnippet, stringExpression.IsNullable);
			}
			return base.ConvertNullToEmptyString(expression, stringExpression);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00010615 File Offset: 0x0000E815
		protected override ISqlSnippet CreateBasicSqlSnippetForLower(ISqlSnippet argument)
		{
			return base.CreateBasicSqlSnippetForLower(MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], argument));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00010634 File Offset: 0x0000E834
		protected override ISqlSnippet CreateBasicSqlSnippetForUpper(ISqlSnippet argument)
		{
			return base.CreateBasicSqlSnippetForUpper(MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], argument));
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00010653 File Offset: 0x0000E853
		protected override ISqlSnippet CreateBasicSqlSnippetForLTrim(ISqlSnippet argument)
		{
			return base.CreateBasicSqlSnippetForLTrim(MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], argument));
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00010672 File Offset: 0x0000E872
		protected override ISqlSnippet CreateBasicSqlSnippetForRTrim(ISqlSnippet argument)
		{
			return base.CreateBasicSqlSnippetForRTrim(MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], argument));
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00010694 File Offset: 0x0000E894
		protected override ISqlSnippet CreateBasicSqlSnippetForReplace(ISqlSnippet searchIn, ISqlSnippet searchFor, ISqlSnippet replace)
		{
			return base.CreateBasicSqlSnippetForReplace(MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[0], searchIn), MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[1], searchFor), MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[2], replace));
		}

		// Token: 0x06000381 RID: 897 RVA: 0x000106EC File Offset: 0x0000E8EC
		protected override ISqlSnippet CreateBasicSqlSnippetForDate(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DateOnlyOpenSnippet,
				argument,
				MsSqlFunctionExpression.DateOnlyCloseSnippet
			});
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00010720 File Offset: 0x0000E920
		protected override ISqlSnippet CreateBasicSqlSnippetForDate(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType, ISqlSnippet arg3, DataType arg3DataType)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.ConvertDateTimeCommaSnippet,
				base.CreateSqlSnippetForString(arg1, arg1DataType),
				MsSqlFunctionExpression.PlusDotPlusSnippet,
				base.CreateSqlSnippetForString(arg2, arg2DataType),
				MsSqlFunctionExpression.PlusDotPlusSnippet,
				base.CreateSqlSnippetForString(arg3, arg3DataType),
				MsSqlFunctionExpression.Comma102CloseParenSnippet
			});
		}

		// Token: 0x06000383 RID: 899 RVA: 0x0001077C File Offset: 0x0000E97C
		protected override ISqlSnippet CreateBasicSqlSnippetForDateTime(ISqlSnippet arg1, DataType arg1DataType, ISqlSnippet arg2, DataType arg2DataType, ISqlSnippet arg3, DataType arg3DataType, ISqlSnippet arg4, DataType arg4DataType, ISqlSnippet arg5, DataType arg5DataType, ISqlSnippet arg6, DataType arg6DataType)
		{
			ISqlSnippet sqlSnippet = ((base.SqlBatch.ServerMajorVersion >= 10) ? MsSqlFunctionExpression.ConvertDateTime2CommaSnippet : MsSqlFunctionExpression.ConvertDateTimeCommaSnippet);
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				sqlSnippet,
				base.CreateSqlSnippetForString(arg1, arg1DataType),
				MsSqlFunctionExpression.PlusDashPlusSnippet,
				base.CreateSqlSnippetForString(arg2, arg2DataType),
				MsSqlFunctionExpression.PlusDashPlusSnippet,
				base.CreateSqlSnippetForString(arg3, arg3DataType),
				MsSqlFunctionExpression.PlusSpacePlusSnippet,
				base.CreateSqlSnippetForString(arg4, arg4DataType),
				MsSqlFunctionExpression.PlusColonPlusSnippet,
				base.CreateSqlSnippetForString(arg5, arg5DataType),
				MsSqlFunctionExpression.PlusColonPlusSnippet,
				MsSqlFunctionExpression.SecondsOpenSnippet,
				arg6,
				MsSqlFunctionExpression.SecondsCloseSnippet,
				MsSqlFunctionExpression.Comma121CloseParenSnippet
			});
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0001083E File Offset: 0x0000EA3E
		protected override ISqlSnippet CreateBasicSqlSnippetForTime(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.CastOpenParenSnippet,
				argument,
				MsSqlFunctionExpression.AsTime7CloseParenSnippet
			});
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001086F File Offset: 0x0000EA6F
		protected override ISqlSnippet CreateBasicSqlSnippetForYear(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DPYearSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000386 RID: 902 RVA: 0x000108A0 File Offset: 0x0000EAA0
		protected override ISqlSnippet CreateBasicSqlSnippetForQuarter(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DPQuarterSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000387 RID: 903 RVA: 0x000108D1 File Offset: 0x0000EAD1
		protected override ISqlSnippet CreateBasicSqlSnippetForMonth(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DPMonthSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00010902 File Offset: 0x0000EB02
		protected override ISqlSnippet CreateBasicSqlSnippetForDay(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DPDaySnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00010933 File Offset: 0x0000EB33
		protected override ISqlSnippet CreateBasicSqlSnippetForHour(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DPHourSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00010964 File Offset: 0x0000EB64
		protected override ISqlSnippet CreateBasicSqlSnippetForMinute(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DPMinuteSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00010998 File Offset: 0x0000EB98
		protected override ISqlSnippet CreateBasicSqlSnippetForSecond(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				SqlExpression.OpenParenSnippet,
				MsSqlFunctionExpression.DPSecondSnippet,
				argument,
				SqlExpression.CloseParenSnippet,
				SqlExpression.AddSnippet,
				MsSqlFunctionExpression.DPMillisecondSnippet,
				argument,
				SqlExpression.CloseParenSnippet,
				SqlExpression.DivideSnippet,
				MsSqlFunctionExpression.DecimalOneThousandSnippet,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00010A13 File Offset: 0x0000EC13
		protected override ISqlSnippet CreateBasicSqlSnippetForDayOfYear(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DPDayOfYearSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00010A44 File Offset: 0x0000EC44
		protected override ISqlSnippet CreateBasicSqlSnippetForWeek(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DPWeekSnippet,
				argument,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00010A75 File Offset: 0x0000EC75
		protected override ISqlSnippet CreateBasicSqlSnippetForDayOfWeek(ISqlSnippet argument)
		{
			if (this.m_argsHaveDateTimeOffset)
			{
				argument = MsSqlFunctionExpression.ConvertDateTimeOffsetToUtc(argument);
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DPDayOfWeekOpenSnippet,
				argument,
				MsSqlFunctionExpression.DayOfWeekCloseSnippet
			});
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00010AA8 File Offset: 0x0000ECA8
		protected override ISqlSnippet CreateBasicSqlSnippetForDateDiff(SqlFunctionExpression.DatePart datePart, ISqlSnippet startDate, ISqlSnippet endDate)
		{
			if (datePart == SqlFunctionExpression.DatePart.Week)
			{
				ISqlSnippet sqlSnippet = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					MsSqlFunctionExpression.DateDiffOpenParenSnippet,
					MsSqlFunctionExpression.DayCommaSnippet,
					startDate,
					SqlExpression.CommaSnippet,
					endDate,
					SqlExpression.CloseParenSnippet
				});
				ISqlSnippet sqlSnippet2 = new SqlCompositeSnippet(new ISqlSnippet[]
				{
					MsSqlFunctionExpression.DPWeekDaySnippet,
					endDate,
					SqlExpression.CloseParenSnippet
				});
				return new SqlCompositeSnippet(new ISqlSnippet[]
				{
					SqlExpression.CaseWhenSnippet,
					endDate,
					SqlExpression.GreaterThanSnippet,
					startDate,
					SqlExpression.ThenSnippet,
					SqlExpression.FloorOpenParenSnippet,
					SqlExpression.OpenParenSnippet,
					sqlSnippet,
					SqlExpression.PlusSnippet,
					SqlExpression.SevenSnippet,
					SqlExpression.MinusSnippet,
					sqlSnippet2,
					SqlExpression.CloseParenSnippet,
					SqlExpression.DivideBy7CloseParenSnippet,
					SqlExpression.ElseSnippet,
					MsSqlFunctionExpression.CeilingOpenParenSnippet,
					SqlExpression.OpenParenSnippet,
					sqlSnippet,
					SqlExpression.PlusSnippet,
					SqlExpression.OneSnippet,
					SqlExpression.MinusSnippet,
					sqlSnippet2,
					SqlExpression.CloseParenSnippet,
					SqlExpression.DivideBy7CloseParenSnippet,
					SqlExpression.EndSnippet
				});
			}
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DateDiffOpenParenSnippet,
				this.GetDatePartSqlSnippet(datePart),
				startDate,
				SqlExpression.CommaSnippet,
				endDate,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00010C12 File Offset: 0x0000EE12
		protected override ISqlSnippet CreateBasicSqlSnippetForDateAdd(SqlFunctionExpression.DatePart datePart, ISqlSnippet delta, ISqlSnippet date)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.DateAddOpenParenSnippet,
				this.GetDatePartSqlSnippet(datePart),
				delta,
				SqlExpression.CommaSnippet,
				date,
				SqlExpression.CloseParenSnippet
			});
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00010C49 File Offset: 0x0000EE49
		protected override ISqlSnippet CreateBasicSqlSnippetForIf(ISqlSnippet condition, ISqlSnippet thenSnippet, ISqlSnippet elseSnippet)
		{
			return base.CreateBasicSqlSnippetForIf(condition, MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[1], thenSnippet), MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[2], elseSnippet));
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00010C80 File Offset: 0x0000EE80
		protected override ISqlSnippet CreateBasicSqlSnippetForSwitch(IEnumerable<KeyValuePair<ISqlSnippet, ISqlSnippet>> arguments)
		{
			return base.CreateBasicSqlSnippetForSwitch(this.PrepareSwitchArguments(arguments));
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00010C90 File Offset: 0x0000EE90
		private ISqlSnippet GetDatePartSqlSnippet(SqlFunctionExpression.DatePart datePart)
		{
			switch (datePart)
			{
			case SqlFunctionExpression.DatePart.Year:
				return MsSqlFunctionExpression.YearCommaSnippet;
			case SqlFunctionExpression.DatePart.Quarter:
				return MsSqlFunctionExpression.QuarterCommaSnippet;
			case SqlFunctionExpression.DatePart.Month:
				return MsSqlFunctionExpression.MonthCommaSnippet;
			case SqlFunctionExpression.DatePart.Day:
				return MsSqlFunctionExpression.DayCommaSnippet;
			case SqlFunctionExpression.DatePart.Hour:
				return MsSqlFunctionExpression.HourCommaSnippet;
			case SqlFunctionExpression.DatePart.Minute:
				return MsSqlFunctionExpression.MinuteCommaSnippet;
			case SqlFunctionExpression.DatePart.Second:
				return MsSqlFunctionExpression.SecondCommaSnippet;
			case SqlFunctionExpression.DatePart.Week:
				return MsSqlFunctionExpression.WeekCommaSnippet;
			default:
				throw SQEAssert.AssertFalseAndThrow("Unknown date part: {0}.", new object[] { datePart.ToString() });
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00010D15 File Offset: 0x0000EF15
		private IEnumerable<KeyValuePair<ISqlSnippet, ISqlSnippet>> PrepareSwitchArguments(IEnumerable<KeyValuePair<ISqlSnippet, ISqlSnippet>> arguments)
		{
			int i = 0;
			foreach (KeyValuePair<ISqlSnippet, ISqlSnippet> keyValuePair in arguments)
			{
				yield return new KeyValuePair<ISqlSnippet, ISqlSnippet>(keyValuePair.Key, MsSqlFunctionExpression.CastGuidAsString(base.FunctionNode.Arguments[i * 2 + 1], keyValuePair.Value));
				int num = i + 1;
				i = num;
			}
			IEnumerator<KeyValuePair<ISqlSnippet, ISqlSnippet>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00010D2C File Offset: 0x0000EF2C
		private static bool HasDateTimeOffset(Expression expr)
		{
			if (expr.GetResultType().DataType != DataType.DateTime)
			{
				return false;
			}
			if (expr.Node is FunctionNode)
			{
				using (List<Expression>.Enumerator enumerator = expr.NodeAsFunction.Arguments.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (MsSqlFunctionExpression.HasDateTimeOffset(enumerator.Current))
						{
							return true;
						}
					}
				}
				return false;
			}
			if (expr.Node is AttributeRefNode)
			{
				if (expr.NodeAsAttributeRef.Attribute.CalculatedAttribute != null)
				{
					return MsSqlFunctionExpression.HasDateTimeOffset(expr.NodeAsAttributeRef.Attribute.CalculatedAttribute);
				}
				ColumnBinding binding = expr.NodeAsAttributeRef.Attribute.ModelAttribute.Binding;
				if (binding == null || binding.GetColumn() == null)
				{
					throw SQEAssert.AssertFalseAndThrow("ModelAttribute {0} has column binding that is not valid.", new object[] { expr.NodeAsAttributeRef.Attribute.ModelAttribute.Name });
				}
				return binding.GetColumn().DataType == typeof(DateTimeOffset);
			}
			else
			{
				if (expr.Node is ParameterRefNode)
				{
					return expr.NodeAsParameterRef.Parameter.DefaultValue != null && MsSqlFunctionExpression.HasDateTimeOffset(expr.NodeAsParameterRef.Parameter.DefaultValue);
				}
				if (expr.Node is EntityRefNode || expr.Node is LiteralNode || expr.Node is NullNode)
				{
					return false;
				}
				throw SQEAssert.AssertFalseAndThrow("Unknown expression node type {0}.", new object[] { expr.Node.GetType().Name });
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00010ED0 File Offset: 0x0000F0D0
		private static ISqlSnippet ConvertDateTimeOffsetToUtc(ISqlSnippet argument)
		{
			return new SqlCompositeSnippet(new ISqlSnippet[]
			{
				MsSqlFunctionExpression.ConvertDTOfsToUtcOpen,
				argument,
				MsSqlFunctionExpression.ConvertDTOfsToUtcClose
			});
		}

		// Token: 0x04000186 RID: 390
		private readonly bool m_argsHaveDateTimeOffset;

		// Token: 0x04000187 RID: 391
		private static readonly ISqlSnippet CharIndexOpenParenSnippet = new SqlStringSnippet("CHARINDEX(");

		// Token: 0x04000188 RID: 392
		private static readonly ISqlSnippet SubStringOpenParenSnippet = new SqlStringSnippet("SUBSTRING(");

		// Token: 0x04000189 RID: 393
		private static readonly ISqlSnippet CeilingOpenParenSnippet = new SqlStringSnippet("CEILING(");

		// Token: 0x0400018A RID: 394
		private static readonly ISqlSnippet DateOnlyOpenSnippet = new SqlStringSnippet("CONVERT(DATETIME, CONVERT(CHAR(10), ");

		// Token: 0x0400018B RID: 395
		private static readonly ISqlSnippet DateOnlyCloseSnippet = new SqlStringSnippet(", 102), 102)");

		// Token: 0x0400018C RID: 396
		internal static readonly ISqlSnippet ConvertDateTimeCommaSnippet = new SqlStringSnippet("CONVERT(DATETIME,");

		// Token: 0x0400018D RID: 397
		internal static readonly ISqlSnippet ConvertDateTime2CommaSnippet = new SqlStringSnippet("CONVERT(DATETIME2,");

		// Token: 0x0400018E RID: 398
		internal static readonly ISqlSnippet Comma121CloseParenSnippet = new SqlStringSnippet(", 121)");

		// Token: 0x0400018F RID: 399
		private static readonly ISqlSnippet Comma102CloseParenSnippet = new SqlStringSnippet(", 102)");

		// Token: 0x04000190 RID: 400
		private static readonly ISqlSnippet PlusDotPlusSnippet = new SqlStringSnippet(" + '.' + ");

		// Token: 0x04000191 RID: 401
		private static readonly ISqlSnippet PlusDashPlusSnippet = new SqlStringSnippet(" + '-' + ");

		// Token: 0x04000192 RID: 402
		private static readonly ISqlSnippet PlusSpacePlusSnippet = new SqlStringSnippet(" + ' ' + ");

		// Token: 0x04000193 RID: 403
		private static readonly ISqlSnippet PlusColonPlusSnippet = new SqlStringSnippet(" + ':' + ");

		// Token: 0x04000194 RID: 404
		private static readonly ISqlSnippet DPYearSnippet = new SqlStringSnippet("DATEPART(YEAR, ");

		// Token: 0x04000195 RID: 405
		private static readonly ISqlSnippet DPQuarterSnippet = new SqlStringSnippet("DATEPART(QUARTER, ");

		// Token: 0x04000196 RID: 406
		private static readonly ISqlSnippet DPMonthSnippet = new SqlStringSnippet("DATEPART(MONTH, ");

		// Token: 0x04000197 RID: 407
		private static readonly ISqlSnippet DPDaySnippet = new SqlStringSnippet("DATEPART(DAY, ");

		// Token: 0x04000198 RID: 408
		private static readonly ISqlSnippet DPHourSnippet = new SqlStringSnippet("DATEPART(HOUR, ");

		// Token: 0x04000199 RID: 409
		private static readonly ISqlSnippet DPMinuteSnippet = new SqlStringSnippet("DATEPART(MINUTE, ");

		// Token: 0x0400019A RID: 410
		private static readonly ISqlSnippet DPSecondSnippet = new SqlStringSnippet("DATEPART(SECOND, ");

		// Token: 0x0400019B RID: 411
		private static readonly ISqlSnippet DPMillisecondSnippet = new SqlStringSnippet("DATEPART(MILLISECOND, ");

		// Token: 0x0400019C RID: 412
		private static readonly ISqlSnippet DPDayOfYearSnippet = new SqlStringSnippet("DATEPART(DAYOFYEAR, ");

		// Token: 0x0400019D RID: 413
		private static readonly ISqlSnippet DPWeekSnippet = new SqlStringSnippet("DATEPART(WEEK, ");

		// Token: 0x0400019E RID: 414
		private static readonly ISqlSnippet DPWeekDaySnippet = new SqlStringSnippet("DATEPART(WEEKDAY, ");

		// Token: 0x0400019F RID: 415
		private static readonly ISqlSnippet DPDayOfWeekOpenSnippet = new SqlStringSnippet("((DATEPART(WEEKDAY, ");

		// Token: 0x040001A0 RID: 416
		private static readonly ISqlSnippet DayOfWeekCloseSnippet = new SqlStringSnippet(") + @@DATEFIRST - 2) % 7 + 1)");

		// Token: 0x040001A1 RID: 417
		private static readonly ISqlSnippet DateDiffOpenParenSnippet = new SqlStringSnippet("DATEDIFF(");

		// Token: 0x040001A2 RID: 418
		private static readonly ISqlSnippet DateAddOpenParenSnippet = new SqlStringSnippet("DATEADD(");

		// Token: 0x040001A3 RID: 419
		private static readonly ISqlSnippet YearCommaSnippet = new SqlStringSnippet("YEAR, ");

		// Token: 0x040001A4 RID: 420
		private static readonly ISqlSnippet QuarterCommaSnippet = new SqlStringSnippet("QUARTER, ");

		// Token: 0x040001A5 RID: 421
		private static readonly ISqlSnippet MonthCommaSnippet = new SqlStringSnippet("MONTH, ");

		// Token: 0x040001A6 RID: 422
		private static readonly ISqlSnippet DayCommaSnippet = new SqlStringSnippet("DAY, ");

		// Token: 0x040001A7 RID: 423
		private static readonly ISqlSnippet HourCommaSnippet = new SqlStringSnippet("HOUR, ");

		// Token: 0x040001A8 RID: 424
		private static readonly ISqlSnippet MinuteCommaSnippet = new SqlStringSnippet("MINUTE, ");

		// Token: 0x040001A9 RID: 425
		private static readonly ISqlSnippet SecondCommaSnippet = new SqlStringSnippet("SECOND, ");

		// Token: 0x040001AA RID: 426
		private static readonly ISqlSnippet WeekCommaSnippet = new SqlStringSnippet("WEEK, ");

		// Token: 0x040001AB RID: 427
		private static readonly ISqlSnippet SecondsOpenSnippet = new SqlStringSnippet("STR(");

		// Token: 0x040001AC RID: 428
		private static readonly ISqlSnippet SecondsCloseSnippet = new SqlStringSnippet(", 6, 3)");

		// Token: 0x040001AD RID: 429
		private static readonly ISqlSnippet CastFloatAsStringOpenSnippet = new SqlStringSnippet("REPLACE(RTRIM(REPLACE(REPLACE(RTRIM(REPLACE(LTRIM(STR(");

		// Token: 0x040001AE RID: 430
		private static readonly ISqlSnippet CastFloatAsStringCloseSnippet = new SqlStringSnippet(", 312, 16)), '0', ' ')), ' ', '0'), '.', ' ')), ' ', '.')");

		// Token: 0x040001AF RID: 431
		private static readonly ISqlSnippet AsVarChar36CloseParenSnippet = new SqlStringSnippet(" AS VARCHAR(36))");

		// Token: 0x040001B0 RID: 432
		internal static readonly ISqlSnippet AsBigIntCloseParenSnippet = new SqlStringSnippet(" AS BIGINT)");

		// Token: 0x040001B1 RID: 433
		private static readonly ISqlSnippet DecimalOneThousandSnippet = new SqlStringSnippet("1000.0");

		// Token: 0x040001B2 RID: 434
		internal static readonly ISqlSnippet AsTime7CloseParenSnippet = new SqlStringSnippet(" AS TIME(7))");

		// Token: 0x040001B3 RID: 435
		private static readonly ISqlSnippet ConvertDTOfsToUtcOpen = new SqlStringSnippet("SWITCHOFFSET(CAST(");

		// Token: 0x040001B4 RID: 436
		private static readonly ISqlSnippet ConvertDTOfsToUtcClose = new SqlStringSnippet("AS DATETIMEOFFSET(7)), '+00:00')");

		// Token: 0x020000CA RID: 202
		private sealed class IsGuidTraverser : TraverseExpressionAlgorithm<bool>
		{
			// Token: 0x0600072A RID: 1834 RVA: 0x0001BD6C File Offset: 0x00019F6C
			internal static bool IsGuid(Expression expression)
			{
				return expression.GetResultType().DataType == DataType.String && MsSqlFunctionExpression.IsGuidTraverser.Instance.Traverse(expression);
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x00004555 File Offset: 0x00002755
			protected override bool LiteralVisitor(LiteralNode literalNode)
			{
				return false;
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x0001BD96 File Offset: 0x00019F96
			protected override bool AttributeRefVisitor(DsvColumn dsvColumn)
			{
				return dsvColumn.DataType == typeof(Guid);
			}

			// Token: 0x0600072D RID: 1837 RVA: 0x0001BDAD File Offset: 0x00019FAD
			protected override bool EntityRefVisitor(Type[] keyPartTypes)
			{
				throw SQEAssert.AssertFalseAndThrow("EntityRefVisitor must not be called during Guid detection.", Array.Empty<object>());
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x00004555 File Offset: 0x00002755
			protected override bool FunctionVisitor(FunctionNode functionNode)
			{
				return false;
			}

			// Token: 0x0600072F RID: 1839 RVA: 0x0001B2B4 File Offset: 0x000194B4
			private IsGuidTraverser()
			{
			}

			// Token: 0x04000392 RID: 914
			private static readonly MsSqlFunctionExpression.IsGuidTraverser Instance = new MsSqlFunctionExpression.IsGuidTraverser();
		}
	}
}
