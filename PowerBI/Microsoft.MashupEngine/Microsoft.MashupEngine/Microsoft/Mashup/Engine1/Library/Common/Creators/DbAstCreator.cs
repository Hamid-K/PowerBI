using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x0200117A RID: 4474
	internal abstract class DbAstCreator : SqlAstCreatorBase<DbAstCreator.SqlVariable>
	{
		// Token: 0x06007570 RID: 30064 RVA: 0x00192B3F File Offset: 0x00190D3F
		protected DbAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x06007571 RID: 30065 RVA: 0x00192B4A File Offset: 0x00190D4A
		public DbQueryPlan Create(IExpression expression)
		{
			return new DbQueryPlan(base.GetType(expression), this.CreateResult(), this.CreateOptions(), this.dbEnvironment.SqlSettings);
		}

		// Token: 0x06007572 RID: 30066 RVA: 0x00192B70 File Offset: 0x00190D70
		public DbStatementPlan CreateStatementPlan()
		{
			SqlStatement sqlStatement = this.CreateStatement();
			SqlQueryOptions sqlQueryOptions = this.CreateOptions();
			SqlSettings sqlSettings = this.dbEnvironment.SqlSettings;
			bool? flag = this.returnCountOnly;
			bool flag2 = true;
			return new DbStatementPlan(sqlStatement, sqlQueryOptions, sqlSettings, (flag.GetValueOrDefault() == flag2) & (flag != null));
		}

		// Token: 0x17002097 RID: 8343
		// (get) Token: 0x06007573 RID: 30067 RVA: 0x00192BB4 File Offset: 0x00190DB4
		protected SqlConstant BaseOADateTime
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(DbAstCreator.baseOADateTime);
			}
		}

		// Token: 0x17002098 RID: 8344
		// (get) Token: 0x06007574 RID: 30068 RVA: 0x00192BC0 File Offset: 0x00190DC0
		protected SqlConstant BaseOADate
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateConstant(DbAstCreator.baseOADateTime);
			}
		}

		// Token: 0x17002099 RID: 8345
		// (get) Token: 0x06007575 RID: 30069 RVA: 0x00192BCC File Offset: 0x00190DCC
		protected SqlConstant MicrosecondsPerDay
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(86400000000L);
			}
		}

		// Token: 0x1700209A RID: 8346
		// (get) Token: 0x06007576 RID: 30070 RVA: 0x00192BDC File Offset: 0x00190DDC
		protected SqlConstant MicrosecondsPerHour
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant((long)((ulong)(-694967296)));
			}
		}

		// Token: 0x1700209B RID: 8347
		// (get) Token: 0x06007577 RID: 30071 RVA: 0x00192BE9 File Offset: 0x00190DE9
		protected SqlConstant MicrosecondsPerMinute
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(60000000L);
			}
		}

		// Token: 0x1700209C RID: 8348
		// (get) Token: 0x06007578 RID: 30072 RVA: 0x00192BF6 File Offset: 0x00190DF6
		protected SqlConstant MicrosecondsPerSecond
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(1000000L);
			}
		}

		// Token: 0x1700209D RID: 8349
		// (get) Token: 0x06007579 RID: 30073 RVA: 0x0005579D File Offset: 0x0005399D
		protected SqlExpression TicksPerDay
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(864000000000L);
			}
		}

		// Token: 0x1700209E RID: 8350
		// (get) Token: 0x0600757A RID: 30074 RVA: 0x00192C03 File Offset: 0x00190E03
		protected SqlExpression TicksPerSecond
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(10000000L);
			}
		}

		// Token: 0x1700209F RID: 8351
		// (get) Token: 0x0600757B RID: 30075 RVA: 0x00192C10 File Offset: 0x00190E10
		protected SqlExpression TicksPerMs
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(10000L);
			}
		}

		// Token: 0x170020A0 RID: 8352
		// (get) Token: 0x0600757C RID: 30076 RVA: 0x00192C1D File Offset: 0x00190E1D
		protected SqlExpression TicksPerUs
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(10L);
			}
		}

		// Token: 0x170020A1 RID: 8353
		// (get) Token: 0x0600757D RID: 30077 RVA: 0x00192C27 File Offset: 0x00190E27
		protected SqlExpression NsPerTick
		{
			get
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(100L);
			}
		}

		// Token: 0x0600757E RID: 30078 RVA: 0x00192C31 File Offset: 0x00190E31
		protected SqlExpression GetBaseDateTime(SqlDataType format)
		{
			if (format == SqlDataType.DateTimeOffset)
			{
				return this.Convert(SqlDataType.DateTimeOffset, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(new DateTime(0L)));
			}
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(new DateTime(0L));
		}

		// Token: 0x170020A2 RID: 8354
		// (get) Token: 0x0600757F RID: 30079 RVA: 0x00192C5F File Offset: 0x00190E5F
		protected Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> FunctionLookup
		{
			get
			{
				if (this.functionLookup == null)
				{
					this.functionLookup = this.GetFunctions();
				}
				return this.functionLookup;
			}
		}

		// Token: 0x06007580 RID: 30080 RVA: 0x00192C7C File Offset: 0x00190E7C
		protected virtual Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> GetFunctions()
		{
			return new Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>>
			{
				{
					TableModule.Table.Join,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTableJoin)
				},
				{
					Library.Binary.FromText,
					new Func<IInvocationExpression, SqlExpression>(this.CreateBinaryFromText)
				},
				{
					Library.Date.AddMonths,
					new Func<IInvocationExpression, SqlExpression>(this.CreateDateTimeAddMonths)
				},
				{
					Library.Date.AddYears,
					new Func<IInvocationExpression, SqlExpression>(this.CreateDateTimeAddYears)
				},
				{
					Library.Date.StartOfDay,
					new Func<IInvocationExpression, SqlExpression>(this.CreateDateTimeStartOfDay)
				},
				{
					Library.Duration.TotalDays,
					new Func<IInvocationExpression, SqlExpression>(this.CreateDurationTotalDays)
				},
				{
					Library.List.Average,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListAverage)
				},
				{
					Library.List.Combine,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListCombine)
				},
				{
					Library.List.Contains,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListContains)
				},
				{
					Library.List.First,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListFirst)
				},
				{
					Library.List.Max,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListMax)
				},
				{
					Library.List.Min,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListMin)
				},
				{
					Library.List.StandardDeviation,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListStandardDeviation)
				},
				{
					Library.List.Sum,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListSum)
				},
				{
					LanguageLibrary.List.Count,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListCount)
				},
				{
					Library.List.CountOfNull,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListCountOfNull)
				},
				{
					Library.List.CountOfNotNull,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListCountOfNotNull)
				},
				{
					Library.List.CountOfDistinct,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListCountOfDistinct)
				},
				{
					Library.List.CountOfDistinctNull,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListCountOfDistinctNull)
				},
				{
					Library.List.CountOfDistinctNotNull,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListCountOfDistinctNotNull)
				},
				{
					TableModule.Table.Group,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListAggregateGroupBy)
				},
				{
					TableModule.Table.RowCount,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListCount)
				},
				{
					TableModule.Table.Distinct,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListDistinct)
				},
				{
					TableModule.Table.SelectRows,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListSelect)
				},
				{
					Library._Value.NativeQuery,
					new Func<IInvocationExpression, SqlExpression>(this.CreateNativeQuery)
				},
				{
					TableModule.Table.Sort,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListSort)
				},
				{
					Library.ListRuntime.Transform,
					new Func<IInvocationExpression, SqlExpression>(this.CreateListTransform)
				},
				{
					TableModule.Table.First,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTableFirst)
				},
				{
					Library.Number.Abs,
					this.CreateFunctionCall(SqlLanguageStrings.AbsSqlString, Array.Empty<SqlExpression>())
				},
				{
					Library.Number.Cos,
					this.CreateFunctionCall(SqlLanguageStrings.CosSqlString, Array.Empty<SqlExpression>())
				},
				{
					Library.Number.Exp,
					this.CreateFunctionCall(SqlLanguageStrings.ExpSqlString, Array.Empty<SqlExpression>())
				},
				{
					Library.Number.Log,
					new Func<IInvocationExpression, SqlExpression>(this.CreateNumberNaturalLogarithm)
				},
				{
					Library.Number.Log10,
					new Func<IInvocationExpression, SqlExpression>(this.CreateNumberLogBase10)
				},
				{
					Library.Number.Power,
					new Func<IInvocationExpression, SqlExpression>(this.CreateNumberPower)
				},
				{
					Library.Number.Round,
					new Func<IInvocationExpression, SqlExpression>(this.CreateNumberRound)
				},
				{
					Library.Number.Sign,
					new Func<IInvocationExpression, SqlExpression>(this.CreateNumberSign)
				},
				{
					Library.Number.Sin,
					this.CreateFunctionCall(SqlLanguageStrings.SinSqlString, Array.Empty<SqlExpression>())
				},
				{
					Library.Number.Tan,
					this.CreateFunctionCall(SqlLanguageStrings.TanSqlString, Array.Empty<SqlExpression>())
				},
				{
					Library.Text.Contains,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTextContains)
				},
				{
					Library.Text.EndsWith,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTextEndsWith)
				},
				{
					Library.Text.Length,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTextLength)
				},
				{
					Library.Text.StartsWith,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTextStartsWith)
				},
				{
					Library.Text.Trim,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTextTrim)
				},
				{
					Library.Text.TrimEnd,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTextTrimEnd)
				},
				{
					Library.Text.TrimStart,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTextTrimStart)
				},
				{
					Library._Value.As,
					new Func<IInvocationExpression, SqlExpression>(this.CreateValueAs)
				},
				{
					Library._Value.Equals,
					new Func<IInvocationExpression, SqlExpression>(this.CreateValueEquals)
				},
				{
					Library._Value.NullableEquals,
					new Func<IInvocationExpression, SqlExpression>(this.CreateValueNullableEquals)
				},
				{
					Library._Value.Add,
					this.CreateOperatorWithOptionalPrecision(BinaryScalarOperator.Add)
				},
				{
					Library._Value.Subtract,
					this.CreateOperatorWithOptionalPrecision(BinaryScalarOperator.Subtract)
				},
				{
					Library._Value.Multiply,
					this.CreateOperatorWithOptionalPrecision(BinaryScalarOperator.Multiply)
				},
				{
					Library._Value.Divide,
					this.CreateOperatorWithOptionalPrecision(BinaryScalarOperator.Divide)
				},
				{
					CultureSpecificFunction.NumberFrom,
					new Func<IInvocationExpression, SqlExpression>(this.CreateNumberFrom)
				},
				{
					CultureSpecificFunction.NumberToText,
					new Func<IInvocationExpression, SqlExpression>(this.CreateToText)
				},
				{
					CultureSpecificFunction.SingleFrom,
					new Func<IInvocationExpression, SqlExpression>(this.CreateToSingle)
				},
				{
					CultureSpecificFunction.DoubleFrom,
					new Func<IInvocationExpression, SqlExpression>(this.CreateToDouble)
				},
				{
					CultureSpecificFunction.DecimalFrom,
					new Func<IInvocationExpression, SqlExpression>(this.CreateToDecimal)
				},
				{
					CultureSpecificFunction.TextFrom,
					new Func<IInvocationExpression, SqlExpression>(this.CreateToText)
				},
				{
					CultureSpecificFunction.DateFrom,
					new Func<IInvocationExpression, SqlExpression>(this.CreateToDate)
				},
				{
					CultureSpecificFunction.DateTimeFrom,
					new Func<IInvocationExpression, SqlExpression>(this.CreateToDateTime)
				},
				{
					CultureSpecificFunction.DateTimeZoneFrom,
					new Func<IInvocationExpression, SqlExpression>(this.CreateToDateTimeWithTimeZone)
				},
				{
					CultureSpecificFunction.TextLower,
					this.CreateFunctionCall(SqlLanguageStrings.LowerSqlString, Array.Empty<SqlExpression>())
				},
				{
					CultureSpecificFunction.TextUpper,
					this.CreateFunctionCall(SqlLanguageStrings.UpperSqlString, Array.Empty<SqlExpression>())
				},
				{
					Library.Duration.From,
					new Func<IInvocationExpression, SqlExpression>(this.CreateDurationFrom)
				},
				{
					Library.Number.Atan,
					this.CreateFunctionCall(SqlLanguageStrings.AtanSqlString, Array.Empty<SqlExpression>())
				},
				{
					Library.Number.Sqrt,
					this.CreateFunctionCall(SqlLanguageStrings.SqrtSqlString, Array.Empty<SqlExpression>())
				},
				{
					TableModule.Table.Pivot,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTablePivot)
				},
				{
					TableModule.Table.Unpivot,
					new Func<IInvocationExpression, SqlExpression>(this.CreateTableUnpivot)
				},
				{
					Library.Number.Acos,
					this.CreateFunctionCall(SqlLanguageStrings.AcosSqlString, Array.Empty<SqlExpression>())
				},
				{
					Library.Number.Asin,
					this.CreateFunctionCall(SqlLanguageStrings.AsinSqlString, Array.Empty<SqlExpression>())
				},
				{
					Library.Number.Atan2,
					new Func<IInvocationExpression, SqlExpression>(this.CreateNumberArcTangent2)
				},
				{
					Library.Number.Mod,
					new Func<IInvocationExpression, SqlExpression>(this.CreateNumberModCall)
				},
				{
					Library.Number.RoundUp,
					new Func<IInvocationExpression, SqlExpression>(this.CreateRoundUp)
				},
				{
					Library.Number.RoundDown,
					this.CreateFunctionCall(SqlLanguageStrings.FloorSqlString, Array.Empty<SqlExpression>())
				}
			};
		}

		// Token: 0x170020A3 RID: 8355
		// (get) Token: 0x06007581 RID: 30081 RVA: 0x0019336E File Offset: 0x0019156E
		protected Dictionary<FunctionValue, Func<IInvocationExpression, SqlStatement>> StatementFunctionLookup
		{
			get
			{
				if (this.statementFunctionLookup == null)
				{
					this.statementFunctionLookup = this.GetStatementFunctions();
				}
				return this.statementFunctionLookup;
			}
		}

		// Token: 0x06007582 RID: 30082 RVA: 0x0019338A File Offset: 0x0019158A
		protected virtual Dictionary<FunctionValue, Func<IInvocationExpression, SqlStatement>> GetStatementFunctions()
		{
			return new Dictionary<FunctionValue, Func<IInvocationExpression, SqlStatement>>();
		}

		// Token: 0x06007583 RID: 30083 RVA: 0x00193394 File Offset: 0x00191594
		private bool AreTrueFalseCondition(params SqlExpression[] expressions)
		{
			for (int i = 0; i < expressions.Length; i++)
			{
				if (expressions[i] != this.Condition(true) || expressions[i] != this.Condition(false))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06007584 RID: 30084 RVA: 0x001933CA File Offset: 0x001915CA
		protected virtual BinaryLogicalOperation Condition(bool value)
		{
			if (!value)
			{
				return DbAstCreator.FalseCondition;
			}
			return DbAstCreator.TrueCondition;
		}

		// Token: 0x06007585 RID: 30085 RVA: 0x001933DC File Offset: 0x001915DC
		protected virtual ScalarExpression Constant(Value constant, TypeValue type)
		{
			if (constant.IsNull)
			{
				return SqlConstant.Null;
			}
			type = type.NonNullable;
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsTime.AsClrTimeSpan);
			case ValueKind.Date:
				if (this.dbEnvironment.SqlSettings.DatePrefix == null)
				{
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsDate.AsClrDateTime);
				}
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateConstant(constant.AsDate.AsClrDateTime);
			case ValueKind.DateTime:
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsDateTime.AsClrDateTime);
			case ValueKind.DateTimeZone:
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsDateTimeZone.AsClrDateTimeOffset);
			case ValueKind.Duration:
			{
				if (this.dbEnvironment.SqlSettings.SupportsIntervalConstants)
				{
					return this.IntervalConstant(constant.AsDuration.AsClrTimeSpan);
				}
				SqlExpression sqlExpression = this.CastToBigInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsDuration.AsTimeSpan.Ticks));
				return new ConstantAnnotationExpression(constant, sqlExpression);
			}
			case ValueKind.Number:
			{
				if (type.Equals(TypeValue.Decimal) || type.Equals(TypeValue.Currency) || type.Equals(TypeValue.Percentage))
				{
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsNumber.AsDecimal);
				}
				long num;
				if (constant.AsNumber.TryGetInt64(out num))
				{
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(num);
				}
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsNumber.ToDouble(), this.dbEnvironment.SqlSettings.IsMaxPrecision);
			}
			case ValueKind.Logical:
				if (!constant.IsNumber)
				{
					return this.Constant(constant.AsBoolean);
				}
				if (constant.AsInteger32 != 0)
				{
					return this.Constant(true);
				}
				return this.Constant(false);
			case ValueKind.Text:
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsText.String);
			case ValueKind.Binary:
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(constant.AsBinary.AsBytes);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x06007586 RID: 30086 RVA: 0x001935B8 File Offset: 0x001917B8
		protected virtual Condition ConvertToCondition(SqlExpression expression)
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
			return new BinaryLogicalOperation(this.ConvertToScalar(expression), BinaryLogicalOperator.Equals, this.Constant(true));
		}

		// Token: 0x06007587 RID: 30087 RVA: 0x0019360C File Offset: 0x0019180C
		private SqlQueryExpression ConvertToQuery(SqlExpression expression)
		{
			SqlQueryExpression sqlQueryExpression = expression as SqlQueryExpression;
			if (sqlQueryExpression != null)
			{
				return sqlQueryExpression;
			}
			return base.Select(this.ConvertToScalar(expression), Alias.ScalarColumn).ToPagingQuerySpecification();
		}

		// Token: 0x06007588 RID: 30088 RVA: 0x00193640 File Offset: 0x00191840
		protected SqlExpression ConvertToScalar(SqlExpression expression)
		{
			Condition condition = expression as Condition;
			if (condition != null)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(condition).Then(this.Constant(true))
					.When(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Not(condition))
					.Then(this.Constant(false))
					.Else(SqlConstant.Null);
			}
			SqlQueryExpression sqlQueryExpression = expression as SqlQueryExpression;
			if (sqlQueryExpression != null)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.SimplifySimpleScalarExpression(sqlQueryExpression);
			}
			return expression;
		}

		// Token: 0x06007589 RID: 30089 RVA: 0x001936B8 File Offset: 0x001918B8
		protected Func<IInvocationExpression, SqlExpression> CreateFunctionCall(ConstantSqlString function, params SqlExpression[] additionalArguments)
		{
			Func<IExpression, SqlExpression> <>9__1;
			return delegate(IInvocationExpression invocation)
			{
				IEnumerable<IExpression> arguments = invocation.Arguments;
				Func<IExpression, SqlExpression> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (IExpression arg) => this.CreateScalarExpression(arg));
				}
				SqlExpression[] array = arguments.Select(func).Concat(additionalArguments).ToArray<SqlExpression>();
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(function), array);
			};
		}

		// Token: 0x0600758A RID: 30090 RVA: 0x001936DF File Offset: 0x001918DF
		protected virtual SqlExpression CreateRoundUp(IInvocationExpression invocation)
		{
			return this.CreateFunctionCall(SqlLanguageStrings.CeilingSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x0600758B RID: 30091 RVA: 0x001936F7 File Offset: 0x001918F7
		protected virtual SqlExpression CreateSysDateTimeOffset(IInvocationExpression invocation)
		{
			return this.CreateFunctionCall(SqlLanguageStrings.SysDateTimeOffsetSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x0600758C RID: 30092
		protected abstract SqlExpression CreateAddOperation(IBinaryExpression add);

		// Token: 0x0600758D RID: 30093 RVA: 0x0019370F File Offset: 0x0019190F
		protected virtual SqlExpression CreateSubtractOperation(IBinaryExpression subtract)
		{
			return this.CreateBinaryScalarOperation(BinaryScalarOperator.Subtract, subtract);
		}

		// Token: 0x0600758E RID: 30094
		protected abstract SqlExpression CreateBinaryFromText(IInvocationExpression invocation);

		// Token: 0x0600758F RID: 30095 RVA: 0x0019371C File Offset: 0x0019191C
		protected virtual SqlExpression CreateBinaryScalarOperation(BinaryScalarOperator op, IBinaryExpression invocation)
		{
			SqlExpression sqlExpression;
			if (this.TryVisitDateTimeDurationBinaryArithmetic(invocation.Left, invocation.Right, op, out sqlExpression))
			{
				return sqlExpression;
			}
			return new BinaryScalarOperation(this.CreateScalarExpression(invocation.Left), op, this.CreateScalarExpression(invocation.Right));
		}

		// Token: 0x06007590 RID: 30096 RVA: 0x00193760 File Offset: 0x00191960
		protected SqlExpression CreateCoalesceOperation(IBinaryExpression coalesce)
		{
			TypeValue type = base.GetType(coalesce.Left);
			if (!type.IsNullable)
			{
				return this.CreateScalarExpression(coalesce.Left);
			}
			if (type.IsNull)
			{
				return this.CreateScalarExpression(coalesce.Right);
			}
			return this.CreateCoalesceOperation(coalesce.Left, coalesce.Right);
		}

		// Token: 0x06007591 RID: 30097 RVA: 0x001937B8 File Offset: 0x001919B8
		protected virtual SqlExpression CreateCoalesceOperation(IExpression left, IExpression right)
		{
			SqlExpression sqlExpression = this.CreateScalarExpression(left);
			Condition condition = new UnaryLogicalOperation(UnaryLogicalOperator.IsNull, sqlExpression);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(condition).Then(this.CreateScalarExpression(right))
				.Else(sqlExpression);
		}

		// Token: 0x06007592 RID: 30098 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected virtual SqlExpression VisitDateTimeTimeSpanBinaryScalarOperation(SqlExpression dateTime, TimeSpan timeSpan, TypeValue dateTimeType)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06007593 RID: 30099 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected virtual SqlExpression VisitDateTimeDurationBinaryScalarOperation(SqlExpression dateTime, SqlExpression duration, TypeValue dateTimeType)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x06007594 RID: 30100 RVA: 0x00193804 File Offset: 0x00191A04
		protected bool TryVisitDateTimeDurationBinaryArithmetic(IExpression left, IExpression right, BinaryScalarOperator op, out SqlExpression expression)
		{
			if (this.dbEnvironment.SqlSettings.SupportsIntervalConstants)
			{
				expression = null;
				return false;
			}
			TypeValue type = base.GetType(left);
			TypeValue type2 = base.GetType(right);
			if ((op == BinaryScalarOperator.Add || op == BinaryScalarOperator.Subtract) && DbAstExpressionChecker.IsDateTimeCompatibleType(type) && type2.TypeKind == ValueKind.Duration)
			{
				SqlExpression sqlExpression = this.CreateScalarExpression(left);
				SqlExpression sqlExpression2 = this.CreateScalarExpression(right);
				ConstantAnnotationExpression constantAnnotationExpression = sqlExpression2 as ConstantAnnotationExpression;
				DurationValue durationValue = ((constantAnnotationExpression != null) ? constantAnnotationExpression.Value : null) as DurationValue;
				if (durationValue != null)
				{
					TimeSpan timeSpan = durationValue.AsClrTimeSpan;
					if (op == BinaryScalarOperator.Subtract)
					{
						timeSpan = timeSpan.Negate();
					}
					expression = this.VisitDateTimeTimeSpanBinaryScalarOperation(sqlExpression, timeSpan, type);
				}
				else
				{
					if (op == BinaryScalarOperator.Subtract)
					{
						sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlConstant.MinusOne, sqlExpression2);
					}
					expression = this.VisitDateTimeDurationBinaryScalarOperation(sqlExpression, sqlExpression2, type);
				}
				return true;
			}
			if (op == BinaryScalarOperator.Add && type.TypeKind == ValueKind.Duration && DbAstExpressionChecker.IsDateTimeCompatibleType(type2))
			{
				SqlExpression sqlExpression3 = this.CreateScalarExpression(right);
				SqlExpression sqlExpression4 = this.CreateScalarExpression(left);
				ConstantAnnotationExpression constantAnnotationExpression2 = sqlExpression4 as ConstantAnnotationExpression;
				DurationValue durationValue2 = ((constantAnnotationExpression2 != null) ? constantAnnotationExpression2.Value : null) as DurationValue;
				if (durationValue2 != null)
				{
					TimeSpan asClrTimeSpan = durationValue2.AsClrTimeSpan;
					expression = this.VisitDateTimeTimeSpanBinaryScalarOperation(sqlExpression3, asClrTimeSpan, type2);
				}
				else
				{
					expression = this.VisitDateTimeDurationBinaryScalarOperation(sqlExpression3, sqlExpression4, type2);
				}
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x06007595 RID: 30101
		protected abstract SqlExpression CreateDateTimeStartOfDay(IInvocationExpression invocation);

		// Token: 0x06007596 RID: 30102
		protected abstract SqlExpression CreateDurationTotalDays(IInvocationExpression invocation);

		// Token: 0x06007597 RID: 30103 RVA: 0x00193938 File Offset: 0x00191B38
		private SqlExpression CreateConcatenateOperation(IBinaryExpression add)
		{
			IExpression left = add.Left;
			IExpression right = add.Right;
			if (base.GetType(left).TypeKind == ValueKind.List)
			{
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.UnionAll(this.CreateQueryExpression(left), this.CreateQueryExpression(right));
			}
			return this.CreateAddOperation(add);
		}

		// Token: 0x06007598 RID: 30104 RVA: 0x00193980 File Offset: 0x00191B80
		private SqlAstCreatorBase<DbAstCreator.SqlVariable>.XSelectExpression SelectConcatenateRecordTypes(RecordTypeValue leftRecordType, Alias leftAlias, RecordTypeValue rightRecordType, Alias rightAlias)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			List<SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference> list = new List<SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference>();
			foreach (string text in leftRecordType.Fields.Keys)
			{
				dictionary.Add(text, list.Count);
				list.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(leftAlias, text, this.dbEnvironment.SqlSettings));
			}
			foreach (string text2 in rightRecordType.Fields.Keys)
			{
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference xcolumnReference = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(rightAlias, text2, this.dbEnvironment.SqlSettings);
				int num;
				if (dictionary.TryGetValue(text2, out num))
				{
					list[num] = xcolumnReference;
				}
				else
				{
					list.Add(xcolumnReference);
				}
			}
			return base.Select(list);
		}

		// Token: 0x06007599 RID: 30105 RVA: 0x00193A80 File Offset: 0x00191C80
		private SqlAstCreatorBase<DbAstCreator.SqlVariable>.XSelectExpression SelectConcatenateRecordTypesAntiSemi(bool left, RecordTypeValue outerRecordType, Alias outerAlias, RecordTypeValue innerRecordType)
		{
			SelectItem[] array = new SelectItem[outerRecordType.FieldKeys.Length + innerRecordType.FieldKeys.Length];
			int num = (left ? 0 : innerRecordType.FieldKeys.Length);
			for (int i = 0; i < outerRecordType.FieldKeys.Length; i++)
			{
				array[num + i] = new SelectItem(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(outerAlias, outerRecordType.FieldKeys[i], this.dbEnvironment.SqlSettings));
			}
			int num2 = (left ? outerRecordType.FieldKeys.Length : 0);
			for (int j = 0; j < innerRecordType.FieldKeys.Length; j++)
			{
				array[num2 + j] = new SelectItem(this.TypedNull(innerRecordType.Fields[j]["Type"].AsType), Alias.NewNativeAlias(innerRecordType.FieldKeys[j]));
			}
			return base.Select(array);
		}

		// Token: 0x0600759A RID: 30106 RVA: 0x00193B75 File Offset: 0x00191D75
		protected Condition CreateConditionExpression(IExpression expression)
		{
			return this.ConvertToCondition(this.GetValue(expression));
		}

		// Token: 0x0600759B RID: 30107 RVA: 0x00193B84 File Offset: 0x00191D84
		public virtual SqlQueryExpression CreateCountQuery(SqlQueryExpression source)
		{
			return base.Select(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Count(null), Alias.ScalarColumn).From(source, Alias.VirtualTable).ToPagingQuerySpecification();
		}

		// Token: 0x0600759C RID: 30108
		protected abstract SqlExpression CreateDateTimeAddMonths(IInvocationExpression invocation);

		// Token: 0x0600759D RID: 30109
		protected abstract SqlExpression CreateDateTimeAddYears(IInvocationExpression invocation);

		// Token: 0x0600759E RID: 30110
		protected abstract SqlExpression CreateDivideOperation(IBinaryExpression divide);

		// Token: 0x0600759F RID: 30111 RVA: 0x00193BB8 File Offset: 0x00191DB8
		protected SqlExpression CreateEqualScalar(IBinaryExpression equal, bool nullable, Func<SqlExpression, SqlExpression> precisionFunction = null)
		{
			IExpression left = equal.Left;
			IExpression right = equal.Right;
			TypeValue type = base.GetType(left);
			TypeValue type2 = base.GetType(right);
			SqlExpression sqlExpression = this.CreateScalarExpression(left);
			SqlExpression sqlExpression2 = this.CreateScalarExpression(right);
			this.AdjustArgumentsForType(equal.Operator, ref type, ref type2, ref sqlExpression, ref sqlExpression2);
			if (sqlExpression == sqlExpression2)
			{
				return this.Constant(true);
			}
			SqlConstant sqlConstant = sqlExpression as SqlConstant;
			SqlConstant sqlConstant2 = sqlExpression2 as SqlConstant;
			if (type.TypeKind == ValueKind.Null)
			{
				if (sqlConstant2 != null && sqlConstant2 != SqlConstant.Null)
				{
					return SqlConstant.NumericFalse;
				}
				if (nullable)
				{
					return SqlConstant.NumericFalse;
				}
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(sqlExpression2);
			}
			else if (type2.TypeKind == ValueKind.Null)
			{
				if (sqlConstant != null && sqlConstant != SqlConstant.Null)
				{
					return SqlConstant.NumericFalse;
				}
				if (nullable)
				{
					return SqlConstant.NumericFalse;
				}
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(sqlExpression);
			}
			else
			{
				bool flag = false;
				if (precisionFunction != null)
				{
					sqlExpression = precisionFunction(sqlExpression);
					sqlExpression2 = precisionFunction(sqlExpression2);
				}
				else
				{
					flag = !DbTypeServices.IsCompatibleType(type, type2, equal.Operator);
				}
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XCondition xcondition = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Equals(sqlExpression, sqlExpression2);
				if (nullable)
				{
					return xcondition;
				}
				if (type.IsNullable && type2.IsNullable && (sqlConstant == null || sqlConstant2 == null))
				{
					Condition condition = SqlAstCreatorBase<DbAstCreator.SqlVariable>.And(new Condition[]
					{
						SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(sqlExpression),
						SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(sqlExpression2)
					});
					if (flag)
					{
						return condition;
					}
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Or(new Condition[]
					{
						SqlAstCreatorBase<DbAstCreator.SqlVariable>.And(new Condition[]
						{
							xcondition,
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNotNull(sqlExpression),
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNotNull(sqlExpression2)
						}),
						condition
					});
				}
				else
				{
					if (flag)
					{
						return this.Condition(false);
					}
					if (sqlExpression == sqlExpression2)
					{
						return this.Condition(true);
					}
					if (this.AreTrueFalseCondition(new SqlExpression[] { sqlExpression, sqlExpression2 }))
					{
						return this.Condition(false);
					}
					if (type.IsNullable && sqlConstant == null)
					{
						return SqlAstCreatorBase<DbAstCreator.SqlVariable>.And(new Condition[]
						{
							xcondition,
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNotNull(sqlExpression)
						});
					}
					if (type2.IsNullable && sqlConstant2 == null)
					{
						return SqlAstCreatorBase<DbAstCreator.SqlVariable>.And(new Condition[]
						{
							xcondition,
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNotNull(sqlExpression2)
						});
					}
					return xcondition;
				}
			}
		}

		// Token: 0x060075A0 RID: 30112
		protected abstract SqlDataType[] AdjustArgumentsForType(TypeValue[] types);

		// Token: 0x060075A1 RID: 30113 RVA: 0x00193E0C File Offset: 0x0019200C
		protected virtual void AdjustArgumentsForType(BinaryOperator2 binaryOperator, ref TypeValue leftType, ref TypeValue rightType, ref SqlExpression leftExpression, ref SqlExpression rightExpression)
		{
			TypeValue[] array = new TypeValue[] { leftType, rightType };
			SqlDataType[] array2 = this.AdjustArgumentsForType(array);
			if (array2 != null)
			{
				if (array2[0] != null)
				{
					leftType = array[0];
					leftExpression = this.Convert(array2[0], leftExpression);
				}
				if (array2[1] != null)
				{
					rightType = array[1];
					rightExpression = this.Convert(array2[1], rightExpression);
				}
			}
		}

		// Token: 0x060075A2 RID: 30114 RVA: 0x00193E67 File Offset: 0x00192067
		protected virtual SqlExpression CastToBigInt(SqlExpression expression)
		{
			return new CastCall
			{
				Type = SqlDataType.BigInt,
				Expression = expression
			};
		}

		// Token: 0x060075A3 RID: 30115 RVA: 0x00193E80 File Offset: 0x00192080
		protected virtual SqlExpression CastToSingle(SqlExpression expression)
		{
			return new CastCall
			{
				Type = SqlDataType.Real,
				Expression = expression
			};
		}

		// Token: 0x060075A4 RID: 30116 RVA: 0x00193E99 File Offset: 0x00192099
		protected virtual SqlExpression CastToDouble(SqlExpression expression)
		{
			return new CastCall
			{
				Type = new SqlDataType(TypeValue.Double, SqlLanguageStrings.DoublePrecisionSqlString),
				Expression = expression
			};
		}

		// Token: 0x060075A5 RID: 30117 RVA: 0x00193EBC File Offset: 0x001920BC
		protected virtual SqlExpression CastToDecimal(SqlExpression expression)
		{
			return new CastCall
			{
				Type = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Decimal(38, 6),
				Expression = expression
			};
		}

		// Token: 0x060075A6 RID: 30118 RVA: 0x00193ED8 File Offset: 0x001920D8
		protected SqlExpression TypedNull(TypeValue type)
		{
			return new CastCall
			{
				Type = this.dbEnvironment.GetSqlScalarType(type),
				Expression = SqlConstant.Null
			};
		}

		// Token: 0x060075A7 RID: 30119 RVA: 0x0000336E File Offset: 0x0000156E
		protected virtual void AdjustFieldAccessForType(TypeValue type, ref SqlExpression expression)
		{
		}

		// Token: 0x060075A8 RID: 30120 RVA: 0x00193EFC File Offset: 0x001920FC
		private SqlExpression CreateInArrayExpression(ListValue list)
		{
			InArrayExpression inArrayExpression = new InArrayExpression();
			for (int i = 0; i < list.Count; i++)
			{
				inArrayExpression.Add(this.Constant(list[i], list[i].Type));
			}
			return inArrayExpression;
		}

		// Token: 0x060075A9 RID: 30121 RVA: 0x00193F40 File Offset: 0x00192140
		protected virtual SqlExpression CreateInstantUtcNow(IInvocationExpression invocation)
		{
			return this.Convert(SqlDataType.DateTimeOffset, new BuiltInFunctionReference(SqlLanguageStrings.SysUtcDateTimeSqlString));
		}

		// Token: 0x060075AA RID: 30122 RVA: 0x00193F58 File Offset: 0x00192158
		protected SqlExpression CreateListAggregateInput(IExpression list)
		{
			ColumnReference columnReference;
			if (this.TryGetGroupByColumnAccess(list, out columnReference))
			{
				return columnReference;
			}
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(list);
			return base.Select(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.ScalarColumn), Alias.ScalarColumn).From(sqlQueryExpression, Alias.VirtualTable).ToPagingQuerySpecification();
		}

		// Token: 0x060075AB RID: 30123 RVA: 0x00193FAC File Offset: 0x001921AC
		protected SqlExpression CreateListAggregate(IExpression list, Func<SqlExpression, AggregateFunctionCall> func)
		{
			return this.CreateListAggregateRaw(list, (SqlExpression expr) => func(expr));
		}

		// Token: 0x060075AC RID: 30124 RVA: 0x00193FDC File Offset: 0x001921DC
		protected SqlExpression CreateListAggregateRaw(IExpression list, Func<SqlExpression, SqlExpression> func)
		{
			ColumnReference columnReference;
			if (this.TryGetGroupByColumnAccess(list, out columnReference))
			{
				return func(columnReference);
			}
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(list);
			return base.Select(func(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.ScalarColumn)), Alias.ScalarColumn).From(sqlQueryExpression, Alias.VirtualTable).ToPagingQuerySpecification();
		}

		// Token: 0x060075AD RID: 30125 RVA: 0x0019403C File Offset: 0x0019223C
		protected SqlExpression CreateListAggregateGroupBy(IInvocationExpression invocation)
		{
			TypeValue[] parameterResults = base.Cursor.GetParameterResults(invocation);
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(invocation.Arguments[0]);
			Alias alias = Alias.NewAssignedAlias(parameterResults[2].AsFunctionType.ParameterName(0), this.dbEnvironment.SqlSettings);
			IExpression expression = EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(((IFunctionExpression)EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(invocation.Arguments[1])).Expression);
			Identifier[] array;
			if (expression.Kind == ExpressionKind.MultiFieldRecordProjection)
			{
				array = ((IMultiFieldRecordProjectionExpression)expression).MemberNames.ToArray<Identifier>();
			}
			else
			{
				IFieldAccessExpression fieldAccessExpression = (IFieldAccessExpression)expression;
				array = new Identifier[] { fieldAccessExpression.MemberName };
			}
			PagingQuerySpecification pagingQuerySpecification = sqlQueryExpression as PagingQuerySpecification;
			if (pagingQuerySpecification == null)
			{
				pagingQuerySpecification = new PagingQuerySpecification();
				pagingQuerySpecification.FromItems.Add(new FromQuery
				{
					Query = sqlQueryExpression
				});
			}
			DbAstCreator.GroupVariables groupVariables = this.groupVariables;
			this.groupVariables = new DbAstCreator.GroupVariables(pagingQuerySpecification.SelectItems);
			IFunctionExpression functionExpression = (IFunctionExpression)EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(invocation.Arguments[2]);
			this.PushFunction(functionExpression, parameterResults[2].AsFunctionType, new DbAstCreator.SqlVariable[] { DbAstCreator.SqlVariable.CreateGroup(alias) });
			PagingQuerySpecification pagingQuerySpecification2 = (PagingQuerySpecification)this.CreateQueryExpression(functionExpression.Expression);
			this.PopFunction(functionExpression);
			if (this.groupVariables.HasVariables)
			{
				SelectItem[] array2 = new SelectItem[pagingQuerySpecification.SelectItems.Count + this.groupVariables.Variables.Count];
				for (int i = 0; i < pagingQuerySpecification.SelectItems.Count; i++)
				{
					Alias alias2 = pagingQuerySpecification.SelectItems[i].Alias ?? pagingQuerySpecification.SelectItems[i].Name;
					array2[i] = new SelectItem(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(alias, alias2), pagingQuerySpecification.SelectItems[i].Alias);
				}
				for (int j = 0; j < this.groupVariables.Variables.Count; j++)
				{
					array2[pagingQuerySpecification.SelectItems.Count + j] = this.groupVariables.Variables[j];
				}
				sqlQueryExpression = base.Select(array2).From(sqlQueryExpression, alias).ToPagingQuerySpecification();
			}
			this.groupVariables = groupVariables;
			SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromExpression xfromExpression = base.Select(pagingQuerySpecification2.SelectItems).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
			{
				sqlQueryExpression,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(alias)
			});
			if (array.Length == 0)
			{
				return xfromExpression.ToPagingQuerySpecification();
			}
			return xfromExpression.GroupBy(array.Select((Identifier name) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(name, this.dbEnvironment.SqlSettings))).ToPagingQuerySpecification();
		}

		// Token: 0x060075AE RID: 30126 RVA: 0x001942F4 File Offset: 0x001924F4
		protected SqlExpression LiftForGroup(SqlExpression expression)
		{
			int num = this.groupVariables.Variables.Count;
			Alias alias;
			do
			{
				num++;
				alias = Alias.NewAssignedAlias("$groupVar" + num.ToString(CultureInfo.InvariantCulture), this.dbEnvironment.SqlSettings);
			}
			while (this.groupVariables.Contains(alias));
			this.groupVariables.Variables.Add(new SelectItem(expression, alias));
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(alias);
		}

		// Token: 0x060075AF RID: 30127 RVA: 0x0019436D File Offset: 0x0019256D
		protected virtual SqlExpression CreateListAverage(IInvocationExpression invocation)
		{
			return this.CreateAggregateFunctionWithOptionalPrecision(invocation.Arguments[0], (invocation.Arguments.Count == 2) ? invocation.Arguments[1] : null, new Func<SqlExpression, SqlExpression>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg));
		}

		// Token: 0x060075B0 RID: 30128 RVA: 0x001943AC File Offset: 0x001925AC
		protected SqlExpression CreateListCombine(IInvocationExpression invocation)
		{
			IListExpression listExpression = (IListExpression)invocation.Arguments[0];
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(listExpression.Members[0]);
			for (int i = 1; i < listExpression.Members.Count; i++)
			{
				sqlQueryExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.UnionAll(sqlQueryExpression, this.CreateQueryExpression(listExpression.Members[i]));
			}
			return sqlQueryExpression;
		}

		// Token: 0x060075B1 RID: 30129 RVA: 0x00194410 File Offset: 0x00192610
		protected SqlExpression CreateListContains(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[1];
			IExpression expression2 = invocation.Arguments[0];
			return new BinaryLogicalOperation(this.CreateScalarExpression(expression), BinaryLogicalOperator.In, (expression2.Kind == ExpressionKind.Constant) ? this.CreateInArrayExpression(((IConstantExpression)expression2).Value.AsList) : this.CreateQueryExpression(expression2));
		}

		// Token: 0x060075B2 RID: 30130 RVA: 0x0019446C File Offset: 0x0019266C
		protected AggregateFunctionCall OneIfAnyNull(SqlExpression argument)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Max(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(argument)).Then(SqlConstant.One)
				.Else(SqlConstant.Zero));
		}

		// Token: 0x060075B3 RID: 30131 RVA: 0x001944B5 File Offset: 0x001926B5
		protected SqlExpression CreateListCount(IInvocationExpression invocation)
		{
			return this.CreateListAggregate(invocation.Arguments[0], new Func<SqlExpression, AggregateFunctionCall>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Count));
		}

		// Token: 0x060075B4 RID: 30132 RVA: 0x001944D5 File Offset: 0x001926D5
		protected SqlExpression CreateListCountOfDistinct(IInvocationExpression invocation)
		{
			return new BinaryScalarOperation(this.CreateListCountOfDistinctNotNull(invocation), BinaryScalarOperator.Add, this.CreateListAggregate(invocation.Arguments[0], new Func<SqlExpression, AggregateFunctionCall>(this.OneIfAnyNull)));
		}

		// Token: 0x060075B5 RID: 30133 RVA: 0x00194504 File Offset: 0x00192704
		protected SqlExpression CreateListCountOfNull(IInvocationExpression invocation)
		{
			return new BinaryScalarOperation(this.CreateListAggregate(invocation.Arguments[0], new Func<SqlExpression, AggregateFunctionCall>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Count)), BinaryScalarOperator.Subtract, this.CreateListAggregate(invocation.Arguments[0], new Func<SqlExpression, AggregateFunctionCall>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.CountOfNotNull)));
		}

		// Token: 0x060075B6 RID: 30134 RVA: 0x00194553 File Offset: 0x00192753
		protected SqlExpression CreateListCountOfNotNull(IInvocationExpression invocation)
		{
			return this.CreateListAggregate(invocation.Arguments[0], new Func<SqlExpression, AggregateFunctionCall>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.CountOfNotNull));
		}

		// Token: 0x060075B7 RID: 30135 RVA: 0x00194573 File Offset: 0x00192773
		protected SqlExpression CreateListCountOfDistinctNotNull(IInvocationExpression invocation)
		{
			return this.CreateListAggregate(invocation.Arguments[0], new Func<SqlExpression, AggregateFunctionCall>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.CountOfDistinct));
		}

		// Token: 0x060075B8 RID: 30136 RVA: 0x00194593 File Offset: 0x00192793
		protected SqlExpression CreateListCountOfDistinctNull(IInvocationExpression invocation)
		{
			return this.CreateListAggregate(invocation.Arguments[0], new Func<SqlExpression, AggregateFunctionCall>(this.OneIfAnyNull));
		}

		// Token: 0x060075B9 RID: 30137 RVA: 0x001945B4 File Offset: 0x001927B4
		protected SqlExpression CreateListDistinct(IInvocationExpression invocation)
		{
			Alias alias = Alias.Underscore;
			TypeValue typeValue = DbAstCreator.EnsureElementType(base.GetType(invocation.Arguments[0]));
			IEnumerable<SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference> enumerable;
			if (typeValue.TypeKind == ValueKind.Record)
			{
				enumerable = typeValue.AsRecordType.Fields.Keys.Select((string f) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(alias, f, this.dbEnvironment.SqlSettings));
			}
			else
			{
				enumerable = new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(alias, Alias.ScalarColumn) };
			}
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(invocation.Arguments[0]);
			PagingQuerySpecification pagingQuerySpecification = sqlQueryExpression as PagingQuerySpecification;
			this.FixInaccessibleSortKeys(pagingQuerySpecification);
			if (((pagingQuerySpecification != null) ? pagingQuerySpecification.OrderByClause : null) == null)
			{
				return base.SelectStar(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Distinct(), enumerable).From(sqlQueryExpression, alias).ToPagingQuerySpecification();
			}
			return base.SelectStar(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Distinct(), enumerable).From(pagingQuerySpecification, alias).OrderBy(pagingQuerySpecification.OrderByClause)
				.ToPagingQuerySpecification();
		}

		// Token: 0x060075BA RID: 30138 RVA: 0x001946CC File Offset: 0x001928CC
		protected SqlExpression CreateListFirst(IInvocationExpression invocation)
		{
			return this.CreateTopExpression(invocation.Arguments[0], 1);
		}

		// Token: 0x060075BB RID: 30139 RVA: 0x001946CC File Offset: 0x001928CC
		protected SqlExpression CreateTableFirst(IInvocationExpression invocation)
		{
			return this.CreateTopExpression(invocation.Arguments[0], 1);
		}

		// Token: 0x170020A4 RID: 8356
		// (get) Token: 0x060075BC RID: 30140 RVA: 0x00002105 File Offset: 0x00000305
		protected virtual bool PivotRequiresAlias
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060075BD RID: 30141 RVA: 0x001946E4 File Offset: 0x001928E4
		protected SqlExpression CreateTablePivot(IInvocationExpression invocation)
		{
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(invocation.Arguments[0]);
			IEnumerable<Alias> enumerable = ((IListExpression)invocation.Arguments[1]).Members.Select((IExpression v) => Alias.NewAssignedAlias(((IConstantExpression)v).Value.AsText.String, this.dbEnvironment.SqlSettings));
			Alias alias = Alias.NewAssignedAlias(((IConstantExpression)invocation.Arguments[2]).Value.AsText.String, this.dbEnvironment.SqlSettings);
			Alias alias2 = Alias.NewAssignedAlias(((IConstantExpression)invocation.Arguments[3]).Value.AsText.String, this.dbEnvironment.SqlSettings);
			Value value = ((IConstantExpression)invocation.Arguments[4]).Value;
			AggregateFunctionCall aggregateFunctionCall;
			if (value.Equals(Library.List.Max))
			{
				aggregateFunctionCall = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Max(new ColumnReference(alias2));
			}
			else if (value.Equals(Library.List.Min))
			{
				aggregateFunctionCall = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Min(new ColumnReference(alias2));
			}
			else if (value.Equals(Library.List.Sum))
			{
				aggregateFunctionCall = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Sum(new ColumnReference(alias2));
			}
			else if (value.Equals(Library.List.Average))
			{
				aggregateFunctionCall = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Avg(new ColumnReference(alias2));
			}
			else
			{
				aggregateFunctionCall = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Count(new ColumnReference(alias2));
			}
			PivotClause pivotClause = new PivotClause
			{
				AttributeColumn = alias,
				ValueColumn = aggregateFunctionCall,
				PivotValues = enumerable,
				RequiresAliasing = this.PivotRequiresAlias
			};
			return this.SelectAllColumns(Alias.VirtualTable, base.GetType(invocation)).From(sqlQueryExpression, pivotClause, Alias.VirtualTable).ToPagingQuerySpecification();
		}

		// Token: 0x060075BE RID: 30142 RVA: 0x0019487C File Offset: 0x00192A7C
		protected virtual SqlExpression CreateTableUnpivot(IInvocationExpression invocation)
		{
			TypeValue type = base.GetType(invocation);
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(invocation.Arguments[0]);
			IEnumerable<Alias> enumerable = ((IListExpression)invocation.Arguments[1]).Members.Select((IExpression v) => Alias.NewAssignedAlias(((IConstantExpression)v).Value.AsText.String, this.dbEnvironment.SqlSettings));
			Alias alias = Alias.NewAssignedAlias(((IConstantExpression)invocation.Arguments[2]).Value.AsText.String, this.dbEnvironment.SqlSettings);
			Alias alias2 = Alias.NewAssignedAlias(((IConstantExpression)invocation.Arguments[3]).Value.AsText.String, this.dbEnvironment.SqlSettings);
			UnpivotClause unpivotClause = new UnpivotClause
			{
				AttributeColumn = alias,
				ValueColumn = alias2,
				PivotValues = enumerable
			};
			return this.SelectAllColumns(Alias.VirtualTable, type).From(sqlQueryExpression, unpivotClause, Alias.VirtualTable).ToPagingQuerySpecification();
		}

		// Token: 0x060075BF RID: 30143 RVA: 0x00194974 File Offset: 0x00192B74
		private SqlExpression CreateRelationalAlgebraJoinHelper(IInvocationExpression invocation, JoinOperator joinKind)
		{
			IExpression expression = invocation.Arguments[0];
			IExpression expression2 = invocation.Arguments[2];
			IListExpression listExpression = (IListExpression)invocation.Arguments[1];
			IListExpression listExpression2 = (IListExpression)invocation.Arguments[3];
			bool[] array = new bool[listExpression.Members.Count];
			if (invocation.Arguments.Count > 6 && invocation.Arguments[6].Kind == ExpressionKind.List)
			{
				IListExpression listExpression3 = (IListExpression)invocation.Arguments[6];
				for (int i = 0; i < array.Length; i++)
				{
					Value value;
					array[i] = listExpression3.Members[i].TryGetConstant(out value) && value.AsFunction.FunctionIdentity.Equals(Library._Value.NullableEquals.FunctionIdentity);
				}
			}
			RecordTypeValue itemType = base.Cursor.GetResult(expression).AsTableType.ItemType;
			RecordTypeValue itemType2 = base.Cursor.GetResult(expression2).AsTableType.ItemType;
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(expression);
			SqlQueryExpression sqlQueryExpression2 = this.CreateQueryExpression(expression2);
			List<Condition> list = new List<Condition>();
			for (int j = 0; j < listExpression.Members.Count; j++)
			{
				string asString = ((IConstantExpression)listExpression.Members[j]).Value.AsString;
				string asString2 = ((IConstantExpression)listExpression2.Members[j]).Value.AsString;
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference xcolumnReference = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.OuterSource, asString, this.dbEnvironment.SqlSettings);
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference xcolumnReference2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.InnerSource, asString2, this.dbEnvironment.SqlSettings);
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XCondition xcondition = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Equals(xcolumnReference, xcolumnReference2);
				TypeValue asType = itemType.Fields[asString]["Type"].AsType;
				TypeValue asType2 = itemType2.Fields[asString2]["Type"].AsType;
				if (asType.IsNullable && asType2.IsNullable && !array[j])
				{
					xcondition = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Or(new Condition[]
					{
						xcondition,
						SqlAstCreatorBase<DbAstCreator.SqlVariable>.And(new Condition[]
						{
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(xcolumnReference),
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(xcolumnReference2)
						})
					});
				}
				list.Add(xcondition);
			}
			SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromExpression xfromExpression = this.SelectConcatenateRecordTypes(itemType, Alias.OuterSource, itemType2, Alias.InnerSource).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
			{
				sqlQueryExpression,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(Alias.OuterSource)
			});
			if (list.Count == 0 && joinKind == JoinOperator.InnerJoin)
			{
				return xfromExpression.CrossJoin(sqlQueryExpression2, Alias.InnerSource).ToPagingQuerySpecification();
			}
			if (!this.dbEnvironment.SqlSettings.SupportsFullOuterJoinExpression && joinKind == JoinOperator.FullOuterJoin)
			{
				List<Condition> list2 = new List<Condition>();
				for (int k = 0; k < listExpression.Members.Count; k++)
				{
					list2.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.OuterSource, ((IConstantExpression)listExpression.Members[k]).Value.AsString, this.dbEnvironment.SqlSettings)));
				}
				SqlQueryExpression sqlQueryExpression3 = xfromExpression.Join(sqlQueryExpression2, Alias.InnerSource, JoinOperator.LeftOuterJoin).On(list).ToPagingQuerySpecification();
				SqlQueryExpression sqlQueryExpression4 = xfromExpression.Join(sqlQueryExpression2, Alias.InnerSource, JoinOperator.RightOuterJoin).On(list).Where(list2)
					.ToPagingQuerySpecification();
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.UnionAll(sqlQueryExpression3, sqlQueryExpression4);
			}
			return xfromExpression.Join(sqlQueryExpression2, Alias.InnerSource, joinKind).On(list).ToPagingQuerySpecification();
		}

		// Token: 0x060075C0 RID: 30144 RVA: 0x00194D78 File Offset: 0x00192F78
		private SqlExpression CreateRelationalAlgebraAntiSemiJoin(IInvocationExpression invocation, TableTypeAlgebra.JoinKind joinKind)
		{
			UnaryLogicalOperator unaryLogicalOperator;
			bool flag;
			switch (joinKind)
			{
			case TableTypeAlgebra.JoinKind.LeftAnti:
				unaryLogicalOperator = UnaryLogicalOperator.NotExists;
				flag = true;
				break;
			case TableTypeAlgebra.JoinKind.RightAnti:
				unaryLogicalOperator = UnaryLogicalOperator.NotExists;
				flag = false;
				break;
			case TableTypeAlgebra.JoinKind.LeftSemi:
				unaryLogicalOperator = UnaryLogicalOperator.Exists;
				flag = true;
				break;
			case TableTypeAlgebra.JoinKind.RightSemi:
				unaryLogicalOperator = UnaryLogicalOperator.Exists;
				flag = false;
				break;
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
			IExpression expression = invocation.Arguments[flag ? 0 : 2];
			IExpression expression2 = invocation.Arguments[flag ? 2 : 0];
			IListExpression listExpression = (IListExpression)invocation.Arguments[flag ? 1 : 3];
			IListExpression listExpression2 = (IListExpression)invocation.Arguments[flag ? 3 : 1];
			bool[] array = new bool[listExpression.Members.Count];
			if (invocation.Arguments.Count > 6 && invocation.Arguments[6].Kind == ExpressionKind.List)
			{
				IListExpression listExpression3 = (IListExpression)invocation.Arguments[6];
				for (int i = 0; i < array.Length; i++)
				{
					Value value;
					array[i] = listExpression3.Members[i].TryGetConstant(out value) && value.AsFunction.FunctionIdentity.Equals(Library._Value.NullableEquals.FunctionIdentity);
				}
			}
			RecordTypeValue itemType = base.Cursor.GetResult(expression).AsTableType.ItemType;
			RecordTypeValue itemType2 = base.Cursor.GetResult(expression2).AsTableType.ItemType;
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(expression);
			SqlQueryExpression sqlQueryExpression2 = this.CreateQueryExpression(expression2);
			List<Condition> list = new List<Condition>();
			for (int j = 0; j < listExpression.Members.Count; j++)
			{
				string asString = ((IConstantExpression)listExpression.Members[j]).Value.AsString;
				string asString2 = ((IConstantExpression)listExpression2.Members[j]).Value.AsString;
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference xcolumnReference = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.OuterSource, asString, this.dbEnvironment.SqlSettings);
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference xcolumnReference2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.InnerSource, asString2, this.dbEnvironment.SqlSettings);
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XCondition xcondition = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Equals(xcolumnReference, xcolumnReference2);
				TypeValue asType = itemType.Fields[asString]["Type"].AsType;
				TypeValue asType2 = itemType2.Fields[asString2]["Type"].AsType;
				if (asType.IsNullable && asType2.IsNullable && !array[j])
				{
					xcondition = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Or(new Condition[]
					{
						xcondition,
						SqlAstCreatorBase<DbAstCreator.SqlVariable>.And(new Condition[]
						{
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(xcolumnReference),
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(xcolumnReference2)
						})
					});
				}
				list.Add(xcondition);
			}
			return this.SelectConcatenateRecordTypesAntiSemi(flag, itemType, Alias.OuterSource, itemType2).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
			{
				sqlQueryExpression,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(Alias.OuterSource)
			}).Where(new UnaryLogicalOperation(unaryLogicalOperator, base.Select(SqlConstant.One, null).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
			{
				sqlQueryExpression2,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(Alias.InnerSource)
			}).Where(list)
				.ToPagingQuerySpecification()))
				.ToPagingQuerySpecification();
		}

		// Token: 0x060075C1 RID: 30145 RVA: 0x00195108 File Offset: 0x00193308
		protected SqlExpression CreateTableJoin(IInvocationExpression invocation)
		{
			TableTypeAlgebra.JoinKind value = Library.JoinKind.Type.GetValue(((ConstantExpressionSyntaxNode)invocation.Arguments[4]).Value.AsNumber);
			switch (value)
			{
			case TableTypeAlgebra.JoinKind.Inner:
				return this.CreateRelationalAlgebraJoinHelper(invocation, JoinOperator.InnerJoin);
			case TableTypeAlgebra.JoinKind.LeftOuter:
				return this.CreateRelationalAlgebraJoinHelper(invocation, JoinOperator.LeftOuterJoin);
			case TableTypeAlgebra.JoinKind.FullOuter:
				return this.CreateRelationalAlgebraJoinHelper(invocation, JoinOperator.FullOuterJoin);
			case TableTypeAlgebra.JoinKind.RightOuter:
				return this.CreateRelationalAlgebraJoinHelper(invocation, JoinOperator.RightOuterJoin);
			case TableTypeAlgebra.JoinKind.LeftAnti:
			case TableTypeAlgebra.JoinKind.RightAnti:
			case TableTypeAlgebra.JoinKind.LeftSemi:
			case TableTypeAlgebra.JoinKind.RightSemi:
				return this.CreateRelationalAlgebraAntiSemiJoin(invocation, value);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x060075C2 RID: 30146 RVA: 0x001951A0 File Offset: 0x001933A0
		protected SqlExpression CreateListMax(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			return this.CreateListAggregateRaw(expression, this.EnsureNumericBit(expression, new Func<SqlExpression, AggregateFunctionCall>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Max)));
		}

		// Token: 0x060075C3 RID: 30147 RVA: 0x001951D4 File Offset: 0x001933D4
		protected SqlExpression CreateListMin(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			return this.CreateListAggregateRaw(expression, this.EnsureNumericBit(expression, new Func<SqlExpression, AggregateFunctionCall>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Min)));
		}

		// Token: 0x060075C4 RID: 30148 RVA: 0x00195208 File Offset: 0x00193408
		protected Func<SqlExpression, SqlExpression> EnsureNumericBit(IExpression list, Func<SqlExpression, AggregateFunctionCall> func)
		{
			if (base.Cursor.GetResult(list).AsListType.ItemType.TypeKind == ValueKind.Logical)
			{
				return (SqlExpression expr) => this.Convert(SqlDataType.Bit, func(this.Convert(SqlDataType.TinyInt, expr)));
			}
			return (SqlExpression expr) => func(expr);
		}

		// Token: 0x060075C5 RID: 30149 RVA: 0x00195260 File Offset: 0x00193460
		protected SqlExpression CreateNativeQuery(IInvocationExpression invocation)
		{
			RecordTypeValue recordType = base.Cursor.GetResult(invocation).AsTableType.ItemType;
			bool mitigatedColumns = false;
			IList<SelectItem> list = recordType.Fields.Keys.Select((string o) => this.MitigateColumn(o, RecordTypeAlgebra.Field(recordType, o), ref mitigatedColumns)).ToList<SelectItem>();
			string asString = ((IConstantExpression)invocation.Arguments[1]).Value.AsString;
			return base.SelectStar(list).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
			{
				new VerbatimSqlQueryExpression(asString, null),
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(Alias.VirtualTable)
			}).ToPagingQuerySpecification();
		}

		// Token: 0x060075C6 RID: 30150 RVA: 0x00195328 File Offset: 0x00193528
		protected SqlExpression CreateListSelect(IInvocationExpression invocation)
		{
			TypeValue[] parameterResults = base.Cursor.GetParameterResults(invocation);
			Alias alias = Alias.NewAssignedAlias(parameterResults[1].AsFunctionType.ParameterName(0), this.dbEnvironment.SqlSettings);
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(invocation.Arguments[0]);
			IFunctionExpression functionExpression = (IFunctionExpression)EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(invocation.Arguments[1]);
			this.PushFunction(functionExpression, parameterResults[1].AsFunctionType, new DbAstCreator.SqlVariable[] { DbAstCreator.SqlVariable.CreateRow(alias) });
			Condition condition = this.CreateConditionExpression(functionExpression.Expression);
			this.PopFunction(functionExpression);
			SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromExpression xfromExpression = this.SelectAllColumns(alias, parameterResults[0].AsTableType.ItemType).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
			{
				sqlQueryExpression,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(alias)
			});
			if (condition == this.Condition(true))
			{
				return xfromExpression.ToPagingQuerySpecification();
			}
			return xfromExpression.Where(condition).ToPagingQuerySpecification();
		}

		// Token: 0x060075C7 RID: 30151 RVA: 0x00195428 File Offset: 0x00193628
		protected SqlExpression CreateListSort(IInvocationExpression invocation)
		{
			Alias underscore = Alias.Underscore;
			TypeValue itemType = base.GetType(invocation.Arguments[0]).AsTableType.ItemType;
			OrderByClause orderByClause = new OrderByClause();
			HashSet<string> hashSet = new HashSet<string>();
			SqlQueryExpression sqlQueryExpression = this.CreateListSortListQuery(invocation, orderByClause, hashSet);
			return this.SelectAllColumns(underscore, itemType).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
			{
				sqlQueryExpression,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(underscore)
			}).OrderBy(orderByClause)
				.ToPagingQuerySpecification();
		}

		// Token: 0x060075C8 RID: 30152 RVA: 0x001954BC File Offset: 0x001936BC
		private SqlQueryExpression CreateListSortListQuery(IExpression expression, OrderByClause orderBy, HashSet<string> fieldsSeen)
		{
			if (expression.Kind == ExpressionKind.Invocation)
			{
				IInvocationExpression invocationExpression = (IInvocationExpression)expression;
				if (((IConstantExpression)invocationExpression.Function).Value.Equals(TableModule.Table.Sort))
				{
					this.CreateOrderByItems(orderBy, (IListExpression)invocationExpression.Arguments[1], base.GetType(invocationExpression.Arguments[0]), fieldsSeen);
					return this.CreateListSortListQuery(invocationExpression.Arguments[0], orderBy, fieldsSeen);
				}
			}
			return this.CreateQueryExpression(expression);
		}

		// Token: 0x060075C9 RID: 30153 RVA: 0x0019553C File Offset: 0x0019373C
		protected SqlExpression CreateListStandardDeviation(IInvocationExpression invocation)
		{
			return this.CreateListAggregate(invocation.Arguments[0], new Func<SqlExpression, AggregateFunctionCall>(this.Stdev));
		}

		// Token: 0x060075CA RID: 30154 RVA: 0x0019555D File Offset: 0x0019375D
		protected SqlExpression CreateListSum(IInvocationExpression invocation)
		{
			return this.CreateAggregateFunctionWithOptionalPrecision(invocation.Arguments[0], (invocation.Arguments.Count == 2) ? invocation.Arguments[1] : null, new Func<SqlExpression, SqlExpression>(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Sum));
		}

		// Token: 0x060075CB RID: 30155 RVA: 0x0019559C File Offset: 0x0019379C
		protected SqlExpression CreateAggregateFunctionWithOptionalPrecision(IExpression list, IExpression precision, Func<SqlExpression, SqlExpression> func)
		{
			Func<SqlExpression, SqlExpression> func2 = func;
			if (precision != null)
			{
				Func<SqlExpression, SqlExpression> castingFunction = this.CreateNumericCastFromPrecision(precision as IConstantExpression);
				func2 = (SqlExpression x) => func(castingFunction(x));
			}
			return this.CreateListAggregateRaw(list, func2);
		}

		// Token: 0x060075CC RID: 30156 RVA: 0x001955E8 File Offset: 0x001937E8
		protected Func<SqlExpression, SqlExpression> CreateNumericCastFromPrecision(IConstantExpression precision)
		{
			if (precision == null || precision.Value.IsNull)
			{
				return (SqlExpression x) => x;
			}
			if (precision.Value.Equals(Library.PrecisionEnum.Decimal))
			{
				return new Func<SqlExpression, SqlExpression>(this.CastToDecimal);
			}
			if (precision.Value.Equals(Library.PrecisionEnum.Double))
			{
				return new Func<SqlExpression, SqlExpression>(this.CastToDouble);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x060075CD RID: 30157 RVA: 0x0019566C File Offset: 0x0019386C
		protected SqlExpression CreateListTransform(IInvocationExpression invocation)
		{
			TypeValue[] parameterResults = base.Cursor.GetParameterResults(invocation);
			IFunctionExpression functionExpression = (IFunctionExpression)EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(invocation.Arguments[1]);
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(invocation.Arguments[0]);
			PagingQuerySpecification pagingQuerySpecification = sqlQueryExpression as PagingQuerySpecification;
			this.FixInaccessibleSortKeys(pagingQuerySpecification);
			Alias alias = Alias.NewAssignedAlias(parameterResults[1].AsFunctionType.ParameterName(0), this.dbEnvironment.SqlSettings);
			this.PushFunction(functionExpression, parameterResults[1].AsFunctionType, new DbAstCreator.SqlVariable[] { DbAstCreator.SqlVariable.CreateRow(alias) });
			SqlQueryExpression sqlQueryExpression2 = this.CreateQueryExpression(functionExpression.Expression);
			this.PopFunction(functionExpression);
			PagingQuerySpecification pagingQuerySpecification2 = (PagingQuerySpecification)sqlQueryExpression2;
			if (((pagingQuerySpecification != null) ? pagingQuerySpecification.OrderByClause : null) == null)
			{
				return base.Select(pagingQuerySpecification2.SelectItems).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
				{
					sqlQueryExpression,
					SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(alias)
				}).ToPagingQuerySpecification();
			}
			return base.Select(pagingQuerySpecification2.SelectItems).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
			{
				sqlQueryExpression,
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(alias)
			}).OrderBy(pagingQuerySpecification.OrderByClause)
				.ToPagingQuerySpecification();
		}

		// Token: 0x060075CE RID: 30158 RVA: 0x001957C0 File Offset: 0x001939C0
		private void FixInaccessibleSortKeys(PagingQuerySpecification querySpec)
		{
			if (querySpec == null || querySpec.OrderByClause == null)
			{
				return;
			}
			OrderByClause orderByClause = querySpec.OrderByClause;
			Func<int, string> <>9__0;
			for (int i = 0; i < orderByClause.OrderByItems.Count; i++)
			{
				ColumnReference sortColumn = orderByClause.OrderByItems[i].SortColumn;
				SelectItem bestMatch = querySpec.SelectItems.GetBestMatch(sortColumn);
				if (bestMatch == null)
				{
					int count = querySpec.SelectItems.Count;
					Func<int, string> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (int index) => querySpec.SelectItems[index].Name.Name);
					}
					Alias alias = Alias.NewNativeAlias(TableValue.GetUniqueName(Keys.New(count, func), i));
					querySpec.SelectItems.Add(new SelectItem(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(sortColumn.Qualifier, sortColumn.Name), alias));
					OrderByOption order = orderByClause.OrderByItems[i].Order;
					orderByClause.OrderByItems[i] = new OrderByItem(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(sortColumn.Qualifier, alias), order);
				}
				else if (bestMatch.Alias != null)
				{
					OrderByOption order2 = orderByClause.OrderByItems[i].Order;
					orderByClause.OrderByItems[i] = new OrderByItem(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(bestMatch.Alias), order2);
				}
			}
		}

		// Token: 0x060075CF RID: 30159 RVA: 0x00195930 File Offset: 0x00193B30
		private SqlExpression CreateLogicalOperation(BinaryLogicalOperator predicate, IBinaryExpression binary)
		{
			IExpression left = binary.Left;
			IExpression right = binary.Right;
			TypeValue type = base.GetType(left);
			TypeValue type2 = base.GetType(right);
			SqlExpression sqlExpression = this.CreateScalarExpression(left);
			SqlExpression sqlExpression2 = this.CreateScalarExpression(right);
			this.AdjustArgumentsForType(binary.Operator, ref type, ref type2, ref sqlExpression, ref sqlExpression2);
			return new BinaryLogicalOperation(sqlExpression, predicate, sqlExpression2);
		}

		// Token: 0x060075D0 RID: 30160 RVA: 0x0019598C File Offset: 0x00193B8C
		private SqlExpression CreateNotEqualScalar(IBinaryExpression notEquals)
		{
			IExpression left = notEquals.Left;
			IExpression right = notEquals.Right;
			TypeValue type = base.GetType(left);
			TypeValue type2 = base.GetType(right);
			SqlExpression sqlExpression = this.CreateScalarExpression(left);
			SqlExpression sqlExpression2 = this.CreateScalarExpression(right);
			this.AdjustArgumentsForType(notEquals.Operator, ref type, ref type2, ref sqlExpression, ref sqlExpression2);
			if (sqlExpression == sqlExpression2)
			{
				return SqlConstant.NumericFalse;
			}
			SqlConstant sqlConstant = sqlExpression as SqlConstant;
			SqlConstant sqlConstant2 = sqlExpression2 as SqlConstant;
			if (type.TypeKind == ValueKind.Null)
			{
				if (sqlConstant2 == SqlConstant.Null)
				{
					return SqlConstant.NumericFalse;
				}
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNotNull(sqlExpression2);
			}
			else if (type2.TypeKind == ValueKind.Null)
			{
				if (sqlConstant == SqlConstant.Null)
				{
					return SqlConstant.NumericFalse;
				}
				return SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNotNull(sqlExpression);
			}
			else
			{
				bool flag = !DbTypeServices.IsCompatibleType(type, type2, notEquals.Operator);
				SqlAstCreatorBase<DbAstCreator.SqlVariable>.XCondition xcondition = SqlAstCreatorBase<DbAstCreator.SqlVariable>.NotEqualTo(sqlExpression, sqlExpression2);
				if (type.IsNullable && type2.IsNullable && (sqlConstant == null || sqlConstant2 == null))
				{
					Condition condition = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Or(new Condition[]
					{
						SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNotNull(sqlExpression),
						SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNotNull(sqlExpression2)
					});
					if (flag)
					{
						return condition;
					}
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.And(new Condition[]
					{
						SqlAstCreatorBase<DbAstCreator.SqlVariable>.Or(new Condition[]
						{
							xcondition,
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(sqlExpression),
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(sqlExpression2)
						}),
						condition
					});
				}
				else
				{
					if (flag)
					{
						return this.Condition(true);
					}
					if (sqlExpression == sqlExpression2)
					{
						return this.Condition(false);
					}
					if (this.AreTrueFalseCondition(new SqlExpression[] { sqlExpression, sqlExpression2 }))
					{
						return this.Condition(true);
					}
					if (type.IsNullable && sqlConstant == null)
					{
						return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Or(new Condition[]
						{
							xcondition,
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(sqlExpression)
						});
					}
					if (type2.IsNullable && sqlConstant2 == null)
					{
						return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Or(new Condition[]
						{
							xcondition,
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.IsNull(sqlExpression2)
						});
					}
					return xcondition;
				}
			}
		}

		// Token: 0x060075D1 RID: 30161
		protected abstract SqlExpression CreateNumberArcTangent2(IInvocationExpression invocation);

		// Token: 0x060075D2 RID: 30162 RVA: 0x00195B9A File Offset: 0x00193D9A
		protected virtual SqlExpression CreateNumberLogBase10(IInvocationExpression invocation)
		{
			return this.CreateFunctionCall(SqlLanguageStrings.Log10SqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x060075D3 RID: 30163 RVA: 0x00195BB4 File Offset: 0x00193DB4
		protected SqlExpression CreateNumberModCall(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = this.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = this.CreateScalarExpression(invocation.Arguments[1]);
			IConstantExpression constantExpression = null;
			if (invocation.Arguments.Count == 3)
			{
				IConstantExpression constantExpression2 = invocation.Arguments[2] as IConstantExpression;
				constantExpression = (constantExpression2.Value.IsNull ? null : constantExpression2);
			}
			return this.CreateNumberMod(sqlExpression, sqlExpression2, constantExpression);
		}

		// Token: 0x060075D4 RID: 30164 RVA: 0x00195C24 File Offset: 0x00193E24
		protected virtual SqlExpression CreateNumberMod(SqlExpression number, SqlExpression divisor, IConstantExpression precision = null)
		{
			Func<SqlExpression, SqlExpression> func = this.CreateNumericCastFromPrecision(precision);
			return new BinaryScalarOperation(func(number), BinaryScalarOperator.Modulo, func(divisor));
		}

		// Token: 0x060075D5 RID: 30165 RVA: 0x0004635D File Offset: 0x0004455D
		protected virtual SqlExpression CreateNumberNaturalLogarithm(IInvocationExpression invocation)
		{
			return this.CreateFunctionCall(SqlLanguageStrings.LogSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x060075D6 RID: 30166 RVA: 0x00195C4D File Offset: 0x00193E4D
		protected virtual SqlExpression CreateNumberPower(IInvocationExpression invocation)
		{
			return this.CreateFunctionCall(SqlLanguageStrings.PowerSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x060075D7 RID: 30167 RVA: 0x00195C68 File Offset: 0x00193E68
		protected virtual SqlExpression CreateNumberRound(IInvocationExpression invocation)
		{
			int count = invocation.Arguments.Count;
			if (count == 1)
			{
				return this.CreateFunctionCall(SqlLanguageStrings.RoundSqlString, new SqlExpression[] { SqlConstant.Zero })(invocation);
			}
			if (count != 2)
			{
				throw new InvalidOperationException();
			}
			return this.CreateFunctionCall(SqlLanguageStrings.RoundSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x060075D8 RID: 30168 RVA: 0x00195CC7 File Offset: 0x00193EC7
		protected virtual SqlExpression CreateNumberSign(IInvocationExpression invocation)
		{
			return this.CreateFunctionCall(SqlLanguageStrings.SignSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x060075D9 RID: 30169 RVA: 0x00195CE0 File Offset: 0x00193EE0
		protected virtual SqlExpression CreateNumberFrom(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			ValueKind typeKind = base.GetType(expression).TypeKind;
			if (typeKind - ValueKind.Date <= 1)
			{
				return this.CreateToDouble(invocation);
			}
			return this.CreateScalarExpression(expression);
		}

		// Token: 0x060075DA RID: 30170
		protected abstract SqlExpression CreateToText(IInvocationExpression invocation);

		// Token: 0x060075DB RID: 30171 RVA: 0x00195D1C File Offset: 0x00193F1C
		protected SqlQueryOptions CreateOptions()
		{
			return DbAstCreator.CreateOptions(this.dbEnvironment);
		}

		// Token: 0x060075DC RID: 30172 RVA: 0x00195D2C File Offset: 0x00193F2C
		public static SqlQueryOptions CreateOptions(DbEnvironment dbEnvironment)
		{
			SqlQueryOptions sqlQueryOptions = new SqlQueryOptions();
			object obj;
			if (dbEnvironment != null && dbEnvironment.UserOptions.TryGetValue("MaxDegreeOfParallelism", out obj))
			{
				sqlQueryOptions.MaxDegreeOfParallelism = new int?((int)obj);
			}
			return sqlQueryOptions;
		}

		// Token: 0x060075DD RID: 30173
		protected abstract SqlExpression ConvertNumberToDate(SqlExpression number);

		// Token: 0x060075DE RID: 30174
		protected abstract SqlExpression ConvertNumberToDateTime(SqlExpression number);

		// Token: 0x060075DF RID: 30175 RVA: 0x00195D68 File Offset: 0x00193F68
		protected virtual SqlExpression CreateToDate(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = this.CreateScalarExpression(expression);
			if (type.TypeKind == ValueKind.Number)
			{
				return this.ConvertNumberToDate(sqlExpression);
			}
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x060075E0 RID: 30176 RVA: 0x00195DB0 File Offset: 0x00193FB0
		protected virtual SqlExpression CreateToDateTime(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = this.CreateScalarExpression(expression);
			ValueKind typeKind = type.TypeKind;
			if (typeKind == ValueKind.DateTime)
			{
				return sqlExpression;
			}
			if (typeKind != ValueKind.Number)
			{
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
			return this.ConvertNumberToDateTime(sqlExpression);
		}

		// Token: 0x060075E1 RID: 30177 RVA: 0x0004FA9D File Offset: 0x0004DC9D
		protected virtual SqlExpression CreateToDateTimeWithTimeZone(IInvocationExpression invocation)
		{
			throw new InvalidOperationException(Strings.UnreachableCodePath);
		}

		// Token: 0x060075E2 RID: 30178 RVA: 0x00195E02 File Offset: 0x00194002
		protected virtual SqlExpression CreateToSingle(IInvocationExpression invocation)
		{
			return this.CastToSingle(this.CreateToNonIntType(invocation));
		}

		// Token: 0x060075E3 RID: 30179 RVA: 0x00195E11 File Offset: 0x00194011
		protected virtual SqlExpression CreateToDouble(IInvocationExpression invocation)
		{
			return this.CastToDouble(this.CreateToNonIntType(invocation));
		}

		// Token: 0x060075E4 RID: 30180 RVA: 0x00195E20 File Offset: 0x00194020
		protected virtual SqlExpression CreateToDecimal(IInvocationExpression invocation)
		{
			return this.CastToDecimal(this.CreateToNonIntType(invocation));
		}

		// Token: 0x060075E5 RID: 30181 RVA: 0x00195E30 File Offset: 0x00194030
		protected virtual SqlExpression CreateToNonIntType(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue type = base.GetType(expression);
			SqlExpression sqlExpression = this.CreateScalarExpression(expression);
			ValueKind typeKind = type.TypeKind;
			if (typeKind == ValueKind.Date)
			{
				return this.ConvertDateToNumber(sqlExpression);
			}
			if (typeKind != ValueKind.DateTime)
			{
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
			return this.ConvertDateTimeToNumber(sqlExpression);
		}

		// Token: 0x060075E6 RID: 30182
		protected abstract SqlExpression ConvertDateToNumber(SqlExpression expression);

		// Token: 0x060075E7 RID: 30183
		protected abstract SqlExpression ConvertDateTimeToNumber(SqlExpression expression);

		// Token: 0x060075E8 RID: 30184 RVA: 0x00195E88 File Offset: 0x00194088
		protected SqlExpression CreateOADateTimeSignExpression(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Case().When(new BinaryLogicalOperation(expression, BinaryLogicalOperator.LessThan, this.BaseOADateTime)).Then(SqlConstant.MinusOne)
				.Else(SqlConstant.One);
		}

		// Token: 0x060075E9 RID: 30185 RVA: 0x00195ED0 File Offset: 0x001940D0
		protected virtual SqlExpression CreateDurationFrom(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = this.CreateScalarExpression(invocation.Arguments[0]);
			return this.CastToBigInt(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(sqlExpression, this.TicksPerDay));
		}

		// Token: 0x060075EA RID: 30186 RVA: 0x00195F04 File Offset: 0x00194104
		private void CreateOrderByItems(OrderByClause orderBy, IListExpression sortCriteriaCollection, TypeValue elementType, HashSet<string> fieldsSeen)
		{
			sortCriteriaCollection = (IListExpression)EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(sortCriteriaCollection);
			for (int i = 0; i < sortCriteriaCollection.Members.Count; i++)
			{
				IRecordExpression recordExpression = (IRecordExpression)EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(sortCriteriaCollection.Members[i]);
				base.Cursor.Push(sortCriteriaCollection, i);
				IFunctionExpression functionExpression = (IFunctionExpression)EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(recordExpression.Members.First((VariableInitializer m) => m.Name.Equals("KeySelector")).Value);
				base.Cursor.Push(recordExpression, "KeySelector");
				base.Cursor.Push(functionExpression, new TypeValue[] { elementType });
				base.EnterScope(functionExpression, new DbAstCreator.SqlVariable[] { DbAstCreator.SqlVariable.CreateRow(Alias.Underscore) });
				string name = ((IFieldAccessExpression)functionExpression.Expression).MemberName.Name;
				base.ExitScope(functionExpression);
				base.Cursor.Pop();
				base.Cursor.Pop();
				if (fieldsSeen.Add(name))
				{
					bool flag = true;
					IExpression value = recordExpression.Members.FirstOrDefault((VariableInitializer m) => m.Name.Equals("Ascending")).Value;
					if (value != null)
					{
						base.Cursor.Push(recordExpression, "Ascending");
						flag = ((IConstantExpression)value).Value.AsBoolean;
						base.Cursor.Pop();
					}
					orderBy.OrderByItems.Add(new OrderByItem(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.Underscore, name, this.dbEnvironment.SqlSettings), flag ? OrderByOption.Ascending : OrderByOption.Descending));
				}
				base.Cursor.Pop();
			}
		}

		// Token: 0x060075EB RID: 30187 RVA: 0x001960D0 File Offset: 0x001942D0
		public SqlQueryExpression CreateQuery(SqlExpression sourceExpression)
		{
			return this.ConvertToQuery(sourceExpression);
		}

		// Token: 0x060075EC RID: 30188 RVA: 0x001960DC File Offset: 0x001942DC
		public virtual SqlQueryExpression CreatePagingQuery(SqlExpression sourceQuery, string[] columnNames, string[] sortColumnNames, long offsetExpression, long? fetchExpression)
		{
			SqlQueryExpression sqlQueryExpression = this.CreatePagingSourceQuery(sourceQuery, columnNames, sortColumnNames);
			if (offsetExpression == 0L && fetchExpression == null)
			{
				return sqlQueryExpression;
			}
			PagingQuerySpecification pagingQuerySpecification = sqlQueryExpression as PagingQuerySpecification;
			if (pagingQuerySpecification != null)
			{
				pagingQuerySpecification = pagingQuerySpecification.ShallowCopy();
				if (pagingQuerySpecification.PagingClause != null && pagingQuerySpecification.PagingClause.FetchExpression != null)
				{
					if (fetchExpression != null)
					{
						fetchExpression = new long?(Math.Min(fetchExpression.Value, pagingQuerySpecification.PagingClause.FetchExpression.Value));
					}
					else
					{
						fetchExpression = pagingQuerySpecification.PagingClause.FetchExpression;
					}
				}
			}
			else
			{
				pagingQuerySpecification = base.SelectStar(columnNames.Select((string c) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(c, this.dbEnvironment.SqlSettings))).From(sqlQueryExpression).ToPagingQuerySpecification();
			}
			pagingQuerySpecification.PagingClause = new PagingClause
			{
				OffsetExpression = offsetExpression,
				FetchExpression = fetchExpression
			};
			return pagingQuerySpecification;
		}

		// Token: 0x060075ED RID: 30189 RVA: 0x001961BC File Offset: 0x001943BC
		private SqlQueryExpression CreatePagingSourceQuery(SqlExpression sourceExpression, string[] columnNames, string[] sortColumnNames)
		{
			SqlQueryExpression sqlQueryExpression = this.ConvertToQuery(sourceExpression);
			if (sortColumnNames != null && (!(sqlQueryExpression is PagingQuerySpecification) || ((PagingQuerySpecification)sqlQueryExpression).OrderByClause == null))
			{
				OrderByClause orderByClause = new OrderByClause();
				IList<OrderByItem> orderByItems = orderByClause.OrderByItems;
				for (int i = 0; i < sortColumnNames.Length; i++)
				{
					orderByItems.Add(new OrderByItem(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.OrderedSource, sortColumnNames[i], this.dbEnvironment.SqlSettings), OrderByOption.Ascending));
				}
				sqlQueryExpression = base.Select(this.GetPagingSourceQueryOrderedSelectItems(sqlQueryExpression, columnNames)).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
				{
					sqlQueryExpression,
					SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(Alias.OrderedSource)
				}).OrderBy(orderByClause)
					.ToPagingQuerySpecification();
			}
			return sqlQueryExpression;
		}

		// Token: 0x060075EE RID: 30190 RVA: 0x0019628E File Offset: 0x0019448E
		protected virtual IEnumerable<SelectItem> GetPagingSourceQueryOrderedSelectItems(SqlQueryExpression queryExpression, string[] columnNames)
		{
			return columnNames.Select((string name) => this.CreatePagingSourceQueryOrderedSelectItem(name));
		}

		// Token: 0x060075EF RID: 30191 RVA: 0x001962A2 File Offset: 0x001944A2
		protected SelectItem CreatePagingSourceQueryOrderedSelectItem(string columnName)
		{
			return new SelectItem(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.OrderedSource, columnName, this.dbEnvironment.SqlSettings));
		}

		// Token: 0x060075F0 RID: 30192 RVA: 0x001962C4 File Offset: 0x001944C4
		protected SqlQueryExpression CreateQueryExpression(IExpression expression)
		{
			return this.ConvertToQuery(this.GetValue(expression));
		}

		// Token: 0x060075F1 RID: 30193 RVA: 0x001962D3 File Offset: 0x001944D3
		protected SqlQueryExpression CreateResult()
		{
			return this.CreateQueryExpression(base.RootExpression);
		}

		// Token: 0x060075F2 RID: 30194 RVA: 0x001962E1 File Offset: 0x001944E1
		protected SqlExpression CreateScalarExpression(IExpression expression)
		{
			return this.ConvertToScalar(this.GetValue(expression));
		}

		// Token: 0x060075F3 RID: 30195 RVA: 0x001962F0 File Offset: 0x001944F0
		protected SqlStatement CreateStatement()
		{
			return this.CreateStatement(base.RootExpression);
		}

		// Token: 0x060075F4 RID: 30196 RVA: 0x00196300 File Offset: 0x00194500
		protected SqlStatement CreateStatement(IExpression expression)
		{
			IInvocationExpression invocationExpression = (IInvocationExpression)expression;
			Value value = ((IConstantExpression)invocationExpression.Function).Value;
			Func<IInvocationExpression, SqlStatement> func;
			if (value.IsFunction && this.StatementFunctionLookup.TryGetValue(value.AsFunction, out func))
			{
				return func(invocationExpression);
			}
			if (value is QueryResultFunctionValue)
			{
				return this.CreateExecuteStoredProcedure(invocationExpression);
			}
			throw new InvalidOperationException("Unsupported action not caught by the checker!");
		}

		// Token: 0x060075F5 RID: 30197
		protected abstract SqlExpression CreateTextContains(IInvocationExpression invocation);

		// Token: 0x060075F6 RID: 30198 RVA: 0x00196364 File Offset: 0x00194564
		protected virtual SqlExpression CreateTextEndsWith(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			SqlExpression sqlExpression = this.CreateScalarExpression(arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Equals(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RightSqlString), new SqlExpression[]
			{
				this.CreateScalarExpression(arguments[0]),
				this.Len(sqlExpression)
			}), sqlExpression);
		}

		// Token: 0x060075F7 RID: 30199 RVA: 0x001963C0 File Offset: 0x001945C0
		protected SqlExpression CreateTextLength(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = this.CreateScalarExpression(invocation.Arguments[0]);
			return this.Len(sqlExpression);
		}

		// Token: 0x060075F8 RID: 30200 RVA: 0x001963E8 File Offset: 0x001945E8
		protected virtual SqlExpression CreateTextStartsWith(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			SqlExpression sqlExpression = this.CreateScalarExpression(arguments[1]);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Equals(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LeftSqlString), new SqlExpression[]
			{
				this.CreateScalarExpression(arguments[0]),
				this.Len(sqlExpression)
			}), sqlExpression);
		}

		// Token: 0x060075F9 RID: 30201 RVA: 0x00196444 File Offset: 0x00194644
		protected virtual SqlExpression CreateTextTrim(IInvocationExpression invocation)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LTrimSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.RTrimSqlString), new SqlExpression[] { this.CreateScalarExpression(invocation.Arguments[0]) }) });
		}

		// Token: 0x060075FA RID: 30202 RVA: 0x00196493 File Offset: 0x00194693
		protected virtual SqlExpression CreateTextTrimEnd(IInvocationExpression invocation)
		{
			return this.CreateFunctionCall(SqlLanguageStrings.RTrimSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x060075FB RID: 30203 RVA: 0x001964AB File Offset: 0x001946AB
		protected virtual SqlExpression CreateTextTrimStart(IInvocationExpression invocation)
		{
			return this.CreateFunctionCall(SqlLanguageStrings.LTrimSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x060075FC RID: 30204 RVA: 0x001964C3 File Offset: 0x001946C3
		private SqlExpression CreateValueAs(IInvocationExpression invocation)
		{
			return this.GetValue(invocation.Arguments[0]);
		}

		// Token: 0x060075FD RID: 30205 RVA: 0x001964D7 File Offset: 0x001946D7
		protected SqlExpression CreateValueEquals(IInvocationExpression invocation)
		{
			return this.CreateValueEquals(invocation, false);
		}

		// Token: 0x060075FE RID: 30206 RVA: 0x001964E1 File Offset: 0x001946E1
		protected SqlExpression CreateValueNullableEquals(IInvocationExpression invocation)
		{
			return this.CreateValueEquals(invocation, true);
		}

		// Token: 0x060075FF RID: 30207 RVA: 0x001964EC File Offset: 0x001946EC
		private SqlExpression CreateValueEquals(IInvocationExpression invocation, bool isNullable)
		{
			IConstantExpression constantExpression = ((invocation.Arguments.Count == 3) ? (invocation.Arguments[2] as IConstantExpression) : null);
			return this.CreateEqualScalar(BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, invocation.Arguments[0], invocation.Arguments[1], TokenRange.Null), isNullable, this.CreateNumericCastFromPrecision(constantExpression));
		}

		// Token: 0x06007600 RID: 30208 RVA: 0x0019654D File Offset: 0x0019474D
		private Func<IInvocationExpression, SqlExpression> CreateOperatorWithOptionalPrecision(BinaryScalarOperator op)
		{
			return delegate(IInvocationExpression invocation)
			{
				SqlExpression sqlExpression = this.CreateScalarExpression(invocation.Arguments[0]);
				SqlExpression sqlExpression2 = this.CreateScalarExpression(invocation.Arguments[1]);
				Value value = Value.Null;
				if (invocation.Arguments.Count == 3)
				{
					value = ((IConstantExpression)invocation.Arguments[2]).Value;
				}
				if (value.IsNull)
				{
					BinaryOperator2 binaryOperator;
					switch (op)
					{
					case BinaryScalarOperator.Add:
						binaryOperator = BinaryOperator2.Add;
						goto IL_009E;
					case BinaryScalarOperator.Subtract:
						binaryOperator = BinaryOperator2.Subtract;
						goto IL_009E;
					case BinaryScalarOperator.Multiply:
						binaryOperator = BinaryOperator2.Multiply;
						goto IL_009E;
					case BinaryScalarOperator.Divide:
						binaryOperator = BinaryOperator2.Divide;
						goto IL_009E;
					}
					throw new InvalidOperationException();
					IL_009E:
					this.VisitBinary(BinaryExpressionSyntaxNode.New(binaryOperator, invocation.Arguments[0], invocation.Arguments[1], TokenRange.Null));
					return this.returnValue;
				}
				Func<SqlExpression, SqlExpression> func = this.CreateNumericCastFromPrecision(new ConstantExpressionSyntaxNode(value));
				sqlExpression = func(sqlExpression);
				sqlExpression2 = func(sqlExpression2);
				return new BinaryScalarOperation(sqlExpression, op, sqlExpression2);
			};
		}

		// Token: 0x06007601 RID: 30209 RVA: 0x00196570 File Offset: 0x00194770
		private SqlExpression CreateTopExpression(IExpression expression, int topExpression)
		{
			PagingQuerySpecification pagingQuerySpecification = (PagingQuerySpecification)this.CreateQueryExpression(expression);
			if (pagingQuerySpecification.PagingClause != null)
			{
				pagingQuerySpecification.PagingClause = new PagingClause
				{
					FetchExpression = new long?(Math.Min((long)topExpression, pagingQuerySpecification.PagingClause.FetchExpression.GetValueOrDefault(2147483647L))),
					OffsetExpression = pagingQuerySpecification.PagingClause.OffsetExpression
				};
			}
			else
			{
				pagingQuerySpecification.PagingClause = new PagingClause
				{
					FetchExpression = new long?((long)topExpression)
				};
			}
			return pagingQuerySpecification;
		}

		// Token: 0x06007602 RID: 30210 RVA: 0x001965F4 File Offset: 0x001947F4
		private static TypeValue EnsureElementType(TypeValue type)
		{
			if (type.TypeKind != ValueKind.Table)
			{
				return type;
			}
			return type.AsTableType.ItemType;
		}

		// Token: 0x06007603 RID: 30211 RVA: 0x0019660D File Offset: 0x0019480D
		protected SqlExpression GetValue(IExpression node)
		{
			return this.GetValue(node, base.GetType(node));
		}

		// Token: 0x06007604 RID: 30212 RVA: 0x0019661D File Offset: 0x0019481D
		private SqlExpression GetValue(IExpression node, TypeValue type)
		{
			this.SetReturnValueType(type);
			base.Visit(node);
			return this.returnValue;
		}

		// Token: 0x06007605 RID: 30213 RVA: 0x00196634 File Offset: 0x00194834
		protected virtual SelectItem MitigateColumn(string name, TypeValue type, ref bool mitigateColumns)
		{
			SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference xcolumnReference = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.VirtualTable, Alias.NewNativeAlias(name));
			xcolumnReference;
			if (TypeServices.IsSerializedText(type) || TypeServices.IsGuid(type))
			{
				mitigateColumns = true;
				return new SelectItem(this.Convert(SqlDataType.NVarChar, xcolumnReference), Alias.NewAssignedAlias(name, this.dbEnvironment.SqlSettings));
			}
			return new SelectItem(xcolumnReference, Alias.NewAssignedAlias(name, this.dbEnvironment.SqlSettings));
		}

		// Token: 0x06007606 RID: 30214 RVA: 0x001966B0 File Offset: 0x001948B0
		private SqlExpression OptimizeMemberAccessExpression(IExpression expression)
		{
			IFieldAccessExpression fieldAccessExpression = EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(expression) as IFieldAccessExpression;
			if (fieldAccessExpression != null)
			{
				return this.OptimizeMemberAccessToScalarVariableField(fieldAccessExpression);
			}
			return null;
		}

		// Token: 0x06007607 RID: 30215 RVA: 0x001966D8 File Offset: 0x001948D8
		private SqlExpression OptimizeMemberAccessToScalarVariableField(IFieldAccessExpression expression)
		{
			if (expression.Expression.Kind != ExpressionKind.Identifier)
			{
				return null;
			}
			IIdentifierExpression identifierExpression = (IIdentifierExpression)expression.Expression;
			DbAstCreator.SqlVariable value = base.Environment.GetValue(identifierExpression.Name, identifierExpression.IsInclusive);
			if (value.Kind != DbAstCreator.SqlVariableKind.Row)
			{
				return null;
			}
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(value.TableAlias, expression.MemberName, this.dbEnvironment.SqlSettings);
		}

		// Token: 0x06007608 RID: 30216 RVA: 0x0019674A File Offset: 0x0019494A
		private void PopFunction(IFunctionExpression function)
		{
			base.ExitScope(function);
			base.Cursor.Pop();
		}

		// Token: 0x06007609 RID: 30217 RVA: 0x00196760 File Offset: 0x00194960
		private void PushFunction(IFunctionExpression function, FunctionTypeValue functionType, params DbAstCreator.SqlVariable[] parameterBindings)
		{
			TypeValue[] array = new TypeValue[functionType.ParameterCount];
			for (int i = 0; i < functionType.ParameterCount; i++)
			{
				array[i] = functionType.ParameterType(i);
			}
			base.Cursor.Push(function, array);
			base.EnterScope(function, parameterBindings);
		}

		// Token: 0x0600760A RID: 30218 RVA: 0x001967AC File Offset: 0x001949AC
		protected SqlAstCreatorBase<DbAstCreator.SqlVariable>.XSelectExpression SelectAllColumns(Alias alias, TypeValue type)
		{
			type = DbAstCreator.EnsureElementType(type);
			if (type.TypeKind == ValueKind.Record)
			{
				return base.SelectStar(type.AsRecordType.Fields.Keys.Select((string f) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(alias, f, this.dbEnvironment.SqlSettings)));
			}
			return base.SelectStar(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(alias, Alias.ScalarColumn) });
		}

		// Token: 0x0600760B RID: 30219 RVA: 0x0019682A File Offset: 0x00194A2A
		private void SetReturnValueType(TypeValue type)
		{
			this.returnValueType = type;
		}

		// Token: 0x0600760C RID: 30220 RVA: 0x00196833 File Offset: 0x00194A33
		private void SetReturnValue(SqlExpression value)
		{
			this.returnValue = value;
		}

		// Token: 0x0600760D RID: 30221 RVA: 0x0019683C File Offset: 0x00194A3C
		protected override IExpression VisitBinary(IBinaryExpression binary)
		{
			switch (binary.Operator)
			{
			case BinaryOperator2.Add:
				this.SetReturnValue(this.CreateAddOperation(binary));
				return binary;
			case BinaryOperator2.Subtract:
				this.SetReturnValue(this.CreateSubtractOperation(binary));
				return binary;
			case BinaryOperator2.Multiply:
				this.SetReturnValue(this.CreateBinaryScalarOperation(BinaryScalarOperator.Multiply, binary));
				return binary;
			case BinaryOperator2.Divide:
				this.SetReturnValue(this.CreateDivideOperation(binary));
				return binary;
			case BinaryOperator2.GreaterThan:
				this.SetReturnValue(this.CreateLogicalOperation(BinaryLogicalOperator.GreaterThan, binary));
				return binary;
			case BinaryOperator2.LessThan:
				this.SetReturnValue(this.CreateLogicalOperation(BinaryLogicalOperator.LessThan, binary));
				return binary;
			case BinaryOperator2.GreaterThanOrEquals:
				this.SetReturnValue(this.CreateLogicalOperation(BinaryLogicalOperator.GreaterThanOrEqual, binary));
				return binary;
			case BinaryOperator2.LessThanOrEquals:
				this.SetReturnValue(this.CreateLogicalOperation(BinaryLogicalOperator.LessThanOrEqual, binary));
				return binary;
			case BinaryOperator2.Equals:
				this.SetReturnValue(this.CreateEqualScalar(binary, false, null));
				return binary;
			case BinaryOperator2.NotEquals:
				this.SetReturnValue(this.CreateNotEqualScalar(binary));
				return binary;
			case BinaryOperator2.And:
			{
				Condition condition = this.CreateConditionExpression(binary.Left);
				Condition condition2 = this.CreateConditionExpression(binary.Right);
				if (condition == this.Condition(false) || condition2 == this.Condition(false))
				{
					this.SetReturnValue(this.Condition(false));
					return binary;
				}
				if (condition == this.Condition(true))
				{
					this.SetReturnValue(condition2);
					return binary;
				}
				if (condition2 == this.Condition(true))
				{
					this.SetReturnValue(condition);
					return binary;
				}
				this.SetReturnValue(SqlAstCreatorBase<DbAstCreator.SqlVariable>.And(new Condition[] { condition, condition2 }));
				return binary;
			}
			case BinaryOperator2.Or:
			{
				Condition condition = this.CreateConditionExpression(binary.Left);
				Condition condition2 = this.CreateConditionExpression(binary.Right);
				if (condition == this.Condition(true) || condition2 == this.Condition(true))
				{
					this.SetReturnValue(this.Condition(true));
					return binary;
				}
				if (condition == this.Condition(false))
				{
					this.SetReturnValue(condition2);
					return binary;
				}
				if (condition2 == this.Condition(false))
				{
					this.SetReturnValue(condition);
					return binary;
				}
				this.SetReturnValue(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Or(new Condition[] { condition, condition2 }));
				return binary;
			}
			case BinaryOperator2.Concatenate:
				this.SetReturnValue(this.CreateConcatenateOperation(binary));
				return binary;
			case BinaryOperator2.As:
				return this.VisitExpression(binary.Left);
			case BinaryOperator2.Coalesce:
				this.SetReturnValue(this.CreateCoalesceOperation(binary));
				return binary;
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600760E RID: 30222 RVA: 0x00196A9C File Offset: 0x00194C9C
		protected override IExpression VisitConstant(IConstantExpression constant)
		{
			constant = (IConstantExpression)base.VisitConstant(constant);
			QueryResultTableValue queryResultTableValue = constant.Value as QueryResultTableValue;
			if (queryResultTableValue != null)
			{
				RecordTypeValue recordType = queryResultTableValue.Type.AsTableType.ItemType;
				bool mitigatedColumns = false;
				IList<SelectItem> list = recordType.Fields.Keys.Select((string o) => this.MitigateColumn(o, RecordTypeAlgebra.Field(recordType, o), ref mitigatedColumns)).ToList<SelectItem>();
				if (!mitigatedColumns)
				{
					if (!list.Any((SelectItem i) => i.Alias.IsMitigated))
					{
						this.SetReturnValue(base.SelectStar(list).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
						{
							this.GetTableReference(queryResultTableValue),
							SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(Alias.VirtualTable)
						}).ToPagingQuerySpecification());
						return constant;
					}
				}
				this.SetReturnValue(base.Select(list).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
				{
					this.GetTableReference(queryResultTableValue),
					SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(Alias.VirtualTable)
				}).ToPagingQuerySpecification());
			}
			else
			{
				this.SetReturnValue(this.Constant(constant.Value, this.returnValueType));
			}
			return constant;
		}

		// Token: 0x0600760F RID: 30223 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override IDocument VisitExpressionDocument(IExpressionDocument document)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007610 RID: 30224 RVA: 0x00196C00 File Offset: 0x00194E00
		protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
		{
			ColumnReference columnReference;
			if (this.TryGetGroupByKeyColumnAccess(fieldAccess, out columnReference))
			{
				this.SetReturnValue(columnReference);
				return fieldAccess;
			}
			SqlExpression sqlExpression = this.OptimizeMemberAccessExpression(fieldAccess);
			if (sqlExpression != null)
			{
				this.AdjustFieldAccessForType(base.GetType(fieldAccess), ref sqlExpression);
				this.SetReturnValue(sqlExpression);
				return fieldAccess;
			}
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(fieldAccess.Expression);
			this.SetReturnValue(base.Select(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(fieldAccess.MemberName, this.dbEnvironment.SqlSettings), Alias.ScalarColumn).From(sqlQueryExpression).ToPagingQuerySpecification());
			return fieldAccess;
		}

		// Token: 0x06007611 RID: 30225 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override IExpression VisitFunction(IFunctionExpression function)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007612 RID: 30226 RVA: 0x00196C98 File Offset: 0x00194E98
		protected override IExpression VisitIdentifier(IIdentifierExpression identifier)
		{
			DbAstCreator.SqlVariable variable = base.Environment.GetValue(identifier.Name, identifier.IsInclusive);
			DbAstCreator.SqlVariableKind kind = variable.Kind;
			if (kind != DbAstCreator.SqlVariableKind.InlineExpression)
			{
				if (kind == DbAstCreator.SqlVariableKind.Row)
				{
					TypeValue type = base.GetType(identifier);
					TypeValue typeValue = type;
					if (type.TypeKind == ValueKind.Table)
					{
						typeValue = type.AsTableType.ItemType;
					}
					if (typeValue.TypeKind == ValueKind.Record)
					{
						IEnumerable<SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference> enumerable = typeValue.AsRecordType.Fields.Keys.Select((string key) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(variable.TableAlias, key, this.dbEnvironment.SqlSettings));
						this.SetReturnValue(base.Select(enumerable).ToPagingQuerySpecification());
					}
					else
					{
						this.SetReturnValue(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(variable.TableAlias, Alias.ScalarColumn));
					}
				}
			}
			else
			{
				this.SetReturnValue(this.GetValue(variable.Expression));
			}
			return identifier;
		}

		// Token: 0x06007613 RID: 30227 RVA: 0x00196D8C File Offset: 0x00194F8C
		protected override IExpression VisitIf(IIfExpression @if)
		{
			DbAstCreator.CaseBuilder caseBuilder = new DbAstCreator.CaseBuilder(this);
			this.VisitIfRecursive(@if, caseBuilder);
			this.SetReturnValue(caseBuilder.CreateExpression());
			return @if;
		}

		// Token: 0x06007614 RID: 30228 RVA: 0x00196DB8 File Offset: 0x00194FB8
		private void VisitIfRecursive(IIfExpression @if, DbAstCreator.CaseBuilder builder)
		{
			TypeValue type = base.GetType(@if.TrueCase);
			TypeValue type2 = base.GetType(@if.FalseCase);
			if (type.TypeKind == ValueKind.Null && type2.TypeKind == ValueKind.Null)
			{
				builder.AddDefault(SqlConstant.Null, type);
				return;
			}
			Condition condition = this.CreateConditionExpression(@if.Condition);
			SqlExpression sqlExpression = this.CreateScalarExpression(@if.TrueCase);
			builder.AddCondition(condition, sqlExpression, type);
			IExpression expression = EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(@if.FalseCase);
			if (expression.Kind == ExpressionKind.If)
			{
				this.VisitIfRecursive((IIfExpression)expression, builder);
				return;
			}
			builder.AddDefault(this.CreateScalarExpression(@if.FalseCase), type2);
		}

		// Token: 0x06007615 RID: 30229 RVA: 0x00196E58 File Offset: 0x00195058
		protected override IExpression VisitInvocation(IInvocationExpression invocation)
		{
			Value value = ((IConstantExpression)invocation.Function).Value;
			Func<IInvocationExpression, SqlExpression> func;
			if (value.IsFunction && this.FunctionLookup.TryGetValue(value.AsFunction, out func))
			{
				this.SetReturnValue(func(invocation));
			}
			else
			{
				if (!(value is QueryResultFunctionValue))
				{
					throw new InvalidOperationException(value.GetUnderlyingClrType().FullName);
				}
				FunctionTypeValue asFunctionType = ((QueryResultFunctionValue)value).Type.AsFunctionType;
				Value value2 = asFunctionType.MetaValue["Sql.Schema"];
				Value value3 = asFunctionType.MetaValue["Sql.Table"];
				SqlExpression[] array = new SqlExpression[invocation.Arguments.Count];
				for (int i = 0; i < invocation.Arguments.Count; i++)
				{
					ValueKind typeKind = asFunctionType.ParameterType(i).TypeKind;
					if (typeKind == ValueKind.Record || typeKind == ValueKind.Table || typeKind == ValueKind.Any || typeKind == ValueKind.List)
					{
						throw DataSourceException.NewDataSourceError<Message0>(this.dbEnvironment.Host, Strings.UnsupportedFunctionParameterType, this.dbEnvironment.Resource, null, null);
					}
					array[i] = this.GetValue(invocation.Arguments[i], asFunctionType.ParameterType(i));
				}
				StoredFunctionReference storedFunctionReference = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<StoredFunctionReference>(new StoredFunctionReference(value2.IsNull ? null : Alias.NewNativeAlias(value2.AsString), Alias.NewNativeAlias(value3.AsString)), array);
				TypeValue typeValue = asFunctionType.ReturnType;
				if (TypeServices.IsScalar(typeValue))
				{
					this.SetReturnValue(storedFunctionReference);
				}
				else
				{
					if (typeValue.TypeKind == ValueKind.Table)
					{
						typeValue = typeValue.AsTableType.ItemType;
					}
					this.SetReturnValue(base.Select(typeValue.AsRecordType.Fields.Keys.Select((string key) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(Alias.VirtualTable, key, this.dbEnvironment.SqlSettings))).From(new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromItem[]
					{
						storedFunctionReference,
						SqlAstCreatorBase<DbAstCreator.SqlVariable>.As(Alias.VirtualTable)
					}).ToPagingQuerySpecification());
				}
			}
			return invocation;
		}

		// Token: 0x06007616 RID: 30230 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override IExpression VisitLet(ILetExpression let)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06007617 RID: 30231 RVA: 0x00197060 File Offset: 0x00195260
		protected override IExpression VisitMultiFieldRecordProjection(IMultiFieldRecordProjectionExpression multiFieldRecordProjection)
		{
			SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(multiFieldRecordProjection.Expression);
			PagingQuerySpecification pagingQuerySpecification = sqlQueryExpression as PagingQuerySpecification;
			this.FixInaccessibleSortKeys(pagingQuerySpecification);
			IEnumerable<SqlAstCreatorBase<DbAstCreator.SqlVariable>.XColumnReference> enumerable = multiFieldRecordProjection.MemberNames.Select((Identifier m) => SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(m, this.dbEnvironment.SqlSettings));
			SqlAstCreatorBase<DbAstCreator.SqlVariable>.XFromExpression xfromExpression = base.Select(enumerable).From(sqlQueryExpression);
			if (((pagingQuerySpecification != null) ? pagingQuerySpecification.OrderByClause : null) == null)
			{
				this.SetReturnValue(xfromExpression.ToPagingQuerySpecification());
			}
			else
			{
				this.SetReturnValue(xfromExpression.OrderBy(pagingQuerySpecification.OrderByClause).ToPagingQuerySpecification());
			}
			return multiFieldRecordProjection;
		}

		// Token: 0x06007618 RID: 30232 RVA: 0x001970EC File Offset: 0x001952EC
		protected override IExpression VisitRecord(IRecordExpression record)
		{
			base.EnterScope(record.Members, record.Members.Select((VariableInitializer m) => DbAstCreator.SqlVariable.CreateInlineExpression(m.Value)).ToList<DbAstCreator.SqlVariable>());
			List<SelectItem> list = new List<SelectItem>(record.Members.Count);
			foreach (VariableInitializer variableInitializer in record.Members)
			{
				base.Cursor.Push(record, variableInitializer.Name);
				list.Add(new SelectItem(this.CreateScalarExpression(variableInitializer.Value), Alias.NewAssignedAlias(variableInitializer.Name.Name, this.dbEnvironment.SqlSettings)));
				base.Cursor.Pop();
			}
			base.ExitScope(record.Members);
			this.SetReturnValue(base.Select(list).ToPagingQuerySpecification());
			return record;
		}

		// Token: 0x06007619 RID: 30233 RVA: 0x001971F4 File Offset: 0x001953F4
		protected override IExpression VisitUnary(IUnaryExpression unary)
		{
			switch (unary.Operator)
			{
			case UnaryOperator2.Not:
				this.SetReturnValue(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Not(this.CreateConditionExpression(unary.Expression)));
				break;
			case UnaryOperator2.Negative:
				this.SetReturnValue(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Minus(this.CreateScalarExpression(unary.Expression)));
				break;
			case UnaryOperator2.Positive:
				this.SetReturnValue(this.CreateScalarExpression(unary.Expression));
				break;
			default:
				throw new InvalidOperationException("Unexpected unary operator " + unary.Operator.ToString());
			}
			return unary;
		}

		// Token: 0x0600761A RID: 30234 RVA: 0x00197288 File Offset: 0x00195488
		protected SqlStatement CreateBind(IInvocationExpression invocation)
		{
			bool flag = this.countOnly;
			this.countOnly = true;
			if (this.returnCountOnly == null)
			{
				this.returnCountOnly = new bool?(true);
			}
			SqlStatement sqlStatement = this.CreateStatement(invocation.Arguments[0]);
			this.countOnly = flag;
			return sqlStatement;
		}

		// Token: 0x0600761B RID: 30235 RVA: 0x001972D8 File Offset: 0x001954D8
		protected virtual SqlStatement CreateInsertRows(IInvocationExpression invocation)
		{
			QueryResultTableValue queryResultTableValue;
			IFunctionExpression functionExpression;
			if (DbAstExpressionChecker.TryGetTargetTable(EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(invocation.Arguments[0]), out queryResultTableValue, out functionExpression) && functionExpression == null)
			{
				IList<IExpression> list;
				if (DbAstExpressionChecker.TryGetInvocation(invocation.Arguments[1], StatementBuilder.BatchedRows, 1, out list) && list[0].Kind == ExpressionKind.Constant)
				{
					TableValue asTable = ((IConstantExpression)list[0]).Value.AsTable;
					List<ColumnReference> list2 = asTable.Columns.Select((string c) => new ColumnReference(Alias.NewNativeAlias(c))).ToList<ColumnReference>();
					List<IList<ScalarExpression>> list3 = new List<IList<ScalarExpression>>();
					foreach (IValueReference valueReference in asTable)
					{
						RecordValue asRecord = valueReference.Value.AsRecord;
						ScalarExpression[] array = new ScalarExpression[list2.Count];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = this.GetInsertValue(asRecord[i], asTable.GetColumnType(i));
						}
						list3.Add(array);
					}
					return this.CreateInsertStatement(this.GetTableReference(queryResultTableValue), Alias.Inserted, queryResultTableValue.Type.AsTableType, list2, list3);
				}
				SqlQueryExpression sqlQueryExpression = this.CreateQueryExpression(invocation.Arguments[1]);
				QuerySpecification querySpecification = sqlQueryExpression as QuerySpecification;
				if (querySpecification == null)
				{
					DbValueBuilder dbValueBuilder = queryResultTableValue.ValueBuilder as DbValueBuilder;
					querySpecification = ((dbValueBuilder != null) ? dbValueBuilder.DbQueryPlan.Query : null) as QuerySpecification;
				}
				if (querySpecification != null)
				{
					List<ColumnReference> list4 = querySpecification.SelectItems.Select((SelectItem s) => new ColumnReference(s.Alias ?? s.Name)).ToList<ColumnReference>();
					return this.CreateInsertStatement(this.GetTableReference(queryResultTableValue), Alias.Inserted, queryResultTableValue.Type.AsTableType, list4, sqlQueryExpression);
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600761C RID: 30236 RVA: 0x001974D8 File Offset: 0x001956D8
		protected virtual SqlStatement CreateUpdateRows(IInvocationExpression invocation)
		{
			QueryResultTableValue queryResultTableValue;
			IFunctionExpression functionExpression;
			if (DbAstExpressionChecker.TryGetTargetTable(EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(invocation.Arguments[0]), out queryResultTableValue, out functionExpression))
			{
				List<SqlColumnUpdate> list = new List<SqlColumnUpdate>();
				IListExpression listExpression = (IListExpression)EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(invocation.Arguments[1]);
				TableTypeValue asTableType = queryResultTableValue.Type.AsTableType;
				foreach (IExpression expression in listExpression.Members)
				{
					IListExpression listExpression2 = (IListExpression)expression;
					IConstantExpression constantExpression = (IConstantExpression)listExpression2.Members[0];
					IFunctionExpression functionExpression2 = (IFunctionExpression)listExpression2.Members[1];
					FunctionTypeValue asFunctionType = base.GetType(functionExpression2).AsFunctionType;
					this.PushFunction(functionExpression2, asFunctionType, new DbAstCreator.SqlVariable[] { DbAstCreator.SqlVariable.CreateRow(null) });
					string asString = constantExpression.Value.AsString;
					TypeValue nonNullable = asTableType.ItemType.Fields[asString]["Type"].AsType.NonNullable;
					SqlExpression value = this.GetValue(functionExpression2.Expression, nonNullable);
					this.PopFunction(functionExpression2);
					list.Add(new SqlColumnUpdate(new ColumnReference(Alias.NewNativeAlias(asString)), value));
				}
				return this.CreateUpdateStatement(this.GetTableReference(queryResultTableValue), Alias.Inserted, asTableType, list, this.GetWhereClause(functionExpression));
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600761D RID: 30237 RVA: 0x0019764C File Offset: 0x0019584C
		protected virtual SqlStatement CreateDeleteRows(IInvocationExpression invocation)
		{
			QueryResultTableValue queryResultTableValue;
			IFunctionExpression functionExpression;
			if (DbAstExpressionChecker.TryGetTargetTable(EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(invocation.Arguments[0]), out queryResultTableValue, out functionExpression))
			{
				return this.CreateDeleteStatement(this.GetTableReference(queryResultTableValue), Alias.Deleted, queryResultTableValue.Type.AsTableType, this.GetWhereClause(functionExpression));
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600761E RID: 30238 RVA: 0x001976A0 File Offset: 0x001958A0
		private TableReference GetTableReference(QueryResultTableValue tableQuery)
		{
			Value value = tableQuery.Type.MetaValue["Sql.Schema"];
			Value value2 = tableQuery.Type.MetaValue["Sql.Table"];
			Alias alias = null;
			DbEnvironment dbEnvironment = tableQuery.Environment as DbEnvironment;
			if (dbEnvironment != null && dbEnvironment.UserOptions.GetBool("EnableCrossDatabaseFolding", false))
			{
				alias = Alias.NewNativeAlias(dbEnvironment.Database);
			}
			return new TableReference((!value.IsNull) ? Alias.NewNativeAlias(value.AsString) : null, Alias.NewNativeAlias(value2.AsString), alias);
		}

		// Token: 0x0600761F RID: 30239 RVA: 0x00197730 File Offset: 0x00195930
		private Condition GetWhereClause(IFunctionExpression predicate)
		{
			Condition condition = null;
			if (predicate != null)
			{
				FunctionTypeValue asFunctionType = base.GetType(predicate).AsFunctionType;
				this.PushFunction(predicate, asFunctionType, new DbAstCreator.SqlVariable[] { DbAstCreator.SqlVariable.CreateRow(null) });
				condition = this.CreateConditionExpression(predicate.Expression);
				this.PopFunction(predicate);
			}
			return condition;
		}

		// Token: 0x06007620 RID: 30240 RVA: 0x0019777B File Offset: 0x0019597B
		protected virtual SqlExpression CreateExecuteStoredProcedureArgument(List<SqlStatement> statements, int argumentIndex, SqlDataType argumentType, SqlExpression argument)
		{
			return argument;
		}

		// Token: 0x06007621 RID: 30241 RVA: 0x0019777F File Offset: 0x0019597F
		public static bool TryAsBinaryExpression(IExpression expression, out IBinaryExpression binaryExpression)
		{
			if (expression.Kind == ExpressionKind.Binary)
			{
				binaryExpression = (IBinaryExpression)expression;
				return true;
			}
			binaryExpression = null;
			return false;
		}

		// Token: 0x06007622 RID: 30242 RVA: 0x00197798 File Offset: 0x00195998
		protected virtual SqlStatement CreateExecuteStoredProcedure(IInvocationExpression invocation)
		{
			FunctionValue functionValue;
			if (DbAstExpressionChecker.TryGetConstantFunction(invocation, out functionValue))
			{
				FunctionTypeValue asFunctionType = functionValue.Type.AsFunctionType;
				Value value = asFunctionType.MetaValue["Sql.Schema"];
				Value value2 = asFunctionType.MetaValue["Sql.Table"];
				List<SqlStatement> list = new List<SqlStatement>();
				SqlExpression[] array = new SqlExpression[invocation.Arguments.Count];
				for (int i = 0; i < invocation.Arguments.Count; i++)
				{
					ValueKind typeKind = asFunctionType.ParameterType(i).TypeKind;
					if (typeKind == ValueKind.Record || typeKind == ValueKind.Table || typeKind == ValueKind.Any || typeKind == ValueKind.List)
					{
						throw DataSourceException.NewDataSourceError<Message0>(this.dbEnvironment.Host, Strings.UnsupportedFunctionParameterType, this.dbEnvironment.Resource, null, null);
					}
					TypeValue typeValue = asFunctionType.ParameterType(i);
					SqlExpression value3 = this.GetValue(invocation.Arguments[i], typeValue);
					array[i] = this.CreateExecuteStoredProcedureArgument(list, i, this.dbEnvironment.GetSqlScalarType(typeValue), value3);
				}
				StoredProcedureReference storedProcedureReference = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<StoredProcedureReference>(new StoredProcedureReference(value.IsNull ? null : Alias.NewNativeAlias(value.AsString), Alias.NewNativeAlias(value2.AsString)), array);
				list.Add(this.CreateExecuteStoredProcedure(storedProcedureReference));
				return new SqlStatementList(list);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06007623 RID: 30243 RVA: 0x001978EC File Offset: 0x00195AEC
		protected virtual SqlStatement CreateExecuteStoredProcedure(StoredProcedureReference procedureCall)
		{
			return new SqlExecuteStoredProcedureStatement(procedureCall);
		}

		// Token: 0x06007624 RID: 30244 RVA: 0x001978F4 File Offset: 0x00195AF4
		protected virtual OutputClause CreateOutputClause(Alias alias, TableTypeValue tableType)
		{
			return OutputClause.Null;
		}

		// Token: 0x06007625 RID: 30245 RVA: 0x001978FB File Offset: 0x00195AFB
		protected virtual SqlStatement CreateInsertStatement(TableReference table, Alias alias, TableTypeValue tableType, List<ColumnReference> columnList, List<IList<ScalarExpression>> values)
		{
			return new SqlInsertStatement(table, values, this.CreateOutputClause(alias, tableType), columnList);
		}

		// Token: 0x06007626 RID: 30246 RVA: 0x0019790F File Offset: 0x00195B0F
		protected virtual SqlStatement CreateInsertStatement(TableReference table, Alias alias, TableTypeValue tableType, List<ColumnReference> columnList, SqlQueryExpression sourceQuery)
		{
			return new SqlInsertStatement(table, sourceQuery, this.CreateOutputClause(alias, tableType), columnList);
		}

		// Token: 0x06007627 RID: 30247 RVA: 0x00197923 File Offset: 0x00195B23
		protected virtual SqlStatement CreateUpdateStatement(TableReference table, Alias alias, TableTypeValue tableType, List<SqlColumnUpdate> updates, Condition whereClause)
		{
			return new SqlUpdateStatement(table, updates, this.CreateOutputClause(alias, tableType), whereClause);
		}

		// Token: 0x06007628 RID: 30248 RVA: 0x00197937 File Offset: 0x00195B37
		protected virtual SqlStatement CreateDeleteStatement(TableReference table, Alias alias, TableTypeValue tableType, Condition whereClause)
		{
			return new SqlDeleteStatement(table, this.CreateOutputClause(alias, tableType), whereClause);
		}

		// Token: 0x06007629 RID: 30249 RVA: 0x0019794C File Offset: 0x00195B4C
		private bool TryGetGroupByColumnAccess(IExpression list, out ColumnReference keyColumnReference)
		{
			IFieldAccessExpression fieldAccessExpression = EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(list) as IFieldAccessExpression;
			if (fieldAccessExpression != null)
			{
				IIdentifierExpression identifierExpression = EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(fieldAccessExpression.Expression) as IIdentifierExpression;
				if (identifierExpression != null)
				{
					DbAstCreator.SqlVariable value = base.Environment.GetValue(identifierExpression.Name, identifierExpression.IsInclusive);
					if (value.Kind == DbAstCreator.SqlVariableKind.Group)
					{
						keyColumnReference = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(value.TableAlias, fieldAccessExpression.MemberName, this.dbEnvironment.SqlSettings);
						return true;
					}
				}
			}
			else
			{
				IIdentifierExpression identifierExpression2 = EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(list) as IIdentifierExpression;
				if (identifierExpression2 != null)
				{
					DbAstCreator.SqlVariable value2 = base.Environment.GetValue(identifierExpression2.Name, identifierExpression2.IsInclusive);
					if (value2.Kind == DbAstCreator.SqlVariableKind.Group)
					{
						keyColumnReference = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(value2.TableAlias, Alias.ScalarColumn);
						return true;
					}
				}
			}
			keyColumnReference = null;
			return false;
		}

		// Token: 0x0600762A RID: 30250 RVA: 0x00197A18 File Offset: 0x00195C18
		private bool TryGetGroupByKeyColumnAccess(IFieldAccessExpression fieldAccess, out ColumnReference keyColumnReference)
		{
			IInvocationExpression invocationExpression = EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(fieldAccess.Expression) as IInvocationExpression;
			if (invocationExpression != null)
			{
				IConstantExpression constantExpression = invocationExpression.Function as IConstantExpression;
				if (constantExpression != null && constantExpression.Value.Equals(TableModule.Table.First))
				{
					IIdentifierExpression identifierExpression = EnvironmentAstVisitor<DbAstCreator.SqlVariable>.Reduce(invocationExpression.Arguments[0]) as IIdentifierExpression;
					if (identifierExpression != null)
					{
						DbAstCreator.SqlVariable value = base.Environment.GetValue(identifierExpression.Name, identifierExpression.IsInclusive);
						if (value.Kind == DbAstCreator.SqlVariableKind.Group)
						{
							keyColumnReference = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Column(value.TableAlias, fieldAccess.MemberName, this.dbEnvironment.SqlSettings);
							return true;
						}
					}
				}
			}
			keyColumnReference = null;
			return false;
		}

		// Token: 0x0600762B RID: 30251 RVA: 0x00197AC8 File Offset: 0x00195CC8
		private ScalarExpression GetInsertValue(Value value, TypeValue columnType)
		{
			ValueKind kind = value.Kind;
			if (kind == ValueKind.Null)
			{
				return SqlConstant.Null;
			}
			if (kind != ValueKind.Logical)
			{
				return this.Constant(value, columnType);
			}
			return this.Constant(value.AsBoolean);
		}

		// Token: 0x04004077 RID: 16503
		private static readonly BinaryLogicalOperation TrueCondition = new BinaryLogicalOperation(SqlConstant.NumericTrue, BinaryLogicalOperator.Equals, SqlConstant.One);

		// Token: 0x04004078 RID: 16504
		private static readonly BinaryLogicalOperation FalseCondition = new BinaryLogicalOperation(SqlConstant.NumericFalse, BinaryLogicalOperator.Equals, SqlConstant.One);

		// Token: 0x04004079 RID: 16505
		private static readonly DateTime baseOADateTime = new DateTime(1899, 12, 30);

		// Token: 0x0400407A RID: 16506
		private Dictionary<FunctionValue, Func<IInvocationExpression, SqlExpression>> functionLookup;

		// Token: 0x0400407B RID: 16507
		private Dictionary<FunctionValue, Func<IInvocationExpression, SqlStatement>> statementFunctionLookup;

		// Token: 0x0400407C RID: 16508
		private TypeValue returnValueType;

		// Token: 0x0400407D RID: 16509
		private SqlExpression returnValue;

		// Token: 0x0400407E RID: 16510
		private DbAstCreator.GroupVariables groupVariables;

		// Token: 0x0400407F RID: 16511
		protected bool countOnly;

		// Token: 0x04004080 RID: 16512
		private bool? returnCountOnly;

		// Token: 0x0200117B RID: 4475
		public enum SqlVariableKind
		{
			// Token: 0x04004082 RID: 16514
			Group,
			// Token: 0x04004083 RID: 16515
			InlineExpression,
			// Token: 0x04004084 RID: 16516
			Row
		}

		// Token: 0x0200117C RID: 4476
		public class SqlVariable
		{
			// Token: 0x170020A5 RID: 8357
			// (get) Token: 0x06007634 RID: 30260 RVA: 0x00197B9F File Offset: 0x00195D9F
			public IExpression Expression
			{
				get
				{
					return (IExpression)this.value;
				}
			}

			// Token: 0x170020A6 RID: 8358
			// (get) Token: 0x06007635 RID: 30261 RVA: 0x00197BAC File Offset: 0x00195DAC
			// (set) Token: 0x06007636 RID: 30262 RVA: 0x00197BB4 File Offset: 0x00195DB4
			public DbAstCreator.SqlVariableKind Kind { get; private set; }

			// Token: 0x170020A7 RID: 8359
			// (get) Token: 0x06007637 RID: 30263 RVA: 0x00197BBD File Offset: 0x00195DBD
			public Alias TableAlias
			{
				get
				{
					return (Alias)this.value;
				}
			}

			// Token: 0x06007638 RID: 30264 RVA: 0x00197BCA File Offset: 0x00195DCA
			public static DbAstCreator.SqlVariable CreateGroup(Alias tableAlias)
			{
				return new DbAstCreator.SqlVariable
				{
					value = tableAlias,
					Kind = DbAstCreator.SqlVariableKind.Group
				};
			}

			// Token: 0x06007639 RID: 30265 RVA: 0x00197BDF File Offset: 0x00195DDF
			public static DbAstCreator.SqlVariable CreateInlineExpression(IExpression expression)
			{
				return new DbAstCreator.SqlVariable
				{
					value = expression,
					Kind = DbAstCreator.SqlVariableKind.InlineExpression
				};
			}

			// Token: 0x0600763A RID: 30266 RVA: 0x00197BF4 File Offset: 0x00195DF4
			public static DbAstCreator.SqlVariable CreateRow(Alias tableAlias)
			{
				return new DbAstCreator.SqlVariable
				{
					value = tableAlias,
					Kind = DbAstCreator.SqlVariableKind.Row
				};
			}

			// Token: 0x04004085 RID: 16517
			private object value;
		}

		// Token: 0x0200117D RID: 4477
		private class GroupVariables
		{
			// Token: 0x0600763C RID: 30268 RVA: 0x00197C09 File Offset: 0x00195E09
			public GroupVariables(IList<SelectItem> reservedVariables)
			{
				this.reservedVariables = reservedVariables;
			}

			// Token: 0x170020A8 RID: 8360
			// (get) Token: 0x0600763D RID: 30269 RVA: 0x00197C18 File Offset: 0x00195E18
			public bool HasVariables
			{
				get
				{
					return this.variables != null;
				}
			}

			// Token: 0x170020A9 RID: 8361
			// (get) Token: 0x0600763E RID: 30270 RVA: 0x00197C23 File Offset: 0x00195E23
			public List<SelectItem> Variables
			{
				get
				{
					if (this.variables == null)
					{
						this.variables = new List<SelectItem>();
					}
					return this.variables;
				}
			}

			// Token: 0x0600763F RID: 30271 RVA: 0x00197C3E File Offset: 0x00195E3E
			public bool Contains(Alias name)
			{
				return DbAstCreator.GroupVariables.Contains(this.reservedVariables, name) || (this.variables != null && DbAstCreator.GroupVariables.Contains(this.variables, name));
			}

			// Token: 0x06007640 RID: 30272 RVA: 0x00197C68 File Offset: 0x00195E68
			private static bool Contains(IList<SelectItem> selectItems, Alias name)
			{
				for (int i = 0; i < selectItems.Count; i++)
				{
					if (name.Equals(selectItems[i].Name))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04004087 RID: 16519
			private IList<SelectItem> reservedVariables;

			// Token: 0x04004088 RID: 16520
			private List<SelectItem> variables;
		}

		// Token: 0x0200117E RID: 4478
		private sealed class CaseBuilder
		{
			// Token: 0x06007641 RID: 30273 RVA: 0x00197C9D File Offset: 0x00195E9D
			public CaseBuilder(DbAstCreator astCreator)
			{
				this.astCreator = astCreator;
				this.whenItems = new List<WhenItem>();
				this.types = new List<TypeValue>();
			}

			// Token: 0x06007642 RID: 30274 RVA: 0x00197CC2 File Offset: 0x00195EC2
			public void AddCondition(Condition condition, SqlExpression expression, TypeValue type)
			{
				this.whenItems.Add(new WhenItem
				{
					When = condition,
					Then = expression
				});
				this.types.Add(type);
			}

			// Token: 0x06007643 RID: 30275 RVA: 0x00197CEE File Offset: 0x00195EEE
			public void AddDefault(SqlExpression expression, TypeValue type)
			{
				this.defaultCase = expression;
				this.defaultType = type;
			}

			// Token: 0x06007644 RID: 30276 RVA: 0x00197D00 File Offset: 0x00195F00
			public SqlExpression CreateExpression()
			{
				this.types.Add(this.defaultType);
				SqlDataType[] array = this.astCreator.AdjustArgumentsForType(this.types.ToArray());
				if (array != null)
				{
					for (int i = 0; i < this.whenItems.Count; i++)
					{
						if (array[i] != null)
						{
							this.whenItems[i] = new WhenItem
							{
								When = this.whenItems[i].When,
								Then = this.astCreator.Convert(array[i], this.whenItems[i].Then)
							};
						}
					}
					if (array[this.whenItems.Count] != null)
					{
						this.defaultCase = this.astCreator.Convert(array[this.whenItems.Count], this.defaultCase);
					}
				}
				return new SqlAstCreatorBase<DbAstCreator.SqlVariable>.XCaseWhenThenElse(this.whenItems, this.defaultCase);
			}

			// Token: 0x04004089 RID: 16521
			private readonly DbAstCreator astCreator;

			// Token: 0x0400408A RID: 16522
			private readonly List<WhenItem> whenItems;

			// Token: 0x0400408B RID: 16523
			private readonly List<TypeValue> types;

			// Token: 0x0400408C RID: 16524
			private SqlExpression defaultCase;

			// Token: 0x0400408D RID: 16525
			private TypeValue defaultType;
		}
	}
}
