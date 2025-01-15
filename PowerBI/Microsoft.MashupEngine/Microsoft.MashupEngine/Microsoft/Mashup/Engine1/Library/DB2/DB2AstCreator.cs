using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.DB2
{
	// Token: 0x02000CC2 RID: 3266
	internal sealed class DB2AstCreator : DbAstCreator
	{
		// Token: 0x0600582C RID: 22572 RVA: 0x0004FA88 File Offset: 0x0004DC88
		private DB2AstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x0600582D RID: 22573 RVA: 0x000020FA File Offset: 0x000002FA
		protected override SqlDataType[] AdjustArgumentsForType(TypeValue[] types)
		{
			return null;
		}

		// Token: 0x0600582E RID: 22574 RVA: 0x00133D6E File Offset: 0x00131F6E
		public static DB2AstCreator Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			return new DB2AstCreator(expression, cursor, externalEnvironment);
		}

		// Token: 0x0600582F RID: 22575 RVA: 0x00133D78 File Offset: 0x00131F78
		protected override SqlExpression VisitDateTimeTimeSpanBinaryScalarOperation(SqlExpression dateTime, TimeSpan timeSpan, TypeValue dateTimeType)
		{
			SqlExpression sqlExpression = dateTime;
			if (timeSpan.Days != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, DB2AstCreator.IntervalExpression.Days(DB2AstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Days))));
			}
			if (dateTimeType.TypeKind == ValueKind.Date)
			{
				return sqlExpression;
			}
			if (timeSpan.Hours != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, DB2AstCreator.IntervalExpression.Hours(DB2AstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Hours))));
			}
			if (timeSpan.Minutes != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, DB2AstCreator.IntervalExpression.Minutes(DB2AstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Minutes))));
			}
			if (timeSpan.Seconds != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, DB2AstCreator.IntervalExpression.Seconds(DB2AstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Seconds))));
			}
			long num = timeSpan.Ticks % 10000000L;
			if (num != 0L)
			{
				long num2 = num / 10L;
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, DB2AstCreator.IntervalExpression.Microseconds(DB2AstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(num2))));
			}
			return sqlExpression;
		}

		// Token: 0x06005830 RID: 22576 RVA: 0x00133E5C File Offset: 0x0013205C
		protected override SqlExpression CreateDateTimeStartOfDay(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			IExpression expression = invocation.Arguments[0];
			ValueKind typeKind = base.GetType(expression).TypeKind;
			if (typeKind == ValueKind.Date)
			{
				return sqlExpression;
			}
			if (typeKind == ValueKind.DateTime)
			{
				return new CastCall
				{
					Type = SqlDataType.DateTime,
					Expression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { sqlExpression })
				};
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06005831 RID: 22577 RVA: 0x00133EE4 File Offset: 0x001320E4
		protected override SqlExpression VisitDateTimeDurationBinaryScalarOperation(SqlExpression dateTime, SqlExpression duration, TypeValue dateTimeType)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(duration, base.TicksPerDay);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(dateTime, DB2AstCreator.IntervalExpression.Days(DB2AstCreator.CastToInt(sqlExpression)));
			if (dateTimeType.TypeKind == ValueKind.Date)
			{
				return sqlExpression2;
			}
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[] { duration, base.TicksPerDay }), base.TicksPerSecond);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[] { duration, base.TicksPerSecond }), base.TicksPerUs);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, DB2AstCreator.IntervalExpression.Seconds(DB2AstCreator.CastToInt(sqlExpression3))), DB2AstCreator.IntervalExpression.Microseconds(DB2AstCreator.CastToInt(sqlExpression4)));
		}

		// Token: 0x06005832 RID: 22578 RVA: 0x00133F98 File Offset: 0x00132198
		protected override SqlExpression CreateAddOperation(IBinaryExpression add)
		{
			IExpression left = add.Left;
			if (base.GetType(left).TypeKind == ValueKind.Text)
			{
				return this.CreateBinaryScalarOperation(BinaryScalarOperator.Concatenate, add);
			}
			return this.CreateBinaryScalarOperation(BinaryScalarOperator.Add, add);
		}

		// Token: 0x06005833 RID: 22579 RVA: 0x00133FCC File Offset: 0x001321CC
		protected override SqlExpression CreateListAverage(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue itemType = base.GetType(expression).AsListType.ItemType;
			SqlExpression sqlExpression;
			switch (itemType.TypeKind)
			{
			case ValueKind.Time:
			case ValueKind.DateTime:
				sqlExpression = DB2AstCreator.DateTimeAggregateBase;
				break;
			case ValueKind.Date:
				sqlExpression = DB2AstCreator.DateAggregateBase;
				break;
			case ValueKind.DateTimeZone:
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(DB2AstCreator.BaseDateTime.AddDefaultTimeZone(base.ExternalEnvironment.Host));
				break;
			default:
				return base.CreateListAverage(invocation);
			}
			SqlExpression sqlExpression2 = base.CreateListAggregateInput(expression);
			if (itemType.TypeKind == ValueKind.Time)
			{
				sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TimestampSqlString), new SqlExpression[]
				{
					DB2AstCreator.BaseDateForTimeAdjustment,
					sqlExpression2
				});
			}
			SqlExpression sqlExpression3 = base.LiftForGroup(this.ConvertDateTimeToMicroseconds(base.CreateListAggregateInput(expression), sqlExpression));
			SqlExpression sqlExpression4 = this.ConvertMicrosecondsToDateTime(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.BigIntSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg(sqlExpression3) }), sqlExpression);
			if (itemType.TypeKind == ValueKind.Time)
			{
				return base.Select(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TimeSqlString), new SqlExpression[] { sqlExpression4 }), Alias.ScalarColumn).ToPagingQuerySpecification();
			}
			return base.Select(sqlExpression4, Alias.ScalarColumn).ToPagingQuerySpecification();
		}

		// Token: 0x06005834 RID: 22580 RVA: 0x0013410F File Offset: 0x0013230F
		protected override SqlExpression ConvertNumberToDate(SqlExpression number)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.BaseOADateTime, DB2AstCreator.IntervalExpression.Days(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TruncateSqlString), new SqlExpression[] { number })));
		}

		// Token: 0x06005835 RID: 22581 RVA: 0x0013413C File Offset: 0x0013233C
		protected override SqlExpression ConvertNumberToDateTime(SqlExpression number)
		{
			SqlExpression sqlExpression = this.ConvertNumberToDate(number);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Abs(number), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Abs(number)));
			SqlExpression sqlExpression3 = DB2AstCreator.IntervalExpression.Seconds(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IntSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(sqlExpression2, SqlConstant.SecondsPerDay) }));
			SqlExpression sqlExpression4 = DB2AstCreator.IntervalExpression.Microseconds(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IntSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(sqlExpression2, base.MicrosecondsPerDay),
				base.MicrosecondsPerSecond
			}) }));
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression3), sqlExpression4);
		}

		// Token: 0x06005836 RID: 22582 RVA: 0x001341EC File Offset: 0x001323EC
		private SqlExpression ConvertDateTimeToMicroseconds(SqlExpression dateTime, SqlExpression baseValue)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaysSqlString), new SqlExpression[] { dateTime }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaysSqlString), new SqlExpression[] { baseValue })), base.MicrosecondsPerDay);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString), new SqlExpression[] { dateTime }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString), new SqlExpression[] { baseValue })), base.MicrosecondsPerHour);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString), new SqlExpression[] { dateTime }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString), new SqlExpression[] { baseValue })), base.MicrosecondsPerMinute);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SecondSqlString), new SqlExpression[] { dateTime }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SecondSqlString), new SqlExpression[] { baseValue })), base.MicrosecondsPerSecond);
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MicrosecondSqlString), new SqlExpression[] { dateTime }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MicrosecondSqlString), new SqlExpression[] { baseValue }));
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression4, sqlExpression5))));
		}

		// Token: 0x06005837 RID: 22583 RVA: 0x00134358 File Offset: 0x00132558
		private SqlExpression ConvertMicrosecondsToDateTime(SqlExpression totalMicroseconds, SqlExpression baseValue)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(totalMicroseconds, base.MicrosecondsPerDay);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[] { totalMicroseconds, base.MicrosecondsPerDay }), base.MicrosecondsPerSecond);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[] { totalMicroseconds, base.MicrosecondsPerSecond });
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(baseValue, DB2AstCreator.IntervalExpression.Days(DB2AstCreator.CastToInt(sqlExpression))), DB2AstCreator.IntervalExpression.Seconds(DB2AstCreator.CastToInt(sqlExpression2))), DB2AstCreator.IntervalExpression.Microseconds(DB2AstCreator.CastToInt(sqlExpression3)));
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x00132041 File Offset: 0x00130241
		protected override SqlExpression ConvertDateToNumber(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaysSqlString), new SqlExpression[] { expression }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaysSqlString), new SqlExpression[] { base.BaseOADateTime }));
		}

		// Token: 0x06005839 RID: 22585 RVA: 0x001343F4 File Offset: 0x001325F4
		protected override SqlExpression ConvertDateTimeToNumber(SqlExpression expression)
		{
			SqlExpression sqlExpression = this.ConvertDateToNumber(expression);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.CastToBigInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString), new SqlExpression[] { expression })), base.MicrosecondsPerHour);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.CastToBigInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString), new SqlExpression[] { expression })), base.MicrosecondsPerMinute);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.CastToBigInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SecondSqlString), new SqlExpression[] { expression })), base.MicrosecondsPerSecond);
			SqlExpression sqlExpression5 = this.CastToBigInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MicrosecondSqlString), new SqlExpression[] { expression }));
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression4, sqlExpression5))), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(86400000000.0, true));
			SqlExpression sqlExpression7 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateOADateTimeSignExpression(expression), sqlExpression6);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression7);
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x001344F0 File Offset: 0x001326F0
		protected override SqlExpression CreateTextContains(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LocateSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[1]),
				base.CreateScalarExpression(arguments[0])
			}), BinaryLogicalOperator.GreaterThan, SqlConstant.Zero);
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x0004628D File Offset: 0x0004448D
		protected override SqlExpression CreateDateTimeAddMonths(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.AddMonthsSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x0600583C RID: 22588 RVA: 0x00134544 File Offset: 0x00132744
		protected override SqlExpression CreateDateTimeAddYears(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = new BinaryScalarOperation(base.CreateScalarExpression(invocation.Arguments[1]), BinaryScalarOperator.Multiply, SqlConstant.Twelve);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.AddMonthsSqlString), new SqlExpression[] { sqlExpression, sqlExpression2 });
		}

		// Token: 0x0600583D RID: 22589 RVA: 0x001345A0 File Offset: 0x001327A0
		protected override SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.Atan2SqlString), new SqlExpression[] { sqlExpression2, sqlExpression });
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x001345F0 File Offset: 0x001327F0
		protected override SqlExpression CreateNumberMod(SqlExpression number, SqlExpression divisor, IConstantExpression precision = null)
		{
			Func<SqlExpression, SqlExpression> func = base.CreateNumericCastFromPrecision(precision);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(func(number), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.FloorSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(func(number), func(divisor)) }), divisor));
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x00046345 File Offset: 0x00044545
		protected override SqlExpression CreateNumberNaturalLogarithm(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.LnSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x00134644 File Offset: 0x00132844
		protected override SqlExpression CreateToText(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			if (base.GetType(expression).TypeKind == ValueKind.Logical)
			{
				SqlExpression sqlExpression = base.GetValue(expression);
				SqlConstant sqlConstant;
				SqlConstant sqlConstant2;
				if (sqlExpression is Condition)
				{
					sqlConstant = SqlConstant.BooleanTrue;
					sqlConstant2 = SqlConstant.BooleanFalse;
				}
				else
				{
					sqlExpression = base.CreateScalarExpression(expression);
					sqlConstant = SqlConstant.One;
					sqlConstant2 = SqlConstant.Zero;
				}
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, sqlConstant)).Then(SqlConstant.StringTrue)
					.When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, sqlConstant2))
					.Then(SqlConstant.StringFalse)
					.Else(SqlConstant.Null);
			}
			return base.CreateFunctionCall(SqlLanguageStrings.CharSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06005841 RID: 22593 RVA: 0x0008468F File Offset: 0x0008288F
		protected override SqlExpression Len(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LengthSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06005842 RID: 22594 RVA: 0x00134710 File Offset: 0x00132910
		protected override SqlExpression CreateDivideOperation(IBinaryExpression divide)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(divide.Left);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(divide.Right);
			Func<SqlExpression, SqlExpression> func = (SqlExpression e) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DoubleSqlString), new SqlExpression[] { e });
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(func(sqlExpression), func(sqlExpression2));
		}

		// Token: 0x06005843 RID: 22595 RVA: 0x0012EA16 File Offset: 0x0012CC16
		protected override SqlExpression CreateBinaryFromText(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.VarBinarySqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06005844 RID: 22596 RVA: 0x001322EC File Offset: 0x001304EC
		protected override SqlExpression CastToSingle(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RealSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06005845 RID: 22597 RVA: 0x00132307 File Offset: 0x00130507
		protected override SqlExpression CastToDouble(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DoubleSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06005846 RID: 22598 RVA: 0x0013476A File Offset: 0x0013296A
		protected override SqlExpression CastToDecimal(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DecimalSqlString), new SqlExpression[]
			{
				expression,
				DB2AstCreator.decimalPrecision,
				DB2AstCreator.decimalScale
			});
		}

		// Token: 0x06005847 RID: 22599 RVA: 0x0013234D File Offset: 0x0013054D
		protected override SqlExpression CastToBigInt(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.BigIntSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06005848 RID: 22600 RVA: 0x00132368 File Offset: 0x00130568
		private static SqlExpression CastToInt(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IntSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06005849 RID: 22601 RVA: 0x00134798 File Offset: 0x00132998
		protected override SqlExpression CreateDurationTotalDays(IInvocationExpression invocation)
		{
			IBinaryExpression binaryExpression;
			if (DbAstCreator.TryAsBinaryExpression(invocation.Arguments[0], out binaryExpression))
			{
				SqlExpression sqlExpression = base.CreateScalarExpression(binaryExpression.Left);
				SqlExpression sqlExpression2 = base.CreateScalarExpression(binaryExpression.Right);
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaysSqlString), new SqlExpression[] { sqlExpression }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaysSqlString), new SqlExpression[] { sqlExpression2 }));
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x040031C4 RID: 12740
		private static readonly SqlConstant decimalPrecision = new SqlConstant(ConstantType.Integer, "29");

		// Token: 0x040031C5 RID: 12741
		private static readonly SqlConstant decimalScale = new SqlConstant(ConstantType.Integer, "10");

		// Token: 0x040031C6 RID: 12742
		private static readonly DateTime BaseDateTime = new DateTime(2000, 1, 1);

		// Token: 0x040031C7 RID: 12743
		private static readonly SqlExpression DateAggregateBase = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(DB2AstCreator.BaseDateTime) });

		// Token: 0x040031C8 RID: 12744
		private static readonly SqlExpression DateTimeAggregateBase = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(DB2AstCreator.BaseDateTime);

		// Token: 0x040031C9 RID: 12745
		private static readonly SqlExpression BaseDateForTimeAdjustment = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("2000-01-01") });

		// Token: 0x02000CC3 RID: 3267
		private sealed class IntervalExpression : SqlExpression
		{
			// Token: 0x0600584B RID: 22603 RVA: 0x001348B7 File Offset: 0x00132AB7
			private IntervalExpression(SqlExpression expression, ConstantSqlString intervalUnit)
			{
				this.expression = expression;
				this.intervalUnit = intervalUnit;
			}

			// Token: 0x0600584C RID: 22604 RVA: 0x001348CD File Offset: 0x00132ACD
			public static DB2AstCreator.IntervalExpression Days(SqlExpression expression)
			{
				return new DB2AstCreator.IntervalExpression(expression, SqlLanguageStrings.DaysSqlString);
			}

			// Token: 0x0600584D RID: 22605 RVA: 0x001348DA File Offset: 0x00132ADA
			public static DB2AstCreator.IntervalExpression Hours(SqlExpression expression)
			{
				return new DB2AstCreator.IntervalExpression(expression, SqlLanguageStrings.HoursSqlString);
			}

			// Token: 0x0600584E RID: 22606 RVA: 0x001348E7 File Offset: 0x00132AE7
			public static DB2AstCreator.IntervalExpression Minutes(SqlExpression expression)
			{
				return new DB2AstCreator.IntervalExpression(expression, SqlLanguageStrings.MinutesSqlString);
			}

			// Token: 0x0600584F RID: 22607 RVA: 0x001348F4 File Offset: 0x00132AF4
			public static DB2AstCreator.IntervalExpression Seconds(SqlExpression expression)
			{
				return new DB2AstCreator.IntervalExpression(expression, SqlLanguageStrings.SecondsSqlString);
			}

			// Token: 0x06005850 RID: 22608 RVA: 0x00134901 File Offset: 0x00132B01
			public static DB2AstCreator.IntervalExpression Microseconds(SqlExpression expression)
			{
				return new DB2AstCreator.IntervalExpression(expression, SqlLanguageStrings.MicrosecondsSqlString);
			}

			// Token: 0x17001A7A RID: 6778
			// (get) Token: 0x06005851 RID: 22609 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06005852 RID: 22610 RVA: 0x0013490E File Offset: 0x00132B0E
			public override void WriteCreateScript(ScriptWriter writer)
			{
				this.expression.WriteCreateScript(writer);
				writer.WriteSpaceBefore(this.intervalUnit);
			}

			// Token: 0x040031CA RID: 12746
			private readonly ConstantSqlString intervalUnit;

			// Token: 0x040031CB RID: 12747
			private readonly SqlExpression expression;
		}
	}
}
