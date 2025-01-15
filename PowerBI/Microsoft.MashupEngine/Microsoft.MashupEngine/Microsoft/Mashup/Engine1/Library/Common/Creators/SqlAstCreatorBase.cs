using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x0200119D RID: 4509
	internal abstract class SqlAstCreatorBase<TBinding> : EnvironmentAstVisitor<TBinding>
	{
		// Token: 0x06007713 RID: 30483 RVA: 0x0019D794 File Offset: 0x0019B994
		protected SqlAstCreatorBase(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
			this.dbEnvironment = environment;
		}

		// Token: 0x06007714 RID: 30484 RVA: 0x0019D7A6 File Offset: 0x0019B9A6
		protected static BinaryScalarOperation Add(SqlExpression left, SqlExpression right)
		{
			return new BinaryScalarOperation(left, BinaryScalarOperator.Add, right);
		}

		// Token: 0x06007715 RID: 30485 RVA: 0x0019D7B0 File Offset: 0x0019B9B0
		protected static BinaryScalarOperation Subtract(SqlExpression left, SqlExpression right)
		{
			return new BinaryScalarOperation(left, BinaryScalarOperator.Subtract, right);
		}

		// Token: 0x06007716 RID: 30486 RVA: 0x0019D7BA File Offset: 0x0019B9BA
		protected static BinaryScalarOperation Multiply(SqlExpression left, SqlExpression right)
		{
			return new BinaryScalarOperation(left, BinaryScalarOperator.Multiply, right);
		}

		// Token: 0x06007717 RID: 30487 RVA: 0x0019D7C4 File Offset: 0x0019B9C4
		protected static BinaryScalarOperation Divide(SqlExpression left, SqlExpression right)
		{
			return new BinaryScalarOperation(left, BinaryScalarOperator.Divide, right);
		}

		// Token: 0x06007718 RID: 30488 RVA: 0x0019D7CE File Offset: 0x0019B9CE
		protected static SqlExpression Floor(SqlExpression expression)
		{
			return SqlAstCreatorBase<TBinding>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.FloorSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x06007719 RID: 30489 RVA: 0x0019D7E9 File Offset: 0x0019B9E9
		protected static SqlExpression Abs(SqlExpression expression)
		{
			return SqlAstCreatorBase<TBinding>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.AbsSqlString), new SqlExpression[] { expression });
		}

		// Token: 0x0600771A RID: 30490 RVA: 0x0019D804 File Offset: 0x0019BA04
		protected static Condition And(IEnumerable<Condition> conditions)
		{
			Condition condition = conditions.FirstOrDefault<Condition>();
			foreach (Condition condition2 in conditions.Skip(1))
			{
				condition = new ConditionOperation(condition, ConditionOperator.And, condition2);
			}
			return condition;
		}

		// Token: 0x0600771B RID: 30491 RVA: 0x0019D85C File Offset: 0x0019BA5C
		protected static Condition And(params Condition[] conditions)
		{
			return SqlAstCreatorBase<TBinding>.And(conditions);
		}

		// Token: 0x0600771C RID: 30492 RVA: 0x0019D864 File Offset: 0x0019BA64
		protected static SqlAstCreatorBase<TBinding>.XAlias As(Alias alias)
		{
			return new SqlAstCreatorBase<TBinding>.XAlias(alias);
		}

		// Token: 0x0600771D RID: 30493 RVA: 0x0019D86C File Offset: 0x0019BA6C
		protected static AggregateFunctionCall Avg(SqlExpression expression)
		{
			return AggregateFunctionCall.Average(expression);
		}

		// Token: 0x0600771E RID: 30494 RVA: 0x0019D874 File Offset: 0x0019BA74
		protected static T Call<T>(T function, params SqlExpression[] args) where T : FunctionReferenceBase
		{
			IList<FunctionParameterValue> parameters = function.Parameters;
			for (int i = 0; i < args.Length; i++)
			{
				FunctionParameterValue functionParameterValue = args[i] as FunctionParameterValue;
				ICollection<FunctionParameterValue> collection = parameters;
				FunctionParameterValue functionParameterValue2;
				if ((functionParameterValue2 = functionParameterValue) == null)
				{
					(functionParameterValue2 = new FunctionParameterValue()).Value = args[i];
				}
				collection.Add(functionParameterValue2);
			}
			return function;
		}

		// Token: 0x0600771F RID: 30495 RVA: 0x0019D8C0 File Offset: 0x0019BAC0
		protected static SqlAstCreatorBase<TBinding>.XCase Case()
		{
			return default(SqlAstCreatorBase<TBinding>.XCase);
		}

		// Token: 0x06007720 RID: 30496 RVA: 0x0019D8D6 File Offset: 0x0019BAD6
		protected static SqlAstCreatorBase<TBinding>.XColumnReference Column(Alias name)
		{
			return new SqlAstCreatorBase<TBinding>.XColumnReference(name);
		}

		// Token: 0x06007721 RID: 30497 RVA: 0x0019D8DE File Offset: 0x0019BADE
		protected static SqlAstCreatorBase<TBinding>.XColumnReference Column(string name, SqlSettings sqlSettings)
		{
			return new SqlAstCreatorBase<TBinding>.XColumnReference(Alias.NewAssignedAlias(name, sqlSettings));
		}

		// Token: 0x06007722 RID: 30498 RVA: 0x0019D8EC File Offset: 0x0019BAEC
		protected static SqlAstCreatorBase<TBinding>.XColumnReference Column(Alias qualifier, Alias name)
		{
			return new SqlAstCreatorBase<TBinding>.XColumnReference(qualifier, name);
		}

		// Token: 0x06007723 RID: 30499 RVA: 0x0019D8F5 File Offset: 0x0019BAF5
		protected static SqlAstCreatorBase<TBinding>.XColumnReference Column(Alias qualifier, string name, SqlSettings sqlSettings)
		{
			return new SqlAstCreatorBase<TBinding>.XColumnReference(qualifier, Alias.NewAssignedAlias(name, sqlSettings));
		}

		// Token: 0x06007724 RID: 30500 RVA: 0x0019D904 File Offset: 0x0019BB04
		protected virtual SqlConstant Constant(bool value)
		{
			if (!value)
			{
				return SqlConstant.NumericFalse;
			}
			return SqlConstant.NumericTrue;
		}

		// Token: 0x06007725 RID: 30501 RVA: 0x0019D914 File Offset: 0x0019BB14
		protected static SqlConstant Constant(byte[] value)
		{
			BinaryValue binaryValue = BinaryValue.New(value);
			Value value2 = Library.Binary.ToText.Invoke(binaryValue, Library.BinaryEncoding.Hex);
			return new SqlConstant(ConstantType.Binary, value2.AsString);
		}

		// Token: 0x06007726 RID: 30502 RVA: 0x0019D945 File Offset: 0x0019BB45
		protected static SqlConstant Constant(DateTime value)
		{
			return new SqlConstant(ConstantType.DateTime, value.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF", CultureInfo.InvariantCulture));
		}

		// Token: 0x06007727 RID: 30503 RVA: 0x0019D95E File Offset: 0x0019BB5E
		protected static SqlConstant Constant(DateTimeOffset value)
		{
			return new SqlConstant(ConstantType.DateTimeOffset, value.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFFzzz", CultureInfo.InvariantCulture));
		}

		// Token: 0x06007728 RID: 30504 RVA: 0x0019D978 File Offset: 0x0019BB78
		protected static SqlConstant Constant(decimal value)
		{
			string text = value.ToString(CultureInfo.InvariantCulture);
			if (!text.Contains("."))
			{
				text += ".";
			}
			return new SqlConstant(ConstantType.Decimal, text);
		}

		// Token: 0x06007729 RID: 30505 RVA: 0x0019D9B3 File Offset: 0x0019BBB3
		protected static SqlConstant Constant(double value, bool maxPrecision = true)
		{
			return new SqlConstant(ConstantType.Float, Regex.Replace(value.ToString(maxPrecision ? "E15" : "E", CultureInfo.InvariantCulture), "(-?\\d\\.[0-9]*[1-9]|-?\\d\\.0)(0*)([Ee][\\+\\-][0-9]*)", "$1$3"));
		}

		// Token: 0x0600772A RID: 30506 RVA: 0x0019D9E6 File Offset: 0x0019BBE6
		protected static SqlConstant Constant(int value)
		{
			return new SqlConstant(ConstantType.Integer, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600772B RID: 30507 RVA: 0x0019D9FA File Offset: 0x0019BBFA
		public static SqlConstant Constant(long value)
		{
			return new SqlConstant(ConstantType.Integer, value.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x0600772C RID: 30508 RVA: 0x0019DA0E File Offset: 0x0019BC0E
		protected static SqlConstant Constant(string value)
		{
			if (value == null)
			{
				return SqlConstant.Null;
			}
			if (ScriptWriter.IsAnsiString(value))
			{
				return new SqlConstant(ConstantType.AnsiString, value);
			}
			return new SqlConstant(ConstantType.UnicodeString, value);
		}

		// Token: 0x0600772D RID: 30509 RVA: 0x0019DA30 File Offset: 0x0019BC30
		protected static SqlConstant Constant(TimeSpan value)
		{
			return new SqlConstant(ConstantType.Time, value.ToString());
		}

		// Token: 0x0600772E RID: 30510 RVA: 0x0019DA48 File Offset: 0x0019BC48
		protected virtual SqlConstant IntervalConstant(TimeSpan value)
		{
			return new SqlConstant(ConstantType.Interval, string.Format(CultureInfo.InvariantCulture, "{0:00} {1:00}:{2:00}:{3:00}.{4:0000000}", new object[]
			{
				value.Days,
				value.Hours,
				value.Minutes,
				value.Seconds,
				value.Ticks % 10000000L
			}));
		}

		// Token: 0x0600772F RID: 30511 RVA: 0x0019DAC3 File Offset: 0x0019BCC3
		protected static SqlConstant DateConstant(DateTime value)
		{
			return new SqlConstant(ConstantType.Date, value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
		}

		// Token: 0x06007730 RID: 30512 RVA: 0x0019DAE0 File Offset: 0x0019BCE0
		protected static SqlConstant TimeConstant(TimeSpan value)
		{
			return new SqlConstant(ConstantType.Time, new DateTime(value.Ticks).ToString("HH:mm:ss", CultureInfo.InvariantCulture));
		}

		// Token: 0x06007731 RID: 30513 RVA: 0x0019DB11 File Offset: 0x0019BD11
		protected virtual SqlExpression Convert(SqlDataType columnType, SqlExpression expression)
		{
			return new ConvertCall
			{
				Type = columnType,
				Expression = expression
			};
		}

		// Token: 0x06007732 RID: 30514 RVA: 0x0019DB26 File Offset: 0x0019BD26
		protected virtual SqlExpression Convert(SqlDataType columnType, SqlExpression expression, int style)
		{
			return new ConvertCall
			{
				Type = columnType,
				Expression = expression,
				Style = new int?(style)
			};
		}

		// Token: 0x06007733 RID: 30515 RVA: 0x0019DB47 File Offset: 0x0019BD47
		protected static AggregateFunctionCall Count(SqlExpression expression)
		{
			return AggregateFunctionCall.Count(SqlConstant.One);
		}

		// Token: 0x06007734 RID: 30516 RVA: 0x0019DB53 File Offset: 0x0019BD53
		protected static AggregateFunctionCall CountBig(SqlExpression expression)
		{
			return AggregateFunctionCall.CountBig(SqlConstant.One);
		}

		// Token: 0x06007735 RID: 30517 RVA: 0x0019DB5F File Offset: 0x0019BD5F
		protected static AggregateFunctionCall CountOfDistinct(SqlExpression expression)
		{
			return AggregateFunctionCall.Count(AggregateFunctionCall.Distinct(expression));
		}

		// Token: 0x06007736 RID: 30518 RVA: 0x0019DB6C File Offset: 0x0019BD6C
		protected static AggregateFunctionCall CountOfNotNull(SqlExpression expression)
		{
			return AggregateFunctionCall.Count(expression);
		}

		// Token: 0x06007737 RID: 30519 RVA: 0x0019DB74 File Offset: 0x0019BD74
		protected static FunctionReferenceBase DateAdd(DatePart datePart, SqlExpression value, SqlExpression date)
		{
			return SqlAstCreatorBase<TBinding>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateAddSqlString), new SqlExpression[] { datePart, value, date });
		}

		// Token: 0x06007738 RID: 30520 RVA: 0x0019DB97 File Offset: 0x0019BD97
		protected static FunctionReferenceBase DateDiff(DatePart datePart, SqlExpression startDate, SqlExpression endDate)
		{
			return SqlAstCreatorBase<TBinding>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.DateDiffSqlString), new SqlExpression[] { datePart, startDate, endDate });
		}

		// Token: 0x06007739 RID: 30521 RVA: 0x0019DBBC File Offset: 0x0019BDBC
		protected static SqlDataType Decimal(byte precision, byte scale)
		{
			return new SqlDataType(TypeValue.Decimal.NewFacets(TypeFacets.NewNumeric(new int?(10), new int?((int)precision), new int?((int)scale), SqlLanguageStrings.DecimalSqlString.String)));
		}

		// Token: 0x0600773A RID: 30522 RVA: 0x000023C4 File Offset: 0x000005C4
		protected static RepeatedRowOption Distinct()
		{
			return RepeatedRowOption.Distinct;
		}

		// Token: 0x0600773B RID: 30523 RVA: 0x0019DBFD File Offset: 0x0019BDFD
		protected static SqlAstCreatorBase<TBinding>.XCondition Equals(SqlExpression left, SqlExpression right)
		{
			return new BinaryLogicalOperation(left, BinaryLogicalOperator.Equals, right);
		}

		// Token: 0x0600773C RID: 30524 RVA: 0x0019DC0C File Offset: 0x0019BE0C
		protected static FromItem OptimizedFrom(SqlQueryExpression queryExpression)
		{
			PagingQuerySpecification pagingQuerySpecification = queryExpression as PagingQuerySpecification;
			if (pagingQuerySpecification != null)
			{
				if (pagingQuerySpecification.OrderByClause != null)
				{
					PagingQuerySpecification pagingQuerySpecification2 = pagingQuerySpecification.ShallowCopy();
					if (pagingQuerySpecification2.PagingClause == null)
					{
						pagingQuerySpecification2.OrderByClause = null;
					}
					else
					{
						pagingQuerySpecification2.PagingClause = pagingQuerySpecification2.PagingClause.ShallowCopy();
						pagingQuerySpecification2.PagingClause.SuppressOrderBy = true;
					}
					queryExpression = pagingQuerySpecification2;
				}
				if (pagingQuerySpecification.ReturnsAllColumns && pagingQuerySpecification.RepeatedRowOption != RepeatedRowOption.Distinct && pagingQuerySpecification.PagingClause == null && pagingQuerySpecification.FromItems.Count == 1 && pagingQuerySpecification.FromItems[0].RotationClause == null)
				{
					return pagingQuerySpecification.FromItems[0].ShallowCopy();
				}
			}
			return new FromQuery
			{
				Query = queryExpression
			};
		}

		// Token: 0x0600773D RID: 30525 RVA: 0x0019DCBE File Offset: 0x0019BEBE
		protected static FromItem OptimizedFrom(SqlQueryExpression queryExpression, Alias alias)
		{
			FromItem fromItem = SqlAstCreatorBase<TBinding>.OptimizedFrom(queryExpression);
			fromItem.Alias = alias;
			return fromItem;
		}

		// Token: 0x0600773E RID: 30526 RVA: 0x0019DCCD File Offset: 0x0019BECD
		protected static SqlAstCreatorBase<TBinding>.XCondition IsNotNull(SqlExpression expression)
		{
			return new UnaryLogicalOperation(UnaryLogicalOperator.IsNotNull, expression);
		}

		// Token: 0x0600773F RID: 30527 RVA: 0x0019DCDB File Offset: 0x0019BEDB
		protected static SqlAstCreatorBase<TBinding>.XCondition IsNull(SqlExpression expression)
		{
			return new UnaryLogicalOperation(UnaryLogicalOperator.IsNull, expression);
		}

		// Token: 0x06007740 RID: 30528 RVA: 0x0019DCE9 File Offset: 0x0019BEE9
		protected static FieldAccessExpression FieldAccess(ColumnReference column, ConstantSqlString field)
		{
			return new FieldAccessExpression(column, field);
		}

		// Token: 0x06007741 RID: 30529 RVA: 0x0019DCF4 File Offset: 0x0019BEF4
		protected virtual SqlExpression Len(SqlExpression expression)
		{
			SqlExpression sqlExpression = SqlAstCreatorBase<TBinding>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.ReplaceSqlString), new SqlExpression[]
			{
				expression,
				SqlAstCreatorBase<TBinding>.Constant(" "),
				SqlAstCreatorBase<TBinding>.Constant("*")
			});
			return SqlAstCreatorBase<TBinding>.Call<BuiltInFunctionReference>(new BuiltInFunctionReference(SqlLanguageStrings.LenSqlString), new SqlExpression[] { sqlExpression });
		}

		// Token: 0x06007742 RID: 30530 RVA: 0x0019DD4E File Offset: 0x0019BF4E
		protected static AggregateFunctionCall Max(SqlExpression expression)
		{
			return AggregateFunctionCall.Max(expression);
		}

		// Token: 0x06007743 RID: 30531 RVA: 0x0019DD56 File Offset: 0x0019BF56
		protected static AggregateFunctionCall Min(SqlExpression expression)
		{
			return AggregateFunctionCall.Min(expression);
		}

		// Token: 0x06007744 RID: 30532 RVA: 0x0019DD5E File Offset: 0x0019BF5E
		protected static SqlExpression Minus(SqlExpression expression)
		{
			return new UnaryScalarOperation(UnaryScalarOperator.Negative, expression);
		}

		// Token: 0x06007745 RID: 30533 RVA: 0x0019DD67 File Offset: 0x0019BF67
		protected static ConditionOperation Not(Condition expression)
		{
			return new ConditionOperation(ConditionOperator.Not, expression);
		}

		// Token: 0x06007746 RID: 30534 RVA: 0x0019DD70 File Offset: 0x0019BF70
		protected static SqlAstCreatorBase<TBinding>.XCondition NotEqualTo(SqlExpression left, SqlExpression right)
		{
			return new BinaryLogicalOperation(left, BinaryLogicalOperator.NotEqualTo, right);
		}

		// Token: 0x06007747 RID: 30535 RVA: 0x0019DD80 File Offset: 0x0019BF80
		protected static Condition Or(params Condition[] conditions)
		{
			if (conditions.Length == 0)
			{
				return null;
			}
			Condition condition = conditions[0];
			for (int i = 1; i < conditions.Length; i++)
			{
				condition = new ConditionOperation(condition, ConditionOperator.Or, conditions[i]);
			}
			return condition;
		}

		// Token: 0x06007748 RID: 30536 RVA: 0x0019DDB1 File Offset: 0x0019BFB1
		protected SqlAstCreatorBase<TBinding>.XSelectExpression Select(SqlAstCreatorBase<TBinding>.XColumnReference column)
		{
			return new SqlAstCreatorBase<TBinding>.XSelectExpression(new SelectItem[] { this.CreateSelectItem(column) });
		}

		// Token: 0x06007749 RID: 30537 RVA: 0x0019DDC8 File Offset: 0x0019BFC8
		protected SqlAstCreatorBase<TBinding>.XSelectExpression Select(IEnumerable<SqlAstCreatorBase<TBinding>.XColumnReference> columns)
		{
			return new SqlAstCreatorBase<TBinding>.XSelectExpression(columns.Select((SqlAstCreatorBase<TBinding>.XColumnReference cr) => this.CreateSelectItem(cr)).ToArray<SelectItem>());
		}

		// Token: 0x0600774A RID: 30538 RVA: 0x0019DDE6 File Offset: 0x0019BFE6
		protected SqlAstCreatorBase<TBinding>.XSelectExpression Select(IEnumerable<SelectItem> selectItems)
		{
			return new SqlAstCreatorBase<TBinding>.XSelectExpression(selectItems.ToArray<SelectItem>());
		}

		// Token: 0x0600774B RID: 30539 RVA: 0x0019DDF3 File Offset: 0x0019BFF3
		protected SqlAstCreatorBase<TBinding>.XSelectExpression SelectStar(IEnumerable<SqlAstCreatorBase<TBinding>.XColumnReference> columns)
		{
			return new SqlAstCreatorBase<TBinding>.XSelectExpression(columns.Select((SqlAstCreatorBase<TBinding>.XColumnReference cr) => this.CreateSelectItem(cr)).ToArray<SelectItem>(), true);
		}

		// Token: 0x0600774C RID: 30540 RVA: 0x0019DE12 File Offset: 0x0019C012
		protected SqlAstCreatorBase<TBinding>.XSelectExpression SelectStar(IEnumerable<SelectItem> selectItems)
		{
			return new SqlAstCreatorBase<TBinding>.XSelectExpression(selectItems.ToArray<SelectItem>(), true);
		}

		// Token: 0x0600774D RID: 30541 RVA: 0x0019DE20 File Offset: 0x0019C020
		protected SqlAstCreatorBase<TBinding>.XSelectExpression SelectStar(RepeatedRowOption repeatedRowOption, IEnumerable<SqlAstCreatorBase<TBinding>.XColumnReference> columns)
		{
			return new SqlAstCreatorBase<TBinding>.XSelectExpression(repeatedRowOption, columns.Select((SqlAstCreatorBase<TBinding>.XColumnReference cr) => this.CreateSelectItem(cr)).ToArray<SelectItem>(), true);
		}

		// Token: 0x0600774E RID: 30542 RVA: 0x0019DE40 File Offset: 0x0019C040
		protected SqlAstCreatorBase<TBinding>.XSelectExpression Select(SqlExpression selectItem, Alias alias)
		{
			return new SqlAstCreatorBase<TBinding>.XSelectExpression(new SelectItem[]
			{
				new SelectItem(selectItem, alias)
			});
		}

		// Token: 0x0600774F RID: 30543 RVA: 0x0019DE57 File Offset: 0x0019C057
		protected virtual SelectItem CreateSelectItem(SqlAstCreatorBase<TBinding>.XColumnReference columnReference)
		{
			return new SelectItem(columnReference);
		}

		// Token: 0x06007750 RID: 30544 RVA: 0x0019DE64 File Offset: 0x0019C064
		private static PagingQuerySpecification Simplify(PagingQuerySpecification specification)
		{
			PagingQuerySpecification pagingQuerySpecification;
			do
			{
				pagingQuerySpecification = specification;
				specification = SqlAstCreatorBase<TBinding>.SimplifyPassThrough(specification);
				specification = SqlAstCreatorBase<TBinding>.SimplifySelectSubset(specification);
			}
			while (specification != pagingQuerySpecification);
			return specification;
		}

		// Token: 0x06007751 RID: 30545 RVA: 0x0019DE88 File Offset: 0x0019C088
		private static PagingQuerySpecification SimplifyPassThrough(PagingQuerySpecification specification)
		{
			if (specification.FromItems.Count != 1)
			{
				return specification;
			}
			if (specification.WhereClause != null)
			{
				return specification;
			}
			if (specification.OrderByClause != null)
			{
				return specification;
			}
			if (specification.SelectItems.Any((SelectItem o) => o.Expression is AggregateFunctionCall))
			{
				return specification;
			}
			FromQuery fromQuery = specification.FromItems[0] as FromQuery;
			if (fromQuery == null)
			{
				return specification;
			}
			PagingQuerySpecification pagingQuerySpecification = fromQuery.Query as PagingQuerySpecification;
			if (pagingQuerySpecification == null)
			{
				return specification;
			}
			if (pagingQuerySpecification.FromItems.Count != 1)
			{
				return specification;
			}
			if (!pagingQuerySpecification.ReturnsAllColumns)
			{
				return specification;
			}
			if (pagingQuerySpecification.PagingClause != null)
			{
				return specification;
			}
			fromQuery.Alias = specification.FromItems[0].Alias;
			specification.FromItems[0] = fromQuery;
			specification.WhereClause = pagingQuerySpecification.WhereClause;
			specification.PagingClause = specification.PagingClause ?? pagingQuerySpecification.PagingClause;
			if (specification.RepeatedRowOption != RepeatedRowOption.Distinct)
			{
				specification.RepeatedRowOption = pagingQuerySpecification.RepeatedRowOption;
			}
			return specification;
		}

		// Token: 0x06007752 RID: 30546 RVA: 0x0019DF90 File Offset: 0x0019C190
		private static PagingQuerySpecification SimplifySelectSubset(PagingQuerySpecification specification)
		{
			if (specification.FromItems.Count != 1)
			{
				return specification;
			}
			if (specification.GroupByClause != null)
			{
				return specification;
			}
			if (specification.WhereClause != null)
			{
				return specification;
			}
			if (specification.OrderByClause != null)
			{
				return specification;
			}
			if (specification.FromItems[0].RotationClause != null)
			{
				return specification;
			}
			FromQuery fromQuery = specification.FromItems[0] as FromQuery;
			if (fromQuery == null)
			{
				return specification;
			}
			if (!specification.SelectItems.All((SelectItem si) => si.Expression is ColumnReference))
			{
				return specification;
			}
			PagingQuerySpecification pagingQuerySpecification = fromQuery.Query as PagingQuerySpecification;
			if (pagingQuerySpecification == null)
			{
				return specification;
			}
			if (pagingQuerySpecification.SelectItems.Any((SelectItem si) => si.Name == null))
			{
				return specification;
			}
			PagingQuerySpecification pagingQuerySpecification2 = pagingQuerySpecification.ShallowCopy();
			pagingQuerySpecification2.SelectItems.Clear();
			pagingQuerySpecification2.ReturnsAllColumns = false;
			HashSet<SelectItem> hashSet = new HashSet<SelectItem>();
			foreach (SelectItem selectItem in specification.SelectItems)
			{
				Alias reference = ((ColumnReference)selectItem.Expression).Name;
				SelectItem selectItem2 = pagingQuerySpecification.SelectItems.SingleOrDefault((SelectItem si) => si.Name.Equals(reference));
				if (selectItem2 == null)
				{
					return specification;
				}
				hashSet.Add(selectItem2);
				SelectItem selectItem3 = selectItem2.ShallowCopy();
				if (selectItem.Alias != null)
				{
					selectItem3.Alias = selectItem.Alias;
				}
				pagingQuerySpecification2.SelectItems.Add(selectItem3);
			}
			if (pagingQuerySpecification.RepeatedRowOption == RepeatedRowOption.Distinct && specification.RepeatedRowOption != RepeatedRowOption.Distinct && hashSet.Count < pagingQuerySpecification.SelectItems.Count)
			{
				return specification;
			}
			pagingQuerySpecification2.PagingClause = specification.PagingClause ?? pagingQuerySpecification.PagingClause;
			if (pagingQuerySpecification2.RepeatedRowOption != RepeatedRowOption.Distinct)
			{
				pagingQuerySpecification2.RepeatedRowOption = specification.RepeatedRowOption;
			}
			return pagingQuerySpecification2;
		}

		// Token: 0x06007753 RID: 30547 RVA: 0x0019E19C File Offset: 0x0019C39C
		protected static SqlExpression SimplifySimpleScalarExpression(SqlQueryExpression query)
		{
			PagingQuerySpecification pagingQuerySpecification = query as PagingQuerySpecification;
			if (pagingQuerySpecification != null && pagingQuerySpecification.FromItems.Count == 0)
			{
				SelectItem selectItem = pagingQuerySpecification.SelectItems.SingleOrDefault<SelectItem>();
				if (selectItem != null)
				{
					return selectItem.Expression;
				}
			}
			return query;
		}

		// Token: 0x06007754 RID: 30548 RVA: 0x0019E1D7 File Offset: 0x0019C3D7
		protected virtual AggregateFunctionCall Stdev(SqlExpression expression)
		{
			return AggregateFunctionCall.StandardDeviation(expression);
		}

		// Token: 0x06007755 RID: 30549 RVA: 0x0019E1DF File Offset: 0x0019C3DF
		protected static AggregateFunctionCall Sum(SqlExpression expression)
		{
			return AggregateFunctionCall.Sum(expression);
		}

		// Token: 0x06007756 RID: 30550 RVA: 0x0019E1E7 File Offset: 0x0019C3E7
		protected static SqlQueryExpression UnionAll(SqlQueryExpression left, SqlQueryExpression right)
		{
			return new BinaryQueryOperation(left, BinaryQueryOperator.UnionAll, right);
		}

		// Token: 0x040040D2 RID: 16594
		protected readonly DbEnvironment dbEnvironment;

		// Token: 0x040040D3 RID: 16595
		private const string TimeFormat = "HH:mm:ss";

		// Token: 0x040040D4 RID: 16596
		private const string DateFormat = "yyyy-MM-dd";

		// Token: 0x040040D5 RID: 16597
		private const string DateTimeFormat = "yyyy-MM-dd HH:mm:ss.FFFFFFF";

		// Token: 0x040040D6 RID: 16598
		private const string DateTimeOffsetFormat = "yyyy-MM-dd HH:mm:ss.FFFFFFFzzz";

		// Token: 0x040040D7 RID: 16599
		protected const long secondsPerDay = 86400L;

		// Token: 0x040040D8 RID: 16600
		protected const long ticksPerDay = 864000000000L;

		// Token: 0x040040D9 RID: 16601
		protected const long ticksPerHour = 36000000000L;

		// Token: 0x040040DA RID: 16602
		protected const long ticksPerMinute = 600000000L;

		// Token: 0x040040DB RID: 16603
		protected const long ticksPerSecond = 10000000L;

		// Token: 0x040040DC RID: 16604
		protected const long ticksPerMs = 10000L;

		// Token: 0x040040DD RID: 16605
		protected const long ticksPerUs = 10L;

		// Token: 0x040040DE RID: 16606
		protected const long nsPerTick = 100L;

		// Token: 0x040040DF RID: 16607
		protected const long microsecondsPerSecond = 1000000L;

		// Token: 0x040040E0 RID: 16608
		protected const long microsecondsPerMinute = 60000000L;

		// Token: 0x040040E1 RID: 16609
		protected const long microsecondsPerHour = 3600000000L;

		// Token: 0x040040E2 RID: 16610
		protected const long microsecondsPerDay = 86400000000L;

		// Token: 0x0200119E RID: 4510
		protected struct XAlias
		{
			// Token: 0x0600775A RID: 30554 RVA: 0x0019E1FA File Offset: 0x0019C3FA
			public XAlias(Alias alias)
			{
				this.alias = alias;
			}

			// Token: 0x170020B6 RID: 8374
			// (get) Token: 0x0600775B RID: 30555 RVA: 0x0019E203 File Offset: 0x0019C403
			public Alias Alias
			{
				get
				{
					return this.alias;
				}
			}

			// Token: 0x0600775C RID: 30556 RVA: 0x0019E20B File Offset: 0x0019C40B
			public static implicit operator SqlAstCreatorBase<TBinding>.XFromItem(SqlAstCreatorBase<TBinding>.XAlias alias)
			{
				return new SqlAstCreatorBase<TBinding>.XFromItem(new SqlAstCreatorBase<TBinding>.FromNothing
				{
					Alias = alias.Alias
				});
			}

			// Token: 0x040040E3 RID: 16611
			private Alias alias;
		}

		// Token: 0x0200119F RID: 4511
		private class FromNothing : FromItem
		{
			// Token: 0x0600775D RID: 30557 RVA: 0x000091AE File Offset: 0x000073AE
			public override FromItem ShallowCopy()
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600775E RID: 30558 RVA: 0x000091AE File Offset: 0x000073AE
			public override void WriteCreateScript(ScriptWriter writer)
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x020011A0 RID: 4512
		protected struct XCase
		{
			// Token: 0x06007760 RID: 30560 RVA: 0x0019E22C File Offset: 0x0019C42C
			public SqlAstCreatorBase<TBinding>.XCaseWhen When(Condition condition)
			{
				return new SqlAstCreatorBase<TBinding>.XCaseWhen(condition, new List<WhenItem>());
			}
		}

		// Token: 0x020011A1 RID: 4513
		protected struct XCaseWhen
		{
			// Token: 0x06007761 RID: 30561 RVA: 0x0019E239 File Offset: 0x0019C439
			public XCaseWhen(Condition condition, List<WhenItem> whenItems)
			{
				this.condition = condition;
				this.whenItems = whenItems;
			}

			// Token: 0x06007762 RID: 30562 RVA: 0x0019E249 File Offset: 0x0019C449
			public SqlAstCreatorBase<TBinding>.XCaseWhenThen Then(SqlExpression expression)
			{
				this.whenItems.Add(new WhenItem
				{
					When = this.condition,
					Then = expression
				});
				return new SqlAstCreatorBase<TBinding>.XCaseWhenThen(this.whenItems);
			}

			// Token: 0x040040E4 RID: 16612
			private Condition condition;

			// Token: 0x040040E5 RID: 16613
			private List<WhenItem> whenItems;
		}

		// Token: 0x020011A2 RID: 4514
		protected struct XCaseWhenThen
		{
			// Token: 0x06007763 RID: 30563 RVA: 0x0019E279 File Offset: 0x0019C479
			public XCaseWhenThen(List<WhenItem> whenItems)
			{
				this.whenItems = whenItems;
			}

			// Token: 0x06007764 RID: 30564 RVA: 0x0019E282 File Offset: 0x0019C482
			public SqlAstCreatorBase<TBinding>.XCaseWhenThenElse Else(SqlExpression expression)
			{
				return new SqlAstCreatorBase<TBinding>.XCaseWhenThenElse(this.whenItems, expression);
			}

			// Token: 0x06007765 RID: 30565 RVA: 0x0019E290 File Offset: 0x0019C490
			public SqlAstCreatorBase<TBinding>.XCaseWhen When(Condition condition)
			{
				return new SqlAstCreatorBase<TBinding>.XCaseWhen(condition, this.whenItems);
			}

			// Token: 0x040040E6 RID: 16614
			private List<WhenItem> whenItems;
		}

		// Token: 0x020011A3 RID: 4515
		protected struct XCaseWhenThenElse
		{
			// Token: 0x06007766 RID: 30566 RVA: 0x0019E29E File Offset: 0x0019C49E
			public XCaseWhenThenElse(List<WhenItem> whenItems, SqlExpression elseExpression)
			{
				this.whenItems = whenItems;
				this.elseExpression = elseExpression;
			}

			// Token: 0x06007767 RID: 30567 RVA: 0x0019E2AE File Offset: 0x0019C4AE
			public static implicit operator CaseFunction(SqlAstCreatorBase<TBinding>.XCaseWhenThenElse wte)
			{
				CaseFunction caseFunction = new CaseFunction();
				caseFunction.ElseExpression = wte.elseExpression;
				caseFunction.WhenItems.AddRange(wte.whenItems);
				return caseFunction;
			}

			// Token: 0x06007768 RID: 30568 RVA: 0x0019E2D2 File Offset: 0x0019C4D2
			public static implicit operator SqlAstCreatorBase<TBinding>.XSelectItem(SqlAstCreatorBase<TBinding>.XCaseWhenThenElse wte)
			{
				CaseFunction caseFunction = new CaseFunction();
				caseFunction.ElseExpression = wte.elseExpression;
				caseFunction.WhenItems.AddRange(wte.whenItems);
				return caseFunction;
			}

			// Token: 0x040040E7 RID: 16615
			private SqlExpression elseExpression;

			// Token: 0x040040E8 RID: 16616
			private List<WhenItem> whenItems;
		}

		// Token: 0x020011A4 RID: 4516
		protected struct XColumnReference
		{
			// Token: 0x06007769 RID: 30569 RVA: 0x0019E2FB File Offset: 0x0019C4FB
			public XColumnReference(Alias name)
			{
				this = new SqlAstCreatorBase<TBinding>.XColumnReference(null, name);
			}

			// Token: 0x0600776A RID: 30570 RVA: 0x0019E305 File Offset: 0x0019C505
			public XColumnReference(Alias qualifier, Alias name)
			{
				this.qualifier = qualifier;
				this.name = name;
			}

			// Token: 0x0600776B RID: 30571 RVA: 0x0019E315 File Offset: 0x0019C515
			public static implicit operator ColumnReference(SqlAstCreatorBase<TBinding>.XColumnReference column)
			{
				return new ColumnReference(column.qualifier, column.name);
			}

			// Token: 0x040040E9 RID: 16617
			private readonly Alias name;

			// Token: 0x040040EA RID: 16618
			private readonly Alias qualifier;
		}

		// Token: 0x020011A5 RID: 4517
		protected struct XCondition
		{
			// Token: 0x0600776C RID: 30572 RVA: 0x0019E328 File Offset: 0x0019C528
			public XCondition(Condition condition)
			{
				this.condition = condition;
			}

			// Token: 0x0600776D RID: 30573 RVA: 0x0019E331 File Offset: 0x0019C531
			public static implicit operator Condition(SqlAstCreatorBase<TBinding>.XCondition condition)
			{
				return condition.condition;
			}

			// Token: 0x0600776E RID: 30574 RVA: 0x0019E339 File Offset: 0x0019C539
			public static implicit operator SqlAstCreatorBase<TBinding>.XCondition(Condition condition)
			{
				return new SqlAstCreatorBase<TBinding>.XCondition(condition);
			}

			// Token: 0x040040EB RID: 16619
			private Condition condition;
		}

		// Token: 0x020011A6 RID: 4518
		protected struct XFromExpression
		{
			// Token: 0x0600776F RID: 30575 RVA: 0x0019E344 File Offset: 0x0019C544
			public XFromExpression(RepeatedRowOption repeatedRowOption, PagingClause pagingClause, IList<SelectItem> selectItems, IList<FromItem> fromItems, bool returnsAllColumns)
			{
				this.returnsAllColumns = returnsAllColumns;
				this.repeatedRowOption = repeatedRowOption;
				this.pagingClause = pagingClause;
				this.selectItems = new List<SelectItem>(selectItems);
				this.fromItems = new List<FromItem>();
				for (int i = 0; i < fromItems.Count; i++)
				{
					if (fromItems[i] is SqlAstCreatorBase<TBinding>.FromNothing)
					{
						fromItems[i - 1].Alias = fromItems[i].Alias;
					}
					else
					{
						if (fromItems[i].Alias == null && fromItems[i] is FromQuery)
						{
							fromItems[i].Alias = Alias.Underscore;
						}
						this.fromItems.Add(fromItems[i]);
					}
				}
			}

			// Token: 0x170020B7 RID: 8375
			// (get) Token: 0x06007770 RID: 30576 RVA: 0x0019E400 File Offset: 0x0019C600
			public List<FromItem> FromItems
			{
				get
				{
					return this.fromItems;
				}
			}

			// Token: 0x170020B8 RID: 8376
			// (get) Token: 0x06007771 RID: 30577 RVA: 0x0019E408 File Offset: 0x0019C608
			public List<SelectItem> SelectItems
			{
				get
				{
					return this.selectItems;
				}
			}

			// Token: 0x06007772 RID: 30578 RVA: 0x0019E410 File Offset: 0x0019C610
			public SqlAstCreatorBase<TBinding>.XJoin CrossJoin(SqlAstCreatorBase<TBinding>.XFromItem query, Alias alias)
			{
				return this.Join(query, alias, JoinOperator.CrossJoin);
			}

			// Token: 0x06007773 RID: 30579 RVA: 0x0019E41B File Offset: 0x0019C61B
			public SqlAstCreatorBase<TBinding>.XGroupBy GroupBy(IEnumerable<SqlAstCreatorBase<TBinding>.XColumnReference> columns)
			{
				return this.GroupBy(columns.Select((SqlAstCreatorBase<TBinding>.XColumnReference column) => new GroupByItem
				{
					Expression = column
				}));
			}

			// Token: 0x06007774 RID: 30580 RVA: 0x0019E448 File Offset: 0x0019C648
			public SqlAstCreatorBase<TBinding>.XGroupBy GroupBy(IEnumerable<GroupByItem> groupByItems)
			{
				return this.GroupBy(groupByItems.ToArray<GroupByItem>());
			}

			// Token: 0x06007775 RID: 30581 RVA: 0x0019E456 File Offset: 0x0019C656
			public SqlAstCreatorBase<TBinding>.XGroupBy GroupBy(params GroupByItem[] groupByItems)
			{
				return new SqlAstCreatorBase<TBinding>.XGroupBy(this.repeatedRowOption, this.pagingClause, this.selectItems, this.fromItems, groupByItems);
			}

			// Token: 0x06007776 RID: 30582 RVA: 0x0019E478 File Offset: 0x0019C678
			public PagingQuerySpecification ToPagingQuerySpecification()
			{
				PagingQuerySpecification pagingQuerySpecification = new PagingQuerySpecification();
				pagingQuerySpecification.SelectItems.AddRange(this.selectItems);
				pagingQuerySpecification.FromItems.AddRange(this.fromItems);
				pagingQuerySpecification.RepeatedRowOption = this.repeatedRowOption;
				pagingQuerySpecification.ReturnsAllColumns = this.returnsAllColumns;
				pagingQuerySpecification.PagingClause = this.pagingClause;
				return SqlAstCreatorBase<TBinding>.Simplify(pagingQuerySpecification);
			}

			// Token: 0x06007777 RID: 30583 RVA: 0x0019E4D5 File Offset: 0x0019C6D5
			public static implicit operator SqlAstCreatorBase<TBinding>.XFromItem(SqlAstCreatorBase<TBinding>.XFromExpression from)
			{
				return from.ToPagingQuerySpecification();
			}

			// Token: 0x06007778 RID: 30584 RVA: 0x0019E4E3 File Offset: 0x0019C6E3
			public SqlAstCreatorBase<TBinding>.XJoin Join(SqlAstCreatorBase<TBinding>.XFromItem fromExpression, Alias alias, JoinOperator joinKind)
			{
				return new SqlAstCreatorBase<TBinding>.XJoin(this.repeatedRowOption, this.pagingClause, this.selectItems, this.fromItems, fromExpression, alias, joinKind);
			}

			// Token: 0x06007779 RID: 30585 RVA: 0x0019E50A File Offset: 0x0019C70A
			public SqlAstCreatorBase<TBinding>.XOrderBy OrderBy(OrderByClause orderByClause)
			{
				return new SqlAstCreatorBase<TBinding>.XOrderBy(this.repeatedRowOption, this.pagingClause, this.selectItems, this.fromItems, null, orderByClause);
			}

			// Token: 0x0600777A RID: 30586 RVA: 0x0019E52B File Offset: 0x0019C72B
			public SqlAstCreatorBase<TBinding>.XWhere Where(Condition condition)
			{
				return new SqlAstCreatorBase<TBinding>.XWhere(this.repeatedRowOption, this.pagingClause, this.selectItems, this.fromItems, condition);
			}

			// Token: 0x0600777B RID: 30587 RVA: 0x0019E54B File Offset: 0x0019C74B
			public SqlAstCreatorBase<TBinding>.XWhere Where(IList<Condition> conditions)
			{
				return new SqlAstCreatorBase<TBinding>.XWhere(this.repeatedRowOption, this.pagingClause, this.selectItems, this.fromItems, SqlAstCreatorBase<TBinding>.And(conditions));
			}

			// Token: 0x040040EC RID: 16620
			private List<FromItem> fromItems;

			// Token: 0x040040ED RID: 16621
			private RepeatedRowOption repeatedRowOption;

			// Token: 0x040040EE RID: 16622
			private bool returnsAllColumns;

			// Token: 0x040040EF RID: 16623
			private List<SelectItem> selectItems;

			// Token: 0x040040F0 RID: 16624
			private PagingClause pagingClause;
		}

		// Token: 0x020011A8 RID: 4520
		protected struct XFromItem
		{
			// Token: 0x0600777F RID: 30591 RVA: 0x0019E58F File Offset: 0x0019C78F
			public XFromItem(FromItem item)
			{
				this.item = item;
			}

			// Token: 0x06007780 RID: 30592 RVA: 0x0019E598 File Offset: 0x0019C798
			public static implicit operator FromItem(SqlAstCreatorBase<TBinding>.XFromItem item)
			{
				return item.item;
			}

			// Token: 0x06007781 RID: 30593 RVA: 0x0019E5A0 File Offset: 0x0019C7A0
			public static implicit operator SqlAstCreatorBase<TBinding>.XFromItem(StoredFunctionReference functionReference)
			{
				return new SqlAstCreatorBase<TBinding>.XFromItem(new FromFunction
				{
					Function = functionReference
				});
			}

			// Token: 0x06007782 RID: 30594 RVA: 0x0019E5B3 File Offset: 0x0019C7B3
			public static implicit operator SqlAstCreatorBase<TBinding>.XFromItem(SqlQueryExpression expression)
			{
				return new SqlAstCreatorBase<TBinding>.XFromItem(SqlAstCreatorBase<TBinding>.OptimizedFrom(expression));
			}

			// Token: 0x06007783 RID: 30595 RVA: 0x0019E5C0 File Offset: 0x0019C7C0
			public static implicit operator SqlAstCreatorBase<TBinding>.XFromItem(TableReference tableReference)
			{
				return new SqlAstCreatorBase<TBinding>.XFromItem(new FromTable
				{
					Table = tableReference
				});
			}

			// Token: 0x040040F3 RID: 16627
			private FromItem item;
		}

		// Token: 0x020011A9 RID: 4521
		protected struct XGroupBy
		{
			// Token: 0x06007784 RID: 30596 RVA: 0x0019E5D4 File Offset: 0x0019C7D4
			public XGroupBy(RepeatedRowOption rro, PagingClause pagingClause, IList<SelectItem> selectItems, IList<FromItem> fromItems, IList<GroupByItem> groupByItems)
			{
				this.selectItems = new List<SelectItem>(selectItems);
				this.fromItems = new List<FromItem>(fromItems);
				this.groupByClause = new GroupByClause();
				this.groupByClause.Items.AddRange(groupByItems);
				this.rro = rro;
				this.pagingClause = pagingClause;
			}

			// Token: 0x06007785 RID: 30597 RVA: 0x0019E628 File Offset: 0x0019C828
			public PagingQuerySpecification ToPagingQuerySpecification()
			{
				PagingQuerySpecification pagingQuerySpecification = new PagingQuerySpecification();
				pagingQuerySpecification.SelectItems.AddRange(this.selectItems);
				pagingQuerySpecification.FromItems.AddRange(this.fromItems);
				pagingQuerySpecification.GroupByClause = this.groupByClause;
				pagingQuerySpecification.RepeatedRowOption = this.rro;
				pagingQuerySpecification.PagingClause = this.pagingClause;
				return SqlAstCreatorBase<TBinding>.Simplify(pagingQuerySpecification);
			}

			// Token: 0x040040F4 RID: 16628
			private List<FromItem> fromItems;

			// Token: 0x040040F5 RID: 16629
			private GroupByClause groupByClause;

			// Token: 0x040040F6 RID: 16630
			private RepeatedRowOption rro;

			// Token: 0x040040F7 RID: 16631
			private List<SelectItem> selectItems;

			// Token: 0x040040F8 RID: 16632
			private PagingClause pagingClause;
		}

		// Token: 0x020011AA RID: 4522
		protected struct XJoin
		{
			// Token: 0x06007786 RID: 30598 RVA: 0x0019E688 File Offset: 0x0019C888
			public XJoin(RepeatedRowOption repeatedRowOption, PagingClause pagingClause, IList<SelectItem> selectItems, IList<FromItem> fromItems, FromItem newFromItem, Alias alias, JoinOperator joinKind)
			{
				this.repeatedRowOption = repeatedRowOption;
				this.pagingClause = pagingClause;
				this.selectItems = new List<SelectItem>(selectItems);
				newFromItem.Alias = alias;
				this.join = new JoinOperation
				{
					Left = fromItems[0],
					Operator = joinKind,
					Right = newFromItem
				};
			}

			// Token: 0x06007787 RID: 30599 RVA: 0x0019E6E4 File Offset: 0x0019C8E4
			public PagingQuerySpecification ToPagingQuerySpecification()
			{
				PagingQuerySpecification pagingQuerySpecification = new PagingQuerySpecification();
				pagingQuerySpecification.SelectItems.AddRange(this.selectItems);
				pagingQuerySpecification.FromItems.Add(this.join);
				pagingQuerySpecification.RepeatedRowOption = this.repeatedRowOption;
				pagingQuerySpecification.PagingClause = this.pagingClause;
				return SqlAstCreatorBase<TBinding>.Simplify(pagingQuerySpecification);
			}

			// Token: 0x06007788 RID: 30600 RVA: 0x0019E735 File Offset: 0x0019C935
			public SqlAstCreatorBase<TBinding>.XJoin On(IEnumerable<Condition> conditions)
			{
				this.join.JoinCondition = SqlAstCreatorBase<TBinding>.And(conditions);
				return this;
			}

			// Token: 0x06007789 RID: 30601 RVA: 0x0019E750 File Offset: 0x0019C950
			public SqlAstCreatorBase<TBinding>.XWhere Where(IList<Condition> conditions)
			{
				IList<FromItem> list = new List<FromItem>(1);
				list.Add(this.join);
				return new SqlAstCreatorBase<TBinding>.XWhere(this.repeatedRowOption, this.pagingClause, this.selectItems, list, SqlAstCreatorBase<TBinding>.And(conditions));
			}

			// Token: 0x040040F9 RID: 16633
			private JoinOperation join;

			// Token: 0x040040FA RID: 16634
			private RepeatedRowOption repeatedRowOption;

			// Token: 0x040040FB RID: 16635
			private List<SelectItem> selectItems;

			// Token: 0x040040FC RID: 16636
			private PagingClause pagingClause;
		}

		// Token: 0x020011AB RID: 4523
		protected struct XOrderBy
		{
			// Token: 0x0600778A RID: 30602 RVA: 0x0019E78E File Offset: 0x0019C98E
			public XOrderBy(RepeatedRowOption repeatedRowOption, PagingClause pagingClause, IList<SelectItem> selectItems, IList<FromItem> fromItems, Condition whereClause, OrderByClause orderByClause)
			{
				this.selectItems = selectItems.ToList<SelectItem>();
				this.fromItems = fromItems.ToList<FromItem>();
				this.whereClause = whereClause;
				this.repeatedRowOption = repeatedRowOption;
				this.pagingClause = pagingClause;
				this.orderByClause = orderByClause;
			}

			// Token: 0x0600778B RID: 30603 RVA: 0x0019E7C8 File Offset: 0x0019C9C8
			public PagingQuerySpecification ToPagingQuerySpecification()
			{
				PagingQuerySpecification pagingQuerySpecification = new PagingQuerySpecification();
				pagingQuerySpecification.SelectItems.AddRange(this.selectItems);
				pagingQuerySpecification.FromItems.AddRange(this.fromItems);
				pagingQuerySpecification.WhereClause = this.whereClause;
				pagingQuerySpecification.RepeatedRowOption = this.repeatedRowOption;
				pagingQuerySpecification.PagingClause = this.pagingClause;
				pagingQuerySpecification.OrderByClause = this.orderByClause;
				return SqlAstCreatorBase<TBinding>.Simplify(pagingQuerySpecification);
			}

			// Token: 0x040040FD RID: 16637
			private readonly List<FromItem> fromItems;

			// Token: 0x040040FE RID: 16638
			private readonly OrderByClause orderByClause;

			// Token: 0x040040FF RID: 16639
			private readonly RepeatedRowOption repeatedRowOption;

			// Token: 0x04004100 RID: 16640
			private readonly List<SelectItem> selectItems;

			// Token: 0x04004101 RID: 16641
			private readonly PagingClause pagingClause;

			// Token: 0x04004102 RID: 16642
			private readonly Condition whereClause;
		}

		// Token: 0x020011AC RID: 4524
		protected struct XSelectExpression
		{
			// Token: 0x0600778C RID: 30604 RVA: 0x0019E831 File Offset: 0x0019CA31
			public XSelectExpression(IList<SelectItem> selectItems)
			{
				this = new SqlAstCreatorBase<TBinding>.XSelectExpression(selectItems, false);
			}

			// Token: 0x0600778D RID: 30605 RVA: 0x0019E83B File Offset: 0x0019CA3B
			public XSelectExpression(IList<SelectItem> selectItems, bool returnsAllColumns)
			{
				this = new SqlAstCreatorBase<TBinding>.XSelectExpression(RepeatedRowOption.None, null, selectItems, returnsAllColumns);
			}

			// Token: 0x0600778E RID: 30606 RVA: 0x0019E847 File Offset: 0x0019CA47
			public XSelectExpression(RepeatedRowOption repeatedRowOption, IList<SelectItem> selectItems, bool returnsAllColumns)
			{
				this = new SqlAstCreatorBase<TBinding>.XSelectExpression(repeatedRowOption, null, selectItems, returnsAllColumns);
			}

			// Token: 0x0600778F RID: 30607 RVA: 0x0019E853 File Offset: 0x0019CA53
			public XSelectExpression(RepeatedRowOption repeatedRowOption, PagingClause pagingClause, IList<SelectItem> selectItems, bool returnsAllColumns)
			{
				this.repeatedRowOption = repeatedRowOption;
				this.pagingClause = pagingClause;
				this.selectItems = new List<SelectItem>(selectItems);
				this.returnsAllColumns = returnsAllColumns;
			}

			// Token: 0x06007790 RID: 30608 RVA: 0x0019E877 File Offset: 0x0019CA77
			public XSelectExpression(PagingClause pagingClause, IList<SelectItem> selectItems, bool returnsAllColumns)
			{
				this = new SqlAstCreatorBase<TBinding>.XSelectExpression(RepeatedRowOption.None, pagingClause, selectItems, returnsAllColumns);
			}

			// Token: 0x06007791 RID: 30609 RVA: 0x0019E884 File Offset: 0x0019CA84
			public SqlAstCreatorBase<TBinding>.XFromExpression From(params SqlAstCreatorBase<TBinding>.XFromItem[] fromItems)
			{
				FromItem[] array = new FromItem[fromItems.Length];
				for (int i = 0; i < fromItems.Length; i++)
				{
					array[i] = fromItems[i];
				}
				return new SqlAstCreatorBase<TBinding>.XFromExpression(this.repeatedRowOption, this.pagingClause, this.selectItems, array, this.returnsAllColumns);
			}

			// Token: 0x06007792 RID: 30610 RVA: 0x0019E8D5 File Offset: 0x0019CAD5
			public SqlAstCreatorBase<TBinding>.XFromExpression From(SqlQueryExpression queryExpression)
			{
				return new SqlAstCreatorBase<TBinding>.XFromExpression(this.repeatedRowOption, this.pagingClause, this.selectItems, new FromItem[] { SqlAstCreatorBase<TBinding>.OptimizedFrom(queryExpression) }, this.returnsAllColumns);
			}

			// Token: 0x06007793 RID: 30611 RVA: 0x0019E903 File Offset: 0x0019CB03
			public SqlAstCreatorBase<TBinding>.XFromExpression From(SqlQueryExpression queryExpression, Alias alias)
			{
				return new SqlAstCreatorBase<TBinding>.XFromExpression(this.repeatedRowOption, this.pagingClause, this.selectItems, new FromItem[] { SqlAstCreatorBase<TBinding>.OptimizedFrom(queryExpression, alias) }, this.returnsAllColumns);
			}

			// Token: 0x06007794 RID: 30612 RVA: 0x0019E934 File Offset: 0x0019CB34
			public SqlAstCreatorBase<TBinding>.XFromExpression From(SqlQueryExpression queryExpression, RotationClause rotationClause, Alias alias)
			{
				FromItem fromItem = SqlAstCreatorBase<TBinding>.OptimizedFrom(queryExpression, alias);
				fromItem.RotationClause = rotationClause;
				return new SqlAstCreatorBase<TBinding>.XFromExpression(this.repeatedRowOption, this.pagingClause, this.selectItems, new FromItem[] { fromItem }, this.returnsAllColumns);
			}

			// Token: 0x06007795 RID: 30613 RVA: 0x0019E978 File Offset: 0x0019CB78
			public PagingQuerySpecification ToPagingQuerySpecification()
			{
				PagingQuerySpecification pagingQuerySpecification = new PagingQuerySpecification();
				pagingQuerySpecification.SelectItems.AddRange(this.selectItems);
				pagingQuerySpecification.RepeatedRowOption = this.repeatedRowOption;
				pagingQuerySpecification.PagingClause = this.pagingClause;
				pagingQuerySpecification.ReturnsAllColumns = this.returnsAllColumns;
				return SqlAstCreatorBase<TBinding>.Simplify(pagingQuerySpecification);
			}

			// Token: 0x04004103 RID: 16643
			private RepeatedRowOption repeatedRowOption;

			// Token: 0x04004104 RID: 16644
			private IList<SelectItem> selectItems;

			// Token: 0x04004105 RID: 16645
			private bool returnsAllColumns;

			// Token: 0x04004106 RID: 16646
			private PagingClause pagingClause;
		}

		// Token: 0x020011AD RID: 4525
		protected struct XSelectItem
		{
			// Token: 0x06007796 RID: 30614 RVA: 0x0019E9C4 File Offset: 0x0019CBC4
			public XSelectItem(SelectItem item)
			{
				this.item = item;
			}

			// Token: 0x06007797 RID: 30615 RVA: 0x0019E9CD File Offset: 0x0019CBCD
			public static implicit operator SelectItem(SqlAstCreatorBase<TBinding>.XSelectItem item)
			{
				return item.item;
			}

			// Token: 0x06007798 RID: 30616 RVA: 0x0019E9D5 File Offset: 0x0019CBD5
			public static implicit operator SqlAstCreatorBase<TBinding>.XSelectItem(ColumnReference reference)
			{
				return new SqlAstCreatorBase<TBinding>.XSelectItem(new SelectItem(reference));
			}

			// Token: 0x06007799 RID: 30617 RVA: 0x0019E9D5 File Offset: 0x0019CBD5
			public static implicit operator SqlAstCreatorBase<TBinding>.XSelectItem(SqlExpression expression)
			{
				return new SqlAstCreatorBase<TBinding>.XSelectItem(new SelectItem(expression));
			}

			// Token: 0x04004107 RID: 16647
			private SelectItem item;
		}

		// Token: 0x020011AE RID: 4526
		protected struct XWhere
		{
			// Token: 0x0600779A RID: 30618 RVA: 0x0019E9E2 File Offset: 0x0019CBE2
			public XWhere(RepeatedRowOption repeatedRowOption, PagingClause pagingClause, IList<SelectItem> selectItems, IList<FromItem> fromItems, Condition whereClause)
			{
				this.selectItems = new List<SelectItem>(selectItems);
				this.fromItems = new List<FromItem>(fromItems);
				this.whereClause = whereClause;
				this.repeatedRowOption = repeatedRowOption;
				this.pagingClause = pagingClause;
			}

			// Token: 0x0600779B RID: 30619 RVA: 0x0019EA14 File Offset: 0x0019CC14
			public PagingQuerySpecification ToPagingQuerySpecification()
			{
				PagingQuerySpecification pagingQuerySpecification = new PagingQuerySpecification();
				pagingQuerySpecification.SelectItems.AddRange(this.selectItems);
				pagingQuerySpecification.FromItems.AddRange(this.fromItems);
				pagingQuerySpecification.WhereClause = this.whereClause;
				pagingQuerySpecification.RepeatedRowOption = this.repeatedRowOption;
				pagingQuerySpecification.PagingClause = this.pagingClause;
				return SqlAstCreatorBase<TBinding>.Simplify(pagingQuerySpecification);
			}

			// Token: 0x0600779C RID: 30620 RVA: 0x0019EA71 File Offset: 0x0019CC71
			public SqlAstCreatorBase<TBinding>.XOrderBy OrderBy(OrderByClause orderByClause)
			{
				return new SqlAstCreatorBase<TBinding>.XOrderBy(this.repeatedRowOption, this.pagingClause, this.selectItems, this.fromItems, this.whereClause, orderByClause);
			}

			// Token: 0x04004108 RID: 16648
			private List<FromItem> fromItems;

			// Token: 0x04004109 RID: 16649
			private RepeatedRowOption repeatedRowOption;

			// Token: 0x0400410A RID: 16650
			private List<SelectItem> selectItems;

			// Token: 0x0400410B RID: 16651
			private PagingClause pagingClause;

			// Token: 0x0400410C RID: 16652
			private Condition whereClause;
		}
	}
}
