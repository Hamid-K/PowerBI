using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MySQL
{
	// Token: 0x0200090F RID: 2319
	internal sealed class MySQLAstCreator : NoOutputClauseDbAstCreator
	{
		// Token: 0x060041FB RID: 16891 RVA: 0x00045C03 File Offset: 0x00043E03
		private MySQLAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x000DE7BE File Offset: 0x000DC9BE
		protected override Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> functions = base.GetFunctions();
			functions.Add(Library.Text.Replace, base.CreateFunctionCall(SqlLanguageStrings.ReplaceSqlString, Array.Empty<SqlExpression>()));
			return functions;
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x000020FA File Offset: 0x000002FA
		protected override SqlDataType[] AdjustArgumentsForType(TypeValue[] types)
		{
			return null;
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x000DE7E1 File Offset: 0x000DC9E1
		public static MySQLAstCreator Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			return new MySQLAstCreator(expression, cursor, externalEnvironment);
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x000DE7EC File Offset: 0x000DC9EC
		protected override SqlExpression CreateAddOperation(IBinaryExpression add)
		{
			IExpression left = add.Left;
			if (base.GetType(left).TypeKind == ValueKind.Text)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ConcatSqlString), new SqlExpression[]
				{
					base.CreateScalarExpression(left),
					base.CreateScalarExpression(add.Right)
				});
			}
			return this.CreateBinaryScalarOperation(BinaryScalarOperator.Add, add);
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x000DE848 File Offset: 0x000DCA48
		protected override SqlExpression CreateTextContains(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LocateSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[1]),
				base.CreateScalarExpression(arguments[0])
			}), BinaryLogicalOperator.GreaterThan, SqlConstant.Zero);
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x000DE89C File Offset: 0x000DCA9C
		protected override SqlExpression ConvertNumberToDate(SqlExpression number)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TruncateSqlString), new SqlExpression[]
			{
				number,
				SqlConstant.Zero
			});
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.BaseOADateTime, MySQLAstCreator.IntervalExpression.Day(sqlExpression));
			return new CastCall
			{
				Type = SqlDataType.Date,
				Expression = sqlExpression2
			};
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x000DE8F4 File Offset: 0x000DCAF4
		protected override SqlExpression ConvertNumberToDateTime(SqlExpression number)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(number, SqlConstant.SecondsPerDay);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.BaseOADateTime, MySQLAstCreator.IntervalExpression.Second(sqlExpression));
			return new CastCall
			{
				Type = SqlDataType.DateTime,
				Expression = sqlExpression2
			};
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x000DE938 File Offset: 0x000DCB38
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

		// Token: 0x06004204 RID: 16900 RVA: 0x000DE9C0 File Offset: 0x000DCBC0
		protected override SqlExpression CreateToDate(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			ValueKind typeKind = type.TypeKind;
			if (typeKind == ValueKind.Date)
			{
				return sqlExpression;
			}
			if (typeKind == ValueKind.DateTime)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { sqlExpression });
			}
			return base.CreateToDate(invocation);
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x000DEA1C File Offset: 0x000DCC1C
		protected override SqlExpression CreateToDateTime(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			ValueKind typeKind = type.TypeKind;
			if (typeKind == ValueKind.Date)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TimestampSqlString), new SqlExpression[] { sqlExpression });
			}
			if (typeKind != ValueKind.DateTime)
			{
				return base.CreateToDateTime(invocation);
			}
			return sqlExpression;
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x000DEA78 File Offset: 0x000DCC78
		protected override SqlExpression ConvertDateToNumber(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateDiffSqlString), new SqlExpression[] { expression, base.BaseOADateTime });
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x000DEA9C File Offset: 0x000DCC9C
		protected override SqlExpression ConvertDateTimeToNumber(SqlExpression expression)
		{
			SqlExpression sqlExpression = this.ConvertDateToNumber(expression);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression }), base.MicrosecondsPerHour);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression }), base.MicrosecondsPerMinute);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MicrosecondSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression });
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression3, sqlExpression4)), base.MicrosecondsPerDay);
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(expression, BinaryLogicalOperator.LessThan, base.BaseOADateTime)).Then(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(-1.0, true))
				.Else(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(1.0, true)), sqlExpression5);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression6);
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x000DEB9F File Offset: 0x000DCD9F
		protected override SqlExpression CreateDateTimeAddMonths(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.CreateScalarExpression(invocation.Arguments[0]), MySQLAstCreator.IntervalExpression.Month(base.CreateScalarExpression(invocation.Arguments[1])));
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x000DEBCF File Offset: 0x000DCDCF
		protected override SqlExpression CreateDateTimeAddYears(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.CreateScalarExpression(invocation.Arguments[0]), MySQLAstCreator.IntervalExpression.Year(base.CreateScalarExpression(invocation.Arguments[1])));
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x00046303 File Offset: 0x00044503
		protected override SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.Atan2SqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x00046345 File Offset: 0x00044545
		protected override SqlExpression CreateNumberNaturalLogarithm(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.LnSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x0600420C RID: 16908 RVA: 0x000DEBFF File Offset: 0x000DCDFF
		protected override SqlExpression CreateDurationFrom(IInvocationExpression invocation)
		{
			return MySQLAstCreator.IntervalExpression.Second(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateScalarExpression(invocation.Arguments[0]), SqlConstant.SecondsPerDay));
		}

		// Token: 0x0600420D RID: 16909 RVA: 0x000DEC24 File Offset: 0x000DCE24
		protected override SqlExpression CreateToText(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			if (type.TypeKind != ValueKind.Logical)
			{
				return new CastCall
				{
					Type = new SqlDataType(type, SqlLanguageStrings.CharSqlString),
					Expression = base.CreateScalarExpression(expression)
				};
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

		// Token: 0x0600420E RID: 16910 RVA: 0x00046609 File Offset: 0x00044809
		protected override SqlExpression Len(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CharLengthSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x0600420F RID: 16911 RVA: 0x000DED44 File Offset: 0x000DCF44
		protected override SqlExpression CreateDivideOperation(IBinaryExpression divide)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(divide.Left);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(divide.Right);
			return new BinaryScalarOperation(sqlExpression, BinaryScalarOperator.Divide, sqlExpression2);
		}

		// Token: 0x06004210 RID: 16912 RVA: 0x00046651 File Offset: 0x00044851
		protected override SqlExpression CreateBinaryFromText(IInvocationExpression invocation)
		{
			return new CastCall
			{
				Type = new SqlDataType(TypeValue.Binary, SqlLanguageStrings.BinarySqlString),
				Expression = base.CreateScalarExpression(invocation.Arguments[0])
			};
		}

		// Token: 0x06004211 RID: 16913 RVA: 0x000DED74 File Offset: 0x000DCF74
		protected override SqlExpression CreateDurationTotalDays(IInvocationExpression invocation)
		{
			IBinaryExpression binaryExpression;
			if (DbAstCreator.TryAsBinaryExpression(invocation.Arguments[0], out binaryExpression))
			{
				SqlExpression sqlExpression = base.CreateScalarExpression(binaryExpression.Left);
				SqlExpression sqlExpression2 = base.CreateScalarExpression(binaryExpression.Right);
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateDiffSqlString), new SqlExpression[] { sqlExpression, sqlExpression2 });
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06004212 RID: 16914 RVA: 0x0005028C File Offset: 0x0004E48C
		protected override SqlExpression CreateTextTrim(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.TrimSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06004213 RID: 16915 RVA: 0x000DEDDD File Offset: 0x000DCFDD
		protected override AggregateFunctionCall Stdev(SqlExpression expression)
		{
			return AggregateFunctionCall.StandardDeviation(expression, MySQLAstCreator.StdDevSqlString);
		}

		// Token: 0x06004214 RID: 16916 RVA: 0x000DEDEA File Offset: 0x000DCFEA
		protected override SqlExpression CastToSingle(SqlExpression expression)
		{
			return this.CastToDouble(expression);
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000DEDF3 File Offset: 0x000DCFF3
		protected override SqlExpression CastToDouble(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(expression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(0.0, true));
		}

		// Token: 0x040022B1 RID: 8881
		private const string StdDev = "stddev";

		// Token: 0x040022B2 RID: 8882
		private static readonly ConstantSqlString StdDevSqlString = new ConstantSqlString("stddev");

		// Token: 0x02000910 RID: 2320
		private sealed class IntervalExpression : SqlExpression
		{
			// Token: 0x06004217 RID: 16919 RVA: 0x000DEE1B File Offset: 0x000DD01B
			private IntervalExpression(SqlExpression expression, ConstantSqlString intervalUnit)
			{
				this.expression = expression;
				this.intervalUnit = intervalUnit;
			}

			// Token: 0x06004218 RID: 16920 RVA: 0x000DEE31 File Offset: 0x000DD031
			public static MySQLAstCreator.IntervalExpression Second(SqlExpression expression)
			{
				return new MySQLAstCreator.IntervalExpression(expression, SqlLanguageStrings.SecondSqlString);
			}

			// Token: 0x06004219 RID: 16921 RVA: 0x000DEE3E File Offset: 0x000DD03E
			public static MySQLAstCreator.IntervalExpression Day(SqlExpression expression)
			{
				return new MySQLAstCreator.IntervalExpression(expression, SqlLanguageStrings.DaySqlString);
			}

			// Token: 0x0600421A RID: 16922 RVA: 0x000DEE4B File Offset: 0x000DD04B
			public static MySQLAstCreator.IntervalExpression Month(SqlExpression expression)
			{
				return new MySQLAstCreator.IntervalExpression(expression, SqlLanguageStrings.MonthSqlString);
			}

			// Token: 0x0600421B RID: 16923 RVA: 0x000DEE58 File Offset: 0x000DD058
			public static MySQLAstCreator.IntervalExpression Year(SqlExpression expression)
			{
				return new MySQLAstCreator.IntervalExpression(expression, SqlLanguageStrings.YearSqlString);
			}

			// Token: 0x17001512 RID: 5394
			// (get) Token: 0x0600421C RID: 16924 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600421D RID: 16925 RVA: 0x000DEE65 File Offset: 0x000DD065
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.WriteSpaceAfter(SqlLanguageStrings.IntervalSqlString);
				this.expression.WriteCreateScript(writer);
				writer.WriteSpaceBefore(this.intervalUnit);
			}

			// Token: 0x040022B3 RID: 8883
			private readonly ConstantSqlString intervalUnit;

			// Token: 0x040022B4 RID: 8884
			private readonly SqlExpression expression;
		}
	}
}
