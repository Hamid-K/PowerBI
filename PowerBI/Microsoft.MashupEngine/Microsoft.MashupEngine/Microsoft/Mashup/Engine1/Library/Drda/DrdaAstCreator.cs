using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000C98 RID: 3224
	internal abstract class DrdaAstCreator : DbAstCreator
	{
		// Token: 0x06005710 RID: 22288 RVA: 0x0004FA88 File Offset: 0x0004DC88
		public DrdaAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x06005711 RID: 22289 RVA: 0x000020FA File Offset: 0x000002FA
		protected override SqlDataType[] AdjustArgumentsForType(TypeValue[] types)
		{
			return null;
		}

		// Token: 0x06005712 RID: 22290 RVA: 0x0012E558 File Offset: 0x0012C758
		protected override SqlExpression CreateAddOperation(IBinaryExpression add)
		{
			IExpression left = add.Left;
			if (base.GetType(left).TypeKind == ValueKind.Text)
			{
				return this.CreateBinaryScalarOperation(BinaryScalarOperator.Concatenate, add);
			}
			return this.CreateBinaryScalarOperation(BinaryScalarOperator.Add, add);
		}

		// Token: 0x06005713 RID: 22291 RVA: 0x0012E58C File Offset: 0x0012C78C
		protected override SqlExpression CreateListAverage(IInvocationExpression invocation)
		{
			IExpression expression = invocation.Arguments[0];
			TypeValue itemType = base.GetType(expression).AsListType.ItemType;
			SqlExpression sqlExpression;
			switch (itemType.TypeKind)
			{
			case ValueKind.Time:
			case ValueKind.DateTime:
				sqlExpression = DrdaAstCreator.DateTimeAggregateBase;
				break;
			case ValueKind.Date:
				sqlExpression = DrdaAstCreator.DateAggregateBase;
				break;
			case ValueKind.DateTimeZone:
				sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(DrdaAstCreator.BaseDateTime.AddDefaultTimeZone(base.ExternalEnvironment.Host));
				break;
			default:
				return base.CreateListAverage(invocation);
			}
			SqlExpression sqlExpression2 = base.CreateListAggregateInput(expression);
			if (itemType.TypeKind == ValueKind.Time)
			{
				sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.TimestampSqlString), new SqlExpression[]
				{
					DrdaAstCreator.BaseDateForTimeAdjustment,
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

		// Token: 0x06005714 RID: 22292 RVA: 0x0012E6D0 File Offset: 0x0012C8D0
		private SqlExpression ConvertDateTimeToMicroseconds(SqlExpression dateTime, SqlExpression baseValue)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaysSqlString), new SqlExpression[] { dateTime }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DaysSqlString), new SqlExpression[] { baseValue })), base.MicrosecondsPerDay);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString), new SqlExpression[] { dateTime }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.HourSqlString), new SqlExpression[] { baseValue })), base.MicrosecondsPerHour);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString), new SqlExpression[] { dateTime }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MinuteSqlString), new SqlExpression[] { baseValue })), base.MicrosecondsPerMinute);
			SqlExpression sqlExpression4 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SecondSqlString), new SqlExpression[] { dateTime }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.SecondSqlString), new SqlExpression[] { baseValue })), base.MicrosecondsPerSecond);
			SqlExpression sqlExpression5 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MicrosecondSqlString), new SqlExpression[] { dateTime }), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.MicrosecondSqlString), new SqlExpression[] { baseValue }));
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression2, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression3, SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(sqlExpression4, sqlExpression5))));
		}

		// Token: 0x06005715 RID: 22293 RVA: 0x0012E83C File Offset: 0x0012CA3C
		private SqlExpression ConvertMicrosecondsToDateTime(SqlExpression totalMicroseconds, SqlExpression baseValue)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(totalMicroseconds, base.MicrosecondsPerDay);
			SqlExpression sqlExpression2 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[] { totalMicroseconds, base.MicrosecondsPerDay }), base.MicrosecondsPerSecond);
			SqlExpression sqlExpression3 = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ModSqlString), new SqlExpression[] { totalMicroseconds, base.MicrosecondsPerSecond });
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Add(baseValue, DrdaAstCreator.IntervalExpression.Days(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IntSqlString), new SqlExpression[] { sqlExpression }))), DrdaAstCreator.IntervalExpression.Seconds(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IntSqlString), new SqlExpression[] { sqlExpression2 }))), DrdaAstCreator.IntervalExpression.Microseconds(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.IntSqlString), new SqlExpression[] { sqlExpression3 })));
		}

		// Token: 0x06005716 RID: 22294 RVA: 0x0012E914 File Offset: 0x0012CB14
		protected override SqlExpression CreateTextContains(IInvocationExpression invocation)
		{
			IList<IExpression> arguments = invocation.Arguments;
			return new BinaryLogicalOperation(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LocateSqlString), new SqlExpression[]
			{
				base.CreateScalarExpression(arguments[1]),
				base.CreateScalarExpression(arguments[0])
			}), BinaryLogicalOperator.GreaterThan, SqlConstant.Zero);
		}

		// Token: 0x06005717 RID: 22295 RVA: 0x0004628D File Offset: 0x0004448D
		protected override SqlExpression CreateDateTimeAddMonths(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.AddMonthsSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x06005718 RID: 22296 RVA: 0x0012E968 File Offset: 0x0012CB68
		protected override SqlExpression CreateDateTimeAddYears(IInvocationExpression invocation)
		{
			SqlExpression sqlExpression = base.CreateScalarExpression(invocation.Arguments[0]);
			SqlExpression sqlExpression2 = new BinaryScalarOperation(base.CreateScalarExpression(invocation.Arguments[1]), BinaryScalarOperator.Multiply, SqlConstant.Twelve);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.AddMonthsSqlString), new SqlExpression[] { sqlExpression, sqlExpression2 });
		}

		// Token: 0x06005719 RID: 22297 RVA: 0x0012E9C4 File Offset: 0x0012CBC4
		protected override SqlExpression CreateNumberMod(SqlExpression number, SqlExpression divisor, IConstantExpression precision)
		{
			Func<SqlExpression, SqlExpression> func = base.CreateNumericCastFromPrecision(precision);
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Subtract(func(number), SqlAstCreatorBase<DbAstCreator.SqlVariable>.Multiply(SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.FloorSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Divide(func(number), func(divisor)) }), divisor));
		}

		// Token: 0x0600571A RID: 22298 RVA: 0x00046345 File Offset: 0x00044545
		protected override SqlExpression CreateNumberNaturalLogarithm(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.LnSqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x0600571B RID: 22299 RVA: 0x0008468F File Offset: 0x0008288F
		protected override SqlExpression Len(SqlExpression expression)
		{
			return SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LengthSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x0600571C RID: 22300 RVA: 0x0012EA16 File Offset: 0x0012CC16
		protected override SqlExpression CreateBinaryFromText(IInvocationExpression invocation)
		{
			return base.CreateFunctionCall(SqlLanguageStrings.VarBinarySqlString, Array.Empty<SqlExpression>())(invocation);
		}

		// Token: 0x0600571D RID: 22301 RVA: 0x0012EA30 File Offset: 0x0012CC30
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

		// Token: 0x0600571E RID: 22302 RVA: 0x0012EAA1 File Offset: 0x0012CCA1
		protected override OutputClause CreateOutputClause(Alias alias, TableTypeValue tableType)
		{
			return this.CreateOutputClause(alias, tableType, null, null, StatementType.Other);
		}

		// Token: 0x0600571F RID: 22303 RVA: 0x0012EAB0 File Offset: 0x0012CCB0
		private OutputClause CreateOutputClause(Alias alias, TableTypeValue tableType, TableReference table, Condition whereClause, StatementType statementType)
		{
			if (!this.countOnly)
			{
				return new DrdaAstCreator.DrdaOutputClause(tableType.ItemType.Fields.Keys.Select((string c) => new SelectItem(new ColumnReference(null, Alias.NewNativeAlias(c)))).ToList<SelectItem>(), this.dbEnvironment, table, whereClause, statementType);
			}
			return OutputClause.Null;
		}

		// Token: 0x06005720 RID: 22304 RVA: 0x0012EB14 File Offset: 0x0012CD14
		protected override SqlStatement CreateInsertStatement(TableReference table, Alias alias, TableTypeValue tableType, List<ColumnReference> columnList, List<IList<ScalarExpression>> values)
		{
			return new SqlInsertStatement(table, values, this.CreateOutputClause(alias, tableType, table, null, StatementType.Insert), columnList);
		}

		// Token: 0x06005721 RID: 22305 RVA: 0x0012EB2B File Offset: 0x0012CD2B
		protected override SqlStatement CreateInsertStatement(TableReference table, Alias alias, TableTypeValue tableType, List<ColumnReference> columnList, SqlQueryExpression sourceQuery)
		{
			return new SqlInsertStatement(table, sourceQuery, this.CreateOutputClause(alias, tableType, table, null, StatementType.Insert), columnList);
		}

		// Token: 0x06005722 RID: 22306 RVA: 0x0012EB42 File Offset: 0x0012CD42
		protected override SqlStatement CreateUpdateStatement(TableReference table, Alias alias, TableTypeValue tableType, List<SqlColumnUpdate> updates, Condition whereClause)
		{
			return new SqlUpdateStatement(table, updates, this.CreateOutputClause(alias, tableType, table, whereClause, StatementType.Update), whereClause);
		}

		// Token: 0x06005723 RID: 22307 RVA: 0x0012EB5A File Offset: 0x0012CD5A
		protected override SqlStatement CreateDeleteStatement(TableReference table, Alias alias, TableTypeValue tableType, Condition whereClause)
		{
			return new SqlDeleteStatement(table, this.CreateOutputClause(alias, tableType, table, whereClause, StatementType.Delete), whereClause);
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x0012EB70 File Offset: 0x0012CD70
		protected override ScalarExpression Constant(Value constant, TypeValue type)
		{
			if (!constant.IsNull)
			{
				type = type.NonNullable;
				if (type.TypeKind == ValueKind.Date)
				{
					return SqlAstCreatorBase<DbAstCreator.SqlVariable>.DateConstant(constant.AsDate.AsClrDateTime);
				}
			}
			return base.Constant(constant, type);
		}

		// Token: 0x0400311A RID: 12570
		protected static readonly DateTime BaseDateTime = new DateTime(2000, 1, 1);

		// Token: 0x0400311B RID: 12571
		private static readonly SqlExpression DateAggregateBase = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(DrdaAstCreator.BaseDateTime) });

		// Token: 0x0400311C RID: 12572
		private static readonly SqlExpression DateTimeAggregateBase = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant(DrdaAstCreator.BaseDateTime);

		// Token: 0x0400311D RID: 12573
		private static readonly SqlExpression BaseDateForTimeAdjustment = SqlAstCreatorBase<DbAstCreator.SqlVariable>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateSqlString), new SqlExpression[] { SqlAstCreatorBase<DbAstCreator.SqlVariable>.Constant("2000-01-01") });

		// Token: 0x02000C99 RID: 3225
		protected sealed class IntervalExpression : SqlExpression
		{
			// Token: 0x06005726 RID: 22310 RVA: 0x0012EC1F File Offset: 0x0012CE1F
			private IntervalExpression(SqlExpression expression, ConstantSqlString intervalUnit)
			{
				this.expression = expression;
				this.intervalUnit = intervalUnit;
			}

			// Token: 0x06005727 RID: 22311 RVA: 0x0012EC35 File Offset: 0x0012CE35
			public static DrdaAstCreator.IntervalExpression Days(SqlExpression expression)
			{
				return new DrdaAstCreator.IntervalExpression(expression, SqlLanguageStrings.DaysSqlString);
			}

			// Token: 0x06005728 RID: 22312 RVA: 0x0012EC42 File Offset: 0x0012CE42
			public static DrdaAstCreator.IntervalExpression Seconds(SqlExpression expression)
			{
				return new DrdaAstCreator.IntervalExpression(expression, SqlLanguageStrings.SecondsSqlString);
			}

			// Token: 0x06005729 RID: 22313 RVA: 0x0012EC4F File Offset: 0x0012CE4F
			public static DrdaAstCreator.IntervalExpression Microseconds(SqlExpression expression)
			{
				return new DrdaAstCreator.IntervalExpression(expression, SqlLanguageStrings.MicrosecondsSqlString);
			}

			// Token: 0x17001A40 RID: 6720
			// (get) Token: 0x0600572A RID: 22314 RVA: 0x00002105 File Offset: 0x00000305
			public override int Precedence
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x0600572B RID: 22315 RVA: 0x0012EC5C File Offset: 0x0012CE5C
			public override void WriteCreateScript(ScriptWriter writer)
			{
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				writer.WriteSubexpression(10, this.expression);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
				writer.WriteSpaceBeforeAndAfter(this.intervalUnit);
			}

			// Token: 0x0400311E RID: 12574
			private readonly ConstantSqlString intervalUnit;

			// Token: 0x0400311F RID: 12575
			private readonly SqlExpression expression;
		}

		// Token: 0x02000C9A RID: 3226
		private sealed class DrdaOutputClause : OutputClause
		{
			// Token: 0x0600572C RID: 22316 RVA: 0x0012EC90 File Offset: 0x0012CE90
			public DrdaOutputClause(List<SelectItem> columnList, DbEnvironment dbEnvironment, TableReference table, Condition whereClause, StatementType statementType)
				: base(columnList)
			{
				this.statementType = statementType;
				this.table = table;
				this.whereClause = whereClause;
				if (this.statementType != StatementType.Insert)
				{
					string serverVersion = dbEnvironment.ServerVersion;
					if (serverVersion.StartsWith("IFX/", StringComparison.OrdinalIgnoreCase) || serverVersion.StartsWith("DB2/400", StringComparison.OrdinalIgnoreCase))
					{
						this.usingMultipleStatements = true;
					}
				}
			}

			// Token: 0x0600572D RID: 22317 RVA: 0x0012ECF0 File Offset: 0x0012CEF0
			public override void WritePrefixScript(ScriptWriter writer)
			{
				if (this.usingMultipleStatements)
				{
					if (this.statementType == StatementType.Delete)
					{
						this.WriteSelectStatement(writer);
						writer.WriteSpaceBefore(SqlLanguageSymbols.SemiColonSqlString);
						return;
					}
				}
				else
				{
					writer.WriteSpaceAfter(SqlLanguageStrings.SelectSqlString);
					base.WriteColumns(writer);
					writer.WriteSpaceBefore(SqlLanguageStrings.FromSqlString);
					writer.WriteSpaceBefore((this.statementType == StatementType.Delete) ? SqlLanguageStrings.OldSqlString : SqlLanguageStrings.FinalSqlString);
					writer.WriteSpaceBefore(SqlLanguageStrings.TableSqlString);
					writer.WriteSpaceBefore(SqlLanguageSymbols.OpenParenthesisSqlString);
					writer.WriteLine();
				}
			}

			// Token: 0x0600572E RID: 22318 RVA: 0x0012ED75 File Offset: 0x0012CF75
			public override void WriteSuffixScript(ScriptWriter writer)
			{
				if (this.usingMultipleStatements)
				{
					if (this.statementType == StatementType.Update)
					{
						writer.WriteSpaceBefore(SqlLanguageSymbols.SemiColonSqlString);
						this.WriteSelectStatement(writer);
						return;
					}
				}
				else
				{
					writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
				}
			}

			// Token: 0x0600572F RID: 22319 RVA: 0x0012EDA8 File Offset: 0x0012CFA8
			private void WriteSelectStatement(ScriptWriter writer)
			{
				writer.WriteSpaceAfter(SqlLanguageStrings.SelectSqlString);
				base.WriteColumns(writer);
				writer.WriteSpaceBefore(SqlLanguageStrings.FromSqlString);
				this.table.WriteCreateScript(writer);
				if (this.whereClause != null)
				{
					writer.WriteLine();
					writer.WriteSpaceAfter(SqlLanguageStrings.WhereSqlString);
					this.whereClause.WriteCreateScript(writer);
				}
			}

			// Token: 0x04003120 RID: 12576
			private readonly bool usingMultipleStatements;

			// Token: 0x04003121 RID: 12577
			private readonly StatementType statementType;

			// Token: 0x04003122 RID: 12578
			private readonly TableReference table;

			// Token: 0x04003123 RID: 12579
			private readonly Condition whereClause;
		}
	}
}
