using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Teradata
{
	// Token: 0x020002D3 RID: 723
	internal sealed class TeradataAstCreator : NoOutputClauseDbAstCreator
	{
		// Token: 0x06001C9A RID: 7322 RVA: 0x00045C03 File Offset: 0x00043E03
		private TeradataAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x06001C9B RID: 7323 RVA: 0x000020FA File Offset: 0x000002FA
		protected override SqlDataType[] AdjustArgumentsForType(TypeValue[] types)
		{
			return null;
		}

		// Token: 0x06001C9C RID: 7324 RVA: 0x00045C10 File Offset: 0x00043E10
		protected override ScalarExpression Constant(Value constant, TypeValue type)
		{
			if (!constant.IsNull)
			{
				type = type.NonNullable;
				ValueKind typeKind = type.TypeKind;
				if (typeKind != ValueKind.DateTime)
				{
					if (typeKind != ValueKind.Number)
					{
						if (typeKind == ValueKind.Binary)
						{
							if (constant.Type.TypeKind == ValueKind.Number)
							{
								return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(BitConverter.GetBytes(constant.AsNumber.AsInteger64));
							}
						}
					}
					else
					{
						if (type.Equals(TypeValue.Single) || type.Equals(TypeValue.Double))
						{
							return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsNumber.AsDecimal);
						}
						if (type.Equals(TypeValue.Byte))
						{
							return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(BitConverter.GetBytes(constant.AsNumber.AsInteger64));
						}
						if (constant.Type.TypeKind == ValueKind.Logical)
						{
							return this.Constant(constant.AsBoolean);
						}
					}
				}
				else if (constant.Type.TypeKind == ValueKind.DateTimeZone)
				{
					return this.Constant(constant, constant.Type);
				}
			}
			return base.Constant(constant, type);
		}

		// Token: 0x06001C9D RID: 7325 RVA: 0x00045D00 File Offset: 0x00043F00
		protected override SqlConstant IntervalConstant(TimeSpan value)
		{
			return new SqlConstant(ConstantType.Interval, string.Format(CultureInfo.InvariantCulture, "{0:00} {1:00}:{2:00}:{3:00}.{4:000000}", new object[]
			{
				value.Days,
				value.Hours,
				value.Minutes,
				value.Seconds,
				value.Ticks % 10000000L
			}));
		}

		// Token: 0x06001C9E RID: 7326 RVA: 0x00045D7B File Offset: 0x00043F7B
		public static TeradataAstCreator Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			return new TeradataAstCreator(expression, cursor, externalEnvironment);
		}

		// Token: 0x06001C9F RID: 7327 RVA: 0x00045D88 File Offset: 0x00043F88
		protected override SqlExpression CreateAddOperation(IBinaryExpression add)
		{
			IExpression left = add.Left;
			if (base.GetType(left).TypeKind == ValueKind.Text)
			{
				return this.CreateBinaryScalarOperation(BinaryScalarOperator.Concatenate, add);
			}
			return this.CreateBinaryScalarOperation(BinaryScalarOperator.Add, add);
		}

		// Token: 0x06001CA0 RID: 7328 RVA: 0x00045DBC File Offset: 0x00043FBC
		public override SqlQueryExpression CreatePagingQuery(SqlExpression sourceQuery, string[] columnNames, string[] sortColumnNames, long offsetExpression, long? fetchExpression)
		{
			SqlQueryExpression sqlQueryExpression = base.CreatePagingQuery(sourceQuery, columnNames, sortColumnNames, offsetExpression, fetchExpression);
			PagingQuerySpecification pagingQuerySpecification = sqlQueryExpression as PagingQuerySpecification;
			if (pagingQuerySpecification != null && pagingQuerySpecification.PagingClause != null && pagingQuerySpecification.PagingClause.FetchExpression != null && pagingQuerySpecification.RepeatedRowOption == RepeatedRowOption.Distinct)
			{
				PagingClause pagingClause = pagingQuerySpecification.PagingClause;
				pagingQuerySpecification.PagingClause = null;
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromExpression xfromExpression = base.SelectStar(columnNames.Select((string c) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(c, this.dbEnvironment.SqlSettings))).From(pagingQuerySpecification);
				return new PagingQuerySpecification(xfromExpression.SelectItems, xfromExpression.FromItems)
				{
					RepeatedRowOption = RepeatedRowOption.None,
					PagingClause = pagingClause
				};
			}
			return sqlQueryExpression;
		}

		// Token: 0x06001CA1 RID: 7329 RVA: 0x00045E60 File Offset: 0x00044060
		protected override SqlExpression CreateTextContains(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CoalesceSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IndexSqlString), new SqlExpression[]
				{
					base.CreateScalarExpression(arguments[0]),
					base.CreateScalarExpression(arguments[1])
				}),
				SqlConstant.Zero
			}), BinaryLogicalOperator.GreaterThan, SqlConstant.Zero);
		}

		// Token: 0x06001CA2 RID: 7330 RVA: 0x00045ED4 File Offset: 0x000440D4
		protected override SqlExpression CreateTextStartsWith(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CoalesceSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IndexSqlString), new SqlExpression[]
				{
					base.CreateScalarExpression(arguments[0]),
					base.CreateScalarExpression(arguments[1])
				}),
				SqlConstant.Zero
			}), BinaryLogicalOperator.Equals, SqlConstant.One);
		}

		// Token: 0x06001CA3 RID: 7331 RVA: 0x00045F48 File Offset: 0x00044148
		protected override SqlExpression CreateTextEndsWith(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			SqlExpression sqlExpression = base.CreateScalarExpression(arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(arguments[1]);
			ConstantSqlString coalesceSqlString = SqlLanguageStrings.CoalesceSqlString;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(coalesceSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IndexSqlString), new SqlExpression[]
				{
					SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SubstrSqlString), new SqlExpression[]
					{
						sqlExpression,
						SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CharLengthSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(coalesceSqlString), new SqlExpression[]
						{
							sqlExpression,
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("")
						}) }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CharLengthSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(coalesceSqlString), new SqlExpression[]
						{
							sqlExpression2,
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("")
						}) })), SqlConstant.One)
					}),
					sqlExpression2
				}),
				SqlConstant.Zero
			}), BinaryLogicalOperator.Equals, SqlConstant.One);
		}

		// Token: 0x06001CA4 RID: 7332 RVA: 0x00046060 File Offset: 0x00044260
		protected override SqlExpression ConvertNumberToDate(SqlExpression number)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TruncSqlString), new SqlExpression[] { number });
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.BaseOADate, sqlExpression);
		}

		// Token: 0x06001CA5 RID: 7333 RVA: 0x00046094 File Offset: 0x00044294
		protected override SqlExpression ConvertNumberToDateTime(SqlExpression number)
		{
			SqlExpression sqlExpression = new CastCall
			{
				Type = SqlDataType.Timestamp,
				Expression = this.ConvertNumberToDate(number)
			};
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RoundSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Abs(number), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Abs(number))), base.MicrosecondsPerDay) });
			TeradataAstCreator.IntervalExpression intervalExpression = TeradataAstCreator.IntervalExpression.Hour(TeradataAstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(sqlExpression2, base.MicrosecondsPerHour)));
			TeradataAstCreator.IntervalExpression intervalExpression2 = TeradataAstCreator.IntervalExpression.Minute(TeradataAstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(this.CreateNumberMod(sqlExpression2, base.MicrosecondsPerHour, null), base.MicrosecondsPerMinute)));
			TeradataAstCreator.IntervalExpression intervalExpression3 = TeradataAstCreator.IntervalExpression.Second(new CastCall
			{
				Type = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(18, 6),
				Expression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(this.CreateNumberMod(sqlExpression2, base.MicrosecondsPerMinute, null), base.MicrosecondsPerSecond)
			});
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, intervalExpression), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(intervalExpression2, intervalExpression3));
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x00046184 File Offset: 0x00044384
		protected override SqlExpression ConvertDateToNumber(SqlExpression expression)
		{
			CastCall castCall = new CastCall();
			castCall.Type = SqlDataType.Date;
			castCall.Expression = expression;
			SqlExpression sqlExpression = new CastCall
			{
				Type = SqlDataType.Date,
				Expression = base.BaseOADateTime
			};
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(castCall, sqlExpression);
		}

		// Token: 0x06001CA7 RID: 7335 RVA: 0x000461CC File Offset: 0x000443CC
		protected override SqlExpression ConvertDateTimeToNumber(SqlExpression expression)
		{
			SqlExpression sqlExpression = this.ConvertDateToNumber(expression);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression });
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression });
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SecondSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression });
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(sqlExpression2, SqlConstant.SecondsPerHour), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(sqlExpression3, SqlConstant.SecondsPerMinute), sqlExpression4)), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(86400.0, true));
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateOADateTimeSignExpression(expression), sqlExpression5);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression6);
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x0004628D File Offset: 0x0004448D
		protected override SqlExpression CreateDateTimeAddMonths(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.AddMonthsSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x000462A8 File Offset: 0x000444A8
		protected override SqlExpression CreateDateTimeAddYears(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = new BinaryScalarOperation(base.CreateScalarExpression(invocation.Arguments[1]), BinaryScalarOperator.Multiply, SqlConstant.Twelve);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.AddMonthsSqlString), new SqlExpression[] { sqlExpression, sqlExpression2 });
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00046303 File Offset: 0x00044503
		protected override SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.Atan2SqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06001CAB RID: 7339 RVA: 0x0004631C File Offset: 0x0004451C
		protected override SqlExpression CreateNumberMod(SqlExpression number, SqlExpression divisor, IConstantExpression precision = null)
		{
			Func<SqlExpression, SqlExpression> func = base.CreateNumericCastFromPrecision(precision);
			return new BinaryScalarOperation(func(number), BinaryScalarOperator.InlineModulo, func(divisor));
		}

		// Token: 0x06001CAC RID: 7340 RVA: 0x00046345 File Offset: 0x00044545
		protected override SqlExpression CreateNumberNaturalLogarithm(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.LnSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06001CAD RID: 7341 RVA: 0x0004635D File Offset: 0x0004455D
		protected override SqlExpression CreateNumberLogBase10(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.LogSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06001CAE RID: 7342 RVA: 0x00046375 File Offset: 0x00044575
		protected override SqlExpression CreateNumberPower(IInvocationExpression invocation)
		{
			return new BinaryScalarOperation(base.CreateScalarExpression(invocation.Arguments[0]), BinaryScalarOperator.PowerAsteriskVariant, base.CreateScalarExpression(invocation.Arguments[1]));
		}

		// Token: 0x06001CAF RID: 7343 RVA: 0x000463A4 File Offset: 0x000445A4
		protected override SqlExpression CreateToText(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			ValueKind typeKind = base.GetType(expression).TypeKind;
			if (typeKind != ValueKind.Logical)
			{
				if (typeKind != ValueKind.Text)
				{
					return base.CreateFunctionCall(SqlLanguageStrings.ToCharSqlString, Array.Empty<SqlExpression>())(invocation);
				}
				return base.CreateScalarExpression(expression);
			}
			else
			{
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
		}

		// Token: 0x06001CB0 RID: 7344 RVA: 0x000464C8 File Offset: 0x000446C8
		protected override SqlExpression CreateDurationFrom(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = TeradataAstCreator.IntervalExpression.Day(sqlExpression);
			BinaryScalarOperation binaryScalarOperation = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Abs(sqlExpression), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Floor(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Abs(sqlExpression))), SqlConstant.SecondsPerDay);
			TeradataAstCreator.IntervalExpression intervalExpression = TeradataAstCreator.IntervalExpression.Hour(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(binaryScalarOperation, SqlConstant.SecondsPerHour));
			TeradataAstCreator.IntervalExpression intervalExpression2 = TeradataAstCreator.IntervalExpression.Minute(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(new BinaryScalarOperation(binaryScalarOperation, BinaryScalarOperator.InlineModulo, SqlConstant.SecondsPerHour), SqlConstant.SecondsPerMinute));
			TeradataAstCreator.IntervalExpression intervalExpression3 = TeradataAstCreator.IntervalExpression.Second(new BinaryScalarOperation(binaryScalarOperation, BinaryScalarOperator.InlineModulo, SqlConstant.SecondsPerMinute));
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, intervalExpression), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(intervalExpression2, intervalExpression3));
		}

		// Token: 0x06001CB1 RID: 7345 RVA: 0x00046560 File Offset: 0x00044760
		protected override SqlExpression CreateDurationTotalDays(IInvocationExpression invocation)
		{
			IBinaryExpression binaryExpression;
			if (DbAstCreator.TryAsBinaryExpression(invocation.Arguments[0], out binaryExpression))
			{
				IExpression left = binaryExpression.Left;
				IExpression right = binaryExpression.Right;
				SqlExpression sqlExpression = base.CreateScalarExpression(left);
				SqlExpression sqlExpression2 = base.CreateScalarExpression(right);
				if (base.GetType(left).TypeKind == ValueKind.DateTime)
				{
					sqlExpression = new CastCall
					{
						Type = SqlDataType.Date,
						Expression = sqlExpression
					};
				}
				if (base.GetType(right).TypeKind == ValueKind.DateTime)
				{
					sqlExpression2 = new CastCall
					{
						Type = SqlDataType.Date,
						Expression = sqlExpression2
					};
				}
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression, sqlExpression2);
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06001CB2 RID: 7346 RVA: 0x00046609 File Offset: 0x00044809
		protected override SqlExpression Len(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CharLengthSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00046624 File Offset: 0x00044824
		protected override SqlExpression CreateDivideOperation(IBinaryExpression divide)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(divide.Left);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(divide.Right);
			return new BinaryScalarOperation(sqlExpression, BinaryScalarOperator.Divide, sqlExpression2);
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00046651 File Offset: 0x00044851
		protected override SqlExpression CreateBinaryFromText(IInvocationExpression invocation)
		{
			return new CastCall
			{
				Type = new SqlDataType(TypeValue.Binary, SqlLanguageStrings.BinarySqlString),
				Expression = base.CreateScalarExpression(invocation.Arguments[0])
			};
		}

		// Token: 0x06001CB5 RID: 7349 RVA: 0x00046685 File Offset: 0x00044885
		protected override SqlExpression CreateTextTrim(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TrimSqlString), new SqlExpression[] { base.CreateScalarExpression(invocation.Arguments[0]) });
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x000466B1 File Offset: 0x000448B1
		protected override SqlExpression CreateTextTrimEnd(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TrailingSqlString, SqlLanguageStrings.TrimSqlString), new SqlExpression[] { base.CreateScalarExpression(invocation.Arguments[0]) });
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000466E2 File Offset: 0x000448E2
		protected override SqlExpression CreateTextTrimStart(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LeadingSqlString, SqlLanguageStrings.TrimSqlString), new SqlExpression[] { base.CreateScalarExpression(invocation.Arguments[0]) });
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x00046713 File Offset: 0x00044913
		private static SqlExpression CastToInt(SqlExpression expression)
		{
			return new CastCall
			{
				Type = SqlDataType.Int,
				Expression = expression
			};
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x0004672C File Offset: 0x0004492C
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
					Type = SqlDataType.Timestamp,
					Expression = new CastCall
					{
						Type = SqlDataType.Date,
						Expression = sqlExpression
					}
				};
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x020002D4 RID: 724
		private sealed class IntervalExpression : SqlExpression
		{
			// Token: 0x06001CBB RID: 7355 RVA: 0x000467C0 File Offset: 0x000449C0
			private IntervalExpression(SqlExpression expression, ConstantSqlString intervalUnit)
			{
				this.expression = expression;
				this.intervalUnit = intervalUnit;
			}

			// Token: 0x06001CBC RID: 7356 RVA: 0x000467D6 File Offset: 0x000449D6
			public static TeradataAstCreator.IntervalExpression Year(SqlExpression expression)
			{
				return new TeradataAstCreator.IntervalExpression(expression, SqlLanguageStrings.YearSqlString);
			}

			// Token: 0x06001CBD RID: 7357 RVA: 0x000467E3 File Offset: 0x000449E3
			public static TeradataAstCreator.IntervalExpression Day(SqlExpression expression)
			{
				return new TeradataAstCreator.IntervalExpression(expression, SqlLanguageStrings.DaySqlString);
			}

			// Token: 0x06001CBE RID: 7358 RVA: 0x000467F0 File Offset: 0x000449F0
			public static TeradataAstCreator.IntervalExpression Hour(SqlExpression expression)
			{
				return new TeradataAstCreator.IntervalExpression(expression, SqlLanguageStrings.HourSqlString);
			}

			// Token: 0x06001CBF RID: 7359 RVA: 0x000467FD File Offset: 0x000449FD
			public static TeradataAstCreator.IntervalExpression Minute(SqlExpression expression)
			{
				return new TeradataAstCreator.IntervalExpression(expression, SqlLanguageStrings.MinuteSqlString);
			}

			// Token: 0x06001CC0 RID: 7360 RVA: 0x0004680A File Offset: 0x00044A0A
			public static TeradataAstCreator.IntervalExpression Second(SqlExpression expression)
			{
				return new TeradataAstCreator.IntervalExpression(expression, SqlLanguageStrings.SecondSqlString);
			}

			// Token: 0x17000D46 RID: 3398
			// (get) Token: 0x06001CC1 RID: 7361 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06001CC2 RID: 7362 RVA: 0x00046818 File Offset: 0x00044A18
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				this.expression.WriteCreateScript(writer);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
				writer.WriteSpaceBeforeAndAfter(SqlLanguageSymbols.MultiplySqlString);
				writer.WriteSpaceAfter(SqlLanguageStrings.IntervalSqlString);
				writer.WriteSpaceAfter(TeradataAstCreator.IntervalExpression.DigitOneSqlString);
				writer.Write(this.intervalUnit);
			}

			// Token: 0x040009A9 RID: 2473
			private static readonly ConstantSqlString DigitOneSqlString = new ConstantSqlString("'1'");

			// Token: 0x040009AA RID: 2474
			private readonly ConstantSqlString intervalUnit;

			// Token: 0x040009AB RID: 2475
			private readonly SqlExpression expression;
		}
	}
}
