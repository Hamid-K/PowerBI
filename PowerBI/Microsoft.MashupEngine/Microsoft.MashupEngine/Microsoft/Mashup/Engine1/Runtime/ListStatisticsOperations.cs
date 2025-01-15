using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Table;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001562 RID: 5474
	internal class ListStatisticsOperations
	{
		// Token: 0x06008816 RID: 34838 RVA: 0x001CD87E File Offset: 0x001CBA7E
		public static bool CanSkipSegment(ITableSegmentWithStatistics table, IList<QueryExpression> queryExpressions)
		{
			return !ListStatisticsOperations.BoundsEvaluator.EvaluateFilter(table, queryExpressions);
		}

		// Token: 0x06008817 RID: 34839 RVA: 0x001CD88C File Offset: 0x001CBA8C
		public static bool TryGetAggregation(Query query, Grouping grouping, IEnumerable<ITableSegmentWithStatistics> table, out Query groupQuery)
		{
			if (grouping.KeyColumns.Length == 0 && grouping.Constructors.Length != 0)
			{
				RecordTypeValue recordTypeValue = QueryTableValue.NewRowType(query);
				Func<ListStatistics, Value>[] array = new Func<ListStatistics, Value>[grouping.Constructors.Length];
				int[] array2 = new int[grouping.Constructors.Length];
				for (int i = 0; i < grouping.Constructors.Length; i++)
				{
					QueryExpression queryExpression;
					if (!QueryExpressionBuilder.TryToQueryExpression(recordTypeValue, grouping.Constructors[i].Function, out queryExpression) || !ListStatisticsOperations.TryGetAggregate(queryExpression, out array2[i], out array[i]))
					{
						array = null;
						break;
					}
				}
				if (array != null)
				{
					groupQuery = new ListStatisticsOperations.StatisticsGroupQuery(query, grouping, table, array, array2);
					return true;
				}
			}
			groupQuery = null;
			return false;
		}

		// Token: 0x06008818 RID: 34840 RVA: 0x001CD92C File Offset: 0x001CBB2C
		private static bool TryGetAggregate(QueryExpression expr, out int column, out Func<ListStatistics, Value> aggregator)
		{
			IList<QueryExpression> list;
			if (expr.TryGetInvocation(TableModule.Table.RowCount, 1, out list))
			{
				column = -1;
				aggregator = (ListStatistics stats) => stats.TotalCount;
				return true;
			}
			if (expr.TryGetInvocation(Library.List.Min, 1, out list) && list[0].TryGetColumnAccess(out column))
			{
				aggregator = (ListStatistics stats) => stats.Minimum;
				return true;
			}
			if (expr.TryGetInvocation(Library.List.Max, 1, out list) && list[0].TryGetColumnAccess(out column))
			{
				aggregator = (ListStatistics stats) => stats.Maximum;
				return true;
			}
			Value value;
			bool flag;
			if (expr.TryGetInvocation(LanguageLibrary.List.Count, 1, out list) && list[0].TryGetInvocation(LanguageLibrary.List.Select, 2, out list) && list[0].TryGetColumnAccess(out column) && list[1].TryGetConstant(out value) && value.IsFunction && value.AsFunction.Expression is IFunctionExpression && ((IFunctionExpression)value.AsFunction.Expression).TryGetIsNullOrIsNotNullFilter(out flag))
			{
				if (flag)
				{
					aggregator = (ListStatistics stats) => stats.NullCount;
				}
				else
				{
					aggregator = (ListStatistics stats) => stats.NotNullCount;
				}
				return true;
			}
			column = 0;
			aggregator = null;
			return false;
		}

		// Token: 0x02001563 RID: 5475
		private sealed class StatisticsGroupQuery : GroupQuery
		{
			// Token: 0x0600881A RID: 34842 RVA: 0x001CDAC4 File Offset: 0x001CBCC4
			public StatisticsGroupQuery(Query innerQuery, Grouping grouping, IEnumerable<ITableSegmentWithStatistics> table, Func<ListStatistics, Value>[] aggregates, int[] columns)
				: base(grouping, innerQuery, false)
			{
				this.table = table;
				this.aggregates = aggregates;
				this.maxColumn = -1;
				for (int i = 0; i < columns.Length; i++)
				{
					this.maxColumn = Math.Max(this.maxColumn, columns[i]);
				}
				this.maxColumn = Math.Max(this.maxColumn, 0);
				for (int j = 0; j < columns.Length; j++)
				{
					if (columns[j] == -1)
					{
						columns[j] = this.maxColumn;
					}
				}
				this.columns = columns;
			}

			// Token: 0x0600881B RID: 34843 RVA: 0x001CDB50 File Offset: 0x001CBD50
			public override IEnumerable<IValueReference> GetRows()
			{
				TableValue tableValue = this.MakeAggregation();
				if (tableValue != null)
				{
					return tableValue;
				}
				return base.GetRows();
			}

			// Token: 0x0600881C RID: 34844 RVA: 0x001CDB70 File Offset: 0x001CBD70
			private TableValue MakeAggregation()
			{
				ListStatistics[] statistics = new ListStatistics[this.maxColumn + 1];
				for (int k = 0; k < this.columns.Length; k++)
				{
					statistics[this.columns[k]] = ListStatistics.Empty;
				}
				foreach (ITableSegmentWithStatistics tableSegmentWithStatistics in this.table)
				{
					for (int j = 0; j < statistics.Length; j++)
					{
						if (statistics[j] != null)
						{
							ListStatistics listStatistics;
							if (!tableSegmentWithStatistics.TryGetStatistics(j, out listStatistics))
							{
								return null;
							}
							statistics[j] = statistics[j].Combine(listStatistics);
						}
					}
				}
				RecordValue recordValue = RecordValue.New(base.Grouping.ResultKeys, (int i) => this.aggregates[i](statistics[this.columns[i]]));
				return ListValue.New(new Value[] { recordValue }).ToTable(TableTypeValue.New(QueryTableValue.NewRowType(this)));
			}

			// Token: 0x04004B6B RID: 19307
			private readonly IEnumerable<ITableSegmentWithStatistics> table;

			// Token: 0x04004B6C RID: 19308
			private readonly Func<ListStatistics, Value>[] aggregates;

			// Token: 0x04004B6D RID: 19309
			private readonly int[] columns;

			// Token: 0x04004B6E RID: 19310
			private readonly int maxColumn;
		}

		// Token: 0x02001565 RID: 5477
		private sealed class BoundsEvaluator : QueryExpressionVisitor
		{
			// Token: 0x0600881F RID: 34847 RVA: 0x001CDCB4 File Offset: 0x001CBEB4
			private BoundsEvaluator(ITableSegmentWithStatistics table)
			{
				this.table = table;
				this.value = ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
			}

			// Token: 0x06008820 RID: 34848 RVA: 0x001CDCD0 File Offset: 0x001CBED0
			public static bool EvaluateFilter(ITableSegmentWithStatistics table, IList<QueryExpression> expressions)
			{
				ListStatisticsOperations.BoundsEvaluator boundsEvaluator = new ListStatisticsOperations.BoundsEvaluator(table);
				ListStatisticsOperations.BoundsEvaluator.Bounds bounds = ListStatisticsOperations.BoundsEvaluator.Bounds.True;
				for (int i = 0; i < expressions.Count; i++)
				{
					boundsEvaluator.Visit(expressions[i]);
					bounds = bounds.And(boundsEvaluator.value);
				}
				return bounds.MightBeTrue;
			}

			// Token: 0x06008821 RID: 34849 RVA: 0x001CDD1E File Offset: 0x001CBF1E
			protected override QueryExpression VisitArgumentAccess(ArgumentAccessQueryExpression argument)
			{
				this.value = ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				return argument;
			}

			// Token: 0x06008822 RID: 34850 RVA: 0x001CDD2C File Offset: 0x001CBF2C
			protected override QueryExpression VisitBinary(BinaryQueryExpression binary)
			{
				base.Visit(binary.Left);
				ListStatisticsOperations.BoundsEvaluator.Bounds bounds = this.value;
				base.Visit(binary.Right);
				ListStatisticsOperations.BoundsEvaluator.Bounds bounds2 = this.value;
				switch (binary.Operator)
				{
				case BinaryOperator2.GreaterThan:
					this.value = bounds.GreaterThan(bounds2);
					break;
				case BinaryOperator2.LessThan:
					this.value = bounds.LessThan(bounds2);
					break;
				case BinaryOperator2.GreaterThanOrEquals:
					this.value = bounds.GreaterThanOrEqual(bounds2);
					break;
				case BinaryOperator2.LessThanOrEquals:
					this.value = bounds.LessThanOrEqual(bounds2);
					break;
				case BinaryOperator2.Equals:
					this.value = bounds.Equal(bounds2);
					break;
				case BinaryOperator2.NotEquals:
					this.value = bounds.NotEqual(bounds2);
					break;
				case BinaryOperator2.And:
					this.value = bounds.And(bounds2);
					break;
				case BinaryOperator2.Or:
					this.value = bounds.Or(bounds2);
					break;
				default:
					this.value = ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					break;
				}
				return binary;
			}

			// Token: 0x06008823 RID: 34851 RVA: 0x001CDE24 File Offset: 0x001CC024
			protected override QueryExpression VisitColumnAccess(ColumnAccessQueryExpression columnAccess)
			{
				ListStatistics listStatistics;
				if (this.table.TryGetStatistics(columnAccess.Column, out listStatistics))
				{
					this.value = new ListStatisticsOperations.BoundsEvaluator.Bounds(listStatistics);
				}
				else
				{
					this.value = ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				}
				return columnAccess;
			}

			// Token: 0x06008824 RID: 34852 RVA: 0x001CDE60 File Offset: 0x001CC060
			protected override QueryExpression VisitConstant(ConstantQueryExpression constant)
			{
				this.value = new ListStatisticsOperations.BoundsEvaluator.Bounds(constant.Value);
				return constant;
			}

			// Token: 0x06008825 RID: 34853 RVA: 0x001CDE74 File Offset: 0x001CC074
			protected override QueryExpression VisitIf(IfQueryExpression ifExpr)
			{
				base.Visit(ifExpr.Condition);
				bool? asBoolean = this.value.AsBoolean;
				if (asBoolean != null)
				{
					if (asBoolean.GetValueOrDefault())
					{
						base.Visit(ifExpr.TrueCase);
					}
					else
					{
						base.Visit(ifExpr.FalseCase);
					}
				}
				else
				{
					base.Visit(ifExpr.TrueCase);
					ListStatisticsOperations.BoundsEvaluator.Bounds bounds = this.value;
					base.Visit(ifExpr.FalseCase);
					this.value = this.value.Union(bounds);
				}
				return ifExpr;
			}

			// Token: 0x06008826 RID: 34854 RVA: 0x001CDD1E File Offset: 0x001CBF1E
			protected override QueryExpression VisitInvocation(InvocationQueryExpression invocation)
			{
				this.value = ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				return invocation;
			}

			// Token: 0x06008827 RID: 34855 RVA: 0x001CDF00 File Offset: 0x001CC100
			protected override QueryExpression VisitUnary(UnaryQueryExpression unary)
			{
				base.Visit(unary.Expression);
				switch (unary.Operator)
				{
				case UnaryOperator2.Not:
					this.value = this.value.Not();
					break;
				case UnaryOperator2.Negative:
					this.value = this.value.Negate();
					break;
				case UnaryOperator2.Positive:
					break;
				default:
					this.value = ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					break;
				}
				return unary;
			}

			// Token: 0x04004B71 RID: 19313
			private readonly ITableSegmentWithStatistics table;

			// Token: 0x04004B72 RID: 19314
			private ListStatisticsOperations.BoundsEvaluator.Bounds value;

			// Token: 0x02001566 RID: 5478
			private struct Bounds
			{
				// Token: 0x06008828 RID: 34856 RVA: 0x001CDF67 File Offset: 0x001CC167
				public Bounds(Value value)
				{
					this = new ListStatisticsOperations.BoundsEvaluator.Bounds(value.Kind, value);
				}

				// Token: 0x06008829 RID: 34857 RVA: 0x001CDF78 File Offset: 0x001CC178
				public Bounds(ListStatistics statistics)
				{
					this.Kind = statistics.Type.TypeKind;
					this.MinValue = statistics.Minimum;
					this.MaxValue = statistics.Maximum;
					this.boundKind = ListStatisticsOperations.BoundsEvaluator.Bounds.BoundKind.Range;
					this.nullState = ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.Undefined;
					if (!statistics.NullCount.IsNull && !statistics.TotalCount.IsNull)
					{
						long asInteger = statistics.NullCount.AsNumber.AsInteger64;
						long asInteger2 = statistics.TotalCount.AsNumber.AsInteger64;
						if (asInteger == asInteger2)
						{
							this.nullState = ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.AllNull;
							this.boundKind = ListStatisticsOperations.BoundsEvaluator.Bounds.BoundKind.Constant;
							return;
						}
						if (asInteger == 0L)
						{
							this.nullState = ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.AllNotNull;
							if (this.MinValue.Equals(this.MaxValue))
							{
								this.boundKind = ListStatisticsOperations.BoundsEvaluator.Bounds.BoundKind.Constant;
							}
						}
					}
				}

				// Token: 0x0600882A RID: 34858 RVA: 0x001CE03C File Offset: 0x001CC23C
				private Bounds(ValueKind kind, Value value)
				{
					this.Kind = kind;
					this.MinValue = value;
					this.MaxValue = value;
					if (kind == ValueKind.None)
					{
						this.boundKind = ListStatisticsOperations.BoundsEvaluator.Bounds.BoundKind.Undefined;
						this.nullState = ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.Undefined;
						return;
					}
					this.boundKind = ListStatisticsOperations.BoundsEvaluator.Bounds.BoundKind.Constant;
					this.nullState = (value.IsNull ? ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.AllNull : ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.AllNotNull);
				}

				// Token: 0x0600882B RID: 34859 RVA: 0x001CE097 File Offset: 0x001CC297
				private Bounds(ValueKind kind, ListStatisticsOperations.BoundsEvaluator.Bounds.BoundKind boundKind, Value minValue, Value maxValue, bool? nullState)
				{
					this.Kind = kind;
					this.boundKind = boundKind;
					this.MinValue = minValue;
					this.MaxValue = maxValue;
					this.nullState = nullState;
				}

				// Token: 0x0600882C RID: 34860 RVA: 0x001CE0BE File Offset: 0x001CC2BE
				public static ListStatisticsOperations.BoundsEvaluator.Bounds From(bool value)
				{
					if (!value)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.False;
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.True;
				}

				// Token: 0x170023CC RID: 9164
				// (get) Token: 0x0600882D RID: 34861 RVA: 0x001CE0D0 File Offset: 0x001CC2D0
				public bool MightBeTrue
				{
					get
					{
						bool? asBoolean = this.AsBoolean;
						bool flag = false;
						return !((asBoolean.GetValueOrDefault() == flag) & (asBoolean != null));
					}
				}

				// Token: 0x170023CD RID: 9165
				// (get) Token: 0x0600882E RID: 34862 RVA: 0x001CE0FB File Offset: 0x001CC2FB
				public bool IsMaybe
				{
					get
					{
						return this.Kind == ValueKind.None;
					}
				}

				// Token: 0x170023CE RID: 9166
				// (get) Token: 0x0600882F RID: 34863 RVA: 0x001CE107 File Offset: 0x001CC307
				public bool IsConstant
				{
					get
					{
						return this.boundKind == ListStatisticsOperations.BoundsEvaluator.Bounds.BoundKind.Constant;
					}
				}

				// Token: 0x170023CF RID: 9167
				// (get) Token: 0x06008830 RID: 34864 RVA: 0x001CE114 File Offset: 0x001CC314
				public bool? AsBoolean
				{
					get
					{
						if (this.Kind == ValueKind.Logical && this.IsConstant)
						{
							return new bool?(this.MinValue.AsBoolean);
						}
						return null;
					}
				}

				// Token: 0x170023D0 RID: 9168
				// (get) Token: 0x06008831 RID: 34865 RVA: 0x001CE14C File Offset: 0x001CC34C
				public bool IsNull
				{
					get
					{
						return this.boundKind == ListStatisticsOperations.BoundsEvaluator.Bounds.BoundKind.Constant && this.MinValue.IsNull;
					}
				}

				// Token: 0x170023D1 RID: 9169
				// (get) Token: 0x06008832 RID: 34866 RVA: 0x001CE164 File Offset: 0x001CC364
				private ListStatisticsOperations.BoundsEvaluator.Bounds EqualNull
				{
					get
					{
						bool? flag = this.nullState;
						bool? undefined = ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.Undefined;
						if (!((flag.GetValueOrDefault() == undefined.GetValueOrDefault()) & (flag != null == (undefined != null))))
						{
							return ListStatisticsOperations.BoundsEvaluator.Bounds.From(this.nullState.Value);
						}
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
				}

				// Token: 0x170023D2 RID: 9170
				// (get) Token: 0x06008833 RID: 34867 RVA: 0x001CE1B8 File Offset: 0x001CC3B8
				private ListStatisticsOperations.BoundsEvaluator.Bounds NotEqualNull
				{
					get
					{
						bool? flag = this.nullState;
						bool? undefined = ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.Undefined;
						if (!((flag.GetValueOrDefault() == undefined.GetValueOrDefault()) & (flag != null == (undefined != null))))
						{
							return ListStatisticsOperations.BoundsEvaluator.Bounds.From(!this.nullState.Value);
						}
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
				}

				// Token: 0x06008834 RID: 34868 RVA: 0x001CE210 File Offset: 0x001CC410
				public ListStatisticsOperations.BoundsEvaluator.Bounds Not()
				{
					if (this.IsNull)
					{
						return this;
					}
					bool? asBoolean = this.AsBoolean;
					if (asBoolean != null)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.From(!asBoolean.Value);
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				}

				// Token: 0x06008835 RID: 34869 RVA: 0x001CE254 File Offset: 0x001CC454
				public ListStatisticsOperations.BoundsEvaluator.Bounds Negate()
				{
					if (this.IsNull)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Null;
					}
					if (this.Kind == ValueKind.Number)
					{
						return new ListStatisticsOperations.BoundsEvaluator.Bounds(ValueKind.Number, this.boundKind, this.MaxValue.Negate(), this.MinValue.Negate(), this.nullState);
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				}

				// Token: 0x06008836 RID: 34870 RVA: 0x001CE2A8 File Offset: 0x001CC4A8
				public ListStatisticsOperations.BoundsEvaluator.Bounds Or(ListStatisticsOperations.BoundsEvaluator.Bounds other)
				{
					bool? asBoolean = this.AsBoolean;
					bool? asBoolean2 = other.AsBoolean;
					bool? flag = asBoolean;
					bool flag2 = true;
					if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
					{
						flag = asBoolean2;
						flag2 = true;
						if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
						{
							flag = asBoolean;
							flag2 = false;
							if ((flag.GetValueOrDefault() == flag2) & (flag != null))
							{
								flag = asBoolean2;
								flag2 = false;
								if ((flag.GetValueOrDefault() == flag2) & (flag != null))
								{
									return ListStatisticsOperations.BoundsEvaluator.Bounds.False;
								}
							}
							if (this.IsNull || other.IsNull)
							{
								return ListStatisticsOperations.BoundsEvaluator.Bounds.Null;
							}
							return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
						}
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.True;
				}

				// Token: 0x06008837 RID: 34871 RVA: 0x001CE34C File Offset: 0x001CC54C
				public ListStatisticsOperations.BoundsEvaluator.Bounds And(ListStatisticsOperations.BoundsEvaluator.Bounds other)
				{
					bool? asBoolean = this.AsBoolean;
					bool? asBoolean2 = other.AsBoolean;
					bool? flag = asBoolean;
					bool flag2 = false;
					if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
					{
						flag = asBoolean2;
						flag2 = false;
						if (!((flag.GetValueOrDefault() == flag2) & (flag != null)))
						{
							flag = asBoolean;
							flag2 = true;
							if ((flag.GetValueOrDefault() == flag2) & (flag != null))
							{
								flag = asBoolean2;
								flag2 = true;
								if ((flag.GetValueOrDefault() == flag2) & (flag != null))
								{
									return ListStatisticsOperations.BoundsEvaluator.Bounds.True;
								}
							}
							if (this.IsNull || other.IsNull)
							{
								return ListStatisticsOperations.BoundsEvaluator.Bounds.Null;
							}
							return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
						}
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.False;
				}

				// Token: 0x06008838 RID: 34872 RVA: 0x001CE3F0 File Offset: 0x001CC5F0
				public ListStatisticsOperations.BoundsEvaluator.Bounds Equal(ListStatisticsOperations.BoundsEvaluator.Bounds other)
				{
					if (this.IsMaybe || other.IsMaybe)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsConstant && other.IsConstant)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.From(this.MinValue.Equals(other.MinValue));
					}
					if (this.IsNull)
					{
						return other.EqualNull;
					}
					if (other.IsNull)
					{
						return this.EqualNull;
					}
					if (this.Kind != other.Kind)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.False;
					}
					if (this.MaxValue.LessThan(other.MinValue) || this.MinValue.GreaterThan(other.MaxValue))
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.False;
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				}

				// Token: 0x06008839 RID: 34873 RVA: 0x001CE4A4 File Offset: 0x001CC6A4
				public ListStatisticsOperations.BoundsEvaluator.Bounds NotEqual(ListStatisticsOperations.BoundsEvaluator.Bounds other)
				{
					if (this.IsMaybe || other.IsMaybe)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsConstant && other.IsConstant)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.From(!this.MinValue.Equals(other.MinValue));
					}
					if (this.IsNull)
					{
						return other.NotEqualNull;
					}
					if (other.IsNull)
					{
						return this.NotEqualNull;
					}
					if (this.Kind != other.Kind)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.True;
					}
					if (this.MaxValue.LessThan(other.MinValue) || this.MinValue.GreaterThan(other.MaxValue))
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.True;
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				}

				// Token: 0x0600883A RID: 34874 RVA: 0x001CE558 File Offset: 0x001CC758
				public ListStatisticsOperations.BoundsEvaluator.Bounds GreaterThan(ListStatisticsOperations.BoundsEvaluator.Bounds other)
				{
					if (this.IsMaybe || other.IsMaybe)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsNull || other.IsNull)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Null;
					}
					if (this.Kind != other.Kind)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsConstant && other.IsConstant)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.From(this.MinValue.CompareTo(other.MinValue) > 0);
					}
					if (this.MaxValue.LessThanOrEqual(other.MinValue))
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.False;
					}
					if (this.MinValue.GreaterThan(other.MaxValue))
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.True;
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				}

				// Token: 0x0600883B RID: 34875 RVA: 0x001CE60C File Offset: 0x001CC80C
				public ListStatisticsOperations.BoundsEvaluator.Bounds GreaterThanOrEqual(ListStatisticsOperations.BoundsEvaluator.Bounds other)
				{
					if (this.IsMaybe || other.IsMaybe)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsNull || other.IsNull)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Null;
					}
					if (this.Kind != other.Kind)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsConstant && other.IsConstant)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.From(this.MinValue.CompareTo(other.MinValue) >= 0);
					}
					if (this.MaxValue.LessThan(other.MinValue))
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.False;
					}
					if (this.MinValue.GreaterThanOrEqual(other.MaxValue))
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.True;
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				}

				// Token: 0x0600883C RID: 34876 RVA: 0x001CE6C0 File Offset: 0x001CC8C0
				public ListStatisticsOperations.BoundsEvaluator.Bounds LessThan(ListStatisticsOperations.BoundsEvaluator.Bounds other)
				{
					if (this.IsMaybe || other.IsMaybe)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsNull || other.IsNull)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Null;
					}
					if (this.Kind != other.Kind)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsConstant && other.IsConstant)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.From(this.MinValue.CompareTo(other.MinValue) < 0);
					}
					if (this.MaxValue.LessThan(other.MinValue))
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.True;
					}
					if (this.MinValue.GreaterThanOrEqual(other.MaxValue))
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.False;
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				}

				// Token: 0x0600883D RID: 34877 RVA: 0x001CE774 File Offset: 0x001CC974
				public ListStatisticsOperations.BoundsEvaluator.Bounds LessThanOrEqual(ListStatisticsOperations.BoundsEvaluator.Bounds other)
				{
					if (this.IsMaybe || other.IsMaybe)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsNull || other.IsNull)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Null;
					}
					if (this.Kind != other.Kind)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsConstant && other.IsConstant)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.From(this.MinValue.CompareTo(other.MinValue) <= 0);
					}
					if (this.MaxValue.LessThanOrEqual(other.MinValue))
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.True;
					}
					if (this.MinValue.GreaterThan(other.MaxValue))
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.False;
					}
					return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
				}

				// Token: 0x0600883E RID: 34878 RVA: 0x001CE828 File Offset: 0x001CCA28
				public ListStatisticsOperations.BoundsEvaluator.Bounds Union(ListStatisticsOperations.BoundsEvaluator.Bounds other)
				{
					if (this.IsMaybe || other.IsMaybe)
					{
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
					}
					if (this.IsNull || other.IsNull)
					{
						if (!this.IsNull)
						{
							return new ListStatisticsOperations.BoundsEvaluator.Bounds(this.Kind, this.boundKind, this.MinValue, this.MaxValue, ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.Undefined);
						}
						if (!other.IsNull)
						{
							return new ListStatisticsOperations.BoundsEvaluator.Bounds(other.Kind, other.boundKind, other.MinValue, other.MaxValue, ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.Undefined);
						}
						return ListStatisticsOperations.BoundsEvaluator.Bounds.Null;
					}
					else
					{
						if (this.Kind != other.Kind)
						{
							return ListStatisticsOperations.BoundsEvaluator.Bounds.Maybe;
						}
						bool? allNotNull = this.nullState;
						bool? allNotNull2 = ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.AllNotNull;
						bool? flag;
						if ((allNotNull.GetValueOrDefault() == allNotNull2.GetValueOrDefault()) & (allNotNull != null == (allNotNull2 != null)))
						{
							allNotNull2 = other.nullState;
							allNotNull = ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.AllNotNull;
							if ((allNotNull2.GetValueOrDefault() == allNotNull.GetValueOrDefault()) & (allNotNull2 != null == (allNotNull != null)))
							{
								flag = ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.AllNotNull;
								goto IL_0107;
							}
						}
						flag = ListStatisticsOperations.BoundsEvaluator.Bounds.NullState.Undefined;
						IL_0107:
						bool? flag2 = flag;
						Value value = ((this.MinValue.CompareTo(other.MinValue) < 0) ? this.MinValue : other.MinValue);
						Value value2 = ((this.MaxValue.CompareTo(other.MaxValue) > 0) ? this.MaxValue : other.MaxValue);
						return new ListStatisticsOperations.BoundsEvaluator.Bounds(this.Kind, ListStatisticsOperations.BoundsEvaluator.Bounds.BoundKind.Range, value, value2, flag2);
					}
				}

				// Token: 0x04004B73 RID: 19315
				public static readonly ListStatisticsOperations.BoundsEvaluator.Bounds Maybe = new ListStatisticsOperations.BoundsEvaluator.Bounds(ValueKind.None, Value.Null);

				// Token: 0x04004B74 RID: 19316
				public static readonly ListStatisticsOperations.BoundsEvaluator.Bounds False = new ListStatisticsOperations.BoundsEvaluator.Bounds(ValueKind.Logical, LogicalValue.False);

				// Token: 0x04004B75 RID: 19317
				public static readonly ListStatisticsOperations.BoundsEvaluator.Bounds True = new ListStatisticsOperations.BoundsEvaluator.Bounds(ValueKind.Logical, LogicalValue.True);

				// Token: 0x04004B76 RID: 19318
				public static readonly ListStatisticsOperations.BoundsEvaluator.Bounds Null = new ListStatisticsOperations.BoundsEvaluator.Bounds(ValueKind.Null, Value.Null);

				// Token: 0x04004B77 RID: 19319
				public readonly ValueKind Kind;

				// Token: 0x04004B78 RID: 19320
				private readonly ListStatisticsOperations.BoundsEvaluator.Bounds.BoundKind boundKind;

				// Token: 0x04004B79 RID: 19321
				private readonly bool? nullState;

				// Token: 0x04004B7A RID: 19322
				public readonly Value MinValue;

				// Token: 0x04004B7B RID: 19323
				public readonly Value MaxValue;

				// Token: 0x02001567 RID: 5479
				private enum BoundKind
				{
					// Token: 0x04004B7D RID: 19325
					Undefined,
					// Token: 0x04004B7E RID: 19326
					Range,
					// Token: 0x04004B7F RID: 19327
					Constant
				}

				// Token: 0x02001568 RID: 5480
				private static class NullState
				{
					// Token: 0x04004B80 RID: 19328
					public static bool? Undefined = null;

					// Token: 0x04004B81 RID: 19329
					public static bool? AllNull = new bool?(true);

					// Token: 0x04004B82 RID: 19330
					public static bool? AllNotNull = new bool?(false);
				}
			}
		}
	}
}
