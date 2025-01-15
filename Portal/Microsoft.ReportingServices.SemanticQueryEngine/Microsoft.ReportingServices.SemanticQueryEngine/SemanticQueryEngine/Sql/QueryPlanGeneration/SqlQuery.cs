using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x0200005D RID: 93
	internal abstract class SqlQuery
	{
		// Token: 0x06000456 RID: 1110 RVA: 0x000130D4 File Offset: 0x000112D4
		protected SqlQuery(QueryPlanBuilder qpBuilder)
		{
			this.m_qpBuilder = qpBuilder;
			this.m_queryID = this.m_qpBuilder.GetNextQueryID();
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001312B File Offset: 0x0001132B
		internal virtual void SelectExpression(ExpressionProcessInfo info, bool keyExpression)
		{
			this.AddExpressionToProcess(info);
			this.m_selectExpressions.Add(info);
			if (keyExpression)
			{
				this.m_keyExpressions.Add(info);
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00013150 File Offset: 0x00011350
		internal virtual SqlNestedQuery BuildQueryPlanSubtree()
		{
			this.ProcessExpressions();
			this.BuildQueryPlanSubtreeExt();
			for (int i = 0; i < this.m_nestedQueries.Count; i++)
			{
				SqlNestedQuery sqlNestedQuery = this.m_nestedQueries[i];
				SqlNestedQuery sqlNestedQuery2 = this.m_nestedQueries[i].BuildQueryPlanSubtree();
				if (sqlNestedQuery2 != null)
				{
					foreach (ExpressionProcessInfo expressionProcessInfo in this.m_selectExpressions)
					{
						if (expressionProcessInfo.NestedQuery == sqlNestedQuery)
						{
							expressionProcessInfo.NestedQuery = sqlNestedQuery2;
						}
					}
					this.m_nestedQueries.ReplaceAt(i, sqlNestedQuery2);
				}
			}
			return null;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000131FC File Offset: 0x000113FC
		internal SqlSelectQuery BuildSql(SqlBatch sqlBatch, bool applySort)
		{
			SqlSelectQuery sqlSelectQuery = this.CreateSqlSelectQuery(sqlBatch);
			for (int i = 0; i < this.NestedQueries.Count; i++)
			{
				this.JoinNestedQuery(this.NestedQueries[i], sqlSelectQuery, sqlBatch);
			}
			FunctionContext functionContext = new FunctionContext();
			for (int j = 0; j < this.SelectExpressions.Count; j++)
			{
				ExpressionProcessInfo expressionProcessInfo = this.SelectExpressions[j];
				sqlSelectQuery.SelectExpression(this.CreateSqlExpression(expressionProcessInfo, functionContext, sqlSelectQuery), expressionProcessInfo);
				if (functionContext.Count > 0)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
			}
			this.BuildFilterSql(sqlSelectQuery);
			this.ApplyGroupBy(sqlSelectQuery);
			if (applySort)
			{
				this.ApplySort(sqlSelectQuery);
			}
			return sqlSelectQuery;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000132A0 File Offset: 0x000114A0
		internal virtual void ApplySort(SqlSelectQuery selectQuery)
		{
			if (this.KeyExpressions.Count > 1)
			{
				for (int i = 0; i < selectQuery.SqlBatch.TopLevelSelectExpressions.Count; i++)
				{
					Expression objKey = selectQuery.SqlBatch.TopLevelSelectExpressions[i].ObjKey;
					if (this.KeyExpressions.Contains(objKey))
					{
						if (this.IsDetailsKey(objKey))
						{
							break;
						}
						selectQuery.OrderBy(objKey);
					}
				}
			}
		}

		// Token: 0x0600045B RID: 1115
		internal abstract bool IsDetailsKey(Expression keyExpressionObjKey);

		// Token: 0x0600045C RID: 1116 RVA: 0x0001330C File Offset: 0x0001150C
		internal virtual void Trace(FormattedStringWriter qpTracer)
		{
			qpTracer.IndentWriteLine(base.GetType().Name);
			this.TraceQueryExtInfo(qpTracer);
			qpTracer.IndentWriteLine("Expressions To Process:");
			int num = qpTracer.IndentationLevel + 1;
			qpTracer.IndentationLevel = num;
			for (int i = 0; i < this.m_expressionsToProcess.Count; i++)
			{
				ExpressionProcessInfo expressionProcessInfo = this.m_expressionsToProcess[i];
				qpTracer.IndentWriteLine("{0} {1} NQID={2} {3}", new object[]
				{
					this.m_selectExpressions.Contains(expressionProcessInfo) ? "S" : " ",
					this.TraceExpressionType(expressionProcessInfo),
					(expressionProcessInfo.NestedQuery != null) ? expressionProcessInfo.NestedQuery.QueryID.ToString("####00", CultureInfo.InvariantCulture) : "  ",
					expressionProcessInfo.ToString()
				});
			}
			for (int j = 0; j < this.m_selectExpressions.Count; j++)
			{
				ExpressionProcessInfo expressionProcessInfo2 = this.m_selectExpressions[j];
				if (!this.m_expressionsToProcess.Contains(expressionProcessInfo2))
				{
					qpTracer.IndentWriteLine("{0} {1} NQID={2} {3}", new object[]
					{
						"S",
						this.TraceExpressionType(expressionProcessInfo2),
						(expressionProcessInfo2.NestedQuery != null) ? expressionProcessInfo2.NestedQuery.QueryID.ToString("####00", CultureInfo.InvariantCulture) : "  ",
						expressionProcessInfo2.ToString()
					});
				}
			}
			num = qpTracer.IndentationLevel - 1;
			qpTracer.IndentationLevel = num;
			if (this.m_nestedQueries.Count > 0)
			{
				qpTracer.IndentWriteLine("Nested Queries: Count = {0}", new object[] { this.m_nestedQueries.Count.ToString("####00", CultureInfo.InvariantCulture) });
				for (int k = 0; k < this.m_nestedQueries.Count; k++)
				{
					qpTracer.IndentWriteLine("  {0}", new object[] { this.m_nestedQueries[k].QueryID.ToString("####00", CultureInfo.InvariantCulture) });
					num = qpTracer.IndentationLevel + 1;
					qpTracer.IndentationLevel = num;
					this.m_nestedQueries[k].Trace(qpTracer);
					num = qpTracer.IndentationLevel - 1;
					qpTracer.IndentationLevel = num;
				}
				return;
			}
			qpTracer.IndentWriteLine("Nested Queries: NONE");
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00013560 File Offset: 0x00011760
		internal ExpressionProcessInfoCollection SelectExpressions
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_selectExpressions;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x00013568 File Offset: 0x00011768
		internal ExpressionProcessInfoCollection KeyExpressions
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_keyExpressions;
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00013570 File Offset: 0x00011770
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00013578 File Offset: 0x00011778
		internal virtual bool PerformAggregation
		{
			[DebuggerStepThrough]
			get
			{
				return this.__performAggregation;
			}
			[DebuggerStepThrough]
			set
			{
				this.__performAggregation = value;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00013581 File Offset: 0x00011781
		internal QueryPlanBuilder QueryPlanBuilder
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_qpBuilder;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x00004555 File Offset: 0x00002755
		internal virtual bool IsWrapperQuery
		{
			[DebuggerStepThrough]
			get
			{
				return false;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00013589 File Offset: 0x00011789
		internal int QueryID
		{
			get
			{
				return this.m_queryID;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x00013591 File Offset: 0x00011791
		protected void AddExpressionToProcess(ExpressionProcessInfo info)
		{
			info.SetOwner(this);
			this.OptimizePathPointIndex(info);
			this.FinalizeExpressionProcessInfo(info);
			this.m_expressionsToProcess.Add(info);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected virtual void OptimizePathPointIndex(ExpressionProcessInfo info)
		{
		}

		// Token: 0x06000466 RID: 1126
		protected abstract void FinalizeExpressionProcessInfo(ExpressionProcessInfo info);

		// Token: 0x06000467 RID: 1127 RVA: 0x000135B4 File Offset: 0x000117B4
		protected virtual void PreprocessExpression(ExpressionProcessInfo info)
		{
			if (info.NestedQueryKey == null && info.NodeAsFunction != null)
			{
				if (info.IsInvokedAggregate)
				{
					info.CheckAggregateExpression(true);
					this.AddExpressionToProcess(ExpressionProcessInfo.CreateForAggregateArgument(info));
					return;
				}
				for (int i = 0; i < info.FunctionArguments.Count; i++)
				{
					this.AddExpressionToProcess(this.CreateExpressionInfoForFunctionArgument(info, info.FunctionArguments[i]));
				}
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001361D File Offset: 0x0001181D
		protected virtual ExpressionProcessInfo CreateExpressionInfoForFunctionArgument(ExpressionProcessInfo functionInfo, Expression functionArgument)
		{
			return ExpressionProcessInfo.CreateForFunctionArgument(functionInfo, functionArgument);
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00013628 File Offset: 0x00011828
		protected virtual void ProcessExpression(ExpressionProcessInfo info)
		{
			if (info.NestedQueryKey == null)
			{
				return;
			}
			SqlNestedQuery sqlNestedQuery = this.FindNestedQuery(info.NestedQueryKey);
			AggregationContext aggregationContext;
			if (sqlNestedQuery == null)
			{
				sqlNestedQuery = info.NestedQueryKey.CreatedNestedQuery(this.QueryPlanBuilder);
				aggregationContext = this.GetNestedAggregationContext(info, sqlNestedQuery.PerformAggregation);
				if (info.Path.GetCardinalityContext() == CardinalityContext.NonUniqueSet && aggregationContext == AggregationContext.Aggregate)
				{
					sqlNestedQuery = new SqlQueryOnNonUniqueSet(sqlNestedQuery);
				}
				this.NestedQueries.Add(sqlNestedQuery);
			}
			else
			{
				aggregationContext = this.GetNestedAggregationContext(info, sqlNestedQuery.PerformAggregation);
			}
			info.NestedQuery = sqlNestedQuery;
			ExpressionProcessInfo expressionProcessInfo = ExpressionProcessInfo.CreateForNestedQuery(info, aggregationContext);
			sqlNestedQuery.SelectExpression(expressionProcessInfo, false);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected virtual void BuildQueryPlanSubtreeExt()
		{
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000136BC File Offset: 0x000118BC
		protected SqlNestedQuery FindNestedQuery(NestedQueryKey key)
		{
			foreach (SqlNestedQuery sqlNestedQuery in this.m_nestedQueries)
			{
				if (sqlNestedQuery.Key != null && sqlNestedQuery.Key.IsReuseableBy(key))
				{
					return sqlNestedQuery;
				}
			}
			return null;
		}

		// Token: 0x0600046C RID: 1132
		protected abstract SqlSelectQuery CreateSqlSelectQuery(SqlBatch sqlBatch);

		// Token: 0x0600046D RID: 1133 RVA: 0x00013720 File Offset: 0x00011920
		protected virtual void JoinNestedQuery(SqlNestedQuery nestedQuery, SqlSelectQuery selectQuery, SqlBatch sqlBatch)
		{
			SqlSelectQuery sqlSelectQuery = nestedQuery.BuildSql(sqlBatch, false);
			nestedQuery.TableSourceInParentQuery = nestedQuery.Join(selectQuery, sqlSelectQuery);
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x00013744 File Offset: 0x00011944
		protected virtual SqlExpression CreateSqlExpression(ExpressionProcessInfo info, FunctionContext functionContext, SqlSelectQuery selectQuery)
		{
			SqlExpression sqlExpression;
			if (info.NodeAsAttributeRef != null || info.NodeAsEntityRef != null || info.NodeAsLiteral != null || info.NodeAsNull != null)
			{
				sqlExpression = this.CreateSqlExpressionForScalarValue(info, selectQuery);
			}
			else
			{
				if (info.NodeAsFunction == null)
				{
					throw SQEAssert.AssertFalseAndThrow("Unknown or invalid expression node.", Array.Empty<object>());
				}
				if (info.CheckAggregateExpression(false))
				{
					sqlExpression = this.CreateSqlExpressionForAggregate(info, functionContext, selectQuery);
				}
				else
				{
					sqlExpression = this.CreateSqlExpressionForScalarFunction(info, functionContext, selectQuery);
				}
			}
			return sqlExpression;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00003FB8 File Offset: 0x000021B8
		protected virtual void BuildFilterSql(SqlSelectQuery selectQuery)
		{
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x000137B6 File Offset: 0x000119B6
		protected virtual void ApplyGroupBy(SqlSelectQuery selectQuery)
		{
			if (this.PerformAggregation)
			{
				selectQuery.GroupBy = true;
			}
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x000137C7 File Offset: 0x000119C7
		protected virtual void TraceQueryExtInfo(FormattedStringWriter qpTracer)
		{
			qpTracer.IndentWriteLine("AGGREGATE={0}", new object[] { this.PerformAggregation });
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000137E8 File Offset: 0x000119E8
		protected virtual string TraceExpressionType(ExpressionProcessInfo info)
		{
			if (!this.m_keyExpressions.Contains(info))
			{
				return " ";
			}
			return "K";
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00013803 File Offset: 0x00011A03
		protected NestedQueryCollection NestedQueries
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_nestedQueries;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001380C File Offset: 0x00011A0C
		private void ProcessExpressions()
		{
			for (int i = 0; i < this.m_expressionsToProcess.Count; i++)
			{
				this.PreprocessExpression(this.m_expressionsToProcess[i]);
			}
			this.m_expressionsToProcess.Sort(new SqlQuery.NestedQueryOptimizingComparer());
			for (int j = 0; j < this.m_expressionsToProcess.Count; j++)
			{
				this.ProcessExpression(this.m_expressionsToProcess[j]);
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00013879 File Offset: 0x00011A79
		private AggregationContext GetNestedAggregationContext(ExpressionProcessInfo info, bool nestedQueryPerformAggregation)
		{
			if (info.AggregationContext == AggregationContext.Aggregate)
			{
				return AggregationContext.Inner;
			}
			if (info.AggregationContext == AggregationContext.Scalar && nestedQueryPerformAggregation)
			{
				return AggregationContext.Aggregate;
			}
			return info.AggregationContext;
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001389C File Offset: 0x00011A9C
		private SqlExpression CreateSqlExpressionForScalarValue(ExpressionProcessInfo info, SqlSelectQuery selectQuery)
		{
			if (info.AggregationContext != AggregationContext.Scalar || (info.NodeAsAttributeRef == null && info.NodeAsEntityRef == null && info.NodeAsLiteral == null && info.NodeAsNull == null))
			{
				throw SQEAssert.AssertFalseAndThrow("Invalid expression info: not a scalar value.", Array.Empty<object>());
			}
			if (info.NestedQuery != null)
			{
				return selectQuery.CreateSqlPassThruExpression(info, info.NestedQuery.TableSourceInParentQuery);
			}
			if (info.NodeAsLiteral != null)
			{
				return selectQuery.CreateSqlLiteralExpression(info);
			}
			if (info.NodeAsNull != null)
			{
				return selectQuery.CreateSqlNullExpression(info);
			}
			return selectQuery.CreateSqlPassThruExpression(info, selectQuery.PrimaryTableSource);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001392C File Offset: 0x00011B2C
		private SqlExpression CreateSqlExpressionForAggregate(ExpressionProcessInfo info, FunctionContext functionContext, SqlSelectQuery selectQuery)
		{
			info.CheckAggregateExpression(true);
			SqlExpression sqlExpression;
			if (info.NestedQuery != null)
			{
				sqlExpression = selectQuery.CreateSqlPassThruExpression(info, info.NestedQuery.TableSourceInParentQuery);
			}
			else
			{
				functionContext.Push(new FunctionContext.Frame(info.NodeAsFunction));
				SqlExpression[] array = this.CreateSqlExpressionsForFunctionArguments(info, functionContext, selectQuery);
				functionContext.Pop();
				if (array.Length != 1)
				{
					throw SQEAssert.AssertFalseAndThrow("Invalid number of aggregate arguments.", Array.Empty<object>());
				}
				sqlExpression = array[0];
			}
			return selectQuery.CreateSqlAggregateExpression(info, functionContext, sqlExpression);
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x000139A4 File Offset: 0x00011BA4
		private SqlExpression CreateSqlExpressionForScalarFunction(ExpressionProcessInfo info, FunctionContext functionContext, SqlSelectQuery selectQuery)
		{
			if (info.AggregationContext != AggregationContext.Scalar || info.NodeAsFunction == null || (info.NodeAsFunction.GetFunctionInfo().IsAggregate && info.NodeAsFunction.FunctionName != FunctionName.In))
			{
				throw SQEAssert.AssertFalseAndThrow("Specified info must be a scalar function.", Array.Empty<object>());
			}
			if (info.NestedQuery != null)
			{
				return selectQuery.CreateSqlPassThruExpression(info, info.NestedQuery.TableSourceInParentQuery);
			}
			functionContext.Push(new FunctionContext.Frame(info.NodeAsFunction));
			SqlExpression[] array = this.CreateSqlExpressionsForFunctionArguments(info, functionContext, selectQuery);
			functionContext.Pop();
			return selectQuery.CreateSqlFunctionExpression(info, functionContext, array);
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00013A38 File Offset: 0x00011C38
		private SqlExpression[] CreateSqlExpressionsForFunctionArguments(ExpressionProcessInfo functionInfo, FunctionContext functionContext, SqlSelectQuery selectQuery)
		{
			if (functionInfo.NodeAsFunction == null)
			{
				throw SQEAssert.AssertFalseAndThrow("functionInfo is not a function node.", Array.Empty<object>());
			}
			SqlExpression[] array = new SqlExpression[functionInfo.FunctionArguments.Count];
			for (int i = 0; i < array.Length; i++)
			{
				ExpressionProcessInfo expressionProcessInfo = this.m_expressionsToProcess[functionInfo.FunctionArguments[i]];
				if (expressionProcessInfo == null)
				{
					throw SQEAssert.AssertFalseAndThrow();
				}
				if (functionInfo.CheckAggregateExpression(false) && expressionProcessInfo.IsBlob(this.QueryPlanBuilder))
				{
					throw new SemanticQueryEngineException(SR.AggregateOfBinaryExpression);
				}
				if (functionContext.Current.IsInBooleanArgument)
				{
					throw SQEAssert.AssertFalseAndThrow("IsInBooleanArgument flag of function context is not cleared.", Array.Empty<object>());
				}
				functionContext.Current.IsInBooleanArgument = functionInfo.IsBooleanArgument(i);
				array[i] = this.CreateSqlExpression(expressionProcessInfo, functionContext, selectQuery);
				functionContext.Current.IsInBooleanArgument = false;
			}
			return array;
		}

		// Token: 0x040001E6 RID: 486
		private readonly QueryPlanBuilder m_qpBuilder;

		// Token: 0x040001E7 RID: 487
		private readonly ExpressionProcessInfoCollection m_selectExpressions = new ExpressionProcessInfoCollection();

		// Token: 0x040001E8 RID: 488
		private readonly ExpressionProcessInfoCollection m_keyExpressions = new ExpressionProcessInfoCollection();

		// Token: 0x040001E9 RID: 489
		private readonly ExpressionProcessInfoCollection m_expressionsToProcess = new ExpressionProcessInfoCollection();

		// Token: 0x040001EA RID: 490
		private readonly NestedQueryCollection m_nestedQueries = new NestedQueryCollection();

		// Token: 0x040001EB RID: 491
		private readonly int m_queryID;

		// Token: 0x040001EC RID: 492
		private bool __performAggregation;

		// Token: 0x020000D3 RID: 211
		private sealed class NestedQueryOptimizingComparer : Comparer<ExpressionProcessInfo>
		{
			// Token: 0x0600075C RID: 1884 RVA: 0x0001C83A File Offset: 0x0001AA3A
			internal NestedQueryOptimizingComparer()
			{
			}

			// Token: 0x0600075D RID: 1885 RVA: 0x0001C850 File Offset: 0x0001AA50
			public override int Compare(ExpressionProcessInfo objx, ExpressionProcessInfo objy)
			{
				NestedQueryKey nestedQueryKey = objx.NestedQueryKey;
				NestedQueryKey nestedQueryKey2 = objy.NestedQueryKey;
				if (nestedQueryKey != nestedQueryKey2)
				{
					if (nestedQueryKey == null)
					{
						return 1;
					}
					if (nestedQueryKey2 == null)
					{
						return -1;
					}
					int num = this.m_baseComparer.Compare(nestedQueryKey, nestedQueryKey2);
					if (num != 0)
					{
						return num;
					}
				}
				if (nestedQueryKey != null && nestedQueryKey2 != null)
				{
					int num2 = nestedQueryKey2.NonTransitiveCompatibilityPathLength.CompareTo(nestedQueryKey.NonTransitiveCompatibilityPathLength);
					if (num2 != 0)
					{
						return num2;
					}
				}
				return objx.SerialID.CompareTo(objy.SerialID);
			}

			// Token: 0x040003B3 RID: 947
			private readonly NestedQueryKey.OptimizingComparer m_baseComparer = new NestedQueryKey.OptimizingComparer();
		}
	}
}
