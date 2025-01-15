using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Sql
{
	// Token: 0x020003A7 RID: 935
	internal sealed class SqlAstCreator : DbAstCreator
	{
		// Token: 0x06002079 RID: 8313 RVA: 0x00055740 File Offset: 0x00053940
		private SqlAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
			this.supportsDateTime2 = environment.SqlSettings.GetSetting<bool>("SupportsDateTime2", true);
			this.supportsTrimFrom = environment.SqlSettings.GetSetting<bool>("SupportsTrimFrom", false);
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x00055779 File Offset: 0x00053979
		private bool SupportsDateTime2
		{
			get
			{
				return this.supportsDateTime2;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x00055779 File Offset: 0x00053979
		private bool SupportsNanoSecond
		{
			get
			{
				return this.supportsDateTime2;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x00055781 File Offset: 0x00053981
		private bool SupportsTrimFrom
		{
			get
			{
				return this.supportsTrimFrom;
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x00055789 File Offset: 0x00053989
		private new DbEnvironment ExternalEnvironment
		{
			get
			{
				return (DbEnvironment)base.ExternalEnvironment;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x00055796 File Offset: 0x00053996
		private SqlExpression SqlDateTimeBaseOffset
		{
			get
			{
				return SqlConstant.Two;
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x0005579D File Offset: 0x0005399D
		private SqlExpression Nanosecond100sPerDay
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(864000000000L);
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06002080 RID: 8320 RVA: 0x000557AD File Offset: 0x000539AD
		private SqlExpression MillisecondsPerDay
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(86400000L);
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x000557BC File Offset: 0x000539BC
		private double CurrentTimeZoneOffSetMinutes
		{
			get
			{
				return TimeZoneHelpers.GetDefaultTimeZone(this.ExternalEnvironment.Host).Value.GetUtcOffset(DateTime.Now).TotalMinutes;
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06002082 RID: 8322 RVA: 0x000557F0 File Offset: 0x000539F0
		private SqlDataType DateTimeType
		{
			get
			{
				if (!this.SupportsDateTime2)
				{
					return SqlDataType.DateTime;
				}
				return SqlDataType.DateTime2;
			}
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x00055808 File Offset: 0x00053A08
		protected override Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> functions = base.GetFunctions();
			ICollection<KeyValuePair<FunctionValue, Func<IInvocationExpression, SqlExpression>>> collection = functions;
			Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> dictionary = new Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>>();
			dictionary.Add(Library.Date.Day, new Func<IInvocationExpression, SqlExpression>(this.CreateDateDay));
			dictionary.Add(CultureSpecificFunction.DateDayOfWeek, new Func<IInvocationExpression, SqlExpression>(this.CreateDayOfWeek));
			dictionary.Add(Library.Date.DayOfYear, new Func<IInvocationExpression, SqlExpression>(this.CreateDayOfYear));
			dictionary.Add(CultureSpecificFunction.DateWeekOfYear, new Func<IInvocationExpression, SqlExpression>(this.CreateWeekOfYear));
			dictionary.Add(Library.Date.AddDays, new Func<IInvocationExpression, SqlExpression>(this.CreateDateTimeAddDays));
			dictionary.Add(Library.Date.AddWeeks, new Func<IInvocationExpression, SqlExpression>(this.CreateDateTimeAddWeeks));
			dictionary.Add(Library.Date.AddQuarters, new Func<IInvocationExpression, SqlExpression>(this.CreateDateTimeAddQuarters));
			dictionary.Add(Library.Date.Month, new Func<IInvocationExpression, SqlExpression>(this.CreateDateMonth));
			dictionary.Add(Library.Date.QuarterOfYear, new Func<IInvocationExpression, SqlExpression>(this.CreateQuarterOfYear));
			dictionary.Add(Library.Date.Year, new Func<IInvocationExpression, SqlExpression>(this.CreateDateYear));
			dictionary.Add(Library.Date.EndOfDay, new Func<IInvocationExpression, SqlExpression>(this.CreateEndOfDay));
			dictionary.Add(CultureSpecificFunction.DateStartOfWeek, new Func<IInvocationExpression, SqlExpression>(this.CreateStartOfWeek));
			dictionary.Add(CultureSpecificFunction.DateEndOfWeek, new Func<IInvocationExpression, SqlExpression>(this.CreateEndOfWeek));
			dictionary.Add(Library.Date.StartOfMonth, new Func<IInvocationExpression, SqlExpression>(this.CreateStartOfMonth));
			dictionary.Add(Library.Date.EndOfMonth, new Func<IInvocationExpression, SqlExpression>(this.CreateEndOfMonth));
			dictionary.Add(Library.Date.StartOfQuarter, new Func<IInvocationExpression, SqlExpression>(this.CreateStartOfQuarter));
			dictionary.Add(Library.Date.EndOfQuarter, new Func<IInvocationExpression, SqlExpression>(this.CreateEndOfQuarter));
			dictionary.Add(Library.Date.StartOfYear, new Func<IInvocationExpression, SqlExpression>(this.CreateStartOfYear));
			dictionary.Add(Library.Date.EndOfYear, new Func<IInvocationExpression, SqlExpression>(this.CreateEndOfYear));
			dictionary.Add(Library.Time.Hour, new Func<IInvocationExpression, SqlExpression>(this.CreateTimeHour));
			dictionary.Add(Library.Time.Minute, new Func<IInvocationExpression, SqlExpression>(this.CreateTimeMinute));
			dictionary.Add(Library.Time.Second, new Func<IInvocationExpression, SqlExpression>(this.CreateTimeSecond));
			dictionary.Add(Library.Time.StartOfHour, new Func<IInvocationExpression, SqlExpression>(this.CreateStartOfHour));
			dictionary.Add(Library.Time.EndOfHour, new Func<IInvocationExpression, SqlExpression>(this.CreateEndOfHour));
			dictionary.Add(Library.Duration.TotalHours, new Func<IInvocationExpression, SqlExpression>(this.CreateDurationTotalHours));
			dictionary.Add(Library.Duration.TotalMinutes, new Func<IInvocationExpression, SqlExpression>(this.CreateDurationTotalMinutes));
			dictionary.Add(Library.Duration.TotalSeconds, new Func<IInvocationExpression, SqlExpression>(this.CreateDurationTotalSeconds));
			dictionary.Add(Library.Duration.duration, new Func<IInvocationExpression, SqlExpression>(this.CreateDurationLiteral));
			dictionary.Add(CultureSpecificFunction.ByteFrom, new Func<IInvocationExpression, SqlExpression>(this.CreateToInt64));
			dictionary.Add(CultureSpecificFunction.Int8From, new Func<IInvocationExpression, SqlExpression>(this.CreateToInt64));
			dictionary.Add(CultureSpecificFunction.Int16From, new Func<IInvocationExpression, SqlExpression>(this.CreateToInt64));
			dictionary.Add(CultureSpecificFunction.Int32From, new Func<IInvocationExpression, SqlExpression>(this.CreateToInt64));
			dictionary.Add(CultureSpecificFunction.Int64From, new Func<IInvocationExpression, SqlExpression>(this.CreateToInt64));
			dictionary.Add(CultureSpecificFunction.CurrencyFrom, new Func<IInvocationExpression, SqlExpression>(this.CreateToCurrency));
			dictionary.Add(Library.Logical.From, new Func<IInvocationExpression, SqlExpression>(this.CreateToLogical));
			dictionary.Add(Library.Text.Start, this.CreateTextFunctionCall(SqlLanguageStrings.LeftSqlString, new bool[1]));
			dictionary.Add(Library.Text.End, this.CreateTextFunctionCall(SqlLanguageStrings.RightSqlString, new bool[1]));
			Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> dictionary2 = dictionary;
			FunctionValue middle = Library.Text.Middle;
			ConstantSqlString substringSqlString = SqlLanguageStrings.SubstringSqlString;
			bool[] array = new bool[2];
			array[0] = true;
			dictionary2.Add(middle, this.CreateTextFunctionCall(substringSqlString, array));
			dictionary.Add(Library.Text.Replace, base.CreateFunctionCall(SqlLanguageStrings.ReplaceSqlString, Array.Empty<SqlExpression>()));
			dictionary.Add(Library.Text.PositionOf, new Func<IInvocationExpression, SqlExpression>(this.CreateTextPositionOf));
			dictionary.Add(TimeSpecificFunction.DateTimeFixedLocalNow, base.CreateFunctionCall(SqlLanguageStrings.SysDateTimeSqlString, Array.Empty<SqlExpression>()));
			dictionary.Add(TimeSpecificFunction.DateTimeLocalNow, base.CreateFunctionCall(SqlLanguageStrings.SysDateTimeSqlString, Array.Empty<SqlExpression>()));
			dictionary.Add(TimeSpecificFunction.DateTimeZoneFixedLocalNow, base.CreateFunctionCall(SqlLanguageStrings.SysDateTimeOffsetSqlString, Array.Empty<SqlExpression>()));
			dictionary.Add(TimeSpecificFunction.DateTimeZoneLocalNow, base.CreateFunctionCall(SqlLanguageStrings.SysDateTimeOffsetSqlString, Array.Empty<SqlExpression>()));
			dictionary.Add(TimeSpecificFunction.DateTimeZoneFixedUtcNow, new Func<IInvocationExpression, SqlExpression>(this.CreateDateTimeZoneUtcNowFunction));
			dictionary.Add(TimeSpecificFunction.DateTimeZoneUtcNow, new Func<IInvocationExpression, SqlExpression>(this.CreateDateTimeZoneUtcNowFunction));
			dictionary.Add(TypeSpecificFunction.TextNewGuid, base.CreateFunctionCall(SqlLanguageStrings.NewIdSqlString, Array.Empty<SqlExpression>()));
			collection.AddRange(dictionary);
			return functions;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00055C90 File Offset: 0x00053E90
		protected override Dictionary<FunctionValue, Func<IInvocationExpression, SqlStatement>> GetStatementFunctions()
		{
			return new Dictionary<FunctionValue, Func<IInvocationExpression, SqlStatement>>
			{
				{
					ActionModule.Action.Bind,
					new Func<IInvocationExpression, SqlStatement>(base.CreateBind)
				},
				{
					ActionModule.TableAction.InsertRows,
					new Func<IInvocationExpression, SqlStatement>(this.CreateInsertRows)
				},
				{
					ActionModule.TableAction.UpdateRows,
					new Func<IInvocationExpression, SqlStatement>(this.CreateUpdateRows)
				},
				{
					ActionModule.TableAction.DeleteRows,
					new Func<IInvocationExpression, SqlStatement>(this.CreateDeleteRows)
				}
			};
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x00055D01 File Offset: 0x00053F01
		private SqlExpression CreateDateTimeZoneUtcNowFunction(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ToDateTimeOffsetSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SysUtcDateTimeSqlString), Array.Empty<SqlExpression>()),
				SqlConstant.Zero
			});
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x00055D38 File Offset: 0x00053F38
		private SqlExpression ConvertDateTimeToTicks(SqlExpression dateTime, SqlDataType format)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.Day, base.GetBaseDateTime(format), dateTime);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, sqlExpression, base.GetBaseDateTime(format));
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.Millisecond, sqlExpression2, dateTime);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Millisecond, sqlExpression3, sqlExpression2);
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.Nanosecond, sqlExpression4, dateTime);
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(this.Convert(SqlDataType.BigInt, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(this.Convert(SqlDataType.BigInt, sqlExpression5), base.NsPerTick)), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.Convert(SqlDataType.BigInt, sqlExpression3), base.TicksPerMs), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.Convert(SqlDataType.BigInt, sqlExpression), base.TicksPerDay)));
			return base.Select(sqlExpression6, Alias.ScalarColumn).ToPagingQuerySpecification();
		}

		// Token: 0x06002087 RID: 8327 RVA: 0x00055E08 File Offset: 0x00054008
		private SqlExpression CreateDurationLiteral(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = null;
			long[] array = new long[] { 864000000000L, 36000000000L, 600000000L };
			for (int i = 0; i < 3; i++)
			{
				IExpression expression = invocation.Arguments[i];
				IConstantExpression2 constantExpression = expression as IConstantExpression2;
				INumberValue numberValue = ((constantExpression != null) ? constantExpression.Value : null) as INumberValue;
				long? num = ((numberValue != null) ? new long?(numberValue.AsInteger64) : null);
				if (num == null || num.Value != 0L)
				{
					SqlExpression sqlExpression2 = ((num != null) ? this.Convert(SqlDataType.BigInt, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(num.Value * array[i])) : SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateScalarExpression(expression), this.Convert(SqlDataType.BigInt, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(array[i]))));
					sqlExpression = ((sqlExpression == null) ? sqlExpression2 : SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression2));
				}
			}
			IExpression expression2 = invocation.Arguments[3];
			IConstantExpression2 constantExpression2 = expression2 as IConstantExpression2;
			INumberValue numberValue2 = ((constantExpression2 != null) ? constantExpression2.Value : null) as INumberValue;
			double? num2 = ((numberValue2 != null) ? new double?(numberValue2.AsDouble) : null);
			SqlExpression sqlExpression3 = null;
			if (num2 == null)
			{
				sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateScalarExpression(expression2), this.Convert(SqlDataType.BigInt, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(10000000L)));
			}
			else if (num2.Value != 0.0)
			{
				sqlExpression3 = this.Convert(SqlDataType.BigInt, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(num2.Value * 10000000.0, true));
			}
			if (sqlExpression3 != null)
			{
				sqlExpression = ((sqlExpression == null) ? sqlExpression3 : SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression3));
			}
			return sqlExpression ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(0);
		}

		// Token: 0x06002088 RID: 8328 RVA: 0x00055FB4 File Offset: 0x000541B4
		private SqlExpression ConvertTicksToDateTime(SqlExpression ticks, SqlDataType format, SqlExpression ticksPerDay)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(ticks, ticksPerDay));
			BinaryScalarOperation binaryScalarOperation = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(ticks, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(ticksPerDay, sqlExpression));
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, sqlExpression, base.GetBaseDateTime(format));
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(binaryScalarOperation, base.TicksPerMs));
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(binaryScalarOperation, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.TicksPerMs, sqlExpression3));
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Millisecond, sqlExpression3, sqlExpression2);
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(sqlExpression4, base.NsPerTick);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Nanosecond, sqlExpression6, sqlExpression5);
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00056038 File Offset: 0x00054238
		protected override SqlExpression ConvertDateToNumber(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.Day, base.BaseOADateTime, expression);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected override SqlExpression ConvertDateTimeToNumber(SqlExpression expression)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x0005604C File Offset: 0x0005424C
		private SqlExpression ConvertDateTimeAliasToDouble(Alias dateTimeAlias)
		{
			ColumnReference columnReference = new ColumnReference(dateTimeAlias);
			SqlExpression sqlExpression = base.CreateOADateTimeSignExpression(columnReference);
			SqlExpression sqlExpression2 = this.Convert(SqlDataType.Float, this.Convert(SqlDataType.DateTime, this.Convert(SqlDataType.Time, columnReference)));
			if (this.SupportsDateTime2)
			{
				SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(new BinaryScalarOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DatePartSqlString), new SqlExpression[]
				{
					DatePart.Nanosecond,
					columnReference
				}), BinaryScalarOperator.Modulo, SqlConstant.Million), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(86400000000000.0, true));
				sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, sqlExpression3);
			}
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(sqlExpression, sqlExpression2);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(this.ConvertDateToNumber(columnReference), sqlExpression4);
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x000560F4 File Offset: 0x000542F4
		private SqlExpression ConvertDateTimeToDouble(SqlExpression dateTime)
		{
			SqlQueryExpression sqlQueryExpression = base.Select(dateTime, Alias.ScalarColumn).ToPagingQuerySpecification();
			SqlExpression sqlExpression = this.ConvertDateTimeAliasToDouble(Alias.ScalarColumn);
			return base.Select(sqlExpression, Alias.ScalarColumn).From(sqlQueryExpression, Alias.VirtualTable).ToPagingQuerySpecification();
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00056144 File Offset: 0x00054344
		protected override SqlExpression ConvertNumberToDate(SqlExpression number)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, base.BaseOADateTime, number);
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00056158 File Offset: 0x00054358
		protected override SqlExpression ConvertNumberToDateTime(SqlExpression number)
		{
			SqlQueryExpression sqlQueryExpression = base.Select(number, Alias.ScalarColumn).ToPagingQuerySpecification();
			ColumnReference columnReference = new ColumnReference(Alias.ScalarColumn);
			SqlExpression sqlExpression = this.Convert(SqlDataType.BigInt, SqlAstCreator.CreateRound(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(columnReference, this.Nanosecond100sPerDay), SqlConstant.Zero));
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(columnReference, BinaryLogicalOperator.LessThan, SqlConstant.Zero)).Then(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(new BinaryScalarOperation(sqlExpression, BinaryScalarOperator.Modulo, this.Nanosecond100sPerDay), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(2))))
				.Else(sqlExpression);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(sqlExpression2, this.Nanosecond100sPerDay), base.BaseOADateTime);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Millisecond, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(new BinaryScalarOperation(sqlExpression2, BinaryScalarOperator.Modulo, this.Nanosecond100sPerDay), SqlConstant.TenThousand), sqlExpression3);
			if (this.SupportsDateTime2)
			{
				sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Nanosecond, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(new BinaryScalarOperation(new BinaryScalarOperation(sqlExpression2, BinaryScalarOperator.Modulo, this.Nanosecond100sPerDay), BinaryScalarOperator.Modulo, SqlConstant.TenThousand), SqlConstant.Hundred), sqlExpression4);
			}
			return base.Select(sqlExpression4, Alias.ScalarColumn).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
			{
				sqlQueryExpression,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(Alias.VirtualTable)
			}).ToPagingQuerySpecification();
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x000562BC File Offset: 0x000544BC
		public static SqlAstCreator Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			return new SqlAstCreator(expression, cursor, externalEnvironment);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x000562C8 File Offset: 0x000544C8
		public override SqlQueryExpression CreateCountQuery(SqlQueryExpression source)
		{
			return base.Select(SqlAstCreatorBase<DbAstCreator.SqlVariable>.CountBig(null), Alias.ScalarColumn).From(source, Alias.VirtualTable).ToPagingQuerySpecification();
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x000562FC File Offset: 0x000544FC
		protected override SqlExpression CastToDouble(SqlExpression expression)
		{
			return this.Convert(SqlDataType.Float, expression);
		}

		// Token: 0x06002092 RID: 8338 RVA: 0x0005630A File Offset: 0x0005450A
		protected override SqlExpression CastToDecimal(SqlExpression expression)
		{
			return this.Convert(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(38, 6), expression);
		}

		// Token: 0x06002093 RID: 8339 RVA: 0x0005631C File Offset: 0x0005451C
		protected override SqlExpression VisitDateTimeTimeSpanBinaryScalarOperation(SqlExpression dateTime, TimeSpan timeSpan, TypeValue dateTimeType)
		{
			SqlExpression sqlExpression = dateTime;
			if (timeSpan.Days != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Days), sqlExpression);
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

		// Token: 0x06002094 RID: 8340 RVA: 0x000563DC File Offset: 0x000545DC
		protected override SqlExpression VisitDateTimeDurationBinaryScalarOperation(SqlExpression dateTime, SqlExpression duration, TypeValue dateTimeType)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(duration, base.TicksPerDay));
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, sqlExpression, dateTime);
			if (dateTimeType.TypeKind == ValueKind.Date)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, sqlExpression, dateTime);
			}
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(duration, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.TicksPerDay, sqlExpression));
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(sqlExpression3, base.TicksPerMs));
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Millisecond, sqlExpression4, sqlExpression2);
			SqlDataType sqlDataType = this.dbEnvironment.GetSqlScalarType(dateTimeType);
			if (sqlDataType == SqlDataType.DateTime2 && !this.SupportsDateTime2)
			{
				sqlDataType = SqlDataType.DateTime;
			}
			if (sqlDataType == SqlDataType.DateTime2 || sqlDataType == SqlDataType.DateTimeOffset || sqlDataType == SqlDataType.Time)
			{
				if (sqlDataType == SqlDataType.DateTime2)
				{
					sqlExpression5 = this.Convert(sqlDataType, sqlExpression5);
				}
				SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.TicksPerMs, sqlExpression4)), base.NsPerTick));
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Nanosecond, sqlExpression6, sqlExpression5);
			}
			return sqlExpression5;
		}

		// Token: 0x06002095 RID: 8341 RVA: 0x000564DC File Offset: 0x000546DC
		protected override SqlExpression CreateAddOperation(IBinaryExpression add)
		{
			SqlExpression sqlExpression;
			bool flag = this.TryConvertGuidExpression(add.Left, out sqlExpression);
			SqlExpression sqlExpression2;
			bool flag2 = this.TryConvertGuidExpression(add.Right, out sqlExpression2);
			if (flag || flag2)
			{
				return new BinaryScalarOperation(sqlExpression ?? base.CreateScalarExpression(add.Left), BinaryScalarOperator.Add, sqlExpression2 ?? base.CreateScalarExpression(add.Right));
			}
			return this.CreateBinaryScalarOperation(BinaryScalarOperator.Add, add);
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0005653B File Offset: 0x0005473B
		private bool TryConvertGuidExpression(IExpression expression, out SqlExpression sqlExpression)
		{
			if (expression != null && TypeServices.IsGuid(base.GetType(expression)))
			{
				sqlExpression = this.Convert(SqlDataType.NVarChar, base.CreateScalarExpression(expression));
				return true;
			}
			sqlExpression = null;
			return false;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x00056568 File Offset: 0x00054768
		protected override SqlExpression CreateBinaryFromText(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return this.Convert(SqlDataType.VarBinary, sqlExpression, 2);
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x00056598 File Offset: 0x00054798
		private SqlExpression CreateDateTimeAddDays(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, sqlExpression2, sqlExpression);
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000565D8 File Offset: 0x000547D8
		private SqlExpression CreateDateTimeAddWeeks(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Week, sqlExpression2, sqlExpression);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x00056618 File Offset: 0x00054818
		protected override SqlExpression CreateDateTimeAddMonths(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Month, sqlExpression2, sqlExpression);
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x00056658 File Offset: 0x00054858
		private SqlExpression CreateDateTimeAddQuarters(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Quarter, sqlExpression2, sqlExpression);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00056698 File Offset: 0x00054898
		protected override SqlExpression CreateDateTimeAddYears(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Year, sqlExpression2, sqlExpression);
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000566D8 File Offset: 0x000548D8
		protected override SqlExpression CreateDivideOperation(IBinaryExpression divide)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(divide.Left);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(divide.Right);
			return new BinaryScalarOperation(this.CastToDecimal(sqlExpression), BinaryScalarOperator.Divide, this.Convert(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(38, 6), sqlExpression2));
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x0005671C File Offset: 0x0005491C
		protected override SqlExpression CreateBinaryScalarOperation(BinaryScalarOperator op, IBinaryExpression invocation)
		{
			SqlDataType sqlScalarType = this.dbEnvironment.GetSqlScalarType(base.GetType(invocation.Left));
			SqlDataType sqlScalarType2 = this.dbEnvironment.GetSqlScalarType(base.GetType(invocation.Right));
			if ((SqlEnvironment.IsMDateTimeCompatibleType(sqlScalarType) || sqlScalarType == SqlDataType.DateTimeOffset || sqlScalarType == SqlDataType.Time) && (SqlEnvironment.IsMDateTimeCompatibleType(sqlScalarType2) || sqlScalarType2 == SqlDataType.DateTimeOffset || sqlScalarType2 == SqlDataType.Time))
			{
				SqlExpression sqlExpression = this.ConvertDateTimeToTicks(base.CreateScalarExpression(invocation.Left), sqlScalarType);
				SqlExpression sqlExpression2 = this.ConvertDateTimeToTicks(base.CreateScalarExpression(invocation.Right), sqlScalarType2);
				return this.Convert(SqlDataType.BigInt, new BinaryScalarOperation(sqlExpression, op, sqlExpression2));
			}
			return base.CreateBinaryScalarOperation(op, invocation);
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x000567D0 File Offset: 0x000549D0
		protected override SqlDataType[] AdjustArgumentsForType(TypeValue[] types)
		{
			bool[] array = new bool[types.Length];
			bool flag = false;
			bool[] array2 = new bool[types.Length];
			bool flag2 = false;
			TypeValue typeValue = types[0].NonNullable;
			for (int i = 0; i < types.Length; i++)
			{
				TypeValue nonNullable = types[i].NonNullable;
				if (typeValue != null && !typeValue.Equals(nonNullable))
				{
					typeValue = null;
				}
				array[i] = nonNullable.Equals(TypeValue.Single);
				flag = flag || array[i];
				array2[i] = TypeServices.IsGuid(nonNullable);
				flag2 = flag2 || array2[i];
			}
			if (typeValue != null || (!flag && !flag2))
			{
				return null;
			}
			SqlDataType[] array3 = new SqlDataType[types.Length];
			for (int j = 0; j < types.Length; j++)
			{
				if (flag && !array[j])
				{
					array3[j] = SqlDataType.Real;
				}
				else if (flag2 && array2[j])
				{
					array3[j] = SqlDataType.NVarChar;
					types[j] = TypeValue.Text;
				}
			}
			return array3;
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x000568B8 File Offset: 0x00054AB8
		protected override void AdjustArgumentsForType(BinaryOperator2 binaryOperator, ref TypeValue leftType, ref TypeValue rightType, ref SqlExpression leftExpression, ref SqlExpression rightExpression)
		{
			if ((binaryOperator == BinaryOperator2.Equals || binaryOperator == BinaryOperator2.NotEquals) && ((TypeServices.IsGuid(leftType) && SqlAstCreator.IsConstantGuidOrNull(rightExpression)) || (TypeServices.IsGuid(rightType) && SqlAstCreator.IsConstantGuidOrNull(leftExpression))))
			{
				return;
			}
			base.AdjustArgumentsForType(binaryOperator, ref leftType, ref rightType, ref leftExpression, ref rightExpression);
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x000568F8 File Offset: 0x00054AF8
		private static bool IsConstantGuidOrNull(SqlExpression expression)
		{
			SqlConstant sqlConstant = expression as SqlConstant;
			return sqlConstant != null && (sqlConstant.Type == ConstantType.Null || ((sqlConstant.Type == ConstantType.AnsiString || sqlConstant.Type == ConstantType.UnicodeString) && SqlAstCreator.guidPattern.IsMatch(sqlConstant.Literal)));
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x00056944 File Offset: 0x00054B44
		protected override SqlExpression CreateListAverage(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			SqlDataType sqlDataType = this.dbEnvironment.GetSqlScalarType(base.GetType(expression).AsListType.ItemType);
			if (sqlDataType == SqlDataType.DateTime2 && !this.SupportsDateTime2)
			{
				sqlDataType = SqlDataType.DateTime;
			}
			if (sqlDataType == SqlDataType.Date || sqlDataType == SqlDataType.DateTime2 || sqlDataType == SqlDataType.DateTimeOffset || sqlDataType == SqlDataType.Time)
			{
				bool flag = false;
				if (sqlDataType == SqlDataType.Time)
				{
					flag = true;
					sqlDataType = SqlDataType.DateTime2;
				}
				SqlExpression sqlExpression = base.CreateListAggregateInput(expression);
				if (flag)
				{
					sqlExpression = this.Convert(sqlDataType, sqlExpression);
				}
				SqlExpression sqlExpression2 = base.LiftForGroup(this.ConvertDateTimeToTicks(sqlExpression, sqlDataType));
				SqlExpression sqlExpression3 = this.ConvertTicksToDateTime(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.ScalarColumn), sqlDataType, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.TicksPerDay));
				return base.Select(flag ? this.Convert(SqlDataType.Time, sqlExpression3) : sqlExpression3, Alias.ScalarColumn).From(base.Select(new SelectItem[]
				{
					new SelectItem(base.TicksPerDay, Alias.TicksPerDay),
					new SelectItem(this.Convert(SqlDataType.BigInt, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg(sqlExpression2)), Alias.ScalarColumn)
				}).ToPagingQuerySpecification(), Alias.VirtualTable).ToPagingQuerySpecification();
			}
			Value value = Value.Null;
			if (invocation.Arguments.Count == 2)
			{
				value = ((IConstantExpression)invocation.Arguments[1]).Value;
			}
			if (value.IsNull)
			{
				value = Library.PrecisionEnum.Double;
			}
			return base.CreateAggregateFunctionWithOptionalPrecision(expression, new ConstantExpressionSyntaxNode(value), new Func<SqlExpression, SqlExpression>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg));
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x00056AE8 File Offset: 0x00054CE8
		protected override SqlExpression CreateNumberPower(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.PowerSqlString), new SqlExpression[]
			{
				this.Convert(SqlDataType.Float, base.CreateScalarExpression(invocation.Arguments[0])),
				base.CreateScalarExpression(invocation.Arguments[1])
			});
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00056B3F File Offset: 0x00054D3F
		protected override SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.Atn2SqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x00056B58 File Offset: 0x00054D58
		protected override SqlExpression CreateNumberFrom(IInvocationExpression invocation)
		{
			switch (base.GetType(invocation.Arguments[0]).TypeKind)
			{
			case ValueKind.Time:
				return this.Convert(SqlDataType.Float, this.Convert(SqlDataType.DateTime, base.CreateScalarExpression(invocation.Arguments[0])));
			case ValueKind.Date:
				return this.Convert(SqlDataType.Float, this.ConvertDateToNumber(base.CreateScalarExpression(invocation.Arguments[0])));
			case ValueKind.DateTime:
				return this.ConvertDateTimeToDouble(base.CreateScalarExpression(invocation.Arguments[0]));
			case ValueKind.DateTimeZone:
			{
				SqlExpression sqlExpression = this.Convert(this.DateTimeType, SqlAstCreator.CreateSwitchOffset(base.CreateScalarExpression(invocation.Arguments[0]), this.CurrentTimeZoneOffSetMinutes));
				return this.ConvertDateTimeToDouble(sqlExpression);
			}
			case ValueKind.Number:
				return base.CreateScalarExpression(invocation.Arguments[0]);
			case ValueKind.Logical:
				return this.Convert(SqlDataType.Float, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(base.CreateConditionExpression(invocation.Arguments[0])).Then(SqlConstant.One)
					.Else(SqlConstant.Zero));
			}
			return this.Convert(SqlDataType.Float, base.CreateScalarExpression(invocation.Arguments[0]));
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x00056CBC File Offset: 0x00054EBC
		protected override SqlExpression CreateTableUnpivot(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			SqlQueryExpression sqlQueryExpression = base.CreateQueryExpression(expression);
			RecordValue fields = (base.Cursor.GetResult(expression) as TableTypeValue).ItemType.Fields;
			IList<IExpression> members = ((IListExpression)invocation.Arguments[1]).Members;
			int num = members.Count<IExpression>();
			string[] array = new string[num];
			Alias[] array2 = new Alias[num];
			TypeFacets[] array3 = new TypeFacets[num];
			ValueKind valueKind = ValueKind.None;
			for (int i = 0; i < num; i++)
			{
				string @string = ((IConstantExpression)members[i]).Value.AsText.String;
				array[i] = @string;
				TypeValue asType = fields[@string]["Type"].AsType;
				if (valueKind == ValueKind.None)
				{
					valueKind = asType.TypeKind;
				}
				array3[i] = asType.Facets;
				array2[i] = Alias.NewAssignedAlias(@string, this.dbEnvironment.SqlSettings);
			}
			Alias alias = Alias.NewAssignedAlias(((IConstantExpression)invocation.Arguments[2]).Value.AsText.String, this.dbEnvironment.SqlSettings);
			Alias alias2 = Alias.NewAssignedAlias(((IConstantExpression)invocation.Arguments[3]).Value.AsText.String, this.dbEnvironment.SqlSettings);
			SqlDataType sqlDataType = null;
			switch (valueKind)
			{
			case ValueKind.Time:
				sqlDataType = SqlAstCreator.CalculateUnpivotTypeForDatesOrTimes(array3, new SqlDataType(TypeValue.Time, SqlLanguageStrings.TimeSqlString));
				break;
			case ValueKind.DateTime:
				sqlDataType = SqlAstCreator.CalculateUnpivotTypeForDatesOrTimes(array3, new SqlDataType(TypeValue.DateTime, SqlLanguageStrings.DateTime2SqlString));
				break;
			case ValueKind.DateTimeZone:
				sqlDataType = SqlAstCreator.CalculateUnpivotTypeForDatesOrTimes(array3, new SqlDataType(TypeValue.DateTimeZone, SqlLanguageStrings.DateTimeOffsetSqlString));
				break;
			case ValueKind.Number:
				sqlDataType = SqlAstCreator.CalculateUnpivotTypeForNumber(array3);
				break;
			case ValueKind.Text:
				sqlDataType = SqlAstCreator.CalculateUnpivotTypeForText(array3);
				break;
			case ValueKind.Binary:
				sqlDataType = SqlAstCreator.CalculateUnpivotTypeForBinary(array3);
				break;
			}
			if (sqlDataType != null)
			{
				sqlQueryExpression = base.Select(SqlAstCreator.CastFields(base.GetType(expression).AsTableType.ItemType.FieldKeys, array, sqlDataType)).From(sqlQueryExpression, Alias.VirtualTable).ToPagingQuerySpecification();
			}
			UnpivotClause unpivotClause = new UnpivotClause
			{
				AttributeColumn = alias,
				ValueColumn = alias2,
				PivotValues = array2
			};
			return base.SelectAllColumns(Alias.VirtualTable, base.GetType(invocation)).From(sqlQueryExpression, unpivotClause, Alias.VirtualTable).ToPagingQuerySpecification();
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x00056F4C File Offset: 0x0005514C
		private static IList<SelectItem> CastFields(Keys allFieldnames, string[] fieldsToCast, SqlDataType dataType)
		{
			List<SelectItem> list = new List<SelectItem>(allFieldnames.Length);
			foreach (string text in allFieldnames)
			{
				Alias alias = Alias.NewNativeAlias(text);
				list.Add(fieldsToCast.Contains(text) ? new SelectItem(new CastCall
				{
					Type = dataType,
					Expression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(alias)
				}, alias) : new SelectItem(new ColumnReference(alias)));
			}
			return list;
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x00056FE8 File Offset: 0x000551E8
		private static SqlDataType CalculateUnpivotTypeForNumber(IEnumerable<TypeFacets> facets)
		{
			bool flag = false;
			int? num;
			int? num2;
			if (!TypeFacets.CompareForNumeric(facets, out num, out num2, out flag))
			{
				return null;
			}
			if (flag)
			{
				return SqlDataType.Float;
			}
			if (num == null)
			{
				return SqlDataType.Float;
			}
			if (num2.Value != 0)
			{
				int num3 = num.Value + num2.Value;
				int num4 = num2.Value;
				if (num3 > 38)
				{
					num4 -= num3 - 38;
					num3 = 38;
				}
				return new SqlDataType(TypeValue.Decimal.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?(num3), new int?(num4), SqlLanguageStrings.DecimalSqlString.String)));
			}
			switch (num.Value)
			{
			case 1:
			case 2:
				return SqlDataType.TinyInt;
			case 3:
			case 4:
				return SqlDataType.SmallInt;
			case 5:
			case 6:
			case 7:
			case 8:
			case 9:
				return SqlDataType.Int;
			case 10:
			case 11:
			case 12:
			case 13:
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
				return SqlDataType.BigInt;
			default:
				return new SqlDataType(TypeValue.Decimal.NewFacets(TypeFacets.NewNumeric(new int?(10), num, null, SqlLanguageStrings.DecimalSqlString.String)));
			}
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0005713C File Offset: 0x0005533C
		private static SqlDataType CalculateUnpivotTypeForBinary(IEnumerable<TypeFacets> facets)
		{
			long? num = new long?(-1L);
			if (TypeFacets.CompareMaxLength(facets, out num))
			{
				return new SqlDataType(TypeValue.Binary.NewFacets(TypeFacets.NewBinary(num, null, null)), SqlLanguageStrings.VarBinarySqlString);
			}
			return null;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x00057184 File Offset: 0x00055384
		private static SqlDataType CalculateUnpivotTypeForText(IEnumerable<TypeFacets> facets)
		{
			long? num = null;
			bool? flag = new bool?(false);
			bool flag2 = TypeFacets.CompareForText(facets, out num, out flag);
			if (num == null || flag == null)
			{
				return SqlDataType.NVarChar;
			}
			if (flag2 || flag.Value)
			{
				return new SqlDataType(TypeValue.Text.NewFacets(TypeFacets.NewText(num, null, null)), SqlLanguageStrings.NVarCharSqlString);
			}
			return null;
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x000571F8 File Offset: 0x000553F8
		private static SqlDataType CalculateUnpivotTypeForDatesOrTimes(IEnumerable<TypeFacets> facets, SqlDataType presumptiveType)
		{
			int? num;
			if (TypeFacets.CompareDateTimePrecision(facets, out num))
			{
				return presumptiveType ?? new SqlDataType(TypeValue.DateTime.NewFacets(TypeFacets.NewDateTime(num, SqlLanguageStrings.DateTime2SqlString.String)));
			}
			return null;
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x00057238 File Offset: 0x00055438
		protected override SqlExpression CreateToText(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			if (base.GetType(expression).TypeKind != ValueKind.Logical)
			{
				return this.Convert(SqlDataType.NVarChar, base.CreateScalarExpression(expression));
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

		// Token: 0x060020AD RID: 8365 RVA: 0x00057340 File Offset: 0x00055540
		protected override SqlExpression CreateToDate(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			if (base.GetType(expression).TypeKind == ValueKind.Number)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, sqlExpression, base.BaseOADateTime);
			}
			return this.Convert(SqlDataType.Date, sqlExpression);
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x00057390 File Offset: 0x00055590
		protected override SqlExpression CreateToDateTime(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			TypeValue type = base.GetType(expression);
			ValueKind typeKind = type.TypeKind;
			SqlExpression sqlExpression2;
			if (typeKind != ValueKind.Time)
			{
				if (typeKind != ValueKind.DateTimeZone)
				{
					if (typeKind == ValueKind.Number)
					{
						if (SqlAstCreator.IsWholeNumberType(this.ExternalEnvironment.GetSqlScalarType(type.NonNullable)))
						{
							return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, sqlExpression, base.BaseOADateTime);
						}
						return this.ConvertNumberToDateTime(sqlExpression);
					}
				}
				else if (this.dbEnvironment.UnsafeTypeConversions)
				{
					sqlExpression2 = SqlAstCreator.CreateSwitchOffset(sqlExpression, this.CurrentTimeZoneOffSetMinutes);
					goto IL_00B6;
				}
			}
			else if (this.dbEnvironment.UnsafeTypeConversions)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Minus(this.SqlDateTimeBaseOffset), this.Convert(this.DateTimeType, sqlExpression));
			}
			sqlExpression2 = sqlExpression;
			IL_00B6:
			return this.Convert(this.DateTimeType, sqlExpression2);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x00057460 File Offset: 0x00055660
		protected override SqlExpression CreateToDateTimeWithTimeZone(IInvocationExpression invocation)
		{
			return this.Convert(SqlDataType.DateTimeOffset, base.CreateScalarExpression(invocation.Arguments[0]));
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x00057480 File Offset: 0x00055680
		protected override SqlExpression CreateTextContains(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			IExpression expression = arguments[0];
			IExpression expression2 = arguments[1];
			if (expression2.Kind == ExpressionKind.Constant)
			{
				Value value = ((IConstantExpression)expression2).Value;
				if (value.IsText && value.AsText.Length <= 3998 && value.AsString.IndexOfAny(SqlAstCreator.LikeWildcardCharacters) < 0)
				{
					string text = "%" + value.AsString + "%";
					return new BinaryLogicalOperation(base.CreateScalarExpression(expression), BinaryLogicalOperator.Like, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(text));
				}
			}
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CharIndexSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(expression2),
				base.CreateScalarExpression(expression)
			}), BinaryLogicalOperator.GreaterThan, SqlConstant.Zero);
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x00057544 File Offset: 0x00055744
		protected override SqlExpression CreateTextStartsWith(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			IExpression expression = arguments[0];
			IExpression expression2 = arguments[1];
			if (expression2.Kind == ExpressionKind.Constant)
			{
				Value value = ((IConstantExpression)expression2).Value;
				if (value.IsText && value.AsText.Length <= 3999 && value.AsString.IndexOfAny(SqlAstCreator.LikeWildcardCharacters) < 0)
				{
					string text = value.AsString + "%";
					return new BinaryLogicalOperation(base.CreateScalarExpression(expression), BinaryLogicalOperator.Like, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(text));
				}
			}
			SqlExpression sqlExpression = base.CreateScalarExpression(arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Equals(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LeftSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[0]),
				this.Len(sqlExpression)
			}), sqlExpression);
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0005761F File Offset: 0x0005581F
		private Func<IInvocationExpression, SqlExpression> CreateTextFunctionCall(ConstantSqlString function, params bool[] adjustInputs)
		{
			return delegate(IInvocationExpression invocation)
			{
				SqlExpression[] array = new SqlExpression[invocation.Arguments.Count];
				array[0] = this.CreateScalarExpression(invocation.Arguments[0]);
				for (int i = 1; i < array.Length; i++)
				{
					array[i] = this.CreateScalarExpression(invocation.Arguments[i]);
					if (adjustInputs[i - 1])
					{
						array[i] = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(array[i], SqlConstant.One);
					}
				}
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(function), array);
			};
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00057648 File Offset: 0x00055848
		private SqlExpression CreateTextPositionOf(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CharIndexSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(invocation.Arguments[1]),
				base.CreateScalarExpression(invocation.Arguments[0])
			}), SqlConstant.One);
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000576A0 File Offset: 0x000558A0
		protected override SqlStatement CreateInsertRows(IInvocationExpression invocation)
		{
			SqlInsertStatement sqlInsertStatement = (SqlInsertStatement)base.CreateInsertRows(invocation);
			Keys identityColumnNames = this.dbEnvironment.GetIdentityColumnNames(sqlInsertStatement.Table.Schema.Name, sqlInsertStatement.Table.Name.Name);
			foreach (ColumnReference columnReference in sqlInsertStatement.ColumnList)
			{
				if (identityColumnNames.Contains(columnReference.Name.Name))
				{
					return new SqlStatementList(new SqlStatement[]
					{
						new SqlSetIdentityInsertStatement(sqlInsertStatement.Table, true),
						sqlInsertStatement,
						new SqlSetIdentityInsertStatement(sqlInsertStatement.Table, false)
					});
				}
			}
			return sqlInsertStatement;
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x00057768 File Offset: 0x00055968
		protected override SqlExpression CreateExecuteStoredProcedureArgument(List<SqlStatement> statements, int argumentIndex, SqlDataType argumentType, SqlExpression argument)
		{
			Alias alias = Alias.NewNativeAlias("@Variable" + argumentIndex.ToString());
			statements.Add(new SqlDeclareVariableStatement(alias, argumentType));
			statements.Add(new SqlSetVariableStatement(alias, argument));
			return new VariableReference(alias);
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000577B0 File Offset: 0x000559B0
		protected override SelectItem MitigateColumn(string name, TypeValue type, ref bool mitigateColumns)
		{
			string text;
			if (!this.dbEnvironment.UserOptions.GetBool("OmitSRID", false) && TypeServices.TryGetSerializationFormat(type, out text))
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference xcolumnReference = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.VirtualTable, Alias.NewNativeAlias(name));
				if (text == "GeographyWKT")
				{
					mitigateColumns = true;
					return new SelectItem(this.ConstructGeospatialExpression(xcolumnReference, true), Alias.NewAssignedAlias(name, this.dbEnvironment.SqlSettings));
				}
				if (text == "GeometryWKT")
				{
					mitigateColumns = true;
					return new SelectItem(this.ConstructGeospatialExpression(xcolumnReference, false), Alias.NewAssignedAlias(name, this.dbEnvironment.SqlSettings));
				}
			}
			if (TypeServices.IsGuid(type))
			{
				type = TypeValue.Text;
			}
			return base.MitigateColumn(name, type, ref mitigateColumns);
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x00057875 File Offset: 0x00055A75
		public static SqlAstCreator New(DbEnvironment externalEnvironment)
		{
			return new SqlAstCreator(null, null, externalEnvironment);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x00057880 File Offset: 0x00055A80
		private SqlExpression CreateDateYear(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression);
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x000578AC File Offset: 0x00055AAC
		private SqlExpression CreateStartOfYear(IInvocationExpression invocation)
		{
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression);
			switch (typeKind)
			{
			case ValueKind.Date:
				return this.CreateStartOfDate(sqlExpression2, null, null);
			case ValueKind.DateTime:
				return this.CreateStartOfDateTime(sqlExpression2, null, null, null, null, null, null);
			case ValueKind.DateTimeZone:
				return this.CreateStartOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), sqlExpression2, null, null, null, null, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060020BA RID: 8378 RVA: 0x00057948 File Offset: 0x00055B48
		private SqlExpression CreateEndOfYear(IInvocationExpression invocation)
		{
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression);
			switch (typeKind)
			{
			case ValueKind.Date:
				return this.CreateEndOfDate(sqlExpression2, null, null);
			case ValueKind.DateTime:
				return this.CreateEndOfDateTime(sqlExpression2, null, null, null, null, null, null);
			case ValueKind.DateTimeZone:
				return this.CreateEndOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), sqlExpression2, null, null, null, null, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060020BB RID: 8379 RVA: 0x000579E4 File Offset: 0x00055BE4
		private SqlExpression CreateStartOfWeek(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Minus(this.CreateDayOfWeek(invocation)), base.CreateScalarExpression(expression));
			switch (type.TypeKind)
			{
			case ValueKind.Date:
				return sqlExpression;
			case ValueKind.DateTime:
				return new CastCall
				{
					Type = new SqlDataType(type, this.SupportsDateTime2 ? SqlLanguageStrings.DateTime2SqlString : SqlLanguageStrings.DateTimeSqlString),
					Expression = new CastCall
					{
						Type = SqlDataType.Date,
						Expression = sqlExpression
					}
				};
			case ValueKind.DateTimeZone:
				return this.CreateStartOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression), null, null, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x00057ACC File Offset: 0x00055CCC
		private SqlExpression CreateEndOfWeek(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			ValueKind typeKind = base.GetType(expression).TypeKind;
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(6), this.CreateDayOfWeek(invocation)), base.CreateScalarExpression(expression));
			switch (typeKind)
			{
			case ValueKind.Date:
				return sqlExpression;
			case ValueKind.DateTime:
				return this.CreateEndOfDateTime(SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression), null, null, null, null);
			case ValueKind.DateTimeZone:
				return this.CreateEndOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression), null, null, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x00057BA4 File Offset: 0x00055DA4
		private SqlExpression CreateStartOfQuarter(IInvocationExpression invocation)
		{
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = SqlAstCreator.CreateDatePartFunction(DatePart.Quarter, sqlExpression);
			switch (typeKind)
			{
			case ValueKind.Date:
				return this.CreateStartOfDate(SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlConstant.Three, sqlExpression2), SqlConstant.Two), null);
			case ValueKind.DateTime:
				return this.CreateStartOfDateTime(SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlConstant.Three, sqlExpression2), SqlConstant.Two), null, null, null, null, null);
			case ValueKind.DateTimeZone:
				return this.CreateStartOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlConstant.Three, sqlExpression2), SqlConstant.Two), null, null, null, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x00057C9C File Offset: 0x00055E9C
		private SqlExpression CreateEndOfQuarter(IInvocationExpression invocation)
		{
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlConstant.Three, SqlAstCreator.CreateDatePartFunction(DatePart.Quarter, sqlExpression));
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateFromPartsSqlString), new SqlExpression[]
			{
				SqlConstant.One,
				sqlExpression3,
				SqlConstant.One
			});
			switch (typeKind)
			{
			case ValueKind.Date:
				return this.CreateEndOfDate(sqlExpression2, sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString), new SqlExpression[] { SqlAstCreator.EndOfMonth(sqlExpression4) }));
			case ValueKind.DateTime:
				return this.CreateEndOfDateTime(sqlExpression2, sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString), new SqlExpression[] { SqlAstCreator.EndOfMonth(sqlExpression4) }), null, null, null, null);
			case ValueKind.DateTimeZone:
				return this.CreateEndOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), sqlExpression2, sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString), new SqlExpression[] { SqlAstCreator.EndOfMonth(sqlExpression4) }), null, null, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x00057DD4 File Offset: 0x00055FD4
		private SqlExpression CreateStartOfMonth(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			SqlExpression sqlExpression2 = SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression);
			SqlExpression sqlExpression3 = SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression);
			switch (typeKind)
			{
			case ValueKind.Date:
				return this.CreateStartOfDate(sqlExpression2, sqlExpression3, null);
			case ValueKind.DateTime:
				return this.CreateStartOfDateTime(sqlExpression2, sqlExpression3, null, null, null, null, null);
			case ValueKind.DateTimeZone:
				return this.CreateStartOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), sqlExpression2, sqlExpression3, null, null, null, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x00057E80 File Offset: 0x00056080
		private SqlExpression CreateEndOfMonth(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			SqlExpression sqlExpression2 = SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression);
			SqlExpression sqlExpression3 = SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression);
			switch (typeKind)
			{
			case ValueKind.Date:
				return this.CreateEndOfDate(sqlExpression2, sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString), new SqlExpression[] { SqlAstCreator.EndOfMonth(sqlExpression) }));
			case ValueKind.DateTime:
				return this.CreateEndOfDateTime(sqlExpression2, sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString), new SqlExpression[] { SqlAstCreator.EndOfMonth(sqlExpression) }), null, null, null, null);
			case ValueKind.DateTimeZone:
				return this.CreateEndOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), sqlExpression2, sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString), new SqlExpression[] { SqlAstCreator.EndOfMonth(sqlExpression) }), null, null, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x00057F88 File Offset: 0x00056188
		protected override SqlExpression CreateDateTimeStartOfDay(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			switch (type.TypeKind)
			{
			case ValueKind.Date:
				return sqlExpression;
			case ValueKind.DateTime:
				return new CastCall
				{
					Type = new SqlDataType(type, this.SupportsDateTime2 ? SqlLanguageStrings.DateTime2SqlString : SqlLanguageStrings.DateTimeSqlString),
					Expression = new CastCall
					{
						Type = SqlDataType.Date,
						Expression = sqlExpression
					}
				};
			case ValueKind.DateTimeZone:
				return this.CreateStartOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression), null, null, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x00058068 File Offset: 0x00056268
		private SqlExpression CreateEndOfDay(IInvocationExpression invocation)
		{
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			switch (typeKind)
			{
			case ValueKind.Date:
				return sqlExpression;
			case ValueKind.DateTime:
				return this.CreateEndOfDateTime(SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression), null, null, null, null);
			case ValueKind.DateTimeZone:
				return this.CreateEndOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression), null, null, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x0005812C File Offset: 0x0005632C
		private SqlExpression CreateStartOfHour(IInvocationExpression invocation)
		{
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = SqlAstCreator.CreateDatePartFunction(DatePart.Hour, sqlExpression);
			switch (typeKind)
			{
			case ValueKind.Time:
				return this.CreateStartOfTime(sqlExpression2, null, null, null);
			case ValueKind.DateTime:
				return this.CreateStartOfDateTime(SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression), sqlExpression2, null, null, null);
			case ValueKind.DateTimeZone:
				return this.CreateStartOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression), sqlExpression2, null, null, null);
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x00058208 File Offset: 0x00056408
		private SqlExpression CreateEndOfHour(IInvocationExpression invocation)
		{
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = SqlAstCreator.CreateDatePartFunction(DatePart.Hour, sqlExpression);
			switch (typeKind)
			{
			case ValueKind.Time:
				return this.CreateEndOfTime(sqlExpression2, null, null, null);
			case ValueKind.DateTime:
				return this.CreateEndOfDateTime(SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression), sqlExpression2, null, null, null);
			case ValueKind.DateTimeZone:
				return this.CreateEndOfDateTimeOffset(SqlAstCreator.CreateDatePartFunction(DatePart.TzOffset, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Year, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression), SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression), sqlExpression2, null, null, null);
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000582E4 File Offset: 0x000564E4
		private SqlExpression CreateStartOfTime(SqlExpression hour, SqlExpression minute = null, SqlExpression second = null, SqlExpression ticks = null)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TimeFromPartsSqlString), new SqlExpression[]
			{
				hour,
				minute ?? SqlConstant.Zero,
				second ?? SqlConstant.Zero,
				ticks ?? SqlConstant.Zero,
				SqlConstant.Seven
			});
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x0005833C File Offset: 0x0005653C
		private SqlExpression CreateEndOfTime(SqlExpression hour, SqlExpression minute = null, SqlExpression second = null, SqlExpression ticks = null)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TimeFromPartsSqlString), new SqlExpression[]
			{
				hour,
				minute ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(59),
				second ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(59),
				ticks ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(9999999),
				SqlConstant.Seven
			});
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x0005839B File Offset: 0x0005659B
		private SqlExpression CreateStartOfDate(SqlExpression year, SqlExpression month = null, SqlExpression day = null)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateFromPartsSqlString), new SqlExpression[]
			{
				year,
				month ?? SqlConstant.One,
				day ?? SqlConstant.One
			});
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000583D0 File Offset: 0x000565D0
		private SqlExpression CreateEndOfDate(SqlExpression year, SqlExpression month = null, SqlExpression day = null)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateFromPartsSqlString), new SqlExpression[]
			{
				year,
				month ?? SqlConstant.Twelve,
				day ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(31)
			});
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x00058408 File Offset: 0x00056608
		private SqlExpression CreateStartOfDateTime(SqlExpression year, SqlExpression month = null, SqlExpression day = null, SqlExpression hour = null, SqlExpression minute = null, SqlExpression second = null, SqlExpression ticks = null)
		{
			if (!this.SupportsDateTime2)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateTimeFromPartsSqlString), new SqlExpression[]
				{
					year,
					month ?? SqlConstant.One,
					day ?? SqlConstant.One,
					hour ?? SqlConstant.Zero,
					minute ?? SqlConstant.Zero,
					second ?? SqlConstant.Zero,
					ticks ?? SqlConstant.Zero
				});
			}
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateTime2FromPartsSqlString), new SqlExpression[]
			{
				year,
				month ?? SqlConstant.One,
				day ?? SqlConstant.One,
				hour ?? SqlConstant.Zero,
				minute ?? SqlConstant.Zero,
				second ?? SqlConstant.Zero,
				ticks ?? SqlConstant.Zero,
				SqlConstant.Seven
			});
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000584FC File Offset: 0x000566FC
		private SqlExpression CreateEndOfDateTime(SqlExpression year, SqlExpression month = null, SqlExpression day = null, SqlExpression hour = null, SqlExpression minute = null, SqlExpression second = null, SqlExpression ticks = null)
		{
			if (!this.SupportsDateTime2)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateTimeFromPartsSqlString), new SqlExpression[]
				{
					year,
					month ?? SqlConstant.Twelve,
					day ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(31),
					hour ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(23),
					minute ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(59),
					second ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(59),
					ticks ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(999)
				});
			}
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateTime2FromPartsSqlString), new SqlExpression[]
			{
				year,
				month ?? SqlConstant.Twelve,
				day ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(31),
				hour ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(23),
				minute ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(59),
				second ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(59),
				ticks ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(9999999),
				SqlConstant.Seven
			});
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x0005860C File Offset: 0x0005680C
		private SqlExpression CreateStartOfDateTimeOffset(SqlExpression tzOffset, SqlExpression year, SqlExpression month = null, SqlExpression day = null, SqlExpression hour = null, SqlExpression minute = null, SqlExpression second = null, SqlExpression ticks = null)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateTimeOffsetFromPartsSqlString), new SqlExpression[]
			{
				year,
				month ?? SqlConstant.One,
				day ?? SqlConstant.One,
				hour ?? SqlConstant.Zero,
				minute ?? SqlConstant.Zero,
				second ?? SqlConstant.Zero,
				ticks ?? SqlConstant.Zero,
				new BinaryScalarOperation(tzOffset, BinaryScalarOperator.Divide, SqlConstant.MinutesPerHour),
				new BinaryScalarOperation(tzOffset, BinaryScalarOperator.Modulo, SqlConstant.MinutesPerHour),
				SqlConstant.Seven
			});
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000586B0 File Offset: 0x000568B0
		private SqlExpression CreateEndOfDateTimeOffset(SqlExpression tzOffset, SqlExpression year, SqlExpression month = null, SqlExpression day = null, SqlExpression hour = null, SqlExpression minute = null, SqlExpression second = null, SqlExpression ticks = null)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateTimeOffsetFromPartsSqlString), new SqlExpression[]
			{
				year,
				month ?? SqlConstant.Twelve,
				day ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(31),
				hour ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(23),
				minute ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(59),
				second ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(59),
				ticks ?? SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(9999999),
				new BinaryScalarOperation(tzOffset, BinaryScalarOperator.Divide, SqlConstant.MinutesPerHour),
				new BinaryScalarOperation(tzOffset, BinaryScalarOperator.Modulo, SqlConstant.MinutesPerHour),
				SqlConstant.Seven
			});
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x00058760 File Offset: 0x00056960
		private SqlExpression CreateQuarterOfYear(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreator.CreateDatePartFunction(DatePart.Quarter, sqlExpression);
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0005878C File Offset: 0x0005698C
		private SqlExpression CreateWeekOfYear(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreator.CreateDatePartFunction(DatePart.Week, sqlExpression);
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x000587B8 File Offset: 0x000569B8
		private SqlExpression CreateDayOfYear(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreator.CreateDatePartFunction(DatePart.DayOfYear, sqlExpression);
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x000587E4 File Offset: 0x000569E4
		private SqlExpression CreateDayOfWeek(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreator.CreateDatePartFunction(DatePart.WeekDay, sqlExpression), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(6)), new VariableReference(Alias.NewNativeAlias(SqlLanguageStrings.__DateFirstSqlString)));
			if (invocation.Arguments.Count == 2)
			{
				IExpression expression = invocation.Arguments[1];
				if (base.GetType(expression).TypeKind != ValueKind.Null)
				{
					sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression2, base.CreateScalarExpression(expression));
				}
			}
			return this.CreateNumberMod(sqlExpression2, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(7), null);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x00058874 File Offset: 0x00056A74
		private SqlExpression CreateDateMonth(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreator.CreateDatePartFunction(DatePart.Month, sqlExpression);
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x000588A0 File Offset: 0x00056AA0
		private SqlExpression CreateDateDay(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreator.CreateDatePartFunction(DatePart.Day, sqlExpression);
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x000588CC File Offset: 0x00056ACC
		private SqlExpression CreateTimeHour(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreator.CreateDatePartFunction(DatePart.Hour, sqlExpression);
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x000588F8 File Offset: 0x00056AF8
		private SqlExpression CreateTimeMinute(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return SqlAstCreator.CreateDatePartFunction(DatePart.Minute, sqlExpression);
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00058924 File Offset: 0x00056B24
		private SqlExpression CreateTimeSecond(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = SqlAstCreator.CreateDatePartFunction(DatePart.Second, sqlExpression);
			SqlExpression sqlExpression3;
			if (this.SupportsNanoSecond)
			{
				sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreator.CreateDatePartFunction(DatePart.Nanosecond, sqlExpression), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(1000000000m));
			}
			else
			{
				sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreator.CreateDatePartFunction(DatePart.Millisecond, sqlExpression), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(1000m));
			}
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, sqlExpression3);
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x0005899F File Offset: 0x00056B9F
		private static SqlExpression CreateDatePartFunction(DatePart datePart, SqlExpression date)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DatePartSqlString), new SqlExpression[] { datePart, date });
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x000589BE File Offset: 0x00056BBE
		protected override SqlExpression CreateDurationTotalDays(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(base.CreateScalarExpression(invocation.Arguments[0]), this.Convert(SqlDataType.Float, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(864000000000L)));
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x000589F0 File Offset: 0x00056BF0
		private SqlExpression CreateDurationTotalHours(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(base.CreateScalarExpression(invocation.Arguments[0]), this.Convert(SqlDataType.Float, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(36000000000L)));
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x00058A22 File Offset: 0x00056C22
		private SqlExpression CreateDurationTotalMinutes(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(base.CreateScalarExpression(invocation.Arguments[0]), this.Convert(SqlDataType.Float, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(600000000L)));
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x00058A51 File Offset: 0x00056C51
		private SqlExpression CreateDurationTotalSeconds(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(base.CreateScalarExpression(invocation.Arguments[0]), this.Convert(SqlDataType.Float, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(10000000L)));
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x00058A80 File Offset: 0x00056C80
		private SqlExpression CreateToInt64(IInvocationExpression invocation)
		{
			switch (base.GetType(invocation.Arguments[0]).TypeKind)
			{
			case ValueKind.Time:
			{
				SqlExpression sqlExpression = this.CreateNumberFrom(invocation);
				return this.Convert(SqlDataType.BigInt, SqlAstCreator.CreateRound(sqlExpression, SqlConstant.Zero));
			}
			case ValueKind.Date:
				return this.Convert(SqlDataType.BigInt, this.ConvertDateToNumber(base.CreateScalarExpression(invocation.Arguments[0])));
			case ValueKind.DateTime:
			case ValueKind.DateTimeZone:
			{
				SqlExpression sqlExpression2 = this.CreateNumberFrom(invocation);
				return this.Convert(SqlDataType.BigInt, SqlAstCreator.CreateRound(sqlExpression2, SqlConstant.Zero));
			}
			case ValueKind.Number:
			{
				SqlExpression sqlExpression3 = base.CreateScalarExpression(invocation.Arguments[0]);
				return this.Convert(SqlDataType.BigInt, SqlAstCreator.CreateRound(sqlExpression3, SqlConstant.Zero));
			}
			case ValueKind.Logical:
				return this.Convert(SqlDataType.BigInt, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(base.CreateConditionExpression(invocation.Arguments[0])).Then(SqlConstant.One)
					.Else(SqlConstant.Zero));
			case ValueKind.Text:
			{
				SqlExpression sqlExpression4 = this.CreateNumberFrom(invocation);
				return this.Convert(SqlDataType.BigInt, SqlAstCreator.CreateRound(sqlExpression4, SqlConstant.Zero));
			}
			}
			return this.Convert(SqlDataType.BigInt, base.CreateScalarExpression(invocation.Arguments[0]));
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected override SqlExpression CreateToSingle(IInvocationExpression invocation)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x00058BEC File Offset: 0x00056DEC
		protected override SqlExpression CreateToDouble(IInvocationExpression invocation)
		{
			if (base.GetType(invocation.Arguments[0]).TypeKind == ValueKind.Number)
			{
				return this.Convert(SqlDataType.Float, base.CreateScalarExpression(invocation.Arguments[0]));
			}
			return this.CreateNumberFrom(invocation);
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x00058C38 File Offset: 0x00056E38
		protected override SqlExpression CreateToDecimal(IInvocationExpression invocation)
		{
			return this.CreateToDecimalType(invocation, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(38, 17));
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x00058C4A File Offset: 0x00056E4A
		private SqlExpression CreateToCurrency(IInvocationExpression invocation)
		{
			return this.CreateToDecimalType(invocation, SqlDataType.Money);
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x00058C58 File Offset: 0x00056E58
		private SqlExpression CreateToDecimalType(IInvocationExpression invocation, SqlDataType sqlType)
		{
			TypeValue type = base.GetType(invocation.Arguments[0]);
			switch (type.TypeKind)
			{
			case ValueKind.Time:
			{
				SqlExpression sqlExpression = this.CreateNumberFrom(invocation);
				return this.Convert(sqlType, sqlExpression);
			}
			case ValueKind.Date:
				return this.Convert(sqlType, this.ConvertDateToNumber(base.CreateScalarExpression(invocation.Arguments[0])));
			case ValueKind.DateTime:
			case ValueKind.DateTimeZone:
			{
				SqlExpression sqlExpression2 = this.CreateNumberFrom(invocation);
				return this.Convert(sqlType, sqlExpression2);
			}
			case ValueKind.Number:
				if (this.ExternalEnvironment.GetSqlScalarType(type.NonNullable).Type.Equals(sqlType.Type))
				{
					return base.CreateScalarExpression(invocation.Arguments[0]);
				}
				break;
			case ValueKind.Logical:
				return this.Convert(sqlType, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(base.CreateConditionExpression(invocation.Arguments[0])).Then(SqlConstant.One)
					.Else(SqlConstant.Zero));
			case ValueKind.Text:
			{
				SqlExpression sqlExpression3 = this.CreateNumberFrom(invocation);
				return this.Convert(sqlType, sqlExpression3);
			}
			}
			return this.Convert(sqlType, base.CreateScalarExpression(invocation.Arguments[0]));
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x00058DA0 File Offset: 0x00056FA0
		private SqlExpression CreateToLogical(IInvocationExpression invocation)
		{
			ValueKind typeKind = base.GetType(invocation.Arguments[0]).TypeKind;
			if (typeKind == ValueKind.Logical)
			{
				return base.CreateScalarExpression(invocation.Arguments[0]);
			}
			if (typeKind != ValueKind.Text)
			{
				return new BinaryLogicalOperation(base.CreateScalarExpression(invocation.Arguments[0]), BinaryLogicalOperator.NotEqualTo, SqlConstant.Zero);
			}
			return new BinaryLogicalOperation(this.Convert(SqlDataType.Bit, base.CreateScalarExpression(invocation.Arguments[0])), BinaryLogicalOperator.Equals, SqlConstant.One);
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x00058E28 File Offset: 0x00057028
		private static SqlExpression CreateSwitchOffset(SqlExpression expression, double timeZoneOffsetMins)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SwitchOffsetSqlString), new SqlExpression[]
			{
				expression,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeZoneOffsetMins, true)
			});
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x00058E4D File Offset: 0x0005704D
		private static SqlExpression CreateRound(SqlExpression expression, SqlExpression roundingExpression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RoundSqlString), new SqlExpression[] { expression, roundingExpression });
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00058E6C File Offset: 0x0005706C
		private static bool IsWholeNumberType(SqlDataType type)
		{
			return type.Equals(SqlDataType.TinyInt) || type.Equals(SqlDataType.SmallInt) || type.Equals(SqlDataType.Int) || type.Equals(SqlDataType.BigInt);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x00058EA4 File Offset: 0x000570A4
		protected override SqlExpression CreateTextTrim(IInvocationExpression invocation)
		{
			if (this.SupportsTrimFrom)
			{
				List<SqlExpression> list = new List<SqlExpression>();
				foreach (int num in SqlConstant.MLanguageWhitespaceCodePoints)
				{
					list.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.NCharSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(num) }));
				}
				return new SqlAstCreator.TrimFromExpression(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ConcatSqlString), list.ToArray()), base.CreateScalarExpression(invocation.Arguments[0]));
			}
			return base.CreateTextTrim(invocation);
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x00058F50 File Offset: 0x00057150
		private static SqlExpression EndOfMonth(SqlExpression date)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.EoMonthString), new SqlExpression[] { date });
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x00058F6C File Offset: 0x0005716C
		protected override OutputClause CreateOutputClause(Alias alias, TableTypeValue tableType)
		{
			if (!this.countOnly)
			{
				Keys keys = tableType.ItemType.Fields.Keys;
				List<SelectItem> list = new List<SelectItem>(keys.Length);
				int i = 0;
				while (i < keys.Length)
				{
					Alias alias2 = Alias.NewNativeAlias(keys[i]);
					ColumnReference columnReference = new ColumnReference(alias, alias2);
					TypeValue asType = tableType.ItemType.Fields[i]["Type"].AsType;
					string text;
					if (!TypeServices.TryGetSerializationFormat(asType, out text))
					{
						goto IL_00BE;
					}
					if (!(text == "GeographyWKT"))
					{
						if (!(text == "GeometryWKT"))
						{
							goto IL_00BE;
						}
						list.Add(new SelectItem(this.ConstructGeospatialExpression(columnReference, false), alias2));
					}
					else
					{
						list.Add(new SelectItem(this.ConstructGeospatialExpression(columnReference, true), alias2));
					}
					IL_00EF:
					i++;
					continue;
					IL_00BE:
					if (TypeServices.IsSerializedText(asType))
					{
						list.Add(new SelectItem(this.Convert(SqlDataType.NVarChar, columnReference), alias2));
						goto IL_00EF;
					}
					list.Add(new SelectItem(columnReference));
					goto IL_00EF;
				}
				return new SqlAstCreator.SqlOutputClause(list);
			}
			return OutputClause.Null;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x00059084 File Offset: 0x00057284
		private SqlExpression ConstructGeospatialExpression(ColumnReference column, bool isGeography)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Equals(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(isGeography ? SpatialUtilities.DefaultGeographyCoordinateSystemID : SpatialUtilities.DefaultGeometryCoordinateSystemID), SqlAstCreatorBase<DbAstCreator.SqlVariable>.FieldAccess(column, SqlLanguageStrings.STSridSqlString))).Then(this.Convert(SqlDataType.NVarChar, column))
				.Else(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("SRID="), this.Convert(SqlDataType.NVarChar, SqlAstCreatorBase<DbAstCreator.SqlVariable>.FieldAccess(column, SqlLanguageStrings.STSridSqlString))), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(";"), this.Convert(SqlDataType.NVarChar, column))));
		}

		// Token: 0x04000C6C RID: 3180
		private const int MaxLengthStringLiteral = 4000;

		// Token: 0x04000C6D RID: 3181
		private const string VariablePrefix = "@Variable";

		// Token: 0x04000C6E RID: 3182
		private static readonly char[] LikeWildcardCharacters = new char[] { '%', '_', '[' };

		// Token: 0x04000C6F RID: 3183
		private static readonly Regex guidPattern = new Regex("^[0-9A-F]{8}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{4}-[0-9A-F]{12}$", RegexOptions.IgnoreCase);

		// Token: 0x04000C70 RID: 3184
		private readonly bool supportsDateTime2;

		// Token: 0x04000C71 RID: 3185
		private readonly bool supportsTrimFrom;

		// Token: 0x020003A8 RID: 936
		private sealed class SqlOutputClause : OutputClause
		{
			// Token: 0x060020EA RID: 8426 RVA: 0x00059155 File Offset: 0x00057355
			public SqlOutputClause(List<SelectItem> columnList)
				: base(columnList)
			{
			}

			// Token: 0x060020EB RID: 8427 RVA: 0x0005915E File Offset: 0x0005735E
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.WriteLine();
				writer.WriteSpaceAfter(SqlLanguageStrings.OutputSqlString);
				base.WriteColumns(writer);
			}
		}

		// Token: 0x020003A9 RID: 937
		private sealed class TrimFromExpression : SqlExpression
		{
			// Token: 0x060020EC RID: 8428 RVA: 0x00059178 File Offset: 0x00057378
			public TrimFromExpression(SqlExpression characters, SqlExpression expression)
			{
				this.characters = characters;
				this.expression = expression;
			}

			// Token: 0x17000E2F RID: 3631
			// (get) Token: 0x060020ED RID: 8429 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060020EE RID: 8430 RVA: 0x00059190 File Offset: 0x00057390
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.Write(SqlLanguageStrings.TrimSqlString);
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				writer.WriteSubexpression(10, this.characters);
				writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.FromSqlString);
				writer.WriteSubexpression(10, this.expression);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			}

			// Token: 0x04000C72 RID: 3186
			private readonly SqlExpression characters;

			// Token: 0x04000C73 RID: 3187
			private readonly SqlExpression expression;
		}
	}
}
