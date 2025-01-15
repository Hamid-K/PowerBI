using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000CAD RID: 3245
	internal sealed class MsDb2AstCreator : DrdaAstCreator
	{
		// Token: 0x060057B4 RID: 22452 RVA: 0x001304CC File Offset: 0x0012E6CC
		private MsDb2AstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x060057B5 RID: 22453 RVA: 0x00131668 File Offset: 0x0012F868
		protected override Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> functions = base.GetFunctions();
			functions.Add(Library.Binary.Length, base.CreateFunctionCall(SqlLanguageStrings.LengthSqlString, Array.Empty<SqlExpression>()));
			functions.Add(BinaryOperator.BitwiseAnd, base.CreateFunctionCall(SqlLanguageStrings.BitAndSqlString, Array.Empty<SqlExpression>()));
			functions.Add(BinaryOperator.BitwiseOr, base.CreateFunctionCall(SqlLanguageStrings.BitOrSqlString, Array.Empty<SqlExpression>()));
			functions.Add(BinaryOperator.BitwiseXor, base.CreateFunctionCall(SqlLanguageStrings.BitXorSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Text.PositionOf, new Func<IInvocationExpression, SqlExpression>(this.CreatePositionOf));
			functions.Add(Library.Text.Start, base.CreateFunctionCall(SqlLanguageStrings.LeftSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Text.End, base.CreateFunctionCall(SqlLanguageStrings.RightSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Text.Replace, base.CreateFunctionCall(SqlLanguageStrings.ReplaceSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Text.Middle, new Func<IInvocationExpression, SqlExpression>(this.CreateTextMiddle));
			functions.Add(TypeSpecificFunction.NumberRandomBetween, new Func<IInvocationExpression, SqlExpression>(this.CreateRandomBetween));
			functions.Add(Library.Number.Cosh, base.CreateFunctionCall(SqlLanguageStrings.CoshSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Number.Sinh, base.CreateFunctionCall(SqlLanguageStrings.SinhSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Number.Tanh, base.CreateFunctionCall(SqlLanguageStrings.TanhSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Number.Ln, base.CreateFunctionCall(SqlLanguageStrings.LnSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Number.IsEven, new Func<IInvocationExpression, SqlExpression>(this.CreateIsEven));
			functions.Add(Library.Number.IsOdd, new Func<IInvocationExpression, SqlExpression>(this.CreateIsOdd));
			functions.Add(Library.Date.AddDays, new Func<IInvocationExpression, SqlExpression>(this.CreateAddDays));
			functions.Add(Library.Date.AddWeeks, new Func<IInvocationExpression, SqlExpression>(this.CreateAddWeeks));
			functions.Add(Library.Date.AddQuarters, new Func<IInvocationExpression, SqlExpression>(this.CreateAddQuarters));
			functions.Add(Library.Date.Day, base.CreateFunctionCall(SqlLanguageStrings.DaySqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Date.Month, base.CreateFunctionCall(SqlLanguageStrings.MonthSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Date.Year, base.CreateFunctionCall(SqlLanguageStrings.YearSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Date.DayOfYear, base.CreateFunctionCall(SqlLanguageStrings.DayOfYearSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Date.QuarterOfYear, base.CreateFunctionCall(SqlLanguageStrings.QuarterSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Time.Hour, base.CreateFunctionCall(SqlLanguageStrings.HourSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Time.Minute, base.CreateFunctionCall(SqlLanguageStrings.MinuteSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Time.Second, base.CreateFunctionCall(SqlLanguageStrings.SecondSqlString, Array.Empty<SqlExpression>()));
			functions.Add(CultureSpecificFunction.DateDayOfWeek, new Func<IInvocationExpression, SqlExpression>(this.CreateDateDayOfWeek));
			functions.Add(CultureSpecificFunction.DateWeekOfYear, base.CreateFunctionCall(SqlLanguageStrings.WeekSqlString, Array.Empty<SqlExpression>()));
			return functions;
		}

		// Token: 0x060057B6 RID: 22454 RVA: 0x00131968 File Offset: 0x0012FB68
		private SqlExpression CreatePositionOf(IInvocationExpression invocation)
		{
			SqlExpression[] array = invocation.Arguments.Select((IExpression arg) => base.CreateScalarExpression(arg)).ToArray<SqlExpression>();
			return new BinaryScalarOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.PosstrSqlString), array), BinaryScalarOperator.Subtract, SqlConstant.One);
		}

		// Token: 0x060057B7 RID: 22455 RVA: 0x001319B0 File Offset: 0x0012FBB0
		private SqlExpression CreateTextMiddle(IInvocationExpression invocation)
		{
			List<SqlExpression> list = new List<SqlExpression>
			{
				base.CreateScalarExpression(invocation.Arguments[0]),
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.CreateScalarExpression(invocation.Arguments[1]), SqlConstant.One)
			};
			if (invocation.Arguments.Count == 3)
			{
				list.Add(base.CreateScalarExpression(invocation.Arguments[2]));
			}
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SubstrSqlString), list.ToArray());
		}

		// Token: 0x060057B8 RID: 22456 RVA: 0x00131A38 File Offset: 0x0012FC38
		private SqlExpression CreateRandomBetween(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RandSqlString), Array.Empty<SqlExpression>()), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression2, sqlExpression)), sqlExpression);
		}

		// Token: 0x060057B9 RID: 22457 RVA: 0x00131A91 File Offset: 0x0012FC91
		public static DbAstCreator Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			return new MsDb2AstCreator(expression, cursor, externalEnvironment);
		}

		// Token: 0x060057BA RID: 22458 RVA: 0x00131A9C File Offset: 0x0012FC9C
		protected override SqlExpression VisitDateTimeTimeSpanBinaryScalarOperation(SqlExpression dateTime, TimeSpan timeSpan, TypeValue dateTimeType)
		{
			SqlExpression sqlExpression = dateTime;
			if (timeSpan.Days != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, MsDb2AstCreator.MsDb2IntervalExpression.Days(MsDb2AstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Days))));
			}
			if (dateTimeType.TypeKind == ValueKind.Date)
			{
				return sqlExpression;
			}
			if (timeSpan.Hours != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, MsDb2AstCreator.MsDb2IntervalExpression.Hours(MsDb2AstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Hours))));
			}
			if (timeSpan.Minutes != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, MsDb2AstCreator.MsDb2IntervalExpression.Minutes(MsDb2AstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Minutes))));
			}
			if (timeSpan.Seconds != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, MsDb2AstCreator.MsDb2IntervalExpression.Seconds(MsDb2AstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Seconds))));
			}
			long num = timeSpan.Ticks % 10000000L;
			if (num != 0L)
			{
				long num2 = num / 10L;
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, MsDb2AstCreator.MsDb2IntervalExpression.Microseconds(MsDb2AstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(num2))));
			}
			return sqlExpression;
		}

		// Token: 0x060057BB RID: 22459 RVA: 0x00131B80 File Offset: 0x0012FD80
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
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TimestampSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { sqlExpression }) });
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x060057BC RID: 22460 RVA: 0x00131C08 File Offset: 0x0012FE08
		protected override SqlExpression VisitDateTimeDurationBinaryScalarOperation(SqlExpression dateTime, SqlExpression duration, TypeValue dateTimeType)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(duration, base.TicksPerDay);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(dateTime, MsDb2AstCreator.MsDb2IntervalExpression.Days(MsDb2AstCreator.CastToInt(sqlExpression)));
			if (dateTimeType.TypeKind == ValueKind.Date)
			{
				return sqlExpression2;
			}
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[] { duration, base.TicksPerDay }), base.TicksPerSecond);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[] { duration, base.TicksPerSecond }), base.TicksPerUs);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, MsDb2AstCreator.MsDb2IntervalExpression.Seconds(MsDb2AstCreator.CastToInt(sqlExpression3))), MsDb2AstCreator.MsDb2IntervalExpression.Microseconds(MsDb2AstCreator.CastToInt(sqlExpression4)));
		}

		// Token: 0x060057BD RID: 22461 RVA: 0x00131CBC File Offset: 0x0012FEBC
		private SqlExpression CreateAddDays(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.CreateScalarExpression(invocation.Arguments[0]), DrdaAstCreator.IntervalExpression.Days(base.CreateScalarExpression(invocation.Arguments[1])));
		}

		// Token: 0x060057BE RID: 22462 RVA: 0x00131CEC File Offset: 0x0012FEEC
		private SqlExpression CreateAddWeeks(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.CreateScalarExpression(invocation.Arguments[0]), DrdaAstCreator.IntervalExpression.Days(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateScalarExpression(invocation.Arguments[1]), SqlConstant.Seven)));
		}

		// Token: 0x060057BF RID: 22463 RVA: 0x00131D28 File Offset: 0x0012FF28
		private SqlExpression CreateAddQuarters(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.AddMonthsSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(invocation.Arguments[0]),
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateScalarExpression(invocation.Arguments[1]), SqlConstant.Three)
			});
		}

		// Token: 0x060057C0 RID: 22464 RVA: 0x00131D80 File Offset: 0x0012FF80
		private SqlExpression CreateDateDayOfWeek(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			int count = invocation.Arguments.Count;
			if (count == 1)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DayOfWeekSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.One);
			}
			if (count != 2)
			{
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DayOfWeekSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.Six), sqlExpression2),
				SqlConstant.Seven
			});
		}

		// Token: 0x060057C1 RID: 22465 RVA: 0x00131E4C File Offset: 0x0013004C
		protected override SqlExpression CreateToDate(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			switch (type.TypeKind)
			{
			case ValueKind.Null:
			case ValueKind.DateTime:
				return MsDb2AstCreator.CastToDate(sqlExpression);
			case ValueKind.Date:
				return sqlExpression;
			case ValueKind.Number:
				return this.ConvertNumberToDate(sqlExpression);
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x060057C2 RID: 22466 RVA: 0x00131EC0 File Offset: 0x001300C0
		protected override SqlExpression CreateToDateTime(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			switch (type.TypeKind)
			{
			case ValueKind.Null:
			case ValueKind.Date:
			case ValueKind.DateTime:
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.Timestamp_iso_SqlString), new SqlExpression[] { sqlExpression });
			case ValueKind.Time:
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TimestampSqlString), new SqlExpression[] { base.BaseOADate, sqlExpression });
			case ValueKind.Number:
				return this.ConvertNumberToDateTime(sqlExpression);
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x060057C3 RID: 22467 RVA: 0x00131F67 File Offset: 0x00130167
		protected override SqlExpression ConvertNumberToDate(SqlExpression number)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.BaseOADateTime, DrdaAstCreator.IntervalExpression.Days(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TruncateSqlString), new SqlExpression[] { number })));
		}

		// Token: 0x060057C4 RID: 22468 RVA: 0x00131F94 File Offset: 0x00130194
		protected override SqlExpression ConvertNumberToDateTime(SqlExpression number)
		{
			SqlExpression sqlExpression = this.ConvertNumberToDate(number);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Abs(number), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Abs(number)));
			SqlExpression sqlExpression3 = DrdaAstCreator.IntervalExpression.Seconds(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IntSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(sqlExpression2, SqlConstant.SecondsPerDay) }));
			SqlExpression sqlExpression4 = DrdaAstCreator.IntervalExpression.Microseconds(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IntSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(sqlExpression2, base.MicrosecondsPerDay),
				base.MicrosecondsPerSecond
			}) }));
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression3), sqlExpression4);
		}

		// Token: 0x060057C5 RID: 22469 RVA: 0x00132041 File Offset: 0x00130241
		protected override SqlExpression ConvertDateToNumber(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaysSqlString), new SqlExpression[] { expression }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaysSqlString), new SqlExpression[] { base.BaseOADateTime }));
		}

		// Token: 0x060057C6 RID: 22470 RVA: 0x00132080 File Offset: 0x00130280
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

		// Token: 0x060057C7 RID: 22471 RVA: 0x0013217C File Offset: 0x0013037C
		private SqlExpression CreateIsEven(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Equals(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(sqlExpression),
				SqlConstant.Two
			}), SqlConstant.Zero);
		}

		// Token: 0x060057C8 RID: 22472 RVA: 0x001321D4 File Offset: 0x001303D4
		private SqlExpression CreateIsOdd(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.NotEqualTo(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(sqlExpression),
				SqlConstant.Two
			}), SqlConstant.Zero);
		}

		// Token: 0x060057C9 RID: 22473 RVA: 0x0013222C File Offset: 0x0013042C
		protected override SqlExpression CreateDivideOperation(IBinaryExpression divide)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(divide.Left);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(divide.Right);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(this.CastToDouble(sqlExpression), this.CastToDouble(sqlExpression2));
		}

		// Token: 0x060057CA RID: 22474 RVA: 0x00132266 File Offset: 0x00130466
		protected override ScalarExpression Constant(Value constant, TypeValue type)
		{
			if (!constant.IsNull)
			{
				type = type.NonNullable;
				if (type.TypeKind == ValueKind.Time)
				{
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.TimeConstant(constant.AsTime.AsClrTimeSpan);
				}
			}
			return base.Constant(constant, type);
		}

		// Token: 0x060057CB RID: 22475 RVA: 0x0013229C File Offset: 0x0013049C
		protected override SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.Atan2SqlString), new SqlExpression[] { sqlExpression2, sqlExpression });
		}

		// Token: 0x060057CC RID: 22476 RVA: 0x001322EC File Offset: 0x001304EC
		protected override SqlExpression CastToSingle(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RealSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x060057CD RID: 22477 RVA: 0x00132307 File Offset: 0x00130507
		protected override SqlExpression CastToDouble(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DoubleSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x060057CE RID: 22478 RVA: 0x00132322 File Offset: 0x00130522
		protected override SqlExpression CastToDecimal(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DecimalSqlString), new SqlExpression[]
			{
				expression,
				MsDb2AstCreator.decimalPrecision,
				MsDb2AstCreator.decimalScale
			});
		}

		// Token: 0x060057CF RID: 22479 RVA: 0x0013234D File Offset: 0x0013054D
		protected override SqlExpression CastToBigInt(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.BigIntSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x060057D0 RID: 22480 RVA: 0x00132368 File Offset: 0x00130568
		private static SqlExpression CastToInt(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IntSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x060057D1 RID: 22481 RVA: 0x00132383 File Offset: 0x00130583
		private static SqlExpression CastToDate(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x060057D2 RID: 22482 RVA: 0x001323A0 File Offset: 0x001305A0
		protected override SqlExpression CreateToText(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			if (base.GetType(expression).TypeKind != ValueKind.Logical)
			{
				return base.CreateFunctionCall(SqlLanguageStrings.CharSqlString, Array.Empty<SqlExpression>())(invocation);
			}
			Condition condition = base.GetValue(expression) as Condition;
			if (condition != null)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(condition).Then(SqlConstant.StringTrue)
					.When(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Not(condition))
					.Then(SqlConstant.StringFalse)
					.Else(SqlConstant.Null);
			}
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, SqlConstant.One)).Then(SqlConstant.StringTrue)
				.When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, SqlConstant.Zero))
				.Then(SqlConstant.StringFalse)
				.Else(SqlConstant.Null);
		}

		// Token: 0x060057D3 RID: 22483 RVA: 0x001324A4 File Offset: 0x001306A4
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

		// Token: 0x04003181 RID: 12673
		private static readonly SqlConstant decimalPrecision = new SqlConstant(ConstantType.Integer, "29");

		// Token: 0x04003182 RID: 12674
		private static readonly SqlConstant decimalScale = new SqlConstant(ConstantType.Integer, "10");

		// Token: 0x02000CAE RID: 3246
		private sealed class MsDb2IntervalExpression : SqlExpression
		{
			// Token: 0x060057D6 RID: 22486 RVA: 0x00132552 File Offset: 0x00130752
			private MsDb2IntervalExpression(SqlExpression expression, ConstantSqlString intervalUnit)
			{
				this.expression = expression;
				this.intervalUnit = intervalUnit;
			}

			// Token: 0x060057D7 RID: 22487 RVA: 0x00132568 File Offset: 0x00130768
			public static MsDb2AstCreator.MsDb2IntervalExpression Days(SqlExpression expression)
			{
				return new MsDb2AstCreator.MsDb2IntervalExpression(expression, SqlLanguageStrings.DaysSqlString);
			}

			// Token: 0x060057D8 RID: 22488 RVA: 0x00132575 File Offset: 0x00130775
			public static MsDb2AstCreator.MsDb2IntervalExpression Hours(SqlExpression expression)
			{
				return new MsDb2AstCreator.MsDb2IntervalExpression(expression, SqlLanguageStrings.HoursSqlString);
			}

			// Token: 0x060057D9 RID: 22489 RVA: 0x00132582 File Offset: 0x00130782
			public static MsDb2AstCreator.MsDb2IntervalExpression Minutes(SqlExpression expression)
			{
				return new MsDb2AstCreator.MsDb2IntervalExpression(expression, SqlLanguageStrings.MinutesSqlString);
			}

			// Token: 0x060057DA RID: 22490 RVA: 0x0013258F File Offset: 0x0013078F
			public static MsDb2AstCreator.MsDb2IntervalExpression Seconds(SqlExpression expression)
			{
				return new MsDb2AstCreator.MsDb2IntervalExpression(expression, SqlLanguageStrings.SecondsSqlString);
			}

			// Token: 0x060057DB RID: 22491 RVA: 0x0013259C File Offset: 0x0013079C
			public static MsDb2AstCreator.MsDb2IntervalExpression Microseconds(SqlExpression expression)
			{
				return new MsDb2AstCreator.MsDb2IntervalExpression(expression, SqlLanguageStrings.MicrosecondsSqlString);
			}

			// Token: 0x17001A68 RID: 6760
			// (get) Token: 0x060057DC RID: 22492 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060057DD RID: 22493 RVA: 0x001325A9 File Offset: 0x001307A9
			public override void WriteCreateScript(ScriptWriter writer)
			{
				this.expression.WriteCreateScript(writer);
				writer.WriteSpaceBefore(this.intervalUnit);
			}

			// Token: 0x04003183 RID: 12675
			private readonly ConstantSqlString intervalUnit;

			// Token: 0x04003184 RID: 12676
			private readonly SqlExpression expression;
		}
	}
}
