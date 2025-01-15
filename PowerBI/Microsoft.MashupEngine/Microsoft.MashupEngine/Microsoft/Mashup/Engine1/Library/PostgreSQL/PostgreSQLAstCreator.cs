using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.PostgreSQL
{
	// Token: 0x02000533 RID: 1331
	internal sealed class PostgreSQLAstCreator : DbAstCreator
	{
		// Token: 0x06002A94 RID: 10900 RVA: 0x0004FA88 File Offset: 0x0004DC88
		private PostgreSQLAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x06002A95 RID: 10901 RVA: 0x0007FDF8 File Offset: 0x0007DFF8
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

		// Token: 0x06002A96 RID: 10902 RVA: 0x0007FE6C File Offset: 0x0007E06C
		protected override Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> functions = base.GetFunctions();
			functions.Add(Library.Date.Day, this.CreateDatePartExtract(SqlLanguageStrings.DaySqlString));
			functions.Add(Library.Date.DayOfYear, this.CreateDatePartExtract(SqlLanguageStrings.DoySqlString));
			functions.Add(Library.Date.Month, this.CreateDatePartExtract(SqlLanguageStrings.MonthSqlString));
			functions.Add(Library.Date.Year, this.CreateDatePartExtract(SqlLanguageStrings.YearSqlString));
			functions.Add(Library.Date.QuarterOfYear, this.CreateDatePartExtract(SqlLanguageStrings.QuarterSqlString));
			functions.Add(Library.Time.Hour, this.CreateDatePartExtract(SqlLanguageStrings.HourSqlString));
			functions.Add(Library.Time.Minute, this.CreateDatePartExtract(SqlLanguageStrings.MinuteSqlString));
			functions.Add(Library.Time.Second, this.CreateDatePartExtract(SqlLanguageStrings.SecondSqlString));
			functions.Add(Library.Text.Start, base.CreateFunctionCall(SqlLanguageStrings.LeftSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Text.End, base.CreateFunctionCall(SqlLanguageStrings.RightSqlString, Array.Empty<SqlExpression>()));
			functions.Add(Library.Text.Middle, new Func<IInvocationExpression, SqlExpression>(this.CreateTextMiddle));
			functions.Add(Library.Text.PositionOf, new Func<IInvocationExpression, SqlExpression>(this.CreateTextPositionOf));
			functions.Add(Library.Text.Replace, base.CreateFunctionCall(SqlLanguageStrings.ReplaceSqlString, Array.Empty<SqlExpression>()));
			functions.Add(CultureSpecificFunction.ByteFrom, new Func<IInvocationExpression, SqlExpression>(this.CreateNumberFrom));
			functions.Add(CultureSpecificFunction.Int8From, new Func<IInvocationExpression, SqlExpression>(this.CreateNumberFrom));
			functions.Add(CultureSpecificFunction.Int16From, new Func<IInvocationExpression, SqlExpression>(this.CreateNumberFrom));
			functions.Add(CultureSpecificFunction.Int32From, new Func<IInvocationExpression, SqlExpression>(this.CreateNumberFrom));
			functions.Add(CultureSpecificFunction.Int64From, new Func<IInvocationExpression, SqlExpression>(this.CreateNumberFrom));
			functions.Add(CultureSpecificFunction.DateDayOfWeek, new Func<IInvocationExpression, SqlExpression>(this.CreateDateDayOfWeek));
			return functions;
		}

		// Token: 0x06002A97 RID: 10903 RVA: 0x0008003D File Offset: 0x0007E23D
		public static PostgreSQLAstCreator Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			return new PostgreSQLAstCreator(expression, cursor, externalEnvironment);
		}

		// Token: 0x06002A98 RID: 10904 RVA: 0x00080047 File Offset: 0x0007E247
		protected override SqlConstant Constant(bool value)
		{
			if (!value)
			{
				return SqlConstant.BooleanFalse;
			}
			return SqlConstant.BooleanTrue;
		}

		// Token: 0x06002A99 RID: 10905 RVA: 0x00080058 File Offset: 0x0007E258
		protected override ScalarExpression Constant(Value constant, TypeValue type)
		{
			if (!constant.IsNull)
			{
				type = type.NonNullable;
				if (type.TypeKind == ValueKind.Text && constant.Type.TypeKind == ValueKind.Binary)
				{
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsBinary.AsBytes);
				}
			}
			return base.Constant(constant, type);
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x000800A6 File Offset: 0x0007E2A6
		protected override BinaryLogicalOperation Condition(bool value)
		{
			if (!value)
			{
				return PostgreSQLAstCreator.FalseCondition;
			}
			return PostgreSQLAstCreator.TrueCondition;
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x000800B8 File Offset: 0x0007E2B8
		protected override Condition ConvertToCondition(SqlExpression expression)
		{
			Condition condition = expression as Condition;
			if (condition != null)
			{
				return condition;
			}
			if (expression == this.Constant(true))
			{
				return this.Condition(true);
			}
			if (expression == this.Constant(false))
			{
				return this.Condition(false);
			}
			return new BinaryLogicalOperation(base.ConvertToScalar(expression), BinaryLogicalOperator.Equals, SqlConstant.BooleanTrue);
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x00080108 File Offset: 0x0007E308
		protected override SqlExpression CreateToNonIntType(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			ValueKind typeKind = type.TypeKind;
			if (typeKind == ValueKind.Date)
			{
				return this.ConvertDateToNumber(sqlExpression);
			}
			if (typeKind != ValueKind.DateTime)
			{
				return sqlExpression;
			}
			return this.ConvertDateTimeToNumber(sqlExpression);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected override SqlExpression CreateToSingle(IInvocationExpression invocation)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06002A9E RID: 10910 RVA: 0x00080154 File Offset: 0x0007E354
		protected override SqlDataType[] AdjustArgumentsForType(TypeValue[] types)
		{
			bool[] array = new bool[types.Length];
			bool flag = false;
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i].TypeKind != ValueKind.Number)
				{
					return null;
				}
				array[i] = PostgreSQLAstCreator.IsCurrency(types[i]);
				flag = flag || array[i];
			}
			if (!flag)
			{
				return null;
			}
			SqlDataType[] array2 = new SqlDataType[types.Length];
			for (int j = 0; j < types.Length; j++)
			{
				if (array[j])
				{
					array2[j] = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(19, 2);
				}
			}
			return array2;
		}

		// Token: 0x06002A9F RID: 10911 RVA: 0x000801D0 File Offset: 0x0007E3D0
		private SqlExpression CreateDateDayOfWeek(IInvocationExpression invocation)
		{
			int count = invocation.Arguments.Count;
			if (count == 1)
			{
				return this.CreateDatePartExtract(SqlLanguageStrings.DowSqlString)(invocation);
			}
			if (count != 2)
			{
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DowSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.Seven), sqlExpression2);
			SqlExpression sqlExpression4 = new CastCall
			{
				Type = SqlDataType.Int,
				Expression = sqlExpression3
			};
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[]
			{
				sqlExpression4,
				SqlConstant.Seven
			});
		}

		// Token: 0x06002AA0 RID: 10912 RVA: 0x000802AC File Offset: 0x0007E4AC
		protected override SqlExpression CreateToDate(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			ValueKind typeKind = type.TypeKind;
			if (typeKind - ValueKind.Date <= 2)
			{
				return new CastCall
				{
					Type = SqlDataType.Date,
					Expression = sqlExpression
				};
			}
			if (typeKind != ValueKind.Number)
			{
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
			return this.ConvertNumberToDate(sqlExpression);
		}

		// Token: 0x06002AA1 RID: 10913 RVA: 0x00080318 File Offset: 0x0007E518
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
					Expression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { sqlExpression })
				};
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06002AA2 RID: 10914 RVA: 0x000803A0 File Offset: 0x0007E5A0
		protected override SqlExpression CreateToDateTime(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			ValueKind typeKind = type.TypeKind;
			if (typeKind - ValueKind.Date <= 2)
			{
				return new CastCall
				{
					Type = SqlDataType.Timestamp,
					Expression = sqlExpression
				};
			}
			if (typeKind != ValueKind.Number)
			{
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
			return this.ConvertNumberToDateTime(sqlExpression);
		}

		// Token: 0x06002AA3 RID: 10915 RVA: 0x0008040C File Offset: 0x0007E60C
		protected override SqlExpression CreateNumberFrom(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			switch (type.TypeKind)
			{
			case ValueKind.Date:
			case ValueKind.DateTime:
				return this.CreateToDouble(invocation);
			case ValueKind.Number:
				return sqlExpression;
			case ValueKind.Logical:
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(sqlExpression)).Then(SqlConstant.Null)
					.When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, this.Constant(true)))
					.Then(SqlConstant.One)
					.Else(SqlConstant.Zero);
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06002AA4 RID: 10916 RVA: 0x000804D8 File Offset: 0x0007E6D8
		protected override SqlExpression CreateAddOperation(IBinaryExpression add)
		{
			IExpression left = add.Left;
			if (base.GetType(left).TypeKind == ValueKind.Text)
			{
				return this.CreateBinaryScalarOperation(BinaryScalarOperator.Concatenate, add);
			}
			return this.CreateBinaryScalarOperation(BinaryScalarOperator.Add, add);
		}

		// Token: 0x06002AA5 RID: 10917 RVA: 0x0008050C File Offset: 0x0007E70C
		protected override SqlExpression CastToDouble(SqlExpression expression)
		{
			return new CastCall
			{
				Type = new SqlDataType(TypeValue.Double, SqlLanguageStrings.DoublePrecisionSqlString),
				Expression = new CastCall
				{
					Type = new SqlDataType(TypeValue.Decimal, SqlLanguageStrings.NumericSqlString),
					Expression = expression
				}
			};
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x0008055A File Offset: 0x0007E75A
		protected override SqlExpression CastToDecimal(SqlExpression expression)
		{
			return new CastCall
			{
				Type = SqlDataType.Decimal,
				Expression = expression
			};
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x00080574 File Offset: 0x0007E774
		protected override SqlExpression CreateListAverage(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue itemType = base.GetType(expression).AsListType.ItemType;
			Func<SqlExpression, SqlExpression> func = (SqlExpression o) => o;
			SqlConstant sqlConstant;
			switch (itemType.TypeKind)
			{
			case ValueKind.Time:
				sqlConstant = PostgreSQLAstCreator.TimeAggregateBase;
				break;
			case ValueKind.Date:
				sqlConstant = PostgreSQLAstCreator.DateTimeAggregateBase;
				func = (SqlExpression o) => PostgreSQLAstCreator.IntervalExpression.Day(o);
				break;
			case ValueKind.DateTime:
				sqlConstant = PostgreSQLAstCreator.DateTimeAggregateBase;
				break;
			case ValueKind.DateTimeZone:
				sqlConstant = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(PostgreSQLAstCreator.BaseDateTime.AddDefaultTimeZone(base.ExternalEnvironment.Host));
				break;
			default:
			{
				Value value = ((invocation.Arguments.Count == 2) ? ((IConstantExpression)invocation.Arguments[1]).Value : Value.Null);
				if (itemType.NonNullable.Equals(TypeValue.Currency) && value.IsNull)
				{
					value = Library.PrecisionEnum.Decimal;
				}
				return base.CreateAggregateFunctionWithOptionalPrecision(expression, value.IsNull ? null : new ConstantExpressionSyntaxNode(value), new Func<SqlExpression, SqlExpression>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg));
			}
			}
			SqlExpression sqlExpression = func(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(base.CreateListAggregateInput(expression), sqlConstant));
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.SecondsPerDay);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.SecondsPerHour);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression }), SqlConstant.SecondsPerMinute);
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SecondSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression });
			SqlExpression sqlExpression6 = base.LiftForGroup(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, sqlExpression3), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression4, sqlExpression5)));
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlConstant, PostgreSQLAstCreator.IntervalExpression.Second(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg(sqlExpression6)));
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x0008079F File Offset: 0x0007E99F
		private Func<IInvocationExpression, SqlExpression> CreateDatePartExtract(ConstantSqlString partname)
		{
			return (IInvocationExpression invocation) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(partname, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { this.CreateScalarExpression(invocation.Arguments[0]) });
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000807C0 File Offset: 0x0007E9C0
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

		// Token: 0x06002AAA RID: 10922 RVA: 0x00080848 File Offset: 0x0007EA48
		private SqlExpression CreateTextPositionOf(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryScalarOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.StrposSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[0]),
				base.CreateScalarExpression(arguments[1])
			}), BinaryScalarOperator.Subtract, SqlConstant.One);
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x0008089C File Offset: 0x0007EA9C
		protected override SqlExpression CreateTextContains(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.StrposSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[0]),
				base.CreateScalarExpression(arguments[1])
			}), BinaryLogicalOperator.GreaterThan, SqlConstant.Zero);
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000808F0 File Offset: 0x0007EAF0
		protected override SqlExpression CreateTextStartsWith(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.StrposSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[0]),
				base.CreateScalarExpression(arguments[1])
			}), BinaryLogicalOperator.Equals, SqlConstant.One);
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x00080944 File Offset: 0x0007EB44
		protected override SqlExpression CreateDateTimeAddMonths(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.CreateScalarExpression(invocation.Arguments[0]), PostgreSQLAstCreator.IntervalExpression.Month(base.CreateScalarExpression(invocation.Arguments[1])));
		}

		// Token: 0x06002AAE RID: 10926 RVA: 0x00080974 File Offset: 0x0007EB74
		protected override SqlExpression CreateDateTimeAddYears(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.CreateScalarExpression(invocation.Arguments[0]), PostgreSQLAstCreator.IntervalExpression.Year(base.CreateScalarExpression(invocation.Arguments[1])));
		}

		// Token: 0x06002AAF RID: 10927 RVA: 0x00046303 File Offset: 0x00044503
		protected override SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.Atan2SqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002AB0 RID: 10928 RVA: 0x00046345 File Offset: 0x00044545
		protected override SqlExpression CreateNumberNaturalLogarithm(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.LnSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002AB1 RID: 10929 RVA: 0x0004635D File Offset: 0x0004455D
		protected override SqlExpression CreateNumberLogBase10(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.LogSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002AB2 RID: 10930 RVA: 0x000809A4 File Offset: 0x0007EBA4
		protected override SqlExpression CreateNumberMod(SqlExpression number, SqlExpression divisor, IConstantExpression precision = null)
		{
			if (precision != null && precision.Value.Equals(Library.PrecisionEnum.Double))
			{
				return new BinaryScalarOperation(this.CastToDouble(number), BinaryScalarOperator.Modulo, this.CastToDouble(divisor));
			}
			return new BinaryScalarOperation(this.Convert(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(38, 6), number), BinaryScalarOperator.Modulo, this.Convert(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(38, 6), divisor));
		}

		// Token: 0x06002AB3 RID: 10931 RVA: 0x00080A00 File Offset: 0x0007EC00
		protected override SqlExpression CreateNumberRound(IInvocationExpression invocation)
		{
			int count = invocation.Arguments.Count;
			if (count == 1)
			{
				return base.CreateFunctionCall(SqlLanguageStrings.RoundSqlString, Array.Empty<SqlExpression>())(invocation);
			}
			if (count != 2)
			{
				throw new InvalidOperationException();
			}
			if (PostgreSQLAstCreator.IsCurrency(base.GetType(invocation.Arguments[0])) || PostgreSQLAstCreator.IsBinaryPrecision(base.GetType(invocation.Arguments[0])))
			{
				SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
				SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
				SqlExpression sqlExpression3 = this.Convert(new SqlDataType(TypeValue.Decimal, SqlLanguageStrings.NumericSqlString), sqlExpression);
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RoundSqlString), new SqlExpression[] { sqlExpression3, sqlExpression2 });
			}
			return base.CreateNumberRound(invocation);
		}

		// Token: 0x06002AB4 RID: 10932 RVA: 0x00080AD7 File Offset: 0x0007ECD7
		protected override SqlExpression ConvertNumberToDate(SqlExpression number)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.BaseOADateTime, PostgreSQLAstCreator.IntervalExpression.Day(number));
		}

		// Token: 0x06002AB5 RID: 10933 RVA: 0x00080AEC File Offset: 0x0007ECEC
		protected override SqlExpression ConvertNumberToDateTime(SqlExpression number)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RoundSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(number, SqlConstant.SecondsPerDay) });
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(number, BinaryLogicalOperator.LessThan, SqlConstant.Zero)).Then(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.CreateNumberMod(sqlExpression, SqlConstant.SecondsPerDay, null), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(2))))
				.Else(sqlExpression), SqlConstant.SecondsPerDay);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(base.BaseOADateTime, PostgreSQLAstCreator.IntervalExpression.Day(sqlExpression2));
		}

		// Token: 0x06002AB6 RID: 10934 RVA: 0x00080B88 File Offset: 0x0007ED88
		protected override SqlExpression ConvertDateToNumber(SqlExpression expression)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(expression, base.BaseOADateTime);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression });
		}

		// Token: 0x06002AB7 RID: 10935 RVA: 0x00080BC0 File Offset: 0x0007EDC0
		protected override SqlExpression ConvertDateTimeToNumber(SqlExpression expression)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(expression, base.BaseOADateTime);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { sqlExpression });
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression }), base.MicrosecondsPerHour);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression }), base.MicrosecondsPerMinute);
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MicrosecondsSqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression });
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression4, sqlExpression5)), base.MicrosecondsPerDay);
			SqlExpression sqlExpression7 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateOADateTimeSignExpression(expression), sqlExpression6);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, sqlExpression7);
		}

		// Token: 0x06002AB8 RID: 10936 RVA: 0x00080CA0 File Offset: 0x0007EEA0
		protected override SqlExpression CreateDurationFrom(IInvocationExpression invocation)
		{
			return PostgreSQLAstCreator.IntervalExpression.Second(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateScalarExpression(invocation.Arguments[0]), SqlConstant.SecondsPerDay));
		}

		// Token: 0x06002AB9 RID: 10937 RVA: 0x00080CC3 File Offset: 0x0007EEC3
		private SqlExpression ConvertDurationToNumber(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaySqlString, SqlLanguageStrings.ExtractSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x00080CE4 File Offset: 0x0007EEE4
		protected override SqlExpression CreateDurationTotalDays(IInvocationExpression invocation)
		{
			IBinaryExpression binaryExpression;
			if (DbAstCreator.TryAsBinaryExpression(invocation.Arguments[0], out binaryExpression))
			{
				SqlExpression sqlExpression = base.CreateScalarExpression(binaryExpression.Left);
				SqlExpression sqlExpression2 = base.CreateScalarExpression(binaryExpression.Right);
				TypeValue type = base.GetType(binaryExpression.Left);
				TypeValue type2 = base.GetType(binaryExpression.Right);
				if (type.TypeKind == ValueKind.Date)
				{
					sqlExpression = new CastCall
					{
						Type = SqlDataType.Timestamp,
						Expression = sqlExpression
					};
				}
				if (type2.TypeKind == ValueKind.Date)
				{
					sqlExpression2 = new CastCall
					{
						Type = SqlDataType.Timestamp,
						Expression = sqlExpression2
					};
				}
				return this.ConvertDurationToNumber(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression, sqlExpression2));
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06002ABB RID: 10939 RVA: 0x00080D9C File Offset: 0x0007EF9C
		protected override SqlExpression CreateToText(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			ValueKind typeKind = base.GetType(expression).TypeKind;
			if (typeKind != ValueKind.Logical)
			{
				if (typeKind != ValueKind.Text)
				{
					return new CastCall
					{
						Type = SqlDataType.VarChar,
						Expression = base.CreateScalarExpression(invocation.Arguments[0])
					};
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

		// Token: 0x06002ABC RID: 10940 RVA: 0x00046609 File Offset: 0x00044809
		protected override SqlExpression Len(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.CharLengthSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06002ABD RID: 10941 RVA: 0x00080ED0 File Offset: 0x0007F0D0
		protected override SqlExpression CreateDivideOperation(IBinaryExpression divide)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(divide.Left);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(divide.Right);
			return new BinaryScalarOperation(this.Convert(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(38, 6), sqlExpression), BinaryScalarOperator.Divide, this.Convert(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(38, 6), sqlExpression2));
		}

		// Token: 0x06002ABE RID: 10942 RVA: 0x00080F1B File Offset: 0x0007F11B
		protected override SqlExpression CreateBinaryFromText(IInvocationExpression invocation)
		{
			return new CastCall
			{
				Type = SqlDataType.Bytea,
				Expression = base.CreateScalarExpression(invocation.Arguments[0])
			};
		}

		// Token: 0x06002ABF RID: 10943 RVA: 0x00080F45 File Offset: 0x0007F145
		protected override SqlExpression CreateTextTrim(IInvocationExpression invocation)
		{
			return this.CreateTextTrim(SqlLanguageStrings.TrimSqlString, invocation);
		}

		// Token: 0x06002AC0 RID: 10944 RVA: 0x00080F53 File Offset: 0x0007F153
		protected override SqlExpression CreateTextTrimEnd(IInvocationExpression invocation)
		{
			return this.CreateTextTrim(SqlLanguageStrings.RTrimSqlString, invocation);
		}

		// Token: 0x06002AC1 RID: 10945 RVA: 0x00080F61 File Offset: 0x0007F161
		protected override SqlExpression CreateTextTrimStart(IInvocationExpression invocation)
		{
			return this.CreateTextTrim(SqlLanguageStrings.LTrimSqlString, invocation);
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x00080F70 File Offset: 0x0007F170
		private SqlExpression CreateTextTrim(ConstantSqlString trimfunctionname, IInvocationExpression invocation)
		{
			int count = invocation.Arguments.Count;
			if (count == 1)
			{
				string text = "";
				foreach (int num in SqlConstant.MLanguageWhitespaceCodePoints)
				{
					if (((PostgreSQLEnvironment)this.dbEnvironment).DatabaseCharacterSetIsUTF8 || num < 127)
					{
						text += char.ConvertFromUtf32(num);
					}
				}
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(trimfunctionname), new SqlExpression[]
				{
					base.CreateScalarExpression(invocation.Arguments[0]),
					new SqlConstant(ConstantType.UnicodeString, text)
				});
			}
			if (count != 2)
			{
				throw new InvalidOperationException();
			}
			string text2;
			if (invocation.Arguments[1].TryGetStringConstant(out text2) && text2 == " ")
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(trimfunctionname), new SqlExpression[] { base.CreateScalarExpression(invocation.Arguments[0]) });
			}
			return base.CreateFunctionCall(trimfunctionname, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06002AC3 RID: 10947 RVA: 0x0008108C File Offset: 0x0007F28C
		protected override SqlExpression Convert(SqlDataType columnType, SqlExpression expression)
		{
			if (columnType.Type.Equals(SqlLanguageStrings.NVarCharSqlString))
			{
				columnType = SqlDataType.VarChar;
			}
			if (columnType.Type.Equals(SqlLanguageStrings.NCharSqlString))
			{
				columnType = SqlDataType.Character;
			}
			return new CastCall
			{
				Expression = expression,
				Type = columnType
			};
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x000810E4 File Offset: 0x0007F2E4
		private static bool IsCurrency(TypeValue type)
		{
			return type.NonNullable.Equals(TypeValue.Currency);
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x000810F6 File Offset: 0x0007F2F6
		private static bool IsBinaryPrecision(TypeValue type)
		{
			return type.NonNullable.Equals(TypeValue.Single) || type.NonNullable.Equals(TypeValue.Double);
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0008111C File Offset: 0x0007F31C
		protected override OutputClause CreateOutputClause(Alias alias, TableTypeValue tableType)
		{
			if (!this.countOnly)
			{
				Keys keys = tableType.ItemType.Fields.Keys;
				List<SelectItem> list = new List<SelectItem>(keys.Length);
				for (int i = 0; i < keys.Length; i++)
				{
					Alias alias2 = Alias.NewNativeAlias(keys[i]);
					ColumnReference columnReference = new ColumnReference(alias, alias2);
					if (TypeServices.IsSerializedText(tableType.ItemType.Fields[i]["Type"].AsType))
					{
						list.Add(new SelectItem(this.Convert(SqlDataType.NVarChar, columnReference), alias2));
					}
					else
					{
						list.Add(new SelectItem(columnReference));
					}
				}
				return new PostgreSQLAstCreator.PostgreSQLOutputClause(list);
			}
			return OutputClause.Null;
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000811D3 File Offset: 0x0007F3D3
		protected override AggregateFunctionCall Stdev(SqlExpression expression)
		{
			return AggregateFunctionCall.StandardDeviation(expression, SqlLanguageStrings.StDdevSqlString);
		}

		// Token: 0x04001294 RID: 4756
		private static readonly TimeSpan BaseTime = TimeSpan.Zero;

		// Token: 0x04001295 RID: 4757
		private static readonly DateTime BaseDateTime = new DateTime(2000, 1, 1);

		// Token: 0x04001296 RID: 4758
		private static readonly SqlConstant TimeAggregateBase = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(PostgreSQLAstCreator.BaseTime);

		// Token: 0x04001297 RID: 4759
		private static readonly SqlConstant DateTimeAggregateBase = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(PostgreSQLAstCreator.BaseDateTime);

		// Token: 0x04001298 RID: 4760
		private static readonly BinaryLogicalOperation TrueCondition = new BinaryLogicalOperation(SqlConstant.BooleanTrue, BinaryLogicalOperator.Equals, SqlConstant.BooleanTrue);

		// Token: 0x04001299 RID: 4761
		private static readonly BinaryLogicalOperation FalseCondition = new BinaryLogicalOperation(SqlConstant.BooleanFalse, BinaryLogicalOperator.Equals, SqlConstant.BooleanTrue);

		// Token: 0x02000534 RID: 1332
		private sealed class PostgreSQLOutputClause : OutputClause
		{
			// Token: 0x06002AC9 RID: 10953 RVA: 0x00059155 File Offset: 0x00057355
			public PostgreSQLOutputClause(List<SelectItem> columnList)
				: base(columnList)
			{
			}

			// Token: 0x06002ACA RID: 10954 RVA: 0x00081250 File Offset: 0x0007F450
			public override void WriteSuffixScript(ScriptWriter writer)
			{
				writer.WriteLine();
				writer.WriteSpaceAfter(SqlLanguageStrings.ReturningSqlString);
				writer.Write(SqlLanguageSymbols.MultiplySqlString);
			}
		}

		// Token: 0x02000535 RID: 1333
		private sealed class IntervalExpression : SqlExpression
		{
			// Token: 0x06002ACB RID: 10955 RVA: 0x0008126E File Offset: 0x0007F46E
			private IntervalExpression(SqlExpression expression, SqlConstant intervalUnit)
			{
				this.expression = expression;
				this.intervalUnit = intervalUnit;
			}

			// Token: 0x06002ACC RID: 10956 RVA: 0x00081284 File Offset: 0x0007F484
			public static PostgreSQLAstCreator.IntervalExpression Day(SqlExpression expression)
			{
				return new PostgreSQLAstCreator.IntervalExpression(expression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("1 day"));
			}

			// Token: 0x06002ACD RID: 10957 RVA: 0x00081296 File Offset: 0x0007F496
			public static PostgreSQLAstCreator.IntervalExpression Month(SqlExpression expression)
			{
				return new PostgreSQLAstCreator.IntervalExpression(expression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("1 month"));
			}

			// Token: 0x06002ACE RID: 10958 RVA: 0x000812A8 File Offset: 0x0007F4A8
			public static PostgreSQLAstCreator.IntervalExpression Second(SqlExpression expression)
			{
				return new PostgreSQLAstCreator.IntervalExpression(expression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("1 second"));
			}

			// Token: 0x06002ACF RID: 10959 RVA: 0x000812BA File Offset: 0x0007F4BA
			public static PostgreSQLAstCreator.IntervalExpression Year(SqlExpression expression)
			{
				return new PostgreSQLAstCreator.IntervalExpression(expression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("1 year"));
			}

			// Token: 0x1700101C RID: 4124
			// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x06002AD1 RID: 10961 RVA: 0x000812CC File Offset: 0x0007F4CC
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				this.expression.WriteCreateScript(writer);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
				writer.WriteSpaceBeforeAndAfter(SqlLanguageSymbols.MultiplySqlString);
				writer.WriteSpaceAfter(SqlLanguageStrings.IntervalSqlString);
				this.intervalUnit.WriteCreateScript(writer);
			}

			// Token: 0x0400129A RID: 4762
			private readonly SqlConstant intervalUnit;

			// Token: 0x0400129B RID: 4763
			private readonly SqlExpression expression;
		}
	}
}
