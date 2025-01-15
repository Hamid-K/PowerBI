using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Sybase
{
	// Token: 0x0200036E RID: 878
	internal sealed class SybaseAstCreator : DbAstCreator
	{
		// Token: 0x06001F02 RID: 7938 RVA: 0x0004FA88 File Offset: 0x0004DC88
		private SybaseAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0004FA93 File Offset: 0x0004DC93
		public static SybaseAstCreator Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			return new SybaseAstCreator(expression, cursor, externalEnvironment);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected override SqlExpression CreateToSingle(IInvocationExpression invocation)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x000020FA File Offset: 0x000002FA
		protected override SqlDataType[] AdjustArgumentsForType(TypeValue[] types)
		{
			return null;
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0004FAB0 File Offset: 0x0004DCB0
		protected override SqlExpression VisitDateTimeTimeSpanBinaryScalarOperation(SqlExpression dateTime, TimeSpan timeSpan, TypeValue dateTimeType)
		{
			SqlExpression sqlExpression = dateTime;
			if (timeSpan.Days != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.SybaseDay, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Days), sqlExpression);
			}
			if (dateTimeType.TypeKind == ValueKind.Date)
			{
				return sqlExpression;
			}
			if (timeSpan.Hours != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Hour, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Hours), sqlExpression);
			}
			if (timeSpan.Minutes != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Minute, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Minutes), sqlExpression);
			}
			if (timeSpan.Seconds != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Second, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Seconds), sqlExpression);
			}
			if (timeSpan.Milliseconds != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Millisecond, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Milliseconds), sqlExpression);
			}
			return sqlExpression;
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x0004FB70 File Offset: 0x0004DD70
		protected override SqlExpression CreateDateTimeStartOfDay(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			IExpression expression = invocation.Arguments[0];
			switch (base.GetType(expression).TypeKind)
			{
			case ValueKind.Date:
				return sqlExpression;
			case ValueKind.DateTime:
				return new CastCall
				{
					Type = SqlDataType.DateTime,
					Expression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { sqlExpression })
				};
			case ValueKind.DateTimeZone:
				return this.CreateStartOfDateTimeOffset(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { sqlExpression }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DatePartSqlString), new SqlExpression[]
				{
					DatePart.TzOffset,
					sqlExpression
				}));
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0004FC44 File Offset: 0x0004DE44
		private SqlExpression CreateStartOfDateTimeOffset(SqlExpression datePart, SqlExpression dateTimeZone)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ToDateTimeOffsetSqlString), new SqlExpression[] { datePart, dateTimeZone });
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0004FC64 File Offset: 0x0004DE64
		protected override SqlExpression VisitDateTimeDurationBinaryScalarOperation(SqlExpression dateTime, SqlExpression duration, TypeValue dateTimeType)
		{
			if (dateTimeType.TypeKind == ValueKind.Date)
			{
				SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(duration, base.TicksPerDay));
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.SybaseDay, sqlExpression, dateTime);
			}
			return this.ConvertTicksToDateTime(dateTime, duration);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0004FCA4 File Offset: 0x0004DEA4
		protected override SqlExpression CreateAddOperation(IBinaryExpression add)
		{
			IExpression left = add.Left;
			if (base.GetType(left).TypeKind == ValueKind.Text)
			{
				return this.CreateBinaryScalarOperation(BinaryScalarOperator.Concatenate, add);
			}
			return this.CreateBinaryScalarOperation(BinaryScalarOperator.Add, add);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0004FCD8 File Offset: 0x0004DED8
		protected override SqlExpression CreateBinaryFromText(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return this.Convert(SqlDataType.VarBinary, sqlExpression, 2);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0004FD08 File Offset: 0x0004DF08
		protected override SqlExpression CreateDateTimeAddMonths(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.SybaseMonth, sqlExpression2, sqlExpression);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0004FD48 File Offset: 0x0004DF48
		protected override SqlExpression CreateDateTimeAddYears(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.SybaseYear, sqlExpression2, sqlExpression);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0004FD88 File Offset: 0x0004DF88
		protected override SqlExpression CreateDivideOperation(IBinaryExpression divide)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(divide.Left);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(divide.Right);
			return new BinaryScalarOperation(this.Convert(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(38, 6), sqlExpression), BinaryScalarOperator.Divide, this.Convert(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(38, 6), sqlExpression2));
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0004FDD4 File Offset: 0x0004DFD4
		private SqlExpression ConvertDateTimeToTicks(SqlExpression dateTime, SqlDataType format)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.SybaseDay, base.GetBaseDateTime(format), dateTime);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.SybaseDay, sqlExpression, base.GetBaseDateTime(format));
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.Millisecond, sqlExpression2, dateTime);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Millisecond, sqlExpression3, sqlExpression2);
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.SybaseMicrosecond, sqlExpression4, dateTime);
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.Convert(SqlDataType.BigInt, sqlExpression5), base.TicksPerUs), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.Convert(SqlDataType.BigInt, sqlExpression3), base.TicksPerMs), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.Convert(SqlDataType.BigInt, sqlExpression), base.TicksPerDay)));
			return base.Select(sqlExpression6, Alias.ScalarColumn).ToPagingQuerySpecification();
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0004FE98 File Offset: 0x0004E098
		private SqlExpression ConvertTicksToDateTime(SqlExpression baseDateTime, SqlExpression ticks)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(ticks, base.TicksPerDay));
			BinaryScalarOperation binaryScalarOperation = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(ticks, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.TicksPerDay, sqlExpression));
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.SybaseDay, sqlExpression, baseDateTime);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(binaryScalarOperation, base.TicksPerMs));
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(binaryScalarOperation, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.TicksPerMs, sqlExpression3));
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Millisecond, sqlExpression3, sqlExpression2);
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(sqlExpression4, base.TicksPerUs);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.SybaseMicrosecond, sqlExpression6, sqlExpression5);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0004FF20 File Offset: 0x0004E120
		protected override SqlExpression CreateListAverage(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			SqlDataType sqlDataType = this.dbEnvironment.GetSqlScalarType(base.GetType(expression).AsListType.ItemType);
			if (sqlDataType == SqlDataType.DateTime2)
			{
				sqlDataType = SqlDataType.DateTime;
			}
			if (sqlDataType == SqlDataType.DateTime || sqlDataType == SqlDataType.DateTimeOffset || sqlDataType == SqlDataType.Time)
			{
				bool flag = false;
				if (sqlDataType == SqlDataType.Time)
				{
					flag = true;
					sqlDataType = SqlDataType.DateTime;
				}
				SqlExpression sqlExpression = base.CreateListAggregateInput(expression);
				if (flag)
				{
					sqlExpression = this.Convert(sqlDataType, sqlExpression);
				}
				SqlExpression sqlExpression2 = base.LiftForGroup(this.ConvertDateTimeToTicks(sqlExpression, sqlDataType));
				SqlExpression baseDateTime = base.GetBaseDateTime(sqlDataType);
				SqlExpression sqlExpression3 = this.ConvertTicksToDateTime(baseDateTime, this.Convert(SqlDataType.BigInt, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg(sqlExpression2)));
				return base.Select(flag ? this.Convert(SqlDataType.Time, sqlExpression3) : sqlExpression3, Alias.ScalarColumn).ToPagingQuerySpecification();
			}
			SqlExpression sqlExpression4 = base.CreateListAggregate(expression, (SqlExpression e) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg(this.Convert(SqlDataType.Float, e)));
			if (sqlDataType != SqlDataType.Float)
			{
				sqlExpression4 = this.Convert(sqlDataType, sqlExpression4);
			}
			return sqlExpression4;
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0005002E File Offset: 0x0004E22E
		protected override SqlExpression ConvertNumberToDate(SqlExpression number)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.SybaseDay, number, base.BaseOADateTime);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x00050044 File Offset: 0x0004E244
		protected override SqlExpression ConvertNumberToDateTime(SqlExpression number)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RoundSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(number, SqlConstant.SecondsPerDay),
				SqlConstant.Zero
			});
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(number, BinaryLogicalOperator.LessThan, SqlConstant.Zero)).Then(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.CreateNumberMod(sqlExpression, SqlConstant.SecondsPerDay, null), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(2))))
				.Else(sqlExpression);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.SybaseDay, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(sqlExpression2, SqlConstant.SecondsPerDay), base.BaseOADateTime);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Second, this.CreateNumberMod(sqlExpression2, SqlConstant.SecondsPerDay, null), sqlExpression3);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x00050101 File Offset: 0x0004E301
		protected override SqlExpression ConvertDateToNumber(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.SybaseDay, base.BaseOADateTime, expression);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x00050114 File Offset: 0x0004E314
		protected override SqlExpression ConvertDateTimeToNumber(SqlExpression expression)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.SybaseDay, base.BaseOADateTime, expression);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.SybaseDay, sqlExpression, base.BaseOADateTime);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.Millisecond, sqlExpression2, expression);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Millisecond, sqlExpression3, sqlExpression2);
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.SybaseMicrosecond, sqlExpression4, expression);
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.Convert(SqlDataType.BigInt, sqlExpression5), base.TicksPerUs), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.Convert(SqlDataType.BigInt, sqlExpression3), base.TicksPerMs)), base.TicksPerDay);
			SqlExpression sqlExpression7 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(expression, BinaryLogicalOperator.LessThan, base.BaseOADateTime)).Then(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(-1.0, true), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlConstant.Two, sqlExpression6)))
				.Else(sqlExpression6);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression7);
		}

		// Token: 0x06001F16 RID: 7958 RVA: 0x0005020C File Offset: 0x0004E40C
		protected override SqlExpression CreateDurationTotalDays(IInvocationExpression invocation)
		{
			IBinaryExpression binaryExpression;
			if (DbAstCreator.TryAsBinaryExpression(invocation.Arguments[0], out binaryExpression))
			{
				SqlExpression sqlExpression = base.CreateScalarExpression(binaryExpression.Left);
				SqlExpression sqlExpression2 = base.CreateScalarExpression(binaryExpression.Right);
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateDiffSqlString), new SqlExpression[]
				{
					new SqlConstant(ConstantType.DoubleQuotesString, SqlLanguageStrings.DaySqlString.String),
					sqlExpression2,
					sqlExpression
				});
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06001F17 RID: 7959 RVA: 0x0005028C File Offset: 0x0004E48C
		protected override SqlExpression CreateTextTrim(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.TrimSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06001F18 RID: 7960 RVA: 0x000502A4 File Offset: 0x0004E4A4
		protected override SqlExpression CreateTextContains(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			IExpression expression = arguments[0];
			IExpression expression2 = arguments[1];
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CharIndexSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(expression2),
				base.CreateScalarExpression(expression)
			}), BinaryLogicalOperator.GreaterThan, SqlConstant.Zero);
		}

		// Token: 0x06001F19 RID: 7961 RVA: 0x00046303 File Offset: 0x00044503
		protected override SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.Atan2SqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06001F1A RID: 7962 RVA: 0x000502FC File Offset: 0x0004E4FC
		protected override SqlExpression CreateToText(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			if (base.GetType(expression).TypeKind != ValueKind.Logical)
			{
				return this.Convert(SqlDataType.VarChar, base.CreateScalarExpression(expression));
			}
			SqlExpression sqlExpression = base.GetValue(expression);
			if (sqlExpression is Condition)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When((Condition)sqlExpression).Then(SqlConstant.StringTrue)
					.When(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Not((Condition)sqlExpression))
					.Then(SqlConstant.StringFalse)
					.Else(SqlConstant.Null);
			}
			sqlExpression = base.CreateScalarExpression(expression);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, SqlConstant.One)).Then(SqlConstant.StringTrue)
				.When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, SqlConstant.Zero))
				.Then(SqlConstant.StringFalse)
				.Else(SqlConstant.Null);
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x00046609 File Offset: 0x00044809
		protected override SqlExpression Len(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CharLengthSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected override SqlExpression CastToSingle(SqlExpression expression)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}
	}
}
