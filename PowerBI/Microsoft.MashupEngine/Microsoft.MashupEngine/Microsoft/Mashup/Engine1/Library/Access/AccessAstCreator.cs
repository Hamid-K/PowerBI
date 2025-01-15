using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Access;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Access
{
	// Token: 0x0200122A RID: 4650
	internal sealed class AccessAstCreator : DbAstCreator
	{
		// Token: 0x06007ACE RID: 31438 RVA: 0x0004FA88 File Offset: 0x0004DC88
		private AccessAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x06007ACF RID: 31439 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected override SqlExpression CreateToDecimal(IInvocationExpression invocation)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06007AD0 RID: 31440 RVA: 0x000020FA File Offset: 0x000002FA
		protected override SqlDataType[] AdjustArgumentsForType(TypeValue[] types)
		{
			return null;
		}

		// Token: 0x06007AD1 RID: 31441 RVA: 0x001A7BDC File Offset: 0x001A5DDC
		protected override Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> GetFunctions()
		{
			Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> functions = base.GetFunctions();
			functions.Remove(Library.Number.Acos);
			functions.Remove(Library.Number.Asin);
			functions.Remove(Library.Number.Atan2);
			functions.Remove(Library.Number.RoundUp);
			functions.Remove(TableModule.Table.Pivot);
			functions.Remove(TableModule.Table.Unpivot);
			functions.Remove(Library.List.CountOfDistinct);
			functions.Remove(Library.List.CountOfDistinctNull);
			functions.Remove(Library.List.CountOfDistinctNotNull);
			functions[Library.Number.Atan] = base.CreateFunctionCall(SqlLanguageStrings.AtnSqlString, Array.Empty<SqlExpression>());
			functions[Library.Number.Sqrt] = base.CreateFunctionCall(SqlLanguageStrings.SqrSqlString, Array.Empty<SqlExpression>());
			functions[Library.Number.RoundDown] = base.CreateFunctionCall(SqlLanguageStrings.IntSqlString, Array.Empty<SqlExpression>());
			functions[CultureSpecificFunction.TextLower] = base.CreateFunctionCall(SqlLanguageStrings.LCaseSqlString, Array.Empty<SqlExpression>());
			functions[CultureSpecificFunction.TextUpper] = base.CreateFunctionCall(SqlLanguageStrings.UCaseSqlString, Array.Empty<SqlExpression>());
			functions.Add(TypeSpecificFunction.NumberRandom, base.CreateFunctionCall(SqlLanguageStrings.RndSqlString, Array.Empty<SqlExpression>()));
			functions.Add(TimeSpecificFunction.DateTimeZoneLocalNow, new Func<IInvocationExpression, SqlExpression>(this.CreateSysDateTimeOffset));
			functions.Add(TimeSpecificFunction.DateTimeZoneUtcNow, new Func<IInvocationExpression, SqlExpression>(this.CreateInstantUtcNow));
			return functions;
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x001A7D30 File Offset: 0x001A5F30
		protected override SqlExpression Convert(SqlDataType columnType, SqlExpression expression)
		{
			if (columnType.Type.Equals(SqlLanguageStrings.SqlVariantSqlString))
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(AccessLanguageStrings.CVarSqlString), new SqlExpression[] { expression });
			}
			ConstantSqlString constantSqlString;
			if (!AccessAstCreator.TypeMappings.TryGetValue(columnType.Type, out constantSqlString))
			{
				throw new ArgumentException("Unsupported column type", "columnType");
			}
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(constantSqlString), new SqlExpression[] { expression });
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(AccessLanguageStrings.IIfSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNotNull(expression),
				sqlExpression,
				SqlConstant.Null
			});
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x00080047 File Offset: 0x0007E247
		protected override SqlConstant Constant(bool value)
		{
			if (!value)
			{
				return SqlConstant.BooleanFalse;
			}
			return SqlConstant.BooleanTrue;
		}

		// Token: 0x06007AD4 RID: 31444 RVA: 0x001A7DD5 File Offset: 0x001A5FD5
		public static AccessAstCreator Create(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment externalEnvironment)
		{
			return new AccessAstCreator(expression, cursor, externalEnvironment);
		}

		// Token: 0x06007AD5 RID: 31445 RVA: 0x001A7DE0 File Offset: 0x001A5FE0
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
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.AccessHour, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Hours), sqlExpression);
			}
			if (timeSpan.Minutes != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.AccessMinute, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Minutes), sqlExpression);
			}
			if (timeSpan.Seconds != 0)
			{
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.AccessSecond, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(timeSpan.Seconds), sqlExpression);
			}
			return sqlExpression;
		}

		// Token: 0x06007AD6 RID: 31446 RVA: 0x001A7E80 File Offset: 0x001A6080
		protected override SqlExpression VisitDateTimeDurationBinaryScalarOperation(SqlExpression dateTime, SqlExpression duration, TypeValue dateTimeType)
		{
			if (dateTimeType.TypeKind == ValueKind.Date)
			{
				SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(duration, base.TicksPerDay);
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, sqlExpression, dateTime);
			}
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(duration, base.TicksPerSecond);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.AccessSecond, sqlExpression2, dateTime);
		}

		// Token: 0x06007AD7 RID: 31447 RVA: 0x001A7EC9 File Offset: 0x001A60C9
		protected override SqlExpression CreateAddOperation(IBinaryExpression add)
		{
			return this.CreateBinaryScalarOperation(BinaryScalarOperator.Add, add);
		}

		// Token: 0x06007AD8 RID: 31448 RVA: 0x001A7ED4 File Offset: 0x001A60D4
		protected override SqlExpression CreateBinaryFromText(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			return this.Convert(SqlDataType.VarBinary, sqlExpression, 2);
		}

		// Token: 0x06007AD9 RID: 31449 RVA: 0x001A7F04 File Offset: 0x001A6104
		protected override SqlExpression CreateDateTimeAddMonths(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Month, sqlExpression2, sqlExpression);
		}

		// Token: 0x06007ADA RID: 31450 RVA: 0x001A7F44 File Offset: 0x001A6144
		protected override SqlExpression CreateDateTimeAddYears(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(invocation.Arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Year, sqlExpression2, sqlExpression);
		}

		// Token: 0x06007ADB RID: 31451 RVA: 0x001A7F84 File Offset: 0x001A6184
		protected override SqlExpression CreateDivideOperation(IBinaryExpression divide)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(divide.Left);
			SqlExpression sqlExpression2 = base.CreateScalarExpression(divide.Right);
			return new BinaryScalarOperation(sqlExpression, BinaryScalarOperator.Divide, sqlExpression2);
		}

		// Token: 0x06007ADC RID: 31452 RVA: 0x001A7FB4 File Offset: 0x001A61B4
		protected override SqlExpression CreateListAverage(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue itemType = base.GetType(expression).AsListType.ItemType;
			SqlExpression sqlExpression = base.CreateListAggregate(expression, new Func<SqlExpression, AggregateFunctionCall>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg));
			if (itemType.TypeKind != ValueKind.Number)
			{
				sqlExpression = this.Convert(this.dbEnvironment.GetSqlScalarType(itemType), sqlExpression);
			}
			return sqlExpression;
		}

		// Token: 0x06007ADD RID: 31453 RVA: 0x001A8012 File Offset: 0x001A6212
		protected override SqlExpression ConvertNumberToDate(SqlExpression number)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, number, base.BaseOADateTime);
		}

		// Token: 0x06007ADE RID: 31454 RVA: 0x001A8028 File Offset: 0x001A6228
		protected override SqlExpression ConvertNumberToDateTime(SqlExpression number)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RoundSqlString), new SqlExpression[]
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(number, SqlConstant.SecondsPerDay),
				SqlConstant.Zero
			});
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(number, BinaryLogicalOperator.LessThan, SqlConstant.Zero)).Then(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(this.CreateNumberMod(sqlExpression, SqlConstant.SecondsPerDay, null), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(2))))
				.Else(sqlExpression);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.Day, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(sqlExpression2, SqlConstant.SecondsPerDay), base.BaseOADateTime);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateAdd(DatePart.AccessSecond, this.CreateNumberMod(sqlExpression2, SqlConstant.SecondsPerDay, null), sqlExpression3);
		}

		// Token: 0x06007ADF RID: 31455 RVA: 0x00056038 File Offset: 0x00054238
		protected override SqlExpression ConvertDateToNumber(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateDiff(DatePart.Day, base.BaseOADateTime, expression);
		}

		// Token: 0x06007AE0 RID: 31456 RVA: 0x001A80E8 File Offset: 0x001A62E8
		protected override SqlExpression ConvertDateTimeToNumber(SqlExpression expression)
		{
			SqlExpression sqlExpression = this.ConvertDateToNumber(expression);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString), new SqlExpression[] { expression }), SqlConstant.SecondsPerHour);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString), new SqlExpression[] { expression }), SqlConstant.SecondsPerMinute);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SecondSqlString), new SqlExpression[] { expression });
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression3, sqlExpression4)), SqlConstant.SecondsPerDay);
			SqlExpression sqlExpression6 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateOADateTimeSignExpression(expression), sqlExpression5);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, sqlExpression6);
		}

		// Token: 0x06007AE1 RID: 31457 RVA: 0x001A8190 File Offset: 0x001A6390
		protected override SqlExpression CreateNumberLogBase10(IInvocationExpression invocation)
		{
			return new BinaryScalarOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LogSqlString), new SqlExpression[] { base.CreateScalarExpression(invocation.Arguments[0]) }), BinaryScalarOperator.Divide, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LogSqlString), new SqlExpression[] { SqlConstant.Ten }));
		}

		// Token: 0x06007AE2 RID: 31458 RVA: 0x00056B3F File Offset: 0x00054D3F
		protected override SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.Atn2SqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06007AE3 RID: 31459 RVA: 0x001A81EC File Offset: 0x001A63EC
		protected override SqlExpression CreateNumberMod(SqlExpression number, SqlExpression divisor, IConstantExpression precision = null)
		{
			Func<SqlExpression, SqlExpression> func = base.CreateNumericCastFromPrecision(precision);
			return new BinaryScalarOperation(func(number), BinaryScalarOperator.InlineModulo, func(divisor));
		}

		// Token: 0x06007AE4 RID: 31460 RVA: 0x001A8215 File Offset: 0x001A6415
		protected override SqlExpression CreateNumberPower(IInvocationExpression invocation)
		{
			return new BinaryScalarOperation(base.CreateScalarExpression(invocation.Arguments[0]), BinaryScalarOperator.Power, base.CreateScalarExpression(invocation.Arguments[1]));
		}

		// Token: 0x06007AE5 RID: 31461 RVA: 0x001A8241 File Offset: 0x001A6441
		protected override SqlExpression CreateNumberSign(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.SgnSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06007AE6 RID: 31462 RVA: 0x001A825C File Offset: 0x001A645C
		protected override SqlExpression CreateToText(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = base.CreateScalarExpression(expression);
			if (type.TypeKind == ValueKind.Logical)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, SqlConstant.BooleanTrue)).Then(SqlConstant.StringTrue)
					.When(new BinaryLogicalOperation(sqlExpression, BinaryLogicalOperator.Equals, SqlConstant.BooleanFalse))
					.Then(SqlConstant.StringFalse)
					.Else(SqlConstant.Null);
			}
			return this.Convert(SqlDataType.NVarChar, sqlExpression);
		}

		// Token: 0x06007AE7 RID: 31463 RVA: 0x001A82F8 File Offset: 0x001A64F8
		protected override SqlExpression CreateTextContains(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.InStrSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[0]),
				base.CreateScalarExpression(arguments[1])
			}), BinaryLogicalOperator.GreaterThan, SqlConstant.Zero);
		}

		// Token: 0x06007AE8 RID: 31464 RVA: 0x001A834C File Offset: 0x001A654C
		protected override SqlExpression Len(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LenSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06007AE9 RID: 31465 RVA: 0x00046685 File Offset: 0x00044885
		protected override SqlExpression CreateTextTrim(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TrimSqlString), new SqlExpression[] { base.CreateScalarExpression(invocation.Arguments[0]) });
		}

		// Token: 0x06007AEA RID: 31466 RVA: 0x001A8368 File Offset: 0x001A6568
		protected override ScalarExpression Constant(Value constant, TypeValue type)
		{
			if (!constant.IsNull)
			{
				type = type.NonNullable;
				if (type.TypeKind == ValueKind.Duration)
				{
					SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsDuration.AsTimeSpan.Ticks);
					return new ConstantAnnotationExpression(constant, sqlExpression);
				}
			}
			return base.Constant(constant, type);
		}

		// Token: 0x06007AEB RID: 31467 RVA: 0x001A83B7 File Offset: 0x001A65B7
		protected override SqlExpression CreateDurationFrom(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(base.CreateScalarExpression(invocation.Arguments[0]), base.TicksPerDay);
		}

		// Token: 0x06007AEC RID: 31468 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected override SqlExpression CastToBigInt(SqlExpression expression)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06007AED RID: 31469 RVA: 0x001A83D6 File Offset: 0x001A65D6
		protected override SqlExpression CastToSingle(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(AccessLanguageStrings.CSngSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06007AEE RID: 31470 RVA: 0x001A83F1 File Offset: 0x001A65F1
		protected override SqlExpression CastToDouble(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(AccessLanguageStrings.CDblSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06007AEF RID: 31471 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected override SqlExpression CastToDecimal(SqlExpression expression)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06007AF0 RID: 31472 RVA: 0x001A840C File Offset: 0x001A660C
		protected override SqlExpression CreateDateTimeStartOfDay(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			IExpression expression = invocation.Arguments[0];
			if (base.GetType(expression).TypeKind == ValueKind.DateTime)
			{
				return new CastCall
				{
					Type = new SqlDataType(TypeValue.DateTime, SqlLanguageStrings.DateTimeSqlString),
					Expression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateValueSqlString), new SqlExpression[] { sqlExpression })
				};
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06007AF1 RID: 31473 RVA: 0x00046713 File Offset: 0x00044913
		private static SqlExpression CastToInt(SqlExpression expression)
		{
			return new CastCall
			{
				Type = SqlDataType.Int,
				Expression = expression
			};
		}

		// Token: 0x06007AF2 RID: 31474 RVA: 0x001A8494 File Offset: 0x001A6694
		protected override SqlExpression CreateDurationTotalDays(IInvocationExpression invocation)
		{
			IBinaryExpression binaryExpression;
			if (DbAstCreator.TryAsBinaryExpression(invocation.Arguments[0], out binaryExpression))
			{
				SqlExpression sqlExpression = base.CreateScalarExpression(binaryExpression.Left);
				SqlExpression sqlExpression2 = base.CreateScalarExpression(binaryExpression.Right);
				return AccessAstCreator.CastToInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(sqlExpression, sqlExpression2));
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x04004421 RID: 17441
		private static readonly Dictionary<ConstantSqlString, ConstantSqlString> TypeMappings = new Dictionary<ConstantSqlString, ConstantSqlString>
		{
			{
				SqlLanguageStrings.BitSqlString,
				AccessLanguageStrings.CBoolSqlString
			},
			{
				SqlLanguageStrings.TinyIntSqlString,
				AccessLanguageStrings.CByteSqlString
			},
			{
				SqlLanguageStrings.IntSqlString,
				AccessLanguageStrings.CIntSqlString
			},
			{
				SqlLanguageStrings.BigIntSqlString,
				AccessLanguageStrings.CLngSqlString
			},
			{
				SqlLanguageStrings.FloatSqlString,
				AccessLanguageStrings.CDblSqlString
			},
			{
				SqlLanguageStrings.RealSqlString,
				AccessLanguageStrings.CSngSqlString
			},
			{
				SqlLanguageStrings.DecimalSqlString,
				AccessLanguageStrings.CDecSqlString
			},
			{
				SqlLanguageStrings.NVarCharSqlString,
				AccessLanguageStrings.CStrSqlString
			},
			{
				SqlLanguageStrings.DateTimeSqlString,
				AccessLanguageStrings.CDateSqlString
			},
			{
				SqlLanguageStrings.DateTime2SqlString,
				AccessLanguageStrings.CDateSqlString
			}
		};
	}
}
